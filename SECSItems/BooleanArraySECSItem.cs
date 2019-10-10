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
    /// This class represents/implements a SECSItem with the SECS data type of <c>BO</c>
    /// in an array form. In this case it is an array of zero or more 8-bit boolean values.
    /// From the C# side this data type is handled as a C# <c>bool []</c>.
    /// 
    /// In &quot;wire/transmission&quot; format all SECS items, with the exception of those 
    /// with an item format code of <c>L</c>(List), are sent in an array form.For instance 
    /// if an item is received which has an item format code of I4 (32-bit signed integer) 
    /// and has a length of 8 you know that is it an array containing 2 4-byte signed integers.
    /// If it has a length of 0 you know it is an array with zero 4-byte signed integers. 
    /// Likewise, If an item is received which has an item format code of U2 (16-bit unsigned integer) 
    /// and has a length of 6 you know that is it an array containing 3 2-byte unsigned integers.
    /// 
    /// In practice, when only one item is received in the array (in the I4 case mentioned previously 
    /// if the length was 4 instead of 8) it is handled and processed in a non array form.  Hence 
    /// <c>I4SECSItem</c> * vs <c>I4ArraySECSItem</c>, <c>U2SECSItem</c> vs <c>U2ArraySECSItem</c>, etc.
    /// </summary>
	public class BooleanArraySECSItem : SECSItem
	{
		private bool[] value = null;

        /// <summary>
        /// This constructor creates a SECSItem that has a type of <c>BO</c> with 
        /// the minimum number of length bytes required. Note: It will be created 
        /// with the number of length bytes required based on the length (in elements) 
        /// of the <c>bool []</c> provided. The maximum array length allowed is 
        /// <c>16777215</c> bytes(elements).
        /// </summary>
        /// <param name="value">An array of <c>bool</c> values to be assigned to this SECSItem.</param>
		public BooleanArraySECSItem(bool[] value) : base(SECSItemFormatCode.BO, value.Length)
		{
			this.value = value;
		}

        /// <summary>
        /// This constructor creates a <c>SECSItem</c> that has a type of <c>BO</c> with the specified value.
        /// This form of the constructor is not needed much nowadays.  In the past there were situations 
        /// where the equipment required that messages contained <c>SECSItem</c>s that had a specified number 
        /// of length bytes. This form of the constructor is here to handle these problem child cases.
        /// 
        /// Note: It will be created with the number of length bytes set to greater of,
        /// the specified number of length bytes or the number required based on the 
        /// length (in elements) of the <c>bool []</c> provided.
        /// </summary>
        /// <param name="value">The value to be assigned to this <c>SECSItem</c>.</param>
        /// <param name="desiredNumberOfLengthBytes">The number of length bytes to be used for this <c>SECSItem</c>.
        /// The value for the number of length bytes must be<c> ONE</c>, <c>TWO</c>, or <c>THREE</c>.</param>
        public BooleanArraySECSItem(bool[] value, SECSItemNumLengthBytes desiredNumberOfLengthBytes) : base(SECSItemFormatCode.BO, value.Length, desiredNumberOfLengthBytes)
		{
			this.value = value;
		}

        /// <summary>
        /// This constructor is used to create this <c>SECSItem</c> from data in &quot;wire/transmission&quot;
        /// format. Note: Zero values are interpreted as <c>false</c> and non-zero values are interpreted as 
        /// <c>true</c>.
        /// </summary>
        /// <param name="data">The buffer where the wire/transmission format data is contained.</param>
        /// <param name="itemOffset">The offset into the data where the desired item starts.</param>
		public BooleanArraySECSItem(byte[] data, int itemOffset) : base(data, itemOffset)
		{
            int offset = 1 + inboundNumberOfLengthBytes.ValueOf () + itemOffset;
            bytesConsumed = 1 + inboundNumberOfLengthBytes.ValueOf () + lengthInBytes;

			value = new bool[lengthInBytes];
			for(int i = 0, j = offset; i < value.Length; i++, j++)
			{
				if (data[j] == 0)
					value[i] = false;
				else
					value[i] = true;
			}
		}

        /// <summary>
        /// Gets the value of this <c>SECSItem</c> in a C# friendly format.
        /// </summary>
        /// <returns>A C# <c>bool[]</c> that contains this item's value.</returns>
		public bool[] GetValue()
		{
			return value;
		}

        /// <summary>
        /// Creates and returns a <c>byte[]</c> that represents this <c>SECSItem</c> in &quot;wire/transmission format&quot;.
        /// </summary>
        /// <returns>A <c>byte[]</c> representation of this <c>SECSItem</c>'s content that is suitable for transmission.</returns>
		public override byte[] ToRawSECSItem()
		{
			byte[] output = new byte[OutputHeaderLength()+value.Length];
			int offset = PopulateSECSItemHeaderData(output, value.Length);

			for( int i = offset, j = 0; j < value.Length; i++, j++)
			{
				if (value[j] == false)
					output[i] = 0;
				else
					output[i] = 1;
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
        /// Serves as a hash function for a
        /// <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.BooleanArraySECSItem"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
        /// hash table.</returns>
		public override int GetHashCode()
		{
			return value.GetHashCode();
		}

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.BooleanArraySECSItem"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.BooleanArraySECSItem"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
        /// <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.BooleanArraySECSItem"/>; otherwise, <c>false</c>.</returns>
		public override bool Equals(Object obj)
		{
			if (this == obj)
				return true;
			if (obj == null)
				return false;
			if (GetType() != obj.GetType())
				return false;
			BooleanArraySECSItem other = (BooleanArraySECSItem) obj;
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

