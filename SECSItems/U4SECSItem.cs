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
    /// This class represents a <c>SECSItem</c> with the SECS data type of <c>U4</c>,
    ///  which is a 32-bit unsigned integer. From the C# side this data type is handled as a C# <c>UInt32</c>.
    /// </summary>
	public class U4SECSItem : SECSItem
	{
		private UInt32 _value;
		
		/// <summary>
		/// The value of this <c>U4SECSItem</c>.
		/// </summary>
		public float Value { get { return _value; } }

        /// <summary>
        /// This constructor creates an U4SECSItem that will have the value of
        /// the supplied <c>UInt32</c>.
        /// </summary>
        /// <param name="value">The value to be assigned to this <c>SECSItem</c>.</param>
		public U4SECSItem(UInt32 value) : base(SECSItemFormatCode.U4, 4)
		{
			this._value = value;
		}

        /// <summary>
        /// This constructor creates an U4SECSItem that will have the value of
        /// the supplied <c>UInt32</c>.  In addition when converted to 
        /// &quot;transmission&quot; form it will use the number of length bytes
        /// specified.
        /// </summary>
        /// <param name="value">The value to be assigned to this <c>SECSItem</c>.</param>
        /// <param name="desiredNumberOfLengthBytes">The number of length bytes to be used for this <code>SECSItem</code>.
        /// The value for the number of length bytes must be <c>ONE</c>, <c>TWO</c>, or <c>THREE</c>.</param>
	    public U4SECSItem(UInt32 value, SECSItemNumLengthBytes desiredNumberOfLengthBytes) : base(SECSItemFormatCode.U4, 4, desiredNumberOfLengthBytes)
	    {
	        this._value = value;
	    }
	    
        /// <summary>
        /// This constructor is used to create this <c>SECSItem</c> from data in &quot;wire/transmission&quot; format.
        /// </summary>
        /// <param name="data">The buffer where the wire/transmission format data is contained.</param>
        /// <param name="itemOffset">The offset into the data where the desired item starts.</param>
	    internal U4SECSItem(byte[] data, int itemOffset) : base(data, itemOffset)
	    {
            int offset = 1 + NumberOfLengthBytes.ValueOf() + itemOffset;

	        if (LengthInBytes != 4)
                throw new ArgumentOutOfRangeException("Illegal data length of: " + LengthInBytes +
                    ".  The length of the data independent of the item header must be 4.");
	        
			byte[] temp = new byte[4];
			temp[0] = data[offset];
			temp[1] = data[offset+1];
			temp[2] = data[offset+2];
			temp[3] = data[offset+3];
				
			if (BitConverter.IsLittleEndian)
				Array.Reverse(temp);
		
			_value = BitConverter.ToUInt32(temp, 0);
	    }
	    
        /// <summary>
        /// Gets the value of this <c>SECSItem</c>.
        /// </summary>
        /// <returns>the value of the <c>SECSItem</c>.</returns>
		[ObsoleteAttribute("This method has been deprecated, please use property Value instead.")]
	    public UInt32 GetValue()
	    {
	        return _value;
	    }
	
        /// <summary>
        /// Creates and returns a <c>byte[]</c> that represents this <c>SECSItem</c> in &quot;wire/transmission format&quot;.
        /// </summary>
        /// <returns>A <c>byte[]</c> representation of this <c>SECSItem</c>'s content that is suitable for transmission.</returns>
	    public override byte[] EncodeForTransport()
	    {
	        byte[] output = new byte[OutputHeaderLength()+4];
	        int offset = PopulateSECSItemHeaderData(output, 4);
	        
			byte[] temp = BitConverter.GetBytes(_value);

			if (BitConverter.IsLittleEndian)
				Array.Reverse(temp);
		
			output[offset]   = temp[0];
			output[offset+1] = temp[1];
			output[offset+2] = temp[2];
			output[offset+3] = temp[3];
			
	        return output;
	    }
	    
        /// <summary>
        /// Returns a <c>string</c> representation of this item in a format
        /// suitable for debugging.
        /// </summary>
        /// <returns>A <c>string</c> representation of this item in a format suitable for debugging.</returns>
	    public override String ToString()
	    {
	        return "Format:" + ItemFormatCode.ToString() + " Value: " + _value;
	    }
	    
        /// <summary>
        /// Serves as a hash function for a <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.U4SECSItem"/> object.
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
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.U4SECSItem"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.U4SECSItem"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
        /// <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.U4SECSItem"/>; otherwise, <c>false</c>.</returns>
	    public override bool Equals(Object? obj)
	    {
            if (base.Equals(obj) == false)
                return false;

            // If we are here obj is not null
			if (GetType() != obj.GetType())
				return false;

			U4SECSItem other = (U4SECSItem)obj;
	        if (_value != other._value)
	            return false;

	        return true;
	    }
	}
}
