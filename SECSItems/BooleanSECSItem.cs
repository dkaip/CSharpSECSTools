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
    /// This class represents/implements a <c>SECSItem</c> with the SECS data type of <c>BO</c>,
    ///  which is a boolean. From the C# side this data type is handled as a C# <c>bool</c>.
    /// </summary>
	public class BooleanSECSItem : SECSItem
	{
		private bool _value;
		
		/// <summary>
		/// The value of this <c>BooleanSECSItem</c>.
		/// </summary>
		public bool Value { get { return _value; } }

        /// <summary>
        /// This constructor creates a BooleanSECSItem that will have the value of
        /// the supplied <c>bool</c>.
        /// </summary>
        /// <param name="value">The value to be assigned to this <c>SECSItem</c>.</param>
		public BooleanSECSItem(bool value) : base(SECSItemFormatCode.BO, 1)
		{
			this._value = value;
		}

        /// <summary>
        /// This constructor creates a BooleanSECSItem that will have the value of
        /// the supplied <c>bool</c>.  In addition when converted to 
        /// &quot;transmission&quot; form it will use the number of length bytes
        /// specified.
        /// </summary>
        /// <param name="value">The value to be assigned to this <c>SECSItem</c>.</param>
        /// <param name="desiredNumberOfLengthBytes">The number of length bytes to be used for this <code>SECSItem</code>.
        /// The value for the number of length bytes must be <c>ONE</c>, <c>TWO</c>, or <c>THREE</c>.</param>
        public BooleanSECSItem(bool value, SECSItemNumLengthBytes desiredNumberOfLengthBytes) : base(SECSItemFormatCode.BO, 1, desiredNumberOfLengthBytes)
	    {
	        this._value = value;
	    }
	    
        /// <summary>
        /// This constructor is used to create this <c>SECSItem</c> from data in &quot;wire/transmission&quot; format.
        /// </summary>
        /// <param name="data">The buffer where the wire/transmission format data is contained.</param>
        /// <param name="itemOffset">The offset into the data where the desired item starts.</param>
	    internal BooleanSECSItem(byte[] data, int itemOffset) : base(data, itemOffset)
	    {
            int offset = 1 + NumberOfLengthBytes.ValueOf() + itemOffset;

			if (data[offset] == 0)
				_value = false;
			else
				_value = true;
	    }
	    
        /// <summary>
        /// Gets the value of this <c>SECSItem</c>.
        /// </summary>
        /// <returns>the value of the <c>SECSItem</c>.</returns>
		[ObsoleteAttribute("This method has been deprecated, please use property Value instead.")]
	    public bool GetValue()
	    {
	        return _value;
	    }
	
        /// <summary>
        /// Creates and returns a <c>byte[]</c> that represents this <c>SECSItem</c> in &quot;wire/transmission format&quot;.
        /// </summary>
        /// <returns>A <c>byte[]</c> representation of this <c>SECSItem</c>'s content that is suitable for transmission.</returns>
	    public override byte[] EncodeForTransport()
	    {
	        byte[] output = new byte[OutputHeaderLength()+1];
	        int offset = PopulateSECSItemHeaderData(output, 1);
	        
			if (_value == true)
				output[offset] = 1;
			else
				output[offset] = 0;
			
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
        /// Serves as a hash function for a <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.BooleanSECSItem"/> object.
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
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.BooleanSECSItem"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.BooleanSECSItem"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
        /// <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.BooleanSECSItem"/>, <c>false</c> otherwise.</returns>
	    public override bool Equals(Object? obj)
	    {
            if (base.Equals(obj) == false)
                return false;

            // If we are here obj is not null
			if (GetType() != obj.GetType())
				return false;

			BooleanSECSItem other = (BooleanSECSItem)obj;
	        if (_value != other._value)
	            return false;

	        return true;
	    }
	}
}
