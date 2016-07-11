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
    }
}