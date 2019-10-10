/*
 * Copyright 2019 Douglas Kaip
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
    /// This class represents/implements a <c>SECSItem</c> with the SECS data type of <c>U1</c>,
    /// which is an unsigned byte.  From the C# side this data type is handled as a C# <code>byte</code>.
    /// </summary>
	public class U1SECSItem : SECSItem
	{
		private byte value;
		
        /// <summary>
        /// This constructor creates a <c>SECSItem</c> that has a type of <c>U1</c> with 
        /// the minimum number of length bytes required.
        /// </summary>
        /// <param name="value">The value to be assigned to this <c>SECSItem</c>.</param>
		public U1SECSItem(byte value) : base(SECSItemFormatCode.U1, 1)
		{
			this.value = value;
		}

        /// <summary>
        /// This constructor creates a <c>SECSItem</c> that has a type of <c>U1</c> with
        /// a specified number of length bytes.
        /// 
        /// This form of the constructor is not needed much nowadays.  In the past
        /// there were situations where the equipment required that messages
        /// contained SECSItems that had a specified number of length bytes.
        /// This form of the constructor is here to handle these problem child cases.
        /// 
        /// Note: It will be created with the number of length bytes set
        /// to the greater of the number of length bytes specified or
        /// the number required based on the length of the <c>value</c>
        /// parameter.
        /// </summary>
        /// <param name="value">The value to be assigned to this <c>SECSItem</c>.</param>
        /// <param name="desiredNumberOfLengthBytes">The number of length bytes to be used for this <c>SECSItem</c>.
        /// The value for the number of length bytes must be <c>ONE</c>, <c>TWO</c>, or <c>THREE</c>.</param>
	    public U1SECSItem(byte value, SECSItemNumLengthBytes desiredNumberOfLengthBytes) : base(SECSItemFormatCode.U1, 1, desiredNumberOfLengthBytes)
	    {
	        this.value = value;
	    }
	    
        /// <summary>
        /// This constructor is used to create this SECSItem from
        /// data in &quot;wire/transmission&quot; format.
        /// </summary>
        /// <param name="data">The buffer where the &quot;wire/transmission&quot; format data is contained.</param>
        /// <param name="itemOffset">The offset into the data where the desired item starts.</param>
	    public U1SECSItem(byte[] data, int itemOffset) : base(data, itemOffset)
	    {
            int offset = 1 + inboundNumberOfLengthBytes.ValueOf () + itemOffset;
            bytesConsumed = 1 + inboundNumberOfLengthBytes.ValueOf () + lengthInBytes;
	        if (lengthInBytes != 1)
                throw new ArgumentOutOfRangeException("Illegal data length of: " + lengthInBytes +
                    ".  The length of the data independent of the item header must be 1.");
	        
			value = data[offset];
	    }
	    
        /// <summary>
        /// Gets the value of this <c>SECSItem</c>.
        /// </summary>
        /// <returns>the value of the <c>SECSItem</c>.</returns>
        public byte GetValue()
	    {
	        return value;
	    }
	
        /// <summary>
        /// Creates and returns a <c>byte[]</c> that represents this <c>SECSItem</c> in &quot;wire/transmission format&quot;.
        /// </summary>
        /// <returns>A <c>byte[]</c> representation of this <c>SECSItem</c>'s content that is suitable for transmission.</returns>
	    public override byte[] ToRawSECSItem()
	    {
	        byte[] output = new byte[OutputHeaderLength()+1];
	        int offset = PopulateSECSItemHeaderData(output, 1);

			output[offset] = value;

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
        /// Serves as a hash function for a <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.U1SECSItem"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
        /// hash table.</returns>
	    public override int GetHashCode()
	    {
	        return value.GetHashCode();
	    }
	
        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.U1SECSItem"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.U1SECSItem"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
        /// <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.U1SECSItem"/>; otherwise, <c>false</c>.</returns>
	    public override bool Equals(Object obj)
	    {
	        if (this == obj)
	            return true;
	        if (obj == null)
	            return false;
	        if (GetType() != obj.GetType())
	            return false;
	        U1SECSItem other = (U1SECSItem) obj;
	        if (value != other.value)
	            return false;
	        return true;
	    }
	}
}
