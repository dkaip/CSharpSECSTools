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
	public class F8SECSItemTests
	{
        [Test()]
        public void Test01 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.F8) << 2) | 0x01), 0x08, 127, 0xEF, 255, 255, 255, 255, 255, 255 };
            double expectedOutput = Double.MaxValue;
            F8SECSItem secsItem = new F8SECSItem (input, 0);
            Assert.IsTrue (secsItem.GetValue () == expectedOutput);
        }

        [Test()]
        public void Test02 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.F8) << 2) | 0x01), 0x08, 255, 0XEF, 255, 255, 255, 255, 255, 255 };
            double expectedOutput = Double.MinValue;
            F8SECSItem secsItem = new F8SECSItem (input, 0);
            Assert.IsTrue (secsItem.GetValue () == expectedOutput);
        }

        [Test()]
        public void Test03 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.F8) << 2) | 0x01), 0x08, 255, 0xF0, 0, 0, 0, 0, 0, 0 };
            double expectedOutput = Double.NegativeInfinity;
            F8SECSItem secsItem = new F8SECSItem (input, 0);
            Assert.IsTrue (secsItem.GetValue () == expectedOutput);
        }

        [Test()]
        public void Test04 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.F8) << 2) | 0x01), 0x08, 127, 0xF0, 0, 0, 0, 0, 0, 0 };
            double expectedOutput = Double.PositiveInfinity;
            F8SECSItem secsItem = new F8SECSItem (input, 0);
            Assert.IsTrue (secsItem.GetValue () == expectedOutput);
        }

        [Test()]
        public void Test05 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.F8) << 2) | 0x01), 0x08, 0, 0, 0, 0, 0, 0, 0, 0 };
            double expectedOutput = 0.0D;
            F8SECSItem secsItem = new F8SECSItem (input, 0);
            Assert.IsTrue (secsItem.GetValue () == expectedOutput);
        }

        [Test()]
        public void Test06 ()
        {
            double expectedOutput = 3.141592D;
            F8SECSItem secsItem = new F8SECSItem (expectedOutput);
            Assert.IsTrue (secsItem.GetValue () == expectedOutput);
        }

        [Test()]
        public void Test07 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.F8) << 2) | 0x01), 0x08, 127, 0xEF, 255, 255, 255, 255, 255, 255 };
            F8SECSItem secsItem = new F8SECSItem (input, 0);
            Assert.IsTrue (secsItem.GetSECSItemFormatCode () == SECSItemFormatCode.F8);
        }

        [Test()]
        public void Test08 ()
        {
            double expectedOutput = 3.141592D;
            F8SECSItem secsItem = new F8SECSItem (expectedOutput);
            Assert.IsTrue (secsItem.GetSECSItemFormatCode () == SECSItemFormatCode.F8);
        }

        [Test()]
        public void Test09 ()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.F8) << 2) | 0x01), 0x08, 127, 0xF0, 0, 0, 0, 0, 0, 0 };

            F8SECSItem secsItem = new F8SECSItem (Double.PositiveInfinity);
            Assert.AreEqual (secsItem.ToRawSECSItem (), expectedResult);
        }

        [Test()]
        public void Test10 ()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.F8) << 2) | 0x02), 0, 0x08, 127, 0xF0, 0, 0, 0, 0, 0, 0 };

            F8SECSItem secsItem = new F8SECSItem (Double.PositiveInfinity, SECSItemNumLengthBytes.TWO);
            Assert.AreEqual (secsItem.ToRawSECSItem (), expectedResult);
        }

        [Test()]
        public void Test11 ()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.F8) << 2) | 0x03), 0, 0, 0x08, 127, 0xF0, 0, 0, 0, 0, 0, 0 };

            F8SECSItem secsItem = new F8SECSItem (Double.PositiveInfinity, SECSItemNumLengthBytes.THREE);
            Assert.AreEqual (secsItem.ToRawSECSItem (), expectedResult);
        }

        [Test()]
        public void Test12 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.F8) << 2) | 0x01), 0x00 };

            var exception = Assert.Catch (() => new F8SECSItem (input, 0));

            Assert.IsInstanceOf<ArgumentOutOfRangeException> (exception);

            Assert.IsTrue (exception.Message.Contains ("Illegal data length of: 0.  The length of the data independent of the item header must be 8."));
        }

        [Test()]
        public void Test13 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.F8) << 2) | 0x01), 0x09 };

            var exception = Assert.Catch (() => new F8SECSItem (input, 0));

            Assert.IsInstanceOf<ArgumentOutOfRangeException> (exception);

            Assert.IsTrue (exception.Message.Contains ("Illegal data length of: 9.  The length of the data independent of the item header must be 8."));
        }

        [Test()]
        public void Test14 ()
        {
            F8SECSItem secsItem = new F8SECSItem (3.141592D);
            Assert.IsTrue (secsItem.ToString ().Equals ("Format:F8 Value: 3.141592"));
        }

        [Test()]
        public void Test15 ()
        {
            F8SECSItem secsItem1 = new F8SECSItem (3.141592D);
            F8SECSItem secsItem2 = new F8SECSItem (3.141592D);
            Assert.IsTrue (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test16 ()
        {
            F8SECSItem secsItem1 = new F8SECSItem (3.141592D);
            F8SECSItem secsItem2 = new F8SECSItem (2.141592D);
            Assert.IsFalse (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test17 ()
        {
            F8SECSItem secsItem1 = new F8SECSItem (3.141592D);
            F8SECSItem secsItem2 = null;
            Assert.IsFalse (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test18 ()
        {
            F8SECSItem secsItem1 = new F8SECSItem (3.141592D);
            Assert.IsTrue (secsItem1.Equals (secsItem1));
        }

        [Test()]
        public void Test19 ()
        {
            F8SECSItem secsItem1 = new F8SECSItem (3.141592D);
            SECSItem secsItem2 = null;
            Assert.IsFalse (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test20 ()
        {
            F8SECSItem secsItem1 = new F8SECSItem (3.141592D);
            Object secsItem2 = new F4SECSItem (2.141592F);
            Assert.IsFalse (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test21 ()
        {
            Assert.IsTrue (true);
            /*
            F8SECSItem secsItem1 = new F8SECSItem (3.141592D);
            Assert.IsTrue (secsItem1.GetHashCode () == -1132322401);
            */
        }

        [Test()]
        public void Test22 ()
        {
            F8SECSItem secsItem1 = new F8SECSItem (3.141592D);
            F8SECSItem secsItem2 = new F8SECSItem (3.141592D);
            Assert.IsTrue (secsItem1.GetHashCode () == secsItem2.GetHashCode ());
        }
	}
}

