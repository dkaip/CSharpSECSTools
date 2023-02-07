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
	public class I1ArraySECSItemTests
	{
        [Test()]
        public void Test00()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1) << 2) | 0x01), 0x04, 255, 128, 0, 127 };

            SECSItem secsItem = SECSItemFactory.GenerateSECSItem(input);

            Assert.IsTrue(secsItem.GetType() == typeof(I1ArraySECSItem));
        }

        [Test()]
        public void Test00a()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1) << 2) | 0x01), 0x04, 255, 128, 0, 127 };
            SECSItem secsItem = SECSItemFactory.GenerateSECSItem(input);
            Assert.IsTrue(secsItem.GetType() == typeof(I1ArraySECSItem));
        }

        [Test()]
        public void Test01()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1) << 2) | 0x01), 0x04, 255, 128, 0, 127 };
            sbyte [] output = new sbyte[4];
            output [0] = -1;
            output [1] = -128;
            output [2] = 0;
            output [3] = 127;
            I1ArraySECSItem secsItem = (I1ArraySECSItem)SECSItemFactory.GenerateSECSItem(input);
            Assert.AreEqual(secsItem.Value, output);
        }

        [Test()]
        public void Test02()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1) << 2) | 0x01), 0x04, 255, 128, 0, 127 };
            I1ArraySECSItem secsItem = (I1ArraySECSItem)SECSItemFactory.GenerateSECSItem(input);
            Assert.IsTrue(secsItem.ItemFormatCode == SECSItemFormatCode.I1);
        }

        [Test()]
        public void Test03()
        {
            sbyte [] input = { -1, -128, 0, 1, 127 };
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1) << 2) | 0x01), 0x05, 255, 128, 0, 1, 127 };

            I1ArraySECSItem secsItem = new I1ArraySECSItem(input);
            Assert.AreEqual(secsItem.EncodeForTransport(), expectedResult);
        }

        [Test()]
        public void Test04()
        {
            sbyte [] input = { -1, -128, 0, 1, 127 };
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1) << 2) | 0x02), 0, 0x05, 255, 128, 0, 1, 127 };

            I1ArraySECSItem secsItem = new I1ArraySECSItem(input, SECSItemNumLengthBytes.TWO);
            Assert.AreEqual(secsItem.EncodeForTransport(), expectedResult);
        }

        [Test()]
        public void Test05()
        {
            sbyte [] input = { -1, -128, 0, 1, 127 };
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1) << 2) | 0x03), 0, 0, 0x05, 255, 128, 0, 1, 127 };

            I1ArraySECSItem secsItem = new I1ArraySECSItem(input, SECSItemNumLengthBytes.THREE);
            Assert.AreEqual(secsItem.EncodeForTransport(), expectedResult);
        }

        [Test()]
        public void Test06()
        {
            sbyte [] input = { -1, -128, 0, 1, 127 };

            I1ArraySECSItem secsItem = new I1ArraySECSItem(input, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue(secsItem.ToString().Equals("Format:I1 Value: Array"));
        }

        // [Test()]
        // public void Test07()
        // {
        //     Assert.IsTrue (true);
        //     /*
        //     sbyte [] input = { -1, -128, 0, 1, 127 };

        //     I1ArraySECSItem secsItem = new I1ArraySECSItem (input, SECSItemNumLengthBytes.ONE);

        //     Assert.IsTrue (secsItem.GetHashCode() == 23892571);
        //     */
        // }

        [Test()]
        public void Test08()
        {
            sbyte [] input = { -1, -128, 0, 1, 127 };

            I1ArraySECSItem secsItem = new I1ArraySECSItem(input, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue(secsItem.Equals(secsItem));
        }

        [Test()]
        public void Test09()
        {
            sbyte [] input = { -1, -128, 0, 1, 127 };

            I1ArraySECSItem secsItem = new I1ArraySECSItem(input, SECSItemNumLengthBytes.ONE);

            Assert.IsFalse(secsItem.Equals(null));
        }

        [Test()]
        public void Test10()
        {
            sbyte [] input = { -1, -128, 0, 1, 127 };

            I1ArraySECSItem secsItem = new I1ArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Object secsItem2 = new F8SECSItem(2.141592D);
            Assert.IsFalse(secsItem.Equals(secsItem2));
        }

        [Test()]
        public void Test11()
        {
            sbyte [] input1 = { -1, -128, 0, 1, 127 };
            sbyte [] input2 = { -1, -128, 0, 1, 127 };

            I1ArraySECSItem secsItem1 = new I1ArraySECSItem(input1, SECSItemNumLengthBytes.ONE);
            I1ArraySECSItem secsItem2 = new I1ArraySECSItem(input2, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem1.Equals(secsItem2));
        }

        [Test()]
        public void Test12()
        {
            sbyte [] input1 = { -1, -128, 0, 1, 127 };
            sbyte [] input2 = { -1, -128, 0, 1, 0 };

            I1ArraySECSItem secsItem1 = new I1ArraySECSItem(input1, SECSItemNumLengthBytes.ONE);
            I1ArraySECSItem secsItem2 = new I1ArraySECSItem(input2, SECSItemNumLengthBytes.ONE);

            Assert.IsFalse(secsItem1.Equals(secsItem2));
        }

        [Test()]
        public void Test13()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1) << 2) | 0x01), 0x00 };

            I1ArraySECSItem secsItem = (I1ArraySECSItem)SECSItemFactory.GenerateSECSItem(input);

            Assert.IsTrue(secsItem.Value.Length == 0);
        }

        [Test()]
        public void Test14()
        {
            I1ArraySECSItem secsItem = new I1ArraySECSItem(null);

            Assert.IsTrue(secsItem.Value.Length == 0);
        }

        [Test()]
        public void Test15()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1) << 2) | 0x01), 0x00 };

            I1ArraySECSItem secsItem = new I1ArraySECSItem(null);

            Assert.AreEqual(secsItem.EncodeForTransport(), expectedResult);
        }
	}
}

