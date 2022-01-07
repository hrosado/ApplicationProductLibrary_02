using Apl_Mvc_App.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apl_Mvc_App.Controllers
{
    public class HomeController : Controller
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
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}