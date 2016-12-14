using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cars.Models
{
	public class Order
	{
		public int id_product { get; set; }
		public int id_user { get; set; }
		public string addr { get; set; }
		public string phone { get; set; }
		public DateTime date { get; set; }

		private sbyte stat { get; set; }

		public void Stat_Null()
		{
			this.stat = 0;
		}

		public void Create_Date()
		{
			date = DateTime.Now;
		}

		public int Stat()
		{
			return this.stat;
		}
		/*Constructor*/
		public Order():base(){}

		public Order(int _id_pr, int _id_us, string _adr, string _ph)
		{
			this.id_user = _id_us;
			this.id_product = _id_pr;
			this.phone = _ph;
			this.addr = _adr;
			this.Stat_Null();
			this.Create_Date();
		}
	}

	public class OrderView:ProductInfo
	{
		[Required]
		[Display(Name = "Nick name: ")]
		public string nickName { get; set; }

		[Required]
		[Display(Name = "Address: ")]
		public string addr { get; set; }

		[Required]
		[Display(Name = "Phone: ")]
		[DataType(DataType.PhoneNumber)]
		public string phone { get; set; }

		[Display(Name = "Stat: ")]
		public sbyte stat { get; set; }

		public DateTime date { get; set; }

		[Display(Name = "Type car: ")]
		public List<SelectListItem> typeCars = new List<SelectListItem>();

		public void InitList(SqlConnection _connect)
		{
			this.typeCars.Add(new SelectListItem { Value = "Automatic coupling", Text = "Automatic coupling" });
			this.typeCars.Add(new SelectListItem { Value = "Grain", Text = "Grain" });
			this.typeCars.Add(new SelectListItem { Value = "Minibus", Text = "Minibus" });
			this.typeCars.Add(new SelectListItem { Value = "Tanker", Text = "Tanker" });
			this.typeCars.Add(new SelectListItem { Value = "Transporter", Text = "Transporter" });
			this.typeCars.Add(new SelectListItem { Value = "Wagon", Text = "Wagon" });
		}

		public void Create_Date()
		{
			date = DateTime.Now;
		}

		public OrderView()
		{
			this.maker = "";
			this.model = "";
			this.nickName = "";
			this.addr = "";
			this.phone = "";
			this.price = 0;
			this.year = 0;
			this.stat = 0;
			this.date = new DateTime();

		}


	}

}