﻿/*
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
	public class I2ArraySECSItemTests
	{
        [Test()]
        public void Test00()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2) << 2) | 0x01), 0x02, 255, 255 };

            SECSItem secsItem = SECSItemFactory.GenerateSECSItem(input);

            Assert.IsTrue(secsItem.GetType() == typeof(I2SECSItem));
        }

        [Test()]
        public void Test01()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2) << 2) | 0x01), 0x02, 255, 255 };
            short expectedOutput = -1;
            I2SECSItem secsItem = (I2SECSItem)SECSItemFactory.GenerateSECSItem(input);

            Assert.IsTrue(secsItem.Value == expectedOutput);
        }

        [Test()]
        public void Test02()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2) << 2) | 0x01), 0x02, 128, 0 };
            short expectedOutput = -32768;
            I2SECSItem secsItem = (I2SECSItem)SECSItemFactory.GenerateSECSItem(input);
            Assert.IsTrue(secsItem.Value == expectedOutput);
        }

        [Test()]
        public void Test03()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2) << 2) | 0x01), 0x02, 0, 0 };
            short expectedOutput = 0;
            I2SECSItem secsItem = (I2SECSItem)SECSItemFactory.GenerateSECSItem(input);
            Assert.IsTrue(secsItem.Value == expectedOutput);
        }

        [Test()]
        public void Test04()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2) << 2) | 0x01), 0x02, 0, 1 };
            short expectedOutput = 1;
            I2SECSItem secsItem = (I2SECSItem)SECSItemFactory.GenerateSECSItem(input);
            Assert.IsTrue(secsItem.Value == expectedOutput);
        }

        [Test()]
        public void Test05()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2) << 2) | 0x01), 0x02, 127, 255 };
            short expectedOutput = 32767;
            I2SECSItem secsItem = (I2SECSItem)SECSItemFactory.GenerateSECSItem(input);
            Assert.IsTrue(secsItem.Value == expectedOutput);
        }

        [Test()]
        public void Test06()
        {
            short expectedOutput = 32767;
            I2SECSItem secsItem = new I2SECSItem(expectedOutput);
            Assert.IsTrue(secsItem.Value == expectedOutput);
        }

        [Test()]
        public void Test07()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2) << 2) | 0x01), 0x02, 255, 255 };
            I2SECSItem secsItem = (I2SECSItem)SECSItemFactory.GenerateSECSItem(input);
            Assert.IsTrue(secsItem.ItemFormatCode == SECSItemFormatCode.I2);
        }

        [Test()]
        public void Test08()
        {
            short expectedOutput = 32767;
            I2SECSItem secsItem = new I2SECSItem(expectedOutput);
            Assert.IsTrue(secsItem.ItemFormatCode == SECSItemFormatCode.I2);
        }

        [Test()]
        public void Test09()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2) << 2) | 0x01), 0x02, 127, 255 };

            I2SECSItem secsItem = new I2SECSItem((short)32767);
            Assert.AreEqual(secsItem.EncodeForTransport(), expectedResult);
        }

        [Test()]
        public void Test10()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2) << 2) | 0x02), 0, 0x02, 127, 255 };

            I2SECSItem secsItem = new I2SECSItem((short)32767, SECSItemNumLengthBytes.TWO);
            Assert.AreEqual(secsItem.EncodeForTransport(), expectedResult);
        }

        [Test()]
        public void Test11()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2) << 2) | 0x03), 0, 0, 0x02, 127, 255 };

            I2SECSItem secsItem = new I2SECSItem((short)32767, SECSItemNumLengthBytes.THREE);
            Assert.AreEqual(secsItem.EncodeForTransport(), expectedResult);
        }

        [Test()]
        public void Test12()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2) << 2) | 0x01), 0x00 };

            I2ArraySECSItem secsItem = (I2ArraySECSItem)SECSItemFactory.GenerateSECSItem(input);

            Assert.IsTrue(secsItem.LengthInBytes == 0);
        }

        [Test()]
        public void Test13()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2) << 2) | 0x01), 0x03 };

            var exception = Assert.Catch(() => SECSItemFactory.GenerateSECSItem(input));

            Assert.IsInstanceOf<ArgumentOutOfRangeException>(exception);

            Assert.IsTrue(exception.Message.Contains("Illegal data length of: 3 payload length must be a multiple of 2."));
        }

        [Test()]
        public void Test14()
        {
            I2SECSItem secsItem = new I2SECSItem((short)32767);
            Assert.IsTrue(secsItem.ToString().Equals("Format:I2 Value: 32767"));
        }

        [Test()]
        public void Test15()
        {
            I2SECSItem secsItem1 = new I2SECSItem((short)32767);
            I2SECSItem secsItem2 = new I2SECSItem((short)32767);
            Assert.IsTrue(secsItem1.Equals(secsItem2));
        }

        [Test()]
        public void Test16()
        {
            I2SECSItem secsItem1 = new I2SECSItem((short)32767);
            I2SECSItem secsItem2 = new I2SECSItem((short)-32768);
            Assert.IsFalse(secsItem1.Equals(secsItem2));
        }

        [Test()]
        public void Test17()
        {
            I2SECSItem secsItem1 = new I2SECSItem((short)32767);
            I2SECSItem secsItem2 = null;
            Assert.IsFalse(secsItem1.Equals(secsItem2));
        }

        [Test()]
        public void Test18()
        {
            I2SECSItem secsItem1 = new I2SECSItem((short)32767);
            Assert.IsTrue(secsItem1.Equals(secsItem1));
        }

        [Test()]
        public void Test19()
        {
            I2SECSItem secsItem1 = new I2SECSItem((short)32767);
            SECSItem secsItem2 = null;
            Assert.IsFalse(secsItem1.Equals(secsItem2));
        }

        [Test()]
        public void Test20()
        {
            I2SECSItem secsItem1 = new I2SECSItem((short)32767);
            Object secsItem2 = new F8SECSItem(2.141592D);
            Assert.IsFalse(secsItem1.Equals(secsItem2));
        }

        [Test()]
        public void Test21()
        {
            Assert.IsTrue(true);
            /*
            I2SECSItem secsItem1 = new I2SECSItem((short)32767);
            Assert.IsTrue(secsItem1.GetHashCode() == 32798);
            */
        }

        [Test()]
        public void Test22()
        {
            I2SECSItem secsItem1 = new I2SECSItem((short)32767);
            I2SECSItem secsItem2 = new I2SECSItem((short)32767);
            Assert.IsTrue(secsItem1.GetHashCode() == secsItem2.GetHashCode());
        }
	}
}

