using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class StockDataController : ApiController
    {
        private StockExchangeData data;

        public StockDataController()
        {
            data = new StockExchangeData();
        }

        [System.Web.Mvc.HttpGet]
        public IEnumerable<string[]> GetExchangeStatics()
        {
            var results = data.ExchangeStatistics();
            return results;
        }

        [System.Web.Mvc.HttpGet]
        public IEnumerable<MarketStatistics> GetMarketStatics()
        {
            var results = data.MarketStatistics();
            return results;
        }

        [System.Web.Mvc.HttpGet]
        public IEnumerable<IndexesStatistics> GetIndexesStatics()
        {
            var results = data.IndexesStatistics();
            return results;
        }

        [System.Web.Mvc.HttpGet]
        public IEnumerable<TradingStatistics> GetTradingStatics()
        {
            var results = data.TradingStatistics();
            return results;
        }
    }
}