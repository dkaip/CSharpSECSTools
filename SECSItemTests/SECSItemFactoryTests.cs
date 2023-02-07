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
    public class SECSItemFactoryTests
    {
        [Test()]
        public void Test01()
        {
            byte [] input = {
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.L) << 2) | 0x01), 25,

                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.A) << 2) | 0x02), 0x00, 0x03, 0x41, 0x42, 0x43,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.B) << 2) | 0x03), 0x00, 0x00, 0x05, 128, 255, 0, 1, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO) << 2) | 0x01), 0x08, 1, 0, 2, 0, 255, 0, 1, 1,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO) << 2) | 0x01), 0x01, 5,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F4) << 2) | 0x01), 20,
                127, 127, 255, 255,
                255, 127, 255, 255,
                255, 128, 0, 0,
                127, 128, 0, 0,
                0, 0, 0, 0,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F4) << 2) | 0x01), 0x04, 127, 127, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F8) << 2) | 0x01), 40,
                127, 0xEF, 255, 255, 255, 255, 255, 255,
                255, 0XEF, 255, 255, 255, 255, 255, 255,
                255, 0xF0, 0, 0, 0, 0, 0, 0,
                127, 0xF0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F8) << 2) | 0x01), 0x08, 127, 0xEF, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1) << 2) | 0x01), 0x04, 255, 128, 0, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1) << 2) | 0x01), 0x01, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2) << 2) | 0x01), 10,
                255, 255,
                128, 0,
                0, 0,
                0, 1,
                127, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2) << 2) | 0x01), 0x02, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I4) << 2) | 0x01), 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I4) << 2) | 0x01), 0x04, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8) << 2) | 0x01), 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8) << 2) | 0x01), 0x08, 255, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U1) << 2) | 0x01), 0x04, 255, 128, 0, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U1) << 2) | 0x01), 0x01, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2) << 2) | 0x01), 10,
                255, 255,
                128, 0,
                0, 0,
                0, 1,
                127, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2) << 2) | 0x01), 0x02, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4) << 2) | 0x01), 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4) << 2) | 0x01), 0x04, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U8) << 2) | 0x01), 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U8) << 2) | 0x01), 0x08, 255, 255, 255, 255, 255, 255, 255, 255,

                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.L) << 2) | 0x01), 24,

                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.A) << 2) | 0x02), 0x00, 0x03, 0x41, 0x42, 0x43,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.B) << 2) | 0x03), 0x00, 0x00, 0x05, 128, 255, 0, 1, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO) << 2) | 0x01), 0x08, 1, 0, 2, 0, 255, 0, 1, 1,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO) << 2) | 0x01), 0x01, 5,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F4) << 2) | 0x01), 20,
                127, 127, 255, 255,
                255, 127, 255, 255,
                255, 128, 0, 0,
                127, 128, 0, 0,
                0, 0, 0, 0,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F4) << 2) | 0x01), 0x04, 127, 127, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F8) << 2) | 0x01), 40,
                127, 0xEF, 255, 255, 255, 255, 255, 255,
                255, 0XEF, 255, 255, 255, 255, 255, 255,
                255, 0xF0, 0, 0, 0, 0, 0, 0,
                127, 0xF0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F8) << 2) | 0x01), 0x08, 127, 0xEF, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1) << 2) | 0x01), 0x04, 255, 128, 0, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1) << 2) | 0x01), 0x01, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2) << 2) | 0x01), 10,
                255, 255,
                128, 0,
                0, 0,
                0, 1,
                127, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2) << 2) | 0x01), 0x02, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I4) << 2) | 0x01), 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I4) << 2) | 0x01), 0x04, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8) << 2) | 0x01), 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8) << 2) | 0x01), 0x08, 255, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U1) << 2) | 0x01), 0x04, 255, 128, 0, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U1) << 2) | 0x01), 0x01, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2) << 2) | 0x01), 10,
                255, 255,
                128, 0,
                0, 0,
                0, 1,
                127, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2) << 2) | 0x01), 0x02, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4) << 2) | 0x01), 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4) << 2) | 0x01), 0x04, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U8) << 2) | 0x01), 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U8) << 2) | 0x01), 0x08, 255, 255, 255, 255, 255, 255, 255, 255

                };

            SECSItem secsItem1 = SECSItemFactory.GenerateSECSItem(input);
            SECSItem secsItem2 = SECSItemFactory.GenerateSECSItem(input, 0);

            Assert.AreEqual(secsItem1, secsItem2);
        }

        [Test()]
        public void Test02()
        {
            byte [] input1 = {
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.L) << 2) | 0x01), 25,

                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.A) << 2) | 0x02), 0x00, 0x03, 0x41, 0x42, 0x43,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.B) << 2) | 0x03), 0x00, 0x00, 0x05, 128, 255, 0, 1, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO) << 2) | 0x01), 0x08, 1, 0, 2, 0, 255, 0, 1, 1,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO) << 2) | 0x01), 0x01, 5,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F4) << 2) | 0x01), 20,
                127, 127, 255, 255,
                0, 0, 0, 1,
                255, 128, 0, 0,
                127, 128, 0, 0,
                0, 0, 0, 0,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F4) << 2) | 0x01), 0x04, 127, 127, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F8) << 2) | 0x01), 40,
                127, 0xEF, 255, 255, 255, 255, 255, 255,
                0, 0, 0, 0, 0, 0, 0, 1,
                255, 0xF0, 0, 0, 0, 0, 0, 0,
                127, 0xF0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F8) << 2) | 0x01), 0x08, 127, 0xEF, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1) << 2) | 0x01), 0x04, 255, 128, 0, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1) << 2) | 0x01), 0x01, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2) << 2) | 0x01), 10,
                255, 255,
                128, 0,
                0, 0,
                0, 1,
                127, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2) << 2) | 0x01), 0x02, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I4) << 2) | 0x01), 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I4) << 2) | 0x01), 0x04, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8) << 2) | 0x01), 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8) << 2) | 0x01), 0x08, 255, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U1) << 2) | 0x01), 0x04, 255, 128, 0, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U1) << 2) | 0x01), 0x01, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2) << 2) | 0x01), 10,
                255, 255,
                128, 0,
                0, 0,
                0, 1,
                127, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2) << 2) | 0x01), 0x02, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4) << 2) | 0x01), 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4) << 2) | 0x01), 0x04, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U8) << 2) | 0x01), 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U8) << 2) | 0x01), 0x08, 255, 255, 255, 255, 255, 255, 255, 255,

                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.L) << 2) | 0x01), 24,

                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.A) << 2) | 0x02), 0x00, 0x03, 0x41, 0x42, 0x43,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.B) << 2) | 0x03), 0x00, 0x00, 0x05, 128, 255, 0, 1, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO) << 2) | 0x01), 0x08, 1, 0, 2, 0, 255, 0, 1, 1,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO) << 2) | 0x01), 0x01, 5,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F4) << 2) | 0x01), 20,
                127, 127, 255, 255,
                0, 0, 0, 1,
                255, 128, 0, 0,
                127, 128, 0, 0,
                0, 0, 0, 0,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F4) << 2) | 0x01), 0x04, 127, 127, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F8) << 2) | 0x01), 40,
                127, 0xEF, 255, 255, 255, 255, 255, 255,
                0, 0, 0, 0, 0, 0, 0, 1,
                255, 0xF0, 0, 0, 0, 0, 0, 0,
                127, 0xF0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F8) << 2) | 0x01), 0x08, 127, 0xEF, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1) << 2) | 0x01), 0x04, 255, 128, 0, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1) << 2) | 0x01), 0x01, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2) << 2) | 0x01), 10,
                255, 255,
                128, 0,
                0, 0,
                0, 1,
                127, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2) << 2) | 0x01), 0x02, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I4) << 2) | 0x01), 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I4) << 2) | 0x01), 0x04, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8) << 2) | 0x01), 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8) << 2) | 0x01), 0x08, 255, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U1) << 2) | 0x01), 0x04, 255, 128, 0, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U1) << 2) | 0x01), 0x01, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2) << 2) | 0x01), 10,
                255, 255,
                128, 0,
                0, 0,
                0, 1,
                127, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2) << 2) | 0x01), 0x02, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4) << 2) | 0x01), 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4) << 2) | 0x01), 0x04, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U8) << 2) | 0x01), 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U8) << 2) | 0x01), 0x08, 255, 255, 255, 255, 255, 255, 255, 255

                };

            byte[] input2 = new byte[input1.Length + 10];
            Array.Copy(input1, 0, input2, 10, input1.Length);

            input2[0] = 255;
            input2[1] = 255;
            input2[2] = 255;
            input2[3] = 255;
            input2[4] = 255;
            input2[5] = 255;
            input2[6] = 255;
            input2[7] = 255;
            input2[8] = 255;
            input2[9] = 255;

            SECSItem secsItem1 = SECSItemFactory.GenerateSECSItem(input1);
            SECSItem secsItem2 = SECSItemFactory.GenerateSECSItem(input2, 10);

            Assert.AreEqual(secsItem1, secsItem2);
        }

        [Test()]
        public void Test03()
        {
            byte [] input1 = {
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.L) << 2) | 0x01), 25,

                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.A) << 2) | 0x02), 0x00, 0x03, 0x41, 0x42, 0x43,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.B) << 2) | 0x03), 0x00, 0x00, 0x05, 128, 255, 0, 1, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO) << 2) | 0x01), 0x08, 1, 0, 2, 0, 255, 0, 1, 1,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO) << 2) | 0x01), 0x01, 5,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F4) << 2) | 0x01), 20,
                127, 127, 255, 255,
                0, 0, 0, 1,
                255, 128, 0, 0,
                127, 128, 0, 0,
                0, 0, 0, 0,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F4) << 2) | 0x01), 0x04, 127, 127, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F8) << 2) | 0x01), 40,
                127, 0xEF, 255, 255, 255, 255, 255, 255,
                0, 0, 0, 0, 0, 0, 0, 1,
                255, 0xF0, 0, 0, 0, 0, 0, 0,
                127, 0xF0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F8) << 2) | 0x01), 0x08, 127, 0xEF, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1) << 2) | 0x01), 0x04, 255, 128, 0, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1) << 2) | 0x01), 0x01, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2) << 2) | 0x01), 10,
                255, 255,
                128, 0,
                0, 0,
                0, 1,
                127, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2) << 2) | 0x01), 0x02, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I4) << 2) | 0x01), 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I4) << 2) | 0x01), 0x04, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8) << 2) | 0x01), 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8) << 2) | 0x01), 0x08, 255, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U1) << 2) | 0x01), 0x04, 255, 128, 0, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U1) << 2) | 0x01), 0x01, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2) << 2) | 0x01), 10,
                255, 255,
                128, 0,
                0, 0,
                0, 1,
                127, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2) << 2) | 0x01), 0x02, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4) << 2) | 0x01), 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4) << 2) | 0x01), 0x04, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U8) << 2) | 0x01), 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U8) << 2) | 0x01), 0x08, 255, 255, 255, 255, 255, 255, 255, 255,

                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.L) << 2) | 0x01), 24,

                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.A) << 2) | 0x02), 0x00, 0x03, 0x41, 0x42, 0x43,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.B) << 2) | 0x03), 0x00, 0x00, 0x05, 128, 255, 0, 1, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO) << 2) | 0x01), 0x08, 1, 0, 2, 0, 255, 0, 1, 1,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO) << 2) | 0x01), 0x01, 5,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F4) << 2) | 0x01), 20,
                127, 127, 255, 255,
                0, 0, 0, 1,
                255, 128, 0, 0,
                127, 128, 0, 0,
                0, 0, 0, 0,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F4) << 2) | 0x01), 0x04, 127, 127, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F8) << 2) | 0x01), 40,
                127, 0xEF, 255, 255, 255, 255, 255, 255,
                0, 0, 0, 0, 0, 0, 0, 1,
                255, 0xF0, 0, 0, 0, 0, 0, 0,
                127, 0xF0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.F8) << 2) | 0x01), 0x08, 127, 0xEF, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1) << 2) | 0x01), 0x04, 255, 128, 0, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I1) << 2) | 0x01), 0x01, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2) << 2) | 0x01), 10,
                255, 255,
                128, 0,
                0, 0,
                0, 1,
                127, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I2) << 2) | 0x01), 0x02, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I4) << 2) | 0x01), 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I4) << 2) | 0x01), 0x04, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8) << 2) | 0x01), 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.I8) << 2) | 0x01), 0x08, 255, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U1) << 2) | 0x01), 0x04, 255, 128, 0, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U1) << 2) | 0x01), 0x01, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2) << 2) | 0x01), 10,
                255, 255,
                128, 0,
                0, 0,
                0, 1,
                127, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U2) << 2) | 0x01), 0x02, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4) << 2) | 0x01), 20,
                255, 255, 255, 255,
                128, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1,
                127, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U4) << 2) | 0x01), 0x04, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U8) << 2) | 0x01), 40,
                255, 255, 255, 255, 255, 255, 255, 255,
                128, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                127, 255, 255, 255, 255, 255, 255, 255,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.U8) << 2) | 0x01), 0x08, 255, 255, 255, 255, 255, 255, 255, 255

                };

            byte[] input2 = new byte[input1.Length + 10];
            Array.Copy(input1, 0, input2, 10, input1.Length);

            input2[0] = 255;
            input2[1] = 255;
            input2[2] = 255;
            input2[3] = 255;
            input2[4] = 255;
            input2[5] = 255;
            input2[6] = 255;
            input2[7] = 255;
            input2[8] = 255;
            input2[9] = 255;

            SECSItem secsItem1 = SECSItemFactory.GenerateSECSItem(input1);
            SECSItem secsItem2 = SECSItemFactory.GenerateSECSItem(input2, 9);

            Assert.AreNotEqual(secsItem1, secsItem2);
        }
    }
}
