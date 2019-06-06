using System;
using System.Linq;
using NUnit.Framework;
using com.CIMthetics.CSharpSECSTools.SECSItems;

namespace SECSItemTests
{
	[TestFixture()]
	public class U1ArraySECSItemTests
	{
		[Test()]
		public void test01()
		{
			byte[] input = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U1 ) << 2) | 0x01), 0x04, 255, 128, 0, 127};
			U1ArraySECSItem secsItem = new U1ArraySECSItem(input, 0, 0);
			Assert.IsTrue(secsItem.getValue()[0] == 255);
			Assert.IsTrue(secsItem.getValue()[1] == 128);
			Assert.IsTrue(secsItem.getValue()[2] == 0);
			Assert.IsTrue(secsItem.getValue()[3] == 127);
		}

		[Test()]
		public void test02()
		{
			byte[] input = {255, 128, 0, 127};
			U1ArraySECSItem secsItem = new U1ArraySECSItem(input);
			Assert.IsTrue(secsItem.getValue().SequenceEqual(input));
		}

		[Test()]
		public void test03()
		{
			byte[] input = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U1 ) << 2) | 0x01), 0x04, 255, 128, 0, 127};
			U1ArraySECSItem secsItem = new U1ArraySECSItem(input, 0);
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.U1);
		}

		[Test()]
		public void test04()
		{
			byte[] input = {255, 128, 0, 127};
			U1ArraySECSItem secsItem = new U1ArraySECSItem(input);
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.U1);
		}

		[Test()]
		public void test05()
		{
			byte[] input = { 255, 128, 0, 1, 127 };
			byte[] expectedResult = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U1 ) << 2) | 0x01), 0x05, 255, 128, 0, 1, 127};

			U1ArraySECSItem secsItem = new U1ArraySECSItem(input);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test06()
		{
			byte[] input = { 255, 128, 0, 1, 127 };
			byte[] expectedResult = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U1 ) << 2) | 0x02), 0, 0x05, 255, 128, 0, 1, 127};

			U1ArraySECSItem secsItem = new U1ArraySECSItem(input, 2);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test07()
		{
			byte[] input = { 255, 128, 0, 1, 127 };
			byte[] expectedResult = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.U1 ) << 2) | 0x03), 0, 0, 0x05, 255, 128, 0, 1, 127};

			U1ArraySECSItem secsItem = new U1ArraySECSItem(input, 3);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

	}
}

