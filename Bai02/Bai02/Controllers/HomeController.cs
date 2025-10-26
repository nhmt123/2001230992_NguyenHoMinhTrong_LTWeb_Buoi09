using Bai02.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bai02.Controllers
{
    public class HomeController : Controller
    {
        QL_SachEntities data = new QL_SachEntities();
        public ActionResult DMSach()
        {
            List<Sach> dsSach = data.Saches.ToList();
            return View(dsSach);
        }

        public ActionResult ThemMoi()
        {
            ViewBag.MaCD = new SelectList(data.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe");
            ViewBag.MaNXB = new SelectList(data.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoi(Sach sach, HttpPostedFileBase fileUpLoad)
        {
            // đưa dữ liệu vào dropdown
            ViewBag.MaCD = new SelectList(data.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe");
            ViewBag.MaNXB = new SelectList(data.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            //Kiểm tra đường dẫn file
            if (fileUpLoad == null)
            {
                ViewBag.ThongBao = "Vui lòng chọn ảnh bìa!";
                return View();
            }
            //Thêm vào CSDL
            if (ModelState.IsValid)
            {
                //Lưu tên file
                var filename = Path.GetFileName(fileUpLoad.FileName);
                //Lưu đường dẫn file
                var path = Path.Combine(Server.MapPath("~/Content/Images"), filename);
                //Kiểm tra hình ảnh tồn tại chưa
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại!";
                }
                else
                {
                    //Lưu hình ảnh vào đường dẫn
                    fileUpLoad.SaveAs(path);
                }
                //Gán tên ảnh vào đối tượng
                sach.AnhBia = filename;

                // Tạo thủ công id
                if (data.Saches.Any())
                {
                    int maxId = data.Saches.Max(t => t.MaSach);
                    sach.MaSach = maxId + 1;
                }
                else
                {
                    sach.MaSach = 1;
                }
                //Lưu vào CSDL
                data.Saches.Add(sach);
                data.SaveChanges();
            }
            return RedirectToAction("DMSach");
        }

        public ActionResult ChiTietSach(int id)
        {
            Sach sach = data.Saches.SingleOrDefault(n => n.MaSach == id);
            ViewBag.Masach = sach.MaSach;
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }

        public ActionResult XoaSach(int id)
        {
            Sach sach = data.Saches.SingleOrDefault(n => n.MaSach == id);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        [HttpPost]
        public ActionResult XoaSach(int id, FormCollection collection)
        {
            Sach sach = data.Saches.SingleOrDefault(n => n.MaSach == id);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.Saches.Remove(sach);
            data.SaveChanges();
            return RedirectToAction("DMSach");
        }
        public ActionResult SuaSach(int id)
        {
            Sach sach = data.Saches.SingleOrDefault(n => n.MaSach == id);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaCD = new SelectList(data.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe", sach.MaCD);  
            ViewBag.MaNXB = new SelectList(data.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB", sach.MaNXB);  
            return View(sach);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaSach(int id, HttpPostedFileBase fileUpLoad)
        {
            var sach = data.Saches.SingleOrDefault(n => n.MaSach == id);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            // đưa dữ liệu vào dropdown
            ViewBag.MaCD = new SelectList(data.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe", sach.MaCD);
            ViewBag.MaNXB = new SelectList(data.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB", sach.MaNXB);
            //Kiểm tra đường dẫn file
            if (fileUpLoad == null)
            {
                ViewBag.ThongBao = "Vui lòng chọn ảnh bìa!";
                return View();
            }
            //Thêm vào CSDL
            if (ModelState.IsValid)
            {
                //Lưu tên file
                var filename = Path.GetFileName(fileUpLoad.FileName);
                //Lưu đường dẫn file
                var path = Path.Combine(Server.MapPath("~/Content/Images"), filename);
                //Kiểm tra hình ảnh tồn tại chưa
                if (System.IO.File.Exists(path) && filename != sach.AnhBia) 
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại!";
                }
                else
                {
                    //Lưu hình ảnh vào đường dẫn
                    fileUpLoad.SaveAs(path);
                }
                //Gán tên ảnh vào đối tượng
                sach.AnhBia = filename;
                //Lưu vào CSDL
                UpdateModel(sach);
                data.SaveChanges();
            }
            return RedirectToAction("DMSach");
        }
    }
}