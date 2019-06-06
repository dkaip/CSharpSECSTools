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
	public class I1ArraySECSItem : SECSItem
	{
		private sbyte[] value = null;

		public I1ArraySECSItem(sbyte[] value) : base(SECSItemFormatCode.I1, value.Length)
		{
			this.value = value;
		}

		public I1ArraySECSItem(sbyte[] value, int desiredNumberOfLengthBytes) : base(SECSItemFormatCode.I1, value.Length, desiredNumberOfLengthBytes)
		{
			this.value = value;
		}

		/*
		 * This constructor requires a different signature than the one above.
		 * Hopefully this will not be much of an issue since this constructor
		 * will most likely be used in the lower level code that once running
		 * will tend to be very stable.
		 */
		public I1ArraySECSItem(byte[] data, int itemOffset, int bogus) : base(data, itemOffset)
		{
			int offset = 1 + inboundNumberOfLengthBytes + itemOffset;
			bytesConsumed = 1 + inboundNumberOfLengthBytes + lengthInBytes;

			value = new sbyte[lengthInBytes];
			for(int i = 0, j = offset; i < value.Length; i++, j++)
			{
				value[i] = (sbyte)data[j];
			}
		}

		public sbyte[] getValue()
		{
			return value;
		}

		public override byte[] toRawSECSItem()
		{
			byte[] output = new byte[outputHeaderLength()+value.Length];
			int offset = populateHeaderData(output, value.Length);

			for( int i = offset, j = 0; j < value.Length; i++, j++)
			{
				output[i] = (byte)value[j];
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
			I1ArraySECSItem other = (I1ArraySECSItem) obj;
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

