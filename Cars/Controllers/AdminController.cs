using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Cars.Classes;
using Cars.Models;

namespace Cars.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        SqlConnection connect = new SqlConnection("Data Source=KIRA\\PRO; Initial Catalog=CARS; Integrated Security=SSPI;");


        public ActionResult AdminPanel()
        {
			
            return View();
        }

	    public ActionResult AddProduct()
	    {
			ProductInfoView car = new ProductInfoView();
			car.InitSelectView(connect);
		    return View(car);
	    }

	    public ActionResult AddUser()
	    {
		    return View();
	    }

	    public ActionResult AddRole()
	    {
			Role role = new Role();
		    return View(role);
	    }

	    public ActionResult AddRoleUser()
	    {
			List<RoleInUser> roleInUser = new List<RoleInUser>();
			
			List<User> users = ReadSqlServer.GerListUsers(connect);

			foreach (User user in users)
		    {
				RoleInUser roleUser = new RoleInUser();
				roleUser.AddUser(user);
				roleUser.GetRoles();
			    roleInUser.Add(roleUser);
		    }
		    return View(roleInUser);
	    }

	    public ActionResult AddOrder()
	    {
			OrderView car = new OrderView();
			car.InitList(connect);
			return View(car);
	    }

		/*---------------*/
		/*------method write sql---------*/

	    public ActionResult AddNewCar(ProductInfoView car)
	    {
		    if (ModelState.IsValid)
		    {
			    DateTime time = DateTime.Today;
			    car.AddDate(time);

			    int id = WriteSqlServer.AddCar(connect, car);

			    if (id < 1)
			    {
				    ModelState.AddModelError("", "Product don't added");
			    }
			    else
			    {
				    return RedirectToAction("AdminPanel");
			    }
		    }
		    return View("AddProduct", car);
	    }

		public ActionResult AddNewUser(User user, string returnUrl)
		{
			string hash = new AccountController().CreateSignature(user.password+user.nickName);

			string date =
				"values('" + user.nickName + "','" + user.firstName + "','" + user.lastName + "','" + user.phone +
						  "','" + user.address + "','" + user.email + "','" + hash + "','" + user.image + "')";

			string sql =
				"insert into Users(Nick_Name, First_Name, Last_Name, Phone, Address, Email, Passport,Image) " + date;
			if (ModelState.IsValid)
			{
				if (new AccountController().ValidateLogin(user.nickName))
				{
					FormsAuthentication.SetAuthCookie(user.nickName, user.remember);
					WriteSqlServer.UpdateDBonQuery(connect, sql);
					if (Url.IsLocalUrl(returnUrl))
					{
						return Redirect(returnUrl);
					}
					else
					{
						return RedirectToAction("AdminPanel","Admin");
					}
				}
				else
				{
					ModelState.AddModelError("", "User with this Nick name already exists");
				}
			}
			return RedirectToAction("AddUser", user);
		}

	    public ActionResult AddNewRole(Role role)
	    {
		    if (ModelState.IsValid)
		    {
				if (role.name == "" || role.name == null)
				{
					ModelState.AddModelError("", "Empty name role");
					return View("AddRole", role);
				}

				if (Roles.RoleExists(role.name))
				{
					ModelState.AddModelError("", "Role is exist");
					return View("AddRole", role);
				}

				role.AddNewRole(connect);
		    }
		    
			
			return View("AdminPanel", role);
	    }

	    public ActionResult AddRoleInUser(string nickName, string roleName)
	    {
		    if (roleName == null || roleName == "")
		    {
			    return View("AddRoleUser");
		    }
		    Roles.AddUserToRole(nickName, roleName);
			return View("AdminPanel");
	    }

	    public JsonResult GetRolesInUser(string nickName)
	    {
		    string[] rol = Roles.GetRolesForUser(nickName);

		    string[] all = Roles.GetAllRoles();

		    List<string> list = all.ToList();

		    foreach (string chr in rol)
		    {
			    if (all.Contains(chr))
			    {
					list.Remove(chr);
			    }
		    }

		    all = list.ToArray();
		    return Json(all, JsonRequestBehavior.AllowGet);
	    }

	    public ActionResult GetMakerOnType(string type)
	    {
			string sqlQuery = "Select distinct Maker from Product where Type_car = '" + type + "'";

			connect.Open();
			SqlCommand sql = new SqlCommand(sqlQuery, connect);

			SqlDataReader reader = sql.ExecuteReader();

			List<SelectListItem> result = new List<SelectListItem>();
			while (reader.HasRows)
			{
				while (reader.Read())
				{
					result.Add(new SelectListItem { Value = reader.GetString(0), Text = reader.GetString(0) });
				}
				reader.NextResult();
			}
			reader.Close();
			connect.Close();

			OrderView car = new OrderView();
			car.InitList(connect);
		    ViewData["makers"] = result;

		    return PartialView("Partial/GetMakerOnType", car);
	    }

		[HttpPost]
		public ActionResult GetModelOnMaker(string maker, string type)
		{
			string sqlQuery = "Select distinct Model from Product where Maker = '" + maker + "' and Type_car = '" + type + "'";

			connect.Open();
			SqlCommand sql = new SqlCommand(sqlQuery, connect);

			SqlDataReader reader = sql.ExecuteReader();

			List<SelectListItem> result = new List<SelectListItem>();
			while (reader.HasRows)
			{
				while (reader.Read())
				{
					result.Add(new SelectListItem { Value = reader.GetString(0), Text = reader.GetString(0) });
				}
				reader.NextResult();
			}
			reader.Close();
			connect.Close();

			OrderView car = new OrderView();
			car.InitList(connect);
			ViewData["models"] = result;

			return PartialView("Partial/GetModelOnMaker", car);
		}

		[HttpPost]
		public ActionResult GetCabinOnModel(string maker, string model)
		{
			string sqlQuery = "Select distinct Cabin from Product where Maker = '" + maker + "' and Model = '" + model + "'";

			connect.Open();
			SqlCommand sql = new SqlCommand(sqlQuery, connect);

			SqlDataReader reader = sql.ExecuteReader();

			List<SelectListItem> result = new List<SelectListItem>();
			while (reader.HasRows)
			{
				while (reader.Read())
				{
					result.Add(new SelectListItem { Value = reader.GetString(0), Text = reader.GetString(0) });
				}
				reader.NextResult();
			}
			reader.Close();
			connect.Close();

			OrderView car = new OrderView();
			car.InitList(connect);
			ViewData["cabin"] = result;

			return PartialView("Partial/GetCabinOnModel", car);
		}

		[HttpPost]
		public ActionResult GetVEngOnCabin(string maker, string model, string cabin)
		{
			string sqlQuery = "Select distinct V_engine from Product where Maker = '" + maker + "' and Model = '" + model +
			                  "' and Cabin ='" + cabin + "'";

			connect.Open();
			SqlCommand sql = new SqlCommand(sqlQuery, connect);

			SqlDataReader reader = sql.ExecuteReader();

			List<SelectListItem> result = new List<SelectListItem>();
			while (reader.HasRows)
			{
				while (reader.Read())
				{
					result.Add(new SelectListItem { Value = reader.GetInt16(0).ToString(), Text = reader.GetInt16(0).ToString() });
				}
				reader.NextResult();
			}
			reader.Close();
			connect.Close();

			OrderView car = new OrderView();
			car.InitList(connect);
			ViewData["v-eng"] = result;

			return PartialView("Partial/GetVEngOnCabin", car);
		}

		[HttpPost]
		public ActionResult GetTypeTransOnVEng(string maker, string model, string cabin, string vEng)
		{
			string sqlQuery = "Select distinct Type_transmission from Product where Maker = '" + maker + "' and Model = '" + model +
							  "' and Cabin ='" + cabin + "' And V_engine = " + vEng;

			connect.Open();
			SqlCommand sql = new SqlCommand(sqlQuery, connect);

			SqlDataReader reader = sql.ExecuteReader();

			List<SelectListItem> result = new List<SelectListItem>();
			while (reader.HasRows)
			{
				while (reader.Read())
				{
					result.Add(new SelectListItem { Value = reader.GetInt16(0).ToString(), Text = reader.GetInt16(0).ToString() });
				}
				reader.NextResult();
			}
			reader.Close();
			connect.Close();

			OrderView car = new OrderView();
			car.InitList(connect);
			ViewData["type-trans"] = result;

			return PartialView("Partial/GetTypeTransOnVEng", car);
		}
    }
}
