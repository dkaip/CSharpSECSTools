using System;
using System.Linq;
using NUnit.Framework;
using com.CIMthetics.CSharpSECSTools.SECSItems;

namespace SECSItemTests
{
	[TestFixture()]
	public class ASCIISECSItemTests
	{
		[Test()]
		public void test01()
		{
			ASCIISECSItem secsItem = new ASCIISECSItem("DEF");
			Assert.IsTrue(secsItem.getValue().Equals("DEF"));
		}

		[Test()]
		public void test02()
		{
			byte[] input = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.A ) << 2) | 0x01), 0x03, 0x41, 0x42, 0x43};                  // 'A', 'B', 'C'
			ASCIISECSItem secsItem = new ASCIISECSItem(input, 0);
			Assert.IsTrue(secsItem.getValue().Equals("ABC"));
		}

		[Test()]
		public void test03()
		{
			ASCIISECSItem secsItem = new ASCIISECSItem("DEF");
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.A);
		}

		[Test()]
		public void test04()
		{
			byte[] input = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.A ) << 2) | 0x01), 0x03, 0x41, 0x42, 0x43};                  // 'A', 'B', 'C'
			ASCIISECSItem secsItem = new ASCIISECSItem(input, 0);
			Assert.IsTrue(secsItem.getSECSItemFormatCode() == SECSItemFormatCode.A);
		}

		[Test()]
		public void test05()
		{
			byte[] expectedResult = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.A ) << 2) | 0x01), 0x03, 0x41, 0x42, 0x43};                  // 'A', 'B', 'C'

			ASCIISECSItem secsItem = new ASCIISECSItem("ABC");
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test06()
		{
			byte[] expectedResult = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.A ) << 2) | 0x02), 0, 0x03, 0x41, 0x42, 0x43};                  // 'A', 'B', 'C'

			ASCIISECSItem secsItem = new ASCIISECSItem("ABC", 2);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}

		[Test()]
		public void test07()
		{
			byte[] expectedResult = {(byte)((SECSItemFormatCodeFunctions.getNumberFromSECSItemFormatCode(SECSItemFormatCode.A ) << 2) | 0x03), 0, 0, 0x03, 0x41, 0x42, 0x43};                  // 'A', 'B', 'C'

			ASCIISECSItem secsItem = new ASCIISECSItem("ABC", 3);
			Assert.IsTrue(secsItem.toRawSECSItem().SequenceEqual(expectedResult));
		}
	}
}

