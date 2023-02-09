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

namespace com.CIMthetics.CSharpSECSTools.TextFormatter
{
    /// <summary>
    /// This class is used as the argument of the <c>CreateFormatter</c>
    /// method of the <c>SECSFormatterFactory</c> which creates a
    /// <c>SECSFormatter</c> object that is implemented to produce logging output
    /// as either SML or XML formatted text.
    /// </summary>
    public class TextFormatterConfig
    {
        /// <summary>
        /// Indicates whether or not a timestamp should be added to the 
        /// generated output.
        /// </summary>
        /// <value>
        /// If <c>true</c> a timestamp will be included in the 
        /// generated output.
        /// </value>
        /// <remarks>
        /// The default value is <c>true</c>.
        /// </remarks>
        public bool         AddTimestamp { get; set; } = true;

        /// <summary>
        /// If the <c>AddTimestamp</c> property is set to <c>true</c> this is the output
        /// formatting string that is applied to the timestamp as it is 
        /// converted to textual form.
        /// </summary>
        /// <value>
        /// A format string to be applied to the timestamp.
        /// </value>
        /// <remarks>
        /// The default value is &quot;yyyy-MM-ddTHH:mm:ss.fff&quot;.
        /// </remarks>
        public string       TimestampFormat { get; set; } = "yyyy-MM-ddTHH:mm:ss.fff";

        /// <summary>
        /// Indicates whether or not message direction information should be 
        /// added to the output.
        /// </summary>
        /// <value>
        /// If <c>true</c> message direction information will be included in 
        /// the generated output.
        /// </value>
        /// <remarks>
        /// The default value is <c>true</c>.
        /// </remarks>
        public bool         AddDirection { get; set; } = true;

        /// <summary>
        /// The number of spaces to be used when indentation is required in the
        /// generated output.
        /// </summary>
        /// <value>
        /// The number of spaces to be used when indentation is required in the
        /// generated output.
        /// </value>
        /// <remarks>
        /// The default value is 2.
        /// </remarks>
        public int          IndentAmount { get; set; } = 2;

        /// <summary>
        /// This sets the maximum number of spaces available to be used to provide
        /// indentation for output text generation.
        /// </summary>
        /// <value>
        /// The maximum number of spaces available for indentation.
        /// </value>
        /// <remarks>
        /// This may sound stupid, but, how can you run out of spaces? 
        /// Working with <c>string</c>s tends to 
        /// produce a lot of garbage that the garbage collector will need to
        /// deal with.  Since many of the programs that use this API may be
        /// executing for a long time and generating a lot of output
        /// the spaces required for indentation
        /// have been preallocated in a <c>string[]</c>.
        /// <para>
        /// Unlike most other properties this property is only used during
        /// the construction of a formatter.  Once created this property
        /// is not used again. Thus, changing its value will not have
        /// any effect.
        /// </para>
        /// <para>
        /// The default value is 48.  Specifying an overly large value for
        /// this property does nothing but use more memory.  If the value
        /// is too small a <exception cref="System.IndexOutOfRangeException"/>
        /// will be thrown at the point the <c>CurrentIndentLevel</c> exceeds
        /// the value of this property.  See
        /// <see cref="com.CIMthetics.CSharpSECSTools.TextFormatter.SECSFormatter.CurrentIndentLevel">SECSFormatter</see>
        /// for more information.
        /// </para>
        /// </remarks>
        public int          MaxIndentionSpaces { get; set; } = 48;
        private string      _loggingOutputFormat = "XML";

        /// <summary>
        /// This property specifies the kind of output that a
        /// formatter it to produce.
        /// </summary>
        /// <remarks>
        /// The acceptable values are &quot;SML&quot; or &quot;XML&quot;
        /// </remarks>
        public string       LoggingOutputFormat
        {
            get { return _loggingOutputFormat; }
            set
            {
                if (string.Compare(value, "XML", true) == 0 || string.Compare(value, "SML", true) == 0)
                {
                    _loggingOutputFormat = value.ToUpper();
                }
                else
                {
                    throw new ArgumentException("LoggingOutputFormat may only have a value of either \"SML\" or \"XML\"");
                }
            }
        }

        /// <summary>
        /// This property specifies the output format information for
        /// a formatter when it is producing output in the SML format.
        /// </summary>
        public SMLOutputConfig SMLOutputConfig { get; set; }

        /// <summary>
        /// This property specifies the output format information for
        /// a formatter when it is producing output in the XML format.
        /// </summary>
        public XMLOutputConfig XMLOutputConfig { get; set; }

        /// <summary>
        /// This property returns the current output configuration 
        /// (for <c>SECSHeader</c>s) that this formatter is currently
        /// working with.
        /// </summary>
        /// <value>
        /// If the <c>LoggingOutputFormat</c> property value is &quot;SML&quot;
        /// then the value returned is the value of the <c>SMLOutputConfig.HeaderOutputConfig</c>
        /// property.  Otherwise the value returned is the value of the
        /// <c>XMLOutputConfig.HeaderOutputConfig</c> property.
        /// </value>
        public HeaderOutputConfig HeaderOutputConfig
        {
            get
            {
                if (string.Equals(LoggingOutputFormat, "SML"))
                {
                    return SMLOutputConfig.HeaderOutputConfig;
                }
                else
                {
                    return XMLOutputConfig.HeaderOutputConfig;
                }
            }
        }

        /// <summary>
        /// This property returns the current output configuration 
        /// (for <c>SECSItem</c>s) that this formatter is currently
        /// working with.
        /// </summary>
        /// <value>
        /// If the <c>LoggingOutputFormat</c> property value is &quot;SML&quot;
        /// then the value returned is the value of the <c>SMLOutputConfig.BodyOutputConfig</c>
        /// property.  Otherwise the value returned is the value of the
        /// <c>XMLOutputConfig.BodyOutputConfig</c> property.
        /// </value>
        public BodyOutputConfig   BodyOutputConfig
        {
            get
            {
                if (string.Equals(LoggingOutputFormat, "SML"))
                {
                    return SMLOutputConfig.BodyOutputConfig;
                }
                else
                {
                    return XMLOutputConfig.BodyOutputConfig;
                }
            }
        }

        /// <summary>
        /// Creates a <c>string</c> displaying the current &quot;state&quot; of this instance.
        /// This is mostly only useful for debugging.
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("AddTimestamp:");
            sb.AppendLine(AddTimestamp.ToString());

            sb.Append("TimestampFormat:");
            sb.AppendLine(TimestampFormat);

            sb.Append("AddDirection:");
            sb.AppendLine(AddDirection.ToString());

            sb.Append("LoggingOutputFormat:");
            sb.AppendLine(LoggingOutputFormat);

            sb.AppendLine("SMLOutputConfig:");
            sb.AppendLine(SMLOutputConfig.ToString());

            sb.AppendLine("XMLOutputConfig:");
            sb.AppendLine(XMLOutputConfig.ToString());

            return sb.ToString();
        }
    }
}
