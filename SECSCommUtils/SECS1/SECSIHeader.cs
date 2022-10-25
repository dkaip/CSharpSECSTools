/*
 * Copyright 2019-2022 Douglas Kaip
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System.Data;

#nullable enable

namespace com.CIMthetics.CSharpSECSTools.SECSCommUtils
{
	/// <summary>
	/// This class represents a SECS message header when a 
    /// SECS-II message is sent via the SECS-I transport layer.
    /// Refer to the SEMI Standards covering E004 for more
    /// information.
	/// </summary>
	public class SECSIHeader : SECSHeader
	{
        /// <summary>
        /// Reverse Bit (R-Bit) - The reverse bit (R-bit) signifies the 
        /// direction of a message. The R-bit is set to <c>false</c> for messages 
        /// sent from the host to the equipment and set to <c>true</c> for messages 
        /// sent from the equipment to the host. The 
        /// R-bit is included in the header so that the direction of 
        /// the message is contained in every block.
        ///</summary>
		public bool Rbit { get; set; }

		// Done this way so there is no recursion in the setter method
		private UInt16 _deviceId = 0;
		/// <summary>
        /// Device ID - The device ID defines the source or destination
        /// of the message depending upon the value of the R-bit as shown
        /// in Table 3. Device identification is a property of the equipment
        /// and must be settable according to Section 8. The host can view
        /// the device ID as a logical identifier connected with a physical
        /// device within the equipment. The host has no device ID.
        /// </summary>
		public UInt16 DeviceID 
		{
			get
			{
				return _deviceId;
			}
			set
			{
				if ((value & 0x8000) != 0)
				{
					throw new ConstraintException("Acceptable values for DeviceID are 0 - 32767 inclusive.");
				}

				_deviceId = (UInt16)(value & 0x7FFF);
			}
		}

		/// <summary>
        /// Wait Bit (W-Bit) - The wait bit (W-bit) is used to indicate 
        /// that the sender of a primary message expects a reply. A 
        /// value of <c>true</c> in the W-bit means that a reply is expected. A
        /// value of <c>false</c> in the W-bit means that no reply is expected. 
        /// The W-bit must be set to <c>false</c> in all secondary messages. 
        /// For multi-block messages, the sender must ensure that the W-bit
        /// is the same in every block of the message.
        /// </summary>
		public bool Wbit { get; set; }

		// Done this way so there is no recursion in the setter method
		private byte _stream;
        /// <summary>
		/// The stream of the SECS-II message.
        /// </summary>
		public byte Stream
		{
			get
			{
				return _stream;
			}
			set
			{
				if ((value & 0x80) != 0)
				{
					throw new ConstraintException("Acceptable values for Stream are 0 - 127 inclusive.");
				}

				_stream = (byte)(value & (0x7F));
			}
		}

        /// <summary>
		/// The function of the SECS-II message.
        /// </summary>
		public byte Function { get; set; }

        /// <summary>
        /// End Bit (E-Bit) - The end bit (E-bit) is used to determine if a 
        /// block is the last block of a message. A value of one in the 
        /// E-bit means that the block is the last block. A value of zero 
        /// means that more blocks are to follow.
        /// </summary>
		public bool Ebit { get; set; }

		// Done this way so there is no recursion in the setter method
		private UInt16 _blockNumber = 0;
		/// <summary>
        /// Block Number - A message sent as more than one block is called 
        /// a multi-block message. The first block is given a block number 
        /// of one, and the block number is incremented by one for each 
        /// subsequent block until the entire message is sent. The blocks 
        /// of a multi-block message are sent in order. In a single-block 
        /// message, the block number must have a value of zero or one. The 
        /// maximum block number is 32,767. The upper block number is the 
        /// most significant portion of the block number.
        /// </summary>
		public UInt16 BlockNumber
		{
			get { return _blockNumber; }
			set
			{
				if ((value & 0x8000) != 0)
				{
					throw new ConstraintException("Acceptable values for BlockNumber are 0 - 32767 inclusive.");
				}

				_blockNumber = (UInt16)(value & 0x7FFF);
			}
		}

		public SECSIHeader()
		{
			Rbit = false;
			DeviceID = 0;
			Wbit = false;
			Stream = 0;
			Function = 0;
			Ebit = false;
			BlockNumber = 0;
			SystemBytes = 0;
		}

		/// <summary>
		/// This is a copy constructor.
		/// </summary>
		public SECSIHeader(SECSIHeader header)
		{
			if (header == null)
			{
				throw new ArgumentNullException("Argument \"header\" may not be null.");
			}

			this.Rbit = header.Rbit;
			this.DeviceID = header.DeviceID;
			this.Wbit = header.Wbit;
			this.Stream = header.Stream;
			this.Function = header.Function;
			this.Ebit = header.Ebit;
			this.BlockNumber = header.BlockNumber;
			this.SystemBytes = header.SystemBytes;
		}

		/// <summary>
		/// This constructor will create a SECS-I message header from an HSMS
		/// message header.  It needs to be understood that not all of the
		/// attributes are present in both forms of a SECS message header.
		/// There are only a few situations in which using this
		/// constructor would make sense.  It might make sense in a situation
		/// where an intermediary was "translating" messages from a device using
		/// SECS-I as a transport layer to a device using HSMS as a transport
		/// layer and vica versa.
		/// <para/>
		/// Note: A SessionID in an HSMS header is a 16 bit number whereas the
		/// DeviceID in a SECS-I header is a 15 bit number.  You will get a
		/// <c>ConstraintException</c> if the high bit in the SessionID is set
		/// when you use this constructor.
		/// </summary>
		public SECSIHeader(HSMSHeader header)
		{
			if (header == null)
			{
				throw new ArgumentNullException("Argument \"header\" may not be null.");
			}

			Rbit = false;
			DeviceID = 0;
			Wbit = false;
			Stream = 0;
			Function = 0;
			Ebit = false;
			BlockNumber = 0;
			SystemBytes = 0;

			/*
			    Not all of the fields / attributes of an HSMS message header
				apply to a SECS-I message header so we are just copying over
				the ones that do apply.
			*/
			this.DeviceID = header.SessionID;
			this.Wbit = header.Wbit;
			this.Stream = header.Stream;
			this.Function = header.Function;
			this.SystemBytes = header.SystemBytes;

			if ((header.SessionID & 0x8000) != 0)
			{
				throw new ConstraintException("The high bit of the SessionID was set. This has not been copied over to the DeviceID.");
			}
		}

        public SECSIHeader(byte[] header)
        {
			if (header == null)
			{
				throw new ArgumentNullException("Argument \"header\" may not be null.");
			}

			if (header.Length < 10)
			{
				throw new ArgumentException("The length of argument \"header\" must be >= 10.");
			}

			if ((header[0] & 0x80) != 0)
				Rbit = true;
			else
				Rbit = false;

			_deviceId = (UInt16)(((header[0] & 0x7F) << 8) + header[1]);

			if ((header[2] & 0x80) != 0)
				Wbit = true;
			else
				Wbit = false;

			_stream = (byte)(header[2] & 0x7F);

			Function = header[3];

			if ((header[4] & 0x80) != 0)
				Ebit = true;
			else
				Ebit = false;

			_blockNumber = (UInt16)(((header[4] & 0x7F) << 8) + header[5]);

			// The system bytes in wire format are Big Endian
			SystemBytes = (UInt32)((header[6] << 24) + (header[7] << 16) + (header[8] << 8) + (header[9]));
        }

		override public byte[] EncodeForTransport()
		{
			// Remember that SECS messages are Big Endian
			byte[] temp = new byte[10];

			temp[0] = (byte)((DeviceID & 0x7F00) >> 8);
			if ( Rbit == true )
				temp[0] |= 0x80;
			else
				temp[0] &= 0x7F;

			temp[1] = (byte)(DeviceID & 0xFF);

			temp[2] = Stream;
			if (Wbit == true)
				temp[2] |= 0x80;
			else
				temp[2] &= 0x7F;

			temp[3] = Function;

			temp[4] = (byte)((BlockNumber & 0x7F00) >> 8);
			if ( Ebit == true )
				temp[4] |= 0x80;
			else
				temp[4] &= 0x7F;
			temp[5] = (byte)(BlockNumber & 0xFF);

			// Remember SECS messages are Big Endian
			temp[6] = (byte)((SystemBytes & 0xFF000000) >> 24);
			temp[7] = (byte)((SystemBytes & 0x00FF0000) >> 16);
			temp[8] = (byte)((SystemBytes & 0x0000FF00) >> 8);
			temp[9] = (byte)(SystemBytes & 0x000000FF);

			return temp;
		} // End override public byte[] EncodeForTransport()

	} // End class SECS1Header

} // End namespace CIMthetics.SECSUtilities
