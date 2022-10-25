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
using NUnit.Framework;
using System;
using com.CIMthetics.CSharpSECSTools.SECSItems;

namespace SECSItemTests
{
    [TestFixture ()]
    public class SECSItemsTests
    {
        public class SECSItemTest : SECSItem
        {
            public SECSItemTest (SECSItemFormatCode formatCode, int length) : base (formatCode, length)
            { }

            public SECSItemTest (SECSItemFormatCode formatCode, int length, SECSItemNumLengthBytes desiredNumberOfLengthBytes) : base (formatCode, length, desiredNumberOfLengthBytes)
            { }

            public SECSItemTest (byte [] data, int itemOffset) : base (data, itemOffset)
            { }

            public int TestPopulateSECSItemHeaderData (byte [] buffer, int numberOfBytes)
            {
                return PopulateSECSItemHeaderData (buffer, numberOfBytes);
            }

            public override bool Equals (Object obj)
            {
                return false;
            }

            public override int GetHashCode ()
            {
                return 0;
            }

            public override byte [] EncodeForTransport ()
            {
                return null;
            }
        }

        [Test ()]
        public void Test01 ()
        {
            var exception = Assert.Catch (() => new SECSItemTest (SECSItemFormatCode.U1, -1));

            Assert.IsInstanceOf<ArgumentException> (exception);

            Assert.IsTrue (exception.Message.Contains ("The value for the length argument must be between 0 and 16777215 inclusive."));
        }

        [Test ()]
        public void Test02 ()
        {
            var exception = Assert.Catch (() => new SECSItemTest (SECSItemFormatCode.U1, 16777216));

            Assert.IsInstanceOf<ArgumentException> (exception);

            Assert.IsTrue (exception.Message.Contains ("The value for the length argument must be between 0 and 16777215 inclusive."));
        }

        [Test ()]
        public void Test03 ()
        {
            foreach (SECSItemFormatCode formatCode in Enum.GetValues (typeof (SECSItemFormatCode))) {
                SECSItemTest secsItem = new SECSItemTest (formatCode, 0);

                Assert.IsTrue (secsItem.GetSECSItemFormatCode () == formatCode);
                Assert.IsTrue (secsItem.GetFormatCode () == formatCode);
            }
        }

        [Test ()]
        public void Test04 ()
        {
            SECSItemTest secsItem = new SECSItemTest (SECSItemFormatCode.U1, 0);

            Assert.IsTrue (secsItem.GetOutboundNumberOfLengthBytes () == SECSItemNumLengthBytes.ONE);
        }

        [Test ()]
        public void Test05 ()
        {
            SECSItemTest secsItem = new SECSItemTest (SECSItemFormatCode.U1, 255);

            Assert.IsTrue (secsItem.GetOutboundNumberOfLengthBytes () == SECSItemNumLengthBytes.ONE);
        }

        [Test ()]
        public void Test06 ()
        {
            SECSItemTest secsItem = new SECSItemTest (SECSItemFormatCode.U1, 256);

            Assert.IsTrue (secsItem.GetOutboundNumberOfLengthBytes () == SECSItemNumLengthBytes.TWO);
        }

        [Test ()]
        public void Test07 ()
        {
            SECSItemTest secsItem = new SECSItemTest (SECSItemFormatCode.U1, 65535);

            Assert.IsTrue (secsItem.GetOutboundNumberOfLengthBytes () == SECSItemNumLengthBytes.TWO);
        }

        [Test ()]
        public void Test08 ()
        {
            SECSItemTest secsItem = new SECSItemTest (SECSItemFormatCode.U1, 65536);

            Assert.IsTrue (secsItem.GetOutboundNumberOfLengthBytes () == SECSItemNumLengthBytes.THREE);
        }

        [Test ()]
        public void Test09 ()
        {
            SECSItemTest secsItem = new SECSItemTest (SECSItemFormatCode.U1, 16777215);

            Assert.IsTrue (secsItem.GetOutboundNumberOfLengthBytes () == SECSItemNumLengthBytes.THREE);
        }

