using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Apl_Mvc_App.Models;

namespace Apl_Mvc_App.Controllers
{
    public class FileItemsController : Controller
    {
        private AplDbEntities_ db = new AplDbEntities_();

        // GET: FileItems
        public ActionResult Index()
        {
            return View(db.APLFileItems.ToList());
        }

        // GET: FileItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            APLFileItem aPLFileItem = db.APLFileItems.Find(id);
            if (aPLFileItem == null)
            {
                return HttpNotFound();
            }
            return View(aPLFileItem);
        }

        // GET: FileItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FileItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FileName,TimeStamp,FileSize")] APLFileItem aPLFileItem)
        {
            if (ModelState.IsValid)
            {
                db.APLFileItems.Add(aPLFileItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aPLFileItem);
        }

        // GET: FileItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            APLFileItem aPLFileItem = db.APLFileItems.Find(id);
            if (aPLFileItem == null)
            {
                return HttpNotFound();
            }
            return View(aPLFileItem);
        }

        // POST: FileItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FileName,TimeStamp,FileSize")] APLFileItem aPLFileItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aPLFileItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aPLFileItem);
        }

        // GET: FileItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            APLFileItem aPLFileItem = db.APLFileItems.Find(id);
            if (aPLFileItem == null)
            {
                return HttpNotFound();
            }
            return View(aPLFileItem);
        }

        // POST: FileItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            APLFileItem aPLFileItem = db.APLFileItems.Find(id);
            db.APLFileItems.Remove(aPLFileItem);
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
