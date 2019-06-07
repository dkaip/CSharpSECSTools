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
	public class U4SECSItemTests
	{
		[Test()]
		public void test01()
		{
			byte[] input = {0x00};

			var exception = Assert.Catch(() => new U4SECSItem(input, 0));

			Assert.IsInstanceOf<ArgumentOutOfRangeException>(exception);
		}

		[Test()]
		public void test02()
		{
			byte[] input = {};

			var exception = Assert.Catch(() => new U4SECSItem(input, 0));

			Assert.IsInstanceOf<ArgumentOutOfRangeException>(exception);
		}

		[Test()]
		public void test03()
		{
			byte[] value = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U4 ) << 2) | 0x01), 0x04, 255, 255, 255, 255};
			U4SECSItem secsItem = new U4SECSItem(value, 0);
			Assert.IsTrue(secsItem.getValue() == UInt32.MaxValue);
		}

		[Test()]
		public void test04()
		{
			byte[] value = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U4 ) << 2) | 0x01), 0x04, 128, 0, 0, 0};
			U4SECSItem secsItem = new U4SECSItem(value, 0);
			Assert.IsTrue(secsItem.getValue() == 2147483648U);
		}

		[Test()]
		public void test05()
		{
			byte[] value = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U4 ) << 2) | 0x01), 0x04, 0, 0, 0, 0};
			U4SECSItem secsItem = new U4SECSItem(value, 0);
			Assert.IsTrue(secsItem.getValue() == 0U);
		}

		[Test()]
		public void test06()
		{
			byte[] value = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U4 ) << 2) | 0x01), 0x04, 127, 255, 255, 255};
			U4SECSItem secsItem = new U4SECSItem(value, 0);
			Assert.IsTrue(secsItem.getValue() == 2147483647U);
		}

		[Test()]
		public void test07()
		{
			UInt32 expectedOutput = 0xFFFFFFFFU;
			U4SECSItem secsItem = new U4SECSItem(expectedOutput);
			Assert.IsTrue(secsItem.getValue() == expectedOutput);
		}

		[Test()]
		public void test08()
		{
			byte[] value = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U4 ) << 2) | 0x01), 0x04, 255, 255, 255, 255};
			U4SECSItem secsItem = new U4SECSItem(value, 0);
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.U4);
		}

		[Test()]
		public void test09()
		{
			UInt32 expectedOutput = 0xFFFFFFFFU;
			U4SECSItem secsItem = new U4SECSItem(expectedOutput);
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.U4);
		}

		[Test()]
		public void test10()
		{
			byte[] expectedResult = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U4 ) << 2) | 0x01), 0x04, 255, 255, 255, 255};

			U4SECSItem secsItem = new U4SECSItem(0xFFFFFFFFU);
			Assert.IsTrue(secsItem.ToRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test11()
		{
			byte[] expectedResult = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U4 ) << 2) | 0x02), 0, 0x04, 255, 255, 255, 255};

			U4SECSItem secsItem = new U4SECSItem(0xFFFFFFFFU, 2);
			Assert.IsTrue(secsItem.ToRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test12()
		{
			byte[] expectedResult = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U4 ) << 2) | 0x03), 0, 0, 0x04, 255, 255, 255, 255};

			U4SECSItem secsItem = new U4SECSItem(0xFFFFFFFFU, 3);
			Assert.IsTrue(secsItem.ToRawSECSItem().SequenceEqual(expectedResult));
		}

	}
}

