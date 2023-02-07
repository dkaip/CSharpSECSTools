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

#nullable enable

namespace com.CIMthetics.CSharpSECSTools.SECSItems 
{
    /// <summary>
    /// This class represents a SECS item with a data type of <c>I2</c>
    /// in an array form. In this case it is an array of zero or more 16-bit signed integers.
    /// The C# data type is <c>Int16[]</c>.
    /// </summary>
	public class I2ArraySECSItem : SECSItem
	{
		private Int16[] _value;

		/// <summary>
		/// The value of this <c>I2ArraySECSItem</c>.
		/// </summary>
		public Int16[] Value { get { return _value; } }

        /// <summary>
        /// This constructor creates a <c>I2ArraySECSItem</c> with no elements.
        /// </summary>
		public I2ArraySECSItem() : base(SECSItemFormatCode.I2, 0)
		{
            this._value = new Int16[0];
		}

        /// <summary>
        /// This constructor creates an I2ArraySECSItem that will have the value of
        /// the supplied <c>Int16[]</c>.
        /// </summary>
        /// <param name="value">An array of <c>Int16</c> values to be assigned to this <c>SECSItem</c>.</param>
        /// <remarks>
        /// The array's length should not exceed <c>8388607</c> elements.
        /// </remarks>
		public I2ArraySECSItem(Int16[]? value) : base(SECSItemFormatCode.I2, value == null ? 0 : value.Length * 2)
		{
			if (value == null)
				this._value = new Int16[0];
			else
				this._value = value;
		}

        /// <summary>
        /// This constructor creates a I2ArraySECSItem that will have the value of
        /// the supplied <c>Int16[]</c>.  In addition when converted to 
        /// &quot;transmission&quot; form it will use the number of length bytes
        /// specified <b>OR</b> the minimum number necessary to actually contain 
        /// the length of the content of the <c>Int16[]</c>.
        /// </summary>
        /// <param name="value">The value to be assigned to this <c>SECSItem</c>.</param>
        /// <param name="desiredNumberOfLengthBytes">The number of length bytes to be used for this <code>SECSItem</code>.
        /// The value for the number of length bytes must be <c>ONE</c>, <c>TWO</c>, or <c>THREE</c>.</param>
        /// <remarks>
        /// The array's length should not exceed <c>8388607</c> elements.
        /// </remarks>
		public I2ArraySECSItem(Int16[]? value, SECSItemNumLengthBytes desiredNumberOfLengthBytes) : base(SECSItemFormatCode.I2, value == null ? 0 : value.Length * 2, desiredNumberOfLengthBytes)
		{
			if (value == null)
				this._value = new Int16[0];
			else
				this._value = value;
		}

        /// <summary>
        /// This constructor is used to create this SECSItem from
        /// data in &quot;wire/transmission&quot; format.
        /// </summary>
        /// <param name="data">The buffer where the &quot;wire/transmission&quot; format data is contained.</param>
        /// <param name="itemOffset">The offset into the data where the desired item starts.</param>
		internal I2ArraySECSItem(byte[] data, int itemOffset) : base(data, itemOffset)
		{
            int offset = 1 + NumberOfLengthBytes.ValueOf() + itemOffset;
			if ((LengthInBytes % 2) != 0)
                throw new ArgumentOutOfRangeException("Illegal data length of: " + LengthInBytes + " payload length must be a multiple of 2.");

			_value = new Int16[LengthInBytes / 2];
			byte[] temp = new byte[2];
			for (int i = offset, j = 0; j < _value.Length; i += 2, j++)
			{
				temp[0] = data[i];
				temp[1] = data[i+1];

				if (BitConverter.IsLittleEndian)
					Array.Reverse(temp);

				_value[j] = BitConverter.ToInt16(temp, 0);
			}
		}

        /// <summary>
        /// Gets the value of this <c>SECSItem</c>.
        /// </summary>
        /// <returns>the value of the <c>SECSItem</c>.</returns>
		[ObsoleteAttribute("This method has been deprecated, please use property Value instead.")]
		public Int16[] GetValue()
		{
			return _value;
		}

        /// <summary>
        /// Creates and returns a <c>byte[]</c> that represents this <c>SECSItem</c> in &quot;wire/transmission format&quot;.
        /// </summary>
        /// <returns>A <c>byte[]</c> representation of this <c>SECSItem</c>'s content that is suitable for transmission.</returns>
        public override byte[] EncodeForTransport()
		{
			byte[] output = new byte[OutputHeaderLength()+(_value.Length * 2)];
			int offset = PopulateSECSItemHeaderData(output, (_value.Length * 2));

			for( int i = offset, j = 0; j < _value.Length; i+=2, j++ )
			{
				byte[] temp = BitConverter.GetBytes(_value[j]);

				if (BitConverter.IsLittleEndian)
					Array.Reverse(temp);

				output[i]   = temp[0];
				output[i+1] = temp[1];
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
        /// Serves as a hash function for a <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.I2ArraySECSItem"/> object.
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
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.I2ArraySECSItem"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.I2ArraySECSItem"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
        /// <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.I2ArraySECSItem"/>; otherwise, <c>false</c>.</returns>
		public override bool Equals(Object? obj)
		{
            if (base.Equals(obj) == false)
                return false;

            // If we are here obj is not null
			if (GetType() != obj.GetType())
				return false;

			I2ArraySECSItem other = (I2ArraySECSItem)obj;
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

