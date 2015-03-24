using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using HtmlAgilityPack;

namespace WebApi.Helpers
{
    public class KarachiStockExchangeData
    {
        //public DataTable marketData;

        public IEnumerable<string[]> ReturnMarketData(string htmlCode, string tableName)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlCode);
            var nodes = doc.DocumentNode.SelectNodes("//table[@class='" + tableName + "']/tr");
            //var table = new DataTable("MyTable");
            //var tableTitle = nodes[0].InnerText;
            //var headers = nodes.Skip(1).Select(tr => tr.Element("td")).Select(td => td.InnerText.Trim());
            //foreach (var header in headers)
            //{
            //    table.Columns.Add(header);
            //}
            var rows = nodes.Skip(2).Select(tr => tr
                .Elements("td")
                .Select(td => td.InnerText.Trim())
                .ToArray());
            var values = new List<string[]> {rows.FirstOrDefault()};
            return values;
        }
    }
}