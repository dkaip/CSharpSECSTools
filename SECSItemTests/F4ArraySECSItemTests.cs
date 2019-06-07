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
	public class F4ArraySECSItemTests
	{
		[Test()]
		public void test01()
		{
			byte[] input = {0x00};

			var exception = Assert.Catch(() => new F4ArraySECSItem(input, 0));

			Assert.IsInstanceOf<ArgumentOutOfRangeException>(exception);
		}

		[Test()]
		public void test02()
		{
			byte[] input = {};

			var exception = Assert.Catch(() => new F4ArraySECSItem(input, 0));

			Assert.IsInstanceOf<ArgumentOutOfRangeException>(exception);
		}

		[Test()]
		public void test03()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.F4 ) << 2) | 0x01), 20, 
				127, 127, 255, 255,
				255, 127, 255, 255,
				255, 128, 0, 0,
				127, 128, 0, 0,
				0, 0, 0, 0 };
			F4ArraySECSItem secsItem = new F4ArraySECSItem(input, 0);
			Assert.IsTrue(secsItem.getValue()[0] == Single.MaxValue);
			Assert.IsTrue(secsItem.getValue()[1] == Single.MinValue);
			Assert.IsTrue(secsItem.getValue()[2] == Single.NegativeInfinity);
			Assert.IsTrue(secsItem.getValue()[3] == Single.PositiveInfinity);
			Assert.IsTrue(secsItem.getValue()[4] == 0.0F);
		}

		[Test()]
		public void test04()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.F4 ) << 2) | 0x01), 20, 
				127, 127, 255, 255,
				255, 127, 255, 255,
				255, 128, 0, 0,
				127, 128, 0, 0,
				0, 0, 0, 0 };
			F4ArraySECSItem secsItem = new F4ArraySECSItem(input, 0);
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.F4);
		}

		[Test()]
		public void test05()
		{
			float[] input = {Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F};
			byte[] expectedResult = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.F4 ) << 2) | 0x01), 20, 
				127, 127, 255, 255,
				255, 127, 255, 255,
				255, 128, 0, 0,
				127, 128, 0, 0,
				0, 0, 0, 0 };

			F4ArraySECSItem secsItem = new F4ArraySECSItem(input);
			Assert.IsTrue(secsItem.ToRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test06()
		{
			float[] input = {Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F};
			byte[] expectedResult = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.F4 ) << 2) | 0x02), 0, 20, 
				127, 127, 255, 255,
				255, 127, 255, 255,
				255, 128, 0, 0,
				127, 128, 0, 0,
				0, 0, 0, 0 };

			F4ArraySECSItem secsItem = new F4ArraySECSItem(input, 2);
			Assert.IsTrue(secsItem.ToRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test07()
		{
			float[] input = {Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F};
			byte[] expectedResult = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.F4 ) << 2) | 0x03), 0, 0, 20, 
				127, 127, 255, 255,
				255, 127, 255, 255,
				255, 128, 0, 0,
				127, 128, 0, 0,
				0, 0, 0, 0 };

			F4ArraySECSItem secsItem = new F4ArraySECSItem(input, 3);
			Assert.IsTrue(secsItem.ToRawSECSItem().SequenceEqual(expectedResult));
		}
	}
}

