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
	public class I2SECSItem : SECSItem
	{
		private Int16 value;
		
		public I2SECSItem(Int16 value) : base(SECSItemFormatCode.I2, 2)
		{
			this.value = value;
		}

	    public I2SECSItem(Int16 value, int desiredNUmberOfLengthBytes) : base(SECSItemFormatCode.I2, 2, desiredNUmberOfLengthBytes)
	    {
	        this.value = value;
	    }
	    
	    public I2SECSItem(byte[] data, int itemOffset) : base(data, itemOffset)
	    {
	        int offset = 1 + inboundNumberOfLengthBytes + itemOffset;
	        bytesConsumed = 1 + inboundNumberOfLengthBytes + lengthInBytes;
	        if (lengthInBytes < 2)
	            throw new ArgumentOutOfRangeException("Illegal data length: " + data.Length + " must be 2.");
	        
			byte[] temp = new byte[2];
			temp[0] = data[offset];
			temp[1] = data[offset+1];
				
			if (BitConverter.IsLittleEndian)
				Array.Reverse(temp);
		
			value = BitConverter.ToInt16(temp, 0);
	    }
	    

		/// <summary>
		/// Returns the value of the this SECSItem
		/// </summary>
		/// <returns>The value.</returns>
	    public Int16 getValue()
	    {
	        return value;
	    }
	
	    public override byte[] ToRawSECSItem()
	    {
	        byte[] output = new byte[outputHeaderLength()+2];
	        int offset = populateHeaderData(output, 2);
	        
			byte[] temp = BitConverter.GetBytes(value);

			if (BitConverter.IsLittleEndian)
				Array.Reverse(temp);
		
			output[offset]   = temp[0];
			output[offset+1] = temp[1];
			
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
	        I2SECSItem other = (I2SECSItem) obj;
	        if (value != other.value)
	            return false;
	        return true;
	    }
	}
}
