namespace OkonkwoOandaV20.TradeLibrary.Pricing
{
   /// <summary>
   /// Representation of how many units of an Instrument are available to be 
   /// traded by an Order depending on its postionFill option.
   /// </summary>
   public class UnitsAvailable
   {
      /// <summary>
      /// The number of units that are available to be traded using an Order with a
      /// positionFill option of “DEFAULT”. For an Account with hedging enabled,
      /// this value will be the same as the “OPEN_ONLY” value. For an Account
      /// without hedging enabled, this value will be the same as the
      /// “REDUCE_FIRST” value.
      /// </summary>
      public UnitsAvailableDetails @default { get; set; }

      /// <summary>
      /// The number of units that may are available to be traded with an Order
      /// with a positionFill option of “REDUCE_FIRST”.
      /// </summary>
      public UnitsAvailableDetails reduceFirst { get; set; }

      /// <summary>
      /// The number of units that may are available to be traded with an Order
      /// with a positionFill option of “REDUCE_ONLY”.
      /// </summary>
      public UnitsAvailableDetails reduceOnly { get; set; }

      /// <summary>
      /// The number of units that may are available to be traded with an Order
      /// with a positionFill option of “OPEN_ONLY”.
      /// </summary>
      public UnitsAvailableDetails openOnly { get; set; }
   }
}