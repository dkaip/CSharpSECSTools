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
	public class BooleanSECSItemTests
	{
		[Test()]
		public void test01()
		{
			BooleanSECSItem secsItem = new BooleanSECSItem(true);
			Assert.IsTrue(secsItem.getValue() == true);
		}

		[Test()]
		public void test02()
		{
			BooleanSECSItem secsItem = new BooleanSECSItem(false);
			Assert.IsTrue(secsItem.getValue() == false);
		}

		[Test()]
		public void test03()
		{
			BooleanSECSItem secsItem = new BooleanSECSItem(true);
			Assert.IsFalse(secsItem.getValue() == false);
		}

		[Test()]
		public void test04()
		{
			byte[] input = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x01), 0x01, 5};
			BooleanSECSItem secsItem = new BooleanSECSItem(input, 0);
			Assert.IsTrue(secsItem.getValue() == true);
		}

		[Test()]
		public void test05()
		{
			byte[] input = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x01), 0x01, 0};
			BooleanSECSItem secsItem = new BooleanSECSItem(input, 0);
			Assert.IsTrue(secsItem.getValue() == false);
		}

		[Test()]
		public void test06()
		{
			BooleanSECSItem secsItem = new BooleanSECSItem(true);
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.BO);
		}

		[Test()]
		public void test07()
		{
			byte[] input = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x01), 0x01, 5};
			BooleanSECSItem secsItem = new BooleanSECSItem(input, 0);
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.BO);
		}

		[Test()]
		public void test08()
		{
			byte[] expectedResult = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x01), 0x01, 1};

			BooleanSECSItem secsItem = new BooleanSECSItem(true);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test09()
		{
			byte[] expectedResult = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x02), 0, 1, 1};

			BooleanSECSItem secsItem = new BooleanSECSItem(true, 2);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test10()
		{
			byte[] expectedResult = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x03), 0, 0, 1, 0};

			BooleanSECSItem secsItem = new BooleanSECSItem(false, 3);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}
	}
}

