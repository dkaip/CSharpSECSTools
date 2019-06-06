using System;
using System.Linq;
using NUnit.Framework;
using com.CIMthetics.CSharpSECSTools.SECSItems;

namespace SECSItemTests
{
	[TestFixture()]
	public class BooleanArraySECSItemTests
	{
		[Test()]
		public void test01()
		{
			byte[] input = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x01), 0x08, 1, 0, 2, 0, 255, 0, 1, 1};
			bool[] expectedResult = {true, false, true, false, true, false, true, true};
			BooleanArraySECSItem secsItem = new BooleanArraySECSItem(input, 0);
			Assert.IsTrue(secsItem.getValue().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test02()
		{
			byte[] input = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x01), 0x08, 1, 0, 2, 0, 255, 0, 1, 1};
			BooleanArraySECSItem secsItem = new BooleanArraySECSItem(input, 0);
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.BO);
		}

		[Test()]
		public void test03()
		{
			bool[] input = {true, false, true, false, true, false, true, true};
			byte[] expectedResult = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x01), 8, 1, 0, 1, 0, 1, 0, 1, 1};

			BooleanArraySECSItem secsItem = new BooleanArraySECSItem(input);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test04()
		{
			bool[] input = {true, false, true, false, true, false, true, true};
			byte[] expectedResult = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x02), 0, 8, 1, 0, 1, 0, 1, 0, 1, 1};

			BooleanArraySECSItem secsItem = new BooleanArraySECSItem(input, 2);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test05()
		{
			bool[] input = {true, false, true, false, true, false, true, true};
			byte[] expectedResult = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x03), 0, 0, 8, 1, 0, 1, 0, 1, 0, 1, 1};

			BooleanArraySECSItem secsItem = new BooleanArraySECSItem(input, 3);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

	}
}

