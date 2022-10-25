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
using System;

namespace com.CIMthetics.CSharpSECSTools.SECSItems 
{
    /// <summary>
    /// This class represents/implements a <c>SECSItem</c> with the SECS data type of <c>BO</c>,
    ///  which is a boolean. From the C# side this data type is handled as a C# <c>bool</c>.
    /// </summary>
	public class BooleanSECSItem : SECSItem
	{
		private bool value;
		
        /// <summary>
        /// This constructor creates a <c>SECSItem</c> that has a type of <c>BO</c> 
        /// with the specified value. Note: It will be created with 1 length byte.
        /// </summary>
        /// <param name="value">The value to be assigned to this <code>SECSItem</code>.</param>
		public BooleanSECSItem(bool value) : base(SECSItemFormatCode.BO, 1)
		{
			this.value = value;
		}

        /// <summary>
        /// This constructor creates a SECSItem that has a type of <c>BO</c>
        /// with the specified value. This form of the constructor is not needed much nowadays. 
        /// In the past there were situations where the equipment required that messages
        /// contained SECSItems that had a specified number of length bytes. This form of the 
        /// constructor is here to handle these problem child cases.
        /// Note: It will be created with the specified number of length bytes.
        /// </summary>
        /// <param name="value">The value to be assigned to this <code>SECSItem</code>.</param>
        /// <param name="desiredNumberOfLengthBytes">The number of length bytes to be used for this <code>SECSItem</code>.
        /// The value for the number of length bytes must be <c>ONE</c>, <c>TWO</c>, or <c>THREE</c>.</param>
        public BooleanSECSItem(bool value, SECSItemNumLengthBytes desiredNumberOfLengthBytes) : base(SECSItemFormatCode.BO, 1, desiredNumberOfLengthBytes)
	    {
	        this.value = value;
	    }
	    
        /// <summary>
        /// This constructor is used to create this <c>SECSItem</c> from data in &quot;wire/transmission&quot; format.
        /// </summary>
        /// <param name="data">The buffer where the wire/transmission format data is contained.</param>
        /// <param name="itemOffset">The offset into the data where the desired item starts.</param>
	    public BooleanSECSItem(byte[] data, int itemOffset) : base(data, itemOffset)
	    {
            int offset = 1 + inboundNumberOfLengthBytes.ValueOf () + itemOffset;
            bytesConsumed = 1 + inboundNumberOfLengthBytes.ValueOf () + lengthInBytes;

			if (data[offset] == 0)
				value = false;
			else
				value = true;
	    }
	    
        /// <summary>
        /// Gets the value of this <c>SECSItem</c> in a C# friendly format.
        /// </summary>
        /// <returns>A C# <c>bool</c> that contains this item's value.</returns>
	    public bool GetValue()
	    {
	        return value;
	    }
	
        /// <summary>
        /// Creates and returns a <c>byte[]</c> that represents this <c>SECSItem</c> in &quot;wire/transmission format&quot;.
        /// </summary>
        /// <returns>A <c>byte[]</c> representation of this <c>SECSItem</c>'s content that is suitable for transmission.</returns>
	    public override byte[] EncodeForTransport()
	    {
	        byte[] output = new byte[OutputHeaderLength()+1];
	        int offset = PopulateSECSItemHeaderData(output, 1);
	        
			if (value == true)
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
	        return "Format:" + formatCode.ToString() + " Value: " + value;
	    }
	    
        /// <summary>
        /// Serves as a hash function for a <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.BooleanSECSItem"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
        /// hash table.</returns>
	    public override int GetHashCode()
	    {
	        return value.GetHashCode();
	    }
	
        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.BooleanSECSItem"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.BooleanSECSItem"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
        /// <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.BooleanSECSItem"/>; otherwise, <c>false</c>.</returns>
	    public override bool Equals(Object obj)
	    {
	        if (this == obj)
	            return true;
	        if (obj == null)
	            return false;
	        if (GetType() != obj.GetType())
	            return false;
	        BooleanSECSItem other = (BooleanSECSItem) obj;
	        if (value != other.value)
	            return false;
	        return true;
	    }
	}
}
