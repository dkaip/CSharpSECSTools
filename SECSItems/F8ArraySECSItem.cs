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
using System.Linq;

namespace com.CIMthetics.CSharpSECSTools.SECSItems 
{
    /// <summary>
    /// This class represents/implements a SECSItem with the SECS data type of <c>F8</c>
    /// in an array form. In this case it is an array of zero or more 64-bit floating point 
    /// numbers.  From the C# side this data type is handled as a C# <c>double []</c>.
    /// 
    /// In wire/transmission format all SECS items, with the exception of those with an item format code
    /// of <c>L</c>(List), are sent in an array form. For instance if an item is received which has
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
	public class F8ArraySECSItem : SECSItem
	{
		private double[] value;

        /// <summary>
        /// This constructor creates a SECSItem that has a type of <c>F8</c> with
        /// the minimum number of length bytes required. Note: It will be created 
        /// with the number of length bytes required based on the length (in elements) 
        /// of the <c>double []</c> provided. The maximum array length allowed is 
        /// <c>16777215</c> bytes(elements).
        /// </summary>
        /// <param name="value">An array of c# <c>double</c> to be assigned to this SECSItem.</param>
		public F8ArraySECSItem(double[] value) : base(SECSItemFormatCode.F8, value.Length * 8)
		{
			this.value = value;
		}

        /// <summary>
        /// This constructor creates a SECSItem that has a type of <c>F8</c>
        /// with the specified value. This form of the constructor is not 
        /// needed much nowadays.  In the past there were situations where 
        /// the equipment required that messages contained SECSItems that 
        /// had a specified number of length bytes. This form of the constructor 
        /// is here to handle these problem child cases.
        /// 
        /// Note: It will be created with the number of length bytes set to greater of,
        /// the specified number of length bytes or the number required based on the 
        /// length (in elements) of the<code>double []</code> provided.
        /// </summary>
        /// <param name="value">The value to be assigned to this <c>SECSItem</c>.</param>
        /// <param name="desiredNumberOfLengthBytes">The number of length bytes to be used for 
        /// this <c>SECSItem</c>. The value for the number of length bytes must be 
        /// <c>ONE</c>, <c>TWO</c>, or <c>THREE</c>.</param>
		public F8ArraySECSItem(double[] value, SECSItemNumLengthBytes desiredNumberOfLengthBytes) : base(SECSItemFormatCode.F8, value.Length * 8, desiredNumberOfLengthBytes)
		{
			this.value = value;
		}

        /// <summary>
        /// This constructor is used to create this SECSItem from
        /// data in &quot;wire/transmission&quot; format.
        /// </summary>
        /// <param name="data">The buffer where the &quot;wire/transmission&quot; format data is contained.</param>
        /// <param name="itemOffset">The offset into the data where the desired item starts.</param>
		public F8ArraySECSItem(byte[] data, int itemOffset) : base(data, itemOffset)
		{
            int offset = 1 + inboundNumberOfLengthBytes.ValueOf () + itemOffset;
            bytesConsumed = 1 + inboundNumberOfLengthBytes.ValueOf () + lengthInBytes;
			if ((lengthInBytes == 0) || ((lengthInBytes % 8) != 0))
                throw new ArgumentOutOfRangeException("Illegal data length of: " + lengthInBytes + " payload length must be a non-zero multiple of 8.");

			value = new double[lengthInBytes / 8];
			byte[] temp = new byte[8];
			for (int i = offset, j = 0; j < value.Length; i += 8, j++)
			{
				temp[0] = data[i];
				temp[1] = data[i+1];
				temp[2] = data[i+2];
				temp[3] = data[i+3];
				temp[4] = data[i+4];
				temp[5] = data[i+5];
				temp[6] = data[i+6];
				temp[7] = data[i+7];

				if (BitConverter.IsLittleEndian)
					Array.Reverse(temp);

				value[j] = BitConverter.ToDouble(temp, 0);
			}
		}

        /// <summary>
        /// Gets the value of this <c>SECSItem</c>.
        /// </summary>
        /// <returns>the value of the <c>SECSItem</c>.</returns>
		public double[] GetValue()
		{
			return value;
		}

        /// <summary>
        /// Creates and returns a <c>byte[]</c> that represents this <c>SECSItem</c> in &quot;wire/transmission format&quot;.
        /// </summary>
        /// <returns>A <c>byte[]</c> representation of this <c>SECSItem</c>'s content that is suitable for transmission.</returns>
		public override byte[] ToRawSECSItem()
		{
			byte[] output = new byte[OutputHeaderLength()+(value.Length * 8)];
			int offset = PopulateSECSItemHeaderData(output, (value.Length * 8));

			for( int i = offset, j = 0; j < value.Length; i+=8, j++ )
			{
				byte[] temp = BitConverter.GetBytes(value[j]);

				if (BitConverter.IsLittleEndian)
					Array.Reverse(temp);

				output[i]   = temp[0];
				output[i+1] = temp[1];
				output[i+2] = temp[2];
				output[i+3] = temp[3];
				output[i+4] = temp[4];
				output[i+5] = temp[5];
				output[i+6] = temp[6];
				output[i+7] = temp[7];
			}

			return output;
		}

        /// <summary>
        /// Returns a <c>string</c> representation of this item in a format
        /// suitable for debugging.
        /// </summary>
        /// <returns>a <c>string</c> representation of this item in a format suitable for debugging.</returns>
		public override String ToString()
		{
			return "Format:" + formatCode.ToString() + " Value: Array";
		}

        /// <summary>
        /// Serves as a hash function for a <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.F8ArraySECSItem"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
        /// hash table.</returns>
		public override int GetHashCode()
		{
			return value.GetHashCode();
		}

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.F8ArraySECSItem"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.F8ArraySECSItem"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
        /// <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.F8ArraySECSItem"/>; otherwise, <c>false</c>.</returns>
		public override bool Equals(Object obj)
		{
			if (this == obj)
				return true;
			if (obj == null)
				return false;
			if (GetType() != obj.GetType())
				return false;
			F8ArraySECSItem other = (F8ArraySECSItem) obj;
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

