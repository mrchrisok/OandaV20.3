namespace OkonkwoOandaV20.TradeLibrary.Position
{
   /// <summary>
   /// The specification of a Positon within an Account
   /// http://developer.oanda.com/rest-live-v20/position-df/#Position
   /// </summary>
   public class Position
   {
      /// <summary>
      /// The Position’s Instrument.
      /// </summary>
      public string instrument { get; set; }

      /// <summary>
      /// Profit/loss realized by the Position over the lifetime of the Account.
      /// </summary>
      public decimal pl { get; set; }

      /// <summary>
      /// The unrealized profit/loss of all open Trades that contribute to this Position.
      /// </summary>
      public decimal unrealizedPL { get; set; }

      /// <summary>
      /// Margin currently used by the Position.
      /// </summary> 
      public decimal marginUsed { get; set; }

      /// <summary>
      /// Profit/loss realized by the Position since the Account’s resettablePL was last reset by the client.
      /// </summary>
      public decimal resettablePL { get; set; }

      /// <summary>
      /// The total amount of financing paid/collected for this instrument over the
      /// lifetime of the Account.
      /// </summary> 
      public decimal financing { get; set; }

      /// <summary>
      /// The total amount of commission paid for this instrument over the lifetime
      /// of the Account.
      /// </summary>
      public decimal commission { get; set; }

      /// <summary>
      /// The total amount of fees charged over the lifetime of the Account for the execution of guaranteed 
      /// Stop Loss Orders for this instrument.
      /// </summary>
      public decimal guaranteedExecutionFees { get; set; }

      /// <summary>
      /// The details of the long side of the Position.
      /// </summary>
      public PositionSide @long { get; set; }

      /// <summary>
      /// The details of the short side of the Position.
      /// </summary>
      public PositionSide @short { get; set; }
   }
}
