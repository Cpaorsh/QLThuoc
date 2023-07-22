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
    public class ThuocController : Controller
    {
        private aspnet_QLThuocEntities db = new aspnet_QLThuocEntities();

        // GET: Thuoc
        public ActionResult Index(string loai, string search)
        {
            if (loai == null)
            {
                if (search == null)
                    return View(db.THUOCs.ToList());
                else
                    return View(db.THUOCs.Where(s => s.tenthuoc.StartsWith(search)).ToList());
                    //return View(db.THUOCs.Where(s => s.tenthuoc.StartsWith(search) || s => s.hoachat.StartsWith(search)).ToList());
            }
            else
            {
                //if (search == null)
                    return View(db.THUOCs.Where(s => s.loaithuoc == loai).ToList());
                //else
                //    return View(db.THUOCs.Where(s => s.tenthuoc.StartsWith(search) + s.loaithuoc == loai).ToList());
            }
        }

        // GET: Thuoc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THUOC tHUOC = db.THUOCs.Find(id);
            if (tHUOC == null)
            {
                return HttpNotFound();
            }
            return View(tHUOC);
        }

        [Authorize(Roles = "Thủ kho")]
        // GET: Thuoc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Thuoc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "mathuoc,tenthuoc,hoatchat,loaithuoc,dvtinh,dongia")] THUOC tHUOC)
        {
            if (ModelState.IsValid)
            {
                db.THUOCs.Add(tHUOC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tHUOC);
        }

        [Authorize(Roles = "Thủ kho")]
        // GET: Thuoc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THUOC tHUOC = db.THUOCs.Find(id);
            if (tHUOC == null)
            {
                return HttpNotFound();
            }
            return View(tHUOC);
        }

        // POST: Thuoc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "mathuoc,tenthuoc,hoatchat,loaithuoc,dvtinh,dongia")] THUOC tHUOC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tHUOC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tHUOC);
        }

        [Authorize(Roles = "Thủ kho")]
        // GET: Thuoc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THUOC tHUOC = db.THUOCs.Find(id);
            if (tHUOC == null)
            {
                return HttpNotFound();
            }
            return View(tHUOC);
        }

        // POST: Thuoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            THUOC tHUOC = db.THUOCs.Find(id);
            db.THUOCs.Remove(tHUOC);
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
