using System;
using System.Linq;
using NUnit.Framework;
using com.CIMthetics.CSharpSECSTools.SECSItems;

namespace SECSItemTests
{
	[TestFixture()]
	public class I4ArraySECSItemTests
	{
		[Test()]
		public void test01()
		{
			byte[] input = {0x00};

			var exception = Assert.Catch(() => new I4ArraySECSItem(input, 0));

			Assert.IsInstanceOf<ArgumentOutOfRangeException>(exception);
		}

		[Test()]
		public void test02()
		{
			byte[] input = {};

			var exception = Assert.Catch(() => new I4ArraySECSItem(input, 0));

			Assert.IsInstanceOf<ArgumentOutOfRangeException>(exception);
		}

		[Test()]
		public void test03()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x01), 20,
				255, 255, 255, 255,
				128, 0, 0, 0,
				0, 0, 0, 0,
				0, 0, 0, 1,
				127, 255, 255, 255 };
			I4ArraySECSItem secsItem = new I4ArraySECSItem(input, 0);
			Assert.IsTrue(secsItem.getValue()[0] == -1);
			Assert.IsTrue(secsItem.getValue()[1] == -2147483648);
			Assert.IsTrue(secsItem.getValue()[2] == 0);
			Assert.IsTrue(secsItem.getValue()[3] == 1);
			Assert.IsTrue(secsItem.getValue()[4] == 2147483647);
		}

		[Test()]
		public void test04()
		{
			Int32[] input = new Int32[5];
			input[0] = -1;
			input[1] = -2147483648;
			input[2] = 0;
			input[3] = 1;
			input[4] = 2147483647;
			I4ArraySECSItem secsItem = new I4ArraySECSItem(input);
			Assert.IsTrue(secsItem.getValue().SequenceEqual(input));
		}

		[Test()]
		public void test05()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x01), 20,
				255, 255, 255, 255,
				128, 0, 0, 0, 
				0, 0, 0, 0,
				0, 0, 0, 1,
				127, 255, 255, 255 };
			I4ArraySECSItem secsItem = new I4ArraySECSItem(input, 0);
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.I4);
		}

		[Test()]
		public void test06()
		{
			Int32[] input = new Int32[5];
			input[0] = -1;
			input[1] = -2147483648;
			input[2] = 0;
			input[3] = 1;
			input[4] = 2147483647;
			I4ArraySECSItem secsItem = new I4ArraySECSItem(input);
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.I4);
		}

		[Test()]
		public void test07()
		{
			Int32[] input = new Int32[5];
			input[0] = -1;
			input[1] = -2147483648;
			input[2] = 0;
			input[3] = 1;
			input[4] = 2147483647;
			byte[] expectedResult = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x01), 20,
				255, 255, 255, 255,
				128, 0, 0, 0,
				0, 0, 0, 0,
				0, 0, 0, 1,
				127, 255, 255, 255 };

			I4ArraySECSItem secsItem = new I4ArraySECSItem(input);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test08()
		{
			Int32[] input = new Int32[5];
			input[0] = -1;
			input[1] = -2147483648;
			input[2] = 0;
			input[3] = 1;
			input[4] = 2147483647;
			byte[] expectedResult = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x02), 0, 20,
				255, 255, 255, 255,
				128, 0, 0, 0,
				0, 0, 0, 0,
				0, 0, 0, 1,
				127, 255, 255, 255 };

			I4ArraySECSItem secsItem = new I4ArraySECSItem(input, 2);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test09()
		{
			Int32[] input = new Int32[5];
			input[0] = -1;
			input[1] = -2147483648;
			input[2] = 0;
			input[3] = 1;
			input[4] = 2147483647;
			byte[] expectedResult = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I4 ) << 2) | 0x03), 0, 0, 20,
				255, 255, 255, 255,
				128, 0, 0, 0,
				0, 0, 0, 0,
				0, 0, 0, 1,
				127, 255, 255, 255 };

			I4ArraySECSItem secsItem = new I4ArraySECSItem(input, 3);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

	}
}

