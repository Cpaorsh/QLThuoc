using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLThuoc.Models;
using QLThuoc.Class;
using System.Web.DynamicData;
using System.Web.UI.WebControls;
using Rotativa;

namespace QLThuoc.Controllers
{
    public class CTPXuatController : Controller
    {
        private aspnet_QLThuocEntities db = new aspnet_QLThuocEntities();

        // GET: CTPXuat
        public ActionResult Index(int? id, int? thuoc)
        {
            var cTPXUATs = db.CTPXUATs.Include(c => c.PXUAT).Include(c => c.THUOC);
            IList<CTPXUATHT> cTPXuatHT = new List<CTPXUATHT>();

            cTPXuatHT = cTPXUATs.Select(x => new CTPXUATHT()
            {
                THUOC = x.THUOC,
                PXUAT = x.PXUAT,
                id = x.id,
                mathuoc = x.mathuoc,
                maphieu = x.maphieu,
                slxuat = x.slxuat,
                thanhtien = x.slxuat * x.THUOC.dongia
            }).ToList();

            
            if (id == null)
                if (thuoc == null)
                {
                    return View(cTPXuatHT.ToList());
                }
                else
                    return View(cTPXuatHT.Where(x => x.mathuoc == thuoc).ToList());
            else
                return View(cTPXuatHT.Where(x => x.maphieu == id).ToList());
        }

        // GET: CTPXuat/Details/5
        public ActionResult PrintPDF(int? id)
        {
            var cTPXUATs = db.CTPXUATs.Include(c => c.PXUAT).Include(c => c.THUOC);
            IList<CTPXUATHT> cTPXuatHT = new List<CTPXUATHT>();

            cTPXuatHT = cTPXUATs.Select(x => new CTPXUATHT()
            {
                THUOC = x.THUOC,
                PXUAT = x.PXUAT,
                id = x.id,
                mathuoc = x.mathuoc,
                maphieu = x.maphieu,
                slxuat = x.slxuat,
                thanhtien = x.slxuat * x.THUOC.dongia
            }).ToList();
            List<CTPXUATHT> Data = cTPXuatHT.ToList();

            if (id == null)
            {
                Data = cTPXuatHT.ToList();
            }
            else
                Data = cTPXuatHT.Where(x => x.maphieu == id).ToList();

            return new PartialViewAsPdf("PrintPDF", Data)
            {
                FileName = "Phieuxuat.pdf"
            };
        }

        [Authorize(Roles = "Thủ kho")]
        // GET: CTPXuat/Create
        public ActionResult Create()
        {
            ViewBag.maphieu = new SelectList(db.PXUATs, "maphieu", "maphieu");
            ViewBag.mathuoc = new SelectList(db.THUOCs, "mathuoc", "tenthuoc");
            return View();
        }

        // POST: CTPXuat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,maphieu,mathuoc,slxuat")] CTPXUAT cTPXUAT)
        {
            if (ModelState.IsValid)
            {
                var tongHop = db.TONGHOPs.FirstOrDefault(x => x.mathuoc == cTPXUAT.mathuoc);
                if (cTPXUAT.slxuat <= tongHop.sldau+tongHop.slnhap-tongHop.slxuat-tongHop.slhaohut && cTPXUAT.slxuat >0)
                {
                    if (tongHop != null)
                    {
                        db.CTPXUATs.Add(cTPXUAT);
                        db.SaveChanges();
                        tongHop.slxuat += cTPXUAT.slxuat;
                        db.Entry(tongHop).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Messeger = "Số lượng không thỏa mãn";
                }
            }
            ViewBag.maphieu = new SelectList(db.PXUATs, "maphieu", "maphieu", cTPXUAT.maphieu);
            ViewBag.mathuoc = new SelectList(db.THUOCs, "mathuoc", "tenthuoc", cTPXUAT.mathuoc);
            return View(cTPXUAT);
        }

        [Authorize(Roles = "Thủ kho")]
        // GET: CTPXuat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTPXUAT cTPXUAT = db.CTPXUATs.Find(id);
            if (cTPXUAT == null)
            {
                return HttpNotFound();
            }
            ViewBag.maphieu = new SelectList(db.PXUATs, "maphieu", "maphieu", cTPXUAT.maphieu);
            ViewBag.mathuoc = new SelectList(db.THUOCs, "mathuoc", "tenthuoc", cTPXUAT.mathuoc);
            return View(cTPXUAT);
        }

        // POST: CTPXuat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,maphieu,mathuoc,slxuat")] CTPXUAT cTPXUAT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cTPXUAT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maphieu = new SelectList(db.PXUATs, "maphieu", "maphieu", cTPXUAT.maphieu);
            ViewBag.mathuoc = new SelectList(db.THUOCs, "mathuoc", "tenthuoc", cTPXUAT.mathuoc);
            return View(cTPXUAT);
        }

        [Authorize(Roles = "Thủ kho")]
        // GET: CTPXuat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTPXUAT cTPXUAT = db.CTPXUATs.Find(id);
            if (cTPXUAT == null)
            {
                return HttpNotFound();
            }
            return View(cTPXUAT);
        }

        // POST: CTPXuat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CTPXUAT cTPXUAT = db.CTPXUATs.Find(id);
            var tongHop = db.TONGHOPs.FirstOrDefault(x => x.mathuoc == cTPXUAT.mathuoc);
            db.CTPXUATs.Remove(cTPXUAT);
            db.SaveChanges();
            tongHop.slxuat -= cTPXUAT.slxuat;
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
