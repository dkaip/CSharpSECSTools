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
using System.Text;

#nullable enable

namespace com.CIMthetics.CSharpSECSTools.SECSCommUtils
{
	/// <summary>
	/// This class represents a SECS message header when a 
    /// SECS-II message is sent via an HSMS transport layer.
    /// Refer to the SEMI Standards covering E037 for more
	/// information.
	/// </summary>
	public class HSMSHeader : SECSHeader
	{
		/// <summary>
		/// This is the Session ID (Device ID) of the SECS-II message.
		/// </summary>
		public UInt16 SessionID { get; set; }

		/// <summary>
		/// This is the third byte (<c>[2]</c>) in an HSMS message header.
		/// In most situations this attribute should not be used.  Instead,
		/// use the <c>Wbit</c> and the <c>Stream</c> attributes. Situations
		/// where this might be used are in the cases where subsidiary standards
		/// are used as opposed to a "normal" SECS-II encoding. This will 
		/// probably be the case in the situation where <c>PType</c> is not
		/// equal to 0.
		/// </summary>
		public byte HeaderByte2 { get; set; } // If Data msg W-bit and Stream

		/// <summary>
		/// This is the W-Bit of the SECS-II message.
		/// </summary>
		public bool Wbit
		{
			get { return (HeaderByte2 & 0x80) != 0; }
			set
			{
				if (value)
					HeaderByte2 |= 0x80;
				else
					HeaderByte2 &= 0x7F;
			}
		}

		/// <summary>
		/// This is the Stream of the SECS-II message.
		/// </summary>
		public byte Stream
		{
			get { return (byte)(HeaderByte2 & 0x7F); }
			set
			{
				if (Wbit)
				{
					HeaderByte2 &= 0x80;
					HeaderByte2 |= (byte)(value & 0x7F);
				}
				else
				{
					HeaderByte2 = 0;
					HeaderByte2 |= (byte)(value & 0x7F);
				}
			}
		}

		/// <summary>
		/// This is the fourth byte (<c>[3]</c>) in an HSMS message header.
		/// In most situations this attribute should not be used.  Instead,
		/// use the <c>Wbit</c> and the <c>Stream</c> attributes. Situations
		/// where this might be used are in the cases where subsidiary standards
		/// are used as opposed to a "normal" SECS-II encoding.
		/// </summary>
		public byte HeaderByte3 { get; set; } // If Data msg Function

		/// <summary>
		/// This is the Function of the SECS-II message.
		/// </summary>
		public byte Function
		{
			get { return HeaderByte3; }
			set { HeaderByte3 = value; }
		}
		public PTypeValues PType { get; set; }
		public STypeValues SType { get; set; }

		public HSMSHeader()
		{
			SessionID = 0;
			HeaderByte2 = 0;
			HeaderByte3 = 0;
			PType = 0;
			SType = 0;
			SystemBytes = 0;
		}

		/// <summary>
		/// This is a copy constructor.
		/// </summary>
		public HSMSHeader(HSMSHeader header)
		{
			if (header == null)
			{
				throw new ArgumentNullException("Argument \"header\" may not be null.");
			}

			this.SessionID = header.SessionID;
			this.HeaderByte2 = header.HeaderByte2;
			this.HeaderByte3 = header.HeaderByte3;
			this.PType = header.PType;
			this.SType = header.SType;
			this.SystemBytes = header.SystemBytes;
		}

		/// <summary>
		/// This is a constructor that will create an <c>HSMSHeader</c> from a
		/// <c>SECS1Header</c>.
		/// <para/>
		/// The appropriate fields are copied across and the non-appropriate
		/// fields (Rbit, Ebit, BlockNumber) are not.  <c>PType</c> is set to
		/// indicate SECS-II encoding. <c>SType</c> is set to 0 indicating
		/// a Data Message as opposed to a Control Message.
		/// </summary>
		public HSMSHeader(SECSIHeader header)
		{
			if (header == null)
			{
				throw new ArgumentNullException("Argument \"header\" may not be null.");
			}

			this.SessionID = header.DeviceID;
			this.Wbit = header.Wbit;
			this.Stream = header.Stream;
			this.Function = header.Function;
			this.PType = 0;
			this.SType = 0;
			this.SystemBytes = header.SystemBytes;
		}

