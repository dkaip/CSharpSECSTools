using System;

namespace com.CIMthetics.CSharpSECSTools.SECSItems 
{
	public class U1SECSItem : SECSItem
	{
		private byte value;
		
		public U1SECSItem(byte value) : base(SECSItemFormatCode.U1, 1)
		{
			this.value = value;
		}

	    public U1SECSItem(byte value, int desiredNUmberOfLengthBytes) : base(SECSItemFormatCode.U1, 1, desiredNUmberOfLengthBytes)
	    {
	        this.value = value;
	    }
	    
	    public U1SECSItem(byte[] data, int itemOffset) : base(data, itemOffset)
	    {
	        int offset = 1 + inboundNumberOfLengthBytes + itemOffset;
	        bytesConsumed = 1 + inboundNumberOfLengthBytes + lengthInBytes;
	        if (lengthInBytes < 1)
	            throw new ArgumentOutOfRangeException("Illegal data length: " + data.Length + " must be 1.");
	        
			value = data[offset];
	    }
	    
	    public byte getValue()
	    {
	        return value;
	    }
	
	    public override byte[] toRawSECSItem()
	    {
	        byte[] output = new byte[outputHeaderLength()+1];
	        int offset = populateHeaderData(output, 1);

			output[offset] = value;

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
	        U1SECSItem other = (U1SECSItem) obj;
	        if (value != other.value)
	            return false;
	        return true;
	    }
	}
}
