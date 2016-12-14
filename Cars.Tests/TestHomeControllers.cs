using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cars.Controllers;
using Cars.Models;
using Moq;

namespace Cars.Tests
{
	[TestClass]
	public class TestHomeControllers
	{
		[TestMethod]
		public void TestViewIndex()
		{
			// Arrange
			HomeController controller = new HomeController();

			// Act
			ViewResult result = controller.Index() as ViewResult;

			// Assert
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void TestViewCars()
		{
			// Arrange
			HomeController controller = new HomeController();

			// Act
			ViewResult result = controller.Cars() as ViewResult;

			// Assert
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void TestViewAbout()
		{
			// Arrange
			HomeController controller = new HomeController();

			// Act
			ViewResult result = controller.About() as ViewResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("About", result.ViewBag.Title);
		}

		[TestMethod]
		public void TestViewDetail_OnTheDate()
		{
			// Arrange
			HomeController controller = new HomeController();
			ProductInfo info = new ProductInfo();

			// Act
			ViewResult result = controller.Detail(5) as ViewResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(false, info.Equals(result.Model));
		}

		[TestMethod]
		public void TestViewDetail_OnTheNoDate()
		{
			// Arrange
			HomeController controller = new HomeController();
			ProductInfo info = new ProductInfo();
			// Act
			ViewResult result = controller.Detail(0) as ViewResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(true,info.Equals(result.Model));
		}

		[TestMethod]
		public void TestCountItemBasket()
		{
			// Arrange
			var userMock = new Mock<IPrincipal>();

			//мок для user
			userMock.Expect(p => p.Identity.IsAuthenticated).Returns(true);
			userMock.Expect(x => x.Identity.Name).Returns("Baxye");

			//мок для HttpContext, который идентефицирует всю информацию о подключении
			var contextMock = new Mock<HttpContextBase>();
			contextMock.ExpectGet(ctx => ctx.User).Returns(userMock.Object);

			//создание мока для контекста контроллера
			var controllerContextMock = new Mock<ControllerContext>();
			controllerContextMock.ExpectGet(con => con.HttpContext).Returns(contextMock.Object);
			
			HomeController controller = new HomeController();
			controller.ControllerContext = controllerContextMock.Object;

			// Act
			int result = controller.GetCountItemBasket();

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void TestCountItemBasket_No_Authenticated()
		{
			// Arrange
			var userMock = new Mock<IPrincipal>();

			//мок для user
			userMock.Expect(p => p.Identity.IsAuthenticated).Returns(false);
			userMock.Expect(x => x.Identity.Name).Returns("Baxye");

			//мок для HttpContext, который идентефицирует всю информацию о подключении
			var contextMock = new Mock<HttpContextBase>();
			contextMock.ExpectGet(ctx => ctx.User).Returns(userMock.Object);

			//создание мока для контекста контроллера
			var controllerContextMock = new Mock<ControllerContext>();
			controllerContextMock.ExpectGet(con => con.HttpContext).Returns(contextMock.Object);

			HomeController controller = new HomeController();
			controller.ControllerContext = controllerContextMock.Object;

			// Act
			int result = controller.GetCountItemBasket();

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(0, result);
		}

		[TestMethod]
		public void TestViewBasket()
		{
			// Arrange
			var userMock = new Mock<IPrincipal>();

			//мок для user
			userMock.Expect(p => p.Identity.IsAuthenticated).Returns(true);
			userMock.Expect(x => x.Identity.Name).Returns("Baxye");

			//мок для HttpContext, который идентефицирует всю информацию о подключении
			var contextMock = new Mock<HttpContextBase>();
			contextMock.ExpectGet(ctx => ctx.User).Returns(userMock.Object);

			//создание мока для контекста контроллера
			var controllerContextMock = new Mock<ControllerContext>();
			controllerContextMock.ExpectGet(con => con.HttpContext).Returns(contextMock.Object);

			HomeController controller = new HomeController();
			controller.ControllerContext = controllerContextMock.Object;

			// Act
			ViewResult result = controller.Basket() as ViewResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Model);
			Assert.AreNotEqual(0, ((List<BasketProduct>)result.Model).Count, -1);
		}

		[TestMethod]
		public void TestViewBasket_No_Item_Basket()
		{
			// Arrange
			var userMock = new Mock<IPrincipal>();

			//мок для user
			userMock.Expect(p => p.Identity.IsAuthenticated).Returns(true);
			userMock.Expect(x => x.Identity.Name).Returns("Stoma_asd");

			//мок для HttpContext, который идентефицирует всю информацию о подключении
			var contextMock = new Mock<HttpContextBase>();
			contextMock.ExpectGet(ctx => ctx.User).Returns(userMock.Object);

			//создание мока для контекста контроллера
			var controllerContextMock = new Mock<ControllerContext>();
			controllerContextMock.ExpectGet(con => con.HttpContext).Returns(contextMock.Object);

			HomeController controller = new HomeController();
			controller.ControllerContext = controllerContextMock.Object;

			// Act
			ViewResult result = controller.Basket() as ViewResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Model);
			Assert.AreNotEqual(0, ((List<BasketProduct>)result.Model).Count, -1);
		}

		[TestMethod]
		public void TestViewBasket_No_Authenticated()
		{
			// Arrange
			var userMock = new Mock<IPrincipal>();

			//мок для user
			userMock.Expect(p => p.Identity.IsAuthenticated).Returns(false);
			userMock.Expect(x => x.Identity.Name).Returns("Stoma_asd");

			//мок для HttpContext, который идентефицирует всю информацию о подключении
			var contextMock = new Mock<HttpContextBase>();
			contextMock.ExpectGet(ctx => ctx.User).Returns(userMock.Object);

			//создание мока для контекста контроллера
			var controllerContextMock = new Mock<ControllerContext>();
			controllerContextMock.ExpectGet(con => con.HttpContext).Returns(contextMock.Object);

			HomeController controller = new HomeController();
			controller.ControllerContext = controllerContextMock.Object;

			// Act
			ActionResult result = controller.Basket() as ActionResult;

			// Assert
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void TestAddBasket()
		{
			// Arrange
			int id = 5;
			var userMock = new Mock<IPrincipal>();

			//мок для user
			userMock.Expect(p => p.Identity.IsAuthenticated).Returns(true);
			userMock.Expect(x => x.Identity.Name).Returns("YlasticEars");

			//мок для HttpContext, который идентефицирует всю информацию о подключении
			var contextMock = new Mock<HttpContextBase>();
			contextMock.ExpectGet(ctx => ctx.User).Returns(userMock.Object);

			//создание мока для контекста контроллера
			var controllerContextMock = new Mock<ControllerContext>();
			controllerContextMock.ExpectGet(con => con.HttpContext).Returns(contextMock.Object);

			HomeController controller = new HomeController();
			controller.ControllerContext = controllerContextMock.Object;

			// Act
			var result = controller.AddBasket(id) as RedirectResult;

			// Assert
			Assert.IsNull(result);
		}

		[TestMethod]
		public void TestAddBasket_Copy()
		{
			// Arrange
			int id = 7;
			var userMock = new Mock<IPrincipal>();

			//мок для user
			userMock.Expect(p => p.Identity.IsAuthenticated).Returns(true);
			userMock.Expect(x => x.Identity.Name).Returns("YlasticEars");

			//мок для HttpContext, который идентефицирует всю информацию о подключении
			var contextMock = new Mock<HttpContextBase>();
			contextMock.ExpectGet(ctx => ctx.User).Returns(userMock.Object);

			//создание мока для контекста контроллера
			var controllerContextMock = new Mock<ControllerContext>();
			controllerContextMock.ExpectGet(con => con.HttpContext).Returns(contextMock.Object);

			HomeController controller = new HomeController();
			controller.ControllerContext = controllerContextMock.Object;

			// Act
			var result = controller.AddBasket(id) as RedirectResult;

			// Assert
			Assert.IsNull(result);
		}

		[TestMethod]
		public void TestAddBasket_No_Authenticated()
		{
			// Arrange
			int id = 7;
			var userMock = new Mock<IPrincipal>();

			//мок для user
			userMock.Expect(p => p.Identity.IsAuthenticated).Returns(false);
			userMock.Expect(x => x.Identity.Name).Returns("YlasticEars");

			//мок для HttpContext, который идентефицирует всю информацию о подключении
			var contextMock = new Mock<HttpContextBase>();
			contextMock.ExpectGet(ctx => ctx.User).Returns(userMock.Object);

			//создание мока для контекста контроллера
			var controllerContextMock = new Mock<ControllerContext>();
			controllerContextMock.ExpectGet(con => con.HttpContext).Returns(contextMock.Object);

			HomeController controller = new HomeController();
			controller.ControllerContext = controllerContextMock.Object;

			// Act
			var result = controller.AddBasket(id) as RedirectResult;

			// Assert
			Assert.IsNull(result);
		}

		[TestMethod]
		public void TestDeleteBasket()
		{
			// Arrange
			int id = 5;
			var userMock = new Mock<IPrincipal>();

			//мок для user
			userMock.Expect(p => p.Identity.IsAuthenticated).Returns(true);
			userMock.Expect(x => x.Identity.Name).Returns("YlasticEars");

			//мок для HttpContext, который идентефицирует всю информацию о подключении
			var contextMock = new Mock<HttpContextBase>();
			contextMock.ExpectGet(ctx => ctx.User).Returns(userMock.Object);

			//создание мока для контекста контроллера
			var controllerContextMock = new Mock<ControllerContext>();
			controllerContextMock.ExpectGet(con => con.HttpContext).Returns(contextMock.Object);

			HomeController controller = new HomeController();
			controller.ControllerContext = controllerContextMock.Object;

			// Act
			var result = controller.DeleteBasket(5) as RedirectResult;

			// Assert
			Assert.IsNull(result);
		}

		[TestMethod]
		public void TestDeleteBasket_No_Authenticated()
		{
			// Arrange
			int id = 5;
			var userMock = new Mock<IPrincipal>();

			//мок для user
			userMock.Expect(p => p.Identity.IsAuthenticated).Returns(false);
			userMock.Expect(x => x.Identity.Name).Returns("YlasticEars");

			//мок для HttpContext, который идентефицирует всю информацию о подключении
			var contextMock = new Mock<HttpContextBase>();
			contextMock.ExpectGet(ctx => ctx.User).Returns(userMock.Object);

			//создание мока для контекста контроллера
			var controllerContextMock = new Mock<ControllerContext>();
			controllerContextMock.ExpectGet(con => con.HttpContext).Returns(contextMock.Object);

			HomeController controller = new HomeController();
			controller.ControllerContext = controllerContextMock.Object;

			// Act
			var result = controller.DeleteBasket(5) as RedirectResult;

			// Assert
			Assert.IsNull(result);
		}

		[TestMethod]
		public void TestDeleteAllBasket()
		{
			// Arrange
			int id = 5;
			var userMock = new Mock<IPrincipal>();

			//мок для user
			userMock.Expect(p => p.Identity.IsAuthenticated).Returns(true);
			userMock.Expect(x => x.Identity.Name).Returns("Stoma_asd");

			//мок для HttpContext, который идентефицирует всю информацию о подключении
			var contextMock = new Mock<HttpContextBase>();
			contextMock.ExpectGet(ctx => ctx.User).Returns(userMock.Object);

			//создание мока для контекста контроллера
			var controllerContextMock = new Mock<ControllerContext>();
			controllerContextMock.ExpectGet(con => con.HttpContext).Returns(contextMock.Object);

			HomeController controller = new HomeController();
			controller.ControllerContext = controllerContextMock.Object;

			// Act
			ActionResult result = controller.DeleteAllBasket() as ActionResult;

			// Assert
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void TestDeleteAllBasket_No_Authenticated()
		{
			// Arrange
			int id = 5;
			var userMock = new Mock<IPrincipal>();

			//мок для user
			userMock.Expect(p => p.Identity.IsAuthenticated).Returns(false);
			userMock.Expect(x => x.Identity.Name).Returns("YlasticEars");

			//мок для HttpContext, который идентефицирует всю информацию о подключении
			var contextMock = new Mock<HttpContextBase>();
			contextMock.ExpectGet(ctx => ctx.User).Returns(userMock.Object);

			//создание мока для контекста контроллера
			var controllerContextMock = new Mock<ControllerContext>();
			controllerContextMock.ExpectGet(con => con.HttpContext).Returns(contextMock.Object);

			HomeController controller = new HomeController();
			controller.ControllerContext = controllerContextMock.Object;

			// Act
			ActionResult result = controller.DeleteAllBasket() as ActionResult;

			// Assert
			Assert.IsNotNull(result);
		}
	}
}
