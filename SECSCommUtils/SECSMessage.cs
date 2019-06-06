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
