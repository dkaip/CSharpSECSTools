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
	public class I4SECSItemTests
	{
		[Test()]
		public void test01()
		{
			byte[] input = {0x00};

			var exception = Assert.Catch(() => new I4SECSItem(input, 0));

			Assert.IsInstanceOf<ArgumentOutOfRangeException>(exception);
		}

		[Test()]
		public void test02()
		{
			byte[] input = {};

			var exception = Assert.Catch(() => new I4SECSItem(input, 0));

			Assert.IsInstanceOf<ArgumentOutOfRangeException>(exception);
		}

		[Test()]
		public void test03()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x01), 0x04, 255, 255, 255, 255 };
			Int32 expectedOutput = -1;
			I4SECSItem secsItem = new I4SECSItem(input, 0);
			Assert.IsTrue(secsItem.getValue() == expectedOutput);
		}

		[Test()]
		public void test04()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x01), 0x04, 128, 0, 0, 0 };
			Int32 expectedOutput = -2147483648;
			I4SECSItem secsItem = new I4SECSItem(input, 0);
			Assert.IsTrue(secsItem.getValue() == expectedOutput);
		}

		[Test()]
		public void test05()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x01), 0x04, 0, 0, 0, 0 };
			Int32 expectedOutput = 0;
			I4SECSItem secsItem = new I4SECSItem(input, 0);
			Assert.IsTrue(secsItem.getValue() == expectedOutput);
		}

		[Test()]
		public void test06()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x01), 0x04, 0, 0, 0, 1 };
			Int32 expectedOutput = 1;
			I4SECSItem secsItem = new I4SECSItem(input, 0);
			Assert.IsTrue(secsItem.getValue() == expectedOutput);
		}

		[Test()]
		public void test07()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x01), 0x04, 127, 255, 255, 255 };
			Int32 expectedOutput = 2147483647;
			I4SECSItem secsItem = new I4SECSItem(input, 0);
			Assert.IsTrue(secsItem.getValue() == expectedOutput);
		}

		[Test()]
		public void test08()
		{
			Int32 expectedOutput = 2147483647;
			I4SECSItem secsItem = new I4SECSItem(expectedOutput);
			Assert.IsTrue(secsItem.getValue() == expectedOutput);
		}

		[Test()]
		public void test09()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x01), 0x04, 255, 255, 255, 255 };
			I4SECSItem secsItem = new I4SECSItem(input, 0);
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.I4);
		}

		[Test()]
		public void test10()
		{
			Int32 expectedOutput = 2147483647;
			I4SECSItem secsItem = new I4SECSItem(expectedOutput);
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.I4);
		}

		[Test()]
		public void test11()
		{
			byte[] expectedResult = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x01), 0x04, 127, 255, 255, 255 };

			I4SECSItem secsItem = new I4SECSItem(2147483647);
			Assert.IsTrue(secsItem.ToRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test12()
		{
			byte[] expectedResult = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x02), 0, 0x04, 127, 255, 255, 255 };

			I4SECSItem secsItem = new I4SECSItem(2147483647, 2);
			Assert.IsTrue(secsItem.ToRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test13()
		{
			byte[] expectedResult = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x03), 0, 0, 0x04, 127, 255, 255, 255 };

			I4SECSItem secsItem = new I4SECSItem(2147483647, 3);
			Assert.IsTrue(secsItem.ToRawSECSItem().SequenceEqual(expectedResult));
		}

	}
}

