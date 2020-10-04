using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Spice.Data;

namespace Spice.Areas.Customer.Controllers
{
	public class CartController : Controller
	{
		private readonly ApplicationDbContext _db;
		public CartController1(ApplicationDbContext db)
		{
			_db = db;
		}
		public IActionResult Index()
		{
			return View();
		}
	}
}
