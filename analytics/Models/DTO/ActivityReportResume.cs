using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace analytics.Models.DTO
{
    public class ActivityReportResume
    {
        public virtual int UserCount { get; set; }
        public virtual int ActiveUsersCount { get; set; }
        public virtual int NewUsersCount { get; set; }
        public virtual int ReturningUsersCount { get; set; }
        public virtual int SessionCount { get; set; }
        public virtual int UserSessionRate { get; set; }
    }
}