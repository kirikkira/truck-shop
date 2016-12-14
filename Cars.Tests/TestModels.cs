using System;
using Cars.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cars.Tests
{
	[TestClass]
	public class TestModels
	{
		[TestMethod]
		public void Test_Equal_True_MainInfoCars()
		{
			//arrange
			MainInfoCars info1 = new MainInfoCars(1, "Volvo", "FH16", 2002, 12345, 21, "March", "", "", 12, "", "");
			MainInfoCars info2 = new MainInfoCars(1, "Volvo", "FH16", 2002, 12345, 21, "March", "", "", 12, "", "");

			//act
			bool equal = info1.Equals(info2);

			//Assert
			Assert.AreEqual(true, equal);
		}

		[TestMethod]
		public void Test_Equal_False_MainInfoCars()
		{
			//arrange
			MainInfoCars info1 = new MainInfoCars(1, "Volvo", "FH16", 2002, 12345, 21, "March", "", "", 12, "", "");
			MainInfoCars info2 = new MainInfoCars(2, "Volvo", "FH16", 2002, 12345, 21, "March", "", "", 12, "", "");

			//act
			bool equal = info1.Equals(info2);

			//Assert
			Assert.AreEqual(false, equal);
		}

		[TestMethod]
		public void Test_Equal_False_On_NUll_MainInfoCars()
		{
			//arrange
			MainInfoCars info1 = new MainInfoCars(1, "Volvo", "FH16", 2002, 12345, 21, "March", "", "", 12, "", "");
			MainInfoCars info2 = null;// = new MainInfoCars(2, "Volvo", "FH16", 2002, 12345, 21, "March", "", "", 12, "", "");

			//act
			bool equal = info1.Equals(info2);

			//Assert
			Assert.AreEqual(false, equal);
		}

		[TestMethod]
		public void Test_Equal_True_ProductInfos()
		{
			//arrange
			ProductInfo info1 = new ProductInfo(1, "Volvo", "FH16", 2002, 12345, 21, "March", 2015, "", "", 12, "", "", 12, "",
				"", 12, 12, "", "");
			
			ProductInfo info2 = new ProductInfo(1, "Volvo", "FH16", 2002, 12345, 21, "March", 2015, "", "", 12, "", "", 12, "",
				"", 12, 12, "", "");

			//act
			bool equal = info1.Equals(info2);

			//Assert
			Assert.AreEqual(true, equal);
		}

		[TestMethod]
		public void Test_Equal_False_ProductInfos()
		{
			//arrange
			ProductInfo info1 = new ProductInfo(1, "Volvo", "FH16", 2002, 12345, 21, "March", 2015, "", "", 12, "", "", 12, "",
				"", 12, 12, "", "");

			ProductInfo info2 = new ProductInfo(2, "Volvo", "FH16", 2002, 12345, 21, "March", 2015, "", "", 12, "", "", 12, "",
				"", 12, 12, "", "");
			//act
			bool equal = info1.Equals(info2);

			//Assert
			Assert.AreEqual(false, equal);
		}

		[TestMethod]
		public void Test_Equal_False_On_NUll_ProductInfos()
		{
			//arrange
			ProductInfo info1 = new ProductInfo(1, "Volvo", "FH16", 2002, 12345, 21, "March", 2015, "", "", 12, "", "", 12, "",
				"", 12, 12, "", "");

			ProductInfo info2 = null;//new ProductInfo(1, "Volvo", "FH16", 2002, 12345, 21, "March", 2015, "", "", 12, "", "", 12, "",
				//"", 12, 12, "", "");
			//act
			bool equal = info1.Equals(info2);

			//Assert
			Assert.AreEqual(false, equal);
		}
	}
}
