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
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace com.CIMthetics.CSharpSECSTools.SECSItems
{
	public class ListSECSItem : SECSItem
	{
		private LinkedList<SECSItem> value = null;

		public ListSECSItem(LinkedList<SECSItem> value) : base(SECSItemFormatCode.L, value.Count)
		{
			this.value = value;
		}

		public ListSECSItem(LinkedList<SECSItem> value, int desiredNUmberOfLengthBytes) : base(SECSItemFormatCode.A, value.Count, desiredNUmberOfLengthBytes)
		{
			this.value = value;
		}

		public ListSECSItem(byte[] data, int itemOffset) : base(data, itemOffset)
		{
			int offset = 1 + inboundNumberOfLengthBytes + itemOffset;
			bytesConsumed = offset + lengthInBytes;
			value = new LinkedList<SECSItem>();

			for( int i = 0; i < numberOfElements; i++)
			{
				SECSItem temp = RawSECSData.generateSECSItem(data, offset);
				offset += temp.getBytesConumed();
				this.bytesConsumed += temp.getBytesConumed();
				value.AddLast(temp);
			}
		}

		public LinkedList<SECSItem> getValue()
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
		public SECSItem getElementAt(String address)
		{
			SECSItem result = null;

			String[] addressArray = address.Split('.');
			int elementNumber = int.Parse(addressArray[0]);

			if (elementNumber < 1 || elementNumber > value.Count)
				return null;

			result = value.ElementAt(elementNumber-1);
			if (addressArray.Length > 1)
			{
				if (result.getSECSItemFormatCode() != SECSItemFormatCode.L)
				{
					/*
               		 * If we are here the address has more levels, but, this 
                 	 * element is not a list so we cannot proceed.
                 	*/
					return null;
				}
				else
				{
					return getElementAt(addressArray[1]);
				}
			}

			return result;
		}

		public override byte[] ToRawSECSItem()
		{

			LinkedList<byte[]> outputStorage = new LinkedList<byte[]>();

			int outputBufferSize = 0;
			foreach(SECSItem item in value)
			{
				byte[] itemBytes = item.ToRawSECSItem();
				outputBufferSize += itemBytes.Length;
				outputStorage.AddLast(itemBytes);
			}


			byte[] output = new byte[outputHeaderLength()+outputBufferSize];
			int offset = populateHeaderData(output, value.Count);

			foreach(byte[] rawData in outputStorage)
			{
				Array.Copy(rawData, 0, output, offset, rawData.Length);
				offset += rawData.Length;
			}

			return output;
		}

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

		public override int GetHashCode()
		{
			return value.GetHashCode();
		}

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

				if (!temp1.Current.Equals(temp2.Current))
				{
					return false;
				}
			}
			return true;
		}

	}
}

