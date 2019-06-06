using System;
using System.Linq;
using NUnit.Framework;
using com.CIMthetics.CSharpSECSTools.SECSItems;

namespace SECSItemTests
{
	[TestFixture()]
	public class BinarySECSItemTests
	{
		[Test()]
		public void test01()
		{
			byte[] input = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.B ) << 2) | 0x01), 0x05, 128, 255, 0, 1, 127};
			byte[] expectedResult = {128, 255, 0, 1, 127};

			BinarySECSItem secsItem = new BinarySECSItem(input, 0, 0);
			Assert.IsTrue(secsItem.getValue().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test02()
		{
			byte[] input = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.B ) << 2) | 0x01), 0x05, 128, 255, 0, 1, 127};

			BinarySECSItem secsItem = new BinarySECSItem(input, 0);
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.B);
		}

		[Test()]
		public void test03()
		{
			byte[] input = {128, 255, 0, 1, 127};
			byte[] expectedResult = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.B ) << 2) | 0x01), 0x05, 128, 255, 0, 1, 127};

			BinarySECSItem secsItem = new BinarySECSItem(input);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test04()
		{
			byte[] input = {128, 255, 0, 1, 127};
			byte[] expectedResult = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.B ) << 2) | 0x02), 0, 5, 128, 255, 0, 1, 127};

			BinarySECSItem secsItem = new BinarySECSItem(input, 2);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test05()
		{
			byte[] input = {128, 255, 0, 1, 127};
			byte[] expectedResult = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.B ) << 2) | 0x03), 0, 0, 5, 128, 255, 0, 1, 127};

			BinarySECSItem secsItem = new BinarySECSItem(input, 3);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}
	}
}

