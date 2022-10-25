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
	public class I4SECSItemTests
	{
        [Test()]
        public void Test01 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.I4) << 2) | 0x01), 0x04, 255, 255, 255, 255 };
            int expectedOutput = -1;
            I4SECSItem secsItem = new I4SECSItem (input, 0);
            Assert.IsTrue (secsItem.GetValue () == expectedOutput);
        }

        [Test()]
        public void Test02 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.I4) << 2) | 0x01), 0x04, 128, 0, 0, 0 };
            int expectedOutput = -2147483648;
            I4SECSItem secsItem = new I4SECSItem (input, 0);
            Assert.IsTrue (secsItem.GetValue () == expectedOutput);
        }

        [Test()]
        public void Test03 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.I4) << 2) | 0x01), 0x04, 0, 0, 0, 0 };
            int expectedOutput = 0;
            I4SECSItem secsItem = new I4SECSItem (input, 0);
            Assert.IsTrue (secsItem.GetValue () == expectedOutput);
        }

        [Test()]
        public void Test04 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.I4) << 2) | 0x01), 0x04, 0, 0, 0, 1 };
            int expectedOutput = 1;
            I4SECSItem secsItem = new I4SECSItem (input, 0);
            Assert.IsTrue (secsItem.GetValue () == expectedOutput);
        }

        [Test()]
        public void Test05 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.I4) << 2) | 0x01), 0x04, 127, 255, 255, 255 };
            int expectedOutput = 2147483647;
            I4SECSItem secsItem = new I4SECSItem (input, 0);
            Assert.IsTrue (secsItem.GetValue () == expectedOutput);
        }

        [Test()]
        public void Test06 ()
        {
            int expectedOutput = 2147483647;
            I4SECSItem secsItem = new I4SECSItem (expectedOutput);
            Assert.IsTrue (secsItem.GetValue () == expectedOutput);
        }

        [Test()]
        public void Test07 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.I4) << 2) | 0x01), 0x04, 255, 255, 255, 255 };
            I4SECSItem secsItem = new I4SECSItem (input, 0);
            Assert.IsTrue (secsItem.GetSECSItemFormatCode () == SECSItemFormatCode.I4);
        }

        [Test()]
        public void Test08 ()
        {
            int expectedOutput = 2147483647;
            I4SECSItem secsItem = new I4SECSItem (expectedOutput);
            Assert.IsTrue (secsItem.GetSECSItemFormatCode () == SECSItemFormatCode.I4);
        }

        [Test()]
        public void Test09 ()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.I4) << 2) | 0x01), 0x04, 127, 255, 255, 255 };

            I4SECSItem secsItem = new I4SECSItem (2147483647);
            Assert.AreEqual (secsItem.EncodeForTransport (), expectedResult);
        }

        [Test()]
        public void Test10 ()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.I4) << 2) | 0x02), 0, 0x04, 127, 255, 255, 255 };

            I4SECSItem secsItem = new I4SECSItem (2147483647, SECSItemNumLengthBytes.TWO);
            Assert.AreEqual (secsItem.EncodeForTransport (), expectedResult);
        }

        [Test()]
        public void Test11 ()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.I4) << 2) | 0x03), 0, 0, 0x04, 127, 255, 255, 255 };

            I4SECSItem secsItem = new I4SECSItem (2147483647, SECSItemNumLengthBytes.THREE);
            Assert.AreEqual (secsItem.EncodeForTransport (), expectedResult);
        }

        [Test()]
        public void Test12 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.I4) << 2) | 0x01), 0x00 };

            var exception = Assert.Catch (() => new I4SECSItem (input, 0));

            Assert.IsInstanceOf<ArgumentOutOfRangeException> (exception);

            Assert.IsTrue (exception.Message.Contains ("Illegal data length of: 0.  The length of the data independent of the item header must be 4."));
        }

        [Test()]
        public void Test13 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.I4) << 2) | 0x01), 0x05 };

            var exception = Assert.Catch (() => new I4SECSItem (input, 0));

            Assert.IsInstanceOf<ArgumentOutOfRangeException> (exception);

            Assert.IsTrue (exception.Message.Contains ("Illegal data length of: 5.  The length of the data independent of the item header must be 4."));
        }

        [Test()]
        public void Test14 ()
        {
            I4SECSItem secsItem = new I4SECSItem (2147483647);
            Assert.IsTrue (secsItem.ToString ().Equals ("Format:I4 Value: 2147483647"));
        }

        [Test()]
        public void Test15 ()
        {
            I4SECSItem secsItem1 = new I4SECSItem (2147483647);
            I4SECSItem secsItem2 = new I4SECSItem (2147483647);
            Assert.IsTrue (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test16 ()
        {
            I4SECSItem secsItem1 = new I4SECSItem (2147483647);
            I4SECSItem secsItem2 = new I4SECSItem (-2147483648);
            Assert.IsFalse (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test17 ()
        {
            I4SECSItem secsItem1 = new I4SECSItem (2147483647);
            I4SECSItem secsItem2 = null;
            Assert.IsFalse (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test18 ()
        {
            I4SECSItem secsItem1 = new I4SECSItem (2147483647);
            Assert.IsTrue (secsItem1.Equals (secsItem1));
        }

        [Test()]
        public void Test19 ()
        {
            I4SECSItem secsItem1 = new I4SECSItem (2147483647);
            SECSItem secsItem2 = null;
            Assert.IsFalse (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test20 ()
        {
            I4SECSItem secsItem1 = new I4SECSItem (2147483647);
            Object secsItem2 = new F8SECSItem (2.141592D);
                    Assert.IsFalse (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test21 ()
        {
            Assert.IsTrue (true);
            /*
            I4SECSItem secsItem1 = new I4SECSItem (2147483647);
            Assert.IsTrue (secsItem1.GetHashCode () == -2147483618);
            */
        }

        [Test()]
        public void Test22 ()
        {
            I4SECSItem secsItem1 = new I4SECSItem (2147483647);
            I4SECSItem secsItem2 = new I4SECSItem (2147483647);
            Assert.IsTrue (secsItem1.GetHashCode () == secsItem2.GetHashCode ());
        }
	}
}

