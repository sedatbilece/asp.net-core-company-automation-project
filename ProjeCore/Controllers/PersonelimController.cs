using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjeCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeCore.Controllers
{
    public class PersonelimController : Controller
    {

        Context c = new Context();
        [Authorize]//giriş işlemi gereken sayfayı belirtmek için
        public IActionResult Index()
        {

            var degerler = c.Personels.Include(x => x.Birim).ToList();
            //include ile personel içine birim aktarılmış olacaka
            return View(degerler);
        }
        [HttpGet]
        public IActionResult YeniPersonel()
        {

            //select listin içi dolduruluyor
            List<SelectListItem> degerler = (from x in c.Birims.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.BirimAd,
                                                 Value = x.BirimId.ToString()
                                             }).ToList();

            ViewBag.dgr = degerler;
           
            return View();
        }
        [HttpPost]
        public IActionResult YeniPersonel(Personel p)
        {
            var per = c.Birims.Where(x => x.BirimId == p.Birim.BirimId).FirstOrDefault();
            p.Birim = per;//pers classının birim bölümüne formdan seçilen birim bölümü atandı
            c.Personels.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}
