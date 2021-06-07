namespace OkonkwoOandaV20.TradeLibrary.Transaction
{
   public abstract class EntryOrderTransaction : Transaction
   {
      /// <summary>
      /// The Entry Order's Instrument.
      /// </summary>
      public string instrument { get; set; }

      /// <summary>
      /// The quantity requested to be filled by the Entry Order. A posititive
      /// number of units results in a long Order, and a negative number of units
      /// results in a short Order.
      /// </summary>
      public decimal units { get; set; }

      /// <summary>
      /// The time-in-force requested for the Entry Order.
      /// Valide values are specified in the TimeInForce class.
      /// </summary>
      public string timeInForce { get; set; }

      /// <summary>
      /// Specification of how Positions in the Account are modified when the Order
      /// is filled.
      /// </summary>
      public string positionFill { get; set; }

      /// <summary>
      /// The reason that the Entry Order was initiated
      /// </summary>
      public string reason { get; set; }

      /// <summary>
      /// Client Extensions to add to the Order (only provided if the Order is 
      /// being created with client extensions).
      /// </summary>
      public ClientExtensions clientExtensions { get; set; }

      /// <summary>
      /// The specification of the Take Profit Order that should be created for a
      /// Trade opened when the Order is filled (if such a Trade is created).
      /// </summary>
      public TakeProfitDetails takeProfitOnFill { get; set; }

      /// <summary>
      /// The specification of the Stop Loss Order that should be created for a 
      /// Trade opened when the Order is filled (if such a Trade is created).
      /// </summary>
      public StopLossDetails stopLossOnFill { get; set; }

      /// <summary>
      /// The specification of the Trailing Stop Loss Order that should be created
      /// for a Trade that is opened when the Order is filled (if such a Trade is
      /// created).
      /// </summary>
      public TrailingStopLossDetails trailingStopLossOnFill { get; set; }

      /// <summary>
      /// Client Extensions to add to the Trade created when the Order is filled
      /// (if such a Trade is created).  Do not set, modify, delete
      /// tradeClientExtensions if your account is associated with MT4.
      /// </summary>
      public ClientExtensions tradeClientExtensions { get; set; }
   }
}
