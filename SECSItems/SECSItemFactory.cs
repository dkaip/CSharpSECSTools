/*
 * Copyright 2023 Douglas Kaip
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

#nullable enable

namespace com.CIMthetics.CSharpSECSTools.SECSItems
{
    /// <summary>
    ///  This is a static &quot;factory&quot; class that is used to create a
	/// <c>SECSItem</c> object from a <c>byte[]</c> that is in &quot;wire/transmission&quot; form.
	/// As you might expect the
	/// bytes contained within the <c>byte[]</c> must actually be in the form
	/// of those of an item in a SECS-II message.
    /// </summary>
    public static class SECSItemFactory
    {
        /// <summary>
        /// Generates a <c>SECSItem</c> object from the <c>byte[]</c> that is passed to it
		/// starting with byte 0 of the <c>byte[]</c>.
        /// The data in the <c>byte[]</c> needs to be in &quot;wire/transmission&quot; format.
        /// </summary>
        /// <param name="data">An array of bytes that is in wire/transmission format.</param>
        /// <returns>The resulting <c>SECSItem</c>.</returns>
        public static SECSItem? GenerateSECSItem(byte[] data)
        {
            return GenerateSECSItem(data, 0);
        }

        /// <summary>
        /// Generates a <c>SECSItem</c> object from the <c>byte[]</c> that is passed to it.
		/// The parameter <paramref name="offset"/> specifies an offset into the supplied
		/// <c>byte[]</c> where the data for the <c>SECSItem</c> starts.
        /// The data in the <c>byte[]</c> needs to be in &quot;wire/transmission&quot; format.
        /// </summary>
		/// <remarks>
		/// This would be the desired method to use in the case where the <c>byte[]</c>
		/// also contains the header for a SECs message (located at the beginning of the
		/// <c>byte[]</c>).  In this situation, since a SECS message header is
		/// 10 bytes in length you would pass 10 in as the value of the <paramref name="offset"/>
		/// parameter.  This will cause this method to skip the first 10 bytes of the
		/// <c>byte[]</c> and extract / generate the <c>SECSItem</c> from the remaining bytes.
		/// </remarks>
        /// <returns>The resulting <c>SECSItem</c>.</returns>
        /// <param name="data">An array of bytes that is in wire/transmission format.</param>
        /// <param name="offset">The byte offset into <c>data</c> where the data for
        /// a <c>SECSITEM</c> starts.</param>
		public static SECSItem? GenerateSECSItem(byte[] data, int offset)
		{
			SECSItem? result = null;

			SECSItemFormatCode formatCode = SECSItemFormatCodeFunctions.GetSECSItemFormatCodeFromNumber((byte)((data[offset] >> 2) & 0x0000003F));
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
					result = new BinarySECSItem(data, offset);
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
						result = new I1ArraySECSItem(data, offset);
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
						result = new U1ArraySECSItem(data, offset);
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