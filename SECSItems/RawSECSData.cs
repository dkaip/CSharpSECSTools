/*
 * Copyright 2019 Douglas Kaip
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
using System;

namespace com.CIMthetics.CSharpSECSTools.SECSItems
{
	/// <summary>
	/// This "raw" SECS Item layer has been created because the transport layers
	/// used (SECS I or HSMS) to transport SECS II messages do not really care 
	///	 about the payload of the messages they are transporting.
	/// </summary>
	public class RawSECSData
	{
		private byte[] data;

		/// <summary>
		/// Initializes a new instance of the <see cref="RawSECSData"/> class.
		/// This constructor will most likely be used just before a SECS II 
		/// message is to be sent out on a SECS I serial line or a HSMS connection.
		/// </summary>
		/// <param name="secsItem">Secs item.</param>
		public RawSECSData(SECSItem secsItem)
		{
			data = secsItem.toRawSECSItem();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RawSECSData"/> class.
		///  This constructor will most likely be used to contain raw SECS II
		/// messages that are received from a SECS I serial line or from a
		/// HSMS network connection.
		/// </summary>
		/// <param name="incomingData">Incoming data.</param>
		public RawSECSData(byte[] incomingData)
		{
			data = incomingData;
		}

		/// <summary>
		/// Gets the data.
		/// </summary>
		/// <returns>The data.</returns>
		public byte[] getData()
		{
			return data;
		}

		public static SECSItem generateSECSItem(byte[] data, int offset)
		{
			SECSItem result = null;

			SECSItemFormatCode formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)((data[offset] >> 2) & 0x0000003F));
			int numberOfLengthBytes = (data[offset] & 0x03);
			int incomingDataLength = 0;

			switch (numberOfLengthBytes)
			{
				case 1:
					{
						byte[] temp = new byte[4];
						temp[0] = 0;
						temp[1] = 0;
						temp[2] = 0;
						temp[3] = data[offset +1];

						if (BitConverter.IsLittleEndian)
							Array.Reverse(temp);

						incomingDataLength = BitConverter.ToInt32(temp, 0);
						break;
					}
				case 2:
					{
						byte[] temp = new byte[4];
						temp[0] = 0;
						temp[1] = 0;
						temp[2] = data[offset+1];
						temp[3] = data[offset+2];
						if (BitConverter.IsLittleEndian)
							Array.Reverse(temp);

						incomingDataLength = BitConverter.ToInt32(temp, 0);
						break;
					}
				case 3:
					{
						byte[] temp = new byte[4];
						temp[0] = 0;
						temp[1] = data[offset+1];
						temp[2] = data[offset+2];
						temp[3] = data[offset+3];
						if (BitConverter.IsLittleEndian)
							Array.Reverse(temp);

						incomingDataLength = BitConverter.ToInt32(temp, 0);
						break;
					}
			}


			switch(formatCode)
			{
				case SECSItemFormatCode.L:
					result = new ListSECSItem(data, offset);
					break;
				case SECSItemFormatCode.B:
					result = new BinarySECSItem(data, offset, 0);  // Remember to use the proper constructor for this case
					break;
				case SECSItemFormatCode.BO:
					if (incomingDataLength == 1)
						result = new BooleanSECSItem(data, offset);
					else
						result = new BooleanArraySECSItem(data, offset);
					break;
				case SECSItemFormatCode.A:
					result = new ASCIISECSItem(data, offset);
					break;
				case SECSItemFormatCode.J8:
					break;
				case SECSItemFormatCode.C2:
					break;
				case SECSItemFormatCode.I8:
					if (incomingDataLength == 8)
						result = new I8SECSItem(data, offset);
					else
						result = new I8ArraySECSItem(data, offset);
					break;
				case SECSItemFormatCode.I1:
					if (incomingDataLength == 1)
						result = new I1SECSItem(data, offset);
					else
						result = new I1ArraySECSItem(data, offset, 0); // Need to use this version because of method signature issues
					break;
				case SECSItemFormatCode.I2:
					if (incomingDataLength == 2)
						result = new I2SECSItem(data, offset);
					else
						result = new I2ArraySECSItem(data, offset);
					break;
				case SECSItemFormatCode.I4:
					if (incomingDataLength == 4)
						result = new I4SECSItem(data, offset);
					else
						result = new I4ArraySECSItem(data, offset);
					break;
				case SECSItemFormatCode.F8:
					if (incomingDataLength == 8)
						result = new F8SECSItem(data, offset);
					else
						result = new F8ArraySECSItem(data, offset);
					break;
				case SECSItemFormatCode.F4:
					if (incomingDataLength == 4)
						result = new F4SECSItem(data, offset);
					else
						result = new F4ArraySECSItem(data, offset);
					break;
				case SECSItemFormatCode.U8:
					if (incomingDataLength == 8)
						result = new U8SECSItem(data, offset);
					else
						result = new U8ArraySECSItem(data, offset);
					break;
				case SECSItemFormatCode.U1:
					if (incomingDataLength == 1)
						result = new U1SECSItem(data, offset);
					else
						result = new U1ArraySECSItem(data, offset, 0);  // Remember to use the proper constructor for this case
					break;
				case SECSItemFormatCode.U2:
					if (incomingDataLength == 2)
						result = new U2SECSItem(data, offset);
					else
						result = new U2ArraySECSItem(data, offset);
					break;
				case SECSItemFormatCode.U4:
					if (incomingDataLength == 4)
						result = new U4SECSItem(data, offset);
					else
						result = new U4ArraySECSItem(data, offset);
					break;
				case SECSItemFormatCode.UNDEFINED:
					break;
				case SECSItemFormatCode.HeaderOnly:
					break;
				default:
					break;
			}

			return result;
		}

	}
}

