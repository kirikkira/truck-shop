using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Net.Http.Formatting;
using System.Web.Providers;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using Cars.Classes;
using Cars.Models;

namespace Cars.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        SqlConnection connect = new SqlConnection("Data Source=KIRA\\PRO; Initial Catalog=CARS; Integrated Security=SSPI;");
		
		/*main page*/
        public ActionResult Index()
        {
            SqlConnection connect = new SqlConnection("Data Source=KIRA\\PRO; Initial Catalog=CARS; Integrated Security=SSPI;");
			
			return View(ReadSqlServer.ReadMainInfoCarses(connect));
        }

	    public ActionResult Cars()
	    {
		    return View();
		}

		public ActionResult About()
		{
			ViewBag.Title = "About";
			return View();
		}
        public ActionResult Gallery()
        {
            ViewBag.Title = "Gallery";
            return View();
        }

        [HttpGet]
	    public ActionResult Detail(int id)
	    {
		    return View(ReadSqlServer.ReadProductId(connect, id));
	    }

	    public ActionResult Basket()
	    {
		    if (User.Identity.IsAuthenticated)
		    {
			    string listid = "";
			    int userId = ReadSqlServer.GetIdItem(connect,
				    "select id_user from Users where Nick_name = '" + User.Identity.Name + "'");

			    listid = ReadSqlServer.GetIdBaketUser(connect, userId);
			    if (listid == "")
			    {
				    return View(new List<BasketProduct>());
			    }
			    return View(ReadSqlServer.ReadBasketProducts(connect, listid));
		    }
		    else
		    {
			    return RedirectToAction("Index");
		    }
	    }

	    public ActionResult Cars_Maker(string id)
	    {
		    List<ProductInfo> cars = ReadSqlServer.ReadProductInfos(connect, "where Maker = '" + id + "'");

			return View(cars);
	    }

		/*------------------------------------*/


		/*support page*/
	    [HttpGet]
	    public ActionResult AddBasket(int id, string rtl)
	    {
		    if (User.Identity.IsAuthenticated)
		    {
			    DateTime time_creation = new DateTime();
			    time_creation = DateTime.Now;
			    string create = String.Format("{0}/{1}/{2} {3}:{4}:{5}",
				    time_creation.Day, time_creation.Month, time_creation.Year, time_creation.Hour, time_creation.Minute,
				    time_creation.Second);

			    DateTime time_out = new DateTime();
			    time_out = time_creation.AddDays(20);
			    string oute = String.Format("{0}/{1}/{2} {3}:{4}:{5}", time_out.Day, time_out.Month, time_out.Year, time_out.Hour,
				    time_out.Minute, time_out.Second);
			    string sql = "select id_user from Users where Nick_name = '" + User.Identity.Name + "'";

			    int userId = ReadSqlServer.GetIdItem(connect, sql);

			    sql = "Select COUNT(*)  from Basket where User_id = " + userId + " And Product_id = " + id + "";

			    if (ReadSqlServer.GetCountItem(connect, sql) > 0)
			    {
				    return RedirectToAction("Detail", "Home", new RouteValueDictionary(new {id = id}));
			    }
			    else
			    {
				    sql =
					    "Insert into Basket(User_id, Product_id, time_create, time_out) values('" + userId + "','" + id +
					    "',Convert(datetime,'" + create + "',103),Convert(datetime,'" + oute + "',103))";

				    WriteSqlServer.UpdateDBonQuery(connect, sql);

				    if (rtl == "0")
				    {
					    return RedirectToAction("Cars");
				    }
				    else
				    {
						return RedirectToAction("Cars_Maker", new {id = rtl});   
				    }
			    }
		    }
		    else
		    {
			    if (rtl == "0")
			    {
				    return RedirectToAction("Detail", "Home", new RouteValueDictionary(new {id = id}));
			    }
				else
				{
					return RedirectToAction("Cars_Maker", new { id = rtl });
				}
		    }
	    }

		[HttpGet]
		public ActionResult DeleteBasket(int id)
		{
			if (User.Identity.IsAuthenticated)
			{
				string sql = "select id_user from Users where Nick_name = '" + User.Identity.Name + "'";

				int userId = ReadSqlServer.GetIdItem(connect, sql);

				sql = "delete from Basket where User_id = " + userId + " And Product_id = " + id + "";

				WriteSqlServer.UpdateDBonQuery(connect, sql);

				return RedirectToAction("Basket");
			}
			else
			{
				return RedirectToAction("Basket");
			}
		}

		[HttpGet]
		public ActionResult DeleteAllBasket()
		{
			if (User.Identity.IsAuthenticated)
			{
				string sql = "select id_user from Users where Nick_name = '" + User.Identity.Name + "'";

				int userId = ReadSqlServer.GetIdItem(connect, sql);

				sql = "delete from Basket where User_id = " + userId + "";

				WriteSqlServer.UpdateDBonQuery(connect, sql);

				return RedirectToAction("Basket");
			}
			else
			{
				return RedirectToAction("Basket");
			}
		}

	    public ActionResult BuyCar(string id, string rtl)
	    {
		    if (User.Identity.IsAuthenticated)
		    {
			    ViewBag.Id = id;
			    ViewBag.Rtl = rtl;
			    return View();
		    }
		    return Redirect("~/Home/Index");
	    }
		 
		[HttpGet]
	    public ActionResult AddOrder( string phone, string addr, string id, string rtl)
		{
			int userId = ReadSqlServer.GetIdItem(connect, "select id_user from Users where Nick_name = '" + User.Identity.Name + "'");

		    string sql = "insert into [Order](Id_product, Id_user, Phone_user, Address_User, Date_order, Status)";
		    string sqldel = "delete from Basket where User_id = " + userId + " And Product_id = " + id + "";
			string sqlbefore = "";
		    if (User.Identity.IsAuthenticated)
		    {
				Order ord = new Order(Convert.ToInt32(id), userId, addr, phone);

			    sqlbefore = " values('" + ord.id_product + "','" + ord.id_user + "','"+ord.phone+"','"+ord.addr+ "',Convert(datetime,'" + ord.date + "',103),Cast(" + ord.Stat() + " as bit))";

			    WriteSqlServer.UpdateDBonQuery(connect, sql + sqlbefore);

				WriteSqlServer.UpdateDBonQuery(connect, sqldel);

			    if (rtl == "-1")
			    {
				    return RedirectToAction("Basket", "Home");
			    }
				else if (rtl == "0")
				{
					return RedirectToAction("Detail", new {id = id});
				}
				else
				{
					return RedirectToAction("Cars_Maker", new {id = rtl});
				}
		    }
			return Redirect("~/Home/Index");
		}

	    [HttpGet]
		public ActionResult AddOrderAll(string phone, string addr)
	    {
		    string listid = "", sql = "", sqlbefore = "";
		    string[] id;
		    if (User.Identity.IsAuthenticated)
		    {
				
				int userId = ReadSqlServer.GetIdItem(connect,
					"select id_user from Users where Nick_name = '" + User.Identity.Name + "'");

				listid = ReadSqlServer.GetIdBaketUser(connect, userId);

			    id = listid.Split(',');

				sql = "insert into [Order](Id_product, Id_user, Phone_user, Address_User, Date_order, Status)";

			    foreach (string ch in id)
			    {
					Order ord = new Order(Convert.ToInt32(ch), userId, addr, phone);
					sqlbefore = " values('" + ord.id_product + "','" + ord.id_user + "','" + ord.phone + "','" + ord.addr + "',Convert(datetime,'" + ord.date + "',103),Cast(" + ord.Stat() + " as bit))";
					WriteSqlServer.UpdateDBonQuery(connect, sql + sqlbefore);
			    }

				sql = "delete from Basket where User_id = " + userId + "";

				WriteSqlServer.UpdateDBonQuery(connect, sql);

			    return RedirectToAction("Basket");
		    }
			return RedirectToAction("Basket");
	    }

		/*---------------------------------------*/
	    /*Json object for Angular*/

		/*Index.cshtml*/
	    public JsonResult GetMakers()
	    {
		    return Json(ReadSqlServer.ReadMakers(connect), JsonRequestBehavior.AllowGet);
	    }

	    public int GetCountItemBasket()
	    {
			if (User.Identity.IsAuthenticated)
		    {
				int userId = ReadSqlServer.GetIdItem(connect,
					"select id_user from Users where Nick_name = '" + User.Identity.Name + "'");

			    int count = ReadSqlServer.GetCountItemBasket(connect, userId);

			    return count;
		    }
		    return 0;
	    }

	    public JsonResult ReadCommentsIndex()
	    {
		    return Json(ReadSqlServer.ReadCommentsProduct(connect), JsonRequestBehavior.AllowGet);
	    }
		/*----------------------*/

		/*Cars.cshtml*/
	    public JsonResult GetProductDate(string sql)
	    {
			IEnumerable<ProductInfo> product = ReadSqlServer.ReadProductInfos(connect,sql);

		    return Json(product, JsonRequestBehavior.AllowGet);
	    }

		public JsonResult GetProductDateNamber(string sql,int number)
		{
			IEnumerable<ProductInfo> product = ReadSqlServer.ReadProductInfos(connect, sql, number);

			return Json(product, JsonRequestBehavior.AllowGet);
		}

	    public JsonResult GetCountProduct(string sql)
	    {
			List<Pages> page = new List<Pages>();
		    int count = ReadSqlServer.ReadCountProduct(connect, sql);
		    for (int i = 0; i < Math.Ceiling((double)count/6); i++)
		    {
			    if (i == 0)
			    {
				    page.Add(new Pages(i + 1, "active"));
			    }
			    else
			    {
				    page.Add(new Pages(i+1, ""));
			    }
		    }

		    return Json(page, JsonRequestBehavior.AllowGet);
	    }

	    public JsonResult CarsModelItem()
	    {
			ItemGroup item = new ItemGroup();

			item.GetData(connect);
		    return Json(item, JsonRequestBehavior.AllowGet);
	    }

		public JsonResult _MakersInCars(string maker)
		{
			List<SelectListItem> model = ReadSqlServer.ReadModels(connect, maker);

			return Json(model, JsonRequestBehavior.AllowGet);
		}
		/*-----------------------------------*/

		/*BuyCar.chtml*/

	    public JsonResult DateUser()
	    {
		    if (User.Identity.IsAuthenticated)
		    {
			    return Json(ReadSqlServer.ReadUserV(connect, User.Identity.Name), JsonRequestBehavior.AllowGet);
		    }
		    return Json("error", JsonRequestBehavior.AllowGet);
	    }
		/*---------------------------------------*/

		/*Detail.cshtml*/

		[HttpGet]
		public JsonResult AddComment(string id, string comment)
		{
			string sql = "insert into Comment(Commentariy,id_user, date_add)";

			int userId = ReadSqlServer.GetIdItem(connect,
					"select id_user from Users where Nick_name = '" + User.Identity.Name + "'");

			sql += " values('" + comment + "','" + userId + "',Convert(datetime,'"+DateTime.Now+"',103))";
			int id_return = WriteSqlServer.UpdateDBonQueryId(connect, sql);

			if (id_return < 1) return Json("Error add comment", JsonRequestBehavior.AllowGet);

			sql = "insert into ComVsProduct(id_comments, id_product) values('" + id_return + "','" + id + "')";
			if (WriteSqlServer.UpdateDBonQuery(connect, sql)) return Json("Comment added", JsonRequestBehavior.AllowGet);
			else return Json("Error add comment", JsonRequestBehavior.AllowGet);
		}

	    [HttpGet]
	    public JsonResult ReadComments(string id)
	    {
		    List<Comment> list = ReadSqlServer.ReadCommentsProduct(connect, id);
		    return Json(list, JsonRequestBehavior.AllowGet);
	    }
		/*-----------------------------------------*/

		/*----------------------------------------------*/
    }
}
