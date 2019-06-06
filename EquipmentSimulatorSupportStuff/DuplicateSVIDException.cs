using System;

namespace EquipmentSimulatorSupportStuff.Exceptions
{
	public class DuplicateSVIDException : Exception
	{
		public DuplicateSVIDException () {}

		public DuplicateSVIDException(string nessage) : base(Message) {}

		public DuplicateSVIDException(string message, Exception inner) : base(message, inner) {}
	}
}

