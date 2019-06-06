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
using System.Linq;

namespace com.CIMthetics.CSharpSECSTools.SECSItems 
{
	public class I2ArraySECSItem : SECSItem
	{
		private Int16[] value;

		public I2ArraySECSItem(Int16[] value) : base(SECSItemFormatCode.I2, value.Length * 2)
		{
			this.value = value;
		}

		public I2ArraySECSItem(Int16[] value, int desiredNUmberOfLengthBytes) : base(SECSItemFormatCode.I2, value.Length * 2, desiredNUmberOfLengthBytes)
		{
			this.value = value;
		}

		public I2ArraySECSItem(byte[] data, int itemOffset) : base(data, itemOffset)
		{
			int offset = 1 + inboundNumberOfLengthBytes + itemOffset;
			bytesConsumed = 1 + inboundNumberOfLengthBytes + lengthInBytes;
			if ((lengthInBytes == 0) || ((lengthInBytes % 2) != 0))
				throw new ArgumentOutOfRangeException("Illegal data length: " + data.Length + " must be a non-zero multiple of 2.");

			value = new Int16[lengthInBytes / 2];
			byte[] temp = new byte[2];
			for (int i = offset, j = 0; j < value.Length; i += 2, j++)
			{
				temp[0] = data[i];
				temp[1] = data[i+1];

				if (BitConverter.IsLittleEndian)
					Array.Reverse(temp);

				value[j] = BitConverter.ToInt16(temp, 0);
			}
		}

		public Int16[] getValue()
		{
			return value;
		}


		public override byte[] toRawSECSItem()
		{
			byte[] output = new byte[outputHeaderLength()+(value.Length * 2)];
			int offset = populateHeaderData(output, (value.Length * 2));

			for( int i = offset, j = 0; j < value.Length; i+=2, j++ )
			{
				byte[] temp = BitConverter.GetBytes(value[j]);

				if (BitConverter.IsLittleEndian)
					Array.Reverse(temp);

				output[i]   = temp[0];
				output[i+1] = temp[1];
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
			I2ArraySECSItem other = (I2ArraySECSItem) obj;
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

