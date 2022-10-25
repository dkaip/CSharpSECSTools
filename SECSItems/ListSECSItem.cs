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
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace com.CIMthetics.CSharpSECSTools.SECSItems
{
    /// <summary>
    /// This class represents/implements a <c>SECSItem</c> with the SECS data type of <c>L</c>,
    ///  which is a list of <c>SECSItem</c>s. From the C# side this data
    /// type is handled as a C# <c>LinkedList&lt;SECSItem&gt;</c>.
    /// </summary>
	public class ListSECSItem : SECSItem
	{
		private LinkedList<SECSItem> value = null;

        private String[] delimiterArray = new string[1] { "." };

        /// <summary>
        /// This constructor creates a <c>SECSItem</c> that has a type of <c>L</c>
        /// with the specified value.
		/// <para/>
        /// Note: It will be created with the minimum number of length bytes required to
        /// accommodate the size of the provided list of <c>SECSItem</c>s.
        /// </summary>
        /// <param name="value">The value to be assigned to this <code>SECSItem</code>.</param>
        public ListSECSItem(LinkedList<SECSItem> value) : base(SECSItemFormatCode.L, value == null ? 0 : value.Count)
		{
            if (value == null)
                this.value = new LinkedList<SECSItem>();
            else
			this.value = value;
		}

        /// <summary>
        /// This constructor creates a <c>SECSItem</c> that has a type of <c>L</c>
        /// with the specified value and specified number of length bytes.
        /// <para/>
        /// This form of the constructor is not needed much nowadays.  In the past
        /// there were situations where the equipment required that messages
        /// contained SECSItems that had a specified number of length bytes.
        /// This form of the constructor is here to handle these problem child cases.
		/// <para/>
        /// Note: It will be created with the greater of the specified number of length bytes 
        /// or the number of length bytes required to
        /// accommodate the size of the provided list of <c>SECSItem</c>s.
        /// </summary>
        /// <param name="value">The value to be assigned to this <code>SECSItem</code>.</param>
        /// <param name="desiredNumberOfLengthBytes">The number of length bytes to be used for this <c>SECSItem</c>.
        /// The value for the number of length bytes must be <c>ONE</c>, <c>TWO</c>, or <c>THREE</c>.</param>
        public ListSECSItem(LinkedList<SECSItem> value, SECSItemNumLengthBytes desiredNumberOfLengthBytes) : base(SECSItemFormatCode.L, value == null ? 0 : value.Count, desiredNumberOfLengthBytes)
		{
            if (value == null)
                this.value = new LinkedList<SECSItem>();
            else
			this.value = value;
		}

        /// <summary>
        /// This constructor is used to create this SECSItem from
        /// data in &quot;wire/transmission&quot; format.
        /// </summary>
        /// <param name="data">The buffer where the &quot;wire/transmission&quot; format data is contained.</param>
        /// <param name="itemOffset">The offset into the data where the desired item starts.</param>
		public ListSECSItem(byte[] data, int itemOffset) : base(data, itemOffset)
		{
            int offset = 1 + inboundNumberOfLengthBytes.ValueOf () + itemOffset;
			bytesConsumed = offset + lengthInBytes;
			value = new LinkedList<SECSItem>();

            for( int i = 0; i < lengthInBytes; i++)
			{
				SECSItem temp = SECSItemFactory.GenerateSECSItem(data, offset);
				offset += temp.GetBytesConsumed();
				this.bytesConsumed += temp.GetBytesConsumed();
				value.AddLast(temp);
			}
		}

        /// <summary>
        /// Gets the value of this <c>SECSItem</c>.
        /// </summary>
        /// <returns>the value of the <c>SECSItem</c>.</returns>
		public LinkedList<SECSItem> GetValue()
		{
			return value;
		}

		/// <summary>
		///       This method returns a SECSItem contained in this list based on its 
		/// "address" or null if the item does not exist.  
		///
		///	In the example below a specified address of "3.2" would return the
		///		element with the value 'Answer'.
		///
		///		<ol>
		///		<li>A 'ABC' </li>
		///		<li>A 'DEF' </li>
		///		<li>L 3 </li>
		///		<ol>
		///		<li>I2 -32768</li>
		///		<li>A 'Answer'</li>
		///		<li>U1 255</li>
		///		</ol>
		///		<li>F4 3.141592</li>
		///		</ol>
		/// </summary>
		/// <returns>The <see cref="com.CIMthetics.CSharpSECSTools.SECSItems.SECSItem"/>.</returns>
		/// <param name="address">The SECSItem at the provided address or null if not found.</param>
		public SECSItem GetElementAt(String address)
		{
			SECSItem result = null;
            String[] addressArray = address.Split(delimiterArray, 2, StringSplitOptions.None);
			int elementNumber = int.Parse(addressArray[0]);

			if (elementNumber < 1 || elementNumber > value.Count)
				return null;

			result = value.ElementAt(elementNumber-1);
			if (addressArray.Length > 1)
			{
				if (result.GetSECSItemFormatCode() != SECSItemFormatCode.L)
				{
					/*
               		 * If we are here the address has more levels, but, this 
                 	 * element is not a list so we cannot proceed.
                 	*/
					return null;
				}
				else
				{
					return GetElementAt(addressArray[1]);
				}
			}

			return result;
		}

        /// <summary>
        /// Creates and returns a <c>byte[]</c> that represents this <c>SECSItem</c> in &quot;wire/transmission format&quot;.
        /// </summary>
        /// <returns>A <c>byte[]</c> representation of this <c>SECSItem</c>'s content that is suitable for transmission.</returns>
		public override byte[] EncodeForTransport()
		{

			LinkedList<byte[]> outputStorage = new LinkedList<byte[]>();

			int outputBufferSize = 0;
			foreach(SECSItem item in value)
			{
				byte[] itemBytes = item.EncodeForTransport();
				outputBufferSize += itemBytes.Length;
				outputStorage.AddLast(itemBytes);
			}


			byte[] output = new byte[OutputHeaderLength()+outputBufferSize];
			int offset = PopulateSECSItemHeaderData(output, value.Count);

			foreach(byte[] rawData in outputStorage)
			{
				Array.Copy(rawData, 0, output, offset, rawData.Length);
				offset += rawData.Length;
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
			StringBuilder result = new StringBuilder();

			result.Append("Format:" + formatCode.ToString() + " Value: " + value.Count);
			foreach(SECSItem item in value)
			{
				String temp = item.ToString();
				result.Append("\n" + temp);
			}

			return result.ToString();
		}

        /// <summary>
        /// Serves as a hash function for a <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.ListSECSItem"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
        /// hash table.</returns>
		public override int GetHashCode()
		{
			return value.GetHashCode();
		}

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.ListSECSItem"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.ListSECSItem"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
        /// <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.ListSECSItem"/>; otherwise, <c>false</c>.</returns>
		public override bool Equals(Object obj)
		{
			if (this == obj)
				return true;
			if (obj == null)
				return false;
			if (GetType() != obj.GetType())
				return false;
			ListSECSItem other = (ListSECSItem) obj;
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
			if (value.Count != other.value.Count)
				return false;
			
			LinkedList<SECSItem>.Enumerator temp1 = value.GetEnumerator();
			LinkedList<SECSItem>.Enumerator temp2 = other.value.GetEnumerator();
			for(int i = 0; i < value.Count; i++)
			{
				temp1.MoveNext();
				temp2.MoveNext();

				if (temp1.Current.Equals(temp2.Current) == false)
				{
					return false;
				}
			}
			return true;
		}
	}
}

