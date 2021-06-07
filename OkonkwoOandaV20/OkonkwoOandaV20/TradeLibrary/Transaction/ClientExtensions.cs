namespace OkonkwoOandaV20.TradeLibrary.Transaction
{
   /// <summary>
   /// A ClientExtensions object allows a client to attach a clientID, tag and comment to Orders and Trades in their
   /// Account. Do not set, modify, or delete this field if your account is associated with MT4.
   /// </summary>
   public class ClientExtensions
   {
      /// <summary>
      /// The Client ID of the Order/Trade
      /// </summary>
      public string id { get; set; }

      /// <summary>
      /// A tag associated with the Order/Trade
      /// </summary>
      public string tag { get; set; }

      /// <summary>
      /// A comment associated with the Order/Trade
      /// </summary>
      public string comment { get; set; }
   }
}
