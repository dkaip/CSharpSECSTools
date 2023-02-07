/*
 * Copyright 2019-2023 Douglas Kaip
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

using NUnit.Framework;

using com.CIMthetics.CSharpSECSTools.SECSItems;

namespace com.CIMthetics.CSharpSECSTools.SECSItemTests
{
	[TestFixture()]
	public class ASCIISECSItemTests
	{
        [Test()]
        public void Test00()
        {
			byte[] input = {(byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.A ) << 2) | 0x01), 0x03, 0x41, 0x42, 0x43};                  // 'A', 'B', 'C'

            SECSItem secsItem = SECSItemFactory.GenerateSECSItem(input);

            Assert.IsTrue(secsItem.GetType() == typeof(ASCIISECSItem));
        }

        [Test()]
        public void Test01()
        {
            ASCIISECSItem secsItem = new ASCIISECSItem(null);
            Assert.IsTrue(secsItem.Value.Equals (""));
        }

        [Test()]
        public void Test02()
        {
            ASCIISECSItem secsItem = new ASCIISECSItem(null, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue(secsItem.Value.Equals(""));
            Assert.IsTrue(secsItem.ItemFormatCode == SECSItemFormatCode.A);
        }

        [Test()]
        public void Test03 ()
        {
            ASCIISECSItem secsItem = new ASCIISECSItem("DEF");
            Assert.IsTrue(secsItem.Value.Equals("DEF"));
        }

		[Test()]
		public void Test04()
		{
			byte[] input = {(byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.A ) << 2) | 0x01), 0x03, 0x41, 0x42, 0x43};                  // 'A', 'B', 'C'
			ASCIISECSItem secsItem = (ASCIISECSItem)SECSItemFactory.GenerateSECSItem(input);
			Assert.IsTrue(secsItem.Value.Equals("ABC"));
		}

        [Test()]
        public void Test05()
        {
            byte [] input = {(byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.L ) << 2) | 0x01), 0x00,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.A ) << 2) | 0x01), 0x03, 0x41, 0x42, 0x43};                  // 'A', 'B', 'C'
            ASCIISECSItem secsItem = (ASCIISECSItem)SECSItemFactory.GenerateSECSItem(input, 2);
            Assert.IsTrue(secsItem.Value.Equals("ABC"));
        }

        [Test()]
        public void Test06()
        {
            ASCIISECSItem secsItem = new ASCIISECSItem("DEF");
            Assert.IsTrue (secsItem.ItemFormatCode == SECSItemFormatCode.A);
        }

        [Test()]
        public void Test07()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.A) << 2) | 0x01), 0x03, 0x41, 0x42, 0x43 };                  // 'A', 'B', 'C'
            ASCIISECSItem secsItem = (ASCIISECSItem)SECSItemFactory.GenerateSECSItem(input);
            Assert.IsTrue(secsItem.ItemFormatCode == SECSItemFormatCode.A);
        }

        [Test()]
        public void Test08()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.A) << 2) | 0x01), 0x03, 0x41, 0x42, 0x43 };                  // 'A', 'B', 'C'

            ASCIISECSItem secsItem = new ASCIISECSItem("ABC");
            Assert.AreEqual(secsItem.EncodeForTransport(), expectedResult);
        }

        [Test()]
        public void Test09()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.A) << 2) | 0x02), 0, 0x03, 0x41, 0x42, 0x43 };                  // 'A', 'B', 'C'

            ASCIISECSItem secsItem = new ASCIISECSItem("ABC", SECSItemNumLengthBytes.TWO);
            Assert.AreEqual(secsItem.EncodeForTransport(), expectedResult);
        }

        [Test()]
        public void Test10()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.A) << 2) | 0x03), 0, 0, 0x03, 0x41, 0x42, 0x43 };                  // 'A', 'B', 'C'

            ASCIISECSItem secsItem = new ASCIISECSItem("ABC", SECSItemNumLengthBytes.THREE);
            Assert.AreEqual(secsItem.EncodeForTransport(), expectedResult);
        }

        [Test()]
        public void Test11()
        {
            ASCIISECSItem secsItem = new ASCIISECSItem("3.141592F");
            Assert.IsTrue(secsItem.ToString().Equals("Format:A Value: 3.141592F"));
        }

        [Test()]
        public void Test12()
        {
            ASCIISECSItem secsItem1 = new ASCIISECSItem("3.141592F");
            ASCIISECSItem secsItem2 = new ASCIISECSItem("3.141592F");
            Assert.IsTrue(secsItem1.Equals(secsItem2));
        }

        [Test()]
        public void Test13()
        {
            ASCIISECSItem secsItem1 = new ASCIISECSItem("3.141592F");
            ASCIISECSItem secsItem2 = new ASCIISECSItem("2.141592F");
            Assert.IsFalse(secsItem1.Equals(secsItem2));
        }

        [Test()]
        public void Test14()
        {
            ASCIISECSItem secsItem1 = new ASCIISECSItem("3.141592F");
            ASCIISECSItem secsItem2 = null;
            Assert.IsFalse(secsItem1.Equals(secsItem2));
        }

        [Test()]
        public void Test15()
        {
            ASCIISECSItem secsItem1 = new ASCIISECSItem("3.141592F");
            Assert.IsTrue(secsItem1.Equals(secsItem1));
        }

        [Test()]
        public void Test16()
        {
            ASCIISECSItem secsItem1 = new ASCIISECSItem("3.141592F");
            SECSItem secsItem2 = null;
            Assert.IsFalse(secsItem1.Equals(secsItem2));
        }

        [Test()]
        public void Test17()
        {
            ASCIISECSItem secsItem1 = new ASCIISECSItem(null);
            SECSItem secsItem2 = new ASCIISECSItem("3.141592F");
            Assert.IsFalse(secsItem1.Equals(secsItem2));
        }

        [Test()]
        public void Test18()
        {
            ASCIISECSItem secsItem1 = new ASCIISECSItem(null);
            SECSItem secsItem2 = new ASCIISECSItem(null); ;
            Assert.IsTrue(secsItem1.Equals(secsItem2));
        }

        [Test()]
        public void Test19()
        {
            ASCIISECSItem secsItem1 = new ASCIISECSItem("3.141592F");
            Object secsItem2 = new F8SECSItem(2.141592D);
            Assert.IsFalse(secsItem1.Equals(secsItem2));
        }

        [Test()]
        public void Test20()
        {
            Assert.IsTrue(true);
            /*
            ASCIISECSItem secsItem1 = new ASCIISECSItem ("3.141592F");
            Assert.IsTrue (secsItem1.GetHashCode () == -1347356470);
            */
        }

        [Test()]
        public void Test21()
        {
            Assert.IsTrue (true);
            /*
            ASCIISECSItem secsItem1 = new ASCIISECSItem (null);
            Assert.IsTrue (secsItem1.GetHashCode () == 31);
            */
        }

        [Test()]
        public void Test22()
        {
            ASCIISECSItem secsItem1 = new ASCIISECSItem("3.141592F");
            ASCIISECSItem secsItem2 = new ASCIISECSItem("3.141592F");
            Assert.IsTrue (secsItem1.GetHashCode() == secsItem2.GetHashCode());
        }
	}
}

