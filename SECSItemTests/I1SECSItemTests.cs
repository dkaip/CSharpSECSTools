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
	public class I1SECSItemTests
	{
		[Test()]
		public void test01()
		{
			sbyte value = -1;
			I1SECSItem secsItem = new I1SECSItem(value);
			Assert.IsTrue(secsItem.getValue() == value);
		}

		[Test()]
		public void test02()
		{
			sbyte value = -128;
			I1SECSItem secsItem = new I1SECSItem(value);
			Assert.IsTrue(secsItem.getValue() == value);
		}

		[Test()]
		public void test03()
		{
			sbyte value = 0;
			I1SECSItem secsItem = new I1SECSItem(value);
			Assert.IsTrue(secsItem.getValue() == value);
		}

		[Test()]
		public void test04()
		{
			sbyte value = 127;
			I1SECSItem secsItem = new I1SECSItem(value);
			Assert.IsTrue(secsItem.getValue() == value);
		}

		[Test()]
		public void test05()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I1 ) << 2) | 0x01), 0x01, 255 };
			sbyte expectedOutput = -1;
			I1SECSItem secsItem = new I1SECSItem(input, 0);
			Assert.IsTrue(secsItem.getValue() == expectedOutput);
		}

		[Test()]
		public void test06()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I1 ) << 2) | 0x01), 0x01, 128 };
			sbyte expectedOutput = -128;
			I1SECSItem secsItem = new I1SECSItem(input, 0);
			Assert.IsTrue(secsItem.getValue() == expectedOutput);
		}

		[Test()]
		public void test07()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I1 ) << 2) | 0x01), 0x01, 0 };
			sbyte expectedOutput = 0;
			I1SECSItem secsItem = new I1SECSItem(input, 0);
			Assert.IsTrue(secsItem.getValue() == expectedOutput);
		}

		[Test()]
		public void test08()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I1 ) << 2) | 0x01), 0x01, 127 };
			sbyte expectedOutput = 127;
			I1SECSItem secsItem = new I1SECSItem(input, 0);
			Assert.IsTrue(secsItem.getValue() == expectedOutput);
		}

		[Test()]
		public void test09()
		{
			sbyte value = -1;
			I1SECSItem secsItem = new I1SECSItem(value);
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.I1);
		}

		[Test()]
		public void test10()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I1 ) << 2) | 0x01), 0x01, 127 };
			I1SECSItem secsItem = new I1SECSItem(input, 0);
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.I1);
		}

		[Test()]
		public void test11()
		{
			byte[] expectedResult = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I1 ) << 2) | 0x01), 0x01, 127 };

			I1SECSItem secsItem = new I1SECSItem((sbyte)127);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test12()
		{
			byte[] expectedResult = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I1 ) << 2) | 0x02), 0, 0x01, 127 };

			I1SECSItem secsItem = new I1SECSItem((sbyte)127, 2);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test13()
		{
			byte[] expectedResult = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I1 ) << 2) | 0x03), 0, 0, 0x01, 127 };

			I1SECSItem secsItem = new I1SECSItem((sbyte)127, 3);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}
	}
}

