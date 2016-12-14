using System;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cars.Classes;
using Cars.Controllers;
using Cars.Models;

namespace Cars.Tests
{
	[TestClass]
	public class UnitTest1
	{
		private SqlConnection connect;

		public UnitTest1()
		{
			connect = new SqlConnection("Data Source=(local); Initial Catalog=CARS; Integrated Security=SSPI;");
		}

		[TestMethod]
		public void Test_Add_Item_In_DataBase()
		{
			// Arrange
			User user = new User();
			user.nickName = "Test";
			user.firstName = "Test";
			user.lastName = "Test";
			user.email = "Test@mail.ru";
			user.password = "Test";
			user.phone = "Test";
			user.remember = false;
			string hash = new AccountController().CreateSignature(user.nickName+user.password);

			string date =
				"insert into Users(Nick_Name, First_Name, Last_Name, Phone, Address, Email, Passport,Image) values('" +
				user.nickName + "','" + user.firstName + "','" + user.lastName + "','" + user.phone +
				"','" + user.address + "','" + user.email + "','" + hash + "','" + user.image + "')";

			// Act
			bool trs = WriteSqlServer.UpdateDBonQuery(connect, date);
			User user2 = ReadSqlServer.ReadUser(connect, user.nickName);

			// Assert
			Assert.AreEqual(true, trs);
			Assert.AreEqual(user2.nickName.Replace(" ", ""), user.nickName);
		}

		[TestMethod]
		public void Test_Update_Item_In_DataBase()
		{
			// Arrange
			User user = new User();
			user.nickName = "Test";
			user.firstName = "TestUpdate";

			string date = "update Users set First_name ='" + user.firstName + "' where Nick_name = '" + user.nickName + "'";
			// Act
			
			bool trs = WriteSqlServer.UpdateDBonQuery(connect, date);
			User user2 = ReadSqlServer.ReadUser(connect, user.nickName);

			// Assert
			Assert.AreEqual(true, trs);
			Assert.AreEqual(user2.firstName.Replace(" ", ""), user.firstName);
		}

		[TestMethod]
		public void Test_Update_NoItem_In_DataBase()
		{
			// Arrange
			User user = new User();
			user.nickName = "TestNot";
			user.firstName = "TestUpdate";

			string date = "update Users set First_name ='" + user.firstName + "' where Nick_name = '" + user.nickName + "'";
			// Act

			bool trs = WriteSqlServer.UpdateDBonQuery(connect, date);
			User user2 = ReadSqlServer.ReadUser(connect, user.nickName);

			// Assert
			Assert.AreEqual(false, trs);
			Assert.AreNotEqual(user2.firstName, user.firstName);
		}

		[TestMethod]
		public void Test_Delete_Item__DataBase()
		{
			// Arrange
			User user = new User();
			user.nickName = "Test";

			string date = "delete from Users where Nick_name = '" + user.nickName + "'";
			// Act

			bool trs = WriteSqlServer.UpdateDBonQuery(connect, date);
			User user2 = ReadSqlServer.ReadUser(connect, user.nickName);

			// Assert
			Assert.AreEqual(true, trs);
			Assert.AreEqual(null, user2.nickName);
		}

		[TestMethod]
		public void Test_Validate_User_Pres_Date()
		{
			// Arrange
			User user = new User();
			user.nickName = "YlasticEars";
			user.password = "780d4777";

			// Act
			bool trs = new AccountController().ValidateUser(user.nickName, user.password);
			User user2 = ReadSqlServer.ReadUser(connect, user.nickName);

			// Assert
			Assert.AreEqual(true, trs);
			Assert.AreEqual(user.nickName, user2.nickName.Replace(" ", ""));
		}

		[TestMethod]
		public void Test_Validate_User_Not_Pres_Date()
		{
			// Arrange
			User user = new User();
			user.nickName = "YlasticEars2";
			user.password = "780d4777";

			// Act
			bool trs = new AccountController().ValidateUser(user.nickName, user.password);
			User user2 = ReadSqlServer.ReadUser(connect, user.nickName);

			// Assert
			Assert.AreEqual(false, trs);
			Assert.AreNotEqual(user.nickName, user2.nickName);
		}

		[TestMethod]
		public void Test_Validate_Login_Pres_Date()
		{
			// Arrange
			User user = new User();
			user.nickName = "YlasticEars";
			user.password = "780d4777";

			// Act
			bool trs = new AccountController().ValidateLogin(user.nickName);

			// Assert
			Assert.AreEqual(true, trs);
		}

		[TestMethod]
		public void Test_Validate_Login_Not_Pres_Date()
		{
			// Arrange
			User user = new User();
			user.nickName = "YlasticEars12";
			user.password = "780d4777";

			// Act
			bool trs = new AccountController().ValidateLogin(user.nickName);
			User user2 = ReadSqlServer.ReadUser(connect, user.nickName);

			// Assert
			Assert.AreEqual(true, trs);
			Assert.AreNotEqual(user.nickName, user2.nickName);
		}

		[TestMethod]
		public void Test_Create_Hash_On_Length()
		{
			// Arrange
			User user = new User();
			user.nickName = "YlasticEars";
			user.password = "780d4777";

			User user1 = new User();
			user1.nickName = "Ylastic";
			user1.password = "780d4";

			// Act
			string hash = new AccountController().CreateSignature(user.nickName + user.password);
			string hash1 = new AccountController().CreateSignature(user1.nickName + user1.password);

			// Assert
			Assert.AreEqual(hash.Length, hash1.Length);
			Assert.AreEqual(32,hash.Length);
		}

		[TestMethod]
		public void Test_Create_Hash_On_Copy()
		{
			// Arrange
			User user = new User();
			user.nickName = "YlasticEars";
			user.password = "780d4777";

			User user1 = new User();
			user1.nickName = "YlastiсEars";
			user1.password = "780d4777";

			// Act
			string hash = new AccountController().CreateSignature(user.nickName + user.password);
			string hash1 = new AccountController().CreateSignature(user1.nickName + user1.password);

			// Assert
			Assert.AreNotEqual(hash, hash1);
		}

		[TestMethod]
		public void Test_Validate_Hash_On_User()
		{
			// Arrange
			User user = new User();
			user.nickName = "YlasticEars";
			user.password = "780d4777";

			User user1 = new User();
			user1.nickName = "YlasticEars";
			user1.password = "780d4777";

			// Act
			string hash = new AccountController().CreateSignature(user.nickName + user.password);
			string hash1 = new AccountController().CreateSignature(user1.nickName + user1.password);

			// Assert
			Assert.AreEqual(hash, hash1);
		}
	}
}
