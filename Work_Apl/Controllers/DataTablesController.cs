using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Work_Apl.Models;

namespace Work_Apl.Controllers
{
    public class DataTablesController : Controller
    {
        private AplDbEntities db = new AplDbEntities();
        [Serializable]
        private class FileItems
        {
            public int Id { get; set; }
            public string FileName { get; set; }
            public string FileSize { get; set; }
            public DateTime TimeStamp { get; set; }


        }

        [HttpGet]
        public JsonResult GetData()
        {
            var data = (from file in db.APLFileItems
                        select file).ToList();

            //string jsonString = (from file in db.APLFileItems
            //                     select file).ToString();

            //var results = JsonConvert.DeserializeObject<List<APLFileItem>>(data.ToString());
            // return Json(new { data = results });
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }
        // GET: DataTables
        public ActionResult Index()
        {
            return View(db.APLFileItems.ToList());
        }

        // GET: DataTables/Details/5
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

        // GET: DataTables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DataTables/Create
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

        // GET: DataTables/Edit/5
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

        // POST: DataTables/Edit/5
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

        // GET: DataTables/Delete/5
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

        // POST: DataTables/Delete/5
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
