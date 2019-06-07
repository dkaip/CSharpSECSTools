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
	public class ASCIISECSItemTests
	{
		[Test()]
		public void test01()
		{
			ASCIISECSItem secsItem = new ASCIISECSItem("DEF");
			Assert.IsTrue(secsItem.GetValue().Equals("DEF"));
		}

		[Test()]
		public void test02()
		{
			byte[] input = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.A ) << 2) | 0x01), 0x03, 0x41, 0x42, 0x43};                  // 'A', 'B', 'C'
			ASCIISECSItem secsItem = new ASCIISECSItem(input, 0);
			Assert.IsTrue(secsItem.GetValue().Equals("ABC"));
		}

		[Test()]
		public void test03()
		{
			ASCIISECSItem secsItem = new ASCIISECSItem("DEF");
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.A);
		}

		[Test()]
		public void test04()
		{
			byte[] input = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.A ) << 2) | 0x01), 0x03, 0x41, 0x42, 0x43};                  // 'A', 'B', 'C'
			ASCIISECSItem secsItem = new ASCIISECSItem(input, 0);
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.A);
		}

		[Test()]
		public void test05()
		{
			byte[] expectedResult = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.A ) << 2) | 0x01), 0x03, 0x41, 0x42, 0x43};                  // 'A', 'B', 'C'

			ASCIISECSItem secsItem = new ASCIISECSItem("ABC");
			Assert.IsTrue(secsItem.ToRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test06()
		{
			byte[] expectedResult = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.A ) << 2) | 0x02), 0, 0x03, 0x41, 0x42, 0x43};                  // 'A', 'B', 'C'

			ASCIISECSItem secsItem = new ASCIISECSItem("ABC", 2);
			Assert.IsTrue(secsItem.ToRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test07()
		{
			byte[] expectedResult = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.A ) << 2) | 0x03), 0, 0, 0x03, 0x41, 0x42, 0x43};                  // 'A', 'B', 'C'

			ASCIISECSItem secsItem = new ASCIISECSItem("ABC", 3);
			Assert.IsTrue(secsItem.ToRawSECSItem().SequenceEqual(expectedResult));
		}
	}
}

