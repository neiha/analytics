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
        public List<StringNumberTable> ListSessionsByDateByDateRange()
        {
            string query = "select date as Key, count(distinct userId) as Number from sessions where Date between '2016-05-02' and '2016-05-08' group by date";
            return this.ListFromSQL<StringNumberTable>(query);
        }  
    }
}