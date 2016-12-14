using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cars.Classes;
using Cars.Controllers;
using System.Data.SqlClient;
using System.Web.Mvc;
using Cars.Models;

namespace Cars.Tests
{
	[TestClass]
	public class TestClassReadSqlServer
	{
		private SqlConnection connect;

		public TestClassReadSqlServer()
		{
			connect = new SqlConnection("Data Source=(local); Initial Catalog=CARS; Integrated Security=SSPI;");
		}

		[TestMethod]
		public void Test_GetMainInfo_ReadMainInfoCars()
		{
			// Arrange
			MainInfoCars main = new MainInfoCars();

			// Act
			List<MainInfoCars> carses = ReadSqlServer.ReadMainInfoCarses(connect);

			// Assert 
			foreach (var car in carses)
			{
				Assert.AreEqual(false, main.Equals(car));
			}
		}

		[TestMethod]
		public void Test_GetProduct_ReadProductInfos()
		{
			// Arrange
			ProductInfo main = new ProductInfo();
			var maker = "Volvo";
			var sql = "where Maker = '" + maker + "'";

			// Act
			List<ProductInfo> carses = ReadSqlServer.ReadProductInfos(connect, sql);

			// Assert 
			Assert.AreEqual(0, carses.Count, 6);
			foreach (var car in carses)
			{
				Assert.AreEqual(false, main.Equals(car));
			}
		}

		[TestMethod]
		public void Test_GetProductN_ReadProductInfosNumber()
		{
			// Arrange
			ProductInfo main = new ProductInfo();
			var maker = "Volvo";
			var sql = "where Maker = '" + maker + "'";

			// Act
			List<ProductInfo> carses = ReadSqlServer.ReadProductInfos(connect, sql, 1);

			// Assert 
			Assert.AreEqual(0, carses.Count, 6);
			foreach (var car in carses)
			{
				Assert.AreEqual(false, main.Equals(car));
			}
		}

		[TestMethod]
		public void Test_GetCountProduct_ReadCountProductInfos()
		{
			// Arrange
			var maker = "Volvo";
			var sql = "where Maker = '" + maker + "'";

			// Act
			int count = ReadSqlServer.ReadCountProduct(connect, sql);

			// Assert 
			Assert.AreEqual(7, count);
		}

		[TestMethod]
		public void Test_GetModelsProduct_ReadModels()
		{
			// Arrange
			var maker = "Volvo";

			// Act
			List<SelectListItem> items = ReadSqlServer.ReadModels(connect, maker);

			// Assert 
			Assert.AreNotEqual(0, items.Count);
		}

		[TestMethod]
		public void Test_GetMakersProduct_ReadMakers()
		{
			// Arrange

			// Act
			List<string> items = ReadSqlServer.ReadMakers(connect);

			// Assert 
			Assert.AreNotEqual(0, items.Count);
			foreach (var str in items)
			{
				Assert.AreNotEqual(String.Empty, str);
			}
		}

		[TestMethod]
		public void Test_GetProductOnId_ReadProductId()
		{
			// Arrange
			var id = 5;

			// Act
			ProductInfo items = ReadSqlServer.ReadProductId(connect, id);

			// Assert 
			Assert.AreEqual(id, items.id);
		}

		[TestMethod]
		public void Test_Read_User_Nick()
		{
			// Arrange
			var nick = "YlasticEars";

			// Act
			User items = ReadSqlServer.ReadUser(connect, nick);

			// Assert 
			Assert.AreEqual(nick, items.nickName.Replace(" ", ""));
		}

		[TestMethod]
		public void Test_Read_UserV_Nick()
		{
			// Arrange
			string nick = "YlasticEars";

			// Act
			UserViewEdit items = ReadSqlServer.ReadUserV(connect, nick);

			// Assert 
			Assert.AreEqual(nick, items.nickName.Replace(" ", ""));
		}

		[TestMethod]
		public void Test_Read_Id_User_Nick()
		{
			// Arrange
			string nick = "YlasticEars";
			int id = 9;

			// Act
			int items = ReadSqlServer.ReadIdUser(connect, nick);

			// Assert 
			Assert.AreEqual(id, items);
		}

		[TestMethod]
		public void Test_Read_Basket_Products()
		{
			// Arrange
			string list = "3,5,7";
			// Act
			List<BasketProduct> items = ReadSqlServer.ReadBasketProducts(connect, list);

			// Assert 
			Assert.AreEqual(3, items.Count);

		}

		[TestMethod]
		public void Test_Get_ID_Items()
		{
			// Arrange
			string nick = "YlasticEars";
			string sql = "select id_user from Users where Nick_name = '" + nick + "'";

			// Act
			int items = ReadSqlServer.GetIdItem(connect, sql);

			// Assert 
			Assert.AreEqual(9, items);
		}

		[TestMethod]
		public void Test_Get_ID_Basket_User()
		{
			// Arrange
			int id = 10;

			// Act
			string items = ReadSqlServer.GetIdBaketUser(connect, id);

			// Assert 
			Assert.IsNotNull(items);
		}

		[TestMethod]
		public void Test_Get_ID_Basket_NoUser()
		{
			// Arrange
			int id = 1;

			// Act
			string items = ReadSqlServer.GetIdBaketUser(connect, id);

			// Assert 
			Assert.IsNotNull(items);
			Assert.AreEqual("", items);
		}

		[TestMethod]
		public void Test_Get_ID_Basket_NoCurrentDate()
		{
			//create test for detect datetime create and oute
  			//where oute > GetTime()
			// Arrange
			
			int id = 10;

			// Act
			string items = ReadSqlServer.GetIdBaketUser(connect, id);

			// Assert 
			Assert.IsNotNull(items);
		}

		[TestMethod]
		public void Test_Get_Count_Items_Basket()
		{
			// Arrange
			int id = 10;

			// Act
			int items = ReadSqlServer.GetCountItemBasket(connect, id);

			// Assert 
			Assert.IsNotNull(items);
			Assert.AreNotEqual(0, items, -1);
		}

		[TestMethod]
		public void Test_Get_Count_Item()
		{
			// Arrange
			string sql = "Select Count(*) from Product";

			// Act
			int items = ReadSqlServer.GetCountItem(connect, sql);

			// Assert 
			Assert.AreEqual(21, items);
		}
	}
}
