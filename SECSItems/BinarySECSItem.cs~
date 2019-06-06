using System;
using System.Linq;

namespace com.CIMthetics.CSharpSECSTools.SECSItems
{
	public class BinarySECSItem : SECSItem
	{
		private byte[] value = null;
		
		public BinarySECSItem(byte[] value) : base(SECSItemFormatCode.B, value.Length)
		{
			this.value = value;
		}
		
		public BinarySECSItem(byte[] value, int desiredNumberOfLengthBytes) : base(SECSItemFormatCode.B, value.Length, desiredNumberOfLengthBytes)
		{
			this.value = value;
		}
		
		/*
		 * This constructor requires a different signature than the one above.
		 * Hopefully this will not be much of an issue since this constructor
		 * will most likely be used in the lower level code that once running
		 * will tend to be very stable.
		 */
		public BinarySECSItem(byte[] data, int itemOffset, int bogus) : base(data, itemOffset)
		{
			int offset = 1 + inboundNumberOfLengthBytes + itemOffset;
			bytesConsumed = 1 + inboundNumberOfLengthBytes + lengthInBytes;
			
			value = new byte[lengthInBytes];
			for(int i = 0, j = offset; i < value.Length; i++, j++)
			{
				value[i] = data[j];
			}
		}
			
		public byte[] getValue()
		{
			return value;
		}
		
		public override byte[] toRawSECSItem()
		{
			byte[] output = new byte[outputHeaderLength()+value.Length];
			int offset = populateHeaderData(output, value.Length);
			
			for( int i = offset, j = 0; j < value.Length; i++, j++)
			{
				output[i] = value[j];
			}
			
			return output;
		}
		
	    public override String ToString()
	    {
			if (value.Length == 1)
		        return "Format:" + formatCode.ToString() + " Value: " + value[0];
			else
		        return "Format:" + formatCode.ToString() + " Value: Array";
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

