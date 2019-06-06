using System;
using System.Linq;

namespace com.CIMthetics.CSharpSECSTools.SECSItems 
{
	public class I8ArraySECSItem : SECSItem
	{
		private Int64[] value;

		public I8ArraySECSItem(Int64[] value) : base(SECSItemFormatCode.I8, value.Length * 8)
		{
			this.value = value;
		}

		public I8ArraySECSItem(Int64[] value, int desiredNUmberOfLengthBytes) : base(SECSItemFormatCode.I8, value.Length * 8, desiredNUmberOfLengthBytes)
		{
			this.value = value;
		}

		public I8ArraySECSItem(byte[] data, int itemOffset) : base(data, itemOffset)
		{
			int offset = 1 + inboundNumberOfLengthBytes + itemOffset;
			bytesConsumed = 1 + inboundNumberOfLengthBytes + lengthInBytes;
			if ((lengthInBytes == 0) || ((lengthInBytes % 8) != 0))
				throw new ArgumentOutOfRangeException("Illegal data length: " + data.Length + " must be a non-zero multiple of 8.");

			value = new Int64[lengthInBytes / 8];
			byte[] temp = new byte[8];
			for (int i = offset, j = 0; j < value.Length; i += 8, j++)
			{
				temp[0] = data[i];
				temp[1] = data[i+1];
				temp[2] = data[i+2];
				temp[3] = data[i+3];
				temp[4] = data[i+4];
				temp[5] = data[i+5];
				temp[6] = data[i+6];
				temp[7] = data[i+7];

				if (BitConverter.IsLittleEndian)
					Array.Reverse(temp);

				value[j] = BitConverter.ToInt64(temp, 0);
			}
		}

		public Int64[] getValue()
		{
			return value;
		}


		public override byte[] toRawSECSItem()
		{
			byte[] output = new byte[outputHeaderLength()+(value.Length * 8)];
			int offset = populateHeaderData(output, (value.Length * 8));

			for( int i = offset, j = 0; j < value.Length; i+=8, j++ )
			{
				byte[] temp = BitConverter.GetBytes(value[j]);

				if (BitConverter.IsLittleEndian)
					Array.Reverse(temp);

				output[i]   = temp[0];
				output[i+1] = temp[1];
				output[i+2] = temp[2];
				output[i+3] = temp[3];
				output[i+4] = temp[4];
				output[i+5] = temp[5];
				output[i+6] = temp[6];
				output[i+7] = temp[7];

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
			I8ArraySECSItem other = (I8ArraySECSItem) obj;
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

