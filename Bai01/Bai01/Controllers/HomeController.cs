using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bai01.Models;

namespace Bai01.Controllers
{
    public class HomeController : Controller
    {
        QL_TinTucEntities data = new QL_TinTucEntities();
        public ActionResult Index()
        {
            List<Theloaitin> ds = data.Theloaitins.ToList();
            return View(ds);
        }
        public ActionResult ThemMoi()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Theloaitin ltin)
        {
            // Tạo thủ công id
            //if (data.Theloaitins.Any())
            //{
            //    int maxId = data.Theloaitins.Max(t => t.IDLoai);
            //    ltin.IDLoai = maxId + 1;
            //}
            //else
            //{
            //    ltin.IDLoai = 1;
            //}
            // Tạo tự động vào .edmx chọn property của id chọn StoreGeneratedPattern là Identity
            data.Theloaitins.Add(ltin);
            data.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var EB_tin = data.Theloaitins.First(m => m.IDLoai == id);
            return View(EB_tin);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var Ltin = data.Theloaitins.First(m => m.IDLoai == id);
            var E_Loaitin = collection["Tentheloai"];

            Ltin.IDLoai = id;

            Ltin.Tentheloai = E_Loaitin;

            UpdateModel(Ltin);
            data.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            var Details_tin = data.Theloaitins.Where(m => m.IDLoai == id).First();
            return View(Details_tin);
        }
        public ActionResult Delete(int id)
        {
            var D_tin = data.Theloaitins.First(m => m.IDLoai == id);
            return View(D_tin);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_tin = data.Theloaitins.Where(m => m.IDLoai == id).First();
            data.Theloaitins.Remove(D_tin);
            data.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}