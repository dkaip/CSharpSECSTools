using System;
using System.Text;

namespace com.CIMthetics.CSharpSECSTools.SECSItems 
{
	public class ASCIISECSItem : SECSItem
	{
		private string value;

		public ASCIISECSItem(string value) : base(SECSItemFormatCode.A, 1)
		{
			this.value = value;
		}

		public ASCIISECSItem(string value, int desiredNUmberOfLengthBytes) : base(SECSItemFormatCode.A, 1, desiredNUmberOfLengthBytes)
		{
			this.value = value;
		}

		public ASCIISECSItem(byte[] data, int itemOffset) : base(data, itemOffset)
		{
			int offset = 1 + inboundNumberOfLengthBytes + itemOffset;
			bytesConsumed = 1 + inboundNumberOfLengthBytes + lengthInBytes;

			byte[] temp = new byte[lengthInBytes];
			Array.Copy(data, offset, temp, 0, lengthInBytes);
			value = System.Text.Encoding.ASCII.GetString(temp);
		}

		public string getValue()
		{
			return value;
		}

		public override byte[] toRawSECSItem()
		{
			byte[] output = new byte[outputHeaderLength()+value.Length];
			int offset = populateHeaderData(output, value.Length);

			byte[] temp = Encoding.ASCII.GetBytes(value);

			Array.Copy(temp, 0, output, offset, temp.Length);

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
			ASCIISECSItem other = (ASCIISECSItem) obj;
			if (value != other.value)
				return false;
			return true;
		}
	}
}
