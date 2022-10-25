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
    /// This class represents/implements a <code>SECSItem</code> with the SECS data type of <c>I2</c>,
    /// which is a 2 byte (16-bit) signed integer number. From the C# side this data
    /// type is handled as a C# <c>Int16</c>.
    /// </summary>
	public class I2SECSItem : SECSItem
	{
		private Int16 value;
		
        /// <summary>
        /// This constructor creates a <code>SECSItem</code> that has a type of <c>I2</c>
        /// with the specified value. Note: It will be created with 1 length byte.
        /// </summary>
        /// <param name="value">The value to be assigned to this <c>SECSItem</c>.</param>
		public I2SECSItem(Int16 value) : base(SECSItemFormatCode.I2, 2)
		{
			this.value = value;
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.I2SECSItem"/> class.
        /// </summary>
        /// <param name="value">The value to be assigned to this <c>SECSItem</c>.</param>
        /// <param name="desiredNumberOfLengthBytes">The number of length bytes to be used for this <c>SECSItem</c>.
        /// The value for the number of length bytes must be <c>ONE</c>, <c>TWO</c>, or <c>THREE</c>.</param>
	    public I2SECSItem(Int16 value, SECSItemNumLengthBytes desiredNumberOfLengthBytes) : base(SECSItemFormatCode.I2, 2, desiredNumberOfLengthBytes)
	    {
	        this.value = value;
	    }
	    
        /// <summary>
        /// This constructor is used to create this SECSItem from
        /// data in &quot;wire/transmission&quot; format.
        /// </summary>
        /// <param name="data">The buffer where the &quot;wire/transmission&quot; format data is contained.</param>
        /// <param name="itemOffset">The offset into the data where the desired item starts.</param>
	    public I2SECSItem(byte[] data, int itemOffset) : base(data, itemOffset)
	    {
            int offset = 1 + inboundNumberOfLengthBytes.ValueOf ()+ itemOffset;
            bytesConsumed = 1 + inboundNumberOfLengthBytes.ValueOf () + lengthInBytes;
	        if (lengthInBytes != 2)
                throw new ArgumentOutOfRangeException("Illegal data length of: " + lengthInBytes +
                    ".  The length of the data independent of the item header must be 2.");
	        
			byte[] temp = new byte[2];
			temp[0] = data[offset];
			temp[1] = data[offset+1];
				
			if (BitConverter.IsLittleEndian)
				Array.Reverse(temp);
		
			value = BitConverter.ToInt16(temp, 0);
	    }
	    

        /// <summary>
        /// Gets the value of this <c>SECSItem</c>.
        /// </summary>
        /// <returns>the value of the <c>SECSItem</c>.</returns>
	    public Int16 GetValue()
	    {
	        return value;
	    }
	
        /// <summary>
        /// Creates and returns a <c>byte[]</c> that represents this <c>SECSItem</c> in &quot;wire/transmission format&quot;.
        /// </summary>
        /// <returns>A <c>byte[]</c> representation of this <c>SECSItem</c>'s content that is suitable for transmission.</returns>
	    public override byte[] EncodeForTransport()
	    {
	        byte[] output = new byte[OutputHeaderLength()+2];
	        int offset = PopulateSECSItemHeaderData(output, 2);
	        
			byte[] temp = BitConverter.GetBytes(value);

			if (BitConverter.IsLittleEndian)
				Array.Reverse(temp);
		
			output[offset]   = temp[0];
			output[offset+1] = temp[1];
			
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
        /// Serves as a hash function for a <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.I2SECSItem"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
        /// hash table.</returns>
	    public override int GetHashCode()
	    {
	        return value.GetHashCode();
	    }
	
        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.I2SECSItem"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.I2SECSItem"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
        /// <see cref="T:com.CIMthetics.CSharpSECSTools.SECSItems.I2SECSItem"/>; otherwise, <c>false</c>.</returns>
	    public override bool Equals(Object obj)
	    {
	        if (this == obj)
	            return true;
	        if (obj == null)
	            return false;
	        if (GetType() != obj.GetType())
	            return false;
	        I2SECSItem other = (I2SECSItem) obj;
	        if (value != other.value)
	            return false;
	        return true;
	    }
	}
}
