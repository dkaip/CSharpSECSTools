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
	public class F4SECSItem : SECSItem
	{
		private float value;

		/// <summary>
		/// Creates a new instance of the <see cref="F4SECSItem"/> class.
		/// Using this form of the constructor will result in the smallest 
		/// number of length bytes being used when this item is encoded for
		/// output / transport.
		/// 
		/// </summary>
		/// <param name="value">Value.</param>
		public F4SECSItem(float value) : base(SECSItemFormatCode.F4, 4)
		{
			this.value = value;
		}
		/// <summary>
		/// Creates a new instance of the <see cref="F4SECSItem"/> class.
		/// Using this form of the constructor allows the caller to directly
		/// specify the number of length bytes to be used when this item is
		/// encoded for output / transport.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="desiredNUmberOfLengthBytes">Desired N umber of length bytes.</param>
	    public F4SECSItem(float value, int desiredNUmberOfLengthBytes) : base(SECSItemFormatCode.F4, 4, desiredNUmberOfLengthBytes)
	    {
	        this.value = value;
	    }
	    
		/// <summary>
		/// Creates a new instance of the <see cref="F4SECSItem"/> class.  
		/// This method is used to create an instance of this class from
		/// the binary SECS data that is received via an HSMS or a SECS I
		/// connection.
		/// </summary>
		/// <param name="data">Binary form of a SECS II message</param>
		/// <param name="itemOffset">The offset in the input data where this constructor will grab the data for the creation of this object.</param>
	    public F4SECSItem(byte[] data, int itemOffset) : base(data, itemOffset)
	    {
	        int offset = 1 + inboundNumberOfLengthBytes + itemOffset;
	        bytesConsumed = 1 + inboundNumberOfLengthBytes + lengthInBytes;
	        if (lengthInBytes < 4)
	            throw new ArgumentOutOfRangeException("Illegal data length: " + data.Length + " must be 4.");
	        
			byte[] temp = new byte[4];
			temp[0] = data[offset];
			temp[1] = data[offset+1];
			temp[2] = data[offset+2];
			temp[3] = data[offset+3];
				
			if (BitConverter.IsLittleEndian)
				Array.Reverse(temp);
		
			value = BitConverter.ToSingle(temp, 0);
	    }
	    
		/// <summary>
		/// Returns the value of this SECSItem
		/// </summary>
		/// <returns>The value.</returns>
	    public float getValue()
	    {
	        return value;
	    }
	
		/// <summary>
		/// This method converts this SECSItem into its binary 
		/// form that will be used for output / transport.
		/// </summary>
		/// <returns>The raw SECS item.</returns>
		public override byte[] toRawSECSItem()
	    {
	        byte[] output = new byte[outputHeaderLength()+4];
	        int offset = populateHeaderData(output, 4);
	        
			byte[] temp = BitConverter.GetBytes(value);

			if (BitConverter.IsLittleEndian)
				Array.Reverse(temp);
		
			output[offset]   = temp[0];
			output[offset+1] = temp[1];
			output[offset+2] = temp[2];
			output[offset+3] = temp[3];
			
	        return output;
	    }
	    
	    public override String ToString()
	    {
	        return "Format:" + formatCode.ToString() + " Value: " + value;
	    }
	    
	    public override int GetHashCode()
	    {
	        return value.GetHashCode();
	    }
	
	    public override bool Equals(Object obj)
	    {
	        if (this == obj)
	            return true;
	        if (obj == null)
	            return false;
	        if (GetType() != obj.GetType())
	            return false;
	        F4SECSItem other = (F4SECSItem) obj;
	        if (value != other.value)
	            return false;
	        return true;
	    }
	}
}

