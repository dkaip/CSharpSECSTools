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
	public class I4ArraySECSItemTests
	{
        [Test()]
    public void Test01 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x01), 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255 };
            I4ArraySECSItem secsItem = new I4ArraySECSItem (input, 0);
            Assert.IsTrue (secsItem.GetValue () [0] == -1);
            Assert.IsTrue (secsItem.GetValue () [1] == -2147483648);
            Assert.IsTrue (secsItem.GetValue () [2] == 0);
            Assert.IsTrue (secsItem.GetValue () [3] == 1);
            Assert.IsTrue (secsItem.GetValue () [4] == 2147483647);
        }

        [Test()]
        public void Test02 ()
        {
            int [] input = { -1, -2147483648, 0, 1, 2147483647 };
            I4ArraySECSItem secsItem = new I4ArraySECSItem (input);
            Assert.AreEqual (secsItem.GetValue (), input);
        }

        [Test()]
        public void Test03 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x01), 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255 };
            I4ArraySECSItem secsItem = new I4ArraySECSItem (input, 0);
            Assert.IsTrue (secsItem.GetSECSItemFormatCode () == SECSItemFormatCode.I4);
        }

        [Test()]
        public void Test04 ()
        {
            int [] input = { -1, -2147483648, 0, 1, 2147483647 };
            I4ArraySECSItem secsItem = new I4ArraySECSItem (input);
            Assert.IsTrue (secsItem.GetSECSItemFormatCode () == SECSItemFormatCode.I4);
        }

        [Test()]
        public void Test05 ()
        {
            int [] input = { -1, -2147483648, 0, 1, 2147483647 };
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x01), 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255 };

            I4ArraySECSItem secsItem = new I4ArraySECSItem (input);
            Assert.AreEqual (secsItem.EncodeForTransport (), expectedResult);
        }

        [Test()]
        public void Test06 ()
        {
            int [] input = { -1, -2147483648, 0, 1, 2147483647 };
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x02), 0, 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255 };

            I4ArraySECSItem secsItem = new I4ArraySECSItem (input, SECSItemNumLengthBytes.TWO);
            Assert.AreEqual (secsItem.EncodeForTransport (), expectedResult);
        }

        [Test()]
        public void Test07 ()
        {
            int [] input = { -1, -2147483648, 0, 1, 2147483647 };
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x03), 0, 0, 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255 };

            I4ArraySECSItem secsItem = new I4ArraySECSItem (input, SECSItemNumLengthBytes.THREE);
            Assert.AreEqual (secsItem.EncodeForTransport (), expectedResult);
        }

        [Test()]
        public void Test08 ()
        {
            int [] input = { -1, -2147483648, 0, 1, 2147483647 };

            I4ArraySECSItem secsItem = new I4ArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem.ToString ().Equals ("Format:I4 Value: Array"));
        }

        [Test()]
        public void Test09 ()
        {
            Assert.IsTrue (true);
            /*
            int [] input = { -1, -2147483648, 0, 1, 2147483647 };

            I4ArraySECSItem secsItem = new I4ArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem.GetHashCode () == 27705691);
            */
        }

        [Test()]
        public void Test10 ()
        {
            int [] input = { -1, -2147483648, 0, 1, 2147483647 };

            I4ArraySECSItem secsItem = new I4ArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem.Equals (secsItem));
        }

        [Test()]
        public void Test11 ()
        {
            int [] input = { -1, -2147483648, 0, 1, 2147483647 };

            I4ArraySECSItem secsItem = new I4ArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Assert.IsFalse (secsItem.Equals (null));
        }

        [Test()]
        public void Test12 ()
        {
            int [] input = { -1, -2147483648, 0, 1, 2147483647 };

            I4ArraySECSItem secsItem = new I4ArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Object secsItem2 = new F8SECSItem (2.141592D);
            Assert.IsFalse (secsItem.Equals (secsItem2));
        }

        [Test()]
        public void Test13 ()
        {
            int [] input1 = { -1, -2147483648, 0, 1, 2147483647 };
            int [] input2 = { -1, -2147483648, 0, 1, 2147483647 };

            I4ArraySECSItem secsItem1 = new I4ArraySECSItem (input1, SECSItemNumLengthBytes.ONE);
            I4ArraySECSItem secsItem2 = new I4ArraySECSItem (input2, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test14 ()
        {
            int [] input1 = { -1, -2147483648, 0, 1, 2147483647 };
            int [] input2 = { -1, -2147483648, 0, 1, 0 };

            I4ArraySECSItem secsItem1 = new I4ArraySECSItem (input1, SECSItemNumLengthBytes.ONE);
            I4ArraySECSItem secsItem2 = new I4ArraySECSItem (input2, SECSItemNumLengthBytes.ONE);

            Assert.IsFalse (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test15 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.I4) << 2) | 0x01), 0x00 };

            var exception = Assert.Catch (() => new I4ArraySECSItem (input, 0));

            Assert.IsInstanceOf<ArgumentOutOfRangeException> (exception);

            Assert.IsTrue (exception.Message.Contains ("Illegal data length of: 0 payload length must be a non-zero multiple of 4."));
        }

        [Test()]
        public void Test16 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.I4) << 2) | 0x01), 0x03 };

            var exception = Assert.Catch (() => new I4ArraySECSItem (input, 0));

            Assert.IsInstanceOf<ArgumentOutOfRangeException> (exception);

            Assert.IsTrue (exception.Message.Contains ("Illegal data length of: 3 payload length must be a non-zero multiple of 4."));
        }
	}
}

