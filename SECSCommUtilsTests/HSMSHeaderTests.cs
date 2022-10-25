/*
 * Copyright 2022 Douglas Kaip
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

namespace com.CIMthetics.CSharpSECSTools.SECSCommUtils
{
	[TestFixture()]
	public class HSMSHeaderTests
	{
        [Test()]
        public void Test01()
        {
            HSMSHeader hsmsHeader = new HSMSHeader();
            Assert.IsTrue(hsmsHeader.SessionID == 0);
            Assert.IsTrue(hsmsHeader.HeaderByte2 == 0);
            Assert.IsTrue(hsmsHeader.HeaderByte3 == 0);
            Assert.IsTrue(hsmsHeader.PType == 0);
            Assert.IsTrue(hsmsHeader.SType == 0);
            Assert.IsTrue(hsmsHeader.SystemBytes == 0);
        }

        [Test()]
        public void Test02()
        {
            HSMSHeader hsmsHeader = new HSMSHeader();
            hsmsHeader.SessionID = UInt16.MaxValue;
            hsmsHeader.HeaderByte2 = byte.MaxValue;
            hsmsHeader.HeaderByte3 = byte.MaxValue;
            hsmsHeader.PType = PTypeValues.ReservedNotUsed255;
            hsmsHeader.SType = STypeValues.ReservedNotUsed255;
            hsmsHeader.SystemBytes = UInt32.MaxValue;

            Assert.IsTrue(hsmsHeader.SessionID == UInt16.MaxValue);
            Assert.IsTrue(hsmsHeader.HeaderByte2 == byte.MaxValue);
            Assert.IsTrue(hsmsHeader.HeaderByte3 == byte.MaxValue);
            Assert.IsTrue(hsmsHeader.PType == PTypeValues.ReservedNotUsed255);
            Assert.IsTrue(hsmsHeader.SType == STypeValues.ReservedNotUsed255);
            Assert.IsTrue(hsmsHeader.SystemBytes == UInt32.MaxValue);
        }

        [Test()]
        public void Test03()
        {
            HSMSHeader hsmsHeader = new HSMSHeader();
            Assert.IsTrue(hsmsHeader.Wbit == false);
            Assert.IsTrue(hsmsHeader.Stream == 0);
            Assert.IsTrue(hsmsHeader.Function == 0);
        }

        [Test()]
        public void Test04()
        {
            var exception = Assert.Catch(() => new HSMSHeader((HSMSHeader)null));

            Assert.IsInstanceOf<ArgumentNullException>(exception);

            Assert.IsTrue(exception.Message.Contains ("Argument \"header\" may not be null."));
        }

        [Test()]
        public void Test05()
        {
            var exception = Assert.Catch(() => new HSMSHeader((SECSIHeader)null));

            Assert.IsInstanceOf<ArgumentNullException>(exception);

            Assert.IsTrue(exception.Message.Contains ("Argument \"header\" may not be null."));
        }

        [Test()]
        public void Test100()
        {
            var exception = Assert.Catch(() => new HSMSHeader((byte[])null));

            Assert.IsInstanceOf<ArgumentNullException>(exception);

            Assert.IsTrue(exception.Message.Contains ("Argument \"header\" may not be null."));
        }

        [Test()]
        public void Test101()
        {
            byte[] input = new byte[9];

            var exception = Assert.Catch(() => new HSMSHeader(input));

            Assert.IsInstanceOf<ArgumentException>(exception);

            Assert.IsTrue(exception.Message.Contains ("The length of argument \"header\" must be >= 10."));
        }

        [Test()]
        public void Test102()
        {
            byte[] input = new byte[10] { 255, 255, 255, 255, 255, 255, 255, 255, 255, 255};
            HSMSHeader hsmsHeader = new HSMSHeader(input);

            Assert.IsTrue(hsmsHeader.SessionID == UInt16.MaxValue);
            Assert.IsTrue(hsmsHeader.HeaderByte2 == byte.MaxValue);
            Assert.IsTrue(hsmsHeader.HeaderByte3 == byte.MaxValue);
            Assert.IsTrue(hsmsHeader.PType == PTypeValues.ReservedNotUsed255);
            Assert.IsTrue(hsmsHeader.SType == STypeValues.ReservedNotUsed255);
            Assert.IsTrue(hsmsHeader.SystemBytes == UInt32.MaxValue);
        }

        [Test()]
        public void Test103()
        {
            byte[] input = new byte[10] { 255, 255, 255, 255, 255, 255, 255, 255, 255, 255};

            HSMSHeader hsmsHeader = new HSMSHeader(input);

            byte[] output = hsmsHeader.EncodeForTransport();

            Assert.AreEqual(input, output);
        }

        [Test()]
        public void Test104()
        {
            byte[] input = new byte[10] { 255, 255, 255, 255, 255, 255, 255, 255, 255, 255};
            HSMSHeader hsmsHeader = new HSMSHeader(input);

            Assert.IsTrue(hsmsHeader.Wbit == true);
            Assert.IsTrue(hsmsHeader.Stream == 127);
            Assert.IsTrue(hsmsHeader.Function == byte.MaxValue);
        }

	}
}

