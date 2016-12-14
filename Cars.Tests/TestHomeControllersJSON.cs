using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using Cars.Controllers;
using Cars.Models;
using System.Web.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cars.Tests
{
	[TestClass]
	public class TestHomeControllersJSON
	{
		[TestMethod]
		public void Test_GetMakers()
		{
			// Arrange
			HomeController controller = new HomeController();

			// Act
			JsonResult result = controller.GetMakers() as JsonResult;

			// Assert
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void Test_GetProductDate()
		{
			// Arrange
			HomeController controller = new HomeController();

			var sql = "where Model = 'FH16'";

			// Act
			JsonResult result = controller.GetProductDate(sql) as JsonResult;

			// Assert
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void Test_GetProductDateNumber()
		{
			// Arrange
			HomeController controller = new HomeController();

			var sql = "where Model = 'FH16'";

			// Act
			JsonResult result = controller.GetProductDateNamber(sql, 1) as JsonResult;

			// Assert 
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void Test_GetCountProduct()
		{
			// Arrange
			HomeController controller = new HomeController();

			var sql = "where Model = 'FH16'";

			// Act
			JsonResult result = controller.GetCountProduct(sql) as JsonResult;

			// Assert 
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void Test_GetCountProduct_One()
		{
			// Arrange
			HomeController controller = new HomeController();

			var sql = "";

			// Act
			JsonResult result = controller.GetCountProduct(sql) as JsonResult;

			// Assert 
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void Test_CarsModelItem()
		{
			// Arrange
			HomeController controller = new HomeController();

			// Act
			JsonResult result = controller.CarsModelItem() as JsonResult;

			// Assert 
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void Test_MakerInCar()
		{
			// Arrange
			HomeController controller = new HomeController();

			// Act
			JsonResult result = controller._MakersInCars("Volvo") as JsonResult;

			// Assert 
			Assert.IsNotNull(result);
		}
	}
}
