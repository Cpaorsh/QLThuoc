using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLThuoc.Class;
using QLThuoc.Models;

namespace QLThuoc.Controllers
{
    public class PXuatController : Controller
    {
        private aspnet_QLThuocEntities db = new aspnet_QLThuocEntities();

        // GET: PXuat
        public ActionResult Index()
        {
            var pXUATs = db.PXUATs.Include(p => p.NHANVIEN);
            //IList<PXuatHT> pXuatHT = new List<PXuatHT>();

            //pXuatHT = db.PXUATs.Select(x => new PXuatHT()
            //{
            //    maphieu = x.maphieu,
            //    manv = x.manv,
            //    tongtien = x.CTPXUATs.
                
            //    //x.slxuat * (x.THUOC.dongia ?? 0)
            //}).ToList();
            //return View(pXuatHT.ToList());
            return View(pXUATs.ToList());
        }

        // GET: PXuat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PXUAT pXUAT = db.PXUATs.Find(id);
            if (pXUAT == null)
            {
                return HttpNotFound();
            }
            return View(pXUAT);
        }

        [Authorize(Roles = "Thủ kho")]
        // GET: PXuat/Create
        public ActionResult Create()
        {
            ViewBag.manv = new SelectList(db.NHANVIENs, "manv", "tennv");
            return View();
        }

        // POST: PXuat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maphieu,ngayxuat,manv")] PXUAT pXUAT)
        {
            if (ModelState.IsValid)
            {
                db.PXUATs.Add(pXUAT);
                db.SaveChanges();
                return RedirectToAction("Create", "CTPXUAT");
            }

            ViewBag.manv = new SelectList(db.NHANVIENs, "manv", "tennv", pXUAT.manv);
            return View(pXUAT);
        }

        [Authorize(Roles = "Thủ kho")]
        // GET: PXuat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PXUAT pXUAT = db.PXUATs.Find(id);
            if (pXUAT == null)
            {
                return HttpNotFound();
            }
            ViewBag.manv = new SelectList(db.NHANVIENs, "manv", "tennv", pXUAT.manv);
            return View(pXUAT);
        }

        // POST: PXuat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maphieu,ngayxuat,manv")] PXUAT pXUAT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pXUAT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.manv = new SelectList(db.NHANVIENs, "manv", "tennv", pXUAT.manv);
            return View(pXUAT);
        }

        [Authorize(Roles = "Thủ kho")]
        // GET: PXuat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PXUAT pXUAT = db.PXUATs.Find(id);
            if (pXUAT == null)
            {
                return HttpNotFound();
            }
            return View(pXUAT);
        }

        // POST: PXuat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PXUAT pXUAT = db.PXUATs.Find(id);
            db.PXUATs.Remove(pXUAT);
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
