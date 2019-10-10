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
using System.Linq;

namespace com.CIMthetics.CSharpSECSTools.SECSItems
{
    /// <summary>
    /// This class represents/implements a SECSItem with the SECS data type of <c>B</c>(<c>BINARY</c>),
    /// which is an array of unsigned bytes.
    /// </summary>
	public class BinarySECSItem : SECSItem
	{
		private byte[] value = null;
		
        /// <summary>
        /// This constructor creates a SECSItem that has a type of <c>B</c> with 
        /// the minimum number of length bytes required. Note: It will be created 
        /// with the number of length bytes required based on the length (in elements) 
        /// of the <c>byte []</c> provided. The maximum array length allowed is 
        /// <c>16777215</c> bytes(elements).
        /// </summary>
        /// <param name="value">An array of binary values to be assigned to this <c>SECSItem</c>.</param>
		public BinarySECSItem(byte[] value) : base(SECSItemFormatCode.B, value.Length)
		{
			this.value = value;
		}
		
        /// <summary>
        /// This constructor creates a SECSItem that has a type of <c>B</c> with
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
        /// <param name="value">The value to be assigned to this SECSItem.</param>
        /// <param name="desiredNumberOfLengthBytes">The number of length bytes to be used for this SECSItem.
        /// The value for the number of length bytes must be ONE, TWO, or THREE..</param>
        public BinarySECSItem(byte[] value, SECSItemNumLengthBytes desiredNumberOfLengthBytes) : base(SECSItemFormatCode.B, value.Length, desiredNumberOfLengthBytes)
		{
			this.value = value;
		}
		
        /// <summary>
        /// This constructor is used to create this SECSItem from
        /// data in &quot;wire/transmission&quot; format.
        /// </summary>
        /// <param name="data">The buffer where the &quot;wire/transmission&quot; format data is contained.</param>
        /// <param name="itemOffset">The offset into the data where the desired item starts.</param>
		public BinarySECSItem(byte[] data, int itemOffset) : base(data, itemOffset)
		{
            int offset = 1 + inboundNumberOfLengthBytes.ValueOf () + itemOffset;
            bytesConsumed = 1 + inboundNumberOfLengthBytes.ValueOf () + lengthInBytes;
			
			value = new byte[lengthInBytes];
			for(int i = 0, j = offset; i < value.Length; i++, j++)
			{
				value[i] = data[j];
			}
		}
	    
        /// <summary>
        /// Gets the value of this <c>SECSItem</c>.
        /// </summary>
        /// <returns>the value of the <c>SECSItem</c>.</returns>
		public byte[] GetValue()
		{
			return value;
		}
		
        /// <summary>
        /// Creates and returns a <c>byte[]</c> that represents this <c>SECSItem</c> in &quot;wire/transmission format&quot;.
        /// </summary>
        /// <returns>A <c>byte[]</c> representation of this <c>SECSItem</c>'s content that is suitable for transmission.</returns>
		public override byte[] ToRawSECSItem()
		{
			byte[] output = new byte[OutputHeaderLength()+value.Length];
			int offset = PopulateSECSItemHeaderData(output, value.Length);
			
			for( int i = offset, j = 0; j < value.Length; i++, j++)
			{
				output[i] = value[j];
			}
			
			return output;
		}
		
        /// <summary>
        /// Returns a <c>string</c> representation of this item in a format
        /// suitable for debugging.
        /// </summary>
        /// <returns>A <c>string</c> representation of this item in a format suitable for debugging.</returns>
	    public override String ToString()
	    {
			if (value.Length == 1)
		        return "Format:" + formatCode.ToString() + " Value: " + value[0];
			else
		        return "Format:" + formatCode.ToString() + " Value: Array";
	    }
	    
        /// <summary>
        /// Serves as a hash function for a <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.BinarySECSItem"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
        /// hash table.</returns>
	    public override int GetHashCode()
	    {
	        return value.GetHashCode();
	    }
	
        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.BinarySECSItem"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.BinarySECSItem"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
        /// <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.BinarySECSItem"/>; otherwise, <c>false</c>.</returns>
	    public override bool Equals(Object obj)
	    {
	        if (this == obj)
	            return true;
	        if (obj == null)
	            return false;
	        if (GetType() != obj.GetType())
	            return false;
	        BinarySECSItem other = (BinarySECSItem) obj;
			if (value == null && other.value == null)
				return true;
			if (value == null)
			{
				if (other.value != null)
					return false;
			}
			if (other.value == null)
			{
				if (value != null)
					return false;
			}
			if (value.Length != other.value.Length)
				return false;

			return value.SequenceEqual(other.value);
	    }
		
				
	}
}

