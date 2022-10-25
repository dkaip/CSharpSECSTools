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
using System.Text;

namespace com.CIMthetics.CSharpSECSTools.SECSItems 
{
    /// <summary>
    /// This class represents/implements a <c>SECSItem</c> with the SECS data type of <c>A</c> 
    /// (<c>ASCII</c>),
    /// which is a &quot;string&quot; of<c>ASCII</c> characters.From the C# side this data
    /// type is handled as a C# <c>string</c>. Be careful to only use appropriate character sets
    /// (System.Text.Encoding.ASCII) or undesirable behavior may be manifested.
    /// </summary>
	public class ASCIISECSItem : SECSItem
	{
		private string value = "";

        /// <summary>
        /// This constructor creates a SECSItem that has a type of <code>A</code> with 
        /// the minimum number of length bytes required. Note: It will be created with 
        /// the number of length bytes required based on the length (in characters) of 
        /// the <c>string</c> provided. The maximum string length allowed is 
        /// <c>16777215</c> characters.
        /// </summary>
        /// <param name="value">The value to be assigned to this SECSItem.</param>
        public ASCIISECSItem(string value) : base(SECSItemFormatCode.A, value == null ? 0 : value.Length)
		{
            if (value != null)
    			this.value = value;
		}

        /// <summary>
        /// This constructor creates a SECSItem that has a type of <code>A</code> with 
        /// a specified number of length bytes. This form of the constructor is not 
        /// needed much nowadays.  In the past there were situations where the equipment 
        /// required that messages contained SECSItems that had a specified number of 
        /// length bytes. This form of the constructor is here to handle these problem child cases.
        /// Note: It will be created with the number of length bytes set 
        /// to the greater of the number of length bytes specified or
        /// the number required based on the length of the <c>value</c> parameter.
        /// </summary>
        /// <param name="value">The value to be assigned to this SECSItem.</param>
        /// <param name="desiredNumberOfLengthBytes">The number of length bytes to be used for this SECSItem.</param>
        public ASCIISECSItem(string value, SECSItemNumLengthBytes desiredNumberOfLengthBytes) : base(SECSItemFormatCode.A, value == null ? 0 : value.Length, desiredNumberOfLengthBytes)
		{
            if (value != null)
                this.value = value;
		}

        /// <summary>
        /// This constructor is used to create this SECSItem from
        /// data in &quot;wire/transmission&quot; format.
        /// </summary>
        /// <param name="data">The buffer where the &quot;wire/transmission&quot; format data is contained.</param>
        /// <param name="itemOffset">The offset into the data where the desired item starts.</param>
		public ASCIISECSItem(byte[] data, int itemOffset) : base(data, itemOffset)
		{
            int offset = 1 + inboundNumberOfLengthBytes.ValueOf () + itemOffset;
            bytesConsumed = 1 + inboundNumberOfLengthBytes.ValueOf () + lengthInBytes;

			byte[] temp = new byte[lengthInBytes];
			Array.Copy(data, offset, temp, 0, lengthInBytes);
			value = System.Text.Encoding.ASCII.GetString(temp);
		}

        /// <summary>
        /// Gets the value of this <c>ASCIISECSItem</c>.
        /// </summary>
        /// <returns>The value of this <c>ASCIISECSItem</c>.</returns>
		public string GetValue()
		{
			return value;
		}

        /// <summary>
        /// Creates and returns a <c>byte[]</c> that represents this <c>SECSItem</c> in &quot;wire/transmission&quot; format.
        /// </summary>
        /// <returns>A <c>byte[]</c> representation of this <c>SECSItem</c>'s content that is suitable for transmission.</returns>
		public override byte[] EncodeForTransport()
		{
			byte[] output = new byte[OutputHeaderLength()+value.Length];
            int offset = PopulateSECSItemHeaderData(output, value.Length);

			byte[] temp = Encoding.ASCII.GetBytes(value);

			Array.Copy(temp, 0, output, offset, temp.Length);

			return output;
		}

        /// <summary>
        /// Returns a String representation of this item in a format
        /// suitable for debugging.
        /// </summary>
        /// <returns>A string representation of this item in a format suitable for debugging.</returns>
		public override String ToString()
		{
			return "Format:" + formatCode.ToString() + " Value: " + value;
		}

        /// <summary>
        /// Serves as a hash function for a <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.ASCIISECSItem"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
        /// hash table.</returns>
		public override int GetHashCode()
		{
			return value.GetHashCode();
		}

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.ASCIISECSItem"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.ASCIISECSItem"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
        /// <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.ASCIISECSItem"/>; otherwise, <c>false</c>.</returns>
		public override bool Equals(Object obj)
		{
			if (this == obj)
				return true;
			if (obj == null)
				return false;
			if (GetType() != obj.GetType())
				return false;
			ASCIISECSItem other = (ASCIISECSItem) obj;
			if (value != other.value)
				return false;
			return true;
		}
	}
}
