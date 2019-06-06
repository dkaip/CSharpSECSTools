using System;

namespace com.CIMthetics.CSharpSECSTools.SECSItems 
{
	public class U2SECSItem : SECSItem
	{
		private UInt16 value;
		
		public U2SECSItem(UInt16 value) : base(SECSItemFormatCode.U2, 2)
		{
			this.value = value;
		}

	    public U2SECSItem(UInt16 value, int desiredNUmberOfLengthBytes) : base(SECSItemFormatCode.U2, 2, desiredNUmberOfLengthBytes)
	    {
	        this.value = value;
	    }
	    
	    public U2SECSItem(byte[] data, int itemOffset) : base(data, itemOffset)
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
		
			value = BitConverter.ToUInt16(temp, 0);
	    }
	    
	    public UInt16 getValue()
	    {
	        return value;
	    }
	
	    public override byte[] toRawSECSItem()
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
	        U2SECSItem other = (U2SECSItem) obj;
	        if (value != other.value)
	            return false;
	        return true;
	    }
	}
}
