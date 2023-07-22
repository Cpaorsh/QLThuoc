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
    public class TongHopController : Controller
    {
        private aspnet_QLThuocEntities db = new aspnet_QLThuocEntities();

        // GET: TongHop
        public ActionResult Index()
        {
            var tONGHOPs = db.TONGHOPs.Include(t => t.THUOC);

            IList<TONGHOPHT> tongHopHT = new List<TONGHOPHT>();

            tongHopHT = tONGHOPs.Select(x => new TONGHOPHT()
            {
                THUOC = x.THUOC,
                mathuoc = x.mathuoc,
                sldau = x.sldau,
                slnhap = x.slnhap,
                slxuat = x.slxuat,
                slhaohut = x.slhaohut,
                slcuoi = x.sldau + x.slnhap - x.slxuat - x.slhaohut
            }).ToList();

            return View(tongHopHT.ToList());
        }


        public ActionResult PrintPDF()
        {

            var tONGHOPs = db.TONGHOPs.Include(t => t.THUOC);

            IList<TONGHOPHT> tongHopHT = new List<TONGHOPHT>();

            tongHopHT = tONGHOPs.Select(x => new TONGHOPHT()
            {
                THUOC = x.THUOC,
                mathuoc = x.mathuoc,
                sldau = x.sldau,
                slnhap = x.slnhap,
                slxuat = x.slxuat,
                slhaohut = x.slhaohut,
                slcuoi = x.sldau + x.slnhap - x.slxuat - x.slhaohut
            }).ToList();

            List<TONGHOPHT> Data = tongHopHT.ToList();
            return new PartialViewAsPdf("PrintPDF", Data)
            {
                FileName = "Baocao.pdf"
            };
        }

        // GET: TongHop/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TONGHOP tONGHOP = db.TONGHOPs.Find(id);
            if (tONGHOP == null)
            {
                return HttpNotFound();
            }
            return View(tONGHOP);
        }

        // GET: TongHop/Create
        public ActionResult Create()
        {
            ViewBag.mathuoc = new SelectList(db.THUOCs, "mathuoc", "tenthuoc");
            return View();
        }

        // POST: TongHop/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "mathuoc,hsd,sldau,slnhap,slxuat,slhaohut,slcuoi")] TONGHOP tONGHOP)
        {
            if (ModelState.IsValid)
            {
                db.TONGHOPs.Add(tONGHOP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.mathuoc = new SelectList(db.THUOCs, "mathuoc", "tenthuoc", tONGHOP.mathuoc);
            return View(tONGHOP);
        }

        // GET: TongHop/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TONGHOP tONGHOP = db.TONGHOPs.Find(id);
            if (tONGHOP == null)
            {
                return HttpNotFound();
            }
            ViewBag.mathuoc = new SelectList(db.THUOCs, "mathuoc", "tenthuoc", tONGHOP.mathuoc);
            return View(tONGHOP);
        }

        // POST: TongHop/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "mathuoc,hsd,sldau,slnhap,slxuat,slhaohut,slcuoi")] TONGHOP tONGHOP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tONGHOP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.mathuoc = new SelectList(db.THUOCs, "mathuoc", "tenthuoc", tONGHOP.mathuoc);
            return View(tONGHOP);
        }

        // GET: TongHop/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TONGHOP tONGHOP = db.TONGHOPs.Find(id);
            if (tONGHOP == null)
            {
                return HttpNotFound();
            }
            return View(tONGHOP);
        }

        // POST: TongHop/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TONGHOP tONGHOP = db.TONGHOPs.Find(id);
            db.TONGHOPs.Remove(tONGHOP);
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
