/*
 * Copyright 2019-20223Douglas Kaip
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
    /// This class represents/implements a <c>SECSItem</c> with the SECS data type of <c>F8</c>,
    ///  which is a double. From the C# side this data type is handled as a C# <c>double</c>.
    /// </summary>
	public class F8SECSItem : SECSItem
	{
		private double _value;
		
		/// <summary>
		/// The value of this <c>F8SECSItem</c>.
		/// </summary>
		public double Value { get { return _value; } }

        /// <summary>
        /// This constructor creates an F8SECSItem that will have the value of
        /// the supplied <c>double</c>.
        /// </summary>
        /// <param name="value">The value to be assigned to this <c>SECSItem</c>.</param>
		public F8SECSItem(double value) : base(SECSItemFormatCode.F8, 8)
		{
			this._value = value;
		}

        /// <summary>
        /// This constructor creates an F8SECSItem that will have the value of
        /// the supplied <c>double</c>.  In addition when converted to 
        /// &quot;transmission&quot; form it will use the number of length bytes
        /// specified.
        /// </summary>
        /// <param name="value">The value to be assigned to this <c>SECSItem</c>.</param>
        /// <param name="desiredNumberOfLengthBytes">The number of length bytes to be used for this <code>SECSItem</code>.
        /// The value for the number of length bytes must be <c>ONE</c>, <c>TWO</c>, or <c>THREE</c>.</param>
        public F8SECSItem(double value, SECSItemNumLengthBytes desiredNumberOfLengthBytes) : base(SECSItemFormatCode.F8, 8, desiredNumberOfLengthBytes)
	    {
	        this._value = value;
	    }
	    
        /// <summary>
        /// This constructor is used to create this SECSItem from
        /// data in &quot;wire/transmission&quot; format.
        /// </summary>
        /// <param name="data">The buffer where the &quot;wire/transmission&quot; format data is contained.</param>
        /// <param name="itemOffset">The offset into the data where the desired item starts.</param>
	    internal F8SECSItem(byte[] data, int itemOffset) : base(data, itemOffset)
	    {
            int offset = 1 + NumberOfLengthBytes.ValueOf() + itemOffset;
	        if (LengthInBytes != 8)
                throw new ArgumentOutOfRangeException("Illegal data length of: " + LengthInBytes +
                    ".  The length of the data independent of the item header must be 8.");
	        
			byte[] temp = new byte[8];
			temp[0] = data[offset];
			temp[1] = data[offset+1];
			temp[2] = data[offset+2];
			temp[3] = data[offset+3];
			temp[4] = data[offset+4];
			temp[5] = data[offset+5];
			temp[6] = data[offset+6];
			temp[7] = data[offset+7];
				
			if (BitConverter.IsLittleEndian)
				Array.Reverse(temp);
		
			_value = BitConverter.ToDouble(temp, 0);
	    }
	    
        /// <summary>
        /// Gets the value of this <c>SECSItem</c>.
        /// </summary>
        /// <returns>the value of the <c>SECSItem</c>.</returns>
		[ObsoleteAttribute("This method has been deprecated, please use property Value instead.")]
	    public double GetValue()
	    {
	        return _value;
	    }
	
        /// <summary>
        /// Creates and returns a <c>byte[]</c> that represents this <c>SECSItem</c> in &quot;wire/transmission format&quot;.
        /// </summary>
        /// <returns>A <c>byte[]</c> representation of this <c>SECSItem</c>'s content that is suitable for transmission.</returns>
	    public override byte[] EncodeForTransport()
	    {
	        byte[] output = new byte[OutputHeaderLength()+8];
	        int offset = PopulateSECSItemHeaderData(output, 8);
	        
			byte[] temp = BitConverter.GetBytes(_value);

			if (BitConverter.IsLittleEndian)
				Array.Reverse(temp);
		
			output[offset]   = temp[0];
			output[offset+1] = temp[1];
			output[offset+2] = temp[2];
			output[offset+3] = temp[3];
			output[offset+4] = temp[4];
			output[offset+5] = temp[5];
			output[offset+6] = temp[6];
			output[offset+7] = temp[7];
			
	        return output;
	    }
	    
        /// <summary>
        /// Returns a <c>string</c> representation of this item in a format
        /// suitable for debugging.
        /// </summary>
        /// <returns>a <c>string</c> representation of this item in a format suitable for debugging.</returns>
	    public override String ToString()
	    {
	        return "Format:" + ItemFormatCode.ToString() + " Value: " + _value;
	    }
	    
        /// <summary>
        /// Serves as a hash function for a <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.F8SECSItem"/> object.
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
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.F8SECSItem"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.F8SECSItem"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
        /// <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.F8SECSItem"/>, <c>false</c> otherwise.</returns>
	    public override bool Equals(Object? obj)
	    {
            if (base.Equals(obj) == false)
                return false;

            // If we are here obj is not null
			if (GetType() != obj.GetType())
				return false;

			F8SECSItem other = (F8SECSItem)obj;
	        if (_value != other._value)
	            return false;

	        return true;
	    }
	}
}
