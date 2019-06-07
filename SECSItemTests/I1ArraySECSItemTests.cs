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
using NUnit.Framework;
using com.CIMthetics.CSharpSECSTools.SECSItems;

namespace SECSItemTests
{
	[TestFixture()]
	public class I1ArraySECSItemTests
	{
		[Test()]
		public void test01()
		{
			byte[] input = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I1 ) << 2) | 0x01), 0x04, 255, 128, 0, 127};
			I1ArraySECSItem secsItem = new I1ArraySECSItem(input, 0, 0);
			Assert.IsTrue(secsItem.getValue()[0] == -1);
			Assert.IsTrue(secsItem.getValue()[1] == -128);
			Assert.IsTrue(secsItem.getValue()[2] == 0);
			Assert.IsTrue(secsItem.getValue()[3] == 127);
		}

		[Test()]
		public void test02()
		{
			sbyte[] input = {-1, -128, 0, 127};
			I1ArraySECSItem secsItem = new I1ArraySECSItem(input);
			Assert.IsTrue(secsItem.getValue().SequenceEqual(input));
		}

		[Test()]
		public void test03()
		{
			byte[] input = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I1 ) << 2) | 0x01), 0x04, 255, 128, 0, 127};
			I1ArraySECSItem secsItem = new I1ArraySECSItem(input, 0, 0);
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.I1);
		}

		[Test()]
		public void test04()
		{
			sbyte[] input = {-1, -128, 0, 127};
			I1ArraySECSItem secsItem = new I1ArraySECSItem(input);
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.I1);
		}

		[Test()]
		public void test05()
		{
			sbyte[] input = { -1, -128, 0, 1, 127 };
			byte[] expectedResult = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I1 ) << 2) | 0x01), 0x05, 255, 128, 0, 1, 127};

			I1ArraySECSItem secsItem = new I1ArraySECSItem(input);
			Assert.IsTrue(secsItem.ToRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test06()
		{
			sbyte[] input = { -1, -128, 0, 1, 127 };
			byte[] expectedResult = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I1 ) << 2) | 0x02), 0, 0x05, 255, 128, 0, 1, 127};

			I1ArraySECSItem secsItem = new I1ArraySECSItem(input, 2);
			Assert.IsTrue(secsItem.ToRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test07()
		{
			sbyte[] input = { -1, -128, 0, 1, 127 };
			byte[] expectedResult = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I1 ) << 2) | 0x03), 0, 0, 0x05, 255, 128, 0, 1, 127};

			I1ArraySECSItem secsItem = new I1ArraySECSItem(input, 3);
			Assert.IsTrue(secsItem.ToRawSECSItem().SequenceEqual(expectedResult));
		}

	}
}

