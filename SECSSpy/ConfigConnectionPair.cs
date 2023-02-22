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

using com.CIMthetics.CSharpSECSTools.SECSCommUtils;

#nullable enable

namespace com.CIMthetics.CSharpSECSTools.SECSSpy
{
    public class ConfigConnectionPair
    {
        public SECSConnectionConfigInfo Endpoint1 { get; }
        public SECSConnectionConfigInfo Endpoint2 { get; }

        public ConfigConnectionPair(SECSConnectionConfigInfo Endpoint1, SECSConnectionConfigInfo Endpoint2)
        {
            this.Endpoint1 = Endpoint1;
            this.Endpoint2 = Endpoint2;
        }
    }
}