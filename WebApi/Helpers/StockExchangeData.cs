using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using HtmlAgilityPack;
using WebApi.Constants;
using WebApi.Models;

namespace WebApi.Helpers
{
    public class StockExchangeData
    {
        //public DataTable marketData;
        private string GetHtmlFromUrl(string url)
        {
            var htmlCode = string.Empty;
            using (var client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.UserAgent, "AvoidError");
                htmlCode = client.DownloadString(url);
            }
            return htmlCode;
        }

        public IEnumerable<string[]> ExchangeStatics()
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(GetHtmlFromUrl(StockExchangeUrls.BaseURl + StockExchangeUrls.ExchangeStaticsURl));
            var nodes = doc.DocumentNode.SelectNodes("//table[@class='marketData']/tr");
            var rows = nodes.Skip(2).Select(tr => tr
                .Elements("td")
                .Select(td => td.InnerText.Trim())
                .ToArray());
            var rowList = nodes.Skip(2).Select(tr => tr
                .Elements("td")
                .Select(td => td.InnerText.Trim())
                .ToList());
            var values = new List<string[]> {rows.FirstOrDefault()};
            return values;
        }

        private MarketStatics createMarketStaticsObject(string[] data)
        {
            return new MarketStatics
            {
                MarketName = data[0],
                State = data[1],
                Trades = data[2],
                Volume = data[3],
                Value = data[4]
            };
        }

        private IndexesStatistics createIndexesStaticsObject(string[] data)
        {
            return new IndexesStatistics
            {
                IndexName = data[0],
                CurrentIndex = data[1],
                High = data[2],
                Low = data[3],
                Change = data[4],
                Volume = data[5],
                Value = data[6]
            };  
        }

        public IEnumerable<MarketStatics> MarketStatics()
        {
            var marketLists = new List<MarketStatics>();
            var doc = new HtmlDocument();
            foreach (var marketStaticsURlFlag in StockExchangeUrls.MarketStaticsURlFlags)
            {
                doc.LoadHtml(GetHtmlFromUrl(StockExchangeUrls.BaseURl + StockExchangeUrls.MarketStaticsURl + marketStaticsURlFlag));
                var nodes = doc.DocumentNode.SelectNodes("//table[@class='marketData']/tr");
                var row = nodes.Skip(3).Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToArray());
                var item = createMarketStaticsObject(row.FirstOrDefault());
                marketLists.Add(item);
            }
            return marketLists;
        }

        public IEnumerable<IndexesStatistics> IndexesStatics()
        {
            var indexesLists = new List<IndexesStatistics>();
            var doc = new HtmlDocument();
            foreach (var indexesStaticsURlFlag in StockExchangeUrls.MarketIndexesURlFlags)
            {
                doc.LoadHtml(GetHtmlFromUrl(StockExchangeUrls.BaseURl + StockExchangeUrls.IndexesStaticsURl + indexesStaticsURlFlag));
                var nodes = doc.DocumentNode.SelectNodes("//table[@class='marketData']/tr");
                var row = nodes.Skip(3).Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToArray());
                var item = createIndexesStaticsObject(row.FirstOrDefault());
                indexesLists.Add(item);
            }
            return indexesLists;
        }
    }
}