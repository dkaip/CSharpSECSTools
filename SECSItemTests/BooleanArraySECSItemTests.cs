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
	public class BooleanArraySECSItemTests
	{
		[Test()]
		public void test01()
		{
			byte[] input = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x01), 0x08, 1, 0, 2, 0, 255, 0, 1, 1};
			bool[] expectedResult = {true, false, true, false, true, false, true, true};
			BooleanArraySECSItem secsItem = new BooleanArraySECSItem(input, 0);
			Assert.IsTrue(secsItem.getValue().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test02()
		{
			byte[] input = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x01), 0x08, 1, 0, 2, 0, 255, 0, 1, 1};
			BooleanArraySECSItem secsItem = new BooleanArraySECSItem(input, 0);
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.BO);
		}

		[Test()]
		public void test03()
		{
			bool[] input = {true, false, true, false, true, false, true, true};
			byte[] expectedResult = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x01), 8, 1, 0, 1, 0, 1, 0, 1, 1};

			BooleanArraySECSItem secsItem = new BooleanArraySECSItem(input);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test04()
		{
			bool[] input = {true, false, true, false, true, false, true, true};
			byte[] expectedResult = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x02), 0, 8, 1, 0, 1, 0, 1, 0, 1, 1};

			BooleanArraySECSItem secsItem = new BooleanArraySECSItem(input, 2);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test05()
		{
			bool[] input = {true, false, true, false, true, false, true, true};
			byte[] expectedResult = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x03), 0, 0, 8, 1, 0, 1, 0, 1, 0, 1, 1};

			BooleanArraySECSItem secsItem = new BooleanArraySECSItem(input, 3);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

	}
}

