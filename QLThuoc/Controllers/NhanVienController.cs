using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLThuoc.Models;

namespace QLThuoc.Controllers
{
    public class NhanVienController : Controller
    {
        private aspnet_QLThuocEntities db = new aspnet_QLThuocEntities();

        // GET: NhanVien
        public ActionResult Index()
        {
            var nHANVIENs = db.NHANVIENs.Include(n => n.AspNetUser);
            return View(nHANVIENs.ToList());
        }

        // GET: NhanVien/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHANVIEN nHANVIEN = db.NHANVIENs.Find(id);
            if (nHANVIEN == null)
            {
                return HttpNotFound();
            }
            return View(nHANVIEN);
        }

        [Authorize(Roles = "Trạm trưởng")]
        // GET: NhanVien/Create
        public ActionResult Create()
        {
            ViewBag.manv = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: NhanVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "manv,tennv,chucvu,ngaysinh,diachi")] NHANVIEN nHANVIEN)
        {
            if (ModelState.IsValid)
            {
                db.NHANVIENs.Add(nHANVIEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.manv = new SelectList(db.AspNetUsers, "Id", "Email", nHANVIEN.manv);
            return View(nHANVIEN);
        }

        [Authorize(Roles = "Trạm trưởng")]
        // GET: NhanVien/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHANVIEN nHANVIEN = db.NHANVIENs.Find(id);
            if (nHANVIEN == null)
            {
                return HttpNotFound();
            }
            ViewBag.manv = new SelectList(db.AspNetUsers, "Id", "Email", nHANVIEN.manv);
            return View(nHANVIEN);
        }

        // POST: NhanVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "manv,tennv,chucvu,ngaysinh,diachi")] NHANVIEN nHANVIEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nHANVIEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.manv = new SelectList(db.AspNetUsers, "Id", "Email", nHANVIEN.manv);
            return View(nHANVIEN);
        }

        [Authorize(Roles = "Trạm trưởng")]
        // GET: NhanVien/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHANVIEN nHANVIEN = db.NHANVIENs.Find(id);
            if (nHANVIEN == null)
            {
                return HttpNotFound();
            }
            return View(nHANVIEN);
        }

        // POST: NhanVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NHANVIEN nHANVIEN = db.NHANVIENs.Find(id);
            db.NHANVIENs.Remove(nHANVIEN);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
