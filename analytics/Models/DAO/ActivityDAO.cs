using analytics.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace analytics.Models.DAO
{
    public class ActivityDAO : DAO<Tiempo>
    {
        public int CountTotalUsers()
        {
            string query = "select count(1) from dbo.Users";
            return this.GetValue<int>(query);
        }
        public int CountActiveUsersByDateRange(DateTime From, DateTime to)
        {
            string query = "select count(distinct userId) from sessions  where Date between '2016-05-03' and '2016-05-09'";
            return this.GetValue<int>(query);
        }

        public List<StringNumberTable> ListNewUsersByDateRange(DateTime From, DateTime to)
        {
            string query = "select ActivationDate as DateKey, count(1) as Number  from dbo.Users "
                + " where ActivationDate "
                + "between '2016-05-02' and '2016-05-08'"
                + "group by ActivationDate;";
            return this.ListFromSQL<StringNumberTable>(query);
        }
        public int CountNewUsersByDateRange(DateTime From, DateTime to)
        {
            string query = "select count(1) from dbo.Users where ActivationDate between '2016-05-03' and '2016-05-09'";
            return this.GetValue<int>(query);
        }

        public int CountReturningUsersByDateRange(DateTime From, DateTime To)
        {
            string query = "select count(1) from(" +
                "select userId from sessions  where Date between '2016-05-03' and '2016-05-09' group by userid having count(1) > 1" +
                ") as x";
            return this.GetValue<int>(query);
        }
        public List<StringNumberTable> ListSessionsByDateByDateRange(DateTime From, DateTime To)
        {
            string query = "select date as DateKey, count(distinct userId) as Number from sessions where Date between '2016-05-02' and '2016-05-08' group by date";
            return this.ListFromSQL<StringNumberTable>(query);
        }
        public List<StringNumberTable> ListSessionsByMonthByDateRange(DateTime From, DateTime To)
        {
            string query = "select month(date) as DateKey, count(distinct userId) as Number from sessions where Date between '2016-05-02' and '2016-05-08' group by date";
            return this.ListFromSQL<StringNumberTable>(query);
        }

        public int CountSessionsByDateRange(DateTime From, DateTime To)
        {
            string query = "select count(1) from Sessions where Date between '2016-05-03' and '2016-05-09'";
            return this.GetValue<int>(query);
        }
        public int CountUserSessionRateByDateRange(DateTime From, DateTime To)
        {
            string query = "select avg(nsess) "
                + "from(select count(1) as nsess from sessions where Date between '2016-05-03' and '2016-05-09' group by UserId) as x ";
            return this.GetValue<int>(query);
        }
        public List<StringNumberTable> ListUsersByDayOfWeekByDateRange(DateTime From, DateTime To)
        {
            string query = "exec spu_NuevosUsuariosPromedioDia '2016-04-28', '2016-05-12'";
            return this.ListFromSQL<StringNumberTable>(query);
        }

    }
}