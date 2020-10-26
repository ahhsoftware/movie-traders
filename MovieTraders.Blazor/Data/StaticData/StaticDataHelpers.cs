namespace MovieTraders.Data
{
    public partial class StaticData
    {
        public static string FormatName(byte formatId)
        {
            return FormatList.Find(f => f.FormatId == formatId).Name;
        }

        public static string TradeStatusName(byte tradeStatusId)
        {
            return TradeStatusList.Find(t => t.TradeStatusId == tradeStatusId).Name;
        }

        public static string GenreName(byte genreId)
        {
            return GenreList.Find(g => g.GenreId == genreId).Name;
        }
    }
}
