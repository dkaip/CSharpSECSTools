using System;
using System.Linq;

namespace com.CIMthetics.CSharpSECSTools.SECSItems
{
	public class U1ArraySECSItem : SECSItem
	{
		private byte[] value = null;

		public U1ArraySECSItem(byte[] value) : base(SECSItemFormatCode.U1, value.Length)
		{
			this.value = value;
		}

		public U1ArraySECSItem(byte[] value, int desiredNumberOfLengthBytes) : base(SECSItemFormatCode.U1, value.Length, desiredNumberOfLengthBytes)
		{
			this.value = value;
		}

		/*
		 * This constructor requires a different signature than the one above.
		 * Hopefully this will not be much of an issue since this constructor
		 * will most likely be used in the lower level code that once running
		 * will tend to be very stable.
		 */
		public U1ArraySECSItem(byte[] data, int itemOffset, int bogus) : base(data, itemOffset)
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
			U1ArraySECSItem other = (U1ArraySECSItem) obj;
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

