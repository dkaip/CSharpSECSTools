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

namespace com.CIMthetics.CSharpSECSTools.TextFormatter
{
    /// <summary>
    /// The properties of this class control the output format of
    /// <c>SECSItem</c> objects or in other words the body or content of
    /// a SECS message.
    /// </summary>
    public class BodyOutputConfig
    {
        /// <summary>
        /// This property controls how a <c>SECSItem</c> is displayed in the
        /// case where a formatter is configured to output using XML.
        /// </summary>
        /// <value>
        /// If this property's value is <c>DisplayAsType.Elements</c> a message's
        /// body will be displayed using XML elements for the various <c>SECSItem</c>(s)
        /// elements therein. If the value is <c>DisplayAsType.Attributes</c>
        /// the body will be displayed using XML attributes for the various <c>SECSItem</c>(s)
        /// elements therein.
        /// </value>
        /// <remarks>
        /// This property's value has no effect if the output ends up being SML.
        /// <para>
        /// The default is to display as Attributes.
        /// </para>
        /// </remarks>
        public DisplayAsType    DisplayAsType { get; set; } = DisplayAsType.Attributes;

        /// <summary>
        /// This property is pretty much only here to support &quot;automatic&quot; creation
        /// from a <c>json</c> file.  It will ultimately set the <c>DisplayAsType</c>
        /// property.
        /// </summary>
        /// <value>
        /// The only permissible value is &quot;Elements&quot; or &quot;Attributes&quot;.
        /// </value>
        public string   DisplayAsElementsOrAttributes
        {
            get { return DisplayAsType.ToString(); }
            set
            {
                if (string.Equals(value, "Elements", StringComparison.OrdinalIgnoreCase) == true)
                    DisplayAsType = DisplayAsType.Elements;
                else if (string.Equals(value, "Attributes", StringComparison.OrdinalIgnoreCase) == true)
                    DisplayAsType = DisplayAsType.Attributes;
                else
                {
                    throw new ArgumentException("DisplayAsElementsOrAttributes must be either \"Elements\" or \"Attributes\".");
                }
            }
        }

        /// <summary>
        /// This property controls whether or not the number of length bytes is
        /// displayed in the output.
        /// </summary>
        /// <value>
        /// If <c>true</c> the number of length bytes is include in the output.
        /// </value>
        /// <remarks>
        /// The default is <c>false</c>.
        /// </remarks>
        public bool     DisplayNumberOfLengthBytes { get; set; } = false;

        /// <summary>
        /// This property controls whether or not the value of the length bytes is
        /// displayed in the output.
        /// </summary>
        /// <value>
        /// If <c>true</c> the value of the length bytes is include in the output.
        /// </value>
        /// <remarks>
        /// The default is <c>false</c>.
        /// </remarks>
        public bool     DisplayLengthByteValue { get; set; } = false;

        /// <summary>
        /// This property controls the maximum length of line(s) produced for output.
        /// </summary>
        /// <value>
        /// The maximum length in characters of output lines produced.
        /// </value>
        /// <remarks>
        /// <b>Warning:</b> If the length of this property is set too small an
        /// infinite loop may be entered.  Setting the value of this property to
        /// 50 or larger should be okay.
        /// <para>
        /// The default is <c>132</c> characters in length.
        /// </para>
        /// </remarks>
        public int      MaxOutputLineLength { get; set; } = 132;

        /// <summary>
        /// If the display format is SML this property controls whether or not &quot;count&quot;
        /// values are displayed in the output.  These &quot;count&quot; values indicate how
        /// many elements are in the individual <c>SECSItem</c>s.
        /// </summary>
        /// <value>
        /// If <c>true</c> the value of the number of a <c>SECSItem</c>s elements is include in the output.
        /// </value>
        /// <remarks>
        /// This property only has an effect on output if the display format is SML.
        /// <para>
        /// The default is <c>true</c>.
        /// </para>
        /// </remarks>
        public bool     DisplayCount { get; set; } = true;

        /// <summary>
        /// Creates a <c>string</c> displaying the current &quot;state&quot; of this instance.
        /// This is mostly only useful for debugging.
        /// </summary>
        public override string ToString()
        {
            return  "DisplayAsElementsOrAttributes:" + DisplayAsElementsOrAttributes +
                    " DisplayNumberOfLengthBytes:" + DisplayNumberOfLengthBytes +
                    " DisplayLengthByteValue:" + DisplayLengthByteValue +
                    " MaxOutputLineLength:" + MaxOutputLineLength +
                    " DisplayCount:" + DisplayCount;
        }
    }
}
