
namespace WebApi.Constants
{
    public class StockExchangeUrls
    {
        public const string BaseURl = @"http://dps.kse.com.pk/";
        public const string ExchangeStaticsURl = @"webpages/ts/tsstats.php?mode=1";
        public const string MarketStaticsURl = @"includes/ts/inc_tsmarketstats.php?r=";
        public const string IndexesStaticsURl = @"/includes/ts/inc_tsindicesstats.php?r=";
        public static string[] MarketStaticsURlFlags = {"REG","FUT","CSF","SIF","NDM","SQR","IPO","BNB","ODL","KMT"};
        public static string[] MarketIndexesURlFlags = { "KSE100Index", "AllSharesIndex", "KSE30Index", "KMI30Index", "BKTiIndex", "OGTiIndex"};
    }
}