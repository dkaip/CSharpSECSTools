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

#nullable enable

namespace com.CIMthetics.CSharpSECSTools.SECSItems
{
    /// <summary>
    /// This class represents a SECS item with a data type of <c>U1</c>
    /// in an array form. In this case it is an array of zero or more 8-bit unsigned integers.
    /// The C# data type is <c>byte[]</c>.
    /// </summary>
	public class U1ArraySECSItem : SECSItem
	{
		private byte[] _value;

		/// <summary>
		/// The value of this <c>U1ArraySECSItem</c>.
		/// </summary>
		public byte[] Value { get { return _value; } }

        /// <summary>
        /// This constructor creates a <c>U1ArraySECSItem</c> with no elements.
        /// </summary>
		public U1ArraySECSItem() : base(SECSItemFormatCode.U1, 0)
		{
            this._value = new byte[0];
		}

        /// <summary>
        /// This constructor creates a U1ArraySECSItem that will have the value of
        /// the supplied <c>byte[]</c>.
        /// </summary>
        /// <param name="value">An array of <c>byte</c> values to be assigned to this <c>SECSItem</c>.</param>
        /// <remarks>
        /// The array's length should not exceed <c>16777215</c> elements.
        /// </remarks>
		public U1ArraySECSItem(byte[]? value) : base(SECSItemFormatCode.U1, value == null ? 0 : value.Length)
		{
            if (value == null)
                this._value = new byte[0];
            else
                this._value = value;
		}

        /// <summary>
        /// This constructor creates a U1ArraySECSItem that will have the value of
        /// the supplied <c>byte[]</c>.  In addition when converted to 
        /// &quot;transmission&quot; form it will use the number of length bytes
        /// specified <b>OR</b> the minimum number necessary to actually contain 
        /// the length of the content of the <c>byte[]</c>.
        /// </summary>
        /// <param name="value">The value to be assigned to this <c>SECSItem</c>.</param>
        /// <param name="desiredNumberOfLengthBytes">The number of length bytes to be used for this <code>SECSItem</code>.
        /// The value for the number of length bytes must be <c>ONE</c>, <c>TWO</c>, or <c>THREE</c>.</param>
        /// <remarks>
        /// The array's length should not exceed <c>16777215</c> elements.
        /// </remarks>
		public U1ArraySECSItem(byte[]? value, SECSItemNumLengthBytes desiredNumberOfLengthBytes) : base(SECSItemFormatCode.U1, value == null ? 0 : value.Length, desiredNumberOfLengthBytes)
		{
            if (value == null)
                this._value = new byte[0];
            else
                this._value = value;
		}

        /// <summary>
        /// This constructor is only used by the <c>SECSItemFactory</c>.  Its
        /// purpose is to load the <c>value</c> field with the data provided.
        /// </summary>
        /// <param name="data">The buffer where the &quot;wire/transmission&quot; format data is contained.</param>
        /// <param name="itemOffset">The offset into the data where the desired item starts.</param>
		internal U1ArraySECSItem(byte[] data, int itemOffset) : base(data, itemOffset)
		{
            int offsetToValue = 1 + NumberOfLengthBytes.ValueOf() + itemOffset;

			_value = new byte[LengthInBytes];
			for(int i = 0, j = offsetToValue; i < _value.Length; i++, j++)
			{
				_value[i] = data[j];
			}
		}

        /// <summary>
        /// Gets the value of this <c>SECSItem</c>.
        /// </summary>
        /// <returns>the value of the <c>SECSItem</c>.</returns>
		[ObsoleteAttribute("This method has been deprecated, please use property Value instead.")]
		public byte[] GetValue()
		{
			return _value;
		}

        /// <summary>
        /// Creates and returns a <c>byte[]</c> that represents this <c>SECSItem</c> in &quot;wire/transmission format&quot;.
        /// </summary>
        /// <returns>A <c>byte[]</c> representation of this <c>SECSItem</c>'s content that is suitable for transmission.</returns>
		public override byte[] EncodeForTransport()
		{
			byte[] output = new byte[OutputHeaderLength()+_value.Length];
			int offset = PopulateSECSItemHeaderData(output, _value.Length);

			for( int i = offset, j = 0; j < _value.Length; i++, j++)
			{
				output[i] = _value[j];
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
			return "Format:" + ItemFormatCode.ToString() + " Value: Array";
		}

        /// <summary>
        /// Serves as a hash function for a <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.U1ArraySECSItem"/> object.
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
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.U1ArraySECSItem"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.U1ArraySECSItem"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
        /// <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.U1ArraySECSItem"/>, <c>false</c> otherwise.</returns>
		public override bool Equals(Object? obj)
		{
            if (base.Equals(obj) == false)
                return false;

            // If we are here obj is not null
			if (GetType() != obj.GetType())
				return false;

			U1ArraySECSItem other = (U1ArraySECSItem)obj;
			if (_value == null && other._value == null)
				return true;

			if ((_value != null && other._value != null) == false)
				return false;

            // If we are here both _value fields are not null
			if (_value.Length != other._value.Length)
				return false;

			return _value.SequenceEqual(other._value);
		}
	}
}

