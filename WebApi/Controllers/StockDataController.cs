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
            var results = data.ExchangeStatics();
            return results;
        }

        [System.Web.Mvc.HttpGet]
        public IEnumerable<MarketStatics> GetMarketStatics()
        {
            var results = data.MarketStatics();
            return results;
        }

        [System.Web.Mvc.HttpGet]
        public IEnumerable<IndexesStatistics> GetIndexesStatics()
        {
            var results = data.IndexesStatics();
            return results;
        }
    }
}