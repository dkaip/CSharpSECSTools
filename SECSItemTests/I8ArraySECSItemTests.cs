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
	public class I8ArraySECSItemTests
	{
        [Test()]
        public void Test00()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8) << 2) | 0x01), 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255 };

            SECSItem secsItem = SECSItemFactory.GenerateSECSItem(input);

            Assert.IsTrue(secsItem.GetType() == typeof(I8ArraySECSItem));
        }

        [Test()]
        public void Test01()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8) << 2) | 0x01), 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255 };
            I8ArraySECSItem secsItem = (I8ArraySECSItem)SECSItemFactory.GenerateSECSItem(input);
            Assert.IsTrue(secsItem.Value[0] == -1);
            Assert.IsTrue(secsItem.Value[1] == -9223372036854775808L);
            Assert.IsTrue(secsItem.Value[2] == 0x0000000000000000);
            Assert.IsTrue(secsItem.Value[3] == 0x0000000000000001);
            Assert.IsTrue(secsItem.Value[4] == 9223372036854775807L);
        }

        [Test()]
        public void Test02()
        {
            Int64 [] input = { -1, -9223372036854775808L, 0, 1, 9223372036854775807L};
            I8ArraySECSItem secsItem = new I8ArraySECSItem(input);
            Assert.AreEqual(secsItem.Value, input);
        }

        [Test()]
        public void Test03()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8) << 2) | 0x01), 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255 };
            I8ArraySECSItem secsItem = (I8ArraySECSItem)SECSItemFactory.GenerateSECSItem(input);
            Assert.IsTrue(secsItem.ItemFormatCode == SECSItemFormatCode.I8);
        }

        [Test()]
        public void Test04()
        {
            Int64 [] input = { -1, -9223372036854775808L, 0, 1, 9223372036854775807L };
            I8ArraySECSItem secsItem = new I8ArraySECSItem(input);
            Assert.IsTrue(secsItem.ItemFormatCode == SECSItemFormatCode.I8);
        }

        [Test()]
        public void Test05()
        {
            Int64 [] input = { -1, -9223372036854775808L, 0, 1, 9223372036854775807L };
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8) << 2) | 0x01), 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255 };

            I8ArraySECSItem secsItem = new I8ArraySECSItem(input);
            Assert.AreEqual(secsItem.EncodeForTransport(), expectedResult);
        }

        [Test()]
        public void Test06()
        {
            Int64 [] input = { -1, -9223372036854775808L, 0, 1, 9223372036854775807L };
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8) << 2) | 0x02), 0, 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255 };

            I8ArraySECSItem secsItem = new I8ArraySECSItem(input, SECSItemNumLengthBytes.TWO);
            Assert.AreEqual(secsItem.EncodeForTransport(), expectedResult);
        }

        [Test()]
        public void Test07()
        {
            Int64 [] input = { -1, -9223372036854775808L, 0, 1, 9223372036854775807L };
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8) << 2) | 0x03), 0, 0, 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255 };

            I8ArraySECSItem secsItem = new I8ArraySECSItem(input, SECSItemNumLengthBytes.THREE);
            Assert.AreEqual(secsItem.EncodeForTransport(), expectedResult);
        }

        [Test()]
        public void Test08()
        {
            Int64 [] input = { -1, -9223372036854775808L, 0, 1, 9223372036854775807L };

            I8ArraySECSItem secsItem = new I8ArraySECSItem(input, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue(secsItem.ToString().Equals("Format:I8 Value: Array"));
        }

        [Test()]
        public void Test09()
        {
            Assert.IsTrue(true);
            /*
            UInt64 [] input = { 0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000, 0x0000000000000001, 0x7FFFFFFFFFFFFFFF };

            U8ArraySECSItem secsItem = new U8ArraySECSItem(input, SECSItemNumLengthBytes.ONE);

            Console.WriteLine (secsItem.GetHashCode());
            Assert.IsTrue(secsItem.GetHashCode() == -1611957742);
            */
        }

        [Test()]
        public void Test10()
        {
            Int64 [] input = { -1, -9223372036854775808L, 0, 1, 9223372036854775807L };

            I8ArraySECSItem secsItem = new I8ArraySECSItem(input, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue(secsItem.Equals(secsItem));
        }

        [Test()]
        public void Test11()
        {
            Int64 [] input = { -1, -9223372036854775808L, 0, 1, 9223372036854775807L };

            I8ArraySECSItem secsItem = new I8ArraySECSItem(input, SECSItemNumLengthBytes.ONE);

            Assert.IsFalse(secsItem.Equals(null));
        }

        [Test()]
        public void Test12()
        {
            Int64 [] input = { -1, -9223372036854775808L, 0, 1, 9223372036854775807L };

            I8ArraySECSItem secsItem = new I8ArraySECSItem(input, SECSItemNumLengthBytes.ONE);

            Object secsItem2 = new F8SECSItem(2.141592D);
            Assert.IsFalse(secsItem.Equals(secsItem2));
        }

        [Test()]
        public void Test13()
        {
            Int64 [] input1 = { -1, -9223372036854775808L, 0, 1, 9223372036854775807L };
            Int64 [] input2 = { -1, -9223372036854775808L, 0, 1, 9223372036854775807L };

            I8ArraySECSItem secsItem1 = new I8ArraySECSItem(input1, SECSItemNumLengthBytes.ONE);
            I8ArraySECSItem secsItem2 = new I8ArraySECSItem(input2, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue(secsItem1.Equals(secsItem2));
        }

        [Test()]
        public void Test14()
        {
            Int64 [] input1 = { -1, -9223372036854775808L, 0, 1, 9223372036854775807L };
            Int64 [] input2 = { -1, -9223372036854775808L, 0, 1, 0 };

            I8ArraySECSItem secsItem1 = new I8ArraySECSItem(input1, SECSItemNumLengthBytes.ONE);
            I8ArraySECSItem secsItem2 = new I8ArraySECSItem(input2, SECSItemNumLengthBytes.ONE);


            Assert.IsFalse(secsItem1.Equals(secsItem2));
        }

        [Test()]
        public void Test15()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.I8) << 2) | 0x01), 0x00 };

            I8ArraySECSItem secsItem = (I8ArraySECSItem)SECSItemFactory.GenerateSECSItem(input);

            Assert.IsTrue(secsItem.LengthInBytes == 0);
        }

        [Test()]
        public void Test16()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.I8) << 2) | 0x01), 0x03 };

            var exception = Assert.Catch(() => SECSItemFactory.GenerateSECSItem(input));

            Assert.IsInstanceOf<ArgumentOutOfRangeException>(exception);

            Assert.IsTrue(exception.Message.Contains("Illegal data length of: 3 payload length must be a multiple of 8."));
        }
	}
}

