namespace OkonkwoOandaV20.TradeLibrary.Position
{
   /// <summary>
   /// The dynamic calculated state of a Position
   /// http://developer.oanda.com/rest-live-v20/position-df/#CalculatedPositionState
   /// </summary>
   public class CalculatedPositionState
   {
      /// <summary>
      /// The Position’s Instrument.
      /// </summary>
      public string instrument { get; set; }

      /// <summary>
      /// The Position’s net unrealized profit/loss
      /// </summary>
      public decimal netUnrealizedPL { get; set; }

      /// <summary>
      /// The unrealized profit/loss of the Position’s long open Trades
      /// </summary>
      public decimal longUnrealizedPL { get; set; }

      /// <summary>
      /// The unrealized profit/loss of the Position’s short open Trades
      /// </summary>
      public decimal shortUnrealizedPL { get; set; }

      /// <summary>
      /// Margin currently used by the Position.
      /// </summary>
      public decimal marginUsed { get; set; }
   }
}