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
using System;
using System.Linq;

namespace com.CIMthetics.CSharpSECSTools.SECSItems 
{
    /// <summary>
    /// This class represents/implements a SECSItem with the SECS data type of <c>U2</c>
    /// in an array form. In this case it is an array of zero or more 16-bit unsigned 
    /// integers.From the C# side this data type is handled as a C# <code>UInt16 []</code>.
    /// 
    /// In wire/transmission format all SECS items, with the exception of those with an item format code
    /// of <c>L</c>(List), are sent in an array form.For instance if an item is received which has
    /// an item format code of I4 (32-bit signed integer) and has a length of 8 you know that is it an array
    /// containing 2 4-byte signed integers.  If it has a length of 0 you know it is an array with zero
    /// 4-byte signed integers. Likewise, If an item is received which has
    /// an item format code of U2 (16-bit unsigned integer) and has a length of 6 you know that is it an array
    /// containing 3 2-byte unsigned integers.
    /// 
    /// In practice, when only one item is received in the array (in the I4 case mentioned previously if the 
    /// length was 4 instead of 8) it is handled and processed in a non array form.  Hence <c>I4SECSItem</c>
    /// vs <c>I4ArraySECSItem</c>, <c>U2SECSItem</c> vs <c>U2ArraySECSItem</c>, etc.
    /// </summary>
	public class U2ArraySECSItem : SECSItem
	{
		private UInt16[] value;

        /// <summary>
        /// This constructor creates a SECSItem that has a type of <c>U2</c> with
        /// the minimum number of length bytes required.
        /// Note: It will be created with the number of length bytes required
        /// based on the length (in elements) of the <c>UInt16 []</c> provided.
        /// The maximum array length allowed is <c>16777215</c> bytes(elements).
        /// </summary>
        /// <param name="value">An array of <c>UInt16</c> values to be assigned to this <c>SECSItem</c>.</param>
		public U2ArraySECSItem(UInt16[] value) : base(SECSItemFormatCode.U2, value.Length * 2)
		{
			this.value = value;
		}

        /// <summary>
        /// This constructor creates a SECSItem that has a type of <c>U2</c>
        /// with the specified value.
        /// 
        /// This form of the constructor is not needed much nowadays.  In the past
        /// there were situations where the equipment required that messages
        /// contained SECSItems that had a specified number of length bytes.
        /// This form of the constructor is here to handle these problem child cases.
        /// 
        /// Note: It will be created with the number of length bytes set to greater of,
        /// the specified number of length bytes or the number required based on the 
        /// length (in elements) of the <c>UInt16 []</c> provided.
        /// </summary>
        /// <param name="value">The value to be assigned to this <c>SECSItem</c>.</param>
        /// <param name="desiredNumberOfLengthBytes">The number of length bytes to be used for this <c>SECSItem</c>.
        /// The value for the number of length bytes must be <c>ONE</c>, <c>TWO</c>, or <c>THREE</c>.</param>
		public U2ArraySECSItem(UInt16[] value, SECSItemNumLengthBytes desiredNumberOfLengthBytes) : base(SECSItemFormatCode.U2, value.Length * 2, desiredNumberOfLengthBytes)
		{
			this.value = value;
		}

        /// <summary>
        /// This constructor is used to create this SECSItem from
        /// data in &quot;wire/transmission&quot; format.
        /// </summary>
        /// <param name="data">The buffer where the &quot;wire/transmission&quot; format data is contained.</param>
        /// <param name="itemOffset">The offset into the data where the desired item starts.</param>
		public U2ArraySECSItem(byte[] data, int itemOffset) : base(data, itemOffset)
		{
            int offset = 1 + inboundNumberOfLengthBytes.ValueOf () + itemOffset;
            bytesConsumed = 1 + inboundNumberOfLengthBytes.ValueOf () + lengthInBytes;
			if ((lengthInBytes == 0) || ((lengthInBytes % 2) != 0))
                throw new ArgumentOutOfRangeException("Illegal data length of: " + lengthInBytes + " payload length must be a non-zero multiple of 2.");

			value = new UInt16[lengthInBytes / 2];
			byte[] temp = new byte[2];
			for (int i = offset, j = 0; j < value.Length; i += 2, j++)
			{
				temp[0] = data[i];
				temp[1] = data[i+1];

				if (BitConverter.IsLittleEndian)
					Array.Reverse(temp);

				value[j] = BitConverter.ToUInt16(temp, 0);
			}
		}

        /// <summary>
        /// Gets the value of this <c>SECSItem</c>.
        /// </summary>
        /// <returns>the value of the <c>SECSItem</c>.</returns>
		public UInt16[] GetValue()
		{
			return value;
		}

        /// <summary>
        /// Creates and returns a <c>byte[]</c> that represents this <c>SECSItem</c> in &quot;wire/transmission format&quot;.
        /// </summary>
        /// <returns>A <c>byte[]</c> representation of this <c>SECSItem</c>'s content that is suitable for transmission.</returns>
		public override byte[] EncodeForTransport()
		{
			byte[] output = new byte[OutputHeaderLength()+(value.Length * 2)];
			int offset = PopulateSECSItemHeaderData(output, (value.Length * 2));

			for( int i = offset, j = 0; j < value.Length; i+=2, j++ )
			{
				byte[] temp = BitConverter.GetBytes(value[j]);

				if (BitConverter.IsLittleEndian)
					Array.Reverse(temp);

				output[i]   = temp[0];
				output[i+1] = temp[1];
			}

			return output;
		}

        /// <summary>
        /// Returns a <c>string</c> representation of this item in a format
        /// suitable for debugging.
        /// </summary>
        /// <returns>A <c>string</c> representation of this item in a format suitable for debugging.</returns>
		public override String ToString()
		{
			return "Format:" + formatCode.ToString() + " Value: Array";
		}

        /// <summary>
        /// Serves as a hash function for a <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.U2ArraySECSItem"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
        /// hash table.</returns>
		public override int GetHashCode()
		{
			return value.GetHashCode();
		}

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.U2ArraySECSItem"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.U2ArraySECSItem"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
        /// <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.U2ArraySECSItem"/>; otherwise, <c>false</c>.</returns>
		public override bool Equals(Object obj)
		{
			if (this == obj)
				return true;
			if (obj == null)
				return false;
			if (GetType() != obj.GetType())
				return false;
			U2ArraySECSItem other = (U2ArraySECSItem) obj;
			if (value == null && other.value == null)
				return true;
			if (value == null)
			{
				if (other.value != null)
					return false;
			}
			if (other.value == null)
			{
				if (value != null)
					return false;
			}
			if (value.Length != other.value.Length)
				return false;

			return value.SequenceEqual(other.value);
		}
	}
}

