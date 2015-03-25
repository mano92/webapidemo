using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class TradingStatistics
    {
        public string Symbol { get; set; }
        public string BidVolume { get; set; }
        public string BidPrice { get; set; }
        public string OfferPrice { get; set; }
        public string OfferVolume { get; set; }
        public string LastRate { get; set; }
        public string Change { get; set; }
        public bool ChangeFlag { get; set; }
        public string TotalVolume { get; set; }

    }
}