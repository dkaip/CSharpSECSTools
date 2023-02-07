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
    /// This class representsa <c>SECSItem</c> with the SECS data type of <c>A</c> 
    /// (<c>ASCII</c>),
    /// which is a &quot;string&quot; of<c>ASCII</c> characters.From the C# side this data
    /// type is handled as a C# <c>string</c>. Be careful to only use appropriate character sets
    /// (<c>System.Text.Encoding.ASCII</c>) or undesirable behavior may be manifested.
    /// </summary>
	public class ASCIISECSItem : SECSItem
	{
		private string _value;

		/// <summary>
		/// The value of this <c>ASCIISECSItem</c>.
		/// </summary>
		public string Value { get { return _value; } }

        /// <summary>
        /// This constructor creates a <c>ASCIISECSItem</c> with no elements.
        /// </summary>
		public ASCIISECSItem() : base(SECSItemFormatCode.A, 0)
		{
            this._value = "";
		}

        /// <summary>
        /// This constructor creates an ASCIISECSItem that will have the value of
        /// the supplied <c>string</c>.
        /// </summary>
        /// <param name="value">The <c>string</c> value to be assigned to this SECSItem.</param>
        /// <remarks>
        /// The string's length should not exceed <c>16777215</c> <c>ASCII</c> characters.
        /// </remarks>
        public ASCIISECSItem(string? value) : base(SECSItemFormatCode.A, value == null ? 0 : value.Length)
		{
            if (value == null)
                this._value = "";
            else
    			this._value = value;
		}

        /// <summary>
        /// This constructor creates an ASCIISECSItem that will have the value of
        /// the supplied <c>string</c>.  In addition when converted to 
        /// &quot;transmission&quot; form it will use the number of length bytes
        /// specified <b>OR</b> the minimum number necessary to actually contain 
        /// the length of the content of the <c>sbyte[]</c>.
        /// </summary>
        /// <param name="value">The value to be assigned to this <c>SECSItem</c>.</param>
        /// <param name="desiredNumberOfLengthBytes">The number of length bytes to be used for this <code>SECSItem</code>.
        /// The value for the number of length bytes must be <c>ONE</c>, <c>TWO</c>, or <c>THREE</c>.</param>
        /// <remarks>
        /// The string's length should not exceed <c>16777215</c> <c>ASCII</c> characters.
        /// </remarks>
        public ASCIISECSItem(string? value, SECSItemNumLengthBytes desiredNumberOfLengthBytes) : base(SECSItemFormatCode.A, value == null ? 0 : value.Length, desiredNumberOfLengthBytes)
		{
            if (value == null)
                this._value = "";
            else
    			this._value = value;
		}

        /// <summary>
        /// This constructor is used to create this SECSItem from
        /// data in &quot;wire/transmission&quot; format.
        /// </summary>
        /// <param name="data">The buffer where the &quot;wire/transmission&quot; format data is contained.</param>
        /// <param name="itemOffset">The offset into the data where the desired item starts.</param>
		internal ASCIISECSItem(byte[] data, int itemOffset) : base(data, itemOffset)
		{
            int offset = 1 + NumberOfLengthBytes.ValueOf() + itemOffset;

			byte[] temp = new byte[LengthInBytes];
			Array.Copy(data, offset, temp, 0, LengthInBytes);
			_value = System.Text.Encoding.ASCII.GetString(temp);
		}

        /// <summary>
        /// Gets the value of this <c>SECSItem</c>.
        /// </summary>
        /// <returns>the value of the <c>SECSItem</c>.</returns>
		[ObsoleteAttribute("This method has been deprecated, please use property Value instead.")]
		public string GetValue()
		{
			return _value;
		}

        /// <summary>
        /// Creates and returns a <c>byte[]</c> that represents this <c>SECSItem</c> in &quot;wire/transmission&quot; format.
        /// </summary>
        /// <returns>A <c>byte[]</c> representation of this <c>SECSItem</c>'s content that is suitable for transmission.</returns>
		public override byte[] EncodeForTransport()
		{
			byte[] output = new byte[OutputHeaderLength()+_value.Length];
            int offset = PopulateSECSItemHeaderData(output, _value.Length);

			byte[] temp = Encoding.ASCII.GetBytes(_value);

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
			return "Format:" + ItemFormatCode.ToString() + " Value: " + _value;
		}

        /// <summary>
        /// Serves as a hash function for a <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.ASCIISECSItem"/> object.
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
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.ASCIISECSItem"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.ASCIISECSItem"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
        /// <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.ASCIISECSItem"/>, <c>false</c> otherwise.</returns>
		public override bool Equals(Object? obj)
		{
            if (base.Equals(obj) == false)
                return false;

            // If we are here obj is not null
			if (GetType() != obj.GetType())
				return false;

			ASCIISECSItem other = (ASCIISECSItem)obj;
			if (_value == null && other._value == null)
				return true;

			if ((_value != null && other._value != null) == false)
				return false;

            // If we are here both _value fields are not null
			if (_value.Length != other._value.Length)
				return false;

			return string.Equals(_value, other._value);
		}
	}
}
