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
	public class U2ArraySECSItemTests
	{
		[Test()]
		public void test01()
		{
			byte[] input = {0x00};

			var exception = Assert.Catch(() => new U2ArraySECSItem(input, 0));

			Assert.IsInstanceOf<ArgumentOutOfRangeException>(exception);
		}

		[Test()]
		public void test02()
		{
			byte[] input = {};

			var exception = Assert.Catch(() => new U2ArraySECSItem(input, 0));

			Assert.IsInstanceOf<ArgumentOutOfRangeException>(exception);
		}

		[Test()]
		public void test03()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U2 ) << 2) | 0x01), 10,
				255, 255,
				128, 0,
				0, 0,
				0, 1,
				127, 255};
			U2ArraySECSItem secsItem = new U2ArraySECSItem(input, 0);
			Assert.IsTrue(secsItem.getValue()[0] == 0xFFFF);
			Assert.IsTrue(secsItem.getValue()[1] == 0x8000);
			Assert.IsTrue(secsItem.getValue()[2] == 0x0000);
			Assert.IsTrue(secsItem.getValue()[3] == 0x0001);
			Assert.IsTrue(secsItem.getValue()[4] == 0x7FFF);
		}

		[Test()]
		public void test04()
		{
			UInt16[] input = new UInt16[5];
			input[0] = 0xFFFF;
			input[1] = 0x8000;
			input[2] = 0x0000;
			input[3] = 0x0001;
			input[4] = 0x7FFF;
			U2ArraySECSItem secsItem = new U2ArraySECSItem(input);
			Assert.IsTrue(secsItem.getValue().SequenceEqual(input));
		}

		[Test()]
		public void test05()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U2 ) << 2) | 0x01), 10,
				255, 255,
				128, 0, 
				0, 0,
				0, 1,
				127, 255 };
			U2ArraySECSItem secsItem = new U2ArraySECSItem(input, 0);
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.U2);
		}

		[Test()]
		public void test06()
		{
			UInt16[] input = new UInt16[5];
			input[0] = 0xFFFF;
			input[1] = 0x8000;
			input[2] = 0x0000;
			input[3] = 0x0001;
			input[4] = 0x7FFF;
			U2ArraySECSItem secsItem = new U2ArraySECSItem(input);
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.U2);
		}

		[Test()]
		public void test07()
		{
			UInt16[] input = new UInt16[5];
			input[0] = 0xFFFF;
			input[1] = 0x8000;
			input[2] = 0x0000;
			input[3] = 0x0001;
			input[4] = 0x7FFF;
			byte[] expectedResult = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U2 ) << 2) | 0x01), 10,
				255, 255,
				128, 0,
				0, 0,
				0, 1,
				127, 255 };

			U2ArraySECSItem secsItem = new U2ArraySECSItem(input);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test08()
		{
			UInt16[] input = new UInt16[5];
			input[0] = 0xFFFF;
			input[1] = 0x8000;
			input[2] = 0x0000;
			input[3] = 0x0001;
			input[4] = 0x7FFF;
			byte[] expectedResult = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U2 ) << 2) | 0x02), 0, 10,
				255, 255,
				128, 0,
				0, 0,
				0, 1,
				127, 255 };

			U2ArraySECSItem secsItem = new U2ArraySECSItem(input, 2);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test09()
		{
			UInt16[] input = new UInt16[5];
			input[0] = 0xFFFF;
			input[1] = 0x8000;
			input[2] = 0x0000;
			input[3] = 0x0001;
			input[4] = 0x7FFF;
			byte[] expectedResult = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U2 ) << 2) | 0x03), 0, 0, 10,
				255, 255,
				128, 0,
				0, 0,
				0, 1,
				127, 255 };

			U2ArraySECSItem secsItem = new U2ArraySECSItem(input, 3);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

	}
}

