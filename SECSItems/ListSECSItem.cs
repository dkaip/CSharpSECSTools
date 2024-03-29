﻿/*
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

using System.Collections;
using System.Text;

#nullable enable

namespace com.CIMthetics.CSharpSECSTools.SECSItems
{
    /// <summary>
    /// This class represents a <c>SECSItem</c> with the SECS data type of <c>L</c>,
    ///  which is a list of <c>SECSItem</c>s.
    /// </summary>
	public class ListSECSItem : SECSItem, IList<SECSItem>
	{
		private readonly IList<SECSItem> _value = new List<SECSItem>();

        private String[] delimiterArray = new string[1] { "." };

		/// <summary>
		/// This property, as per the specification, returns the number of 
		/// elements this <c>ListSECEItem</c> contains, NOT, the number of
		/// bytes it take up.
		/// </summary>
		/// <remarks>
		/// This property only counts the elements in this list. It does not
		/// account for any elements within any <c>ListSECSItem</c>s that may be contained in this list.
		/// For example, if this <c>ListSECSItem</c> has 5 elements that are themselves
		/// <c>ListSECSItem</c>s each with 5 elements. It will still only return
		/// a value of 5.
		/// </remarks>
        public override int LengthInBytes { get { return _value.Count; } }

		/// <summary>
		/// The number of <c>SECSItem</c> elements contained within this <c>ListSECSItem</c>
		/// </summary>
		/// <remarks>
		/// This property only counts the elements in this list. It does not
		/// account for any elements within any <c>ListSECSItem</c>s that may be contained in this list.
		/// For example, if this <c>ListSECSItem</c> has 5 elements that are themselves
		/// <c>ListSECSItem</c>s each with 5 elements. It will still only return
		/// a value of 5.
		/// <para>
		/// This returns the same value as the <c>LengthInBytes</c> property.
		/// </para>
		/// </remarks>
        public int Count => _value.Count;

		/// <summary>
		/// Indicates whether or not this Object is read only.
		/// </summary>
        public bool IsReadOnly => _value.IsReadOnly;

		/// <summary>
		/// Gets or sets the <c>SECSItem</c> element at the provided index(zero-based) in this list.
		/// <code>
		/// SECSItem item = myFullListSECSItem[4];
		/// 
		///     or
		/// 
		/// myFullListSECSItem[4] = theReplacementSECSItem;
		/// </code>
		/// </summary>
		/// <param name="index">The zero-based index of the <c>SECSItem</c> that will be retrieved or
		/// replaced in this list.</param>
		/// <value>
		/// A <c>SECSItem</c> at the specified position.
		/// </value>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown if <c>index</c> is <c>&lt; 0 </c> or greater than <c>Count</c>.
		/// </exception>
        public SECSItem this[int index] { get => _value[index]; set => _value[index] = value; }

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
        /// This constructor creates a <c>ListSECSItem</c> with no elements.
        /// </summary>
        public ListSECSItem() : base(SECSItemFormatCode.L, 0) { }

        /// <summary>
        /// This constructor creates a ListSECSItem that will have the value of
        /// the supplied <c>ListSECSItem</c>.
        /// </summary>
        /// <param name="value">A l<c>ListSECSItem</c>.</param>
        /// <remarks>
        /// The array's length should not exceed <c>16777215</c> elements.
		/// <para>
		/// This is a copy constructor.
		/// </para>
        /// </remarks>
        public ListSECSItem(ListSECSItem? value) : base(SECSItemFormatCode.L, value == null ? 0 : value.Count)
		{
			if (value != null)
			{
				/*
				    This is my quick hack for a copy constructor.  Given the
					expected usage patterns of this constructor this is probably
					not a horrible solution.
				*/
				byte[] temp = value.EncodeForTransport();

				SECSItem? newItem = SECSItemFactory.GenerateSECSItem(temp);

				if (newItem != null)
				{
					this._value = ((ListSECSItem)newItem)._value;
				}
			}
		}

        /// <summary>
        /// This constructor creates an ListSECSItem that will have the value of
        /// the supplied <c>ListSECSItem</c>.  In addition when converted to 
        /// &quot;transmission&quot; form it will use the number of length bytes
        /// specified <b>OR</b> the minimum number necessary to actually contain 
        /// the length of the content of this <c>ListSECSItem</c>.
        /// </summary>
        /// <param name="value">The value to be assigned to this <c>SECSItem</c>.</param>
        /// <param name="desiredNumberOfLengthBytes">The number of length bytes to be used for this <code>SECSItem</code>.
        /// The value for the number of length bytes must be <c>ONE</c>, <c>TWO</c>, or <c>THREE</c>.</param>
        /// <remarks>
        /// The array's length should not exceed <c>16777215</c> elements.
		/// <para>
		/// This is a copy constructor.
		/// </para>
        /// </remarks>
        public ListSECSItem(ListSECSItem? value, SECSItemNumLengthBytes desiredNumberOfLengthBytes) : base(SECSItemFormatCode.L, value == null ? 0 : value.Count, desiredNumberOfLengthBytes)
		{
			if (value != null)
			{
				/*
				    This is my quick hack for a copy constructor.  Given the
					expected usage patterns of this constructor this is probably
					not a horrible solution.
				*/
				byte[] temp = value.EncodeForTransport();

				SECSItem? newItem = SECSItemFactory.GenerateSECSItem(temp);

				if (newItem != null)
				{
					this._value = ((ListSECSItem)newItem)._value;
				}
			}
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

					_value.Add(temp);
				}
			}
		}

		/// <summary>
		/// This method returns a SECSItem contained in this list based on its 
		/// &quot;address&quot;.
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
		/// <param name="address">The &quot;address&quot; of the desired item in the format described above.</param>
		/// <returns>The <see cref="com.CIMthetics.CSharpSECSTools.SECSItems.SECSItem"/>
		/// The SECSItem at the provided address or <c>null</c> if not found.
		/// </returns>
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
		/// This method returns the elements contained within this <c>ListSECSItem</c>
		/// as a <c>Dictionary</c>.
		/// </summary>
		/// <returns>
		/// A <c>Dictionary&lt;string, SECSItem&gt;</c> where the individual elements 
		/// contained within are accessible via their &quot;address&quot;.
		/// </returns>
		/// <remarks>
		/// The keys of the dictionary are the &quot;addresses&quot;
		/// of the individual elements.  The format of the &quot;address&quot; keys is the
		/// same as that used by the <see cref="GetElementAt(string)"/> method.  Refer to
		/// it for more information.
		/// <para>
		/// It you have a large message that you need to grab a bunch elements from this
		/// might be more efficient than calling <see cref="GetElementAt(string)"/> a
		/// bunch of times.
		/// </para>
		/// </remarks>
		public Dictionary<string, SECSItem>AsDictionary()
		{
			Dictionary<string, SECSItem> result = new Dictionary<string, SECSItem>();

			AddItemsToDictionary(result, "", this);

			return result;
		}

		private void AddItemsToDictionary(Dictionary<string, SECSItem> dictionary, string prefixString, ListSECSItem secsItem)
		{
			string address = "";

			for(int i = 0; i < secsItem.Count; i++)
			{
				if (string.IsNullOrEmpty(prefixString) == true)
					address = (i + 1).ToString();
				else
					address = prefixString + "." + (i + 1).ToString();

				dictionary.Add(address, secsItem[i]);
				if (secsItem[i].GetType() == typeof(ListSECSItem))
				{
					AddItemsToDictionary(dictionary, address, (ListSECSItem)secsItem[i]);
				}
			}
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

			IEnumerator<SECSItem> temp1 = _value.GetEnumerator();
			IEnumerator<SECSItem>temp2 = other.GetEnumerator();
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

		/// <summary>
		/// Return the zero-based index (position) of first occurrence of the specified <c>SECSItem</c> in the list.
		/// </summary>
		/// <param name="item">The <c>SECSItem</c> to search for within this <c>ListSECSItem</c>.</param>
		/// <returns>
		/// The zero-based index (position) of first occurrence of the specified <c>SECSItem</c> in the list or
		/// -1 if the item was not found in this list.
		/// </returns>
        public int IndexOf(SECSItem item)
        {
            return _value.IndexOf(item);
        }

		/// <summary>
		/// Insert a <c>SECSItem</c> at the specified position in the list.
		/// </summary>
		/// <param name="index">The position where to add <c>item</c> to this list.</param>
		/// <param name="item">The <c>SECSItem</c> to add to insert into this list.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown if <c>index</c> is <c>&lt; 0 </c> or greater than <c>Count</c>.
		/// </exception>
        public void Insert(int index, SECSItem item)
        {
            _value.Insert(index, item);
        }

		/// <summary>
		/// Remove the <c>SECSItem</c> located at the specified position in the list.
		/// </summary>
		/// <param name="index">The zero-based index (position) of the <c>SECSItem</c> to removed.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown if <c>index</c> is <c>&lt; 0 </c> or greater than <c>Count</c>.
		/// </exception>
        public void RemoveAt(int index)
        {
            _value.RemoveAt(index);
        }

		/// <summary>
		/// Add a <c>SECSItem</c> to the end of the list.
		/// </summary>
		/// <param name="item">The <c>SECSItem</c> to add to this list.</param>
        public void Add(SECSItem item)
        {
            _value.Add(item);
        }

		/// <summary>
		/// Remove all of the <c>SECSItems</c> within this list.
		/// </summary>
        public void Clear()
        {
            _value.Clear();
        }

		/// <summary>
		/// Determine whether the specified <c>SECSItem</c> is contained within this list.
		/// </summary>
		/// <param name="item">The <c>SECSItem</c> to search for.</param>
		/// <returns>
		/// <c>true</c> if this list contains the specified <c>SECSItem</c>,
		/// <c>false</c> if otherwise.
		/// </returns> 
        public bool Contains(SECSItem item)
        {
            return _value.Contains(item);
        }

		/// <summary>
		/// Determine whether the specified <c>SECSItem</c> is contained within this list.
		/// </summary>
		/// <param name="array">A one dimensional array of <c>SECSItem</c>s that will be
		/// the destination for the copy.</param>
		/// <param name="arrayIndex">The zero-based index where the copying begins.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown if <c>arrayIndex</c> is <c>&lt; 0 </c>.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// Thrown if <c>array</c> is <c>null</c>.
		/// </exception>
        public void CopyTo(SECSItem[] array, int arrayIndex)
        {
            _value.CopyTo(array, arrayIndex);
        }

		/// <summary>
		/// Remove the first occurrence of the specified <c>SECSItem</c> from the list.
		/// </summary>
		/// <param name="item">The <c>SECSItem</c> to be removed from this list.</param>
		/// <returns>
		/// <c>true</c> if the item was successfully removed,
		/// <c>false</c> if otherwise.
		/// </returns> 
        public bool Remove(SECSItem item)
        {
            return _value.Remove(item);
        }

		/// <summary>
		/// Returns an enumerator that iterates through the items contained in this list.
		/// </summary>
		/// <returns>
		/// An enumerator that may be used to iterate through the items contained in this list.
		/// </returns> 
        public IEnumerator<SECSItem> GetEnumerator()
        {
            return _value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_value).GetEnumerator();
        }
    }
}

