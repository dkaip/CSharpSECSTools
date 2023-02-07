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
    public class SECSItemNumLengthBytesTests
    {
        /*
        [Test()]
        public void testHowManyValues()
        {
            SECSItemNumLengthBytes [] values = (SECSItemNumLengthBytes[])Enum.GetValues (typeof (SECSItemNumLengthBytes));
            Assert.IsTrue(values.Length == 4);
        }
        */

        [Test()]
        public void testEnumValues()
        {
            Assert.IsTrue(SECSItemNumLengthBytes.ONE.ValueOf() == 1);
            Assert.IsTrue(SECSItemNumLengthBytes.TWO.ValueOf() == 2);
            Assert.IsTrue(SECSItemNumLengthBytes.THREE.ValueOf() == 3);
            Assert.IsTrue(SECSItemNumLengthBytes.NOT_INITIALIZED.ValueOf() == -1);
        }

        [Test()]
        public void testEnumValueStrings()
        {
            Assert.IsTrue(string.Equals(SECSItemNumLengthBytes.ONE.ToString(), "ONE"));
            Assert.IsTrue(string.Equals(SECSItemNumLengthBytes.TWO.ToString(), "TWO"));
            Assert.IsTrue(string.Equals(SECSItemNumLengthBytes.THREE.ToString(), "THREE"));
            Assert.IsTrue(string.Equals(SECSItemNumLengthBytes.NOT_INITIALIZED.ToString(), "NOT_INITIALIZED"));
        }
    }
}
