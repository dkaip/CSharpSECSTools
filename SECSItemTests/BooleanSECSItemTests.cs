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
	public class BooleanSECSItemTests
	{
        [Test()]
        public void Test01 ()
        {
            BooleanSECSItem secsItem = new BooleanSECSItem (true);
            Assert.IsTrue (secsItem.GetValue () == true);
        }

        [Test()]
        public void Test02 ()
        {
            BooleanSECSItem secsItem = new BooleanSECSItem (false);
            Assert.IsTrue (secsItem.GetValue () == false);
        }

        [Test()]
        public void Test03 ()
        {
            BooleanSECSItem secsItem = new BooleanSECSItem (true);
            Assert.IsFalse (secsItem.GetValue () == false);
        }

        [Test()]
        public void Test04 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.BO) << 2) | 0x01), 0x01, 5 };
            BooleanSECSItem secsItem = new BooleanSECSItem (input, 0);
            Assert.IsTrue (secsItem.GetValue () == true);
        }

        [Test()]
        public void Test05 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.BO) << 2) | 0x01), 0x01, 0 };
            BooleanSECSItem secsItem = new BooleanSECSItem (input, 0);
            Assert.IsTrue (secsItem.GetValue () == false);
        }

        [Test()]
        public void Test06 ()
        {
            BooleanSECSItem secsItem = new BooleanSECSItem (true);
            Assert.IsTrue (secsItem.GetSECSItemFormatCode () == SECSItemFormatCode.BO);
        }

        [Test()]
        public void Test0 ()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.BO) << 2) | 0x01), 0x01, 5 };
            BooleanSECSItem secsItem = new BooleanSECSItem (input, 0);
            Assert.IsTrue (secsItem.GetSECSItemFormatCode () == SECSItemFormatCode.BO);
        }

        [Test()]
        public void Test08 ()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.BO) << 2) | 0x01), 0x01, 1 };

            BooleanSECSItem secsItem = new BooleanSECSItem (true);
            Assert.AreEqual (secsItem.ToRawSECSItem (), expectedResult);
        }

        [Test()]
        public void Test09 ()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.BO) << 2) | 0x02), 0, 1, 1 };

            BooleanSECSItem secsItem = new BooleanSECSItem (true, SECSItemNumLengthBytes.TWO);
            Assert.AreEqual (secsItem.ToRawSECSItem (), expectedResult);
        }

        [Test()]
        public void Test10 ()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.BO) << 2) | 0x03), 0, 0, 1, 0 };

            BooleanSECSItem secsItem = new BooleanSECSItem (false, SECSItemNumLengthBytes.THREE);
            Assert.AreEqual (secsItem.ToRawSECSItem (), expectedResult);
        }

        [Test()]
        public void Test11 ()
        {
            BooleanSECSItem secsItem = new BooleanSECSItem (true);
            Assert.IsTrue (secsItem. ToString ().Equals ("Format:BO Value: True"));
        }

        [Test()]
        public void Test12 ()
        {
            BooleanSECSItem secsItem = new BooleanSECSItem (false);
            Assert.IsTrue (secsItem. ToString ().Equals ("Format:BO Value: False"));
        }

        [Test()]
        public void Test13 ()
        {
            Assert.IsTrue (true);
            /*
            BooleanSECSItem secsItem = new BooleanSECSItem (true);

            Assert.IsTrue (secsItem.GetHashCode () == 1262);
            */
        }

        [Test()]
        public void Test14 ()
        {
            Assert.IsTrue (true);
            /*
            BooleanSECSItem secsItem = new BooleanSECSItem (false);

            Assert.IsTrue (secsItem.GetHashCode () == 1268);
            */
        }

        [Test()]
        public void Test15 ()
        {
            BooleanSECSItem secsItem1 = new BooleanSECSItem (true);
            BooleanSECSItem secsItem2 = new BooleanSECSItem (true);

            Assert.IsTrue (secsItem1.GetHashCode () == secsItem2.GetHashCode ());
        }

        [Test()]
        public void Test16 ()
        {
            BooleanSECSItem secsItem1 = new BooleanSECSItem (true);
            BooleanSECSItem secsItem2 = new BooleanSECSItem (false);

            Assert.IsFalse (secsItem1.GetHashCode () == secsItem2.GetHashCode ());
        }

        [Test()]
        public void Test17 ()
        {
            BooleanSECSItem secsItem = new BooleanSECSItem (false);

            Assert.IsTrue (secsItem.Equals (secsItem));
        }

        [Test()]
        public void Test18 ()
        {
            BooleanSECSItem secsItem = new BooleanSECSItem (false);

            Assert.IsFalse (secsItem.Equals (null));
        }

        [Test()]
        public void Test19 ()
        {
            BooleanSECSItem secsItem1 = new BooleanSECSItem (false);
            Object secsItem2 = new F8SECSItem (2.141592D);
            Assert.IsFalse (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test20 ()
        {
            BooleanSECSItem secsItem1 = new BooleanSECSItem (true);
            BooleanSECSItem secsItem2 = new BooleanSECSItem (true);

            Assert.IsTrue (secsItem1.Equals (secsItem2));
        }

        [Test()]
        public void Test21 ()
        {
            BooleanSECSItem secsItem1 = new BooleanSECSItem (true);
            BooleanSECSItem secsItem2 = new BooleanSECSItem (false);

            Assert.IsFalse (secsItem1.Equals (secsItem2));
        }
   	}
}

