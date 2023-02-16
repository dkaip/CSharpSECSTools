/*
 * Copyright 2019-2023 Douglas Kaip
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

namespace com.CIMthetics.CSharpSECSTools.SECSItems
{
    /// <summary>
    /// This class represents a <c>SECSItem</c> with the SECS data type of <c>L</c>,
    ///  which is a list of <c>SECSItem</c>s. From the C# side this data
    /// type is handled as a C# <c>LinkedList&lt;SECSItem&gt;</c>.
    /// </summary>
	public class ListSECSItem : SECSItem
	{
		private LinkedList<SECSItem> _value = new LinkedList<SECSItem>();

        private String[] delimiterArray = new string[1] { "." };

		/// <summary>
		/// This property, as per the specification, returns the number of 
		/// elements this <c>ListSECEItem</c> contains, NOT, the number of
		/// bytes it take up.
		/// </summary>
        public override int LengthInBytes { get { return _value.Count; } }

		/// <summary>
		/// This is the offset to the next byte needing processing when constructing
		/// this <c>ListSECSItem</c> from a <c>byte[]</c>.  When processing
		/// <c>SECSItem</c> in <c>byte[]</c> form you cannot know the length in
		/// bytes that a list consumes from the <c>byte[]</c> until after 
		/// you have &quot;filled it out&quot;.  List
		/// generation is done recursively.  Once a sub list is generated the
		/// offset to the next byte to process needs to calculated.  This
		/// attribute contains the offset to the next byte that needs to be
		/// processed after a list is generated.
		/// </summary>
		private UInt32 _offsetToNextItemToProcess = 0;

		/// <summary>
		/// The value of this <c>ListSECSItem</c>.
		/// </summary>
		public LinkedList<SECSItem> Value { get { return _value; } }

        /// <summary>
        /// This constructor creates a <c>ListSECSItem</c> with no elements.
        /// </summary>
		public ListSECSItem() : base(SECSItemFormatCode.L, 0) { }

        /// <summary>
        /// This constructor creates a ListSECSItem that will have the value of
        /// the supplied <c>LinkedList&lt;SECSItem&gt;</c>.
        /// </summary>
        /// <param name="value">A list of <c>LinkedList&lt;SECSItem&gt;</c> values to be assigned to this <c>SECSItem</c>.</param>
        /// <remarks>
        /// The array's length should not exceed <c>16777215</c> elements.
        /// </remarks>
        public ListSECSItem(LinkedList<SECSItem> value) : base(SECSItemFormatCode.L, value.Count)
		{
			this._value = value;
		}

        /// <summary>
        /// This constructor creates an ListSECSItem that will have the value of
        /// the supplied <c>LinkedList&lt;SECSItem&gt;</c>.  In addition when converted to 
        /// &quot;transmission&quot; form it will use the number of length bytes
        /// specified <b>OR</b> the minimum number necessary to actually contain 
        /// the length of the content of the <c>LinkedList&lt;SECSItem&gt;</c>.
        /// </summary>
        /// <param name="value">The value to be assigned to this <c>SECSItem</c>.</param>
        /// <param name="desiredNumberOfLengthBytes">The number of length bytes to be used for this <code>SECSItem</code>.
        /// The value for the number of length bytes must be <c>ONE</c>, <c>TWO</c>, or <c>THREE</c>.</param>
        /// <remarks>
        /// The array's length should not exceed <c>16777215</c> elements.
        /// </remarks>
        public ListSECSItem(LinkedList<SECSItem>? value, SECSItemNumLengthBytes desiredNumberOfLengthBytes) : base(SECSItemFormatCode.L, value == null ? 0 : value.Count, desiredNumberOfLengthBytes)
		{
            if (value != null)
				this._value = value;
		}

        /// <summary>
        /// This constructor is used to create a  <c>ListSECSItem</c> from
        /// data in &quot;wire/transmission&quot; format.
        /// </summary>
        /// <param name="data">The buffer where the &quot;wire/transmission&quot; format data is contained.</param>
        /// <param name="itemOffset">The offset into the data where the desired item starts.</param>
        /// <param name="numberOfElements">The number of elements that the list contains.</param>
		/// <remarks>
		/// This constructor is different from the other constructors of this type in that
		/// the number of elements in the list to be created is passed in as an argument.
		/// Since the <c>SECSItemFactory</c> that calls this form of constructor has already
		/// calculated the number of elements I thought it was much cleaner to just pass
		/// in the number that was already calculated as opposed to adding the code here
		/// to accomplish the same thing.
		/// </remarks>
		internal ListSECSItem(byte[] data, int itemOffset, int numberOfElements) : base(data, itemOffset)
		{

            _offsetToNextItemToProcess = (UInt32)(1 + NumberOfLengthBytes.ValueOf() + itemOffset);

            for( int i = 0; i < numberOfElements; i++)
			{
				SECSItem? temp = SECSItemFactory.GenerateSECSItem(data, (int)_offsetToNextItemToProcess);
				if (temp != null)
				{
					if (temp.ItemFormatCode == SECSItemFormatCode.L)
					{
						// temp is a list
						_offsetToNextItemToProcess = ((ListSECSItem)temp)._offsetToNextItemToProcess;
					}
					else
					{
						// temp is not a list
						_offsetToNextItemToProcess += (UInt32)(1 + temp.NumberOfLengthBytes.ValueOf() + temp.LengthInBytes);
					}

					_value.AddLast(temp);
				}
			}
		}

        /// <summary>
        /// Gets the value of this <c>SECSItem</c>.
        /// </summary>
        /// <returns>the value of the <c>SECSItem</c>.</returns>
		[ObsoleteAttribute("This method has been deprecated, please use property Value instead.")]
		public LinkedList<SECSItem> GetValue()
		{
			return _value;
		}

		/// <summary>
		///       This method returns a SECSItem contained in this list based on its 
		/// &quot;address&quot; or null if the item does not exist.  
		///
		///	In the example below, which represents the content of a list, a specified address of "3.2" would return the
		///		element with the value 'Answer'.
		///
		///		<ol>
		///		<li>A 'ABC' </li>
		///		<li>A 'DEF' </li>
		///		<li>L 4 </li>
		///		<ol>
		///		<li>I2 -32768</li>
		///		<li>A 'Answer'</li>
		///		<li>U1 255</li>
		///     <li>A 'Not The Answer'</li>
		///		</ol>
		///		<li>F4 3.141592</li>
		///		</ol>
		/// </summary>
		/// <returns>The <see cref="com.CIMthetics.CSharpSECSTools.SECSItems.SECSItem"/>.</returns>
		/// <param name="address">The SECSItem at the provided address or null if not found.</param>
		public SECSItem? GetElementAt(String address)
		{
			SECSItem result;
            String[] addressArray = address.Split(delimiterArray, 2, StringSplitOptions.None);
			int elementNumber = int.Parse(addressArray[0]);

			if (elementNumber < 1 || elementNumber > _value.Count)
				return null;

			result = _value.ElementAt(elementNumber-1);
			if (addressArray.Length > 1)
			{
				if (result.ItemFormatCode != SECSItemFormatCode.L)
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
			foreach(SECSItem item in _value)
			{
				byte[] itemBytes = item.EncodeForTransport();
				outputBufferSize += itemBytes.Length;
				outputStorage.AddLast(itemBytes);
			}


			byte[] output = new byte[OutputHeaderLength()+outputBufferSize];
			int offset = PopulateSECSItemHeaderData(output, _value.Count);

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

			result.Append("Format:" + ItemFormatCode.ToString() + " Value: " + _value.Count);
			foreach(SECSItem item in _value)
			{
				if (item.ToString() != null)
					result.Append("\n" + item.ToString() );
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
            unchecked // Overflow is fine, just wrap
            {
                int hash = (int) 2166136261;
                // Suitable nullity checks etc, of course :)
                hash = (hash * 16777619) ^ base.GetHashCode();
                hash = (hash * 16777619) ^ _value.GetHashCode();
                return hash;
            }
		}

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.ListSECSItem"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.ListSECSItem"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
        /// <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.ListSECSItem"/>; otherwise, <c>false</c>.</returns>
		public override bool Equals(Object? obj)
		{
            if (base.Equals(obj) == false)
                return false;

            // If we are here obj is not null
			if (GetType() != obj.GetType())
				return false;

			ListSECSItem other = (ListSECSItem)obj;
			if (_value == null && other._value == null)
				return true;

			if ((_value != null && other._value != null) == false)
				return false;

            // If we are here both _value fields are not null
			if (_value.Count != other._value.Count)
				return false;

			LinkedList<SECSItem>.Enumerator temp1 = _value.GetEnumerator();
			LinkedList<SECSItem>.Enumerator temp2 = other._value.GetEnumerator();
			for(int i = 0; i < _value.Count; i++)
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

