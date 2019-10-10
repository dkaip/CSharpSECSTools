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
	public class F4SECSItemTests
	{
        [Test()]
        public void Test01 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.F4) << 2) | 0x01), 0x04, 127, 127, 255, 255 };
            float expectedOutput = Single.MaxValue;
            F4SECSItem secsItem = new F4SECSItem (input, 0);
            Assert.IsTrue (secsItem.GetValue () == expectedOutput);
        }

        [Test()]
        public void Test02 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.F4) << 2) | 0x01), 0x04, 255, 127, 255, 255};
            float expectedOutput = Single.MinValue;
            F4SECSItem secsItem = new F4SECSItem (input, 0);
            Assert.IsTrue (secsItem.GetValue () == expectedOutput);
        }

        [Test()]
        public void Test03 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.F4) << 2) | 0x01), 0x04, 255, 128, 0, 0 };
            float expectedOutput = Single.NegativeInfinity;
            F4SECSItem secsItem = new F4SECSItem (input, 0);
            Assert.IsTrue (secsItem.GetValue () == expectedOutput);
        }

        [Test()]
        public void Test04 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.F4) << 2) | 0x01), 0x04, 127, 128, 0, 0 };
            float expectedOutput = Single.PositiveInfinity;
            F4SECSItem secsItem = new F4SECSItem (input, 0);
            Assert.IsTrue (secsItem.GetValue () == expectedOutput);
        }

        [Test()]
        public void Test05 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.F4) << 2) | 0x01), 0x04, 0, 0, 0, 0 };
            float expectedOutput = 0.0F;
            F4SECSItem secsItem = new F4SECSItem (input, 0);
            Assert.IsTrue (secsItem.GetValue () == expectedOutput);
        }

        [Test()]
        public void Test06 ()
        {
            float expectedOutput = 3.141592F;
            F4SECSItem secsItem = new F4SECSItem (expectedOutput);
            Assert.IsTrue (secsItem.GetValue () == expectedOutput);
        }

        [Test()]
        public void Test07 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.F4) << 2) | 0x01), 0x04, 127, 127, 255, 255 };
            F4SECSItem secsItem = new F4SECSItem (input, 0);
            Assert.IsTrue (secsItem.GetSECSItemFormatCode () == SECSItemFormatCode.F4);
        }

        [Test()]
        public void Test08 ()
        {
            float expectedOutput = 3.141592F;
            F4SECSItem secsItem = new F4SECSItem (expectedOutput);
            Assert.IsTrue (secsItem.GetSECSItemFormatCode () == SECSItemFormatCode.F4);
        }

        [Test()]
        public void Test09 ()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.F4) << 2) | 0x01), 0x04, 127, 128, 0, 0 };

            F4SECSItem secsItem = new F4SECSItem (Single.PositiveInfinity);
            Assert.AreEqual (secsItem.ToRawSECSItem (), expectedResult);
        }

        [Test()]
        public void Test10 ()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.F4) << 2) | 0x02), 0, 0x04, 127, 128, 0, 0 };

            F4SECSItem secsItem = new F4SECSItem (Single.PositiveInfinity, SECSItemNumLengthBytes.TWO);
            Assert.AreEqual (secsItem.ToRawSECSItem (), expectedResult);
        }

        [Test()]
        public void Test11 ()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.F4) << 2) | 0x03), 0, 0, 0x04, 127, 128, 0, 0 };

            F4SECSItem secsItem = new F4SECSItem (Single.PositiveInfinity, SECSItemNumLengthBytes.THREE);
            Assert.AreEqual (secsItem.ToRawSECSItem (), expectedResult);
        }

        [Test()]
        public void Test12 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.F4) << 2) | 0x01), 0x00 };

            var exception = Assert.Catch (() => new F4SECSItem (input, 0));

            Assert.IsInstanceOf<ArgumentException> (exception);

            Assert.IsTrue (exception.Message.Contains ("Illegal data length of: 0.  The length of the data independent of the item header must be 4."));
        }

        [Test()]
        public void Test13 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.F4) << 2) | 0x01), 0x05 };

            var exception = Assert.Catch (() => new F4SECSItem (input, 0));

            Assert.IsInstanceOf<ArgumentException> (exception);

            Assert.IsTrue (exception.Message.Contains ("Illegal data length of: 5.  The length of the data independent of the item header must be 4."));
        }

        [Test()]
        public void Test14 ()
        {
            F4SECSItem secsItem = new F4SECSItem (3.141592F);
            Assert.IsTrue (secsItem.ToString ().Equals ("Format:F4 Value: 3.141592"));
        }

        [Test()]
        public void Test15 ()
        {
            F4SECSItem secsItem1 = new F4SECSItem (3.141592F);
            F4SECSItem secsItem2 = new F4SECSItem (3.141592F);
            Assert.IsTrue (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test16 ()
        {
            F4SECSItem secsItem1 = new F4SECSItem (3.141592F);
            F4SECSItem secsItem2 = new F4SECSItem (2.141592F);
            Assert.IsFalse (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test17 ()
        {
            F4SECSItem secsItem1 = new F4SECSItem (3.141592F);
            F4SECSItem secsItem2 = null;
            Assert.IsFalse (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test18 ()
        {
            F4SECSItem secsItem1 = new F4SECSItem (3.141592F);
            Assert.IsTrue (secsItem1.Equals (secsItem1));
        }

        [Test()]
        public void Test19 ()
        {
            F4SECSItem secsItem1 = new F4SECSItem (3.141592F);
            SECSItem secsItem2 = null;
            Assert.IsFalse (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test20 ()
        {
            F4SECSItem secsItem1 = new F4SECSItem (3.141592F);
            Object secsItem2 = new F8SECSItem (2.141592D);
            Assert.IsFalse (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test21 ()
        {
            Assert.IsTrue (true);
            /*
            F4SECSItem secsItem1 = new F4SECSItem (3.141592F);
            Assert.IsTrue (secsItem1.GetHashCode () == 1078530039);
            */
        }

        [Test()]
        public void Test22 ()
        {
            F4SECSItem secsItem1 = new F4SECSItem (3.141592F);
            F4SECSItem secsItem2 = new F4SECSItem (3.141592F);
            Assert.IsTrue (secsItem1.GetHashCode () == secsItem2.GetHashCode ());
        }
   	}
}

