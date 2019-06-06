using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.CIMthetics.CSharpSECSTools.SECSCommUtils
{
	public class SECS1Header : SECSHeader
	{
		/*
         * Reverse Bit (R-Bit) - The reverse bit (R-bit) signifies the 
         * direction of a message. The R-bit is set to 0 for messages 
         * to the equipment and set to 1 for messages to the host. The 
         * R-bit is included in the header so that the direction of 
         * the message is contained in every block.
         */
		public bool RBit { get; set; }

		/*
         * Device ID - The device ID defines the source or destination
         * of the message depending upon the value of the R-bit as shown
         * in Table 3. Device identification is a property of the equipment
         * and must be settable according to Section 8. The host can view
         * the device ID as a logical identifier connected with a physical
         * device within the equipment. The host has no device ID.
         */
		public UInt16 DeviceID { get; set; }

		/*
         * Wait Bit (W-Bit) - The wait bit (W-bit) is used to indicate 
         * that the sender of a primary message expects a reply. A 
         * value of one in the W-bit means that a reply is expected. A
         * value of zero in the W-bit means that no reply is expected. 
         * The W-bit must be set to zero in all secondary messages. 
         * For multi-block messages, the sender must ensure that the W-bit
         * is the same in every block of the message.
         */
		public bool WBit { get; set; }

		/*
         * Message ID - The message ID identifies the format and content 
         * of the message being sent (the particular message is one of 
         * many possible for the device in question). The exact message 
         * content is equipment-dependent. The upper message ID is the 
         * most significant portion of the ID.
         * 
         * Primary Message - A primar y message is defined as any odd 
         * numbered message. An odd numbered message will have bit 1 of 
         * the lower message ID set to 1.
         * 
         * Secondary Message - A secondary message is defined as any even 
         * numbered message. An evennumbered message will have bit 1 of 
         * the lower message ID set to 0.
         * 
         * NOTE: In SECS-II, byte three of the header (excluding the W-bit) 
         * is known as the stream, and byte four of the header is known as 
         * the function. See SECS-II for more information.
         */
		//        public UInt16 MessageID { get; set; }
		public byte Stream { get; set; }
		public byte Function { get; set; }

		/*
         * End Bit (E-Bit) - The end bit (E-bit) is used to determine if a 
         * block is the last block of a message. A value of one in the 
         * E-bit means that the block is the last block. A value of zero 
         * means that more blocks are to follow.
         */
		public bool EBit { get; set; }

		/*
         * Block Number - A message sent as more than one block is called 
         * a multi-block message. The first block is given a block number 
         * of one, and the block number is incremented by one for each 
         * subsequent block until the entire message is sent. The blocks 
         * of a multi-block message are sent in order. In a single-block 
         * message, the block number must have a value of zero or one. The 
         * maximum block number is 32,767. The upper block number is the 
         * most significant portion of the block number.
         */
		public Int16 BlockNumber { get; set; }

		/*
         * System Bytes - The system bytes in the header of each message 
         * for a given device ID must satisfy the following requirements. 
         * 
         * Distinction - The system bytes of a primary message must be 
         * distinct from those of all currently open transactions 
         * initiated from the same end of the communications link. 
         * They must also be distinct from those of the most recently 
         * completed transaction. They must also be distinct from any 
         * system bytes of blocks that were not successfully sent since 
         * the last successful block send.
         * 
         * Reply Message - The system bytes of the reply message are 
         * required to be the same as the system bytes of the corresponding
         * primary message.
         * 
         * Multi-Block Messages - The system bytes of all blocks of a 
         * multi-block message must be the same.
         */
		//        public UInt32 SystemBytes { get; set; }

		public SECS1Header( HSMSHeader hdr )
		{
			this.SystemBytes = hdr.SystemBytes;
		}

		override public byte[] EncodeForTransport()
		{
			byte[] temp = new byte[10];

			temp[0] = (byte)((DeviceID & 0x7F00) >> 8);
			if ( RBit == true )
				temp[0] |= 0x80;
			else
				temp[0] &= 0x7F;

			temp[1] = (byte)(DeviceID & 0xFF);

			temp[2] = Stream;
			if (WBit == true)
				temp[2] |= 0x80;
			else
				temp[2] &= 0x7F;

			temp[3] = Function;

			temp[4] = (byte)((BlockNumber & 0x7F) >> 8);
			if ( EBit == true )
				temp[4] |= 0x80;
			else
				temp[4] &= 0x7F;
			temp[5] = (byte)(BlockNumber & 0xFF);

			temp[6] = (byte)((SystemBytes & 0xFF000000) >> 24);
			temp[7] = (byte)((SystemBytes & 0x00FF0000) >> 16);
			temp[8] = (byte)((SystemBytes & 0x0000FF00) >> 8);
			temp[9] = (byte)(SystemBytes & 0x000000FF);

			return temp;
		} // End override public byte[] EncodeForTransport()

	} // End class SECS1Header

} // End namespace CIMthetics.SECSUtilities
