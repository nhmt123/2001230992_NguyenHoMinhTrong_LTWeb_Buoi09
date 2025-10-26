using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Bai03.Models;

namespace Bai03.Controllers
{
    public class HomeController : Controller
    {
        QL_NhanSuEntities data = new QL_NhanSuEntities();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DSNhanVien()
        {
            List<Employee> dsnhanvien = data.Employees.ToList();
            return View(dsnhanvien);
        }

        public ActionResult ThemMoi()
        {
            ViewBag.Gender = new SelectList(new List<string> { "Nam", "Nữ" });
            ViewBag.DeptId = new SelectList(data.Departments.ToList().OrderBy(n => n.Name), "DeptId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoi(Employee employee, HttpPostedFileBase fileUpLoad)
        {
            // đưa dữ liệu vào dropdown
            ViewBag.Gender = new SelectList(new List<string> { "Nam", "Nữ" });
            ViewBag.DeptId = new SelectList(data.Departments.ToList().OrderBy(n => n.Name), "DeptId", "Name");
            //Kiểm tra đường dẫn file
            if (fileUpLoad == null)
            {
                ViewBag.ThongBao = "Vui lòng chọn ảnh!";
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
                employee.Avatar = filename;

                // Tạo thủ công id
                if (data.Employees.Any())
                {
                    int maxId = data.Employees.Max(t => t.Id);
                    employee.Id = maxId + 1;
                }
                else
                {
                    employee.Id = 1;
                }
                //Lưu vào CSDL
                data.Employees.Add(employee);
                data.SaveChanges();
            }
            return RedirectToAction("DSNhanVien");
        }

        public ActionResult ThongTinNhanVien(int id)
        {
            Employee empl = data.Employees.SingleOrDefault(n => n.Id == id);
            ViewBag.Masach = empl.Id;
            if (empl == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(empl);
        }

        public ActionResult XoaNhanVien(int id)
        {
            Employee empl = data.Employees.SingleOrDefault(n => n.Id == id);
            if (empl == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(empl);
        }
        [HttpPost]
        public ActionResult XoaNhanVien(int id, FormCollection collection)
        {
            Employee empl = data.Employees.SingleOrDefault(n => n.Id == id);
            if (empl == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.Employees.Remove(empl);
            data.SaveChanges();
            return RedirectToAction("DSNhanVien");
        }

        public ActionResult SuaThongTinNV(int id)
        {
            Employee empl = data.Employees.SingleOrDefault(n => n.Id == id);
            if (empl == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.Gender = new SelectList(new List<string> { "Nam", "Nữ" }, empl.Id);
            ViewBag.DeptId = new SelectList(data.Departments.ToList().OrderBy(n => n.Name), "DeptId", "Name", empl.Id);
            return View(empl);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaThongTinNV(int id, HttpPostedFileBase fileUpLoad)
        {
            Employee empl = data.Employees.SingleOrDefault(n => n.Id == id);
            if (empl == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            // đưa dữ liệu vào dropdown
            ViewBag.Gender = new SelectList(new List<string> { "Nam", "Nữ" }, empl.Id);
            ViewBag.DeptId = new SelectList(data.Departments.ToList().OrderBy(n => n.Name), "DeptId", "Name", empl.Id);
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
                if (System.IO.File.Exists(path) && filename != empl.Avatar)
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại!";
                }
                else
                {
                    //Lưu hình ảnh vào đường dẫn
                    fileUpLoad.SaveAs(path);
                }
                //Gán tên ảnh vào đối tượng
                empl.Avatar = filename;
                //Lưu vào CSDL
                UpdateModel(empl);
                data.SaveChanges();
            }
            return RedirectToAction("DSNhanVien");
        }

        public ActionResult DanhSachNV_PB()
        {
            List<Employee> dsnhanvien = data.Employees.ToList();
            return View(dsnhanvien);
        }
        public ActionResult DSPhongBan()
        {
            List<Department> dsphongban = data.Departments.ToList();
            return View(dsphongban);
        }
        public ActionResult ThongTinNV_PB(int id)
        {
            Department dept = data.Departments.SingleOrDefault(d => d.DeptId == id);
            if (dept == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Employee> dsnhanvien = data.Employees.Where(e => e.DeptId == id).ToList();
            return View(dsnhanvien);
        }
    }
}