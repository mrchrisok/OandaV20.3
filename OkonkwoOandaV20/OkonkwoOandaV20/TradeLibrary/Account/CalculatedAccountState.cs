namespace OkonkwoOandaV20.TradeLibrary.Account
{
   public class CalculatedAccountState
   {
      public decimal unrealizedPL { get; set; }
      public decimal NAV { get; set; }
      public decimal marginUsed { get; set; }
      public decimal marginAvailable { get; set; }
      public decimal positionValue { get; set; }
      public decimal marginCloseoutUnrealizedPL { get; set; }
      public decimal marginCloseoutNAV { get; set; }
      public decimal marginCloseoutMarginUsed { get; set; }
      public decimal marginCloseoutPercent { get; set; }
      public decimal marginCloseoutPositionValue { get; set; }
      public decimal withdrawalLimit { get; set; }
      public decimal marginCallMarginUsed { get; set; }
      public decimal marginCallPercent { get; set; }
   }
}