        [Test ()]
        public void Test10 ()
        {
            var exception = Assert.Catch (() => new SECSItemTest (SECSItemFormatCode.U1, -1, SECSItemNumLengthBytes.ONE));

            Assert.IsInstanceOf<ArgumentException> (exception);

            Assert.IsTrue (exception.Message.Contains ("The value for the length argument must be between 0 and 16777215 inclusive."));
        }

        [Test ()]
        public void Test11 ()
        {
            var exception = Assert.Catch (() => new SECSItemTest (SECSItemFormatCode.U1, 16777216, SECSItemNumLengthBytes.ONE));

            Assert.IsInstanceOf<ArgumentException> (exception);

            Assert.IsTrue (exception.Message.Contains ("The value for the length argument must be between 0 and 16777215 inclusive."));
        }

        [Test ()]
        public void Test12 ()
        {
            SECSItemTest secsItem = new SECSItemTest (SECSItemFormatCode.U1, 1, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem.GetOutboundNumberOfLengthBytes () == SECSItemNumLengthBytes.ONE);
        }

        [Test ()]
        public void Test13 ()
        {
            SECSItemTest secsItem = new SECSItemTest (SECSItemFormatCode.U1, 256, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem.GetOutboundNumberOfLengthBytes () == SECSItemNumLengthBytes.TWO);
        }

        [Test ()]
        public void Test14 ()
        {
            SECSItemTest secsItem = new SECSItemTest (SECSItemFormatCode.U1, 65536, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem.GetOutboundNumberOfLengthBytes () == SECSItemNumLengthBytes.THREE);
        }

        [Test ()]
        public void Test15 ()
        {
            SECSItemTest secsItem = new SECSItemTest (SECSItemFormatCode.U1, 1, SECSItemNumLengthBytes.TWO);

            Assert.IsTrue (secsItem.GetOutboundNumberOfLengthBytes () == SECSItemNumLengthBytes.TWO);
        }

        [Test ()]
        public void Test16 ()
        {
            SECSItemTest secsItem = new SECSItemTest (SECSItemFormatCode.U1, 256, SECSItemNumLengthBytes.TWO);

            Assert.IsTrue (secsItem.GetOutboundNumberOfLengthBytes () == SECSItemNumLengthBytes.TWO);
        }

        [Test ()]
        public void Test17 ()
        {
            SECSItemTest secsItem = new SECSItemTest (SECSItemFormatCode.U1, 65536, SECSItemNumLengthBytes.TWO);

            Assert.IsTrue (secsItem.GetOutboundNumberOfLengthBytes () == SECSItemNumLengthBytes.THREE);
        }

        [Test ()]
        public void Test18 ()
        {
            SECSItemTest secsItem = new SECSItemTest (SECSItemFormatCode.U1, 1, SECSItemNumLengthBytes.THREE);

            Assert.IsTrue (secsItem.GetOutboundNumberOfLengthBytes () == SECSItemNumLengthBytes.THREE);
        }

        [Test ()]
        public void Test19 ()
        {
            SECSItemTest secsItem = new SECSItemTest (SECSItemFormatCode.U1, 256, SECSItemNumLengthBytes.THREE);

            Assert.IsTrue (secsItem.GetOutboundNumberOfLengthBytes () == SECSItemNumLengthBytes.THREE);
        }

        [Test ()]
        public void Test20 ()
        {
            SECSItemTest secsItem = new SECSItemTest (SECSItemFormatCode.U1, 65536, SECSItemNumLengthBytes.THREE);

            Assert.IsTrue (secsItem.GetOutboundNumberOfLengthBytes () == SECSItemNumLengthBytes.THREE);
        }

