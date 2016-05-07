using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using analytics.Models.DAO;
using analytics.Models.DTO;

namespace analytics.Controllers
{
    public class ReporteController : Controller
    {
        // GET: Reporte
        public ActionResult Index()
        {
            //List<Tiempo> lista = new DAO<Tiempo>().ListAll();
            return View();
        }

        public ActionResult Campaign()
        {
            return View("Campaign");
        }

        public JsonResult DataChart()
        {
            var data = new Dictionary<string, List<int>>();
            data.Add("data1", new List<int> { 100, 200, 300, 300, 200, 100 });
            data.Add("data2", new List<int> { 100, 150, 150, 200, 200, 300 });
            return Json(data);
        }
    }
}