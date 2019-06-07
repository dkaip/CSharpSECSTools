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
using System.Text;

namespace com.CIMthetics.CSharpSECSTools.SECSItems 
{
    /// <summary>
    /// This class represents/implements a SECSItem with the SECS data type of <code>A</code>(<code>ASCII</code>),
    /// which is a &quot;string&quot; of<code> ASCII</code> characters.From the C# side this data
    /// type is handled as a C# <code>string</code>.Be careful to only use ASCII characters or undesirable
    /// behavior may be manifested.
    /// </summary>
	public class ASCIISECSItem : SECSItem
	{
		private readonly string value;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.ASCIISECSItem"/> class.
        ///  This constructor creates a SECSItem that has a type of <code>A</code> with 
        ///  the minimum number of length bytes required.
        /// <br>
        /// Note: It will be created with the number of length bytes required
        /// based on the length(in characters) of the<code>string</code> provided.
        /// The maximum string length allowed is <code>16777215</code> characters.
        /// </br>
        /// </summary>
        /// <param name="value">The value to be assigned to this SECSItem.</param>
        public ASCIISECSItem (string value) : base (SECSItemFormatCode.A, 1)
        {
            this.value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.ASCIISECSItem"/> class.
        /// This constructor creates a SECSItem that has a type of <code>A</code> with
        /// a specified number of length bytes.
        /// <p>
        /// This form of the constructor is not needed much nowadays.  In past
        /// there were situations where the equipment required that messages
        /// contained SECSItems that had a specified number of length bytes.
        /// This form of the constructor is here to handle these problem child cases.
        /// </p>
        /// <p>
        /// Note: It will be created with the number of length bytes set
        /// to the greater of the number of length bytes specified or
        /// the number required based on the length of the<code> text</code>
        /// parameter.
        /// </p>
        /// </summary>
        /// <param name="value">The value to be assigned to this SECSItem.</param>
        /// <param name="desiredNUmberOfLengthBytes">The number of length bytes to be used for this SECSItem.</param>
		public ASCIISECSItem(string value, int desiredNUmberOfLengthBytes) : base(SECSItemFormatCode.A, 1, desiredNUmberOfLengthBytes)
		{
			this.value = value;
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.ASCIISECSItem"/> class.
        /// This constructor is used to create this SECSItem from
        /// data in wire/transmission format.
        /// </summary>
        /// <param name="data">The buffer where the wire/transmission format data is contained.</param>
        /// <param name="itemOffset">The offset into the data parameter where the desired item starts.</param>
		public ASCIISECSItem(byte[] data, int itemOffset) : base(data, itemOffset)
		{
			int offset = 1 + inboundNumberOfLengthBytes + itemOffset;
			bytesConsumed = 1 + inboundNumberOfLengthBytes + lengthInBytes;

			byte[] temp = new byte[lengthInBytes];
			Array.Copy(data, offset, temp, 0, lengthInBytes);
			value = System.Text.Encoding.ASCII.GetString(temp);
		}

        /// <summary>
        /// Returns the value of this <code>ASCIISECSItem</code>.
        /// </summary>
        /// <returns>Returns the value of this <code>ASCIISECSItem</code>.</returns>
        public string GetValue()
		{
			return value;
		}

        /// <summary>
        /// Creates and returns a <code>byte[]</code> that represents this SECSItem in &quot;wire/transmission format&quot;.
        /// </summary>
        /// <returns>The raw SECSI tem.</returns>
		public override byte[] ToRawSECSItem()
		{
			byte[] output = new byte[outputHeaderLength()+value.Length];
			int offset = populateHeaderData(output, value.Length);

			byte[] temp = Encoding.ASCII.GetBytes(value);

			Array.Copy(temp, 0, output, offset, temp.Length);

			return output;
		}

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.ASCIISECSItem"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.ASCIISECSItem"/>.</returns>
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
        /// <param name="anObject">The <see cref="object"/> to compare with the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.ASCIISECSItem"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
        /// <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.ASCIISECSItem"/>; otherwise, <c>false</c>.</returns>
        public override bool Equals(object anObject)
		{
            if (this == anObject)
				return true;
            if (anObject == null)
				return false;
            if (GetType() != anObject.GetType())
				return false;
            ASCIISECSItem other = (ASCIISECSItem)anObject;
			if (value != other.value)
				return false;
			return true;
		}
	}
}
