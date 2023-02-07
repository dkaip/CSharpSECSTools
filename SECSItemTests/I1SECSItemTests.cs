/*
 * Copyright 2019-2023 Douglas Kaip
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

using com.CIMthetics.CSharpSECSTools.SECSItems;

namespace com.CIMthetics.CSharpSECSTools.SECSItemTests
{
	[TestFixture()]
	public class I1SECSItemTests
	{
        [Test()]
        public void Test00()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1) << 2) | 0x01), 0x01, 255 };

            SECSItem secsItem = SECSItemFactory.GenerateSECSItem(input);

            Assert.IsTrue(secsItem.GetType() == typeof(I1SECSItem));
        }

        [Test()]
        public void Test01()
        {
            sbyte value = -1;
            I1SECSItem secsItem = new I1SECSItem(value);
            Assert.IsTrue(secsItem.Value == value);
        }

        [Test()]
        public void Test02()
        {
            sbyte value = -128;
            I1SECSItem secsItem = new I1SECSItem(value);
            Assert.IsTrue(secsItem.Value == value);
        }

        [Test()]
        public void Test03()
        {
            sbyte value = 0;
            I1SECSItem secsItem = new I1SECSItem(value);
            Assert.IsTrue(secsItem.Value == value);
        }

        [Test()]
        public void Test04()
        {
            sbyte value = 127;
            I1SECSItem secsItem = new I1SECSItem(value);
            Assert.IsTrue(secsItem.Value == value);
        }

        [Test()]
        public void Test04a()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1) << 2) | 0x01), 0x01, 255 };
            SECSItem secsItem = SECSItemFactory.GenerateSECSItem(input);
            Assert.IsTrue(secsItem.GetType() == typeof(I1SECSItem));
        }

        [Test()]
        public void Test05()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1) << 2) | 0x01), 0x01, 255 };
            sbyte expectedOutput = -1;
            SECSItem secsItem = SECSItemFactory.GenerateSECSItem(input);
            Assert.IsTrue(((I1SECSItem)secsItem).Value == expectedOutput);
        }

        [Test()]
        public void Test06()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1) << 2) | 0x01), 0x01, 128 };
            sbyte expectedOutput = -128;
            SECSItem secsItem = SECSItemFactory.GenerateSECSItem(input);
            Assert.IsTrue(((I1SECSItem)secsItem).Value == expectedOutput);
        }

        [Test()]
        public void Test07()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1) << 2) | 0x01), 0x01, 0 };
            sbyte expectedOutput = 0;
            SECSItem secsItem = SECSItemFactory.GenerateSECSItem(input);
            Assert.IsTrue(((I1SECSItem)secsItem).Value == expectedOutput);
        }

        [Test()]
        public void Test08()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1) << 2) | 0x01), 0x01, 127 };
            sbyte expectedOutput = 127;
            SECSItem secsItem = SECSItemFactory.GenerateSECSItem(input);
            Assert.IsTrue(((I1SECSItem)secsItem).Value == expectedOutput);
        }

        [Test()]
        public void Test09()
        {
            sbyte value = -1;
            I1SECSItem secsItem = new I1SECSItem(value);
            Assert.IsTrue(secsItem.ItemFormatCode == SECSItemFormatCode.I1);
        }

        [Test()]
        public void Test10()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1) << 2) | 0x01), 0x01, 127 };
            I1SECSItem secsItem = (I1SECSItem)SECSItemFactory.GenerateSECSItem(input);
            Assert.IsTrue(secsItem.ItemFormatCode == SECSItemFormatCode.I1);
        }

        [Test()]
        public void Test11()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1) << 2) | 0x01), 0x01, 127 };

            I1SECSItem secsItem = new I1SECSItem(127);
            Assert.AreEqual(secsItem.EncodeForTransport(), expectedResult);
        }

        [Test()]
        public void Test12()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1) << 2) | 0x02), 0, 0x01, 127 };

            I1SECSItem secsItem = new I1SECSItem(127, SECSItemNumLengthBytes.TWO);
            Assert.AreEqual(secsItem.EncodeForTransport(), expectedResult);
        }

        [Test()]
        public void Test13()
        {
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1) << 2) | 0x03), 0, 0, 0x01, 127 };

            I1SECSItem secsItem = new I1SECSItem(127, SECSItemNumLengthBytes.THREE);
            Assert.AreEqual(secsItem.EncodeForTransport(), expectedResult);
        }

        [Test()]
        public void Test14()
        {
            I1SECSItem secsItem = new I1SECSItem(127);
            Assert.IsTrue(secsItem.ToString().Equals ("Format:I1 Value: 127"));
        }

        [Test()]
        public void Test15()
        {
            I1SECSItem secsItem1 = new I1SECSItem(127);
            I1SECSItem secsItem2 = new I1SECSItem(127);
            Assert.IsTrue(secsItem1.Equals(secsItem2));
        }

        [Test()]
        public void Test16()
        {
            I1SECSItem secsItem1 = new I1SECSItem(127);
            I1SECSItem secsItem2 = new I1SECSItem(-128);
            Assert.IsFalse(secsItem1.Equals(secsItem2));
        }

        [Test()]
        public void Test17()
        {
            I1SECSItem secsItem1 = new I1SECSItem(127);
            I1SECSItem secsItem2 = null;
            Assert.IsFalse(secsItem1.Equals(secsItem2));
        }

        [Test()]
        public void Test18()
        {
            I1SECSItem secsItem1 = new I1SECSItem(127);
            Assert.IsTrue(secsItem1.Equals(secsItem1));
        }

        [Test()]
        public void Test19()
        {
            I1SECSItem secsItem1 = new I1SECSItem(127);
            SECSItem secsItem2 = null;
            Assert.IsFalse(secsItem1.Equals(secsItem2));
        }

        // [Test()]
        // public void Test20()
        // {
        //     Assert.IsTrue (true);
        //     /*
        //     I1SECSItem secsItem = new I1SECSItem (127);

        //     Assert.IsTrue(secsItem.GetHashCode() == 158);
        //     */
        // }
        //
        [Test()]
        public void Test21()
        {
            I1SECSItem secsItem1 = new I1SECSItem(127);
            I1SECSItem secsItem2 = new I1SECSItem(127);
            Assert.IsTrue(secsItem1.GetHashCode() == secsItem2.GetHashCode());
        }

        [Test()]
        public void Test22()
        {
            I1SECSItem secsItem1 = new I1SECSItem(127);
            Object secsItem2 = new F8SECSItem(2.141592D);
            Assert.IsFalse(secsItem1.Equals(secsItem2));
        }
	}
}

