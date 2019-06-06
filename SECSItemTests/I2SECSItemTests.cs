using System;
using System.Linq;
using NUnit.Framework;
using com.CIMthetics.CSharpSECSTools.SECSItems;

namespace SECSItemTests
{
	[TestFixture()]
	public class I2SECSItemTests
	{
		[Test()]
		public void test01()
		{
			byte[] input = {0x00};

			var exception = Assert.Catch(() => new I2SECSItem(input, 0));

			Assert.IsInstanceOf<ArgumentOutOfRangeException>(exception);
		}

		[Test()]
		public void test02()
		{
			byte[] input = {};

			var exception = Assert.Catch(() => new I2SECSItem(input, 0));

			Assert.IsInstanceOf<ArgumentOutOfRangeException>(exception);
		}

		[Test()]
	    public void test03()
	    {
	        byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I2) << 2) | 0x01), 0x02, 255, 255 };
			Int16 expectedOutput = -1;
	        I2SECSItem secsItem = new I2SECSItem(input, 0);
	        Assert.IsTrue(secsItem.getValue() == expectedOutput);
	    }

		[Test()]
		public void test04()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I2 ) << 2) | 0x01), 0x02, 128, 0 };
			Int16 expectedOutput = -32768;
			I2SECSItem secsItem = new I2SECSItem(input, 0);
			Assert.IsTrue(secsItem.getValue() == expectedOutput);
		}

		[Test()]
		public void test05()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I2 ) << 2) | 0x01), 0x02, 0, 0 };
			Int16 expectedOutput = 0;
			I2SECSItem secsItem = new I2SECSItem(input, 0);
			Assert.IsTrue(secsItem.getValue() == expectedOutput);
		}

		[Test()]
		public void test06()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I2 ) << 2) | 0x01), 0x02, 0, 1 };
			Int16 expectedOutput = 1;
			I2SECSItem secsItem = new I2SECSItem(input, 0);
			Assert.IsTrue(secsItem.getValue() == expectedOutput);
		}

		[Test()]
		public void test07()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I2 ) << 2) | 0x01), 0x02, 127, 255 };
			Int16 expectedOutput = 32767;
			I2SECSItem secsItem = new I2SECSItem(input, 0);
			Assert.IsTrue(secsItem.getValue() == expectedOutput);
		}

		[Test()]
		public void test08()
		{
			Int16 expectedOutput = 32767;
			I2SECSItem secsItem = new I2SECSItem(expectedOutput);
			Assert.IsTrue(secsItem.getValue() == expectedOutput);
		}

		[Test()]
		public void test09()
		{
			byte[] input = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I2 ) << 2) | 0x01), 0x02, 255, 255 };
			I2SECSItem secsItem = new I2SECSItem(input, 0);
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.I2);
		}

		[Test()]
		public void test10()
		{
			Int16 expectedOutput = 32767;
			I2SECSItem secsItem = new I2SECSItem(expectedOutput);
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.I2);
		}

		[Test()]
		public void test11()
		{
			byte[] expectedResult = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I2 ) << 2) | 0x01), 0x02, 127, 255 };

			I2SECSItem secsItem = new I2SECSItem((Int16)32767);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test12()
		{
			byte[] expectedResult = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I2 ) << 2) | 0x02), 0, 0x02, 127, 255 };

			I2SECSItem secsItem = new I2SECSItem((Int16)32767, 2);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test13()
		{
			byte[] expectedResult = { (byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.I2 ) << 2) | 0x03), 0, 0, 0x02, 127, 255 };

			I2SECSItem secsItem = new I2SECSItem((Int16)32767, 3);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

	}
}