        [Test ()]
        public void Test21 ()
        {
            byte [] input = { 128, 255, 0, 1, 127 };

            // This is just a lazy way to get a known SECSItem in "wire format"
            BinarySECSItem secsItem = new BinarySECSItem (input, SECSItemNumLengthBytes.ONE);

            SECSItemTest secsItemA = new SECSItemTest (secsItem.EncodeForTransport (), 0);

            Assert.IsTrue (secsItemA.GetInboundNumberOfLengthBytes () == SECSItemNumLengthBytes.ONE);
            Assert.IsTrue (secsItemA.GetSECSItemFormatCode () == SECSItemFormatCode.B);
            Assert.IsTrue (secsItemA.GetLengthInBytes () == 5);
        }

        [Test ()]
        public void Test22 ()
        {
            byte [] input = { 128, 255, 0, 1, 127 };

            // This is just a lazy way to get a known SECSItem in "wire format"
            BinarySECSItem secsItem = new BinarySECSItem (input, SECSItemNumLengthBytes.TWO);

            SECSItemTest secsItemA = new SECSItemTest (secsItem.EncodeForTransport (), 0);

            Assert.IsTrue (secsItemA.GetInboundNumberOfLengthBytes () == SECSItemNumLengthBytes.TWO);
            Assert.IsTrue (secsItemA.GetSECSItemFormatCode () == SECSItemFormatCode.B);
            Assert.IsTrue (secsItemA.GetLengthInBytes () == 5);
        }

        [Test ()]
        public void Test23 ()
        {
            byte [] input = { 128, 255, 0, 1, 127 };

            // This is just a lazy way to get a known SECSItem in "wire format"
            BinarySECSItem secsItem = new BinarySECSItem (input, SECSItemNumLengthBytes.THREE);

            SECSItemTest secsItemA = new SECSItemTest (secsItem.EncodeForTransport (), 0);

            Assert.IsTrue (secsItemA.GetInboundNumberOfLengthBytes () == SECSItemNumLengthBytes.THREE);
            Assert.IsTrue (secsItemA.GetSECSItemFormatCode () == SECSItemFormatCode.B);
            Assert.IsTrue (secsItemA.GetLengthInBytes () == 5);
        }

        [Test ()]
        public void Test24 ()
        {
            byte [] input = null;

            var exception = Assert.Catch (() => new SECSItemTest (input, 0));

            Assert.IsInstanceOf<ArgumentNullException> (exception);

            Assert.IsTrue (exception.Message.Contains ("\"data\" argument must not be null."));
        }

