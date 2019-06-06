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
	public class U4ArraySECSItemTests
	{
		[Test()]
		public void test01()
		{
			byte[] input = {0x00};

			var exception = Assert.Catch(() => new U4ArraySECSItem(input, 0));

			Assert.IsInstanceOf<ArgumentOutOfRangeException>(exception);
		}

		[Test()]
		public void test02()
		{
			byte[] input = {};

			var exception = Assert.Catch(() => new U4ArraySECSItem(input, 0));

			Assert.IsInstanceOf<ArgumentOutOfRangeException>(exception);
		}

		[Test()]
		public void test03()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U4 ) << 2) | 0x01), 20,
				255, 255, 255, 255,
				128, 0, 0, 0,
				0, 0, 0, 0,
				0, 0, 0, 1,
				127, 255, 255, 255 };
			U4ArraySECSItem secsItem = new U4ArraySECSItem(input, 0);
			Assert.IsTrue(secsItem.getValue()[0] == 0xFFFFFFFF);
			Assert.IsTrue(secsItem.getValue()[1] == 0x80000000);
			Assert.IsTrue(secsItem.getValue()[2] == 0x00000000);
			Assert.IsTrue(secsItem.getValue()[3] == 0x00000001);
			Assert.IsTrue(secsItem.getValue()[4] == 0x7FFFFFFF);
		}

		[Test()]
		public void test04()
		{
			UInt32[] input = new UInt32[5];
			input[0] = 0xFFFFFFFF;
			input[1] = 0x80000000;
			input[2] = 0x00000000;
			input[3] = 0x00000001;
			input[4] = 0x7FFFFFFF;
			U4ArraySECSItem secsItem = new U4ArraySECSItem(input);
			Assert.IsTrue(secsItem.getValue().SequenceEqual(input));
		}

		[Test()]
		public void test05()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U4 ) << 2) | 0x01), 20,
				255, 255, 255, 255,
				128, 0, 0, 0, 
				0, 0, 0, 0,
				0, 0, 0, 1,
				127, 255, 255, 255 };
			U4ArraySECSItem secsItem = new U4ArraySECSItem(input, 0);
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.U4);
		}

		[Test()]
		public void test06()
		{
			UInt32[] input = new UInt32[5];
			input[0] = 0xFFFFFFFF;
			input[1] = 0x80000000;
			input[2] = 0x00000000;
			input[3] = 0x00000001;
			input[4] = 0x7FFFFFFF;
			U4ArraySECSItem secsItem = new U4ArraySECSItem(input);
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.U4);
		}

		[Test()]
		public void test07()
		{
			UInt32[] input = new UInt32[5];
			input[0] = 0xFFFFFFFF;
			input[1] = 0x80000000;
			input[2] = 0x00000000;
			input[3] = 0x00000001;
			input[4] = 0x7FFFFFFF;
			byte[] expectedResult = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U4 ) << 2) | 0x01), 20,
				255, 255, 255, 255,
				128, 0, 0, 0,
				0, 0, 0, 0,
				0, 0, 0, 1,
				127, 255, 255, 255 };

			U4ArraySECSItem secsItem = new U4ArraySECSItem(input);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test08()
		{
			UInt32[] input = new UInt32[5];
			input[0] = 0xFFFFFFFF;
			input[1] = 0x80000000;
			input[2] = 0x00000000;
			input[3] = 0x00000001;
			input[4] = 0x7FFFFFFF;
			byte[] expectedResult = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U4 ) << 2) | 0x02), 0, 20,
				255, 255, 255, 255,
				128, 0, 0, 0,
				0, 0, 0, 0,
				0, 0, 0, 1,
				127, 255, 255, 255 };

			U4ArraySECSItem secsItem = new U4ArraySECSItem(input, 2);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test09()
		{
			UInt32[] input = new UInt32[5];
			input[0] = 0xFFFFFFFF;
			input[1] = 0x80000000;
			input[2] = 0x00000000;
			input[3] = 0x00000001;
			input[4] = 0x7FFFFFFF;
			byte[] expectedResult = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U4 ) << 2) | 0x03), 0, 0, 20,
				255, 255, 255, 255,
				128, 0, 0, 0,
				0, 0, 0, 0,
				0, 0, 0, 1,
				127, 255, 255, 255 };

			U4ArraySECSItem secsItem = new U4ArraySECSItem(input, 3);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

	}
}

