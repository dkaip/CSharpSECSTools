using System;

namespace com.CIMthetics.CSharpSECSTools.SECSItems 
{
	public class I4SECSItem : SECSItem
	{
		private Int32 value;
		
		public I4SECSItem(Int32 value) : base(SECSItemFormatCode.I4, 4)
		{
			this.value = value;
		}

	    public I4SECSItem(Int32 value, int desiredNUmberOfLengthBytes) : base(SECSItemFormatCode.I4, 4, desiredNUmberOfLengthBytes)
	    {
	        this.value = value;
	    }
	    
	    public I4SECSItem(byte[] data, int itemOffset) : base(data, itemOffset)
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
		
			value = BitConverter.ToInt32(temp, 0);
	    }
	    
	    public Int32 getValue()
	    {
	        return value;
	    }
	
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
	        I4SECSItem other = (I4SECSItem) obj;
	        if (value != other.value)
	            return false;
	        return true;
	    }
	}
}
