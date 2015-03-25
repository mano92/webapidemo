using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class ExchangeStatics
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public string TotalTrade { get; set; }
        public string Advance { get; set; }
        public string Decline { get; set; }
        public string Unchanged { get; set; }
        public string Total	 { get; set; }
        public string ExchangeVolume { get; set; }
        public string ExchangeValue { get; set; }
    }
}