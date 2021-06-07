/// <summary>
/// http://developer.oanda.com/rest-live-v20/order-df/
/// </summary>
namespace OkonkwoOandaV20.TradeLibrary.Order
{
   /// <summary>
   /// Specification of how Positions in the Account are modified when the Order is filled
   /// </summary>
   public class OrderPositionFill
   {
      /// <summary>
      /// When the Order is filled, only allow Positions to be opened or extended.
      /// </summary>
      public const string OpenOnly = "OPEN_ONLY";

      /// <summary>
      /// When the Order is filled, always fully reduce an existing Position before opening a new Position.
      /// </summary>
      public const string ReduceFirst = "REDUCE_FIRST";

      /// <summary>
      /// When the Order is filled, only reduce an existing Position.
      /// </summary>
      public const string ReduceOnly = "REDUCE_ONLY";

      /// <summary>
      /// When the Order is filled, use REDUCE_FIRST behaviour for non-client hedging Accounts, and OPEN_ONLY 
      /// behaviour for client hedging Accounts.
      /// </summary>
      public const string Default = "DEFAULT";
   }

   /// <summary>
   /// The current state of the Order
   /// </summary>
   public class OrderState
   {
      /// <summary>
      /// The Order is currently pending execution
      /// </summary>
      public const string Pending = "PENDING";

      /// <summary>
      /// The Order has been filled
      /// </summary>
      public const string Filled = "FILLED";

      /// <summary>
      /// The Order has been triggered
      /// </summary>
      public const string Triggered = "TRIGGERED";

      /// <summary>
      /// The Order has been cancelled
      /// </summary>
      public const string Cancelled = "CANCELLED";
   }

   /// <summary>
   /// The state to filter the requested Orders by
   /// </summary>
   public class OrderStateFilter : OrderState
   {
      /// <summary>
      /// The Orders that are in any of the possible OrderState values
      /// </summary>
      public const string All = "ALL";
   }

   /// <summary>
   /// Specification of which price component should be used when determining if an Order should be 
   /// triggered and filled. This allows Orders to be triggered based on the bid, ask, mid, default (ask for buy, 
   /// bid for sell) or inverse (ask for sell, bid for buy) price depending on the desired behaviour. Orders are 
   /// always filled using their default price component. This feature is only provided through the REST API. 
   /// Clients who choose to specify a non-default trigger condition will not see it reflected in any of 
   /// OANDA’s proprietary or partner trading platforms, their transaction history or their account  statements. 
   /// OANDA platforms always assume that an Order’s trigger condition is set to the default value when indicating 
   /// the distance from an Order’s trigger price, and will always provide the default trigger condition when creating 
   /// or modifying an Order. A special restriction applies when creating a guaranteed Stop Loss Order. In this case 
   /// the TriggerCondition value must either be “DEFAULT”, or the “natural” trigger side “DEFAULT” results in. So for 
   /// a Stop Loss Order for a long trade valid values are “DEFAULT” and “BID”, and for short trades “DEFAULT” and “ASK” 
   /// are valid.
   /// </summary>
   public class OrderTriggerCondition
   {
      /// <summary>
      /// Trigger an Order the “natural” way: compare its price to the ask for long Orders and bid for short Orders.
      /// </summary>
      public const string Default = "DEFAULT";

      /// <summary>
      /// Trigger an Order the opposite of the “natural” way: compare its price the bid for long Orders and ask for short Orders.
      /// </summary>
      public const string Inverse = "INVERSE";

      /// <summary>
      /// Trigger an Order by comparing its price to the bid regardless of whether it is long or short.
      /// </summary>
      public const string Bid = "BID";

      /// <summary>
      /// Trigger an Order by comparing its price to the ask regardless of whether it is long or short.
      /// </summary>
      public const string Ask = "ASK";

      /// <summary>
      /// Trigger an Order by comparing its price to the midpoint regardless of whether it is long or short.
      /// </summary>
      public const string Mid = "MID";
   }

   /// <summary>
   /// The type of the Order
   /// </summary>
   public class OrderType : CancellableOrderType
   {
      /// <summary>
      /// A Market Order
      /// </summary>
      public const string Market = "MARKET";

      /// <summary>
      /// A Fixed Price Order
      /// </summary>
      public const string FixedPrice = "FIXED_PRICE";
   }

   /// <summary>
   /// Order types that can be cancelled if still pending
   /// </summary>
   public class CancellableOrderType
   {
      /// <summary>
      /// A Limit Order
      /// </summary>
      public const string Limit = "LIMIT";

      /// <summary>
      /// A Stop Order
      /// </summary>
      public const string Stop = "STOP";

      /// <summary>
      /// A Market-if-touched Order
      /// </summary>
      public const string MarketIfTouched = "MARKET_IF_TOUCHED";

      /// <summary>
      /// 	A Take Profit Order
      /// </summary>
      public const string TakeProfit = "TAKE_PROFIT";

      /// <summary>
      /// A Stop Loss Order
      /// </summary>
      public const string StopLoss = "STOP_LOSS";

      /// <summary>
      /// A Trailing Stop Loss Order
      /// </summary>
      public const string TrailingStopLoss = "TRAILING_STOP_LOSS";
   }

   /// <summary>
   /// The time-in-force of an Order. TimeInForce describes how long an Order should remain pending before being 
   /// automatically cancelled by the execution system.
   /// </summary>
   public class TimeInForce
   {
      /// <summary>
      /// The Order is “Good unTil Cancelled”
      /// </summary>
      public const string GoodUntilCancelled = "GTC";

      /// <summary>
      /// The Order is “Good unTil Date” and will be cancelled at the provided time
      /// </summary>
      public const string GoodUntilDate = "GTD";

      /// <summary>
      /// The Order is “Good For Day” and will be cancelled at 5pm New York time
      /// </summary>
      public const string GoodForDay = "GFD";

      /// <summary>
      /// The Order must be immediately “Filled Or Killed”
      /// </summary>
      public const string FillOrKill = "FOK";

      /// <summary>
      /// The Order must be “Immediatedly paritally filled Or Cancelled”
      /// </summary>
      public const string ImmediatelyOrCancelled = "IOC";
   }
}
