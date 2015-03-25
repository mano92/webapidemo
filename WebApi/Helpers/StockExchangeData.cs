using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using HtmlAgilityPack;
using WebApi.Constants;
using WebApi.Models;

namespace WebApi.Helpers
{
    public class StockExchangeData
    {
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

        public IEnumerable<string[]> ExchangeStatistics()
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

        private MarketStatistics createMarketStatisticsObject(string[] data)
        {
            return new MarketStatistics
            {
                MarketName = data[0],
                State = data[1],
                Trades = data[2],
                Volume = data[3],
                Value = data[4]
            };
        }

        private IndexesStatistics createIndexesStatisticsObject(string[] data)
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

        private TradingStatistics createTradingStatisticsObject(string[] data, bool flag)
        {
            return new TradingStatistics
            {
                Symbol = data[0],
                BidVolume  = data[1],
                BidPrice  = data[2],
                OfferPrice  = data[3],
                OfferVolume  = data[4],
                LastRate  = data[5],
                Change  = data[6],
                TotalVolume = data[7],
                ChangeFlag  = flag
            };
        }

        public IEnumerable<MarketStatistics> MarketStatistics()
        {
            var marketLists = new List<MarketStatistics>();
            var doc = new HtmlDocument();
            foreach (var marketStaticsURlFlag in StockExchangeUrls.MarketStaticsURlFlags)
            {
                doc.LoadHtml(GetHtmlFromUrl(StockExchangeUrls.BaseURl + StockExchangeUrls.MarketStaticsURl + marketStaticsURlFlag));
                var nodes = doc.DocumentNode.SelectNodes("//table[@class='marketData']/tr");
                var row = nodes.Skip(3).Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToArray());
                var item = createMarketStatisticsObject(row.FirstOrDefault());
                marketLists.Add(item);
            }
            return marketLists;
        }

        public IEnumerable<IndexesStatistics> IndexesStatistics()
        {
            var indexesLists = new List<IndexesStatistics>();
            var doc = new HtmlDocument();
            var flag = string.Empty;
            foreach (var indexesStaticsURlFlag in StockExchangeUrls.MarketIndexesURlFlags)
            {
                doc.LoadHtml(GetHtmlFromUrl(StockExchangeUrls.BaseURl + StockExchangeUrls.IndexesStaticsURl + indexesStaticsURlFlag));
                var nodes = doc.DocumentNode.SelectNodes("//table[@class='marketData']/tr");
                var row = nodes.Skip(3).Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToArray());
                ////////////////////////////////////////////////////////////////////////////////////////////////
                var imagepath = doc.DocumentNode.SelectNodes("//table[@class='marketData']/tr/td/img[@src]");
                var firstOrDefault = imagepath.FirstOrDefault();
                if (firstOrDefault != null)
                    flag = firstOrDefault.OuterHtml.Split('/').Last().Split('.').FirstOrDefault();
                var item = createIndexesStatisticsObject(row.FirstOrDefault());
                if(!string.IsNullOrEmpty(flag))
                    item.ChangeFlag = flag;
                ////////////////////////////////////////////////////////////////////////////////////////////////
                indexesLists.Add(item);
            }
            return indexesLists;
        }

        public IEnumerable<TradingStatistics> TradingStatistics()
        {
            var tradingLists = new List<TradingStatistics>();
            var doc = new HtmlDocument();
                doc.LoadHtml(GetHtmlFromUrl(StockExchangeUrls.BaseURl + StockExchangeUrls.TradingStatisticsURI));
                var nodes = doc.DocumentNode.SelectNodes("//table[@class='marketData']/tr");
                var rows = nodes.Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToArray());
                var classes = nodes.Select(tr => tr.Elements("td").Select(td => td.OuterHtml.Trim()).ToArray());
                foreach (var stringse in classes.Zip(rows, Tuple.Create))
                {
                    var flag = stringse.Item1.FirstOrDefault().Contains("marketData_advance");
                    var row = stringse.Item2;
                    var item = createTradingStatisticsObject(row,flag);
                    tradingLists.Add(item);
                }
            return tradingLists;
        }


    }
}