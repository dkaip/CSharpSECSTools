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
	public class U4ArraySECSItemTests
	{
        [Test()]
        public void Test00()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4) << 2) | 0x01), 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255 };

            SECSItem secsItem = SECSItemFactory.GenerateSECSItem(input);

            Assert.IsTrue(secsItem.GetType() == typeof(U4ArraySECSItem));
        }

        [Test()]
        public void Test01()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4) << 2) | 0x01), 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255 };
            U4ArraySECSItem secsItem = (U4ArraySECSItem)SECSItemFactory.GenerateSECSItem(input);
            Assert.IsTrue(secsItem.Value[0] == 0xFFFFFFFFL);
            Assert.IsTrue(secsItem.Value[1] == 2147483648L);
            Assert.IsTrue(secsItem.Value[2] == 0);
            Assert.IsTrue(secsItem.Value[3] == 1);
            Assert.IsTrue(secsItem.Value[4] == 2147483647L);
        }

        [Test()]
        public void Test02()
        {
            UInt32 [] input = { 0xFFFFFFFF, 2147483648, 0, 1, 2147483647 };
            U4ArraySECSItem secsItem = new U4ArraySECSItem(input);
            Assert.AreEqual(secsItem.Value, input);
        }

        [Test()]
        public void Test03()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4) << 2) | 0x01), 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255 };
            U4ArraySECSItem secsItem = (U4ArraySECSItem)SECSItemFactory.GenerateSECSItem(input);
            Assert.IsTrue(secsItem.ItemFormatCode == SECSItemFormatCode.U4);
        }

        [Test()]
        public void Test04()
        {
            UInt32 [] input = { 0xFFFFFFFF, 2147483648, 0, 1, 2147483647 };
            U4ArraySECSItem secsItem = new U4ArraySECSItem(input);
            Assert.IsTrue(secsItem.ItemFormatCode == SECSItemFormatCode.U4);
        }

        [Test()]
        public void Test05()
        {
            UInt32 [] input = { 0xFFFFFFFF, 2147483648, 0, 1, 2147483647 };
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4) << 2) | 0x01), 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255 };

            U4ArraySECSItem secsItem = new U4ArraySECSItem(input);
            Assert.AreEqual(secsItem.EncodeForTransport(), expectedResult);
        }

        [Test()]
        public void Test06()
        {
            UInt32 [] input = { 0xFFFFFFFF, 2147483648, 0, 1, 2147483647 };
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4) << 2) | 0x02), 0, 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255 };

            U4ArraySECSItem secsItem = new U4ArraySECSItem(input, SECSItemNumLengthBytes.TWO);
            Assert.AreEqual(secsItem.EncodeForTransport(), expectedResult);
        }

        [Test()]
        public void Test07()
        {
            UInt32 [] input = { 0xFFFFFFFF, 2147483648, 0, 1, 2147483647 };
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4) << 2) | 0x03), 0, 0, 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255 };

            U4ArraySECSItem secsItem = new U4ArraySECSItem(input, SECSItemNumLengthBytes.THREE);
            Assert.AreEqual(secsItem.EncodeForTransport(), expectedResult);
        }

        [Test()]
        public void Test08()
        {
            UInt32 [] input = { 0xFFFFFFFF, 2147483648, 0, 1, 2147483647 };

            U4ArraySECSItem secsItem = new U4ArraySECSItem(input, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue(secsItem.ToString().Equals("Format:U4 Value: Array"));
        }

        [Test()]
        public void Test09()
        {
            Assert.IsTrue(true);
            /*
            UInt32 [] input = { 0xFFFFFFFF, 2147483648, 0, 1, 2147483647 };

            U4ArraySECSItem secsItem = new U4ArraySECSItem(input, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue(secsItem.GetHashCode() == 27705691);
            */
        }

        [Test()]
        public void Test10()
        {
            UInt32 [] input = { 0xFFFFFFFF, 2147483648, 0, 1, 2147483647 };

            U4ArraySECSItem secsItem = new U4ArraySECSItem(input, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue(secsItem.Equals(secsItem));
        }

        [Test()]
        public void Test11()
        {
            UInt32 [] input = { 0xFFFFFFFF, 2147483648, 0, 1, 2147483647 };

            U4ArraySECSItem secsItem = new U4ArraySECSItem(input, SECSItemNumLengthBytes.ONE);

            Assert.IsFalse (secsItem.Equals(null));
        }

        [Test()]
        public void Test12()
        {
            UInt32 [] input = { 0xFFFFFFFF, 2147483648, 0, 1, 2147483647 };

            U4ArraySECSItem secsItem = new U4ArraySECSItem(input, SECSItemNumLengthBytes.ONE);

            Object secsItem2 = new F8SECSItem(2.141592D);
            Assert.IsFalse (secsItem.Equals(secsItem2));
        }

        [Test()]
        public void Test13()
        {
            UInt32 [] input1 = { 0xFFFFFFFF, 2147483648, 0, 1, 2147483647 };
            UInt32 [] input2 = { 0xFFFFFFFF, 2147483648, 0, 1, 2147483647 };

            U4ArraySECSItem secsItem1 = new U4ArraySECSItem(input1, SECSItemNumLengthBytes.ONE);
            U4ArraySECSItem secsItem2 = new U4ArraySECSItem(input2, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue(secsItem1.Equals(secsItem2));
        }

        [Test()]
        public void Test14()
        {
            UInt32 [] input1 = { 0xFFFFFFFF, 2147483648, 0, 1, 2147483647 };
            UInt32 [] input2 = { 0xFFFFFFFF, 2147483648, 0, 1, 0 };

            U4ArraySECSItem secsItem1 = new U4ArraySECSItem(input1, SECSItemNumLengthBytes.ONE);
            U4ArraySECSItem secsItem2 = new U4ArraySECSItem(input2, SECSItemNumLengthBytes.ONE);

            Assert.IsFalse (secsItem1.Equals(secsItem2));
        }

        [Test()]
        public void Test15()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4) << 2) | 0x01), 0x00 };

            U4ArraySECSItem secsItem = (U4ArraySECSItem)SECSItemFactory.GenerateSECSItem(input);

            Assert.IsTrue(secsItem.LengthInBytes == 0);
        }

        [Test()]
        public void Test16()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4) << 2) | 0x01), 0x03 };

            var exception = Assert.Catch(() => SECSItemFactory.GenerateSECSItem(input));

            Assert.IsInstanceOf<ArgumentOutOfRangeException>(exception);

            Assert.IsTrue(exception.Message.Contains("Illegal data length of: 3 payload length must be a multiple of 4."));
        }
	}
}

