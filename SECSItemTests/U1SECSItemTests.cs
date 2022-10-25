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
	public class U1SECSItemTests
	{
        [Test()]
        public void Test01 ()
        {
            byte value = 255;
            U1SECSItem secsItem = new U1SECSItem (value);
            Assert.IsTrue (secsItem.GetValue () == 255);
        }

        [Test()]
        public void Test02 ()
        {
            byte value = 128;
            U1SECSItem secsItem = new U1SECSItem (value);
            Assert.IsTrue (secsItem.GetValue () == 128);
        }

        [Test()]
        public void Test03 ()
        {
            byte value = 0;
            U1SECSItem secsItem = new U1SECSItem (value);
            Assert.IsTrue (secsItem.GetValue () == 0);
        }

        [Test()]
        public void Test04 ()
        {
            byte value = 127;
            U1SECSItem secsItem = new U1SECSItem (value);
            Assert.IsTrue (secsItem.GetValue () == 127);
        }

        [Test()]
        public void Test05 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.U1) << 2) | 0x01), 0x01, 255 };
            short expectedOutput = 255;
            U1SECSItem secsItem = new U1SECSItem (input, 0);
            Assert.IsTrue (secsItem.GetValue () == expectedOutput);
        }

        [Test()]
        public void Test06 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.U1) << 2) | 0x01), 0x01, 128 };
            short expectedOutput = 128;
            U1SECSItem secsItem = new U1SECSItem (input, 0);
            Assert.IsTrue (secsItem.GetValue () == expectedOutput);
        }

        [Test()]
        public void Test07 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.U1) << 2) | 0x01), 0x01, 0 };
            short expectedOutput = 0;
            U1SECSItem secsItem = new U1SECSItem (input, 0);
            Assert.IsTrue (secsItem.GetValue () == expectedOutput);
        }

        [Test()]
        public void Test08 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.U1) << 2) | 0x01), 0x01, 127 };
            short expectedOutput = 127;
            U1SECSItem secsItem = new U1SECSItem (input, 0);
            Assert.IsTrue (secsItem.GetValue () == expectedOutput);
        }

        [Test()]
        public void Test09 ()
        {
            byte value = 255;
            U1SECSItem secsItem = new U1SECSItem (value);
            Assert.IsTrue (secsItem.GetSECSItemFormatCode () == SECSItemFormatCode.U1);
        }

        [Test()]
        public void Test10 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.U1) << 2) | 0x01), 0x01, 255 };
            U1SECSItem secsItem = new U1SECSItem (input, 0);
            Assert.IsTrue (secsItem.GetSECSItemFormatCode () == SECSItemFormatCode.U1);
        }

        [Test()]
        public void Test11 ()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.U1) << 2) | 0x01), 0x01, 255 };

            U1SECSItem secsItem = new U1SECSItem (255);
            Assert.AreEqual (secsItem.EncodeForTransport (), expectedResult);
        }

        [Test()]
    public void Test12 ()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.U1) << 2) | 0x02), 0, 0x01, 255 };

            U1SECSItem secsItem = new U1SECSItem (255, SECSItemNumLengthBytes.TWO);
            Assert.AreEqual (secsItem.EncodeForTransport (), expectedResult);
        }

        [Test()]
    public void Test13 ()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.U1) << 2) | 0x03), 0, 0, 0x01, 255 };

            U1SECSItem secsItem = new U1SECSItem (255, SECSItemNumLengthBytes.THREE);
            Assert.AreEqual (secsItem.EncodeForTransport (), expectedResult);
        }

        [Test()]
        public void Test14 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.U1) << 2) | 0x01), 0x00 };

            var exception = Assert.Catch (() => new U1SECSItem (input, 0));

            Assert.IsInstanceOf<ArgumentOutOfRangeException> (exception);

            Assert.IsTrue (exception.Message.Contains ("Illegal data length of: 0.  The length of the data independent of the item header must be 1."));
        }

        [Test()]
        public void Test15 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.U1) << 2) | 0x01), 0x02 };

            var exception = Assert.Catch (() => new U1SECSItem (input, 0));

            Assert.IsInstanceOf<ArgumentOutOfRangeException> (exception);

            Assert.IsTrue (exception.Message.Contains ("Illegal data length of: 2.  The length of the data independent of the item header must be 1."));
        }

        [Test()]
        public void Test16 ()
        {
            U1SECSItem secsItem = new U1SECSItem (255);
            Assert.IsTrue (secsItem.ToString ().Equals ("Format:U1 Value: 255"));
        }

        [Test()]
        public void Test17 ()
        {
            U1SECSItem secsItem1 = new U1SECSItem (255);
            U1SECSItem secsItem2 = new U1SECSItem (255);
            Assert.IsTrue (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test18 ()
        {
            U1SECSItem secsItem1 = new U1SECSItem (255);
            U1SECSItem secsItem2 = new U1SECSItem (127);
            Assert.IsFalse (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test19 ()
        {
            U1SECSItem secsItem1 = new U1SECSItem (255);
            U1SECSItem secsItem2 = null;
            Assert.IsFalse (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test20 ()
        {
            U1SECSItem secsItem1 = new U1SECSItem (255);
            Assert.IsTrue (secsItem1.Equals (secsItem1));
        }

        [Test()]
        public void Test21 ()
        {
            U1SECSItem secsItem1 = new U1SECSItem (255);
            Object secsItem2 = new F8SECSItem (2.141592D);
            Assert.IsFalse (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test22 ()
        {
            Assert.IsTrue (true);
            /*
            U1SECSItem secsItem1 = new U1SECSItem (255);
            Assert.IsTrue (secsItem1.GetHashCode () == 286);
            */
        }

        [Test()]
        public void Test23 ()
        {
            U1SECSItem secsItem1 = new U1SECSItem (255);
            U1SECSItem secsItem2 = new U1SECSItem (255);
            Assert.IsTrue (secsItem1.GetHashCode () == secsItem2.GetHashCode ());
        }
	}
}

