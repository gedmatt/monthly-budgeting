using Budgeting.ViewModel;
using Budgeting.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Budgeting.Web.Controllers
{
    public class PeriodController : Controller
    {
        protected ModelContext _context;
        
        public PeriodController()
        {
            _context = new ModelContext();
        }

        public PeriodController(ModelContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetPeriodList()
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
                //TODO: Do dependency injection to shove in context and unit test this
                object[] parameters = { periodId };
                var serializer = new JavaScriptSerializer();
                //serializer.Serialize()
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