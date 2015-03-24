using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ValuesController : ApiController
    {
        private KarachiStockExchangeData data;

        public ValuesController()
        {
            data = new KarachiStockExchangeData();
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        public IEnumerable<string[]> GetExchangeStatics()
        {
            //var marketStatics = new ExchangeStatics();
            var htmlCode = string.Empty;
            using (var client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.UserAgent, "AvoidError");
                htmlCode = client.DownloadString("http://dps.kse.com.pk/webpages/ts/tsstats.php?mode=1");
            }
            var results = data.ReturnMarketData(htmlCode, "marketData");
            return results;
            //return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
