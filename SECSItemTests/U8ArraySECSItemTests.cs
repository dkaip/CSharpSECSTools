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
	public class U8ArraySECSItemTests
	{
        [Test()]
        public void Test01 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U8 ) << 2) | 0x01), 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255 };
            U8ArraySECSItem secsItem = new U8ArraySECSItem (input, 0);
            Assert.IsTrue (secsItem.GetValue () [0] == 0xFFFFFFFFFFFFFFFF);
            Assert.IsTrue (secsItem.GetValue () [1] == 0x8000000000000000);
            Assert.IsTrue (secsItem.GetValue () [2] == 0x0000000000000000);
            Assert.IsTrue (secsItem.GetValue () [3] == 0x0000000000000001);
            Assert.IsTrue (secsItem.GetValue () [4] == 0x7FFFFFFFFFFFFFFF);
        }

        [Test()]
        public void Test02 ()
        {
            UInt64 [] input = { 0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000, 0x0000000000000001, 0x7FFFFFFFFFFFFFFF};
            U8ArraySECSItem secsItem = new U8ArraySECSItem (input);
            Assert.AreEqual (secsItem.GetValue (), input);
        }

        [Test()]
        public void Test03 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U8 ) << 2) | 0x01), 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255 };
            U8ArraySECSItem secsItem = new U8ArraySECSItem (input, 0);
            Assert.IsTrue (secsItem.GetSECSItemFormatCode () == SECSItemFormatCode.U8);
        }

        [Test()]
        public void Test04 ()
        {
            UInt64 [] input = { 0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000, 0x0000000000000001, 0x7FFFFFFFFFFFFFFF };
            U8ArraySECSItem secsItem = new U8ArraySECSItem (input);
            Assert.IsTrue (secsItem.GetSECSItemFormatCode () == SECSItemFormatCode.U8);
        }

        [Test()]
        public void Test05 ()
        {
            UInt64 [] input = { 0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000, 0x0000000000000001, 0x7FFFFFFFFFFFFFFF };
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U8 ) << 2) | 0x01), 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255 };

            U8ArraySECSItem secsItem = new U8ArraySECSItem (input);
            Assert.AreEqual (secsItem.EncodeForTransport (), expectedResult);
        }

        [Test()]
        public void Test06 ()
        {
            UInt64 [] input = { 0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000, 0x0000000000000001, 0x7FFFFFFFFFFFFFFF };
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U8 ) << 2) | 0x02), 0, 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255 };

            U8ArraySECSItem secsItem = new U8ArraySECSItem (input, SECSItemNumLengthBytes.TWO);
            Assert.AreEqual (secsItem.EncodeForTransport (), expectedResult);
        }

        [Test()]
        public void Test07 ()
        {
            UInt64 [] input = { 0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000, 0x0000000000000001, 0x7FFFFFFFFFFFFFFF };
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U8 ) << 2) | 0x03), 0, 0, 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255 };

            U8ArraySECSItem secsItem = new U8ArraySECSItem (input, SECSItemNumLengthBytes.THREE);
            Assert.AreEqual (secsItem.EncodeForTransport (), expectedResult);
        }

        [Test()]
        public void Test08 ()
        {
            UInt64 [] input = { 0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000, 0x0000000000000001, 0x7FFFFFFFFFFFFFFF };

            U8ArraySECSItem secsItem = new U8ArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem.ToString ().Equals ("Format:U8 Value: Array"));
        }

        [Test()]
        public void Test09 ()
        {
            Assert.IsTrue (true);
            /*
            UInt64 [] input = { 0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000, 0x0000000000000001, 0x7FFFFFFFFFFFFFFF };

            U8ArraySECSItem secsItem = new U8ArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Console.WriteLine (secsItem.GetHashCode ());
            Assert.IsTrue (secsItem.GetHashCode () == -1611957742);
            */
        }

        [Test()]
        public void Test10 ()
        {
            UInt64 [] input = { 0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000, 0x0000000000000001, 0x7FFFFFFFFFFFFFFF };

            U8ArraySECSItem secsItem = new U8ArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem.Equals (secsItem));
        }

        [Test()]
        public void Test11 ()
        {
            UInt64 [] input = { 0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000, 0x0000000000000001, 0x7FFFFFFFFFFFFFFF };

            U8ArraySECSItem secsItem = new U8ArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Assert.IsFalse (secsItem.Equals (null));
        }

        [Test()]
        public void Test12 ()
        {
            UInt64 [] input = { 0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000, 0x0000000000000001, 0x7FFFFFFFFFFFFFFF };

            U8ArraySECSItem secsItem = new U8ArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Object secsItem2 = new F8SECSItem (2.141592D);
            Assert.IsFalse (secsItem.Equals (secsItem2));
        }

        [Test()]
        public void Test13 ()
        {
            UInt64 [] input1 = { 0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000, 0x0000000000000001, 0x7FFFFFFFFFFFFFFF };
            UInt64 [] input2 = { 0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000, 0x0000000000000001, 0x7FFFFFFFFFFFFFFF };

            U8ArraySECSItem secsItem1 = new U8ArraySECSItem (input1, SECSItemNumLengthBytes.ONE);
            U8ArraySECSItem secsItem2 = new U8ArraySECSItem (input2, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test14 ()
        {
            UInt64 [] input1 = { 0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000, 0x0000000000000001, 0x7FFFFFFFFFFFFFFF };
            UInt64 [] input2 = { 0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000, 0x0000000000000001, 0x0000000000000000 };

            U8ArraySECSItem secsItem1 = new U8ArraySECSItem (input1, SECSItemNumLengthBytes.ONE);
            U8ArraySECSItem secsItem2 = new U8ArraySECSItem (input2, SECSItemNumLengthBytes.ONE);

            Assert.IsFalse (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test15 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.U8) << 2) | 0x01), 0x00 };

            var exception = Assert.Catch (() => new U8ArraySECSItem (input, 0));

            Assert.IsInstanceOf<ArgumentOutOfRangeException> (exception);

            Assert.IsTrue (exception.Message.Contains ("Illegal data length of: 0 payload length must be a non-zero multiple of 8."));
        }

        [Test()]
        public void Test16 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.U8) << 2) | 0x01), 0x03 };

            var exception = Assert.Catch (() => new U8ArraySECSItem (input, 0));

            Assert.IsInstanceOf<ArgumentOutOfRangeException> (exception);

            Assert.IsTrue (exception.Message.Contains ("Illegal data length of: 3 payload length must be a non-zero multiple of 8."));
        }
	}
}

