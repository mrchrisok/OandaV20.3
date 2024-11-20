using Newtonsoft.Json;
using OkonkwoOandaV20.Framework;
using OkonkwoOandaV20.Framework.JsonConverters;
using System;
using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.Transaction
{
   /// <summary>
   /// The reason that an Account is being funded.
   /// </summary>
   public class FundingReason
   {
      /// <summary>
      /// The client has initiated a funds transfer
      /// </summary>
      public const string ClientFunding = "CLIENT_FUNDING";

      /// <summary>
      /// Funds are being transfered between two Accounts.
      /// </summary>
      public const string AccountTransfer = "ACCOUNT_TRANSFER";

      /// <summary>
      /// Funds are being transfered as part of a Division migration
      /// </summary>
      public const string DivisionMigration = "DIVISION_MIGRATION";

      /// <summary>
      /// Funds are being transfered as part of a Site migration
      /// </summary>
      public const string SiteMigration = "SITE_MIGRATION";

      /// <summary>
      /// Funds are being transfered as part of an Account adjustment
      /// </summary>
      public const string Adjustment = "ADJUSTMENT";
   }

   /// <summary>
   /// A LiquidityRegenerationSchedule indicates how liquidity that is used when filling an 
   /// Order for an instrument is regenerated following the fill.  A liquidity regeneration schedule 
   /// will be in effect until the timestamp of its final step, but may be replaced by a schedule 
   /// created for an Order of the same instrument that is filled while it is still in effect.
   /// </summary>
   public class LiquidityRegenerationSchedule
   {
      /// <summary>
      /// The steps in the Liquidity Regeneration Schedule
      /// </summary>
      public List<LiquidityRegenerationScheduleStep> steps { get; set; }
   }

   /// <summary>
   /// A liquidity regeneration schedule Step indicates the amount of bid and ask 
   /// liquidity that is used by the Account at a certain time. These amounts will only 
   /// change at the timestamp of the following step.
   /// </summary>
   public class LiquidityRegenerationScheduleStep
   {
      /// <summary>
      /// The timestamp of the schedule step.
      /// </summary>
      public DateTime timestamp { get; set; }

      /// <summary>
      /// The amount of bid liquidity used at this step in the schedule.
      /// </summary>
      public decimal bidLiquidityUsed { get; set; }

      /// <summary>
      /// The amount of ask liquidity used at this step in the schedule.
      /// </summary>
      public decimal askLiquidityUsed { get; set; }
   }

   /// <summary>
   /// Details for the Market Order extensions specific to a Market Order placed with the 
   /// intent of fully closing a specific open trade that should have already been closed but 
   /// wasn’t due to halted market conditions
   /// </summary>
   public class MarketOrderDelayedTradeClose
   {
      /// <summary>
      /// The ID of the Trade being closed
      /// </summary>
      public long tradeID { get; set; }

      /// <summary>
      /// The Client ID of the Trade being closed
      /// </summary>
      public string clientTradeID { get; set; }

      /// <summary>
      /// The Transaction ID of the DelayedTradeClosure transaction to which this 
      /// Delayed Trade Close belongs to
      /// </summary>
      public long sourceTransactionID { get; set; }
   }

   /// <summary>
   /// A MarketOrderTradeClose specifies the extensions to a Market Order that has been created
   /// specifically to close a Trade.
   /// </summary>
   public class MarketOrderTradeClose
   {
      /// <summary>
      /// The ID of the Trade requested to be closed
      /// </summary>
      public long tradeID { get; set; }

      /// <summary>
      /// The client ID of the Trade requested to be closed
      /// </summary>
      public string clientTradeID { get; set; }

      /// <summary>
      /// Indication of how much of the Trade to close. Either “ALL”, or a
      /// DecimalNumber reflection a partial close of the Trade.
      /// </summary>
      public string units { get; set; }
   }

   /// <summary>
   /// A MarketOrderPositionCloseout specifies the extensions to a Market Order when it has
   /// been created specifically to closeout a specific Position.
   /// </summary>
   public class MarketOrderPositionCloseout
   {
      /// <summary>
      /// The instrument of the Position being closed out.
      /// </summary>
      public string instrument { get; set; }

      /// <summary>
      /// Indication of how much of the Position to close. Either “ALL”, or a
      /// DecimalNumber reflection a partial close of the Trade. The DecimalNumber
      /// must always be positive, and represent a number that doesn’t exceed the
      /// absolute size of the Position.
      /// </summary>
      public string units { get; set; }
   }

   /// <summary>
   /// Details for the Market Order extensions specific to a Market Order placed that is part of a 
   /// Market Order Margin Closeout in a client's account
   /// </summary>
   public class MarketOrderMarginCloseout
   {
      /// <summary>
      /// The reason the Market Order was created to perform a margin closeout
      /// </summary>
      public string reason { get; set; }
   }

   /// <summary>
   /// The reason that the Market Order was created to perform a margin closeout
   /// </summary>
   public class MarketOrderMarginCloseoutReason
   {
      /// <summary>
      /// Trade closures resulted from violating OANDA’s margin policy
      /// </summary>
      public const string MarginCheckViolation = "MARGIN_CHECK_VIOLATION";

      /// <summary>
      /// Trade closures came from a margin closeout event resulting from regulatory conditions placed on the 
      /// Account’s margin call
      /// </summary>
      public const string RegulatoryMarginCallViolation = "REGULATORY_MARGIN_CALL_VIOLATION";
   }

   /// <summary>
   /// OpenTradeFinancing is used to pay/collect daily financing charge for an open Trade within an Account
   /// </summary>
   public class OpenTradeFinancing
   {
      /// <summary>
      /// The ID of the Trade that financing is being paid/collected for.
      /// </summary>
      public long tradeID { get; set; }

      /// <summary>
      /// The amount of financing paid/collected for the Trade.
      /// </summary>
      public decimal financing { get; set; }
   }

   /// <summary>
   /// The reason that an Order was filled
   /// </summary>
   public class OrderFillReason
   {
      public const string LimitOrder = "LIMIT_ORDER";
      public const string StopOrder = "STOP_ORDER";
      public const string MarketIfTouchedOrder = "MARKET_IF_TOUCHED_ORDER";
      public const string TakeProfitOrder = "TAKE_PROFIT_ORDER";
      public const string StopLossOrder = "STOP_LOSS_ORDER";
      public const string TrailingStopLossOrder = "TRALING_STOP_LOSS_ORDER";
      public const string MarketOrder = "MARKET_ORDER";
      public const string MarketOrderTradeClose = "MARKET_ORDER_TRADE_CLOSE";
      public const string MarketOrderPositionCloseout = "MARKET_ORDER_POSITION_CLOSEOUT";
      public const string MarketOrderMarginCloseout = "MARKET_ORDER_MARGIN_CLOSEOUT";
      public const string MarketOrderDelayedClose = "MARKET_ORDER_DELAYED_TRADE_CLOSE";
   }

   /// <summary>
   /// For additional details, visit http://developer.oanda.com/rest-live-v20/transaction-df////OrderCancelReason
   /// </summary>
   public class OrderCancelReason
   {
      public const string InternalServerError = "INTERNAL_SERVER_ERROR";
      public const string AccountLocked = "ACCOUNT_LOCKED";
      public const string AccountNewPositionsLocked = "ACCOUNT_NEW_POSITIONS_LOCKED";
      public const string AccountNewOrderCreationLocked = "ACCOUNT_ORDER_CREATION_LOCKED";
      public const string AccountOrderFillLocked = "ACCOUNT_ORDER_FILL_LOCKED";
      public const string ClientRequest = "CLIENT_REQUEST";
      public const string Migration = "MIGRATION";
      public const string MarketHalted = "MARKET_HALTED";
      public const string LinkedTradeClosed = "LINKED_TRADE_CLOSED";
      public const string TimeInForceExpired = "TIME_IN_FORCE_EXPIRED";
      public const string InsufficientMargin = "INSUFFICIENT_MARGIN";
      public const string FifoViolation = "FIFO_VIOLATION";
      public const string BoundsViolation = "BOUNDS_VIOLATION";
      public const string ClientRequestReplaced = "CLIENT_REQUEST_REPLACED";
      public const string InsufficientLiquidity = "INSUFFICIENT_LIQUIDITY";
      public const string TakeProfitOnFillGtdTimestampInPast = "TAKE_PROFIT_ON_FILL_GTD_TIMESTAMP_IN_PAST";
      public const string TakeProfitOnFillLoss = "TAKE_PROFIT_ON_FILL_LOSS";
      public const string LosingTakeProfit = "LOSING_TAKE_PROFIT";
      public const string StopLossOnFillGtdTimestampInPast = "STOP_LOSS_ON_FILL_GTD_TIMESTAMP_IN_PAST";
      public const string StopLossOnFillLoss = "STOP_LOSS_ON_FILL_LOSS";
      public const string StopLossOnFillPriceDistanceMaximumExceeded = "STOP_LOSS_ON_FILL_PRICE_DISTANCE_MAXIMUM_EXCEEDED";
      public const string StopLossOnFillRequired = "STOP_LOSS_ON_FILL_REQUIRED";
      public const string StopLossOnFillGuaranteedRequired = "STOP_LOSS_ON_FILL_GUARANTEED_REQUIRED";
      public const string StopLossOnFillGuaranteedNotAllowed = "STOP_LOSS_ON_FILL_GUARANTEED_NOT_ALLOWED";
      public const string StopLossOnFillGuaranteedMinimumDistanceNotMet = "STOP_LOSS_ON_FILL_GUARANTEED_MINIMUM_DISTANCE_NOT_MET";
      public const string StopLossOnFillGuaranteedLevelRestrictionExceeded = "STOP_LOSS_ON_FILL_GUARANTEED_LEVEL_RESTRICTION_EXCEEDED";
      public const string StopLossOnFillGuaranteedHedgingNotAllowed = "STOP_LOSS_ON_FILL_GUARANTEED_HEDGING_NOT_ALLOWED";
      public const string StopLossOnFillTimeInForceInvalid = "STOP_LOSS_ON_FILL_TIME_IN_FORCE_INVALID";
      public const string StopLossOnFillTriggerConditionInvalid = "STOP_LOSS_ON_FILL_TRIGGER_CONDITION_INVALID";
      public const string TakeProfitOnFillPriceDistanceMaximumExceeded = "TAKE_PROFIT_ON_FILL_PRICE_DISTANCE_MAXIMUM_EXCEEDED";
      public const string TrailingStopLossOnFillGtdTimestampInPast = "TRAILING_STOP_LOSS_ON_FILL_GTD_TIMESTAMP_IN_PAST";
      public const string ClientTradeIdAlreadyExists = "CLIENT_TRADE_ID_ALREADY_EXISTS";
      public const string PositionCloseoutFailed = "POSITION_CLOSEOUT_FAILED";
      public const string OpenTradesAllowedExceeded = "OPEN_TRADES_ALLOWED_EXCEEDED";
      public const string PendingOrdersAllowedExceeded = "PENDING_ORDERS_ALLOWED_EXCEEDED";
      public const string TakeProfitOnFillClientOrderIdAlreadyExists = "TAKE_PROFIT_ON_FILL_CLIENT_ORDER_ID_ALREADY_EXISTS";
      public const string StopLossOnFillClientOrderIdAlreadyExists = "STOP_LOSS_ON_FILL_CLIENT_ORDER_ID_ALREADY_EXISTS";
      public const string TrailingStopLossOnFillClientOrderIdAlreadyExists = "TRAILING_STOP_LOSS_ON_FILL_CLIENT_ORDER_ID_ALREADY_EXISTS";
      public const string PositionSizeExceeded = "POSITION_SIZE_EXCEEDED";
      public const string HedgingGsloViolation = "HEDGING_GSLO_VIOLATION";
      public const string AccountPositionValueLimitExceeded = "ACCOUNT_POSITION_VALUE_LIMIT_EXCEEDED";
   }

   /// <summary>
   /// OpenTradeFinancing is used to pay/collect daily financing charge for a Position within an Account
   /// </summary>
   public class PositionFinancing
   {
      /// <summary>
      /// The instrument of the Position that financing is being paid/collected
      /// </summary>
      public string instrument { get; set; }

      /// <summary>
      /// The amount of financing paid/collected for the Position.
      /// </summary>
      public decimal financing { get; set; }

      /// <summary>
      /// The financing paid/collected for each open Trade within the Position.
      /// </summary>
      public List<OpenTradeFinancing> openTradeFinancings { get; set; }
   }

   /// <summary>
   /// A TradeOpen object represents a Trade for an instrument that was opened in an Account. 
   /// It is found embedded in Transactions that affect the position of an instrument in the account, 
   /// specifically the OrderFill Transaction.
   /// </summary>
   public class TradeOpen : TradeAction
   {
      /// <summary>
      /// The client extensions for the newly opened Trade
      /// </summary>
      public ClientExtensions clientExtensions { get; set; }

      /// <summary>
      /// The margin required at the time the Trade was created. Note, this is the
      /// ‘pure’ margin required, it is not the ‘effective’ margin used that
      /// factors in the trade risk if a GSLO is attached to the trade.
      /// </summary>
      public decimal initialMarginRequired { get; set; }
   }

   /// <summary>
   /// A TradeReduce object represents a Trade for an instrument that was reduced (either partially or fully) in an 
   /// Account. It is found embedded in Transactions that affect the position of an instrument in the account, specifically 
   /// the OrderFill Transaction.
   /// </summary>
   public class TradeReduce : TradeAction
   {
      /// <summary>
      /// The client Id for the Trade. 
      /// This is the same as the id in the clientExtensions (if provided) for the opened Trade.
      /// </summary>
      public string clientTradeID { get; set; }

      /// <summary>
      /// The PL realized when reducing the Trade
      /// </summary>
      public decimal realizedPL { get; set; }

      /// <summary>
      /// The financing paid/collected when reducing the Trade
      /// </summary>
      public decimal financing { get; set; }
   }

   public abstract class TradeAction
   {
      /// <summary>
      /// The ID of the Trade targeted by the action.
      /// </summary>
      public long tradeID { get; set; }

      /// <summary>
      /// The number of units transacted during the action.
      /// </summary>
      public decimal units { get; set; }

      /// <summary>
      /// The overall price used for the trade action.
      /// </summary>
      public decimal price { get; set; }

      /// <summary>
      /// This is the fee charged (if the trade action is a TradeOpen) for opening the trade if it has a guaranteed Stop
      /// Loss Order attached to it.
      /// </summary>
      public decimal? guaranteedExecutionFee { get; set; }

      /// <summary>
      /// The half spread cost (if the trade action is a TradeOpen). This can be a positive or
      /// negative value and is represented in the home currency of the Account.
      /// </summary>
      public decimal? halfSpreadCost { get; set; }
   }

   #region price object
   [JsonConverter(typeof(PriceObjectConverter))]
   public class TakeProfitDetails : PriceObject
   {
      public TakeProfitDetails() { }
      public TakeProfitDetails(Instrument.Instrument instrument)
      {
         priceInformation = new PriceInformation()
         {
            instrument = instrument,
            priceProperties = new List<string>() { nameof(price) }
         };
      }

      /// <summary>
      /// The price that the Take Profit Order will be triggered at.
      /// </summary>
      public decimal price { get; set; }
   }

   [JsonConverter(typeof(PriceObjectConverter))]
   public class StopLossDetails : PriceObject
   {
      public StopLossDetails() { }
      public StopLossDetails(Instrument.Instrument instrument)
      {
         priceInformation = new PriceInformation()
         {
            instrument = instrument,
            priceProperties = new List<string>() { nameof(price), nameof(distance) }
         };
      }

      /// <summary>
      /// The price that the Stop Loss Order will be triggered at. Only one of the
      /// price and distance fields may be specified.
      /// </summary>
      public decimal? price { get; set; }

      /// <summary>
      /// Specifies the distance (in price units) from the Trade’s open price to
      /// use as the Stop Loss Order price. Only one of the distance and price
      /// fields may be specified.
      /// </summary>
      public decimal? distance { get; set; }

      /// <summary>
      /// Flag indicating that the price for the Stop Loss Order is guaranteed. The
      /// default value depends on the GuaranteedStopLossOrderMode of the account,
      /// if it is REQUIRED, the default will be true, for DISABLED or ENABLED the
      /// default is false.
      /// </summary>
      public bool guaranteed { get; set; }
   }

   [JsonConverter(typeof(PriceObjectConverter))]
   public class TrailingStopLossDetails : PriceObject
   {
      public TrailingStopLossDetails() { }
      public TrailingStopLossDetails(Instrument.Instrument instrument)
      {
         priceInformation = new PriceInformation()
         {
            instrument = instrument,
            priceProperties = new List<string>() { nameof(distance) }
         };
      }

      /// <summary>
      /// The distance (in price units) from the Trade’s fill price that the
      /// Trailing Stop Loss Order will be triggered at.
      /// </summary>
      public decimal distance { get; set; }
   }

   public abstract class PriceObject : IHasPrices
   {
      /// <summary>
      /// The time in force for the created Order. This may only
      /// be GTC, GTD or GFD.
      /// </summary>
      public string timeInForce { get; set; }

      /// <summary>
      /// The date when the Trailing Stop Loss Order will be cancelled on if
      /// timeInForce is GTD.
      /// </summary>
      public DateTime? gtdTime { get; set; }

      /// <summary>
      /// The Client Extensions to add to the Trailing Stop Loss Order when
      /// created.
      /// </summary>
      public ClientExtensions clientExtensions { get; set; }

      /// <summary>
      /// 
      /// </summary>
      [JsonIgnore]
      public PriceInformation priceInformation { get; set; }
   }
   #endregion

   #region order reasons
   public abstract class OrderReason
   {
      public const string ClientOrder = "CLIENT_ORDER";
   }

   public class MarketOrderReason : OrderReason
   {
      public const string TradeClose = "TRADE_CLOSE";
      public const string PositionCloseout = "POSITION_CLOSEOUT";
      public const string MarginCloseout = "MARGIN_CLOSEOUT";
      public const string DelayedTradeClose = "DELAYED_TRADE_CLOSE";
   }

   public class FixedPriceOrderReason
   {
      public const string PlatformAccountMigration = "PLATFORM_ACCOUNT_MIGRATION";
   }

   #region entry order reasons
   public abstract class EntryOrderReason : OrderReason
   {
      public const string Replacement = "REPLACEMENT";
   }

   public class LimitOrderReason : EntryOrderReason
   {
   }

   public class MarketIfTouchedOrderReason : EntryOrderReason
   {
   }

   public class StopOrderReason : EntryOrderReason
   {
   }
   #endregion

   #region exit order reasons
   public abstract class ExitOrderReason : OrderReason
   {
      public const string Replacement = "REPLACEMENT";
      public const string OnFill = "ON_FILL";
   }

   public class TakeProfitOrderReason : ExitOrderReason
   {
   }

   public class StopLossOrderReason : ExitOrderReason
   {
   }

   public class TrailingStopLossOrderReason : ExitOrderReason
   {
   }
   #endregion

   #endregion

   /// <summary>
   /// The reason that a Transaction was rejected.
   /// </summary>
   public class TransactionRejectReason
   {
      public const string InternalServerError = "INTERNAL_SERVER_ERROR";
      public const string InstrumentPriceUnknown = "INTERNAL_SERVER_ERROR";
      public const string AccountNotActive = "INTERNAL_SERVER_ERROR";
      public const string AccountLocked = "INTERNAL_SERVER_ERROR";
      public const string AccountOrderCreationLocked = "INTERNAL_SERVER_ERROR";
      public const string AccountConfigurationLocked = "INTERNAL_SERVER_ERROR";
      public const string AccountDepositLocked = "INTERNAL_SERVER_ERROR";
      public const string AccountWithdrawalLocked = "INTERNAL_SERVER_ERROR";
      public const string AccountOrderCancelLocked = "INTERNAL_SERVER_ERROR";
      public const string InstrumentNotTradeable = "INTERNAL_SERVER_ERROR";
      public const string PendingOrdersAllowedExceeded = "INTERNAL_SERVER_ERROR";
      public const string OrderIdUnspecified = "ORDER_ID_UNSPECIFIED";
      public const string OrderDoesntExist = "ORDER_DOESNT_EXIST";
      public const string OrderIdentifierInconsistency = "ORDER_IDENTIFIER_INCONSISTENCY";
      public const string TradeIdUnspecified = "TRADE_ID_UNSPECIFIED";
      public const string TradeDoesntExist = "TRADE_DOESNT_EXIST";
      public const string TradeIdentifierInconsistency = "TRADE_IDENTIFIER_INCONSISTENCY";
      public const string InstrumentMissing = "INSTRUMENT_MISSING";
      public const string InstrumentUnknown = "INSTRUMENT_UNKNOWN";
      public const string UnitsMissing = "UNITS_MISSING";
      public const string UnitsInvalid = "UNITS_INVALID";
      public const string UnitsPrecisionExceeded = "UNITS_PRECISION_EXCEEDED";
      public const string UnitsLimitExceeded = "UNITS_LIMIT_EXCEEDED";
      public const string UnitsMinimumNotMet = "UNITS_MIMIMUM_NOT_MET";
      public const string PriceMissing = "PRICE_MISSING";
      public const string PriceInvalid = "PRICE_INVALID";
      public const string PricePrecisionExceeded = "PRICE_PRECISION_EXCEEDED";
      public const string PriceDistanceMissing = "PRICE_DISTANCE_MISSING";
      public const string PriceDistanceInvalid = "PRICE_DISTANCE_INVALID";
      public const string PriceDistancePrecisionExceeded = "PRICE_DISTANCE_PRECISION_EXCEEDED";
      public const string PriceDistanceMaximumExceeded = "PRICE_DISTANCE_MAXIMUM_EXCEEDED";
      public const string PriceDistanceMinimumNotMet = "PRICE_DISTANCE_MINIMUM_NOT_MET";
      public const string TimeInForceMissing = "TIME_IN_FORCE_MISSING";
      public const string TimeInForceInvalid = "TIME_IN_FORCE_INVALID";
      public const string TimeInForceGtdTimestampMissing = "TIME_IN_FORCE_GTD_TIMESTAMP_MISSING";
      public const string TimeInForceGtdTimestampInPast = "TIME_IN_FORCE_GTD_TIMESTAMP_IN_PAST";
      public const string PriceBoundInvalid = "PRICE_BOUND_INVALID";
      public const string PriceBoundPrecisionExceeded = "PRICE_BOUND_PRECISION_EXCEEDED";
      public const string OrdersOnFillDuplicateClientOrderIDs = "ORDERS_ON_FILL_DUPLICATE_CLIENT_ORDER_IDS";
      public const string TradeOnFillClientExtensionsNotSupported = "TRADE_ON_FILL_CLIENT_EXTENSIONS_NOT_SUPPORTED";
      public const string ClientOrderIdInvalid = "CLIENT_ORDER_ID_INVALID";
      public const string ClientOrderIdAlreadyExists = "CLIENT_ORDER_ID_ALREADY_EXISTS";
      public const string ClientOrderTagInvalid = "CLIENT_ORDER_TAG_INVALID";
      public const string ClientOrderCommentInvalid = "CLIENT_ORDER_COMMENT_INVALID";
      public const string ClientTradeIdInvalid = "CLIENT_TRADE_ID_INVALID";
      public const string ClientTradeIdAlreadyExists = "CLIENT_TRADE_ID_ALREADY_EXISTS";
      public const string ClientTradeTagInvalid = "CLIENT_TRADE_TAG_INVALID";
      public const string ClientTradeCommentInvalid = "CLIENT_TRADE_COMMENT_INVALID";
      public const string OrderFillPositionActionMissing = "ORDER_FILL_POSITION_ACTION_MISSING";
      public const string OrderFillPositionActionInvalid = "ORDER_FILL_POSITION_ACTION_INVALID";
      public const string TriggerConditionMissing = "TRIGGER_CONDITION_MISSING";
      public const string TriggerConditionInvalid = "TRIGGER_CONDITION_INVALID";
      public const string OrderPartialFillOptionMissing = "ORDER_PARTIAL_FILL_OPTION_MISSING";
      public const string OrderPartialFillOptionInvalid = "ORDER_PARTIAL_FILL_OPTION_INVALID";
      public const string InvalidReissueImmediatePartialFill = "INVALID_REISSUE_IMMEDIATE_PARTIAL_FILL";
      public const string TakeProfitOrderAlreadyExists = "TAKE_PROFIT_ORDER_ALREADY_EXISTS";
      public const string TakeProfitOnFillPriceMissing = "TAKE_PROFIT_ON_FILL_PRICE_MISSING";
      public const string TakeProfitOnFillPriceInvalid = "TAKE_PROFIT_ON_FILL_PRICE_INVALID";
      public const string TakeProfitOnFillPricePrecisionExceeded = "TAKE_PROFIT_ON_FILL_PRICE_PRECISION_EXCEEDED";
      public const string TakeProfitOnFillTimeInForceMissing = "TAKE_PROFIT_ON_FILL_TIME_IN_FORCE_MISSING";
      public const string TakeProfitOnFillTimeInForceInvalid = "TAKE_PROFIT_ON_FILL_TIME_IN_FORCE_INVALID";
      public const string TakeProfitOnFillGtdTimestampMissing = "TAKE_PROFIT_ON_FILL_GTD_TIMESTAMP_MISSING";
      public const string TakeProfitOnFillGtdTimestampInPast = "TAKE_PROFIT_ON_FILL_GTD_TIMESTAMP_IN_PAST";
      public const string TakeProfitOnFillClientOrderIdInvalid = "TAKE_PROFIT_ON_FILL_CLIENT_ORDER_ID_INVALID";
      public const string TakeProfitOnFillClientOrderTagInvalid = "TAKE_PROFIT_ON_FILL_CLIENT_ORDER_TAG_INVALID";
      public const string TakeProfitOnFillClientOrderCommentInvalid = "TAKE_PROFIT_ON_FILL_CLIENT_ORDER_COMMENT_INVALID";
      public const string TakeProfitOnFillTriggerConditionMissing = "TAKE_PROFIT_ON_FILL_TRIGGER_CONDITION_MISSING";
      public const string TakeProfitOnFillTriggerConditionInvalid = "TAKE_PROFIT_ON_FILL_TRIGGER_CONDITION_INVALID";
      public const string StopLossOrderAlreadyExists = "STOP_LOSS_ORDER_ALREADY_EXISTS";
      public const string StopLossOrderGuaranteedRequired = "STOP_LOSS_ORDER_GUARANTEED_REQUIRED";
      public const string StopLossOrderGuaranteedPriceWithinSpread = "STOP_LOSS_ORDER_GUARANTEED_PRICE_WITHIN_SPREAD";
      public const string StopLossOrderGuaranteedNotAllowed = "STOP_LOSS_ORDER_GUARANTEED_NOT_ALLOWED";
      public const string StopLossOrderGuaranteedHaltedCreateViolation = "STOP_LOSS_ORDER_GUARANTEED_HALTED_CREATE_VIOLATION";
      public const string StopLossOrderGuaranteedHaltedTightenViolation = "STOP_LOSS_ORDER_GUARANTEED_HALTED_TIGHTEN_VIOLATION";
      public const string StopLossOrderGuaranteedHedgingNotAllowed = "STOP_LOSS_ORDER_GUARANTEED_HEDGING_NOT_ALLOWED";
      public const string StopLossOrderGuaranteedMinimumDistanceNotMet = "STOP_LOSS_ORDER_GUARANTEED_MINIMUM_DISTANCE_NOT_MET";
      public const string StopLossOrderNotCancelable = "STOP_LOSS_ORDER_NOT_CANCELABLE";
      public const string StopLossOrderNotReplaceable = "STOP_LOSS_ORDER_NOT_REPLACEABLE";
      public const string StopLossOrderGuaranteedLevelRestrictionExceeded = "STOP_LOSS_ORDER_GUARANTEED_LEVEL_RESTRICTION_EXCEEDED";
      public const string StopLossOrderPriceAndDistanceBothSpecified = "STOP_LOSS_ORDER_PRICE_AND_DISTANCE_BOTH_SPECIFIED";
      public const string StopLossOrderPriceAndDistanceBothMissing = "STOP_LOSS_ORDER_PRICE_AND_DISTANCE_BOTH_MISSING";
      public const string StopLossOnFillRequiredForPendingOrder = "STOP_LOSS_ON_FILL_REQUIRED_FOR_PENDING_ORDER";
      public const string StopLossOnFillGuaranteedNotAllowed = "STOP_LOSS_ON_FILL_GUARANTEED_NOT_ALLOWED";
      public const string StopLossOnFillGuaranteedRequired = "STOP_LOSS_ON_FILL_GUARANTEED_REQUIRED";
      public const string StopLossOnFillPriceMissing = "STOP_LOSS_ON_FILL_PRICE_MISSING";
      public const string StopLossOnFillPriceInvalid = "STOP_LOSS_ON_FILL_PRICE_INVALID";
      public const string StopLossOnFillPricePrecisionExceeded = "STOP_LOSS_ON_FILL_PRICE_PRECISION_EXCEEDED";
      public const string StopLossOnFillGuaranteedMinimumDistanceNotMet = "STOP_LOSS_ON_FILL_GUARANTEED_MINIMUM_DISTANCE_NOT_MET";
      public const string StopLossOnFillGuaranteedLevelRestrictionExceeded = "STOP_LOSS_ON_FILL_GUARANTEED_LEVEL_RESTRICTION_EXCEEDED";
      public const string StopLossOnFillDistanceInvalid = "STOP_LOSS_ON_FILL_DISTANCE_INVALID";
      public const string StopLossOnFillPriceDistanceMaximumExceeded = "STOP_LOSS_ON_FILL_PRICE_DISTANCE_MAXIMUM_EXCEEDED";
      public const string StopLossOnFillDistancePrecisionExceeded = "STOP_LOSS_ON_FILL_DISTANCE_PRECISION_EXCEEDED";
      public const string StopLossOnFillPriceAndDistanceBothSpecified = "STOP_LOSS_ON_FILL_PRICE_AND_DISTANCE_BOTH_SPECIFIED";
      public const string StopLossOnFillPriceAndDistanceBothMissing = "STOP_LOSS_ON_FILL_PRICE_AND_DISTANCE_BOTH_MISSING";
      public const string StopLossOnFillTimeInForceMissing = "STOP_LOSS_ON_FILL_TIME_IN_FORCE_MISSING";
      public const string StopLossOnFillTimeInForceInvalid = "STOP_LOSS_ON_FILL_TIME_IN_FORCE_INVALID";
      public const string StopLossOnFillGtdTimestampMissing = "STOP_LOSS_ON_FILL_GTD_TIMESTAMP_MISSING";
      public const string StopLossOnFillGtdTimestampInPast = "STOP_LOSS_ON_FILL_GTD_TIMESTAMP_IN_PAST";
      public const string StopLossOnFillClientOrderIdInvalid = "STOP_LOSS_ON_FILL_CLIENT_ORDER_ID_INVALID";
      public const string StopLossOnFillClientOrderTagInvalid = "STOP_LOSS_ON_FILL_CLIENT_ORDER_TAG_INVALID";
      public const string StopLossOnFillClientOrderCommentInvalid = "STOP_LOSS_ON_FILL_CLIENT_ORDER_COMMENT_INVALID";
      public const string StopLossOnFillTriggerConditionMissing = "STOP_LOSS_ON_FILL_TRIGGER_CONDITION_MISSING";
      public const string StopLossOnFillTriggerConditionInvalid = "STOP_LOSS_ON_FILL_TRIGGER_CONDITION_INVALID";
      public const string TrailingStopLossOrderAlreadyExists = "TRAILING_STOP_LOSS_ORDER_ALREADY_EXISTS";
      public const string TrailingStopLossOnFillPriceDistanceMissing = "TRAILING_STOP_LOSS_ON_FILL_PRICE_DISTANCE_MISSING";
      public const string TrailingStopLossOnFillPriceDistanceInvalid = "TRAILING_STOP_LOSS_ON_FILL_PRICE_DISTANCE_INVALID";
      public const string TrailingStopLossOnFillPriceDistancePrecisionExceeded = "TRAILING_STOP_LOSS_ON_FILL_PRICE_DISTANCE_PRECISION_EXCEEDED";
      public const string TrailingStopLossOnFillPriceDistanceMaximumExceeded = "TRAILING_STOP_LOSS_ON_FILL_PRICE_DISTANCE_MAXIMUM_EXCEEDED";
      public const string TrailingStopLossOnFillPriceDistanceMinimumNotMet = "TRAILING_STOP_LOSS_ON_FILL_PRICE_DISTANCE_MINIMUM_NOT_MET";
      public const string TrailingStopLossOnFillTimeInForceMissing = "TRAILING_STOP_LOSS_ON_FILL_TIME_IN_FORCE_MISSING";
      public const string TrailingStopLossOnFillTimeInForceInvalid = "TRAILING_STOP_LOSS_ON_FILL_TIME_IN_FORCE_INVALID";
      public const string TrailingStopLossOnFillGtdTimestampMissing = "TRAILING_STOP_LOSS_ON_FILL_GTD_TIMESTAMP_MISSING";
      public const string TrailingStopLossOnFillGtdTimestampInPast = "TRAILING_STOP_LOSS_ON_FILL_GTD_TIMESTAMP_IN_PAST";
      public const string TrailingStopLossOnFillClientOrderIdInvalid = "TRAILING_STOP_LOSS_ON_FILL_CLIENT_ORDER_ID_INVALID";
      public const string TrailingStopLossOnFillClientOrderTagInvalid = "TRAILING_STOP_LOSS_ON_FILL_CLIENT_ORDER_TAG_INVALID";
      public const string TrailingStopLossOnFillClientOrderCommentInvalid = "TRAILING_STOP_LOSS_ON_FILL_CLIENT_ORDER_COMMENT_INVALID";
      public const string TrailingStopLossOrdersNotSupported = "TRAILING_STOP_LOSS_ORDERS_NOT_SUPPORTED";
      public const string TrailingStopLossOnFillTriggerConditionMissing = "TRAILING_STOP_LOSS_ON_FILL_TRIGGER_CONDITION_MISSING";
      public const string TrailingStopLossOnFillTriggerConditionInvalid = "TRAILING_STOP_LOSS_ON_FILL_TRIGGER_CONDITION_INVALID";
      public const string CloseTradeTypeMissing = "CLOSE_TRADE_TYPE_MISSING";
      public const string CloseTradePartialUnitsMissing = "CLOSE_TRADE_PARTIAL_UNITS_MISSING";
      public const string CloseTradeUnitsExceedTradeSize = "CLOSE_TRADE_UNITS_EXCEED_TRADE_SIZE";
      public const string CloseoutPositionDoesntExist = "CLOSEOUT_POSITION_DOESNT_EXIST";
      public const string CloseoutPositionIncompleteSpecification = "CLOSEOUT_POSITION_INCOMPLETE_SPECIFICATION";
      public const string CloseoutPositionUnitsExceedPositionSize = "CLOSEOUT_POSITION_UNITS_EXCEED_POSITION_SIZE";
      public const string CloseoutPositionReject = "CLOSEOUT_POSITION_REJECT";
      public const string CloseoutPositionPartialUnitsMissing = "CLOSEOUT_POSITION_PARTIAL_UNITS_MISSING";
      public const string MarkupGroupIdInvalid = "MARKUP_GROUP_ID_INVALID";
      public const string PositionAggregationModeInvalid = "POSITION_AGGREGATION_MODE_INVALID";
      public const string AdminConfigureDataMissing = "ADMIN_CONFIGURE_DATA_MISSING";
      public const string MarginRateInvalid = "MARGIN_RATE_INVALID";
      public const string MarginRateWouldTriggerCloseout = "MARGIN_RATE_WOULD_TRIGGER_CLOSEOUT";
      public const string AliasInvalid = "ALIAS_INVALID";
      public const string ClientConfigureDataMissing = "CLIENT_CONFIGURE_DATA_MISSING";
      public const string MarginRateWouldTriggerMarginCall = "MARGIN_RATE_WOULD_TRIGGER_MARGIN_CALL";
      public const string AmountInvalid = "AMOUNT_INVALID";
      public const string InsufficientFunds = "INSUFFICIENT_FUNDS";
      public const string AmountMissing = "AMOUNT_MISSING";
      public const string FundingReasonMissing = "FUNDING_REASON_MISSING";
      public const string ClientExtensionsDataMissing = "CLIENT_EXTENSIONS_DATA_MISSING";
      public const string ReplacingOrderInvalid = "REPLACING_ORDER_INVALID";
      public const string ReplacingTradeIdInvalid = "REPLACING_TRADE_ID_INVALID";
      public const string NoSuchOrder = "NO_SUCH_ORDER";
      public const string NoSuchTrade = "NO_SUCH_TRADE";
      public const string NoSuchTransaction = "NO_SUCH_TRANSACTION";
   }

   /// <summary>
   /// The possible types of a Transaction
   /// </summary>
   public class TransactionType
   {
      public const string Create = "CREATE";
      public const string Close = "CLOSE";
      public const string Reopen = "REOPEN";
      public const string ClientConfigure = "CLIENT_CONFIGURE";
      public const string ClientConfigureReject = "CLIENT_CONFIGURE_REJECT";
      public const string TransferFunds = "TRANSFER_FUNDS";
      public const string TransferFundsReject = "TRANSFER_FUNDS_REJECT";
      public const string MarketOrder = "MARKET_ORDER";
      public const string MarketOrderReject = "MARKET_ORDER_REJECT";
      public const string FixedPriceOrder = "FIXED_PRICE_ORDER";
      public const string LimitOrder = "LIMIT_ORDER";
      public const string LimitOrderReject = "LIMIT_ORDER_REJECT";
      public const string StopOrder = "STOP_ORDER";
      public const string StopOrderReject = "STOP_ORDER_REJECT";
      public const string MarketIfTouchedOrder = "MARKET_IF_TOUCHED_ORDER";
      public const string MarketIfTouchedOrderReject = "MARKET_IF_TOUCHED_ORDER_REJECT";
      public const string TakeProfitOrder = "TAKE_PROFIT_ORDER";
      public const string TakeProfitOrderReject = "TAKE_PROFIT_ORDER_REJECT";
      public const string StopLossOrder = "STOP_LOSS_ORDER";
      public const string StopLossOrderReject = "STOP_LOSS_ORDER_REJECT";
      public const string TrailingStopLossOrder = "TRAILING_STOP_LOSS_ORDER";
      public const string TrailingStopLossOrderReject = "TRAILING_STOP_LOSS_ORDER_REJECT";
      public const string OrderFill = "ORDER_FILL";
      public const string OrderCancel = "ORDER_CANCEL";
      public const string OrderCancelReject = "ORDER_CANCEL_REJECT";
      public const string OrderClientExtensionsModify = "ORDER_CLIENT_EXTENSIONS_MODIFY";
      public const string OrderClientExtensionsModifyReject = "ORDER_CLIENT_EXTENSIONS_MODIFY_REJECT";
      public const string TradeClientExtensionsModify = "TRADE_CLIENT_EXTENSIONS_MODIFY";
      public const string TradeClientExtensionsModifyReject = "TRADE_CLIENT_EXTENSIONS_MODIFY_REJECT";
      public const string MarginCallEnter = "MARGIN_CALL_ENTER";
      public const string MarginCallExtend = "MARGIN_CALL_EXTEND";
      public const string MarginCallExit = "MARGIN_CALL_EXIT";
      public const string DelayedTradeClosure = "DELAYED_TRADE_CLOSURE";
      public const string DailyFinancing = "DAILY_FINANCING";
      public const string ResetResettablePL = "RESET_RESETTABLE_PL";
   }

   /// <summary>
   /// A filter that can be used when fetching Transactions
   /// </summary>
   public class TransactionFilter : TransactionType
   {
      public const string Order = "ORDER";
      public const string Funding = "FUNDING";
      public const string Admin = "ADMIN";
      public const string OneCancelsAllOrder = "ONE_CANCELS_ALL_ORDER";
      public const string OneCancelsAllOrderReject = "ONE_CANCELS_ALL_ORDER_REJECT";
      public const string OneCancelsAllOrderTriggered = "ONE_CANCELS_ALL_ORDER_TRIGGERED";
   }

   /// <summary>
   /// A VWAP Receipt provides a record of how the price for an Order fill is constructed. If the Order is 
   /// filled with multiple buckets in a depth of market, each bucket will be represented with a VWAP 
   /// Receipt.
   /// </summary>
   public class VWAPReceipt
   {
      public decimal units { get; set; }
      public decimal price { get; set; }
   }
}
