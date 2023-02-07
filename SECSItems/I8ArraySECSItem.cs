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
    /// This class represents a SECS item with a data type of <c>I8</c>
    /// in an array form. In this case it is an array of zero or more 64-bit signed integers.
    /// The C# data type is <c>Int64[]</c>.
    /// </summary>
	public class I8ArraySECSItem : SECSItem
	{
		private Int64[] _value;

		/// <summary>
		/// The value of this <c>I8ArraySECSItem</c>.
		/// </summary>
		public Int64[] Value { get { return _value; } }

        /// <summary>
        /// This constructor creates a <c>I8ArraySECSItem</c> with no elements.
        /// </summary>
		public I8ArraySECSItem() : base(SECSItemFormatCode.I8, 0)
		{
            this._value = new Int64[0];
		}

        /// <summary>
        /// This constructor creates a I8ArraySECSItem that will have the value of
        /// the supplied <c>Int64[]</c>.
        /// </summary>
        /// <param name="value">An array of <c>Int64</c> values to be assigned to this <c>SECSItem</c>.</param>
        /// <remarks>
        /// The array's length should not exceed <c>2097151</c> elements.
        /// </remarks>
		public I8ArraySECSItem(Int64[]? value) : base(SECSItemFormatCode.I8, value == null ? 0 : value.Length * 8)
		{
			if (value == null)
				this._value = new Int64[0];
			else
				this._value = value;
		}

        /// <summary>
        /// This constructor creates an I8ArraySECSItem that will have the value of
        /// the supplied <c>Int64[]</c>.  In addition when converted to 
        /// &quot;transmission&quot; form it will use the number of length bytes
        /// specified <b>OR</b> the minimum number necessary to actually contain 
        /// the length of the content of the <c>Int64[]</c>.
        /// </summary>
        /// <param name="value">The value to be assigned to this <c>SECSItem</c>.</param>
        /// <param name="desiredNumberOfLengthBytes">The number of length bytes to be used for this <code>SECSItem</code>.
        /// The value for the number of length bytes must be <c>ONE</c>, <c>TWO</c>, or <c>THREE</c>.</param>
        /// <remarks>
        /// The array's length should not exceed <c>2097151</c> elements.
        /// </remarks>
		public I8ArraySECSItem(Int64[]? value, SECSItemNumLengthBytes desiredNumberOfLengthBytes) : base(SECSItemFormatCode.I8, value == null ? 0 : value.Length * 8, desiredNumberOfLengthBytes)
		{
			if (value == null)
				this._value = new Int64[0];
			else
				this._value = value;
		}

        /// <summary>
        /// This constructor is used to create this SECSItem from
        /// data in &quot;wire/transmission&quot; format.
        /// </summary>
        /// <param name="data">The buffer where the &quot;wire/transmission&quot; format data is contained.</param>
        /// <param name="itemOffset">The offset into the data where the desired item starts.</param>
		internal I8ArraySECSItem(byte[] data, int itemOffset) : base(data, itemOffset)
		{
            int offset = 1 + NumberOfLengthBytes.ValueOf() + itemOffset;
			if ((LengthInBytes % 8) != 0)
                throw new ArgumentOutOfRangeException("Illegal data length of: " + LengthInBytes + " payload length must be a multiple of 8.");

			_value = new Int64[LengthInBytes / 8];
			byte[] temp = new byte[8];
			for (int i = offset, j = 0; j < _value.Length; i += 8, j++)
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

				_value[j] = BitConverter.ToInt64(temp, 0);
			}
		}

        /// <summary>
        /// Gets the value of this <c>SECSItem</c>.
        /// </summary>
        /// <returns>the value of the <c>SECSItem</c>.</returns>
		[ObsoleteAttribute("This method has been deprecated, please use property Value instead.")]
		public Int64[] GetValue()
		{
			return _value;
		}


        /// <summary>
        /// Creates and returns a <c>byte[]</c> that represents this <c>SECSItem</c> in &quot;wire/transmission format&quot;.
        /// </summary>
        /// <returns>A <c>byte[]</c> representation of this <c>SECSItem</c>'s content that is suitable for transmission.</returns>
		public override byte[] EncodeForTransport()
		{
			byte[] output = new byte[OutputHeaderLength()+(_value.Length * 8)];
			int offset = PopulateSECSItemHeaderData(output, (_value.Length * 8));

			for( int i = offset, j = 0; j < _value.Length; i+=8, j++ )
			{
				byte[] temp = BitConverter.GetBytes(_value[j]);

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
			return "Format:" + ItemFormatCode.ToString() + " Value: Array";
		}

        /// <summary>
        /// Serves as a hash function for a <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.I8ArraySECSItem"/> object.
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
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.I8ArraySECSItem"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.I8ArraySECSItem"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
        /// <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.I8ArraySECSItem"/>; otherwise, <c>false</c>.</returns>
		public override bool Equals(Object? obj)
		{
            if (base.Equals(obj) == false)
                return false;

            // If we are here obj is not null
			if (GetType() != obj.GetType())
				return false;

			I8ArraySECSItem other = (I8ArraySECSItem)obj;
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

