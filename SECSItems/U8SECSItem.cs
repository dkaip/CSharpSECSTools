using System;

namespace com.CIMthetics.CSharpSECSTools.SECSItems 
{
	public class U8SECSItem : SECSItem
	{
		private UInt64 value;
		
		public U8SECSItem(UInt64 value) : base(SECSItemFormatCode.U8, 8)
		{
			this.value = value;
		}

	    public U8SECSItem(UInt64 value, int desiredNUmberOfLengthBytes) : base(SECSItemFormatCode.U8, 8, desiredNUmberOfLengthBytes)
	    {
	        this.value = value;
	    }
	    
	    public U8SECSItem(byte[] data, int itemOffset) : base(data, itemOffset)
	    {
	        int offset = 1 + inboundNumberOfLengthBytes + itemOffset;
	        bytesConsumed = 1 + inboundNumberOfLengthBytes + lengthInBytes;
	        if (lengthInBytes < 8)
	            throw new ArgumentOutOfRangeException("Illegal data length: " + data.Length + " must be 8.");
	        
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
		
			value = BitConverter.ToUInt64(temp, 0);
	    }
	    
	    public UInt64 getValue()
	    {
	        return value;
	    }
	
	    public override byte[] toRawSECSItem()
	    {
	        byte[] output = new byte[outputHeaderLength()+8];
	        int offset = populateHeaderData(output, 8);
	        
			byte[] temp = BitConverter.GetBytes(value);

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
	        U8SECSItem other = (U8SECSItem) obj;
	        if (value != other.value)
	            return false;
	        return true;
	    }
	}
}
