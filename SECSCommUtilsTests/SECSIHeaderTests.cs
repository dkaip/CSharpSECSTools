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
using System.Data;

namespace com.CIMthetics.CSharpSECSTools.SECSCommUtils
{
    [TestFixture()]
	public class SECSIHeaderTests
	{
        [Test()]
        public void Test01()
        {
            SECSIHeader secs1Header = new SECSIHeader();
            Assert.IsTrue(secs1Header.Rbit == false);
            Assert.IsTrue(secs1Header.DeviceID == 0);
            Assert.IsTrue(secs1Header.Wbit == false);
            Assert.IsTrue(secs1Header.Stream == 0);
            Assert.IsTrue(secs1Header.Function == 0);
            Assert.IsTrue(secs1Header.Ebit == false);
            Assert.IsTrue(secs1Header.BlockNumber == 0);
            Assert.IsTrue(secs1Header.SystemBytes == 0);
        }

        [Test()]
        public void Test02()
        {
            SECSIHeader secs1Header = new SECSIHeader();
            secs1Header.Rbit = true;
            secs1Header.DeviceID = 10;
            secs1Header.Wbit = true;
            secs1Header.Stream = 127;
            secs1Header.Function = 128;
            secs1Header.Ebit = true;
            secs1Header.BlockNumber = 32767;
            secs1Header.SystemBytes = UInt32.MaxValue;

            SECSIHeader secs2Header = new SECSIHeader(secs1Header);

            Assert.IsTrue(secs2Header.Rbit == true);
            Assert.IsTrue(secs2Header.DeviceID == 10);
            Assert.IsTrue(secs2Header.Wbit == true);
            Assert.IsTrue(secs2Header.Stream == 127);
            Assert.IsTrue(secs2Header.Function == 128);
            Assert.IsTrue(secs2Header.Ebit == true);
            Assert.IsTrue(secs2Header.BlockNumber == 32767);
            Assert.IsTrue(secs2Header.SystemBytes == UInt32.MaxValue);
        }

        [Test()]
        public void Test03()
        {
            var exception = Assert.Catch(() => new SECSIHeader((SECSIHeader)null));

            Assert.IsInstanceOf<ArgumentNullException>(exception);

            Assert.IsTrue(exception.Message.Contains ("Argument \"header\" may not be null."));
        }

        [Test()]
        public void Test04()
        {
            var exception = Assert.Catch(() => new SECSIHeader((HSMSHeader)null));

            Assert.IsInstanceOf<ArgumentNullException>(exception);

            Assert.IsTrue(exception.Message.Contains ("Argument \"header\" may not be null."));
        }

        [Test()]
        public void Test101()
        {
            SECSIHeader secs1Header = new SECSIHeader();

            secs1Header.DeviceID = 0;
            Assert.IsTrue(secs1Header.DeviceID == 0);
            secs1Header.DeviceID = 32767;
            Assert.IsTrue(secs1Header.DeviceID == 32767);

            var exception = Assert.Catch(() => secs1Header.DeviceID = (UInt16)32768);

            Assert.IsInstanceOf<ConstraintException>(exception);

            Assert.IsTrue(exception.Message.Contains ("Acceptable values for DeviceID are 0 - 32767 inclusive."));
        }

        [Test()]
        public void Test102()
        {
            SECSIHeader secs1Header = new SECSIHeader();

            secs1Header.Stream = 0;
            Assert.IsTrue(secs1Header.Stream == 0);
            secs1Header.Stream = 127;
            Assert.IsTrue(secs1Header.Stream == 127);

            var exception = Assert.Catch(() => secs1Header.Stream = 150);

            Assert.IsInstanceOf<ConstraintException>(exception);

            Assert.IsTrue (exception.Message.Contains ("Acceptable values for Stream are 0 - 127 inclusive."));
        }

        [Test()]
        public void Test103()
        {
            SECSIHeader secs1Header = new SECSIHeader();

            secs1Header.BlockNumber = 0;
            Assert.IsTrue(secs1Header.BlockNumber == 0);
            secs1Header.BlockNumber = 32767;
            Assert.IsTrue(secs1Header.BlockNumber == 32767);

            var exception = Assert.Catch(() => secs1Header.BlockNumber = 32768);

            Assert.IsInstanceOf<ConstraintException>(exception);

            Assert.IsTrue (exception.Message.Contains ("Acceptable values for BlockNumber are 0 - 32767 inclusive."));
        }

        [Test()]
        public void Test200()
        {
            var exception = Assert.Catch(() => new SECSIHeader((byte[])null));

            Assert.IsInstanceOf<ArgumentNullException>(exception);

            Assert.IsTrue(exception.Message.Contains ("Argument \"header\" may not be null."));
        }

        [Test()]
        public void Test201()
        {
            byte[] input = new byte[9];

            var exception = Assert.Catch(() => new SECSIHeader(input));

            Assert.IsInstanceOf<ArgumentException>(exception);

            Assert.IsTrue(exception.Message.Contains ("The length of argument \"header\" must be >= 10."));
        }

        [Test()]
        public void Test202()
        {
            byte[] input = new byte[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
            SECSIHeader secs1Header = new SECSIHeader(input);

            Assert.IsTrue(secs1Header.Rbit == false);
            Assert.IsTrue(secs1Header.DeviceID == 0);
            Assert.IsTrue(secs1Header.Wbit == false);
            Assert.IsTrue(secs1Header.Stream == 0);
            Assert.IsTrue(secs1Header.Function == 0);
            Assert.IsTrue(secs1Header.Ebit == false);
            Assert.IsTrue(secs1Header.BlockNumber == 0);
            Assert.IsTrue(secs1Header.SystemBytes == 0);
        }

        [Test()]
        public void Test203()
        {
            byte[] input = new byte[10] { 255, 255, 255, 255, 255, 255, 255, 255, 255, 255};
            SECSIHeader secs1Header = new SECSIHeader(input);

            Assert.IsTrue(secs1Header.Rbit == true);
            Assert.IsTrue(secs1Header.DeviceID == (UInt16)0x7FFF);
            Assert.IsTrue(secs1Header.Wbit == true);
            Assert.IsTrue(secs1Header.Stream == 127);
            Assert.IsTrue(secs1Header.Function == 255);
            Assert.IsTrue(secs1Header.Ebit == true);
            Assert.IsTrue(secs1Header.BlockNumber == (UInt16)0x7FFF);
            Assert.IsTrue(secs1Header.SystemBytes == (UInt32)0xFFFFFFFF);
        }

        [Test()]
        public void Test204()
        {
            byte[] input = new byte[10] { 255, 255, 255, 255, 255, 255, 255, 255, 255, 255};

            SECSIHeader secs1Header = new SECSIHeader(input);

            byte[] output = secs1Header.EncodeForTransport();

            Assert.AreEqual(input, output);
        }

        [Test()]
        public void Test205()
        {
            byte[] input = new byte[10] { 255, 255, 255, 255, 255, 255, 255, 255, 255, 255};
            SECSIHeader secs1Header = new SECSIHeader(input);

            Assert.IsTrue(secs1Header.Wbit == true);
            Assert.IsTrue(secs1Header.Stream == 127);
            Assert.IsTrue(secs1Header.Function == byte.MaxValue);
        }

    }
}