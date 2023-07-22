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
using Rotativa;

namespace QLThuoc.Controllers
{
    public class CTPNhapController : Controller
    {
        private aspnet_QLThuocEntities db = new aspnet_QLThuocEntities();

        // GET: CTPNhap
        public ActionResult Index(int? id, int? thuoc)
        {
            var cTPNHAPs = db.CTPNHAPs.Include(c => c.PNHAP).Include(c => c.THUOC);
            if (id == null)
                if (thuoc == null)
                {
                    return View(cTPNHAPs.ToList());
                }    
                else
                    return View(cTPNHAPs.Where(x => x.mathuoc == thuoc).ToList());
            else
                return View(cTPNHAPs.Where(x => x.maphieu == id).ToList());
        }

        // GET: CTPNhap/Details/5
        public ActionResult PrintPDF(int? id)
        {

            List<CTPNHAP> Data = db.CTPNHAPs.ToList();
            if (id == null)
            {
                Data = Data.ToList();
            }
            else
                Data = Data.Where(x => x.maphieu == id).ToList();
            return new PartialViewAsPdf("PrintPDF", Data)
            {
                FileName = "Phieunhap.pdf"
            };
        }

        [Authorize(Roles = "Thủ kho")]
        // GET: CTPNhap/Create
        public ActionResult Create()
        {
            ViewBag.maphieu = new SelectList(db.PNHAPs, "maphieu", "maphieu");
            ViewBag.mathuoc = new SelectList(db.THUOCs, "mathuoc", "tenthuoc");
            return View();
        }

        // POST: CTPNhap/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,maphieu,mathuoc,soks,noisx,hsd,slnhap")] CTPNHAP cTPNHAP)
        {
            if (ModelState.IsValid)
            {
                db.CTPNHAPs.Add(cTPNHAP);
                db.SaveChanges();

                var tongHop = db.TONGHOPs.FirstOrDefault(x=>x.mathuoc == cTPNHAP.mathuoc);
                if (tongHop != null)
                {
                    tongHop.slnhap+=cTPNHAP.slnhap;
                    db.Entry(tongHop).State = EntityState.Modified;
                    db.SaveChanges();
                } 
                    
                return RedirectToAction("Index");
            }

            ViewBag.maphieu = new SelectList(db.PNHAPs, "maphieu", "maphieu", cTPNHAP.maphieu);
            ViewBag.mathuoc = new SelectList(db.THUOCs, "mathuoc", "tenthuoc", cTPNHAP.mathuoc);
            return View(cTPNHAP);
        }

        [Authorize(Roles = "Thủ kho")]
        // GET: CTPNhap/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTPNHAP cTPNHAP = db.CTPNHAPs.Find(id);
            if (cTPNHAP == null)
            {
                return HttpNotFound();
            }
            ViewBag.maphieu = new SelectList(db.PNHAPs, "maphieu", "maphieu", cTPNHAP.maphieu);
            ViewBag.mathuoc = new SelectList(db.THUOCs, "mathuoc", "tenthuoc", cTPNHAP.mathuoc);
            var tongHop = db.TONGHOPs.FirstOrDefault(x => x.mathuoc == cTPNHAP.mathuoc);
            tongHop.slnhap -= cTPNHAP.slnhap;
            db.Entry(tongHop).State = EntityState.Modified;
            db.SaveChanges();
            return View(cTPNHAP);
        }

        // POST: CTPNhap/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,maphieu,mathuoc,soks,noisx,hsd,slnhap")] CTPNHAP cTPNHAP)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(cTPNHAP).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            if (ModelState.IsValid)
            {

                db.Entry(cTPNHAP).State = EntityState.Modified;
                db.SaveChanges();

                var tongHop = db.TONGHOPs.FirstOrDefault(x => x.mathuoc == cTPNHAP.mathuoc);
                tongHop.slnhap += cTPNHAP.slnhap;
                db.Entry(tongHop).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.maphieu = new SelectList(db.PNHAPs, "maphieu", "maphieu", cTPNHAP.maphieu);
            ViewBag.mathuoc = new SelectList(db.THUOCs, "mathuoc", "tenthuoc", cTPNHAP.mathuoc);
            return View(cTPNHAP);
        }

        [Authorize(Roles = "Thủ kho")]
        // GET: CTPNhap/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTPNHAP cTPNHAP = db.CTPNHAPs.Find(id);
            if (cTPNHAP == null)
            {
                return HttpNotFound();
            }
            return View(cTPNHAP);
        }

        // POST: CTPNhap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CTPNHAP cTPNHAP = db.CTPNHAPs.Find(id);
            var tongHop = db.TONGHOPs.FirstOrDefault(x => x.mathuoc == cTPNHAP.mathuoc);
            db.CTPNHAPs.Remove(cTPNHAP);
            db.SaveChanges();
            tongHop.slnhap -= cTPNHAP.slnhap;
            db.Entry(tongHop).State = EntityState.Modified;
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
