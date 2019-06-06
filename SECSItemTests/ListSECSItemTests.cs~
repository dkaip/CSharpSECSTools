using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using com.CIMthetics.CSharpSECSTools.SECSItems;

namespace SECSItemTests
{
	[TestFixture()]
	public class ListSECSItemTests
	{
		[Test()]
		public void test01()
		{
			byte[] input = { 
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.L ) << 2) | 0x01), 25,

				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.A ) << 2) | 0x01), 0x03, 0x41, 0x42, 0x43,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.B ) << 2) | 0x01), 0x05, 128, 255, 0, 1, 127,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x01), 0x08, 1, 0, 2, 0, 255, 0, 1, 1,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x01), 0x01, 5,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.F4 ) << 2) | 0x01), 20, 
				127, 127, 255, 255,
				255, 127, 255, 255,
				255, 128, 0, 0,
				127, 128, 0, 0,
				0, 0, 0, 0,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.F4 ) << 2) | 0x01), 0x04, 127, 127, 255, 255,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.F8 ) << 2) | 0x01), 40, 
				127, 0xEF, 255, 255, 255, 255, 255, 255,
				255, 0XEF, 255, 255, 255, 255, 255, 255,
				255, 0xF0, 0, 0, 0, 0, 0, 0,
				127, 0XF0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.F8 ) << 2) | 0x01), 0x08, 127, 0xEF, 255, 255, 255, 255, 255, 255,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I1 ) << 2) | 0x01), 0x04, 255, 128, 0, 127,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I1 ) << 2) | 0x01), 0x01, 255,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I2 ) << 2) | 0x01), 10,
				255, 255,
				128, 0,
				0, 0,
				0, 1,
				127, 255,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I2 ) << 2) | 0x01), 0x02, 255, 255,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x01), 20,
				255, 255, 255, 255,
				128, 0, 0, 0,
				0, 0, 0, 0,
				0, 0, 0, 1,
				127, 255, 255, 255,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x01), 0x04, 255, 255, 255, 255,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I8 ) << 2) | 0x01), 40,
				255, 255, 255, 255, 255, 255, 255, 255,
				128, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 1,
				127, 255, 255, 255, 255, 255, 255, 255,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I8 ) << 2) | 0x01), 0x08, 255, 255, 255, 255, 255, 255, 255, 255,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U1 ) << 2) | 0x01), 0x04, 255, 128, 0, 127,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U1 ) << 2) | 0x01), 0x01, 255,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U2 ) << 2) | 0x01), 10,
				255, 255,
				128, 0,
				0, 0,
				0, 1,
				127, 255,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U2 ) << 2) | 0x01), 0x02, 255, 255,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U4 ) << 2) | 0x01), 20,
				255, 255, 255, 255,
				128, 0, 0, 0,
				0, 0, 0, 0,
				0, 0, 0, 1,
				127, 255, 255, 255,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U4 ) << 2) | 0x01), 0x04, 255, 255, 255, 255,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U8 ) << 2) | 0x01), 40,
				255, 255, 255, 255, 255, 255, 255, 255,
				128, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 1,
				127, 255, 255, 255, 255, 255, 255, 255,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U8 ) << 2) | 0x01), 0x08, 255, 255, 255, 255, 255, 255, 255, 255,

				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.L ) << 2) | 0x01), 24,

				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.A ) << 2) | 0x01), 0x03, 0x41, 0x42, 0x43,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.B ) << 2) | 0x01), 0x05, 128, 255, 0, 1, 127,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x01), 0x08, 1, 0, 2, 0, 255, 0, 1, 1,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x01), 0x01, 5,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.F4 ) << 2) | 0x01), 20, 
				127, 127, 255, 255,
				255, 127, 255, 255,
				255, 128, 0, 0,
				127, 128, 0, 0,
				0, 0, 0, 0,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.F4 ) << 2) | 0x01), 0x04, 127, 127, 255, 255,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.F8 ) << 2) | 0x01), 40, 
				127, 0xEF, 255, 255, 255, 255, 255, 255,
				255, 0XEF, 255, 255, 255, 255, 255, 255,
				255, 0XF0, 0, 0, 0, 0, 0, 0,
				127, 0XF0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.F8 ) << 2) | 0x01), 0x08, 127, 0xEF, 255, 255, 255, 255, 255, 255,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I1 ) << 2) | 0x01), 0x04, 255, 128, 0, 127,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I1 ) << 2) | 0x01), 0x01, 255,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I2 ) << 2) | 0x01), 10,
				255, 255,
				128, 0,
				0, 0,
				0, 1,
				127, 255,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I2 ) << 2) | 0x01), 0x02, 255, 255,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x01), 20,
				255, 255, 255, 255,
				128, 0, 0, 0,
				0, 0, 0, 0,
				0, 0, 0, 1,
				127, 255, 255, 255,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x01), 0x04, 255, 255, 255, 255,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I8 ) << 2) | 0x01), 40,
				255, 255, 255, 255, 255, 255, 255, 255,
				128, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 1,
				127, 255, 255, 255, 255, 255, 255, 255,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I8 ) << 2) | 0x01), 0x08, 255, 255, 255, 255, 255, 255, 255, 255,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U1 ) << 2) | 0x01), 0x04, 255, 128, 0, 127,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U1 ) << 2) | 0x01), 0x01, 255,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U2 ) << 2) | 0x01), 10,
				255, 255,
				128, 0,
				0, 0,
				0, 1,
				127, 255,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U2 ) << 2) | 0x01), 0x02, 255, 255,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U4 ) << 2) | 0x01), 20,
				255, 255, 255, 255,
				128, 0, 0, 0,
				0, 0, 0, 0,
				0, 0, 0, 1,
				127, 255, 255, 255,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U4 ) << 2) | 0x01), 0x04, 255, 255, 255, 255,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U8 ) << 2) | 0x01), 40,
				255, 255, 255, 255, 255, 255, 255, 255,
				128, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 1,
				127, 255, 255, 255, 255, 255, 255, 255,
				(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U8 ) << 2) | 0x01), 0x08, 255, 255, 255, 255, 255, 255, 255, 255

			};
			ListSECSItem secsItem = new ListSECSItem(input, 0);

			SECSItem testElement = null;
			LinkedList<SECSItem> expectedData1 = new LinkedList<SECSItem>();
			LinkedList<SECSItem> expectedData2 = new LinkedList<SECSItem>();
			testElement = new ASCIISECSItem("ABC");
			expectedData1.AddLast(testElement);
			expectedData2.AddLast(testElement);
			testElement = new BinarySECSItem(new byte[]{128, 255, 0, 1, 127});
			expectedData1.AddLast(testElement);
			expectedData2.AddLast(testElement);
			testElement = new BooleanArraySECSItem(new bool[]{true, false, true, false, true, false, true, true});
			expectedData1.AddLast(testElement);
			expectedData2.AddLast(testElement);
			testElement = new BooleanSECSItem(true);
			expectedData1.AddLast(testElement);
			expectedData2.AddLast(testElement);
			testElement = new F4ArraySECSItem(new float[]{Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, 0.0F});
			expectedData1.AddLast(testElement);
			expectedData2.AddLast(testElement);
			testElement = new F4SECSItem(Single.MaxValue);
			expectedData1.AddLast(testElement);
			expectedData2.AddLast(testElement);
			testElement = new F8ArraySECSItem(new double[]{Double.MaxValue, Double.MinValue, Double.NegativeInfinity, Double.PositiveInfinity, 0.0D});
			expectedData1.AddLast(testElement);
			expectedData2.AddLast(testElement);
			testElement = new F8SECSItem(Double.MaxValue);
			expectedData1.AddLast(testElement);
			expectedData2.AddLast(testElement);
			testElement = new I1ArraySECSItem(new sbyte[]{-1, -128, 0, 127});
			expectedData1.AddLast(testElement);
			expectedData2.AddLast(testElement);
			testElement = new I1SECSItem((sbyte)-1);
			expectedData1.AddLast(testElement);
			expectedData2.AddLast(testElement);
			testElement = new I2ArraySECSItem(new Int16[]{-1, -32768, 0, 1, 32767});
			expectedData1.AddLast(testElement);
			expectedData2.AddLast(testElement);
			testElement = new I2SECSItem(-1);
			expectedData1.AddLast(testElement);
			expectedData2.AddLast(testElement);
			testElement = new I4ArraySECSItem(new Int32[]{-1, -2147483648, 0, 1, 2147483647});
			expectedData1.AddLast(testElement);
			expectedData2.AddLast(testElement);
			testElement = new I4SECSItem(-1);
			expectedData1.AddLast(testElement);
			expectedData2.AddLast(testElement);
			testElement = new I8ArraySECSItem(new Int64[]{-1, -9223372036854775808L, 0, 1, 9223372036854775807L});
			expectedData1.AddLast(testElement);
			expectedData2.AddLast(testElement);
			testElement = new I8SECSItem(-1);
			expectedData1.AddLast(testElement);
			expectedData2.AddLast(testElement);
			testElement = new U1ArraySECSItem(new byte[]{255, 128, 0, 127});
			expectedData1.AddLast(testElement);
			expectedData2.AddLast(testElement);
			testElement = new U1SECSItem((byte)255);
			expectedData1.AddLast(testElement);
			expectedData2.AddLast(testElement);
			testElement = new U2ArraySECSItem(new UInt16[]{65535, 32768, 0, 1, 32767});
			expectedData1.AddLast(testElement);
			expectedData2.AddLast(testElement);
			testElement = new U2SECSItem(65535);
			expectedData1.AddLast(testElement);
			expectedData2.AddLast(testElement);
			testElement = new U4ArraySECSItem(new UInt32[]{0xFFFFFFFF, 2147483648, 0, 1, 2147483647});
			expectedData1.AddLast(testElement);
			expectedData2.AddLast(testElement);
			testElement = new U4SECSItem((UInt32)0xFFFFFFFF);
			expectedData1.AddLast(testElement);
			expectedData2.AddLast(testElement);
			testElement = new U8ArraySECSItem(new UInt64[]{0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000, 0x0000000000000001, 0x7FFFFFFFFFFFFFFF});
			expectedData1.AddLast(testElement);
			expectedData2.AddLast(testElement);
			testElement = new U8SECSItem((UInt64)0xFFFFFFFFFFFFFFFF);
			expectedData1.AddLast(testElement);
			expectedData2.AddLast(testElement);

			ListSECSItem innerList = new ListSECSItem(expectedData2);
			expectedData1.AddLast(innerList);

			ListSECSItem expectedResult = new ListSECSItem(expectedData1);

			Console.WriteLine(secsItem.ToString());
			Console.WriteLine("\n/////////////////////////////////////////////////////////////////////////////////////////////////////\n");
			Console.WriteLine(expectedResult.ToString());

			Assert.IsTrue(secsItem.Equals(expectedResult));
		}

		[Test()]
		public void test02()
		{
		}

		[Test()]
		public void test03()
		{
		}

		[Test()]
		public void test04()
		{
		}

		[Test()]
		public void test05()
		{
		}

		[Test()]
		public void test06()
		{
		}
	}
}