		/// <summary>
		/// Construct an <c>HSMSHeader</c> from a <c>byte[]</c>.  The first 10
		/// bytes of the <paramref name="header"/> argument are used to extract
		/// the header information.
		/// </summary>
		/// <param name="header">The HSMS message header in wire/transmission format.</param>
		public HSMSHeader(byte[] header)
		{
			if (header == null)
			{
				throw new ArgumentNullException("Argument \"header\" may not be null.");
			}

			if (header.Length < 10)
			{
				throw new ArgumentException("The length of argument \"header\" must be >= 10.");
			}

			SessionID = (UInt16)((header[0] << 8) + header[1]);
			HeaderByte2 = header[2];
			HeaderByte3 = header[3];
			PType = (PTypeValues)header[4];
			SType = (STypeValues)header[5];
			// The system bytes in wire format are Big Endian
			SystemBytes = (UInt32)((header[6] << 24) + (header[7] << 16) + (header[8] << 8) + (header[9]));
		}

		override public byte[] EncodeForTransport()
		{
			byte[] temp = new byte[10];

			// The session ID in wire format is Big Endian
			temp[0] = (byte)((SessionID & 0xFF00) >> 8);
			temp[1] = (byte)(SessionID & 0xFF);
			temp[2] = HeaderByte2;
			temp[3] = HeaderByte3;
			temp[4] = (byte)PType;
			temp[5] = (byte)SType;
			// The system bytes in wire format are Big Endian
			temp[6] = (byte)((SystemBytes & 0xFF000000) >> 24);
			temp[7] = (byte)((SystemBytes & 0x00FF0000) >> 16);
			temp[8] = (byte)((SystemBytes & 0x0000FF00) >> 8);
			temp[9] = (byte)(SystemBytes & 0x000000FF);

			return temp;
		}

		override public string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("SessionID: "); sb.Append(SessionID);
			sb.Append(", ");
			if (PType == 0)
			{
				// SECS-II Encoding
				if (SType == 0)
				{
					// It is a data message
					sb.Append("Data Message W: ");
					if ((HeaderByte2 & 0x80) != 0)
						sb.Append("1");
					else
						sb.Append("0");

					sb.Append(", ");
					sb.Append("Stream: "); sb.Append((int)(HeaderByte2 & 0x7F));
					sb.Append(", ");
					sb.Append("Function: "); sb.Append((int)HeaderByte3);
				}
				else
				{
					// It is a control message
					var sTypeValue = (STypeValues)SType;
					sb.Append("Control Message: "); sb.Append(sTypeValue.ToString());
					sb.Append(", ");
					sb.Append("Byte 2: "); sb.Append(HeaderByte2);
					sb.Append(", ");
					sb.Append("Byte 3: "); sb.Append(HeaderByte3);
				}

				sb.Append(", ");
				sb.Append("System Bytes: "); sb.Append(SystemBytes);
			}
			else
			{
				// Not SECS-II encoding just dump it out the best we can
				sb.Append("SessionID: "); sb.Append(SessionID.ToString("X"));
				sb.Append(", ");
				sb.Append("Byte 2: "); sb.Append(HeaderByte2.ToString("X"));
				sb.Append(", ");
				sb.Append("Byte 3: "); sb.Append(HeaderByte3.ToString("X"));
				sb.Append(", ");
				var pTypeValue = (PTypeValues)PType;
				sb.Append("PType: "); sb.Append(pTypeValue.ToString());
				var sTypeValue = (STypeValues)SType;
				sb.Append("SType: "); sb.Append(sTypeValue.ToString());
				sb.Append(", ");
				sb.Append("System Bytes: "); sb.Append(SystemBytes.ToString("X"));
			}

			return sb.ToString();
		}
	} // End class HSMSHeader

} // End namespace CIMthetics.SECSUtilities
