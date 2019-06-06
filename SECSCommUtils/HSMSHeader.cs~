using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.CIMthetics.CSharpSECSTools.SECSCommUtils
{
	public class HSMSHeader : SECSHeader
	{
		/*
         * 
         */
		public UInt16 SessionID { get; set; } // Device ID
		public byte HeaderByte2 { get; set; } // If Data msg W-bit and Stream
		public byte HeaderByte3 { get; set; } // If Data msg Function
		public byte PType { get; set; }
		public byte SType { get; set; }

		public HSMSHeader( SECS1Header hdr )
		{
			this.SystemBytes = hdr.SystemBytes;
		}

		public HSMSHeader()
		{
			SessionID = 0;
			HeaderByte2 = 0;
			HeaderByte3 = 0;
			PType = 0;
			SType = 0;
		}

		public HSMSHeader(byte[] header)
		{
			SessionID = (UInt16)((header[0] << 8) + header[1]);
			HeaderByte2 = header[2];
			HeaderByte3 = header[3];
			PType = header[4];
			SType = header[5];
			SystemBytes = (UInt32)((header[6] << 24) + (header[7] << 16) + (header[8] << 8) + (header[9]));
		}

		override public byte[] EncodeForTransport()
		{
			byte[] temp = new byte[10];

			temp[0] = (byte)((SessionID & 0xFF00) >> 8);
			temp[1] = (byte)(SessionID & 0xFF);
			temp[2] = HeaderByte2;
			temp[3] = HeaderByte3;
			temp[4] = PType;
			temp[5] = SType;
			temp[6] = (byte)((SystemBytes & 0xFF000000) >> 24);
			temp[7] = (byte)((SystemBytes & 0x00FF0000) >> 16);
			temp[8] = (byte)((SystemBytes & 0x0000FF00) >> 8);
			temp[9] = (byte)(SystemBytes & 0x000000FF);

			return temp;
		}
	} // End class HSMSHeader

} // End namespace CIMthetics.SECSUtilities
