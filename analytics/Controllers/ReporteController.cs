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

        public JsonResult JsonListNewUsersByDay()
        {
            ActivityDAO adao = new ActivityDAO();
            List<StringNumberTable> lista = adao.ListNewUsersByDateRange(new DateTime(), new DateTime());
            lista.ForEach(l => l.StringKey = l.DateKey.Day + "-" + l.DateKey.Month);
            //lista.Select(l => l.DateKey).ToString();
            return Json(lista);
        }
        public JsonResult JsonListSessionsByDay()
        {
            ActivityDAO adao = new ActivityDAO();
            List<StringNumberTable> lista = adao.ListSessionsByDateByDateRange(new DateTime(), new DateTime());
            //lista.Select(l => l.DateKey).ToString(s);
            return Json(lista);
        }
        public JsonResult JsonListNewUsersByDayOfWeek()
        {
            ActivityDAO adao = new ActivityDAO();
            List<StringNumberTable> lista = adao.ListUsersByDayOfWeekByDateRange(new DateTime(), new DateTime());
            return Json(lista);
        }
        public JsonResult GetResumenActivity()
        {
            /*            ActivityReportResume resume = new ActivityReportResume();
                        ActivityDAO adao = new ActivityDAO();
                        resume.ActiveUsersCount=adao.CountActiveUsersByDateRange(new DateTime(), new DateTime());
                        resume.NewUsersCount = adao.CountNewUsersByDateRange(new DateTime(), new DateTime());
                        resume.ReturningUsersCount = adao.CountReturningUsersByDateRange(new DateTime(), new DateTime());
                        resume.SessionCount = adao.CountSessionsByDateRange(new DateTime(), new DateTime());
                        resume.UserCount = adao.CountTotalUsers();
                        resume.UserSessionRate = adao.CountUserSessionRateByDateRange(new DateTime(), new DateTime());
                        return Json(OperationResult.Success("", resume));*/
            return Json(OperationResult.Failure("sss"));
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