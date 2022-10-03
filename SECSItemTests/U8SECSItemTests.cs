/*
 * Copyright 2019 Douglas Kaip
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
	public class U8SECSItemTests
	{
        [Test()]
        public void Test01 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.U8) << 2) | 0x01), 0x08, 255, 255, 255, 255, 255, 255, 255, 255 };
            UInt64 expectedOutput = 0xFFFFFFFFFFFFFFFF;
            U8SECSItem secsItem = new U8SECSItem (input, 0);
            Assert.IsTrue (secsItem.GetValue () == expectedOutput);
        }

        [Test()]
        public void Test02 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.U8) << 2) | 0x01), 0x08, 128, 0, 0, 0, 0, 0, 0, 0 };
            UInt64 expectedOutput = 0x8000000000000000;
            U8SECSItem secsItem = new U8SECSItem (input, 0);
            Assert.IsTrue (secsItem.GetValue () == expectedOutput);
        }

        [Test()]
        public void Test03 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.U8) << 2) | 0x01), 0x08, 0, 0, 0, 0, 0, 0, 0, 0 };
            UInt64 expectedOutput = 0x0000000000000000;
            U8SECSItem secsItem = new U8SECSItem (input, 0);
            Assert.IsTrue (secsItem.GetValue () == expectedOutput);
        }

        [Test()]
        public void Test04 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.U8) << 2) | 0x01), 0x08, 0, 0, 0, 0, 0, 0, 0, 1 };
            UInt64 expectedOutput = 0x0000000000000001;
            U8SECSItem secsItem = new U8SECSItem (input, 0);
            Assert.IsTrue (secsItem.GetValue () == expectedOutput);
        }

        [Test()]
        public void Test05 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.U8) << 2) | 0x01), 0x08, 127, 255, 255, 255, 255, 255, 255, 255 };
            UInt64 expectedOutput = 0x7FFFFFFFFFFFFFFF;
            U8SECSItem secsItem = new U8SECSItem (input, 0);
            Assert.IsTrue (secsItem.GetValue () == expectedOutput);
        }

        [Test()]
        public void Test06 ()
        {
            UInt64 expectedOutput = 0xFFFFFFFFFFFFFFFF;
            U8SECSItem secsItem = new U8SECSItem (expectedOutput);
            Assert.IsTrue (secsItem.GetValue () == expectedOutput);
        }

        [Test()]
        public void Test07 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.U8) << 2) | 0x01), 0x08, 255, 255, 255, 255, 255, 255, 255, 255 };
            U8SECSItem secsItem = new U8SECSItem (input, 0);
            Assert.IsTrue (secsItem.GetSECSItemFormatCode () == SECSItemFormatCode.U8);
        }

        [Test()]
        public void Test08 ()
        {
            UInt64 expectedOutput = 0xFFFFFFFFFFFFFFFF;
            U8SECSItem secsItem = new U8SECSItem (expectedOutput);
            Assert.IsTrue (secsItem.GetSECSItemFormatCode () == SECSItemFormatCode.U8);
        }

        [Test()]
        public void Test09 ()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.U8) << 2) | 0x01), 0x08, 127, 255, 255, 255, 255, 255, 255, 255 };

            U8SECSItem secsItem = new U8SECSItem (0x7FFFFFFFFFFFFFFF);
            Assert.AreEqual (secsItem.ToRawSECSItem (), expectedResult);
        }

        [Test()]
        public void Test10 ()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.U8) << 2) | 0x02), 0, 0x08, 255, 255, 255, 255, 255, 255, 255, 255 };

            U8SECSItem secsItem = new U8SECSItem (0xFFFFFFFFFFFFFFFF, SECSItemNumLengthBytes.TWO);
            Assert.AreEqual (secsItem.ToRawSECSItem (), expectedResult);
        }

        [Test()]
    public void Test11 ()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.U8) << 2) | 0x03), 0, 0, 0x08, 127, 1, 0, 0, 0, 0, 255, 255 };

            U8SECSItem secsItem = new U8SECSItem (0x7F0100000000FFFF, SECSItemNumLengthBytes.THREE);
            Assert.AreEqual (secsItem.ToRawSECSItem (), expectedResult);
        }

        [Test()]
        public void Test12 ()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.U8) << 2) | 0x03), 0, 0, 0x08, 0, 0, 0, 0, 0, 0, 0, 1 };

            U8SECSItem secsItem = new U8SECSItem (0x0000000000000001, SECSItemNumLengthBytes.THREE);
            Assert.AreEqual (secsItem.ToRawSECSItem (), expectedResult);
        }

        [Test()]
        public void Test13 ()
        {
            UInt64 bigInt = 0xFFFFFFFFFFFFFFFF;
            U8SECSItem secsItem = new U8SECSItem (bigInt);

            Assert.IsTrue (secsItem.ToString ().Equals ("Format:U8 Value: 18446744073709551615"));
        }

        [Test()]
        public void Test14 ()
        {
            UInt64 bigInt = 0x0000000000000000;
            U8SECSItem secsItem = new U8SECSItem (bigInt);

            Assert.IsTrue (secsItem.ToString ().Equals ("Format:U8 Value: 0"));
        }

        [Test()]
        public void Test15 ()
        {
            U8SECSItem secsItem1 = new U8SECSItem (3141592);
            U8SECSItem secsItem2 = new U8SECSItem (3141592);
            Assert.IsTrue (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test16 ()
        {
            U8SECSItem secsItem1 = new U8SECSItem (3141592);
            U8SECSItem secsItem2 = new U8SECSItem (2141592);
            Assert.IsFalse (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test17 ()
        {
            U8SECSItem secsItem1 = new U8SECSItem (3141592);
            U8SECSItem secsItem2 = null;
            Assert.IsFalse (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test18 ()
        {
            U8SECSItem secsItem1 = new U8SECSItem (3141592);
            Assert.IsTrue (secsItem1.Equals (secsItem1));
        }

        [Test()]
        public void Test19 ()
        {
            U8SECSItem secsItem1 = new U8SECSItem (3141592);
            SECSItem secsItem2 = null;
            Assert.IsFalse (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test20 ()
        {
            U8SECSItem secsItem1 = new U8SECSItem (3141592);
            Object secsItem2 = new F8SECSItem (2.141592D);
            Assert.IsFalse (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test21 ()
        {
            U8SECSItem secsItem1 = new U8SECSItem (0xFFFFFFFFFFFFFFFF);
            Assert.IsTrue (secsItem1.GetHashCode () == 0);
        }

        [Test()]
        public void Test22 ()
        {
            U8SECSItem secsItem1 = new U8SECSItem (0xFFFFFFFFFFFFFFFF);
            U8SECSItem secsItem2 = new U8SECSItem (0xFFFFFFFFFFFFFFFF);
//            Console.WriteLine ("Test22 {0} {1}", secsItem1.GetHashCode (), secsItem2.GetHashCode ());
            Assert.IsTrue (secsItem1.GetHashCode () == secsItem2.GetHashCode ());
        }

        [Test()]
        public void Test23 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.U8) << 2) | 0x01), 0x00 };

            var exception = Assert.Catch (() => new U8SECSItem (input, 0));

            Assert.IsInstanceOf<ArgumentOutOfRangeException> (exception);

            Assert.IsTrue (exception.Message.Contains ("Illegal data length of: 0.  The length of the data independent of the item header must be 8."));
        }

        [Test()]
        public void Test24 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.U8) << 2) | 0x01), 0x09 };

            var exception = Assert.Catch (() => new U8SECSItem (input, 0));

            Assert.IsInstanceOf<ArgumentOutOfRangeException> (exception);

            Assert.IsTrue (exception.Message.Contains ("Illegal data length of: 9.  The length of the data independent of the item header must be 8."));
        }
	}
}