using System;
using System.Linq;
using NUnit.Framework;
using com.CIMthetics.CSharpSECSTools.SECSItems;

namespace SECSItemTests
{
	[TestFixture()]
	public class U8SECSItemTests
	{
		[Test()]
		public void test01()
		{
			byte[] input = {0x00};

			var exception = Assert.Catch(() => new U8SECSItem(input, 0));

			Assert.IsInstanceOf<ArgumentOutOfRangeException>(exception);
		}

		[Test()]
		public void test02()
		{
			byte[] input = {};

			var exception = Assert.Catch(() => new U8SECSItem(input, 0));

			Assert.IsInstanceOf<ArgumentOutOfRangeException>(exception);
		}

		[Test()]
		public void test03()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U8 ) << 2) | 0x01), 0x08, 255, 255, 255, 255, 255, 255, 255, 255 };
			UInt64 expectedOutput = UInt64.MaxValue;
			U8SECSItem secsItem = new U8SECSItem(input, 0);
			Assert.IsTrue(secsItem.getValue() == expectedOutput);
		}

		[Test()]
		public void test04()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U8 ) << 2) | 0x01), 0x08, 128, 0, 0, 0, 0, 0, 0, 0 };
			UInt64 expectedOutput = 9223372036854775808UL;
			U8SECSItem secsItem = new U8SECSItem(input, 0);
			Assert.IsTrue(secsItem.getValue() == expectedOutput);
		}

		[Test()]
		public void test05()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U8 ) << 2) | 0x01), 0x08, 0, 0, 0, 0, 0, 0, 0, 0 };
			UInt64 expectedOutput = 0;
			U8SECSItem secsItem = new U8SECSItem(input, 0);
			Assert.IsTrue(secsItem.getValue() == expectedOutput);
		}

		[Test()]
		public void test06()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U8 ) << 2) | 0x01), 0x08, 0, 0, 0, 0, 0, 0, 0, 1 };
			UInt64 expectedOutput = 1;
			U8SECSItem secsItem = new U8SECSItem(input, 0);
			Assert.IsTrue(secsItem.getValue() == expectedOutput);
		}

		[Test()]
		public void test07()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U8 ) << 2) | 0x01), 0x08, 127, 255, 255, 255, 255, 255, 255, 255 };
			UInt64 expectedOutput = 9223372036854775807UL;
			U8SECSItem secsItem = new U8SECSItem(input, 0);
			Assert.IsTrue(secsItem.getValue() == expectedOutput);
		}

		[Test()]
		public void test08()
		{
			UInt64 expectedOutput = UInt64.MaxValue;
			U8SECSItem secsItem = new U8SECSItem(expectedOutput);
			Assert.IsTrue(secsItem.getValue() == expectedOutput);
		}

		[Test()]
		public void test09()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U8 ) << 2) | 0x01), 0x08, 255, 255, 255, 25, 255, 255, 255, 255 };
			U8SECSItem secsItem = new U8SECSItem(input, 0);
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.U8);
		}

		[Test()]
		public void test10()
		{
			UInt64 expectedOutput = UInt64.MaxValue;
			U8SECSItem secsItem = new U8SECSItem(expectedOutput);
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.U8);
		}

		[Test()]
		public void test11()
		{
			byte[] expectedResult = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U8 ) << 2) | 0x01), 0x08, 127, 255, 255, 255, 255, 255, 255, 255 };

			U8SECSItem secsItem = new U8SECSItem(9223372036854775807UL);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test12()
		{
			byte[] expectedResult = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U8 ) << 2) | 0x02), 0, 0x08, 255, 255, 255, 255, 255, 255, 255, 255 };

			U8SECSItem secsItem = new U8SECSItem(UInt64.MaxValue, 2);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test13()
		{
			byte[] expectedResult = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U8 ) << 2) | 0x03), 0, 0, 0x08, 127, 1, 0, 0, 0, 0, 255, 255 };

			U8SECSItem secsItem = new U8SECSItem(0x7F0100000000FFFFUL, 3);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test14()
		{
			byte[] expectedResult = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U8 ) << 2) | 0x03), 0, 0, 0x08, 0, 0, 0, 0, 0, 0, 0, 1 };

			U8SECSItem secsItem = new U8SECSItem(1UL, 3);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

	}
}

