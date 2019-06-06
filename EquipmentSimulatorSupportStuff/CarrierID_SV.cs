using System;

namespace EquipmentSimulatorSupportStuff.E87
{
	public class CarrierID_SV : SVID, ASECSItem
	{
		UInt32  svid;
		string 	carrierID;

		public CarrierID_SV (string carrierID, UInt32 svid, ConcurrentDictionary<UInt32, string> svidMap)
		{
			this.carrierID = carrierID;
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
}

