using OkonkwoOandaV20.TradeLibrary.Transaction;

namespace OkonkwoOandaV20.TradeLibrary.Order
{
   public class MarketOrder : EntryOrder
   {
      /// <summary>
      /// The worst price that the client is willing to have the Market Order
      /// filled at.
      /// </summary>
      public decimal? priceBound { get; set; }

      /// <summary>
      /// Details of the Trade requested to be closed, only provided when the
      /// Market Order is being used to explicitly close a Trade.
      /// </summary>
      public MarketOrderTradeClose tradeClose { get; set; }

      /// <summary>
      /// Details of the long Position requested to be closed out, only provided
      /// when a Market Order is being used to explicitly closeout a long Position.
      /// </summary>
      public MarketOrderPositionCloseout longPositionCloseout { get; set; }

      /// <summary>
      /// Details of the short Position requested to be closed out, only provided
      /// when a Market Order is being used to explicitly closeout a short
      /// Position.
      /// </summary>
      public MarketOrderPositionCloseout shortPositionCloseout { get; set; }

      /// <summary>
      /// Details of the Margin Closeout that this Market Order was created for
      /// </summary>
      public MarketOrderMarginCloseout marginCloseout { get; set; }

      /// <summary>
      /// Details of the delayed Trade close that this Market Order was created for
      /// </summary>
      public MarketOrderDelayedTradeClose delayedTradeClose { get; set; }
   }
}
