using System;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.ClientServices.Providers;
using System.Web.Mvc;
using System.Web.Routing;
using Cars.Classes;
using Cars.Controllers;
using Cars.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace Cars.Tests
{
	[TestClass]
	public class TestAccountController
	{
		private SqlConnection connect;
		private AccountController _accountController;

		public TestAccountController()
		{
			connect = new SqlConnection("Data Source=(local); Initial Catalog=CARS; Integrated Security=SSPI;");
		}

		[TestMethod]
		public void TestLogin()
		{
			// Arrange
			AccountController controller = new AccountController();
			
			// Act
			ViewResult result = controller.Login() as ViewResult;
			

			// Assert
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void TestRegister()
		{
			// Arrange
			AccountController controller = new AccountController();

			// Act
			ViewResult result = controller.Register() as ViewResult;

			// Assert
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void TestInfoUser()
		{
			// Arrange
			AccountController controller = new AccountController();
			string nick = "Baxye";

			// Act
			ViewResult result = controller.User(nick) as ViewResult;

			// Assert
			Assert.AreEqual(((UserViewEdit)result.Model).nickName.Replace(" ", ""), nick);

		}

		/*[TestMethod]
		public void TestRegister_User()
		{
			//arrage
			var userMock = new Mock<IPrincipal>();
			User user = new User();
			user.nickName = "Test";
			user.firstName = "Test";
			user.lastName = "Test";
			user.address = "Test";
			user.email = "Test";
			user.phone = "Test";
			user.password = "Test";
			//мок для user
			userMock.Expect(p => p.Identity.IsAuthenticated).Returns(true);
			userMock.Expect(x => x.Identity.Name).Returns("YlasticEars");

			//мок для HttpContext, который идентефицирует всю информацию о подключении
			var contextMock = new Mock<HttpContextBase>();
			contextMock.ExpectGet(ctx => ctx.User).Returns(userMock.Object);

			//создание мока для контекста контроллера
			var controllerContextMock = new Mock<ControllerContext>();
			controllerContextMock.ExpectGet(con => con.HttpContext).Returns(contextMock.Object);

			var auth = new Mock<IAuthenticationProvider>();
			auth.ExpectGet(sts => sts.SetAuthCookie("Test", false)).Returns(1);

			AccountController controller = new AccountController();
			controller.ControllerContext = controllerContextMock.Object;

			//act
			var result = controller.Register(user, null,auth.Object) as ActionResult;

			//assert
			Assert.IsNotNull(result);
		}*/
	}
}
