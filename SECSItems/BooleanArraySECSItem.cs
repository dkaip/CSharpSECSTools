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
	public class BooleanArraySECSItem : SECSItem
	{
		private bool[] value = null;

		public BooleanArraySECSItem(bool[] value) : base(SECSItemFormatCode.BO, value.Length)
		{
			this.value = value;
		}

		public BooleanArraySECSItem(bool[] value, int desiredNumberOfLengthBytes) : base(SECSItemFormatCode.BO, value.Length, desiredNumberOfLengthBytes)
		{
			this.value = value;
		}

		public BooleanArraySECSItem(byte[] data, int itemOffset) : base(data, itemOffset)
		{
			int offset = 1 + inboundNumberOfLengthBytes + itemOffset;
			bytesConsumed = 1 + inboundNumberOfLengthBytes + lengthInBytes;

			value = new bool[lengthInBytes];
			for(int i = 0, j = offset; i < value.Length; i++, j++)
			{
				if (data[j] == 0)
					value[i] = false;
				else
					value[i] = true;
			}
		}

		public bool[] getValue()
		{
			return value;
		}

		public override byte[] ToRawSECSItem()
		{
			byte[] output = new byte[outputHeaderLength()+value.Length];
			int offset = populateHeaderData(output, value.Length);

			for( int i = offset, j = 0; j < value.Length; i++, j++)
			{
				if (value[j] == false)
					output[i] = 0;
				else
					output[i] = 1;
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
			BooleanArraySECSItem other = (BooleanArraySECSItem) obj;
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

