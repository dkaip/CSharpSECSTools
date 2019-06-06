using System;
using System.Linq;
using NUnit.Framework;
using com.CIMthetics.CSharpSECSTools.SECSItems;

namespace SECSItemTests
{
	[TestFixture()]
	public class SECSItemFormatCodeTests
	{
		[Test()]
		public void testConvertingToNumber()
		{
			int result = 0;

			result = SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.L);
			Assert.IsTrue( result == 0x00 );

			result = SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.B);
			Assert.IsTrue( result == 0x08 );

			result = SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.BO);
			Assert.IsTrue( result == 0x09 );

			result = SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.A);
			Assert.IsTrue( result == 0x10 );

			result = SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.J8);
			Assert.IsTrue( result == 0x11 );

			result = SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.C2);
			Assert.IsTrue( result == 0x12 );

			result = SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I8);
			Assert.IsTrue( result == 0x18 );

			result = SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I1);
			Assert.IsTrue( result == 0x19 );

			result = SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I2);
			Assert.IsTrue( result == 0x1A );

			result = SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I4);
			Assert.IsTrue( result == 0x1C );

			result = SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.F8);
			Assert.IsTrue( result == 0x20 );

			result = SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.F4);
			Assert.IsTrue( result == 0x24 );

			result = SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U8);
			Assert.IsTrue( result == 0x28 );

			result = SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U1);
			Assert.IsTrue( result == 0x29 );

			result = SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U2);
			Assert.IsTrue( result == 0x2A );

			result = SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U4);
			Assert.IsTrue( result == 0x2C );

			result = SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.UNDEFINED);
			Assert.IsTrue( result == 0x3E );

			result = SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.HeaderOnly);
			Assert.IsTrue( result == 0x3F );

		}

		[Test()]
		public void testConvertingFromANumber()
		{
			SECSItemFormatCode formatCode;

			/*
         * Test the codes that are supposed to be there.
         */
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)0x00);
			Assert.IsTrue(formatCode == SECSItemFormatCode.L);

			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)0x08);
			Assert.IsTrue(formatCode == SECSItemFormatCode.B);

			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)0x09);
			Assert.IsTrue(formatCode == SECSItemFormatCode.BO);

			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)0x10);
			Assert.IsTrue(formatCode == SECSItemFormatCode.A);

			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)0x11);
			Assert.IsTrue(formatCode == SECSItemFormatCode.J8);

			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)0x12);
			Assert.IsTrue(formatCode == SECSItemFormatCode.C2);

			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)0x18);
			Assert.IsTrue(formatCode == SECSItemFormatCode.I8);

			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)0x19);
			Assert.IsTrue(formatCode == SECSItemFormatCode.I1);

			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)0x1A);
			Assert.IsTrue(formatCode == SECSItemFormatCode.I2);

			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)0x1C);
			Assert.IsTrue(formatCode == SECSItemFormatCode.I4);

			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)0x20);
			Assert.IsTrue(formatCode == SECSItemFormatCode.F8);

			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)0x24);
			Assert.IsTrue(formatCode == SECSItemFormatCode.F4);

			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)0x28);
			Assert.IsTrue(formatCode == SECSItemFormatCode.U8);

			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)0x29);
			Assert.IsTrue(formatCode == SECSItemFormatCode.U1);

			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)0x2A);
			Assert.IsTrue(formatCode == SECSItemFormatCode.U2);

			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)0x2C);
			Assert.IsTrue(formatCode == SECSItemFormatCode.U4);

			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)0x3E);
			Assert.IsTrue(formatCode == SECSItemFormatCode.UNDEFINED);

			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)0x3F);
			Assert.IsTrue(formatCode == SECSItemFormatCode.HeaderOnly);

			/*
         * Now we need to verify that we do not get a legitimate code from empty
         * elements in the table.
         * 
         * Yes, yes, it will blow up if a number is specified that is out of 
         * bounds, but, deal with it.
         */
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)1);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)2);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)3);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)4);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)5);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)6);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)7);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)10);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)11);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)12);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)13);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)14);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)15);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)19);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)20);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)21);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)22);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)23);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)27);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)29);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)30);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)31);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)33);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)34);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)35);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)37);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)38);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)39);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)43);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)45);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)46);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)47);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)48);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)49);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)50);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)51);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)52);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)53);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)54);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)55);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)56);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)57);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)58);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)59);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)60);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)61);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);
			formatCode = SECSItemFormatCodeFunctions.getSECSItemFormatCodeFromNumber((byte)62);
			Assert.IsTrue( formatCode == SECSItemFormatCode.UNDEFINED);

		}

		[Test()]
		public void testConvertingToString()
		{
			/*
         * This may seem like a silly test, but, hopefully it will prevent
         * the situation where some silly twit decides to override the
         * toString method.  If this happens it will most likely break
         * quite a bit of code "way up" the food chain.
         */
			Assert.IsTrue( SECSItemFormatCode.L.ToString().Equals("L") );
			Assert.IsTrue( SECSItemFormatCode.B.ToString().Equals("B") );
			Assert.IsTrue( SECSItemFormatCode.BO.ToString().Equals("BO") );
			Assert.IsTrue( SECSItemFormatCode.A.ToString().Equals("A") );
			Assert.IsTrue( SECSItemFormatCode.J8.ToString().Equals("J8") );
			Assert.IsTrue( SECSItemFormatCode.C2.ToString().Equals("C2") );
			Assert.IsTrue( SECSItemFormatCode.I8.ToString().Equals("I8") );
			Assert.IsTrue( SECSItemFormatCode.I1.ToString().Equals("I1") );
			Assert.IsTrue( SECSItemFormatCode.I2.ToString().Equals("I2") );
			Assert.IsTrue( SECSItemFormatCode.I4.ToString().Equals("I4") );
			Assert.IsTrue( SECSItemFormatCode.F8.ToString().Equals("F8") );
			Assert.IsTrue( SECSItemFormatCode.F4.ToString().Equals("F4") );
			Assert.IsTrue( SECSItemFormatCode.U8.ToString().Equals("U8") );
			Assert.IsTrue( SECSItemFormatCode.U1.ToString().Equals("U1") );
			Assert.IsTrue( SECSItemFormatCode.U2.ToString().Equals("U2") );
			Assert.IsTrue( SECSItemFormatCode.U4.ToString().Equals("U4") );
			Assert.IsTrue( SECSItemFormatCode.UNDEFINED.ToString().Equals("UNDEFINED") );
			Assert.IsTrue( SECSItemFormatCode.HeaderOnly.ToString().Equals("HeaderOnly") );

		}
	}
}

