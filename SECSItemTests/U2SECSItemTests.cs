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
	public class U2SECSItemTests
	{
        [Test()]
        public void Test00()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2) << 2) | 0x01), 0x02, 255, 255 };

            SECSItem secsItem = SECSItemFactory.GenerateSECSItem(input);

            Assert.IsTrue(secsItem.GetType() == typeof(U2SECSItem));
        }

        [Test()]
        public void Test01()
        {
            byte [] value = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2) << 2) | 0x01), 0x02, 255, 255 };
            U2SECSItem secsItem = (U2SECSItem)SECSItemFactory.GenerateSECSItem(value);
            Assert.IsTrue(secsItem.Value == 65535);
        }

        [Test()]
        public void Test02()
        {
            byte [] value = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2) << 2) | 0x01), 0x02, 128, 0 };
            U2SECSItem secsItem = (U2SECSItem)SECSItemFactory.GenerateSECSItem(value);
            Assert.IsTrue(secsItem.Value == 32768);
        }

        [Test()]
        public void Test03()
        {
            byte [] value = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2) << 2) | 0x01), 0x02, 0, 0 };
            U2SECSItem secsItem = (U2SECSItem)SECSItemFactory.GenerateSECSItem(value);
            Assert.IsTrue(secsItem.Value == 0);
        }

        [Test()]
        public void Test04()
        {
            byte [] value = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2) << 2) | 0x01), 0x02, 127, 255 };
            U2SECSItem secsItem = (U2SECSItem)SECSItemFactory.GenerateSECSItem(value);
            Assert.IsTrue(secsItem.Value == 32767);
        }

        [Test()]
        public void Test05()
        {
            UInt16 expectedOutput = 65535;
            U2SECSItem secsItem = new U2SECSItem(expectedOutput);
            Assert.IsTrue(secsItem.Value == expectedOutput);
        }

        [Test()]
        public void Test06()
        {
            byte [] value = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2) << 2) | 0x01), 0x02, 255, 255 };
            U2SECSItem secsItem = (U2SECSItem)SECSItemFactory.GenerateSECSItem(value);
            Assert.IsTrue(secsItem.ItemFormatCode == SECSItemFormatCode.U2);
        }

        [Test()]
        public void Test07()
        {
            UInt16 expectedOutput = 65535;
            U2SECSItem secsItem = new U2SECSItem(expectedOutput);
            Assert.IsTrue(secsItem.ItemFormatCode == SECSItemFormatCode.U2);
        }

        [Test()]
        public void Test08()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2) << 2) | 0x01), 0x02, 255, 255 };

            U2SECSItem secsItem = new U2SECSItem((int)65535);
            Assert.AreEqual(secsItem.EncodeForTransport(), expectedResult);
        }

        [Test()]
        public void Test09()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2) << 2) | 0x02), 0, 0x02, 255, 255 };

            U2SECSItem secsItem = new U2SECSItem((int)65535, SECSItemNumLengthBytes.TWO);
            Assert.AreEqual(secsItem.EncodeForTransport(), expectedResult);
        }

        [Test()]
        public void Test10()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2) << 2) | 0x03), 0, 0, 0x02, 255, 255 };

            U2SECSItem secsItem = new U2SECSItem((int)65535, SECSItemNumLengthBytes.THREE);
            Assert.AreEqual(secsItem.EncodeForTransport(), expectedResult);
        }

        [Test()]
        public void Test12()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2) << 2) | 0x01), 0x03 };

            var exception = Assert.Catch(() => SECSItemFactory.GenerateSECSItem(input));

            Assert.IsInstanceOf<ArgumentOutOfRangeException>(exception);

            Assert.IsTrue(exception.Message.Contains("Illegal data length of: 3 payload length must be a multiple of 2."));
        }

        [Test()]
        public void Test13()
        {
            U2SECSItem secsItem = new U2SECSItem(65535);
            Assert.IsTrue(secsItem.ToString().Equals("Format:U2 Value: 65535"));
        }

        [Test()]
        public void Test14()
        {
            U2SECSItem secsItem1 = new U2SECSItem(65535);
            U2SECSItem secsItem2 = new U2SECSItem(65535);
            Assert.IsTrue(secsItem1.Equals(secsItem2));
        }

        [Test()]
        public void Test15()
        {
            U2SECSItem secsItem1 = new U2SECSItem(65535);
            U2SECSItem secsItem2 = new U2SECSItem(0);
            Assert.IsFalse(secsItem1.Equals(secsItem2));
        }

        [Test()]
        public void Test16()
        {
            U2SECSItem secsItem1 = new U2SECSItem(65535);
            U2SECSItem secsItem2 = null;
            Assert.IsFalse(secsItem1.Equals(secsItem2));
        }

        [Test()]
        public void Test17()
        {
            U2SECSItem secsItem1 = new U2SECSItem(65535);
            Assert.IsTrue(secsItem1.Equals(secsItem1));
        }

        [Test()]
        public void Test19()
        {
            U2SECSItem secsItem1 = new U2SECSItem(65535);
            Object secsItem2 = new F8SECSItem(2.141592D);
            Assert.IsFalse(secsItem1.Equals(secsItem2));
        }

        [Test()]
        public void Test20()
        {
            Assert.IsTrue(true);
            /*
            U2SECSItem secsItem1 = new U2SECSItem(65535);
            Assert.IsTrue(secsItem1.GetHashCode() == 65566);
            */
        }

        [Test()]
        public void Test21()
        {
            U2SECSItem secsItem1 = new U2SECSItem(65535);
            U2SECSItem secsItem2 = new U2SECSItem(65535);
            Assert.IsTrue(secsItem1.GetHashCode() == secsItem2.GetHashCode());
        }
	}
}