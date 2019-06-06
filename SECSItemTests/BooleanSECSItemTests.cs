using System;
using System.Linq;
using NUnit.Framework;
using com.CIMthetics.CSharpSECSTools.SECSItems;

namespace SECSItemTests
{
	[TestFixture()]
	public class BooleanSECSItemTests
	{
		[Test()]
		public void test01()
		{
			BooleanSECSItem secsItem = new BooleanSECSItem(true);
			Assert.IsTrue(secsItem.getValue() == true);
		}

		[Test()]
		public void test02()
		{
			BooleanSECSItem secsItem = new BooleanSECSItem(false);
			Assert.IsTrue(secsItem.getValue() == false);
		}

		[Test()]
		public void test03()
		{
			BooleanSECSItem secsItem = new BooleanSECSItem(true);
			Assert.IsFalse(secsItem.getValue() == false);
		}

		[Test()]
		public void test04()
		{
			byte[] input = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x01), 0x01, 5};
			BooleanSECSItem secsItem = new BooleanSECSItem(input, 0);
			Assert.IsTrue(secsItem.getValue() == true);
		}

		[Test()]
		public void test05()
		{
			byte[] input = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x01), 0x01, 0};
			BooleanSECSItem secsItem = new BooleanSECSItem(input, 0);
			Assert.IsTrue(secsItem.getValue() == false);
		}

		[Test()]
		public void test06()
		{
			BooleanSECSItem secsItem = new BooleanSECSItem(true);
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.BO);
		}

		[Test()]
		public void test07()
		{
			byte[] input = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x01), 0x01, 5};
			BooleanSECSItem secsItem = new BooleanSECSItem(input, 0);
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.BO);
		}

		[Test()]
		public void test08()
		{
			byte[] expectedResult = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x01), 0x01, 1};

			BooleanSECSItem secsItem = new BooleanSECSItem(true);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test09()
		{
			byte[] expectedResult = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x02), 0, 1, 1};

			BooleanSECSItem secsItem = new BooleanSECSItem(true, 2);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test10()
		{
			byte[] expectedResult = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.BO ) << 2) | 0x03), 0, 0, 1, 0};

			BooleanSECSItem secsItem = new BooleanSECSItem(false, 3);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}
	}
}

