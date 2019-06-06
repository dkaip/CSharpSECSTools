using System;
using System.Linq;

namespace com.CIMthetics.CSharpSECSTools.SECSItems 
{
	public class F4ArraySECSItem : SECSItem
	{
		private float[] value;

		public F4ArraySECSItem(float[] value) : base(SECSItemFormatCode.F4, value.Length * 4)
		{
			this.value = value;
		}

		public F4ArraySECSItem(float[] value, int desiredNUmberOfLengthBytes) : base(SECSItemFormatCode.F4, value.Length * 4, desiredNUmberOfLengthBytes)
		{
			this.value = value;
		}

		public F4ArraySECSItem(byte[] data, int itemOffset) : base(data, itemOffset)
		{
			int offset = 1 + inboundNumberOfLengthBytes + itemOffset;
			bytesConsumed = 1 + inboundNumberOfLengthBytes + lengthInBytes;
			if ((lengthInBytes == 0) || ((lengthInBytes % 4) != 0))
				throw new ArgumentOutOfRangeException("Illegal data length: " + data.Length + " must be a non-zero multiple of 4.");

			value = new float[lengthInBytes / 4];
			byte[] temp = new byte[4];
			for (int i = offset, j = 0; j < value.Length; i += 4, j++)
			{
				temp[0] = data[i];
				temp[1] = data[i+1];
				temp[2] = data[i+2];
				temp[3] = data[i+3];

				if (BitConverter.IsLittleEndian)
					Array.Reverse(temp);

				value[j] = BitConverter.ToSingle(temp, 0);
			}
		}

		public float[] getValue()
		{
			return value;
		}


		public override byte[] toRawSECSItem()
		{
			byte[] output = new byte[outputHeaderLength()+(value.Length * 4)];
			int offset = populateHeaderData(output, (value.Length * 4));

			for( int i = offset, j = 0; j < value.Length; i+=4, j++ )
			{
				byte[] temp = BitConverter.GetBytes(value[j]);

				if (BitConverter.IsLittleEndian)
					Array.Reverse(temp);

				output[i]   = temp[0];
				output[i+1] = temp[1];
				output[i+2] = temp[2];
				output[i+3] = temp[3];
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
			F4ArraySECSItem other = (F4ArraySECSItem) obj;
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

