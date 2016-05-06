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
            List<Tiempo> lista = new DAO<Tiempo>().ListAll();
            return View();
        }
    }
}