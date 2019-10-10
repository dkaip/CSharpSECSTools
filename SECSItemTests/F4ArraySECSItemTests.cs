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
	public class F4ArraySECSItemTests
	{
        [Test()]
        public void Test01 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F4 ) << 2) | 0x01), 21,
                127, 127, 255, 255,
                0, 0, 0, 1,
                255, 128, 0, 0,
                127, 128, 0, 0,
                0, 0, 0, 0,
                0 };

            var exception = Assert.Catch (() => new F4ArraySECSItem (input, 0));

            Assert.IsInstanceOf<ArgumentOutOfRangeException> (exception);

            Assert.IsTrue (exception.Message.Contains ("Illegal data length of: 21 payload length must be a non-zero multiple of 4."));
        }

        [Test()]
        public void Test02 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F4 ) << 2) | 0x01), 20,
                        127, 127, 255, 255,
                        255, 127, 255, 255,
                        255, 128, 0, 0,
                        127, 128, 0, 0,
                        0, 0, 0, 0 };
            F4ArraySECSItem secsItem = new F4ArraySECSItem (input, 0);
            Assert.IsTrue (secsItem.GetValue () [0] == Single.MaxValue);
            Assert.IsTrue (secsItem.GetValue () [1] == Single.MinValue);
            Assert.IsTrue (secsItem.GetValue () [2] == Single.NegativeInfinity);
            Assert.IsTrue (secsItem.GetValue () [3] == Single.PositiveInfinity);
            Assert.IsTrue (secsItem.GetValue () [4] == 0.0F);
        }

        [Test()]
        public void Test03 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F4 ) << 2) | 0x01), 20,
                        127, 127, 255, 255,
                        255, 127, 255, 255,
                        255, 128, 0, 0,
                        127, 128, 0, 0,
                        0, 0, 0, 0 };
            F4ArraySECSItem secsItem = new F4ArraySECSItem (input, 0);
            Assert.IsTrue (secsItem.GetSECSItemFormatCode () == SECSItemFormatCode.F4);
        }

        [Test()]
        public void Test04 ()
        {
            float [] input = { Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F };
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F4 ) << 2) | 0x01), 20,
                        127, 127, 255, 255,
                        255, 127, 255, 255,
                        255, 128, 0, 0,
                        127, 128, 0, 0,
                        0, 0, 0, 0 };

            F4ArraySECSItem secsItem = new F4ArraySECSItem (input);
            Assert.AreEqual (secsItem.ToRawSECSItem (), expectedResult);
        }

        [Test()]
        public void Test05 ()
        {
            float [] input = { Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F };
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F4 ) << 2) | 0x02), 0, 20,
                        127, 127, 255, 255,
                        255, 127, 255, 255,
                        255, 128, 0, 0,
                        127, 128, 0, 0,
                        0, 0, 0, 0 };

            F4ArraySECSItem secsItem = new F4ArraySECSItem (input, SECSItemNumLengthBytes.TWO);
            Assert.AreEqual (secsItem.ToRawSECSItem (), expectedResult);
        }

        [Test()]
        public void Test06 ()
        {
            float [] input = { Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F };
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F4 ) << 2) | 0x03), 0, 0, 20,
                        127, 127, 255, 255,
                        255, 127, 255, 255,
                        255, 128, 0, 0,
                        127, 128, 0, 0,
                        0, 0, 0, 0 };

            F4ArraySECSItem secsItem = new F4ArraySECSItem (input, SECSItemNumLengthBytes.THREE);
                    Assert.AreEqual (secsItem.ToRawSECSItem (), expectedResult);
        }

        [Test()]
        public void Test07 ()
        {
            float [] input = { Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F };

            F4ArraySECSItem secsItem = new F4ArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem.ToString ().Equals ("Format:F4 Value: Array"));
        }

        [Test()]
        public void Test08 ()
        {
            Assert.IsTrue (true);
            /*
            float [] input = { Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F };

            F4ArraySECSItem secsItem = new F4ArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem.GetHashCode () == 1361524124);
            */
        }

        [Test()]
        public void Test09 ()
        {
            float [] input = { Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F };

            F4ArraySECSItem secsItem = new F4ArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem.Equals (secsItem));
        }

        [Test()]
        public void Test10 ()
        {
            float [] input = { Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F };

            F4ArraySECSItem secsItem = new F4ArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Assert.IsFalse (secsItem.Equals (null));
        }

        [Test()]
        public void Test11 ()
        {
            float [] input = { Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F };

            F4ArraySECSItem secsItem1 = new F4ArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Object secsItem2 = new F8SECSItem (2.141592D);
                    Assert.IsFalse (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test12 ()
        {
            float [] input1 = { Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F };
            float [] input2 = { Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F };

            F4ArraySECSItem secsItem1 = new F4ArraySECSItem (input1, SECSItemNumLengthBytes.ONE);
            F4ArraySECSItem secsItem2 = new F4ArraySECSItem (input2, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test13 ()
        {
            float [] input1 = { Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F };
            float [] input2 = { Single.MaxValue, Single.MinValue, Single.NegativeInfinity, 0.0F, 0.0F };

            F4ArraySECSItem secsItem1 = new F4ArraySECSItem (input1, SECSItemNumLengthBytes.ONE);
            F4ArraySECSItem secsItem2 = new F4ArraySECSItem (input2, SECSItemNumLengthBytes.ONE);

            Assert.IsFalse (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test14 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.F4) << 2) | 0x01), 0x00 };

            var exception = Assert.Catch (() => new F4ArraySECSItem (input, 0));

            Assert.IsInstanceOf<ArgumentException> (exception);

            Assert.IsTrue (exception.Message.Contains ("Illegal data length of: 0 payload length must be a non-zero multiple of 4."));
        }

        [Test()]
        public void Test15 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.F4) << 2) | 0x01), 0x03 };

            var exception = Assert.Catch (() => new F4ArraySECSItem (input, 0));

            Assert.IsInstanceOf<ArgumentException> (exception);

            Assert.IsTrue (exception.Message.Contains ("Illegal data length of: 3 payload length must be a non-zero multiple of 4."));
        }
	}
}

