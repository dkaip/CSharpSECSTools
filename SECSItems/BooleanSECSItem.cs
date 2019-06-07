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
	public class BooleanSECSItem : SECSItem
	{
		private bool value;
		
		public BooleanSECSItem(bool value) : base(SECSItemFormatCode.BO, 1)
		{
			this.value = value;
		}

	    public BooleanSECSItem(bool value, int desiredNUmberOfLengthBytes) : base(SECSItemFormatCode.BO, 1, desiredNUmberOfLengthBytes)
	    {
	        this.value = value;
	    }
	    
	    public BooleanSECSItem(byte[] data, int itemOffset) : base(data, itemOffset)
	    {
	        int offset = 1 + inboundNumberOfLengthBytes + itemOffset;
	        bytesConsumed = 1 + inboundNumberOfLengthBytes + lengthInBytes;

			if (data[offset] == 0)
				value = false;
			else
				value = true;
	    }
	    
	    public bool getValue()
	    {
	        return value;
	    }
	
	    public override byte[] ToRawSECSItem()
	    {
	        byte[] output = new byte[outputHeaderLength()+1];
	        int offset = populateHeaderData(output, 1);
	        
			if (value == true)
				output[offset] = 1;
			else
				output[offset] = 0;
			
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
	        BooleanSECSItem other = (BooleanSECSItem) obj;
	        if (value != other.value)
	            return false;
	        return true;
	    }
	}
}
