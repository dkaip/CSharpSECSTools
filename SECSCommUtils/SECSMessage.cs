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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.CIMthetics.CSharpSECSTools.SECSItems;

namespace com.CIMthetics.CSharpSECSTools.SECSCommUtils
{
	public class SECSMessage
	{
		public SECSHeader Header { get; set; }
		public SECSItem Body { get; set; }
		public bool IsValidMessage { get; private set; }

		public SECSMessage(SECSHeader Header, SECSItem Body)
		{
			this.IsValidMessage = true;
			this.Header = Header;
			this.Body = Body;
		}

		public SECSMessage()
		{
			IsValidMessage = false;
			Header = null;
			Body = null;
		}


	} // End class SECSMessage

} // End namespace CIMthetics.SECSUtilities
