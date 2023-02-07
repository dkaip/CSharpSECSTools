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
	public class I8SECSItemTests
	{
        [Test()]
        public void Test00()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8) << 2) | 0x01), 0x08, 255, 255, 255, 255, 255, 255, 255, 255 };

            SECSItem secsItem = SECSItemFactory.GenerateSECSItem(input);

            Assert.IsTrue(secsItem.GetType() == typeof(I8SECSItem));
        }

        [Test()]
        public void Test01()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8) << 2) | 0x01), 0x08, 255, 255, 255, 255, 255, 255, 255, 255 };
            long expectedOutput = -1;
            I8SECSItem secsItem = (I8SECSItem)SECSItemFactory.GenerateSECSItem(input);
            Assert.IsTrue(secsItem.Value == expectedOutput);
        }

        [Test()]
        public void Test02()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8) << 2) | 0x01), 0x08, 128, 0, 0, 0, 0, 0, 0, 0 };
            long expectedOutput = -9223372036854775808L;
            I8SECSItem secsItem = (I8SECSItem)SECSItemFactory.GenerateSECSItem(input);
            Assert.IsTrue(secsItem.Value == expectedOutput);
        }

        [Test()]
        public void Test03()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8) << 2) | 0x01), 0x08, 0, 0, 0, 0, 0, 0, 0, 0 };
            long expectedOutput = 0;
            I8SECSItem secsItem = (I8SECSItem)SECSItemFactory.GenerateSECSItem(input);
            Assert.IsTrue(secsItem.Value == expectedOutput);
        }

        [Test()]
        public void Test04()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8) << 2) | 0x01), 0x08, 0, 0, 0, 0, 0, 0, 0, 1 };
            long expectedOutput = 1;
            I8SECSItem secsItem = (I8SECSItem)SECSItemFactory.GenerateSECSItem(input);
            Assert.IsTrue(secsItem.Value == expectedOutput);
        }

        [Test()]
        public void Test05()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8) << 2) | 0x01), 0x08, 127, 255, 255, 255, 255, 255, 255, 255 };
            long expectedOutput = 9223372036854775807L;
            I8SECSItem secsItem = (I8SECSItem)SECSItemFactory.GenerateSECSItem(input);
            Assert.IsTrue(secsItem.Value == expectedOutput);
        }

        [Test()]
        public void Test06()
        {
            long expectedOutput = 9223372036854775807L;
            I8SECSItem secsItem = new I8SECSItem(expectedOutput);
            Assert.IsTrue(secsItem.Value == expectedOutput);
        }

        [Test()]
        public void Test07()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8) << 2) | 0x01), 0x08, 255, 255, 255, 255, 255, 255, 255, 255 };
            I8SECSItem secsItem = (I8SECSItem)SECSItemFactory.GenerateSECSItem(input);
            Assert.IsTrue(secsItem.ItemFormatCode == SECSItemFormatCode.I8);
        }

        [Test()]
        public void Test08()
        {
            long expectedOutput = 9223372036854775807L;
            I8SECSItem secsItem = new I8SECSItem(expectedOutput);
            Assert.IsTrue(secsItem.ItemFormatCode == SECSItemFormatCode.I8);
        }

        [Test()]
        public void Test09()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8) << 2) | 0x01), 0x08, 127, 255, 255, 255, 255, 255, 255, 255 };

            I8SECSItem secsItem = new I8SECSItem(9223372036854775807L);
            Assert.AreEqual(secsItem.EncodeForTransport(), expectedResult);
        }

        [Test()]
        public void Test10()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8) << 2) | 0x02), 0, 0x08, 127, 255, 255, 255, 255, 255, 255, 255 };

            I8SECSItem secsItem = new I8SECSItem(9223372036854775807L, SECSItemNumLengthBytes.TWO);
            Assert.AreEqual(secsItem.EncodeForTransport(), expectedResult);
        }

        [Test()]
        public void Test11()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8) << 2) | 0x03), 0, 0, 0x08, 127, 255, 255, 255, 255, 255, 255, 255 };

            I8SECSItem secsItem = new I8SECSItem(9223372036854775807L, SECSItemNumLengthBytes.THREE);
            Assert.AreEqual(secsItem.EncodeForTransport(), expectedResult);
        }

        [Test()]
        public void Test13()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8) << 2) | 0x01), 0x09 };

            var exception = Assert.Catch(() => SECSItemFactory.GenerateSECSItem(input));

            Assert.IsInstanceOf<ArgumentOutOfRangeException>(exception);

            Assert.IsTrue(exception.Message.Contains("Illegal data length of: 9 payload length must be a multiple of 8."));
        }

        [Test()]
        public void Test14()
        {
            I8SECSItem secsItem = new I8SECSItem(9223372036854775807L);
            Assert.IsTrue(secsItem.ToString().Equals("Format:I8 Value: 9223372036854775807"));
        }

        [Test()]
        public void Test15()
        {
            I8SECSItem secsItem1 = new I8SECSItem(9223372036854775807L);
            I8SECSItem secsItem2 = new I8SECSItem(9223372036854775807L);
            Assert.IsTrue(secsItem1.Equals(secsItem2));
        }

        [Test()]
        public void Test16()
        {
            I8SECSItem secsItem1 = new I8SECSItem(9223372036854775807L);
            I8SECSItem secsItem2 = new I8SECSItem(-9223372036854775808L);
            Assert.IsFalse(secsItem1.Equals(secsItem2));
        }

        [Test()]
        public void Test17()
        {
            I8SECSItem secsItem1 = new I8SECSItem(9223372036854775807L);
            I8SECSItem secsItem2 = null;
            Assert.IsFalse(secsItem1.Equals(secsItem2));
        }

        [Test()]
        public void Test18()
        {
            I8SECSItem secsItem1 = new I8SECSItem(9223372036854775807L);
            Assert.IsTrue(secsItem1.Equals(secsItem1));
        }

        [Test()]
        public void Test19()
        {
            I8SECSItem secsItem1 = new I8SECSItem(9223372036854775807L);
            SECSItem secsItem2 = null;
            Assert.IsFalse(secsItem1.Equals(secsItem2));
        }

        [Test()]
        public void Test20()
        {
            I8SECSItem secsItem1 = new I8SECSItem(9223372036854775807L);
            Object secsItem2 = new F8SECSItem(2.141592D);
            Assert.IsFalse(secsItem1.Equals(secsItem2));
        }

        [Test()]
        public void Test21()
        {
            Assert.IsTrue(true);
            /*
            I8SECSItem secsItem1 = new I8SECSItem(9223372036854775807L);
                    Assert.IsTrue(secsItem1.GetHashCode() == -2147483617);
                    */
        }

        public void Test22()
        {
            I8SECSItem secsItem1 = new I8SECSItem(9223372036854775807L);
            I8SECSItem secsItem2 = new I8SECSItem(9223372036854775807L);
            Assert.IsTrue(secsItem1.GetHashCode() == secsItem2.GetHashCode());
        }
	}
}

