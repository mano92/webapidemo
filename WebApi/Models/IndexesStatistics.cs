using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class IndexesStatistics
    {
        public string IndexName { get; set; }
        public string CurrentIndex { get; set; }
        public string High { get; set; }
        public string Low { get; set; }
        public string Change { get; set; }
        public string Volume { get; set; }
        public string Value { get; set; }
    }
}