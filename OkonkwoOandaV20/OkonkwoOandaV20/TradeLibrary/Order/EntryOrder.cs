using OkonkwoOandaV20.TradeLibrary.Transaction;
using System;
using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.Order
{
   /// <summary>
   /// Base Order definition inherited by all Orders that  initiate a new Trade or Position
   /// </summary>
   public abstract class EntryOrder : Order
   {
      /// <summary>
      /// The Order’s Instrument.
      /// </summary>
      public string instrument { get; set; }

      /// <summary>
      /// The quantity requested to be filled by the Order. A posititive
      /// number of units results in a long Order, and a negative number of units
      /// results in a short Order.
      /// </summary>
      public decimal units { get; set; }

      /// <summary>
      /// The time-in-force requested for the Order.
      /// </summary>
      public string timeInForce { get; set; }

      /// <summary>
      /// Specification of how Positions in the Account are modified when the Order
      /// is filled.
      /// Valid values are specified in the OrderPostionFill class
      /// </summary>
      public string positionFill { get; set; }

      /// <summary>
      /// TakeProfitDetails specifies the details of a Take Profit Order to be
      /// created on behalf of a client. This may happen when an Order is filled
      /// that opens a Trade requiring a Take Profit, or when a Trade’s dependent
      /// Take Profit Order is modified directly through the Trade.
      /// </summary>
      public TakeProfitDetails takeProfitOnFill { get; set; }

      /// <summary>
      /// StopLossDetails specifies the details of a Stop Loss Order to be created
      /// on behalf of a client. This may happen when an Order is filled that opens
      /// a Trade requiring a Stop Loss, or when a Trade’s dependent Stop Loss
      /// Order is modified directly through the Trade.
      /// </summary>
      public StopLossDetails stopLossOnFill { get; set; }

      /// <summary>
      /// TrailingStopLossDetails specifies the details of a Trailing Stop Loss
      /// Order to be created on behalf of a client. This may happen when an Order
      /// is filled that opens a Trade requiring a Trailing Stop Loss, or when a
      /// Trade’s dependent Trailing Stop Loss Order is modified directly through
      /// the Trade.
      /// </summary>
      public TrailingStopLossDetails trailingStopLossOnFill { get; set; }

      /// <summary>
      /// Client Extensions to add to the Trade created when the Order is filled
      /// (if such a Trade is created). Do not set, modify, or delete
      /// tradeClientExtensions if your account is associated with MT4.
      /// </summary>
      public ClientExtensions tradeClientExtensions { get; set; }

      /// <summary>
      /// ID of the Transaction that filled this Order (only provided when the
      /// Order’s state is FILLED)
      /// </summary>
      public long? fillingTransactionID { get; set; }

      /// <summary>
      /// Date/time when the Order was filled (only provided when the Order’s state
      /// is FILLED)
      /// </summary>
      public string filledTime { get; set; }

      /// <summary>
      /// Trade ID of Trade opened when the Order was filled (only provided when
      /// the Order’s state is FILLED and a Trade was opened as a result of the
      /// fill)
      /// </summary>
      public long? tradeOpenedID { get; set; }

      /// <summary>
      /// Trade ID of Trade reduced when the Order was filled (only provided when
      /// the Order’s state is FILLED and a Trade was reduced as a result of the
      /// fill)
      /// </summary>
      public long? tradeReducedID { get; set; }

      /// <summary>
      /// Trade IDs of Trades closed when the Order was filled (only provided when
      /// the Order’s state is FILLED and one or more Trades were closed as a
      /// result of the fill)
      /// </summary>
      public List<long> tradeClosedIDs { get; set; }

      /// <summary>
      /// ID of the Transaction that cancelled the Order (only provided when the
      /// Order’s state is CANCELLED)
      /// </summary>
      public long? cancellingTransactionID { get; set; }

      /// <summary>
      /// Date/time when the Order was cancelled (only provided when the state of
      /// the Order is CANCELLED)
      /// </summary>
      public DateTime? cancelledTime { get; set; }
   }
}
