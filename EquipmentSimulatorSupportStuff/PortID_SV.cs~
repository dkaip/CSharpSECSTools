using System;
using System.Collections.Concurrent;
using EquipmentSimulatorSupportStuff;
using com.CIMthetics.CSharpSECSTools.SECSItems;

namespace EquipmentSimulatorSupportStuff.E87
{
	public class PortID_SV : SVID, ASECSItem
	{
		UInt32  svid;
		string 	portID;

		public PortID_SV (string portID, UInt32 svid, ConcurrentDictionary<UInt32, ASECSItem> svidMap)
		{
			this.portID = portID;
			this.svid 	= svid;
		}

		public string getPortID()
		{
			return portID;
		}

		public SECSItem getAsSECSItem()
		{
			return new ASCIISECSItem(PortID_SV);
		}

		public UInt32 getSVID()
		{
			return svid;
		}
}

