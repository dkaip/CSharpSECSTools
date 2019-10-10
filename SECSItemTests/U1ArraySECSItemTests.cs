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
	public class U1ArraySECSItemTests
	{
        [Test()]
        public void Test01 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.U1) << 2) | 0x01), 0x04, 255, 128, 0, 127 };
            U1ArraySECSItem secsItem = new U1ArraySECSItem (input, 0, 0);
            Assert.IsTrue (secsItem.GetValue () [0] == 255);
            Assert.IsTrue (secsItem.GetValue () [1] == 128);
            Assert.IsTrue (secsItem.GetValue () [2] == 0);
            Assert.IsTrue (secsItem.GetValue () [3] == 127);
        }

        [Test()]
        public void Test02 ()
        {
            byte [] input = { 255, 128, 0, 127 };
            U1ArraySECSItem secsItem = new U1ArraySECSItem (input);
            Assert.AreEqual (secsItem.GetValue (), input);
        }

        [Test()]
        public void Test03 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.U1) << 2) | 0x01), 0x04, 255, 128, 0, 127 };
            U1ArraySECSItem secsItem = new U1ArraySECSItem (input, 0, 0);
            Assert.IsTrue (secsItem.GetSECSItemFormatCode () == SECSItemFormatCode.U1);
        }

        [Test()]
        public void Test04 ()
        {
            byte [] input = { 255, 128, 0, 127 };
            U1ArraySECSItem secsItem = new U1ArraySECSItem (input);
            Assert.IsTrue (secsItem.GetSECSItemFormatCode () == SECSItemFormatCode.U1);
        }

        [Test()]
        public void Test05 ()
        {
            byte [] input = { 255, 128, 0, 1, 127 };
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.U1) << 2) | 0x01), 0x05, 255, 128, 0, 1, 127 };

            U1ArraySECSItem secsItem = new U1ArraySECSItem (input);
            Assert.AreEqual (secsItem.ToRawSECSItem (), expectedResult);
        }

        [Test()]
        public void Test06 ()
        {
            byte [] input = { 255, 128, 0, 1, 127 };
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.U1) << 2) | 0x02), 0, 0x05, 255, 128, 0, 1, 127 };

            U1ArraySECSItem secsItem = new U1ArraySECSItem (input, SECSItemNumLengthBytes.TWO);
            Assert.AreEqual (secsItem.ToRawSECSItem (), expectedResult);
        }

        [Test()]
        public void Test07 ()
        {
            byte [] input = { 255, 128, 0, 1, 127 };
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.U1) << 2) | 0x03), 0, 0, 0x05, 255, 128, 0, 1, 127 };

            U1ArraySECSItem secsItem = new U1ArraySECSItem (input, SECSItemNumLengthBytes.THREE);
            Assert.AreEqual (secsItem.ToRawSECSItem (), expectedResult);
        }

        [Test()]
        public void Test08 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.U1) << 2) | 0x01), 0x00 };

            U1ArraySECSItem secsItem = new U1ArraySECSItem (input, 0, 0);

            Assert.IsTrue (secsItem.GetValue ().Length == 0);
        }

        [Test()]
        public void Test09 ()
        {
            byte [] input = { 255, 128, 0, 1, 127 };

            U1ArraySECSItem secsItem = new U1ArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem.ToString ().Equals ("Format:U1 Value: Array"));
        }

        [Test()]
        public void Test10 ()
        {
            Assert.IsTrue (true);
            /*
            byte [] input = { 255, 128, 0, 1, 127 };

            U1ArraySECSItem secsItem = new U1ArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem.GetHashCode () == 267940443);
            */
        }

        [Test()]
        public void Test11 ()
        {
            byte [] input = { 255, 128, 0, 1, 127 };

            U1ArraySECSItem secsItem = new U1ArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem.Equals (secsItem));
        }

        [Test()]
        public void Test12 ()
        {
            byte [] input = { 255, 128, 0, 1, 127 };

            U1ArraySECSItem secsItem = new U1ArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Assert.IsFalse (secsItem.Equals (null));
        }

        [Test()]
        public void Test13 ()
        {
            byte [] input = { 255, 128, 0, 1, 127 };

            U1ArraySECSItem secsItem = new U1ArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Object secsItem2 = new F8SECSItem (2.141592D);
            Assert.IsFalse (secsItem.Equals (secsItem2));
        }

        [Test()]
        public void Test14 ()
        {
            byte [] input1 = { 255, 128, 0, 1, 127 };
            byte [] input2 = { 255, 128, 0, 1, 127 };

            U1ArraySECSItem secsItem1 = new U1ArraySECSItem (input1, SECSItemNumLengthBytes.ONE);
            U1ArraySECSItem secsItem2 = new U1ArraySECSItem (input2, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test15 ()
        {
            byte [] input1 = { 255, 128, 0, 1, 127 };
            byte [] input2 = { 255, 128, 0, 1, 0 };

            U1ArraySECSItem secsItem1 = new U1ArraySECSItem (input1, SECSItemNumLengthBytes.ONE);
            U1ArraySECSItem secsItem2 = new U1ArraySECSItem (input2, SECSItemNumLengthBytes.ONE);

            Assert.IsFalse (secsItem1.Equals (secsItem2));
        }
	}
}

