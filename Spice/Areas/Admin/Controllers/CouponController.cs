using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spice.Data;
using Spice.Models;
using Spice.Models.ViewModels;
using Spice.Utility;

namespace Spice.Areas.Admin.Controllers
{
    [Authorize(Roles=SD.ManagerUser)]
    [Area("Admin")]
	public class CouponController : Controller
	{
		private readonly ApplicationDbContext _db;
		public CouponController(ApplicationDbContext db)
		{
			_db = db;
		}
		public async Task <IActionResult> Index()
		{
			return View(await _db.Coupon.ToListAsync());
		}
		public IActionResult Create()
		{
			return View();
		}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Coupon coupons)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }
                    coupons.Picture = p1;
                }
                _db.Coupon.Add(coupons);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coupons);
        }
        public IActionResult Edit(int id)
        {
            string imreBase64Data = "";
            string imgDataURL = "";

            if (id==null)
			{
                return NotFound();
			}
            
            var coupon = _db.Coupon.Find(id);
            if(coupon.Picture!=null)
			{
               imreBase64Data = Convert.ToBase64String(coupon.Picture);
               imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);

            }



            CouponViewModel model = new CouponViewModel()
            {
                Coupon = coupon,
                dataUrl = imgDataURL



            };
            
            if(coupon==null)
			{
                return NotFound();
            }



            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CouponViewModel coupons)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }
                    
                    coupons.Coupon.Picture = p1;
                }
                //var updateModel = coupons.Coupon;
                //var CouponFromDb = await _db.Coupon.FindAsync(coupons.Coupon.Id);
                //CouponFromDb = updateModel;
                var updateModel = coupons.Coupon;
                 _db.Coupon.Update(coupons.Coupon);

                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coupons.Coupon);
        }
        public IActionResult Delete(int id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var coupon = _db.Coupon.Find(id);

            if (coupon == null)
            {
                return NotFound();
            }



            return View(coupon);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Coupon coupons)
        {
          
                _db.Coupon.Remove(coupons);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
        }
    }
}