        [Test ()]
        public void Test25 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.L) << 2) | 0x01) };

            var exception = Assert.Catch (() => new SECSItemTest (input, 0));

            Assert.IsInstanceOf<ArgumentException> (exception);

            Assert.IsTrue (exception.Message.Contains ("\"data\" argument must have a length >= 2."));
        }

        [Test ()]
        public void Test26 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.L) << 2) | 0x02), 0x00 };

            var exception = Assert.Catch (() => new SECSItemTest (input, 0));

            Assert.IsInstanceOf<ArgumentException> (exception);

            Assert.IsTrue (exception.Message.Contains ("With two length bytes the minimum length for the \"data\" argument is 3."));
        }

        [Test ()]
        public void Test27 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.L) << 2) | 0x03), 0x00, 0x00 };

            var exception = Assert.Catch (() => new SECSItemTest (input, 0));

            Assert.IsInstanceOf<ArgumentException> (exception);

            Assert.IsTrue (exception.Message.Contains ("With three length bytes the minimum length for the \"data\" argument is 4."));
        }

        [Test ()]
        public void Test28 ()
        {
            byte [] input = { };

            var exception = Assert.Catch (() => new SECSItemTest (input, 0));

            Assert.IsInstanceOf<ArgumentException> (exception);

            Assert.IsTrue (exception.Message.Contains ("\"data\" argument must have a length >= 2."));
        }

        [Test ()]
        public void Test29 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.L) << 2) | 0x01), 0x00 };

            SECSItemTest secsItem = new SECSItemTest (input, 0);

            Assert.IsTrue (secsItem.GetLengthInBytes () == 0);
        }

        [Test ()]
        public void Test30 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.L) << 2) | 0x01), (byte)0xFF };

            SECSItemTest secsItem = new SECSItemTest (input, 0);

            Assert.IsTrue (secsItem.GetLengthInBytes () == 255);
        }

        [Test ()]
        public void Test31 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.L) << 2) | 0x02), 0x01, 0x00 };

            SECSItemTest secsItem = new SECSItemTest (input, 0);

            Assert.IsTrue (secsItem.GetLengthInBytes () == 256);
        }

        [Test ()]
        public void Test32 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.L) << 2) | 0x02), (byte)0xFF, (byte)0xFF };

            SECSItemTest secsItem = new SECSItemTest (input, 0);

            Assert.IsTrue (secsItem.GetLengthInBytes () == 65535);
        }

        [Test ()]
        public void Test33 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.L) << 2) | 0x03), 0x01, 0x00, 0x00 };

            SECSItemTest secsItem = new SECSItemTest (input, 0);

            Assert.IsTrue (secsItem.GetLengthInBytes () == 65536);
        }

        [Test ()]
        public void Test34 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.L) << 2) | 0x03), (byte)0xFF, (byte)0xFF, (byte)0xFF };

            SECSItemTest secsItem = new SECSItemTest (input, 0);

            Assert.IsTrue (secsItem.GetLengthInBytes () == 16777215);
        }

        [Test ()]
        public void Test35 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.L) << 2) | 0x00), 0x00 };
            var exception = Assert.Catch (() => new SECSItemTest (input, 0));

            Assert.IsInstanceOf<ArgumentException> (exception);

            Assert.IsTrue (exception.Message.Contains ("The number of length bytes is not allowed to be ZERO."));
        }

        /*
         * Just to stop the coverage checker from complaining.
         */
        [Test ()]
        public void Test36 ()
        {
            SECSItemTest secsItem = new SECSItemTest (SECSItemFormatCode.U1, 1, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem.Equals (secsItem) == false);
        }

        /*
         * Just to stop the coverage checker from complaining.
         */
        [Test ()]
        public void Test37 ()
        {
            SECSItemTest secsItem = new SECSItemTest (SECSItemFormatCode.U1, 1, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem.EncodeForTransport () == null);
        }

        /*
         * Just to stop the coverage checker from complaining.
         */
        [Test ()]
        public void Test38 ()
        {
            SECSItemTest secsItem = new SECSItemTest (SECSItemFormatCode.U1, 1, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem.GetHashCode () == 0);
        }

        [Test ()]
        public void Test39 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.I1) << 2) | 0x01), 0x01, 0x7F };

            SECSItemTest secsItem = new SECSItemTest (input, 0);

            byte [] itemHeader = new byte [2];

            int offset = secsItem.TestPopulateSECSItemHeaderData (itemHeader, 1);
            Assert.IsTrue (offset == 2);
            Assert.IsTrue (itemHeader [0] == 0x65);
            Assert.IsTrue (itemHeader [1] == 1);
        }

        [Test ()]
        public void Test40 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.I1) << 2) | 0x02), 0x00, 0x01, 0x7F };

            SECSItemTest secsItem = new SECSItemTest (input, 0);

            byte [] itemHeader = new byte [3];

            int offset = secsItem.TestPopulateSECSItemHeaderData (itemHeader, 1);
            Assert.IsTrue (offset == 3);
            Assert.IsTrue (itemHeader [0] == 0x66);
            Assert.IsTrue (itemHeader [1] == 0);
            Assert.IsTrue (itemHeader [2] == 1);
        }

        [Test ()]
        public void Test41 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.I1) << 2) | 0x03), 0x00, 0x00, 0x01, 0x7F };

            SECSItemTest secsItem = new SECSItemTest (input, 0);

            byte [] itemHeader = new byte [4];

            int offset = secsItem.TestPopulateSECSItemHeaderData (itemHeader, 1);
            Assert.IsTrue (offset == 4);
            Assert.IsTrue (itemHeader [0] == 0x67);
            Assert.IsTrue (itemHeader [1] == 0);
            Assert.IsTrue (itemHeader [2] == 0);
            Assert.IsTrue (itemHeader [3] == 1);
        }
    }
}
