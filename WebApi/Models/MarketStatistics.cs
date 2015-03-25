using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class MarketStatistics
    {
        public string MarketName { get; set; }
        public string State { get; set; }
        public string Trades { get; set; }
        public string Volume { get; set; }
        public string Value { get; set; }
    }
}