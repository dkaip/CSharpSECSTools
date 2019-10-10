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
using NUnit.Framework;
using System;
using System.Collections.Generic;
using com.CIMthetics.CSharpSECSTools.SECSItems;

namespace SECSItemTests
{
    [TestFixture ()]
    public class RawSECSDataTests
    {
        [Test ()]
        public void Test01 ()
        {
            byte [] input = {
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.L ) << 2) | 0x01), 25,

                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.A ) << 2) | 0x02), 0x00, 0x03, 0x41, 0x42, 0x43,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.B ) << 2) | 0x03), 0x00, 0x00, 0x05, 128, 255, 0, 1, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x01), 0x08, 1, 0, 2, 0, 255, 0, 1, 1,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x01), 0x01, 5,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F4 ) << 2) | 0x01), 20,
                127, 127, 255, 255,
                255, 127, 255, 255,
                255, 128, 0, 0,
                127, 128, 0, 0,
                0, 0, 0, 0,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F4 ) << 2) | 0x01), 0x04, 127, 127, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F8 ) << 2) | 0x01), 40,
                127, 0xEF, 255, 255, 255, 255, 255, 255,
                255, 0XEF, 255, 255, 255, 255, 255, 255,
                255, 0xF0, 0, 0, 0, 0, 0, 0,
                127, 0xF0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F8 ) << 2) | 0x01), 0x08, 127, 0xEF, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1 ) << 2) | 0x01), 0x04, 255, 128, 0, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1 ) << 2) | 0x01), 0x01, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2 ) << 2) | 0x01), 10,
                255, 255,
                128, 0,
                0, 0,
                0, 1,
                127, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2 ) << 2) | 0x01), 0x02, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x01), 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x01), 0x04, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8 ) << 2) | 0x01), 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8 ) << 2) | 0x01), 0x08, 255, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U1 ) << 2) | 0x01), 0x04, 255, 128, 0, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U1 ) << 2) | 0x01), 0x01, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2 ) << 2) | 0x01), 10,
                255, 255,
                128, 0,
                0, 0,
                0, 1,
                127, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2 ) << 2) | 0x01), 0x02, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4 ) << 2) | 0x01), 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4 ) << 2) | 0x01), 0x04, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U8 ) << 2) | 0x01), 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U8 ) << 2) | 0x01), 0x08, 255, 255, 255, 255, 255, 255, 255, 255,

                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.L ) << 2) | 0x01), 24,

                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.A ) << 2) | 0x02), 0x00, 0x03, 0x41, 0x42, 0x43,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.B ) << 2) | 0x03), 0x00, 0x00, 0x05, 128, 255, 0, 1, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x01), 0x08, 1, 0, 2, 0, 255, 0, 1, 1,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x01), 0x01, 5,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F4 ) << 2) | 0x01), 20,
                127, 127, 255, 255,
                255, 127, 255, 255,
                255, 128, 0, 0,
                127, 128, 0, 0,
                0, 0, 0, 0,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F4 ) << 2) | 0x01), 0x04, 127, 127, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F8 ) << 2) | 0x01), 40,
                127, 0xEF, 255, 255, 255, 255, 255, 255,
                255, 0XEF, 255, 255, 255, 255, 255, 255,
                255, 0xF0, 0, 0, 0, 0, 0, 0,
                127, 0xF0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F8 ) << 2) | 0x01), 0x08, 127, 0xEF, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1 ) << 2) | 0x01), 0x04, 255, 128, 0, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1 ) << 2) | 0x01), 0x01, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2 ) << 2) | 0x01), 10,
                255, 255,
                128, 0,
                0, 0,
                0, 1,
                127, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2 ) << 2) | 0x01), 0x02, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x01), 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x01), 0x04, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8 ) << 2) | 0x01), 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8 ) << 2) | 0x01), 0x08, 255, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U1 ) << 2) | 0x01), 0x04, 255, 128, 0, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U1 ) << 2) | 0x01), 0x01, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2 ) << 2) | 0x01), 10,
                255, 255,
                128, 0,
                0, 0,
                0, 1,
                127, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2 ) << 2) | 0x01), 0x02, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4 ) << 2) | 0x01), 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4 ) << 2) | 0x01), 0x04, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U8 ) << 2) | 0x01), 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U8 ) << 2) | 0x01), 0x08, 255, 255, 255, 255, 255, 255, 255, 255

                };
            ListSECSItem secsItem = new ListSECSItem (input, 0);

            SECSItem testElement = null;
            LinkedList<SECSItem> expectedData1 = new LinkedList<SECSItem> ();
            LinkedList<SECSItem> expectedData2 = new LinkedList<SECSItem> ();
            testElement = new ASCIISECSItem ("ABC", SECSItemNumLengthBytes.TWO);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new BinarySECSItem (new byte [] { 128, 255, 0, 1, 127 }, SECSItemNumLengthBytes.THREE);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new BooleanArraySECSItem (new bool [] { true, false, true, false, true, false, true, true });
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new BooleanSECSItem (true);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new F4ArraySECSItem (new float [] { Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F });
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new F4SECSItem (Single.MaxValue);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new F8ArraySECSItem (new double [] { Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D });
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new F8SECSItem (Double.MaxValue);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new I1ArraySECSItem (new sbyte [] { -1, -128, 0, 127 });
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new I1SECSItem (-1);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new I2ArraySECSItem (new Int16 [] { -1, -32768, 0, 1, 32767 });
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new I2SECSItem (-1);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new I4ArraySECSItem (new Int32 [] { -1, -2147483648, 0, 1, 2147483647 });
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new I4SECSItem (-1);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new I8ArraySECSItem (new Int64 [] { -1, -9223372036854775808L, 0, 1, 9223372036854775807L });
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new I8SECSItem (-1);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new U1ArraySECSItem (new byte [] { 255, 128, 0, 127 });
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new U1SECSItem (255);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new U2ArraySECSItem (new UInt16 [] { 65535, 32768, 0, 1, 32767 });
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new U2SECSItem (65535);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new U4ArraySECSItem (new UInt32 [] { 0xFFFFFFFF, 2147483648, 0, 1, 2147483647 });
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new U4SECSItem (0xFFFFFFFF);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new U8ArraySECSItem (new UInt64 [] { 0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000, 0x0000000000000001, 0x7FFFFFFFFFFFFFFF });
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new U8SECSItem (0xFFFFFFFFFFFFFFFF);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);

            ListSECSItem innerList = new ListSECSItem (expectedData2);
            expectedData1.AddLast (innerList);

            ListSECSItem expectedResult = new ListSECSItem (expectedData1);

            RawSECSData rawData1 = new RawSECSData (secsItem);
            RawSECSData rawData2 = new RawSECSData (expectedResult);

            Assert.AreEqual (rawData1.GetData (), rawData2.GetData ());
        }

        [Test ()]
        public void Test02 ()
        {
            byte [] input = {
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.L ) << 2) | 0x01), 25,

                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.A ) << 2) | 0x02), 0x00, 0x03, 0x41, 0x42, 0x43,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.B ) << 2) | 0x03), 0x00, 0x00, 0x05, 128, 255, 0, 1, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x01), 0x08, 1, 0, 2, 0, 255, 0, 1, 1,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x01), 0x01, 5,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F4 ) << 2) | 0x01), 20,
                127, 127, 255, 255,
                0, 0, 0, 1,
                255, 128, 0, 0,
                127, 128, 0, 0,
                0, 0, 0, 0,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F4 ) << 2) | 0x01), 0x04, 127, 127, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F8 ) << 2) | 0x01), 40,
                127, 0xEF, 255, 255, 255, 255, 255, 255,
                0, 0, 0, 0, 0, 0, 0, 1,
                255, 0xF0, 0, 0, 0, 0, 0, 0,
                127, 0xF0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F8 ) << 2) | 0x01), 0x08, 127, 0xEF, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1 ) << 2) | 0x01), 0x04, 255, 128, 0, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1 ) << 2) | 0x01), 0x01, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2 ) << 2) | 0x01), 10,
                255, 255,
                128, 0,
                0, 0,
                0, 1,
                127, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2 ) << 2) | 0x01), 0x02, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x01), 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x01), 0x04, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8 ) << 2) | 0x01), 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8 ) << 2) | 0x01), 0x08, 255, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U1 ) << 2) | 0x01), 0x04, 255, 128, 0, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U1 ) << 2) | 0x01), 0x01, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2 ) << 2) | 0x01), 10,
                255, 255,
                128, 0,
                0, 0,
                0, 1,
                127, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2 ) << 2) | 0x01), 0x02, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4 ) << 2) | 0x01), 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4 ) << 2) | 0x01), 0x04, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U8 ) << 2) | 0x01), 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U8 ) << 2) | 0x01), 0x08, 255, 255, 255, 255, 255, 255, 255, 255,

                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.L ) << 2) | 0x01), 24,

                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.A ) << 2) | 0x02), 0x00, 0x03, 0x41, 0x42, 0x43,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.B ) << 2) | 0x03), 0x00, 0x00, 0x05, 128, 255, 0, 1, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x01), 0x08, 1, 0, 2, 0, 255, 0, 1, 1,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x01), 0x01, 5,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F4 ) << 2) | 0x01), 20,
                127, 127, 255, 255,
                0, 0, 0, 1,
                255, 128, 0, 0,
                127, 128, 0, 0,
                0, 0, 0, 0,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F4 ) << 2) | 0x01), 0x04, 127, 127, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F8 ) << 2) | 0x01), 40,
                127, 0xEF, 255, 255, 255, 255, 255, 255,
                0, 0, 0, 0, 0, 0, 0, 1,
                255, 0xF0, 0, 0, 0, 0, 0, 0,
                127, 0xF0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F8 ) << 2) | 0x01), 0x08, 127, 0xEF, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1 ) << 2) | 0x01), 0x04, 255, 128, 0, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1 ) << 2) | 0x01), 0x01, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2 ) << 2) | 0x01), 10,
                255, 255,
                128, 0,
                0, 0,
                0, 1,
                127, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2 ) << 2) | 0x01), 0x02, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x01), 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x01), 0x04, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8 ) << 2) | 0x01), 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8 ) << 2) | 0x01), 0x08, 255, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U1 ) << 2) | 0x01), 0x04, 255, 128, 0, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U1 ) << 2) | 0x01), 0x01, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2 ) << 2) | 0x01), 10,
                255, 255,
                128, 0,
                0, 0,
                0, 1,
                127, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2 ) << 2) | 0x01), 0x02, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4 ) << 2) | 0x01), 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4 ) << 2) | 0x01), 0x04, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U8 ) << 2) | 0x01), 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U8 ) << 2) | 0x01), 0x08, 255, 255, 255, 255, 255, 255, 255, 255

                };

            RawSECSData rawData1 = new RawSECSData (input);

            Assert.AreEqual (rawData1.GetData (), input);
        }

        [Test ()]
        public void Test03 ()
        {
            byte [] input = {
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.L ) << 2) | 0x01), 25,

                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.A ) << 2) | 0x02), 0x00, 0x03, 0x41, 0x42, 0x43,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.B ) << 2) | 0x03), 0x00, 0x00, 0x05, 128, 255, 0, 1, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x01), 0x08, 1, 0, 2, 0, 255, 0, 1, 1,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x01), 0x01, 5,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F4 ) << 2) | 0x01), 20,
                127, 127, 255, 255,
                255, 127, 255, 255,
                255, 128, 0, 0,
                127, 128, 0, 0,
                0, 0, 0, 0,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F4 ) << 2) | 0x01), 0x04, 127, 127, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F8 ) << 2) | 0x01), 40,
                127, 0xEF, 255, 255, 255, 255, 255, 255,
                255, 0XEF, 255, 255, 255, 255, 255, 255,
                255, 0xF0, 0, 0, 0, 0, 0, 0,
                127, 0xF0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F8 ) << 2) | 0x01), 0x08, 127, 0xEF, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1 ) << 2) | 0x01), 0x04, 255, 128, 0, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1 ) << 2) | 0x01), 0x01, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2 ) << 2) | 0x01), 10,
                255, 255,
                128, 0,
                0, 0,
                0, 1,
                127, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2 ) << 2) | 0x01), 0x02, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x01), 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x01), 0x04, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8 ) << 2) | 0x01), 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8 ) << 2) | 0x01), 0x08, 255, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U1 ) << 2) | 0x01), 0x04, 255, 128, 0, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U1 ) << 2) | 0x01), 0x01, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2 ) << 2) | 0x01), 10,
                255, 255,
                128, 0,
                0, 0,
                0, 1,
                127, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2 ) << 2) | 0x01), 0x02, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4 ) << 2) | 0x01), 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4 ) << 2) | 0x01), 0x04, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U8 ) << 2) | 0x01), 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U8 ) << 2) | 0x01), 0x08, 255, 255, 255, 255, 255, 255, 255, 255,

                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.L ) << 2) | 0x01), 24,

                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.A ) << 2) | 0x02), 0x00, 0x03, 0x41, 0x42, 0x43,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.B ) << 2) | 0x03), 0x00, 0x00, 0x05, 128, 255, 0, 1, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x01), 0x08, 1, 0, 2, 0, 255, 0, 1, 1,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x01), 0x01, 5,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F4 ) << 2) | 0x01), 20,
                127, 127, 255, 255,
                255, 127, 255, 255,
                255, 128, 0, 0,
                127, 128, 0, 0,
                0, 0, 0, 0,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F4 ) << 2) | 0x01), 0x04, 127, 127, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F8 ) << 2) | 0x01), 40,
                127, 0xEF, 255, 255, 255, 255, 255, 255,
                255, 0XEF, 255, 255, 255, 255, 255, 255,
                255, 0xF0, 0, 0, 0, 0, 0, 0,
                127, 0xF0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F8 ) << 2) | 0x01), 0x08, 127, 0xEF, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1 ) << 2) | 0x01), 0x04, 255, 128, 0, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1 ) << 2) | 0x01), 0x01, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2 ) << 2) | 0x01), 10,
                255, 255,
                128, 0,
                0, 0,
                0, 1,
                127, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2 ) << 2) | 0x01), 0x02, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x01), 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x01), 0x04, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8 ) << 2) | 0x01), 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8 ) << 2) | 0x01), 0x08, 255, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U1 ) << 2) | 0x01), 0x04, 255, 128, 0, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U1 ) << 2) | 0x01), 0x01, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2 ) << 2) | 0x01), 10,
                255, 255,
                128, 0,
                0, 0,
                0, 1,
                127, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2 ) << 2) | 0x01), 0x02, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4 ) << 2) | 0x01), 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4 ) << 2) | 0x01), 0x04, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U8 ) << 2) | 0x01), 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U8 ) << 2) | 0x01), 0x08, 255, 255, 255, 255, 255, 255, 255, 255

                };

            SECSItem testElement = null;
            LinkedList<SECSItem> expectedData1 = new LinkedList<SECSItem> ();
            LinkedList<SECSItem> expectedData2 = new LinkedList<SECSItem> ();
            testElement = new ASCIISECSItem ("ABC", SECSItemNumLengthBytes.TWO);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new BinarySECSItem (new byte [] { 128, 255, 0, 1, 127 }, SECSItemNumLengthBytes.THREE);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new BooleanArraySECSItem (new bool [] { true, false, true, false, true, false, true, true });
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new BooleanSECSItem (true);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new F4ArraySECSItem (new float [] { Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F });
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new F4SECSItem (Single.MaxValue);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new F8ArraySECSItem (new double [] { Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D });
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new F8SECSItem (Double.MaxValue);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new I1ArraySECSItem (new sbyte [] { -1, -128, 0, 127 });
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new I1SECSItem (-1);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new I2ArraySECSItem (new Int16 [] { -1, -32768, 0, 1, 32767 });
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new I2SECSItem (-1);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new I4ArraySECSItem (new Int32 [] { -1, -2147483648, 0, 1, 2147483647 });
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new I4SECSItem (-1);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new I8ArraySECSItem (new Int64 [] { -1, -9223372036854775808L, 0, 1, 9223372036854775807L });
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new I8SECSItem (-1);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new U1ArraySECSItem (new byte [] { 255, 128, 0, 127 });
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new U1SECSItem (255);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new U2ArraySECSItem (new UInt16 [] { 65535, 32768, 0, 1, 32767 });
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new U2SECSItem (65535);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new U4ArraySECSItem (new UInt32 [] { 0xFFFFFFFF, 2147483648, 0, 1, 2147483647 });
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new U4SECSItem (0xFFFFFFFF);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new U8ArraySECSItem (new UInt64 [] {0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000, 0x0000000000000001, 0x7FFFFFFFFFFFFFFF});
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);
            testElement = new U8SECSItem (0xFFFFFFFFFFFFFFFF);
            expectedData1.AddLast (testElement);
            expectedData2.AddLast (testElement);

            ListSECSItem innerList = new ListSECSItem (expectedData2);
            expectedData1.AddLast (innerList);

            ListSECSItem expectedResult = new ListSECSItem (expectedData1);

            RawSECSData rawData1 = new RawSECSData (input);

            SECSItem secsItem = rawData1.GenerateSECSItem ();

            Assert.IsTrue (secsItem.Equals (expectedResult));
        }
    }
}
