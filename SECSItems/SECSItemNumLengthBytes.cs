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

namespace com.CIMthetics.CSharpSECSTools.SECSItems
{
    /// <summary>
    /// This <c>class</c> represents the valid values for the number of length bytes for a <c>SECSItem</c>.
    /// This needed to be implemented in this manner instead of an enum because C# will automatically
    /// cast an <c>enum</c> to an <c>int</c> when used as a method argument.  This may cause the compiler
    /// to use the wrong method if the signatures will match because of this.
    /// </summary>
    public class SECSItemNumLengthBytes : IComparable
    {
        private int _value;
        private string _name;

        /// <summary>
        /// One length byte used.
        /// </summary>
        public static readonly SECSItemNumLengthBytes ONE =
            new SECSItemNumLengthBytes (1, "ONE");

        /// <summary>
        /// Two length bytes used.
        /// </summary>
        public static readonly SECSItemNumLengthBytes TWO =
            new SECSItemNumLengthBytes (2, "TWO");

        /// <summary>
        /// Three length bytes used.
        /// </summary>
        public static readonly SECSItemNumLengthBytes THREE =
            new SECSItemNumLengthBytes (3, "THREE");

        /// <summary>
        /// This class instance has not been initialized yet.
        /// </summary>
        public static readonly SECSItemNumLengthBytes NOT_INITIALIZED =
            new SECSItemNumLengthBytes (-1, "NOT_INITIALIZED");

        private SECSItemNumLengthBytes(int value, string name)
        {
            _value = value;
            _name = name;
        }

        /// <summary>
        /// Gets the value of this <c>SECSItemNumLengthBytes</c>.
        /// </summary>
        /// <returns>the value of the <c>SECSItemNumLengthBytes</c>.</returns>
        public int ValueOf ()
        {
            return _value;
        }

        /// <summary>
        /// Gets the value of this <c>SECSItemNumLengthBytes</c> as a <c>string</c>.
        /// </summary>
        /// <returns>the value of the <c>SECSItemNumLengthBytes</c> as a <c>string</c>.</returns>
        public override string ToString ()
        {
            return _name;
        }

        /// <summary>
        /// Compare two <c>SECSItemNumLengthBytes</c> objects to each other.
        /// </summary>
        /// <param name="obj">The <c>SECSItemNumLengthBytes</c> that is to be compared with this one.</param>
        /// <returns><c>0</c> if the <c>SECSItemNumLengthBytes</c> items have the same value, <c>-1</c> if this <c>SECSItemNumLengthBytes</c>
        /// is less than the one specified, and <c>+1</c> if this <c>SECSItemNumLengthBytes</c>
        /// is greater than the one specified</returns>
        public int CompareTo (object obj)
        {
            return _value.CompareTo (((SECSItemNumLengthBytes)obj)._value);
        }
    }
}
