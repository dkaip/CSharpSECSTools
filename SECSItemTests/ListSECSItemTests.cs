﻿/*
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

//#nullable enable

namespace com.CIMthetics.CSharpSECSTools.SECSItemTests
{
    [TestFixture()]
    public class ListSECSItemTests
    {
        [Test()]
        public void Test00()
        {
            byte [] input = {
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.L) << 2) | 0x01), 25,

                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.A) << 2) | 0x01), 0x03, 0x41, 0x42, 0x43,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.B) << 2) | 0x01), 0x05, 128, 255, 0, 1, 127,
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

                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.A) << 2) | 0x01), 0x03, 0x41, 0x42, 0x43,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.B) << 2) | 0x01), 0x05, 128, 255, 0, 1, 127,
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

            SECSItem secsItem = SECSItemFactory.GenerateSECSItem(input);

            Assert.IsTrue(secsItem.GetType() == typeof(ListSECSItem));
        }

        [Test()]
        public void test01()
        {
            byte [] input = {
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.L) << 2) | 0x01), 25,

                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.A) << 2) | 0x01), 0x03, 0x41, 0x42, 0x43,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.B) << 2) | 0x01), 0x05, 128, 255, 0, 1, 127,
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

                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.A) << 2) | 0x01), 0x03, 0x41, 0x42, 0x43,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.B) << 2) | 0x01), 0x05, 128, 255, 0, 1, 127,
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
            
            ListSECSItem secsItem = (ListSECSItem)SECSItemFactory.GenerateSECSItem(input);

            SECSItem testElement = null;
            ListSECSItem expectedData1 = new ListSECSItem();
            ListSECSItem expectedData2 = new ListSECSItem();
            testElement = new ASCIISECSItem("ABC");
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BinarySECSItem(new byte [] { 128, 255, 0, 1, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BooleanArraySECSItem(new bool [] { true, false, true, false, true, false, true, true });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BooleanSECSItem(true);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F4ArraySECSItem(new float [] { Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F4SECSItem(Single.MaxValue);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F8ArraySECSItem(new double [] { Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F8SECSItem(Double.MaxValue);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I1ArraySECSItem(new sbyte [] { -1, -128, 0, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I1SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I2ArraySECSItem(new Int16 [] { -1, -32768, 0, 1, 32767 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I2SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I4ArraySECSItem(new Int32 [] { -1, -2147483648, 0, 1, 2147483647 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I4SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I8ArraySECSItem(new Int64 [] { -1, -9223372036854775808L, 0, 1, 9223372036854775807L });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I8SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U1ArraySECSItem(new byte [] { 255, 128, 0, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U1SECSItem(255);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U2ArraySECSItem(new UInt16 [] { 65535, 32768, 0, 1, 32767 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U2SECSItem(65535);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U4ArraySECSItem(new UInt32 [] { 0xFFFFFFFF, 2147483648, 0, 1, 2147483647 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U4SECSItem(0xFFFFFFFF);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U8ArraySECSItem(new UInt64 [] {0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000, 0x0000000000000001, 0x7FFFFFFFFFFFFFFF});
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U8SECSItem(0xFFFFFFFFFFFFFFFF);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);

            ListSECSItem innerList = new ListSECSItem(expectedData2);
            expectedData1.Add(innerList);

            ListSECSItem expectedResult = new ListSECSItem(expectedData1);

            //        System.out.println(secsItem.toString());
            //        System.out.println("\n/////////////////////////////////////////////////////////////////////////////////////////////////////\n");
            //        System.out.println(expectedResult.toString());
            Assert.IsTrue(secsItem.Equals(expectedResult));
        }

        [Test()]
        public void test02()
        {
            byte [] expectedResult = {
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.L) << 2) | 0x01), 25,

                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.A) << 2) | 0x01), 0x03, 0x41, 0x42, 0x43,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.B) << 2) | 0x01), 0x05, 128, 255, 0, 1, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO) << 2) | 0x01), 0x08, 1, 0, 1, 0, 1, 0, 1, 1,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO) << 2) | 0x01), 0x01, 1,
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

                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.A) << 2) | 0x01), 0x03, 0x41, 0x42, 0x43,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.B) << 2) | 0x01), 0x05, 128, 255, 0, 1, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO) << 2) | 0x01), 0x08, 1, 0, 1, 0, 1, 0, 1, 1,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO) << 2) | 0x01), 0x01, 1,
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

            SECSItem testElement = null;
            ListSECSItem expectedData1 = new ListSECSItem();
            ListSECSItem expectedData2 = new ListSECSItem();
            testElement = new ASCIISECSItem("ABC");
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BinarySECSItem(new byte [] { 128, 255, 0, 1, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BooleanArraySECSItem(new bool [] { true, false, true, false, true, false, true, true });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BooleanSECSItem(true);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F4ArraySECSItem(new float [] { Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F4SECSItem(Single.MaxValue);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F8ArraySECSItem(new double [] { Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F8SECSItem(Double.MaxValue);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I1ArraySECSItem(new sbyte [] { -1, -128, 0, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I1SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I2ArraySECSItem(new Int16 [] { -1, -32768, 0, 1, 32767 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I2SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I4ArraySECSItem(new Int32 [] { -1, -2147483648, 0, 1, 2147483647 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I4SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I8ArraySECSItem(new Int64 [] { -1, -9223372036854775808L, 0, 1, 9223372036854775807L });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I8SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U1ArraySECSItem(new byte [] { 255, 128, 0, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U1SECSItem(255);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U2ArraySECSItem(new UInt16 [] { 65535, 32768, 0, 1, 32767 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U2SECSItem((int)65535);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U4ArraySECSItem(new UInt32 [] { 0xFFFFFFFF, 2147483648, 0, 1, 2147483647 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U4SECSItem(0xFFFFFFFF);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U8ArraySECSItem(new UInt64 [] {0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000, 0x0000000000000001, 0x7FFFFFFFFFFFFFFF});
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U8SECSItem(0xFFFFFFFFFFFFFFFF);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);

            ListSECSItem innerList = new ListSECSItem(expectedData2);
            expectedData1.Add(innerList);

            ListSECSItem secsItem = new ListSECSItem(expectedData1);

            Assert.AreEqual(secsItem.EncodeForTransport(), expectedResult);
        }

        [Test()]
        public void test03()
        {
            byte [] expectedResult = {
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.L) << 2) | 0x02), 0, 25,

                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.A) << 2) | 0x01), 0x03, 0x41, 0x42, 0x43,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.B) << 2) | 0x01), 0x05, 128, 255, 0, 1, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO) << 2) | 0x01), 0x08, 1, 0, 1, 0, 1, 0, 1, 1,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO) << 2) | 0x01), 0x01, 1,
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

                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.L) << 2) | 0x02), 0, 24,

                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.A) << 2) | 0x01), 0x03, 0x41, 0x42, 0x43,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.B) << 2) | 0x01), 0x05, 128, 255, 0, 1, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO) << 2) | 0x01), 0x08, 1, 0, 1, 0, 1, 0, 1, 1,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO) << 2) | 0x01), 0x01, 1,
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

            SECSItem testElement = null;
            ListSECSItem expectedData1 = new ListSECSItem();
            ListSECSItem expectedData2 = new ListSECSItem();
            testElement = new ASCIISECSItem("ABC");
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BinarySECSItem(new byte [] { 128, 255, 0, 1, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BooleanArraySECSItem(new bool [] { true, false, true, false, true, false, true, true });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BooleanSECSItem(true);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F4ArraySECSItem(new float [] { Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F4SECSItem(Single.MaxValue);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F8ArraySECSItem(new double [] { Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F8SECSItem(Double.MaxValue);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I1ArraySECSItem(new sbyte [] { -1, -128, 0, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I1SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I2ArraySECSItem(new Int16 [] { -1, -32768, 0, 1, 32767 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I2SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I4ArraySECSItem(new Int32 [] { -1, -2147483648, 0, 1, 2147483647 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I4SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I8ArraySECSItem(new Int64 [] { -1, -9223372036854775808L, 0, 1, 9223372036854775807L });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I8SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U1ArraySECSItem(new byte [] { 255, 128, 0, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U1SECSItem(255);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U2ArraySECSItem(new UInt16 [] { 65535, 32768, 0, 1, 32767 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U2SECSItem(65535);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U4ArraySECSItem(new UInt32 [] { 0xFFFFFFFF, 2147483648, 0, 1, 2147483647 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U4SECSItem(0xFFFFFFFF);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U8ArraySECSItem(new UInt64 [] {0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000, 0x0000000000000001, 0x7FFFFFFFFFFFFFFF});
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U8SECSItem(0xFFFFFFFFFFFFFFFF);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);

            ListSECSItem innerList = new ListSECSItem(expectedData2, SECSItemNumLengthBytes.TWO);
            expectedData1.Add(innerList);

            ListSECSItem secsItem = new ListSECSItem(expectedData1, SECSItemNumLengthBytes.TWO);

            Assert.AreEqual(secsItem.EncodeForTransport(), expectedResult);
        }

        [Test()]
        public void test04()
        {
            byte [] expectedResult = {
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.L) << 2) | 0x03), 0, 0, 25,

                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.A) << 2) | 0x01), 0x03, 0x41, 0x42, 0x43,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.B) << 2) | 0x01), 0x05, 128, 255, 0, 1, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO) << 2) | 0x01), 0x08, 1, 0, 1, 0, 1, 0, 1, 1,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO) << 2) | 0x01), 0x01, 1,
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

                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.L) << 2) | 0x03), 0, 0, 24,

                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.A) << 2) | 0x01), 0x03, 0x41, 0x42, 0x43,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.B) << 2) | 0x01), 0x05, 128, 255, 0, 1, 127,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO) << 2) | 0x01), 0x08, 1, 0, 1, 0, 1, 0, 1, 1,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.BO) << 2) | 0x01), 0x01, 1,
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

            SECSItem testElement = null;
            ListSECSItem expectedData1 = new ListSECSItem();
            ListSECSItem expectedData2 = new ListSECSItem();
            testElement = new ASCIISECSItem("ABC");
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BinarySECSItem(new byte [] { 128, 255, 0, 1, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BooleanArraySECSItem(new bool [] { true, false, true, false, true, false, true, true });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BooleanSECSItem(true);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F4ArraySECSItem(new float [] { Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F4SECSItem(Single.MaxValue);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F8ArraySECSItem(new double [] { Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F8SECSItem(Double.MaxValue);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I1ArraySECSItem(new sbyte [] { -1, -128, 0, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I1SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I2ArraySECSItem(new Int16 [] { -1, -32768, 0, 1, 32767 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I2SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I4ArraySECSItem(new Int32 [] { -1, -2147483648, 0, 1, 2147483647 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I4SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I8ArraySECSItem(new Int64 [] { -1, -9223372036854775808L, 0, 1, 9223372036854775807L });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I8SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U1ArraySECSItem(new byte [] { 255, 128, 0, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U1SECSItem(255);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U2ArraySECSItem(new UInt16 [] { 65535, 32768, 0, 1, 32767 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U2SECSItem(65535);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U4ArraySECSItem(new UInt32 [] { 0xFFFFFFFF, 2147483648, 0, 1, 2147483647 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U4SECSItem(0xFFFFFFFF);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U8ArraySECSItem(new UInt64 [] {0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000, 0x0000000000000001, 0x7FFFFFFFFFFFFFFF});
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U8SECSItem(0xFFFFFFFFFFFFFFFF);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);

            ListSECSItem innerList = new ListSECSItem(expectedData2, SECSItemNumLengthBytes.THREE);
            expectedData1.Add(innerList);

            ListSECSItem secsItem = new ListSECSItem(expectedData1, SECSItemNumLengthBytes.THREE);

            Assert.AreEqual(secsItem.EncodeForTransport(), expectedResult);
        }

        [Test()]
        public void test05()
        {
            SECSItem testElement = null;
            ListSECSItem expectedData1 = new ListSECSItem();
            ListSECSItem expectedData2 = new ListSECSItem();
            testElement = new ASCIISECSItem("ABC");
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BinarySECSItem(new byte [] { 128, 255, 0, 1, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BooleanArraySECSItem(new bool [] { true, false, true, false, true, false, true, true });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BooleanSECSItem(true);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F4ArraySECSItem(new float [] { Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F4SECSItem(Single.MaxValue);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F8ArraySECSItem(new double [] { Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F8SECSItem(Double.MaxValue);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I1ArraySECSItem(new sbyte [] { -1, -128, 0, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I1SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I2ArraySECSItem(new Int16 [] { -1, -32768, 0, 1, 32767 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I2SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I4ArraySECSItem(new Int32 [] { -1, -2147483648, 0, 1, 2147483647 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I4SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I8ArraySECSItem(new Int64 [] { -1, -9223372036854775808L, 0, 1, 9223372036854775807L });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I8SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U1ArraySECSItem(new byte [] { 255, 128, 0, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U1SECSItem(255);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U2ArraySECSItem(new UInt16 [] { 65535, 32768, 0, 1, 32767 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U2SECSItem((int)65535);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U4ArraySECSItem(new UInt32 [] { 0xFFFFFFFF, 2147483648, 0, 1, 2147483647 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U4SECSItem(0xFFFFFFFF);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U8ArraySECSItem(new UInt64 [] {0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000, 0x0000000000000001, 0x7FFFFFFFFFFFFFFF});
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U8SECSItem(0xFFFFFFFFFFFFFFFF);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);

            ListSECSItem innerList = new ListSECSItem(expectedData2);
            expectedData1.Add(innerList);

            ListSECSItem secsItem = new ListSECSItem(expectedData1);

            SECSItem result = secsItem.GetElementAt("4");
            Assert.IsTrue(result.ItemFormatCode == SECSItemFormatCode.BO);
            Assert.IsTrue(((BooleanSECSItem)result).Value == true);
        }

        [Test()]
        public void test06()
        {
            SECSItem testElement = null;
            ListSECSItem expectedData1 = new ListSECSItem();
            ListSECSItem expectedData2 = new ListSECSItem();
            testElement = new ASCIISECSItem("ABC");
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BinarySECSItem(new byte [] { 128, 255, 0, 1, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BooleanArraySECSItem(new bool [] { true, false, true, false, true, false, true, true });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BooleanSECSItem(true);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F4ArraySECSItem(new float [] { Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F4SECSItem(Single.MaxValue);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F8ArraySECSItem(new double [] { Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F8SECSItem(Double.MaxValue);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I1ArraySECSItem(new sbyte [] { -1, -128, 0, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I1SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I2ArraySECSItem(new Int16 [] { -1, -32768, 0, 1, 32767 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I2SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I4ArraySECSItem(new Int32 [] { -1, -2147483648, 0, 1, 2147483647 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I4SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I8ArraySECSItem(new Int64 [] { -1, -9223372036854775808L, 0, 1, 9223372036854775807L });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I8SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U1ArraySECSItem(new byte [] { 255, 128, 0, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U1SECSItem(255);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U2ArraySECSItem(new UInt16 [] { 65535, 32768, 0, 1, 32767 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U2SECSItem(65535);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U4ArraySECSItem(new UInt32 [] { 0xFFFFFFFF, 2147483648, 0, 1, 2147483647 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U4SECSItem(0xFFFFFFFF);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U8ArraySECSItem(new UInt64 [] {0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000, 0x0000000000000001, 0x7FFFFFFFFFFFFFFF});
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U8SECSItem(0xFFFFFFFFFFFFFFFF);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);

            ListSECSItem innerList = new ListSECSItem(expectedData2);
            expectedData1.Add(innerList);
            expectedData1.Add(innerList);

            ListSECSItem secsItem = new ListSECSItem(expectedData1);

            SECSItem result = secsItem.GetElementAt("25.25.10");
            Assert.IsTrue(result.ItemFormatCode == SECSItemFormatCode.I1);
            Assert.IsTrue(((I1SECSItem)result).Value == -1);
        }
        /*
        TestContext.Out.WriteLine("Length a {0} length b {1}", secsItem.ToRawSECSItem().Length, expectedResult.Length);
            for (int i = 0; i<secsItem.ToRawSECSItem().Length; i++)
            {
                if (secsItem.ToRawSECSItem() [i] != expectedResult [i])
                {
                    Console.WriteLine("They differ at {0}", i);
                    TestContext.Out.WriteLine("They differ at {0} Left {1:X} Right {2:X}", i, secsItem.ToRawSECSItem() [i], expectedResult [i]);
                }
}
byte [] temp = secsItem.ToRawSECSItem();
*/
/*
        [Test()]
        public void test07()
        {
            SECSItem testElement = null;
            ListSECSItem expectedData1 = new ListSECSItem();
            ListSECSItem expectedData2 = new ListSECSItem();
            testElement = new ASCIISECSItem("ABC");
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BinarySECSItem(new byte [] { 128, 255, 0, 1, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BooleanArraySECSItem(new bool [] { true, false, true, false, true, false, true, true });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BooleanSECSItem(true);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F4ArraySECSItem(new float [] { Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F4SECSItem(Single.MaxValue);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F8ArraySECSItem(new double [] { Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F8SECSItem(Double.MaxValue);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I1ArraySECSItem(new sbyte [] { -1, -128, 0, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I1SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I2ArraySECSItem(new Int16 [] { -1, -32768, 0, 1, 32767 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I2SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I4ArraySECSItem(new Int32 [] { -1, -2147483648, 0, 1, 2147483647 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I4SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I8ArraySECSItem(new Int64 [] { -1, -9223372036854775808L, 0, 1, 9223372036854775807L });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I8SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U1ArraySECSItem(new byte [] { 255, 128, 0, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U1SECSItem(255);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U2ArraySECSItem(new UInt16 [] { 65535, 32768, 0, 1, 32767 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U2SECSItem(65535);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U4ArraySECSItem(new UInt32 [] { 0xFFFFFFFF, 2147483648, 0, 1, 2147483647 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U4SECSItem(0xFFFFFFFF);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U8ArraySECSItem(new UInt64 [] {0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000, 0x0000000000000001, 0x7FFFFFFFFFFFFFFF});
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U8SECSItem(0xFFFFFFFFFFFFFFFF);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);

            ListSECSItem innerList = new ListSECSItem(expectedData2);
            expectedData1.Add(innerList);
            expectedData1.Add(innerList);

            ListSECSItem secsItem = new ListSECSItem(expectedData1);

            Assert.IsTrue(secsItem.Value == expectedData1);
        }
*/

        [Test()]
        public void test08()
        {
            SECSItem testElement = null;
            ListSECSItem expectedData1 = new ListSECSItem();
            ListSECSItem expectedData2 = new ListSECSItem();
            testElement = new ASCIISECSItem("ABC");
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BinarySECSItem(new byte [] { 128, 255, 0, 1, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BooleanArraySECSItem(new bool [] { true, false, true, false, true, false, true, true });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BooleanSECSItem(true);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F4ArraySECSItem(new float [] { Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F4SECSItem(Single.MaxValue);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F8ArraySECSItem(new double [] { Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F8SECSItem(Double.MaxValue);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I1ArraySECSItem(new sbyte [] { -1, -128, 0, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I1SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I2ArraySECSItem(new Int16 [] { -1, -32768, 0, 1, 32767 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I2SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I4ArraySECSItem(new Int32 [] { -1, -2147483648, 0, 1, 2147483647 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I4SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I8ArraySECSItem(new Int64 [] { -1, -9223372036854775808L, 0, 1, 9223372036854775807L });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I8SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U1ArraySECSItem(new byte [] { 255, 128, 0, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U1SECSItem(255);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U2ArraySECSItem(new UInt16 [] { 65535, 32768, 0, 1, 32767 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U2SECSItem(65535);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U4ArraySECSItem(new UInt32 [] { 0xFFFFFFFF, 2147483648, 0, 1, 2147483647 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U4SECSItem(0xFFFFFFFF);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U8ArraySECSItem(new UInt64 [] {0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000, 0x0000000000000001, 0x7FFFFFFFFFFFFFFF});
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U8SECSItem(0xFFFFFFFFFFFFFFFF);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);

            ListSECSItem innerList = new ListSECSItem(expectedData2);
            expectedData1.Add(innerList);
            expectedData1.Add(innerList);

            ListSECSItem secsItem = new ListSECSItem(expectedData1);

            SECSItem result = secsItem.GetElementAt("-1");
            Assert.IsTrue(result == null);

            result = secsItem.GetElementAt("1000");
            Assert.IsTrue(result == null);

            result = secsItem.GetElementAt("25.25.10.10");
            Assert.IsTrue(result == null);
        }

        [Test()]
        public void test09()
        {
            ListSECSItem secsItem = new ListSECSItem(null);

            Assert.AreEqual(secsItem.ToString(), "Format:L Value: 0");
        }

        [Test()]
        public void test10()
        {
            SECSItem testElement = null;
            ListSECSItem expectedData1 = new ListSECSItem();
            ListSECSItem expectedData2 = new ListSECSItem();
            testElement = new ASCIISECSItem("ABC");
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BinarySECSItem(new byte [] { 128, 255, 0, 1, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BooleanArraySECSItem(new bool [] { true, false, true, false, true, false, true, true });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BooleanSECSItem(true);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F4ArraySECSItem(new float [] { Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F4SECSItem(Single.MaxValue);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F8ArraySECSItem(new double [] { Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F8SECSItem(Double.MaxValue);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I1ArraySECSItem(new sbyte [] { -1, -128, 0, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I1SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I2ArraySECSItem(new Int16 [] { -1, -32768, 0, 1, 32767 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I2SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I4ArraySECSItem(new Int32 [] { -1, -2147483648, 0, 1, 2147483647 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I4SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I8ArraySECSItem(new Int64 [] { -1, -9223372036854775808L, 0, 1, 9223372036854775807L });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I8SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U1ArraySECSItem(new byte [] { 255, 128, 0, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U1SECSItem(255);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U2ArraySECSItem(new UInt16 [] { 65535, 32768, 0, 1, 32767 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U2SECSItem((int)65535);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U4ArraySECSItem(new UInt32 [] { 0xFFFFFFFF, 2147483648, 0, 1, 2147483647 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U4SECSItem(0xFFFFFFFF);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U8ArraySECSItem(new UInt64 [] {0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000, 0x0000000000000001, 0x7FFFFFFFFFFFFFFF});
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U8SECSItem(0xFFFFFFFFFFFFFFFF);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);

            ListSECSItem innerList = new ListSECSItem(expectedData2);
            expectedData1.Add(innerList);
            expectedData1.Add(innerList);

            ListSECSItem secsItem = new ListSECSItem(expectedData1);

            String expectedResult = "Format:L Value: 26\n" +
                    "Format:A Value: ABC\n" +
                    "Format:B Value: Array\n" +
                    "Format:BO Value: Array\n" +
                    "Format:BO Value: True\n" +
                    "Format:F4 Value: Array\n" +
                    "Format:F4 Value: 3.4028235E+38\n" +
                    "Format:F8 Value: Array\n" +
                    "Format:F8 Value: 1.7976931348623157E+308\n" +
                    "Format:I1 Value: Array\n" +
                    "Format:I1 Value: -1\n" +
                    "Format:I2 Value: Array\n" +
                    "Format:I2 Value: -1\n" +
                    "Format:I4 Value: Array\n" +
                    "Format:I4 Value: -1\n" +
                    "Format:I8 Value: Array\n" +
                    "Format:I8 Value: -1\n" +
                    "Format:U1 Value: Array\n" +
                    "Format:U1 Value: 255\n" +
                    "Format:U2 Value: Array\n" +
                    "Format:U2 Value: 65535\n" +
                    "Format:U4 Value: Array\n" +
                    "Format:U4 Value: 4294967295\n" +
                    "Format:U8 Value: Array\n" +
                    "Format:U8 Value: 18446744073709551615\n" +
                    "Format:L Value: 24\n" +
                    "Format:A Value: ABC\n" +
                    "Format:B Value: Array\n" +
                    "Format:BO Value: Array\n" +
                    "Format:BO Value: True\n" +
                    "Format:F4 Value: Array\n" +
                    "Format:F4 Value: 3.4028235E+38\n" +
                    "Format:F8 Value: Array\n" +
                    "Format:F8 Value: 1.7976931348623157E+308\n" +
                    "Format:I1 Value: Array\n" +
                    "Format:I1 Value: -1\n" +
                    "Format:I2 Value: Array\n" +
                    "Format:I2 Value: -1\n" +
                    "Format:I4 Value: Array\n" +
                    "Format:I4 Value: -1\n" +
                    "Format:I8 Value: Array\n" +
                    "Format:I8 Value: -1\n" +
                    "Format:U1 Value: Array\n" +
                    "Format:U1 Value: 255\n" +
                    "Format:U2 Value: Array\n" +
                    "Format:U2 Value: 65535\n" +
                    "Format:U4 Value: Array\n" +
                    "Format:U4 Value: 4294967295\n" +
                    "Format:U8 Value: Array\n" +
                    "Format:U8 Value: 18446744073709551615\n" +
                    "Format:L Value: 24\n" +
                    "Format:A Value: ABC\n" +
                    "Format:B Value: Array\n" +
                    "Format:BO Value: Array\n" +
                    "Format:BO Value: True\n" +
                    "Format:F4 Value: Array\n" +
                    "Format:F4 Value: 3.4028235E+38\n" +
                    "Format:F8 Value: Array\n" +
                    "Format:F8 Value: 1.7976931348623157E+308\n" +
                    "Format:I1 Value: Array\n" +
                    "Format:I1 Value: -1\n" +
                    "Format:I2 Value: Array\n" +
                    "Format:I2 Value: -1\n" +
                    "Format:I4 Value: Array\n" +
                    "Format:I4 Value: -1\n" +
                    "Format:I8 Value: Array\n" +
                    "Format:I8 Value: -1\n" +
                    "Format:U1 Value: Array\n" +
                    "Format:U1 Value: 255\n" +
                    "Format:U2 Value: Array\n" +
                    "Format:U2 Value: 65535\n" +
                    "Format:U4 Value: Array\n" +
                    "Format:U4 Value: 4294967295\n" +
                    "Format:U8 Value: Array\n" +
                    "Format:U8 Value: 18446744073709551615";

            Assert.AreEqual(secsItem.ToString(), expectedResult);
        }

        [Test()]
        public void test11()
        {
            Assert.IsTrue(true);
            /*
            SECSItem testElement = null;
            LinkedList<SECSItem> expectedData1 = new LinkedList<SECSItem>();
            LinkedList<SECSItem> expectedData2 = new LinkedList<SECSItem>();
            testElement = new ASCIISECSItem("ABC");
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BinarySECSItem(new byte [] { 128, 255, 0, 1, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BooleanArraySECSItem(new bool [] { true, false, true, false, true, false, true, true });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BooleanSECSItem(true);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F4ArraySECSItem(new float [] { Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F4SECSItem(Single.MaxValue);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F8ArraySECSItem(new double [] { Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F8SECSItem(Double.MaxValue);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I1ArraySECSItem(new sbyte [] { -1, -128, 0, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I1SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I2ArraySECSItem(new Int16 [] { -1, -32768, 0, 1, 32767 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I2SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I4ArraySECSItem(new Int32 [] { -1, -2147483648, 0, 1, 2147483647 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I4SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I8ArraySECSItem(new Int64 [] { -1, -9223372036854775808L, 0, 1, 9223372036854775807L });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I8SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U1ArraySECSItem(new byte [] { 255, 128, 0, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U1SECSItem(255);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U2ArraySECSItem(new UInt16 [] { 65535, 32768, 0, 1, 32767 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U2SECSItem(65535);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U4ArraySECSItem(new UInt32 [] { 0xFFFFFFFF, 2147483648, 0, 1, 2147483647 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U4SECSItem(0xFFFFFFFF);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U8ArraySECSItem(new UInt64 [] {0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000, 0x0000000000000001, 0x7FFFFFFFFFFFFFFF});
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U8SECSItem(0xFFFFFFFFFFFFFFFF);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);

            ListSECSItem innerList = new ListSECSItem(expectedData2);
            expectedData1.Add(innerList);
            expectedData1.Add(innerList);

            ListSECSItem secsItem = new ListSECSItem(expectedData1);

            Assert.IsTrue(secsItem.GetHashCode() == -1671758977);
            */
        }

        [Test()]
        public void test13()
        {
            I4SECSItem secsItem1 = new I4SECSItem(2147483647);
            I4SECSItem secsItem2 = new I4SECSItem(2147483647);
            Assert.IsTrue(secsItem1.GetHashCode() == secsItem2.GetHashCode());
        }

        [Test()]
        public void test15()
        {
            I4SECSItem secsItem1 = new I4SECSItem(2147483647);
            Object secsItem2 = new F8SECSItem(2.141592D);
            Assert.IsFalse(secsItem1.Equals(secsItem2));
        }

        [Test()]
        public void test16()
        {
            SECSItem testElement = null;
            ListSECSItem expectedData1 = new ListSECSItem();
            ListSECSItem expectedData2 = new ListSECSItem();
            testElement = new ASCIISECSItem("ABC");
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BinarySECSItem(new byte [] { 128, 255, 0, 1, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BooleanArraySECSItem(new bool [] { true, false, true, false, true, false, true, true });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BooleanSECSItem(true);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F4ArraySECSItem(new float [] { Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F4SECSItem(Single.MaxValue);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F8ArraySECSItem(new double [] { Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F8SECSItem(Double.MaxValue);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I1ArraySECSItem(new sbyte [] { -1, -128, 0, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I1SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I2ArraySECSItem(new Int16 [] { -1, -32768, 0, 1, 32767 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I2SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I4ArraySECSItem(new Int32 [] { -1, -2147483648, 0, 1, 2147483647 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I4SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I8ArraySECSItem(new Int64 [] { -1, -9223372036854775808L, 0, 1, 9223372036854775807L });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I8SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U1ArraySECSItem(new byte [] { 255, 128, 0, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U1SECSItem(255);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U2ArraySECSItem(new UInt16 [] { 65535, 32768, 0, 1, 32767 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U2SECSItem((int)65535);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U4ArraySECSItem(new UInt32 [] { 0xFFFFFFFF, 2147483648, 0, 1, 2147483647 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U4SECSItem(0xFFFFFFFF);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U8ArraySECSItem(new UInt64 [] {0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000, 0x0000000000000001, 0x7FFFFFFFFFFFFFFF});
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U8SECSItem(0xFFFFFFFFFFFFFFFF);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);

            ListSECSItem innerList = new ListSECSItem(expectedData2);
            expectedData1.Add(innerList);
            expectedData1.Add(innerList);

            ListSECSItem secsItem = new ListSECSItem(expectedData1);

            Assert.IsTrue(secsItem.Equals(secsItem));
        }

        [Test()]
        public void test12()
        {
            SECSItem testElement = null;
            ListSECSItem expectedData1 = new ListSECSItem();
            ListSECSItem expectedData2 = new ListSECSItem();
            testElement = new ASCIISECSItem("ABC");
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BinarySECSItem(new byte [] { 128, 255, 0, 1, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BooleanArraySECSItem(new bool [] { true, false, true, false, true, false, true, true });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BooleanSECSItem(true);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F4ArraySECSItem(new float [] { Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F4SECSItem(Single.MaxValue);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F8ArraySECSItem(new double [] { Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F8SECSItem(Double.MaxValue);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I1ArraySECSItem(new sbyte [] { -1, -128, 0, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I1SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I2ArraySECSItem(new Int16 [] { -1, -32768, 0, 1, 32767 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I2SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I4ArraySECSItem(new Int32 [] { -1, -2147483648, 0, 1, 2147483647 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I4SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I8ArraySECSItem(new Int64 [] { -1, -9223372036854775808L, 0, 1, 9223372036854775807L });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I8SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U1ArraySECSItem(new byte [] { 255, 128, 0, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U1SECSItem(255);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U2ArraySECSItem(new UInt16 [] { 65535, 32768, 0, 1, 32767 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U2SECSItem(65535);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U4ArraySECSItem(new UInt32 [] { 0xFFFFFFFF, 2147483648, 0, 1, 2147483647 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U4SECSItem(0xFFFFFFFF);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U8ArraySECSItem(new UInt64 [] {0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000, 0x0000000000000001, 0x7FFFFFFFFFFFFFFF});
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U8SECSItem(0xFFFFFFFFFFFFFFFF);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);

            ListSECSItem innerList = new ListSECSItem(expectedData2);
            expectedData1.Add(innerList);
            expectedData1.Add(innerList);

            ListSECSItem secsItem = new ListSECSItem(expectedData1);

            SECSItem secsItem2 = null;

            Assert.IsFalse(secsItem.Equals(secsItem2));
        }

        [Test()]
        public void test17()
        {
            ListSECSItem secsItem = new ListSECSItem(null);

            Assert.IsTrue(secsItem.Count == 0);
        }

        [Test()]
        public void test18()
        {
            ListSECSItem secsItem = new ListSECSItem(new ListSECSItem());

            Assert.IsTrue(secsItem.Count == 0);
        }

        [Test()]
        public void test19()
        {
            ListSECSItem secsItem = new ListSECSItem(null, SECSItemNumLengthBytes.ONE);

            Assert.IsTrue(secsItem.Count == 0);
        }

        [Test()]
        public void test20()
        {
            ListSECSItem secsItem = new ListSECSItem(new ListSECSItem(), SECSItemNumLengthBytes.ONE);

            Assert.IsTrue(secsItem.Count == 0);
        }

        [Test()]
        public void test21()
        {
            ListSECSItem secsItem1 = new ListSECSItem(new ListSECSItem());
            Object secsItem2 = new F8SECSItem(2.141592D);
            Assert.IsFalse(secsItem1.Equals(secsItem2));
        }

        [Test()]
        public void test22()
        {
            byte [] input = {
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.L) << 2) | 0x01), 25,

                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.A) << 2) | 0x01), 0x03, 0x41, 0x42, 0x43,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.B) << 2) | 0x01), 0x05, 128, 255, 0, 1, 127,
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

                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.A) << 2) | 0x01), 0x03, 0x41, 0x42, 0x43,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.B) << 2) | 0x01), 0x05, 128, 255, 0, 1, 127,
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
            ListSECSItem secsItem = (ListSECSItem)SECSItemFactory.GenerateSECSItem(input);

            SECSItem testElement = null;
            ListSECSItem expectedData1 = new ListSECSItem();
            ListSECSItem expectedData2 = new ListSECSItem();
            testElement = new ASCIISECSItem("ABC");
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BinarySECSItem(new byte [] { 128, 255, 0, 1, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BooleanArraySECSItem(new bool [] { true, false, true, false, true, false, true, true });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BooleanSECSItem(true);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F4ArraySECSItem(new float [] { Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F4SECSItem(Single.MaxValue);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F8ArraySECSItem(new double [] { Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F8SECSItem(Double.MaxValue);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I1ArraySECSItem(new sbyte [] { -1, -128, 0, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I1SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I2ArraySECSItem(new Int16 [] { -1, -32768, 0, 1, 32767 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I2SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I4ArraySECSItem(new Int32 [] { -1, -2147483648, 0, 1, 2147483647 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I4SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I8ArraySECSItem(new Int64 [] { -1, -9223372036854775808L, 0, 1, 9223372036854775807L });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I8SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U1ArraySECSItem(new byte [] { 255, 128, 0, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U1SECSItem(255);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U2ArraySECSItem(new UInt16 [] { 65535, 32768, 0, 1, 32767 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U2SECSItem(65535);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U4ArraySECSItem(new UInt32 [] { 0xFFFFFFFF, 2147483648, 0, 1, 2147483647 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U4SECSItem(0xFFFFFFFF);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U8ArraySECSItem(new UInt64 [] {0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000, 0x0000000000000001, 0x7FFFFFFFFFFFFFFF});
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U8SECSItem(0xFFFFFFFFFFFFFFFF);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            // Add an extra one here
            testElement = new U8SECSItem(0xFFFFFFFFFFFFFFFF);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);

            ListSECSItem innerList = new ListSECSItem(expectedData2);
            expectedData1.Add(innerList);

            ListSECSItem expectedResult = new ListSECSItem(expectedData1);

            Assert.IsFalse(secsItem.Equals(expectedResult));
        }

        [Test()]
        public void test23()
        {
            byte [] input = {
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.L) << 2) | 0x01), 25,

                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.A) << 2) | 0x01), 0x03, 0x41, 0x42, 0x43,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.B) << 2) | 0x01), 0x05, 128, 255, 0, 1, 127,
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

                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.A) << 2) | 0x01), 0x03, 0x41, 0x42, 0x43,
                (byte)((SECSItemFormatCodeFunctions.GetNumberFromSECSItemFormatCode(SECSItemFormatCode.B) << 2) | 0x01), 0x05, 128, 255, 0, 1, 127,
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
                0, 0, 0, 2, // changed to 2 from 1
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
            ListSECSItem secsItem = (ListSECSItem)SECSItemFactory.GenerateSECSItem(input);

            SECSItem testElement = null;
            ListSECSItem expectedData1 = new ListSECSItem();
            ListSECSItem expectedData2 = new ListSECSItem();
            testElement = new ASCIISECSItem("ABC");
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BinarySECSItem(new byte [] { 128, 255, 0, 1, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BooleanArraySECSItem(new bool [] { true, false, true, false, true, false, true, true });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new BooleanSECSItem(true);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F4ArraySECSItem(new float [] { Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F4SECSItem(Single.MaxValue);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F8ArraySECSItem(new double [] { Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new F8SECSItem(Double.MaxValue);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I1ArraySECSItem(new sbyte [] { -1, -128, 0, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I1SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I2ArraySECSItem(new Int16 [] { -1, -32768, 0, 1, 32767 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I2SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I4ArraySECSItem(new Int32 [] { -1, -2147483648, 0, 1, 2147483647 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I4SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I8ArraySECSItem(new Int64 [] { -1, -9223372036854775808L, 0, 1, 9223372036854775807L });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new I8SECSItem(-1);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U1ArraySECSItem(new byte [] { 255, 128, 0, 127 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U1SECSItem(255);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U2ArraySECSItem(new UInt16 [] { 65535, 32768, 0, 1, 32767 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U2SECSItem((int)65535);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U4ArraySECSItem(new UInt32 [] { 0xFFFFFFFF, 2147483648, 0, 1, 2147483647 });
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U4SECSItem(0xFFFFFFFF);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U8ArraySECSItem(new UInt64 [] {0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000, 0x0000000000000001, 0x7FFFFFFFFFFFFFFF});
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);
            testElement = new U8SECSItem(0xFFFFFFFFFFFFFFFF);
            expectedData1.Add(testElement);
            expectedData2.Add(testElement);

            ListSECSItem innerList = new ListSECSItem(expectedData2);
            expectedData1.Add(innerList);

            ListSECSItem expectedResult = new ListSECSItem(expectedData1);

            Assert.IsFalse(secsItem.Equals(expectedResult));
        }

        [Test()]
        public void test24()
        {
            ListSECSItem eventReportData = new ListSECSItem();
			eventReportData.Add(new ASCIISECSItem("DATAID"));
			eventReportData.Add(new ASCIISECSItem("CEID"));

            ListSECSItem reports = new ListSECSItem();

			/////////////////////////////////////////////////////////////////////////////
            ListSECSItem report = new ListSECSItem();
			report.Add(new ASCIISECSItem("RPTID1"));

            ListSECSItem vidList = new ListSECSItem();

			vidList.Add(new U2SECSItem(1));
			vidList.Add(new U2ArraySECSItem(new UInt16[]{ 2, 3, 4, 5}));
			vidList.Add(new BooleanSECSItem(true));
			report.Add(vidList);

			reports.Add(report);

			/////////////////////////////////////////////////////////////////////////////
            report = new ListSECSItem();
			report.Add(new ASCIISECSItem("RPTID2"));

            vidList = new ListSECSItem();

			vidList.Add(new ASCIISECSItem());
			vidList.Add(new BinarySECSItem(new byte[]{ 0, 1, 127, 255, 0, 100}));
			vidList.Add(new F8SECSItem(3.141593D));
			vidList.Add(new BooleanArraySECSItem(new bool[]{ true, false, true, true, false}));
			vidList.Add(new I4SECSItem(0x7FFFFFFF));
			report.Add(vidList);

			reports.Add(report);

			/////////////////////////////////////////////////////////////////////////////
            report = new ListSECSItem();
			report.Add(new ASCIISECSItem("RPTID3"));

            vidList = new ListSECSItem();

			vidList.Add(new ListSECSItem());
			vidList.Add(new ASCIISECSItem());
			vidList.Add(new BinarySECSItem());
			vidList.Add(new BooleanArraySECSItem());
			vidList.Add(new I8ArraySECSItem());
			vidList.Add(new I1ArraySECSItem());
			vidList.Add(new I2ArraySECSItem());
			vidList.Add(new I4ArraySECSItem());
			vidList.Add(new F8ArraySECSItem());
			vidList.Add(new F4ArraySECSItem());
			vidList.Add(new U8ArraySECSItem());
			vidList.Add(new U1ArraySECSItem());
			vidList.Add(new U2ArraySECSItem());
			vidList.Add(new U4ArraySECSItem());
			report.Add(vidList);

			reports.Add(report);

			eventReportData.Add(reports);

            Dictionary<string, SECSItem> testDictionary = eventReportData.AsDictionary();

            bool result = false;
            SECSItem secsItem = null;

            result = testDictionary.TryGetValue("1", out secsItem);
            Assert.IsTrue(result == true);
            Assert.IsTrue(secsItem.GetType() == typeof(ASCIISECSItem));
            Assert.IsTrue(string.Equals(((ASCIISECSItem)secsItem).Value, "DATAID"));

            result = testDictionary.TryGetValue("3", out secsItem);
            Assert.IsTrue(result == true);
            Assert.IsTrue(secsItem.GetType() == typeof(ListSECSItem));
            Assert.IsTrue(((ListSECSItem)secsItem).LengthInBytes == 3);

            result = testDictionary.TryGetValue("3.2.1", out secsItem);
            Assert.IsTrue(result == true);
            Assert.IsTrue(secsItem.GetType() == typeof(ASCIISECSItem));
            Assert.IsTrue(string.Equals(((ASCIISECSItem)secsItem).Value, "RPTID2"));

            result = testDictionary.TryGetValue("3.3.1", out secsItem);
            Assert.IsTrue(result == true);
            Assert.IsTrue(secsItem.GetType() == typeof(ASCIISECSItem));
            Assert.IsTrue(string.Equals(((ASCIISECSItem)secsItem).Value, "RPTID3"));

            result = testDictionary.TryGetValue("3.3.2", out secsItem);
            Assert.IsTrue(result == true);
            Assert.IsTrue(secsItem.GetType() == typeof(ListSECSItem));
            Assert.IsTrue(((ListSECSItem)secsItem).LengthInBytes == 14);

            result = testDictionary.TryGetValue("3.3.2.1", out secsItem);
            Assert.IsTrue(result == true);
            Assert.IsTrue(secsItem.GetType() == typeof(ListSECSItem));
            Assert.IsTrue(((ListSECSItem)secsItem).LengthInBytes == 0);

            result = testDictionary.TryGetValue("3.3.2.9", out secsItem);
            Assert.IsTrue(result == true);
            Assert.IsTrue(secsItem.GetType() == typeof(F8ArraySECSItem));
            Assert.IsTrue(((F8ArraySECSItem)secsItem).LengthInBytes == 0);

            result = testDictionary.TryGetValue("3.2.2.2", out secsItem);
            Assert.IsTrue(result == true);
            Assert.IsTrue(secsItem.GetType() == typeof(BinarySECSItem));
            Assert.IsTrue(((BinarySECSItem)secsItem).LengthInBytes == 6);

            result = testDictionary.TryGetValue("3.1.2.2", out secsItem);
            Assert.IsTrue(result == true);
            Assert.IsTrue(secsItem.GetType() == typeof(U2ArraySECSItem));
            Assert.IsTrue(((U2ArraySECSItem)secsItem).LengthInBytes == 8);
            Assert.IsTrue(((U2ArraySECSItem)secsItem).Value[0] == 2);
            Assert.IsTrue(((U2ArraySECSItem)secsItem).Value[1] == 3);
            Assert.IsTrue(((U2ArraySECSItem)secsItem).Value[2] == 4);
            Assert.IsTrue(((U2ArraySECSItem)secsItem).Value[3] == 5);

        }

    }
}

