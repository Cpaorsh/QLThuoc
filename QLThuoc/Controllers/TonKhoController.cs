using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLThuoc.Models;
using Rotativa;

namespace QLThuoc.Controllers
{
    public class TonKhoController : Controller
    {
        private aspnet_QLThuocEntities db = new aspnet_QLThuocEntities();

        // GET: TonKho
        public ActionResult Index(int? id, int? thuoc)
        {
            var tONKHOes = db.TONKHOes.Include(t => t.KIEMKE).Include(t => t.THUOC);
            if (id == null)
                if (thuoc == null)
                {
                    return View(tONKHOes.ToList());
                }
                else
                    return View(tONKHOes.Where(x => x.mathuoc == thuoc).ToList());
            else
                return View(tONKHOes.Where(x => x.mabb == id).ToList());
        }

        // GET: TonKho/Details/5
        public ActionResult PrintPDF(int? id)
        {

            List<TONKHO> Data = db.TONKHOes.ToList();

            if (id == null)
            {
                Data = Data.ToList();
            }
            else
                Data = Data.Where(x => x.mabb == id).ToList();

            return new PartialViewAsPdf("PrintPDF", Data)
            {
                FileName = "BienBanKiemKe.pdf"
            };
        }

        [Authorize(Roles = "Thủ kho")]
        // GET: TonKho/Create
        public ActionResult Create()
        {
            ViewBag.mabb = new SelectList(db.KIEMKEs, "mabb", "mabb");
            ViewBag.mathuoc = new SelectList(db.THUOCs, "mathuoc", "tenthuoc");
            return View();
        }

        // POST: TonKho/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,mathuoc,mabb,hsd,slton,slhong")] TONKHO tONKHO)
        {
            if (ModelState.IsValid)
            {
                db.TONKHOes.Add(tONKHO);
                db.SaveChanges();

                var tongHop = db.TONGHOPs.FirstOrDefault(x => x.mathuoc == tONKHO.mathuoc);
                if (tongHop != null)
                {
                    tongHop.slhaohut += tONKHO.slhong;
                    db.Entry(tongHop).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.mabb = new SelectList(db.KIEMKEs, "mabb", "mabb", tONKHO.mabb);
            ViewBag.mathuoc = new SelectList(db.THUOCs, "mathuoc", "tenthuoc", tONKHO.mathuoc);
            return View(tONKHO);
        }

        [Authorize(Roles = "Thủ kho")]
        // GET: TonKho/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TONKHO tONKHO = db.TONKHOes.Find(id);
            if (tONKHO == null)
            {
                return HttpNotFound();
            }
            ViewBag.mabb = new SelectList(db.KIEMKEs, "mabb", "mabb", tONKHO.mabb);
            ViewBag.mathuoc = new SelectList(db.THUOCs, "mathuoc", "tenthuoc", tONKHO.mathuoc);
            return View(tONKHO);
        }

        // POST: TonKho/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,mathuoc,mabb,hsd,slton,slhong")] TONKHO tONKHO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tONKHO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.mabb = new SelectList(db.KIEMKEs, "mabb", "mabb", tONKHO.mabb);
            ViewBag.mathuoc = new SelectList(db.THUOCs, "mathuoc", "tenthuoc", tONKHO.mathuoc);
            return View(tONKHO);
        }

        [Authorize(Roles = "Thủ kho")]
        // GET: TonKho/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TONKHO tONKHO = db.TONKHOes.Find(id);
            if (tONKHO == null)
            {
                return HttpNotFound();
            }
            return View(tONKHO);
        }

        // POST: TonKho/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TONKHO tONKHO = db.TONKHOes.Find(id);
            db.TONKHOes.Remove(tONKHO);
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
