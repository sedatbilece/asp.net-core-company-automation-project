using Microsoft.AspNetCore.Mvc;
using ProjeCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeCore.Controllers
{
    public class BirimController : Controller
    {

        Context c = new Context();
        public IActionResult Index()
        {
            var deger = c.Birims.ToList();
            return View(deger);
        }

        [HttpGet]
        public IActionResult YeniBirim()
        {
            return View();
        }
        [HttpPost]
        public IActionResult YeniBirim(Birim d)//post değeri forma girilen değerlerin tutulduğu nesne
        {
            c.Birims.Add(d);
            c.SaveChanges();


            return RedirectToAction("Index");//aksiyona git 
        }


        public IActionResult BirimSil(int id)
        {
            var dep = c.Birims.Find(id);
            c.Birims.Remove(dep);
            c.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult BirimGetir(int id)
        {
            var depart = c.Birims.Find(id);


            return View(depart);
        }

        public IActionResult BirimGüncelle(Birim d)
        {
            var dep = c.Birims.Find(d.BirimId);
            dep.BirimAd = d.BirimAd;
            
            c.SaveChanges();

            return RedirectToAction("Index");
        }



        public IActionResult DetayGetir(int id)
        {

            var bas = c.Birims.Find(id);//birim adını bulmak
            ViewBag.baslik = bas.BirimAd;


            var deg = c.Personels.Where(x => x.BirimId == id).ToList();
            return View(deg);
        }
    }
}
