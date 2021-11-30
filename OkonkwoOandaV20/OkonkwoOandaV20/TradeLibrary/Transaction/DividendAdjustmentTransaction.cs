namespace OkonkwoOandaV20.TradeLibrary.Transaction
{
    public class DividendAdjustmentTransaction : Transaction
    {
        public string instrument { get; set; }
        public decimal dividendAdjustment { get; set; }
        public decimal quoteDividendAdjustment { get; set; }
        public decimal accountBalance { get; set; }
    }
}
