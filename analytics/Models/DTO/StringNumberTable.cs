using System;

namespace analytics.Models.DTO
{
    public class StringNumberTable
    {
        public virtual string StringKey { get; set; }
        public virtual DateTime DateKey { get; set; }
        public virtual int Number { get; set; }
    }
}