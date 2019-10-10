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
using SECSItems;
namespace com.CIMthetics.CSharpSECSTools.SECSItems
{
    /// <summary>
    /// This <c>class</c> represents the valid values for the number of length bytes for a <c>SECSItem</c>.
    /// This needed to be implemented in this manner instead of an enum because C# will automatically
    /// cast an <c>enum</c> to an <c>int</c> when used as a method argument.  This may cause the compiler
    /// to use the wrong method if the signatures will match because of this.
    /// </summary>
    public class SECSItemNumLengthBytes : Enumeration
    {
        public static readonly SECSItemNumLengthBytes ONE =
            new SECSItemNumLengthBytes (1, "ONE");

        public static readonly SECSItemNumLengthBytes TWO =
            new SECSItemNumLengthBytes (2, "TWO");

        public static readonly SECSItemNumLengthBytes THREE =
            new SECSItemNumLengthBytes (3, "THREE");

        public static readonly SECSItemNumLengthBytes NOT_INITIALIZED =
            new SECSItemNumLengthBytes (-1, "NOT_INITIALIZED");

        private SECSItemNumLengthBytes(int value, string name) : base(value, name)
        { }
    }

    /*
    /// <summary>
    /// This <c>enum</c> represents the valid values for the number of length bytes for a <c>SECSItem</c>.
    /// </summary>
    public enum SECSItemNumLengthBytes
    {
        /// <summary>
        /// Indicates 1 length byte.
        /// </summary>
        ONE = 1,

        /// <summary>
        /// Indicates 2 length bytes.
        /// </summary>
        TWO = 2,

        /// <summary>
        /// Indicates 3 length bytes.
        /// </summary>
        THREE = 3,
        NOT_INITIALIZED = -1
    }
    */
}
