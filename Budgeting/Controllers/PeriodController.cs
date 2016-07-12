using Budgeting.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Budgeting.Web.Controllers
{
    public class PeriodController : Controller
    {
        // GET: Period
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult PeriodGetList()
        {
            //TODO: Get list of periods, make it re-orderable in the business object
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetbyID(int periodId)
        {
            object period = null;
            try
            {
                object[] parameters = { periodId };
                //TODO: Period business object method to get a view model
            }
            catch { }
            return Json(period, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Insert(PeriodViewModel model)
        {
            var status = false;
            if (ModelState.IsValid)
            {
                status = true;
                //TODO: Period business object method to save a view model
            }
            return Json(new { success = status });
        }

        public ActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Update(PeriodViewModel model)
        {
            var status = false;
            if (ModelState.IsValid)
            {
                status = true;
                //TODO: Period business object method to update a view model
            }
            return Json(new { success = status });
        }

        [HttpDelete]
        public JsonResult Delete(int periodId)
        {
            var status = true;
            //TODO: Period business object method to delete an entry
            return Json(new { success = status });
        }
    }
}