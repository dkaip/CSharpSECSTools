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
using com.CIMthetics.CSharpSECSTools.SECSStateMachines;

namespace com.CIMthetics.CSharpSECSTools.SECSStateMachinesTests;
[TestFixture ()]
public class SECSStateMachinesTests
{
    [Test ()]
    public void Test01 ()
    {
        // var exception = Assert.Catch (() => new SECSItemTest (SECSItemFormatCode.U1, -1));

        // Assert.IsInstanceOf<ArgumentException> (exception);

        // Assert.IsTrue (exception.Message.Contains ("The value for the length argument must be between 0 and 16777215 inclusive."));
    }
}
