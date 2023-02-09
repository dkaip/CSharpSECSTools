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
    /// <c>SECSHeader</c> objects.
    /// </summary>
    public class HeaderOutputConfig
    {
        /// <summary>
        /// This property controls how a <c>SECSHeader</c> is displayed in the
        /// case where a formatter is configured to output using XML.
        /// </summary>
        /// <value>
        /// If this property's value is <c>DisplayAsType.Elements</c> the message's
        /// header will be displayed using XML elements for the various members of
        /// the <c>SECSHeader</c>. If the value is <c>DisplayAsType.Attributes</c>
        /// the header will be display using XML attributes for the various members
        /// of the <c>SECSHeader</c>.
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
        /// This property controls how a <c>SECSMessage</c>'s stream and function
        /// are displayed when the output format is XML.
        /// </summary>
        /// <value>
        /// If <c>true</c>, a message's stream and function are output in a form
        /// similar to &quot;SxFy&quot;. For example, S1F13. If <c>false</c>
        /// the message's stream and function are output separately in either
        /// attribute or element form.
        /// </value>
        /// <remarks>
        /// This property's value has no effect if the output ends up being SML.
        /// <para>
        /// The default is <c>false</c>.
        /// </para>
        /// </remarks>
        public bool     DisplayMessageIdAsSxFy { get; set; } = false;

        /// <summary>
        /// This property controls whether or not the &quot;Device Id&quot; is
        /// displayed in the output.
        /// </summary>
        /// <value>
        /// If <c>true</c> a message's Device Id is include in the output.
        /// </value>
        /// <remarks>
        /// The default is <c>false</c>.
        /// </remarks>
        public bool     DisplayDeviceId { get; set; } = false;

        /// <summary>
        /// This property controls whether or not the value of the &quot;System Bytes&quot; are
        /// displayed in the output.
        /// </summary>
        /// <value>
        /// If <c>true</c> the value of the system bytes will be displayed in the output.
        /// </value>
        /// <remarks>
        /// The default is <c>false</c>.
        /// </remarks>
        public bool     DisplaySystemBytes { get; set; } = false;

        /// <summary>
        /// This property controls whether or not the value of the &quot;W-bit&quot; is
        /// displayed in the output.
        /// </summary>
        /// <value>
        /// If <c>true</c> the value of the W-bit will be displayed in the output.
        /// </value>
        /// <remarks>
        /// The value of this property is used when generating output in both SML and XML.
        /// <para>
        /// The default is <c>true</c>.
        /// </para>
        /// </remarks>
        public bool     DisplayWBit { get; set; } = true;

        /// <summary>
        /// This property controls whether or not HSMS control messages are
        /// displayed in the output.
        /// </summary>
        /// <value>
        /// If <c>true</c> control messages will be displayed in the output.
        /// </value>
        /// <remarks>
        /// The value of this property is used when the communication is going over an HSMS connection.
        /// <para>
        /// The default is <c>false</c>.
        /// </para>
        /// </remarks>
        public bool     DisplayControlMessages { get; set; } = false;

        /// <summary>
        /// Creates a <c>string</c> displaying the current &quot;state&quot; of this instance.
        /// This is mostly only useful for debugging.
        /// </summary>
        public override string ToString()
        {
            return  "DisplayAsElementsOrAttributes:" + DisplayAsElementsOrAttributes +
                    " DisplayMessageIdAsSxFy:" + DisplayMessageIdAsSxFy +
                    " DisplayDeviceId:" + DisplayDeviceId +
                    " DisplaySystemBytes:" + DisplaySystemBytes +
                    " DisplayWBit:" + DisplayWBit +
                    " DisplayControlMessages:" + DisplayControlMessages;
        }
    }
}
