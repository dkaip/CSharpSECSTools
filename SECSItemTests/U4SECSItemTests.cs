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
	public class U4SECSItemTests
	{
        [Test()]
        public void Test00()
        {
            byte [] value = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4) << 2) | 0x01), 0x04, 255, 255, 255, 255 };

            SECSItem secsItem = SECSItemFactory.GenerateSECSItem(value);

            Assert.IsTrue(secsItem.GetType() == typeof(U4SECSItem));
        }

        [Test()]
        public void Test01()
        {
            byte [] value = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4) << 2) | 0x01), 0x04, 255, 255, 255, 255 };
            U4SECSItem secsItem = (U4SECSItem)SECSItemFactory.GenerateSECSItem(value);
            Assert.IsTrue(secsItem.Value == 0xFFFFFFFF);
        }

        [Test()]
        public void Test02()
        {
            byte [] value = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4) << 2) | 0x01), 0x04, 128, 0, 0, 0 };
            U4SECSItem secsItem = (U4SECSItem)SECSItemFactory.GenerateSECSItem(value);
            Assert.IsTrue(secsItem.Value == 2147483648L);
        }

        [Test()]
        public void Test03()
        {
            byte [] value = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4) << 2) | 0x01), 0x04, 0, 0, 0, 0 };
            U4SECSItem secsItem = (U4SECSItem)SECSItemFactory.GenerateSECSItem(value);
            Assert.IsTrue(secsItem.Value == 0);
        }

        [Test()]
        public void Test04()
        {
            byte [] value = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4) << 2) | 0x01), 0x04, 127, 255, 255, 255 };
            U4SECSItem secsItem = (U4SECSItem)SECSItemFactory.GenerateSECSItem(value);
            Assert.IsTrue(secsItem.Value == 2147483647L);
        }

        [Test()]
        public void Test05()
        {
            UInt32 expectedOutput = 0xFFFFFFFF;
            U4SECSItem secsItem = new U4SECSItem(expectedOutput);
            Assert.IsTrue(secsItem.Value == expectedOutput);
        }

        [Test()]
        public void Test06()
        {
            byte [] value = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4) << 2) | 0x01), 0x04, 255, 255, 255, 255 };
            U4SECSItem secsItem = (U4SECSItem)SECSItemFactory.GenerateSECSItem(value);
            Assert.IsTrue(secsItem.ItemFormatCode == SECSItemFormatCode.U4);
        }

        [Test()]
        public void Test07()
        {
            UInt32 expectedOutput = 0xFFFFFFFF;
            U4SECSItem secsItem = new U4SECSItem(expectedOutput);
            Assert.IsTrue(secsItem.ItemFormatCode == SECSItemFormatCode.U4);
        }

        [Test()]
        public void Test08()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4) << 2) | 0x01), 0x04, 255, 255, 255, 255 };

            U4SECSItem secsItem = new U4SECSItem(0xFFFFFFFF);
            Assert.AreEqual(secsItem.EncodeForTransport(), expectedResult);
        }

        [Test()]
        public void Test09()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4) << 2) | 0x02), 0, 0x04, 255, 255, 255, 255 };

            U4SECSItem secsItem = new U4SECSItem(0xFFFFFFFF, SECSItemNumLengthBytes.TWO);
            Assert.AreEqual(secsItem.EncodeForTransport(), expectedResult);
        }

        [Test()]
        public void Test10()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4) << 2) | 0x03), 0, 0, 0x04, 255, 255, 255, 255 };

            U4SECSItem secsItem = new U4SECSItem(0xFFFFFFFF, SECSItemNumLengthBytes.THREE);
            Assert.AreEqual(secsItem.EncodeForTransport(), expectedResult);
        }

        [Test()]
        public void Test13()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4) << 2) | 0x01), 0x05 };

            var exception = Assert.Catch(() => SECSItemFactory.GenerateSECSItem(input));

            Assert.IsInstanceOf<ArgumentOutOfRangeException>(exception);

            Assert.IsTrue(exception.Message.Contains("Illegal data length of: 5 payload length must be a multiple of 4."));
        }

        [Test()]
        public void Test14()
        {
            U4SECSItem secsItem = new U4SECSItem(4294967295);
            Assert.IsTrue(secsItem.ToString().Equals ("Format:U4 Value: 4294967295"));
        }

        [Test()]
        public void Test15()
        {
            U4SECSItem secsItem1 = new U4SECSItem(4294967295);
            U4SECSItem secsItem2 = new U4SECSItem(4294967295);
            Assert.IsTrue(secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test16()
        {
            U4SECSItem secsItem1 = new U4SECSItem(4294967295);
            U4SECSItem secsItem2 = new U4SECSItem(0);
            Assert.IsFalse(secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test17()
        {
            U4SECSItem secsItem1 = new U4SECSItem(4294967295);
            U4SECSItem secsItem2 = null;
            Assert.IsFalse(secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test18()
        {
            U4SECSItem secsItem1 = new U4SECSItem(4294967295);
            Assert.IsTrue(secsItem1.Equals (secsItem1));
        }

        [Test()]
        public void Test19()
        {
            U4SECSItem secsItem1 = new U4SECSItem(4294967295);
            SECSItem secsItem2 = null;
            Assert.IsFalse(secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test20()
        {
            U4SECSItem secsItem1 = new U4SECSItem(4294967295);
            Object secsItem2 = new F8SECSItem(2.141592D);
            Assert.IsFalse(secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test21()
        {
            Assert.IsTrue(true);
            /*
            U4SECSItem secsItem1 = new U4SECSItem(4294967295);

            Assert.IsTrue(secsItem1.GetHashCode() == 30);
            */
        }

        [Test()]
        public void Test22()
        {
            U4SECSItem secsItem1 = new U4SECSItem(4294967295);
            U4SECSItem secsItem2 = new U4SECSItem(4294967295);
            Assert.IsTrue(secsItem1.GetHashCode() == secsItem2.GetHashCode());
        }
	}
}

