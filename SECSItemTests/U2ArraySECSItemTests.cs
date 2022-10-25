/*
 * Copyright 2019-2022 Douglas Kaip
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
	public class U2ArraySECSItemTests
	{
        [Test()]
        public void Test01 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2 ) << 2) | 0x01), 10,
                255, 255,
                128, 0,
                0, 0,
                0, 1,
                127, 255 };
            U2ArraySECSItem secsItem = new U2ArraySECSItem (input, 0);
            Assert.IsTrue (secsItem.GetValue () [0] == 65535);
            Assert.IsTrue (secsItem.GetValue () [1] == 32768);
            Assert.IsTrue (secsItem.GetValue () [2] == 0);
            Assert.IsTrue (secsItem.GetValue () [3] == 1);
            Assert.IsTrue (secsItem.GetValue () [4] == 32767);
        }

        [Test()]
        public void Test02 ()
        {
            UInt16 [] input = { 65535, 32768, 0, 1, 32767 };
            U2ArraySECSItem secsItem = new U2ArraySECSItem (input);
            Assert.AreEqual (secsItem.GetValue (), input);
        }

        [Test()]
        public void Test03 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2 ) << 2) | 0x01), 10,
                255, 255,
                128, 0,
                0, 0,
                0, 1,
                127, 255 };
            U2ArraySECSItem secsItem = new U2ArraySECSItem (input, 0);
            Assert.IsTrue (secsItem.GetSECSItemFormatCode () == SECSItemFormatCode.U2);
        }

        [Test()]
        public void Test04 ()
        {
            UInt16 [] input = { 65535, 32768, 0, 1, 32767 };
            U2ArraySECSItem secsItem = new U2ArraySECSItem (input);
            Assert.IsTrue (secsItem.GetSECSItemFormatCode () == SECSItemFormatCode.U2);
        }

        [Test()]
        public void Test05 ()
        {
            UInt16 [] input = { 65535, 32768, 0, 1, 32767 };
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2 ) << 2) | 0x01), 10,
                255, 255,
                128, 0,
                0, 0,
                0, 1,
                127, 255 };

            U2ArraySECSItem secsItem = new U2ArraySECSItem (input);
            Assert.AreEqual (secsItem.EncodeForTransport (), expectedResult);
        }

        [Test()]
        public void Test06 ()
        {
            UInt16 [] input = { 65535, 32768, 0, 1, 32767 };
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2 ) << 2) | 0x02), 0, 10,
                255, 255,
                128, 0,
                0, 0,
                0, 1,
                127, 255 };

            U2ArraySECSItem secsItem = new U2ArraySECSItem (input, SECSItemNumLengthBytes.TWO);
            Assert.AreEqual (secsItem.EncodeForTransport (), expectedResult);
        }

        [Test()]
        public void Test07 ()
        {
            UInt16 [] input = { 65535, 32768, 0, 1, 32767 };
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2 ) << 2) | 0x03), 0, 0, 10,
                255, 255,
                128, 0,
                0, 0,
                0, 1,
                127, 255 };

            U2ArraySECSItem secsItem = new U2ArraySECSItem (input, SECSItemNumLengthBytes.THREE);
            Assert.AreEqual (secsItem.EncodeForTransport (), expectedResult);
        }

        [Test()]
        public void Test08 ()
        {
            UInt16 [] input = { 65535, 32768, 0, 1, 32767 };

            U2ArraySECSItem secsItem = new U2ArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem.ToString ().Equals ("Format:U2 Value: Array"));
        }

        [Test()]
        public void Test09 ()
        {
            Assert.IsTrue (true);
            /*
            UInt16 [] input = { 65535, 32768, 0, 1, 32767 };

            U2ArraySECSItem secsItem = new U2ArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem.GetHashCode () == 1398260059);
            */
        }

        [Test()]
        public void Test10 ()
        {
            UInt16 [] input = { 65535, 32768, 0, 1, 32767 };

            U2ArraySECSItem secsItem = new U2ArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem.Equals (secsItem));
        }

        [Test()]
        public void Test11 ()
        {
            UInt16 [] input = { 65535, 32768, 0, 1, 32767 };

            U2ArraySECSItem secsItem = new U2ArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Assert.IsFalse (secsItem.Equals (null));
        }

        [Test()]
        public void Test12 ()
        {
            UInt16 [] input = { 65535, 32768, 0, 1, 32767 };

            U2ArraySECSItem secsItem = new U2ArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Object secsItem2 = new F8SECSItem (2.141592D);
            Assert.IsFalse (secsItem.Equals (secsItem2));
        }

        [Test()]
        public void Test13 ()
        {
            UInt16 [] input1 = { 65535, 32768, 0, 1, 32767 };
            UInt16 [] input2 = { 65535, 32768, 0, 1, 32767 };

            U2ArraySECSItem secsItem1 = new U2ArraySECSItem (input1, SECSItemNumLengthBytes.ONE);
            U2ArraySECSItem secsItem2 = new U2ArraySECSItem (input2, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test14 ()
        {
            UInt16 [] input1 = { 65535, 32768, 0, 1, 32767 };
            UInt16 [] input2 = { 65535, 32768, 0, 1, 0 };

            U2ArraySECSItem secsItem1 = new U2ArraySECSItem (input1, SECSItemNumLengthBytes.ONE);
            U2ArraySECSItem secsItem2 = new U2ArraySECSItem (input2, SECSItemNumLengthBytes.ONE);

            Assert.IsFalse (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test15 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.U2) << 2) | 0x01), 0x00 };

            var exception = Assert.Catch (() => new U2ArraySECSItem (input, 0));

            Assert.IsInstanceOf<ArgumentOutOfRangeException> (exception);

            Assert.IsTrue (exception.Message.Contains ("Illegal data length of: 0 payload length must be a non-zero multiple of 2."));
        }

        [Test()]
        public void Test16 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.U2) << 2) | 0x01), 0x03 };

            var exception = Assert.Catch (() => new U2ArraySECSItem (input, 0));

            Assert.IsInstanceOf<ArgumentOutOfRangeException> (exception);

            Assert.IsTrue (exception.Message.Contains ("Illegal data length of: 3 payload length must be a non-zero multiple of 2."));
        }
	}
}

