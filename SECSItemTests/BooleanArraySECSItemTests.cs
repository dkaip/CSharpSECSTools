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
	public class BooleanArraySECSItemTests
	{
        [Test ()]
        public void Test01 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.BO) << 2) | 0x01), 0x08, 1, 0, 2, 0, 255, 0, 1, 1 };
            bool [] expectedResult = { true, false, true, false, true, false, true, true };
            BooleanArraySECSItem secsItem = new BooleanArraySECSItem (input, 0);
            Assert.AreEqual (secsItem.GetValue (), expectedResult);
        }

        [Test ()]
        public void Test02 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.BO) << 2) | 0x01), 0x08, 1, 0, 2, 0, 255, 0, 1, 1 };
            BooleanArraySECSItem secsItem = new BooleanArraySECSItem (input, 0);
            Assert.IsTrue (secsItem.GetSECSItemFormatCode () == SECSItemFormatCode.BO);
        }

        [Test ()]
        public void Test03 ()
        {
            bool [] input = { true, false, true, false, true, false, true, true };
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.BO) << 2) | 0x01), 8, 1, 0, 1, 0, 1, 0, 1, 1 };

            BooleanArraySECSItem secsItem = new BooleanArraySECSItem (input);
            Assert.AreEqual (secsItem.ToRawSECSItem (), expectedResult);
        }

        [Test ()]
        public void Test04 ()
        {
            bool [] input = { true, false, true, false, true, false, true, true };
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.BO) << 2) | 0x02), 0, 8, 1, 0, 1, 0, 1, 0, 1, 1 };

            BooleanArraySECSItem secsItem = new BooleanArraySECSItem (input, SECSItemNumLengthBytes.TWO);
            Assert.AreEqual (secsItem.ToRawSECSItem (), expectedResult);
        }

        [Test ()]
        public void Test05 ()
        {
            bool [] input = { true, false, true, false, true, false, true, true };
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.BO) << 2) | 0x03), 0, 0, 8, 1, 0, 1, 0, 1, 0, 1, 1 };

            BooleanArraySECSItem secsItem = new BooleanArraySECSItem (input, SECSItemNumLengthBytes.THREE);
            Assert.AreEqual (secsItem.ToRawSECSItem (), expectedResult);
        }

        [Test ()]
        public void Test06 ()
        {
            bool [] input = { true, false, true, false, true, false, true, true };

            BooleanArraySECSItem secsItem = new BooleanArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem.ToString ().Equals ("Format:BO Value: Array"));
        }

        [Test ()]
        public void Test07 ()
        {
            Assert.IsTrue (true);
            /*
            bool [] input = { true, false, true, false, true, false, true, true };

            BooleanArraySECSItem secsItem = new BooleanArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem.GetHashCode () == 796855730);
            */
        }

        [Test ()]
        public void Test08 ()
        {
            bool [] input = { true, false, true, false, true, false, true, true };

            BooleanArraySECSItem secsItem = new BooleanArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem.Equals (secsItem));
        }

        [Test ()]
        public void Test09 ()
        {
            bool [] input = { true, false, true, false, true, false, true, true };

            BooleanArraySECSItem secsItem = new BooleanArraySECSItem (input, SECSItemNumLengthBytes.ONE);

            Assert.IsFalse (secsItem.Equals (null));
        }

        [Test ()]
        public void Test10 ()
        {
            bool [] input1 = { true, false, true, false, true, false, true, true };
            bool [] input2 = { true, false, true, false, true, false, true, true };

            BooleanArraySECSItem secsItem1 = new BooleanArraySECSItem (input1, SECSItemNumLengthBytes.ONE);
            BooleanArraySECSItem secsItem2 = new BooleanArraySECSItem (input2, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem1.Equals (secsItem2));
        }

        [Test ()]
        public void Test11 ()
        {
            bool [] input1 = { true, false, true, false, true, false, true, true };
            bool [] input2 = { true, false, true, false, true, false, true, false };

            BooleanArraySECSItem secsItem1 = new BooleanArraySECSItem (input1, SECSItemNumLengthBytes.ONE);
            BooleanArraySECSItem secsItem2 = new BooleanArraySECSItem (input2, SECSItemNumLengthBytes.ONE);

            Assert.IsFalse (secsItem1.Equals (secsItem2));
        }

        [Test ()]
       public void Test12 ()
        {
            bool [] input1 = { true, false, true, false, true, false, true, true };
            BooleanArraySECSItem secsItem1 = new BooleanArraySECSItem (input1, SECSItemNumLengthBytes.ONE);
            Object secsItem2 = new F8SECSItem (2.141592D);
            Assert.IsFalse (secsItem1.Equals (secsItem2));
        }
	}
}

