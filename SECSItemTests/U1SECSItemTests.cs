using System;
using System.Linq;
using NUnit.Framework;
using com.CIMthetics.CSharpSECSTools.SECSItems;

namespace SECSItemTests
{
	[TestFixture()]
	public class U1SECSItemTests
	{
		[Test()]
		public void test01()
		{
			byte value = 255;
			U1SECSItem secsItem = new U1SECSItem(value);
			Assert.IsTrue(secsItem.getValue() == 255);
		}

		[Test()]
		public void test02()
		{
			byte value = 128;
			U1SECSItem secsItem = new U1SECSItem(value);
			Assert.IsTrue(secsItem.getValue() == 128);
		}

		[Test()]
		public void test03()
		{
			byte value = 0;
			U1SECSItem secsItem = new U1SECSItem(value);
			Assert.IsTrue(secsItem.getValue() == 0);
		}

		[Test()]
		public void test04()
		{
			byte value = 127;
			U1SECSItem secsItem = new U1SECSItem(value);
			Assert.IsTrue(secsItem.getValue() == 127);
		}

		[Test()]
		public void test05()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U1 ) << 2) | 0x01), 0x01, 255 };
			byte expectedOutput = 255;
			U1SECSItem secsItem = new U1SECSItem(input, 0);
			Assert.IsTrue(secsItem.getValue() == expectedOutput);
		}

		[Test()]
		public void test06()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U1 ) << 2) | 0x01), 0x01, 128 };
			byte expectedOutput = 128;
			U1SECSItem secsItem = new U1SECSItem(input, 0);
			Assert.IsTrue(secsItem.getValue() == expectedOutput);
		}

		[Test()]
		public void test07()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U1 ) << 2) | 0x01), 0x01, 0 };
			byte expectedOutput = 0;
			U1SECSItem secsItem = new U1SECSItem(input, 0);
			Assert.IsTrue(secsItem.getValue() == expectedOutput);
		}

		[Test()]
		public void test08()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U1 ) << 2) | 0x01), 0x01, 127 };
			byte expectedOutput = 127;
			U1SECSItem secsItem = new U1SECSItem(input, 0);
			Assert.IsTrue(secsItem.getValue() == expectedOutput);
		}

		[Test()]
		public void test09()
		{
			byte value = 255;
			U1SECSItem secsItem = new U1SECSItem(value);
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.U1);
		}

		[Test()]
		public void test10()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U1 ) << 2) | 0x01), 0x01, 255 };
			U1SECSItem secsItem = new U1SECSItem(input, 0);
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.U1);
		}

		[Test()]
		public void test11()
		{
			byte[] expectedResult = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U1 ) << 2) | 0x01), 0x01, 255 };

			U1SECSItem secsItem = new U1SECSItem(255);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test12()
		{
			byte[] expectedResult = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U1 ) << 2) | 0x02), 0, 0x01, 255 };

			U1SECSItem secsItem = new U1SECSItem(255, 2);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test13()
		{
			byte[] expectedResult = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U1 ) << 2) | 0x03), 0, 0, 0x01, 255 };

			U1SECSItem secsItem = new U1SECSItem(255, 3);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}
	}
}

