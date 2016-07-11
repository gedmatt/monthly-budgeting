using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Budgeting.Web.Controllers
{
    public class ItemController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ItemGetList()
        {
            //TODO: Get list of items, make it re-orderable in the business object
            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}