/*
 * Copyright 2023 Douglas Kaip
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

using System.Text;

using com.CIMthetics.CSharpSECSTools.SECSCommUtils;
using com.CIMthetics.CSharpSECSTools.SECSItems;

namespace com.CIMthetics.CSharpSECSTools.TextFormatter
{
	/// <summary>
	/// This is an <c>abstract</c> base class providing the framework for classes
    /// that will be able to turn <c>SECSMessage</c>s, <c>SECSHeader</c>s, and 
    /// <c>SECSITEM</c>s into a C# <c>string</c> suitable for output to a
    /// terminal and or a file.
    /// <para>
    /// Note: At this time the current max indentation level is 132 spaces.
    /// </para>
    /// </summary>
    public abstract class SECSFormatter
    {
        internal TextFormatterConfig configurationData;

        /// <summary>
        /// This property allows for the viewing or changing of the configured
        /// <c>AddTimestamp</c> parameter. See
        /// <see cref="com.CIMthetics.CSharpSECSTools.TextFormatter.TextFormatterConfig.AddTimestamp">TextFormatterConfig</see>
        /// for more information.
        /// </summary>
        public bool AddTimestamp
        {
            get
            {
                return configurationData.AddTimestamp;
            }
            
            set
            {
                configurationData.AddTimestamp = value;
            }
        }

        /// <summary>
        /// This property allows for the viewing or changing of the configured
        /// <c>TimestampFormat</c> parameter. See
        /// <see cref="com.CIMthetics.CSharpSECSTools.TextFormatter.TextFormatterConfig.TimestampFormat">TextFormatterConfig</see>
        /// for more information.
        /// </summary>
        public string TimestampFormat
        {
            get
            {
                return configurationData.TimestampFormat;
            }
            
            set
            {
                configurationData.TimestampFormat = value;
            }
        }

        /// <summary>
        /// This property allows for the viewing or changing of the configured
        /// <c>AddDirection</c> parameter. See
        /// <see cref="com.CIMthetics.CSharpSECSTools.TextFormatter.TextFormatterConfig.AddDirection">TextFormatterConfig</see>
        /// for more information.
        /// </summary>
        public bool AddDirection
        {
            get
            {
                return configurationData.AddDirection;
            }
            
            set
            {
                configurationData.AddDirection = value;
            }
        }

        /// <summary>
        /// This property allows for the viewing or changing of the configured
        /// <c>IndentAmount</c> parameter. See
        /// <see cref="com.CIMthetics.CSharpSECSTools.TextFormatter.TextFormatterConfig.IndentAmount">TextFormatterConfig</see>
        /// for more information.
        /// </summary>
        public int IndentAmount
        {
            get
            {
                return configurationData.IndentAmount;
            }
            
            set
            {
                configurationData.IndentAmount = value;
            }
        }

        internal readonly string[]  Whitespace;
        private int                 _currentIndentLevel = 0;

        /// <summary>
        /// This is the current level of indention that the formatter is
        /// currently operating with.
        /// </summary>
        public int                 CurrentIndentLevel
        {
            get { return _currentIndentLevel; }
            set
            {
                if (value >= configurationData.MaxIndentionSpaces)
                    _currentIndentLevel = configurationData.MaxIndentionSpaces - 1;
                else 
                    _currentIndentLevel = value;

                if (_currentIndentLevel < 0)
                    _currentIndentLevel = 0;
            }
        }

        internal SECSFormatter(TextFormatterConfig configurationData)
        {
            this.configurationData = configurationData;

            CurrentIndentLevel = 0;

            /*
                The Whitespace array is an array of strings containing spaces.
                Hopefully it will allow for much less memory intensive (think
                GC) prepending of spaces to output lines.
            */
            Whitespace = new string[configurationData.MaxIndentionSpaces + 1];
            for(int i = 0; i < Whitespace.Count(); i++)
            {
                StringBuilder sb = new StringBuilder();

                for(int j = 0; j < i; j++)
                {
                    sb.Append(' ');
                }

                Whitespace[i] = sb.ToString();
            }
        }

        /// <summary>
        /// Create a <c>string</c> representation of a <c>SECSMessage</c>
        /// suitable for output to a terminal or a file.
        /// </summary>
        /// <returns>
        /// A representation of the <c>SECSMessage</c> in <c>string</c> form that
        /// is suitable for output to a terminal or file.
        /// </returns>
        /// <param name="source">
        /// The &quot;source&quot; or producer of the message.
        /// </param>
        /// <param name="destination">
        /// The &quot;destination&quot; or consumer of the message.
        /// </param>
        /// <param name="secsMessage">
        /// The <c>SECSMessage</c> object to generate the <c>string</c>
        /// representation of.
        /// </param>
        /// <remarks>
        /// The <c>SECSMessage</c> object is not modified.
        /// </remarks>
        public abstract string GetSECSMessageAsText(string source, string destination, SECSMessage secsMessage);

        /// <summary>
        /// Create a <c>string</c> representation of a <c>SECSMessage</c>
        /// suitable for output to a terminal or a file.
        /// </summary>
        /// <param name="sb">
        /// A <c>StringBuilder</c> that the user creates and passes in to 
        /// receive the resulting <c>string</c> representation of the
        /// <c>SECSMessage</c>.
        /// </param>
        /// <param name="source">
        /// The &quot;source&quot; or producer of the message.
        /// into a <c>string</c>.
        /// </param>
        /// <param name="destination">
        /// The &quot;destination&quot; or consumer of the message.
        /// </param>
        /// <param name="secsMessage">
        /// The <c>SECSMessage</c> object to generate the <c>string</c>
        /// representation of.
        /// </param>
        /// <remarks>
        /// The <c>SECSMessage</c> object is not modified.
        /// </remarks>
        public abstract void GetSECSMessageAsText(StringBuilder sb, string source, string destination, SECSMessage secsMessage);

        /// <summary>
        /// Create a <c>string</c> representation of a <c>SECSHeader</c>
        /// suitable for output to a terminal or a file.
        /// </summary>
        /// <param name="sb">
        /// A <c>StringBuilder</c> that the user creates and passes in to 
        /// receive the resulting <c>string</c> representation of the
        /// <c>SECSHeader</c>.
        /// </param>
        /// <param name="secsHeader">
        /// The <c>SECSHeader</c> object to generate the <c>string</c>
        /// representation of.
        /// </param>
        /// <remarks>
        /// The <c>SECSHeader</c> object is not modified.
        /// </remarks>
        public abstract void GetHeaderAsText(StringBuilder sb, SECSHeader secsHeader);

        /// <summary>
        /// Create a <c>string</c> representation of a <c>SECSHeader</c>
        /// suitable for output to a terminal or a file.
        /// </summary>
        /// <returns>
        /// A representation of the <c>SECSHeader</c> in <c>string</c> form that
        /// is suitable for output to a terminal or file.
        /// </returns>
        /// <param name="secsHeader">
        /// The <c>SECSHeader</c> object to generate the <c>string</c>
        /// representation of.
        /// </param>
        /// <remarks>
        /// The <c>SECSHeader</c> object is not modified.
        /// </remarks>
        public abstract string GetHeaderAsText(SECSHeader secsHeader);

        /// <summary>
        /// Create a <c>string</c> representation of a <c>SECSItem</c>
        /// suitable for output to a terminal or a file.
        /// </summary>
        /// <param name="sb">
        /// A <c>StringBuilder</c> that the user creates and passes in to 
        /// receive the resulting <c>string</c> representation of the
        /// <c>SECSItem</c>.
        /// </param>
        /// <param name="secsItem">
        /// The <c>SECSItem</c> object to generate the <c>string</c>
        /// representation of.
        /// </param>
        /// <remarks>
        /// The <c>SECSItem</c> object is not modified.
        /// </remarks>
        public abstract void GetSECSItemAsText(StringBuilder sb, SECSItem secsItem);

        /// <summary>
        /// Create a <c>string</c> representation of a <c>SECSItem</c>
        /// suitable for output to a terminal or a file.
        /// </summary>
        /// <returns>
        /// A representation of the <c>SECSItem</c> in <c>string</c> form that
        /// is suitable for output to a terminal or file.
        /// </returns>
        /// <param name="secsItem">
        /// The <c>SECSItem</c> object to generate the <c>string</c>
        /// representation of.
        /// </param>
        /// <remarks>
        /// The <c>SECSItem</c> object is not modified.
        /// </remarks>
        public abstract string GetSECSItemAsText(SECSItem secsItem);

        /// <summary>
        /// Return the number of string digits a signed number will need when
        /// converted to a string.
        /// </summary>
        internal int GetSignedItemLength(Int64 item)
        {
            int itemLength = 0;
            if (item >= 0 && item < 10)
                itemLength = 1;
            else if (item > -10 && item < 0)
                itemLength = 2;
            else
            {
                int extra = 1;
                if (item < 0)
                {
                    // If item is < 0 we need to account for the - sign
                    extra = 2;
                }

                double number = (double)item;
                if (number < 0)
                    number *= -1.0;

                itemLength = (int)(Math.Log10(number)) + extra;
            }

            return itemLength;
        }

        /// <summary>
        /// Return the number of string digits a unsigned number will need when
        /// converted to a string.
        /// </summary>
        internal int GetUnsignedItemLength(UInt64 item)
        {
            int itemLength = 0;
            if (item >= 0 && item < 10)
                itemLength = 1;
            else
            {
                int extra = 1;
                if (item < 0)
                {
                    // If item is < 0 we need to account for the - sign
                    extra = 2;
                }

                double number = (double)item;

                itemLength = (int)(Math.Log10(number)) + extra;
            }

            return itemLength;
        }
    }
}