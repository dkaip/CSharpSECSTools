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
	public class I2ArraySECSItemTests
	{
		[Test()]
		public void test01()
		{
			byte[] input = {0x00};

			var exception = Assert.Catch(() => new I2ArraySECSItem(input, 0));

			Assert.IsInstanceOf<ArgumentOutOfRangeException>(exception);
		}

		[Test()]
		public void test02()
		{
			byte[] input = {};

			var exception = Assert.Catch(() => new I2ArraySECSItem(input, 0));

			Assert.IsInstanceOf<ArgumentOutOfRangeException>(exception);
		}

		[Test()]
		public void test03()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I2 ) << 2) | 0x01), 10,
				255, 255,
				128, 0,
				0, 0,
				0, 1,
				127, 255 };
			I2ArraySECSItem secsItem = new I2ArraySECSItem(input, 0);
			Assert.IsTrue(secsItem.getValue()[0] == -1);
			Assert.IsTrue(secsItem.getValue()[1] == -32768);
			Assert.IsTrue(secsItem.getValue()[2] == 0);
			Assert.IsTrue(secsItem.getValue()[3] == 1);
			Assert.IsTrue(secsItem.getValue()[4] == 32767);
		}

		[Test()]
		public void test04()
		{
			Int16[] input = new Int16[5];
			input[0] = -1;
			input[1] = -32768;
			input[2] = 0;
			input[3] = 1;
			input[4] = 32767;
			I2ArraySECSItem secsItem = new I2ArraySECSItem(input);
			Assert.IsTrue(secsItem.getValue().SequenceEqual(input));
		}

		[Test()]
		public void test05()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I2 ) << 2) | 0x01), 10,
				255, 255,
				128, 0,
				0, 0,
				0, 1,
				127, 255 };
			I2ArraySECSItem secsItem = new I2ArraySECSItem(input, 0);
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.I2);
		}

		[Test()]
		public void test06()
		{
			Int16[] input = new Int16[5];
			input[0] = -1;
			input[1] = -32768;
			input[2] = 0;
			input[3] = 1;
			input[4] = 32767;
			I2ArraySECSItem secsItem = new I2ArraySECSItem(input);
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.I2);
		}

		[Test()]
		public void test07()
		{
			Int16[] input = new Int16[5];
			input[0] = -1;
			input[1] = -32768;
			input[2] = 0;
			input[3] = 1;
			input[4] = 32767;
			byte[] expectedResult = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I2 ) << 2) | 0x01), 10,
				255, 255,
				128, 0,
				0, 0,
				0, 1,
				127, 255 };

			I2ArraySECSItem secsItem = new I2ArraySECSItem(input);
			Assert.IsTrue(secsItem.ToRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test08()
		{
			Int16[] input = new Int16[5];
			input[0] = -1;
			input[1] = -32768;
			input[2] = 0;
			input[3] = 1;
			input[4] = 32767;
			byte[] expectedResult = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I2 ) << 2) | 0x02), 0, 10,
				255, 255,
				128, 0,
				0, 0,
				0, 1,
				127, 255 };

			I2ArraySECSItem secsItem = new I2ArraySECSItem(input, 2);
			Assert.IsTrue(secsItem.ToRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test09()
		{
			Int16[] input = new Int16[5];
			input[0] = -1;
			input[1] = -32768;
			input[2] = 0;
			input[3] = 1;
			input[4] = 32767;
			byte[] expectedResult = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I2 ) << 2) | 0x03), 0, 0, 10,
				255, 255,
				128, 0,
				0, 0,
				0, 1,
				127, 255 };

			I2ArraySECSItem secsItem = new I2ArraySECSItem(input, 3);
			Assert.IsTrue(secsItem.ToRawSECSItem().SequenceEqual(expectedResult));
		}

	}
}

