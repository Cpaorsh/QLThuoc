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
    public class PNhapController : Controller
    {
        private aspnet_QLThuocEntities db = new aspnet_QLThuocEntities();

        // GET: PNhap
        public ActionResult Index()
        {
            var pNHAPs = db.PNHAPs.Include(p => p.NHANVIEN);
            return View(pNHAPs.ToList());
        }

        // GET: PNhap/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PNHAP pNHAP = db.PNHAPs.Find(id);
            if (pNHAP == null)
            {
                return HttpNotFound();
            }
            return View(pNHAP);
        }

        [Authorize(Roles = "Thủ kho")]
        // GET: PNhap/Create
        public ActionResult Create()
        {
            ViewBag.manv = new SelectList(db.NHANVIENs, "manv", "tennv");
            return View();
        }

        // POST: PNhap/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maphieu,ngaynhap,manv")] PNHAP pNHAP)
        {
            if (ModelState.IsValid)
            {
                db.PNHAPs.Add(pNHAP);
                db.SaveChanges();
                return RedirectToAction("Create" ,"CTPNHAP");
            }

            ViewBag.manv = new SelectList(db.NHANVIENs, "manv", "tennv", pNHAP.manv);
            return View(pNHAP);
        }

        [Authorize(Roles = "Thủ kho")]
        // GET: PNhap/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PNHAP pNHAP = db.PNHAPs.Find(id);
            if (pNHAP == null)
            {
                return HttpNotFound();
            }
            ViewBag.manv = new SelectList(db.NHANVIENs, "manv", "tennv", pNHAP.manv);
            return View(pNHAP);
        }

        // POST: PNhap/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maphieu,ngaynhap,manv")] PNHAP pNHAP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pNHAP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.manv = new SelectList(db.NHANVIENs, "manv", "tennv", pNHAP.manv);
            return View(pNHAP);
        }
        [Authorize(Roles = "Thủ kho")]
        // GET: PNhap/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PNHAP pNHAP = db.PNHAPs.Find(id);
            if (pNHAP == null)
            {
                return HttpNotFound();
            }
            return View(pNHAP);
        }

        // POST: PNhap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PNHAP pNHAP = db.PNHAPs.Find(id);
            db.PNHAPs.Remove(pNHAP);
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
