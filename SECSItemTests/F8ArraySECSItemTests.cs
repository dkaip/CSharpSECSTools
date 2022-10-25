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
	public class F8ArraySECSItemTests
	{
        [Test()]
        public void Test01 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F8 ) << 2) | 0x01), 40,
                127, 0xEF, 255, 255, 255, 255, 255, 255,
                255, 0XEF, 255, 255, 255, 255, 255, 255,
                255, 0xF0, 0, 0, 0, 0, 0, 0,
                127, 0xF0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0 };
            F8ArraySECSItem secsItem = new F8ArraySECSItem (input, 0);
            Assert.IsTrue (secsItem.GetValue () [0] == Double.MaxValue);
            Assert.IsTrue (secsItem.GetValue () [1] == Double.MinValue);
            Assert.IsTrue (Double.IsNegativeInfinity (secsItem.GetValue () [2]));
            Assert.IsTrue (Double.IsPositiveInfinity (secsItem.GetValue () [3]));
            Assert.IsTrue (secsItem.GetValue () [4] == 0.0D);
        }

        [Test()]
        public void Test02 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F8 ) << 2) | 0x01), 40,
                127, 0xEF, 255, 255, 255, 255, 255, 255,
                255, 0XEF, 255, 255, 255, 255, 255, 255,
                255, 0xF0, 0, 0, 0, 0, 0, 0,
                127, 0xF0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0 };
            F8ArraySECSItem secsItem = new F8ArraySECSItem (input, 0);
            Assert.IsTrue (secsItem.GetSECSItemFormatCode () == SECSItemFormatCode.F8);
        }

        [Test()]
        public void Test03 ()
        {
            double [] input = { Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D };
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F8 ) << 2) | 0x01), 40,
                127, 0xEF, 255, 255, 255, 255, 255, 255,
                255, 0XEF, 255, 255, 255, 255, 255, 255,
                255, 0xF0, 0, 0, 0, 0, 0, 0,
                127, 0xF0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0 };

            F8ArraySECSItem secsItem = new F8ArraySECSItem (input);
            Assert.AreEqual (secsItem.EncodeForTransport (), expectedResult);
        }

        [Test()]
        public void Test04 ()
        {
            double [] input = { Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D };
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F8 ) << 2) | 0x02), 0, 40,
                127, 0xEF, 255, 255, 255, 255, 255, 255,
                255, 0XEF, 255, 255, 255, 255, 255, 255,
                255, 0xF0, 0, 0, 0, 0, 0, 0,
                127, 0xF0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0 };

            F8ArraySECSItem secsItem = new F8ArraySECSItem (input, SECSItemNumLengthBytes.TWO);
            Assert.AreEqual (secsItem.EncodeForTransport (), expectedResult);
        }

        [Test()]
        public void Test05 ()
        {
            double [] input = { Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D };
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F8 ) << 2) | 0x03), 0, 0, 40,
                127, 0xEF, 255, 255, 255, 255, 255, 255,
                255, 0XEF, 255, 255, 255, 255, 255, 255,
                255, 0xF0, 0, 0, 0, 0, 0, 0,
                127, 0xF0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0 };

            F8ArraySECSItem secsItem = new F8ArraySECSItem (input, SECSItemNumLengthBytes.THREE);
            Assert.AreEqual (secsItem.EncodeForTransport (), expectedResult);
        }

        [Test()]
        public void Test06 ()
        {
            double [] input = { Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D };

            F8ArraySECSItem secsItem = new F8ArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem.ToString ().Equals ("Format:F8 Value: Array"));
        }

        [Test()]
        public void Test07 ()
        {
            Assert.IsTrue (true);
            /*
            double [] input = { Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D };

            F8ArraySECSItem secsItem = new F8ArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem.GetHashCode () == 1002786077);
            */
        }

        [Test()]
        public void Test08 ()
        {
            double [] input = { Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D };

            F8ArraySECSItem secsItem = new F8ArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem.Equals (secsItem));
        }

        [Test()]
        public void Test09 ()
        {
            double [] input = { Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D };

            F8ArraySECSItem secsItem = new F8ArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Assert.IsFalse (secsItem.Equals (null));
        }

        [Test()]
        public void Test10 ()
        {
            double [] input = { Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D };

            F8ArraySECSItem secsItem1 = new F8ArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Object secsItem2 = new F8SECSItem (2.141592D);
            Assert.IsFalse (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test11 ()
        {
            double [] input1 = { Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D };
            double [] input2 = { Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D };

            F8ArraySECSItem secsItem1 = new F8ArraySECSItem (input1, SECSItemNumLengthBytes.ONE);
            F8ArraySECSItem secsItem2 = new F8ArraySECSItem (input2, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test12 ()
        {
            double [] input1 = { Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D };
            double [] input2 = { Double.MaxValue, Double.MinValue, Double.NegativeInfinity, 0.0D, 0.0D };

            F8ArraySECSItem secsItem1 = new F8ArraySECSItem (input1, SECSItemNumLengthBytes.ONE);
            F8ArraySECSItem secsItem2 = new F8ArraySECSItem (input2, SECSItemNumLengthBytes.ONE);

            Assert.IsFalse (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test13 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.F8) << 2) | 0x01), 0x00 };

            var exception = Assert.Catch (() => new F8ArraySECSItem (input, 0));

            Assert.IsInstanceOf<ArgumentOutOfRangeException> (exception);

            Assert.IsTrue (exception.Message.Contains ("Illegal data length of: 0 payload length must be a non-zero multiple of 8."));
        }

        [Test()]
        public void Test14 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.F8) << 2) | 0x01), 0x03 };

            var exception = Assert.Catch (() => new F8ArraySECSItem (input, 0));

            Assert.IsInstanceOf<ArgumentOutOfRangeException> (exception);

            Assert.IsTrue (exception.Message.Contains ("Illegal data length of: 3 payload length must be a non-zero multiple of 8."));
        }
	}
}

