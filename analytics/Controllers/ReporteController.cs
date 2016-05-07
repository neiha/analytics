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
            return View();
        }
        public ActionResult Activity()
        {
            return View();
        }
        public ActionResult Campaign()
        {
            return View("Campaign");
        }
        public JsonResult GetResumenActivity()
        {
            ActivityReportResume resume = new ActivityReportResume();
            ActivityDAO adao = new ActivityDAO();
            resume.ActiveUsersCount=adao.CountActiveUsersByDateRange(new DateTime(), new DateTime());
            resume.NewUsersCount = adao.CountNewUsersByDateRange(new DateTime(), new DateTime());
            resume.ReturningUsersCount = adao.CountReturningUsersByDateRange(new DateTime(), new DateTime());
            resume.SessionCount = adao.CountSessionsByDateRange(new DateTime(), new DateTime());
            resume.UserCount = adao.CountTotalUsers();
            resume.UserSessionRate = adao.CountUserSessionRateByDateRange(new DateTime(), new DateTime());
            return Json(OperationResult.Success("", resume));
        }
    }
}