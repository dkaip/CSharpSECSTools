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
	public class BinarySECSItemTests
	{
        [Test()]
        public void Test00()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.B) << 2) | 0x01), 0x05, 128, 255, 0, 1, 127 };

            SECSItem secsItem = SECSItemFactory.GenerateSECSItem(input);

            Assert.IsTrue(secsItem.GetType() == typeof(BinarySECSItem));
        }

        [Test()]
        public void Test01()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.B) << 2) | 0x01), 0x05, 128, 255, 0, 1, 127 };
            short [] expectedResult = { 128, 255, 0, 1, 127 };

            BinarySECSItem secsItem = (BinarySECSItem)SECSItemFactory.GenerateSECSItem(input);
            Assert.AreEqual(secsItem.Value, expectedResult);
        }

        [Test()]
        public void Test02()
        {
            byte [] input = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.B) << 2) | 0x01), 0x05, 128, 255, 0, 1, 127 };

            BinarySECSItem secsItem = (BinarySECSItem)SECSItemFactory.GenerateSECSItem(input);
            Assert.IsTrue (secsItem.ItemFormatCode == SECSItemFormatCode.B);
        }

        [Test()]
        public void Test03()
        {
            byte [] input = { 128, 255, 0, 1, 127 };
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.B) << 2) | 0x01), 0x05, 128, 255, 0, 1, 127 };

            BinarySECSItem secsItem = new BinarySECSItem(input);
            Assert.AreEqual(secsItem.EncodeForTransport(), expectedResult);
        }

        [Test()]
        public void Test04()
        {
            byte [] input = { 128, 255, 0, 1, 127 };
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode (SECSItemFormatCode.B) << 2) | 0x02), 0, 5, 128, 255, 0, 1, 127 };

            BinarySECSItem secsItem = new BinarySECSItem(input, SECSItemNumLengthBytes.TWO);
            Assert.AreEqual(secsItem.EncodeForTransport(), expectedResult);
        }

        [Test()]
        public void Test05()
        {
            byte [] input = { 128, 255, 0, 1, 127 };
            byte [] expectedResult = { (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.B) << 2) | 0x03), 0, 0, 5, 128, 255, 0, 1, 127 };

            BinarySECSItem secsItem = new BinarySECSItem(input, SECSItemNumLengthBytes.THREE);
            Assert.AreEqual(secsItem.EncodeForTransport(), expectedResult);
        }

        [Test()]
        public void Test06()
        {
            byte [] input = { 128 };

            BinarySECSItem secsItem = new BinarySECSItem(input, SECSItemNumLengthBytes.THREE);

            Assert.IsTrue(secsItem.ToString().Equals("Format:B Value: 128"));
        }

        [Test()]
        public void Test07()
        {
            byte [] input = { 128, 255, 0, 1, 127 };

            BinarySECSItem secsItem = new BinarySECSItem(input, SECSItemNumLengthBytes.THREE);

            Assert.IsTrue(secsItem.ToString().Equals ("Format:B Value: Array"));
        }

        [Test()]
        public void Test08()
        {
            Assert.IsTrue(true);
            /*
            byte [] input = { 128, 255, 0, 1, 127 };

            BinarySECSItem secsItem = new BinarySECSItem (input, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue (secsItem.GetHashCode () == 154436733);
            */
        }

        [Test()]
        public void Test09()
        {
            byte [] input = { 128, 255, 0, 1, 127 };

            BinarySECSItem secsItem = new BinarySECSItem(input, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue(secsItem.Equals(secsItem));
        }

        [Test()]
        public void Test10()
        {
            byte [] input = { 128, 255, 0, 1, 127 };

            BinarySECSItem secsItem = new BinarySECSItem(input, SECSItemNumLengthBytes.ONE);

            Assert.IsFalse(secsItem.Equals(null));
        }

        [Test()]
        public void Test11()
        {
            byte [] input = { 128, 255, 0, 1, 127 };

            BinarySECSItem secsItem1 = new BinarySECSItem(input, SECSItemNumLengthBytes.ONE);
            Object secsItem2 = new F8SECSItem(2.141592D);
            Assert.IsFalse(secsItem1.Equals(secsItem2));
        }

        [Test()]
        public void Test12()
        {
            byte [] input1 = { 128, 255, 0, 1, 127 };
            byte [] input2 = { 128, 255, 0, 1, 127 };

            BinarySECSItem secsItem1 = new BinarySECSItem(input1, SECSItemNumLengthBytes.ONE);
            BinarySECSItem secsItem2 = new BinarySECSItem(input2, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue(secsItem1.Equals(secsItem2));
        }

        [Test()]
        public void Test13()
        {
            byte [] input1 = { 128, 255, 0, 1, 127 };
            byte [] input2 = { 128, 255, 0, 1, 126 };

            BinarySECSItem secsItem1 = new BinarySECSItem(input1, SECSItemNumLengthBytes.ONE);
            BinarySECSItem secsItem2 = new BinarySECSItem(input2, SECSItemNumLengthBytes.ONE);

            Assert.IsFalse(secsItem1.Equals(secsItem2));
        }
	}
}

