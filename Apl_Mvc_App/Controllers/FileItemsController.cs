using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Apl_Mvc_App.Models;
using PagedList;

namespace Apl_Mvc_App.Controllers
{
    public class FileItemsController : Controller
    {
        private AplDb_LocalEntities_ db = new AplDb_LocalEntities_();

        // GET: FileItems
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FileNameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var fileItems = from s in db.APLFileItems
                            select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                fileItems = fileItems.Where(s => s.FileName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    fileItems = fileItems.OrderByDescending(s => s.FileName);
                    break;
                case "Date":
                    fileItems = fileItems.OrderBy(s => s.TimeStamp);
                    break;
                case "date_desc":
                    fileItems = fileItems.OrderByDescending(s => s.TimeStamp);
                    break;
                default:
                    fileItems = fileItems.OrderBy(s => s.FileName);
                    break;
            }


            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(fileItems.ToPagedList(pageNumber, pageSize));
            //return View(fileItems.ToList());
            // return View(db.APLFileItems.ToList());
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
