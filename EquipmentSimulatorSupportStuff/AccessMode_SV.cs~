using System;
using System.Collections.Concurrent;
using EquipmentSimulatorSupportStuff.Exceptions;
using com.CIMthetics.CSharpSECSTools.SECSItems;

namespace EquipmentSimulatorSupportStuff.E87
{
	public class AccessMode_SV : SVID, ASECSItem
	{
		UInt32 svid	= 0;
		byte   accessMode = 0;

		public AccessMode_SV (enum A accessMode, UInt32 svid, ConcurrentDictionary<UInt32, SVID> svidMap)
		{
			bool result = svidMap.TryAdd(svid, this);
			if (result == false)
			{
				throw new DuplicateSVIDException("Duplicate SVID addition attemped to svidMap.  The offender is SVID "
					+ svid + " for AccessMode_SV for " + portID + ".");
			}

			this.accessMode = acccessMode;
			this.svid 	= svid;
		}

		public byte getAccessMode()
		{
			return accessMode;
		}

		public void setAccessMode(byte accessMode)
		{
			this.accessMode = accessMode;
		}

		public SECSItem getAsSECSItem()
		{
			return new U1SECSItem(accessMode);
		}

		public UInt32 getSVID()
		{
			return svid;
		}
	}
}

