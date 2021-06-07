using OkonkwoOandaV20.Framework;
using OkonkwoOandaV20.Framework.TypeConverters;
using OkonkwoOandaV20.TradeLibrary.Transaction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OkonkwoOandaV20.TradeLibrary.REST
{
   public partial class Rest20
   {
	  /// <summary>
	  /// Create or replace a Trade's dependent Orders (TakeProfit, StopLoss and TrailingStopLoss) through the Trade itself
	  /// </summary>
	  /// <param name="accountID">Account identifier</param>
	  /// <param name="tradeSpecifier">Specifier for the Trade</param>
	  /// <param name="parameters">The parameters for the request</param>
	  /// <returns>The Transactions associated with the patched dependent orders</returns>
	  public static async Task<TradeOrdersResponse> PutTradeOrdersAsync(string accountID, long tradeSpecifier, TradeOrdersParameters parameters)
	  {
		 var request = new Request()
		 {
			Uri = $"{ServerUri(EServer.Account)}accounts/{accountID}/trades/{tradeSpecifier}/orders",
			Method = "PUT",
			Parameters = parameters ?? new TradeOrdersParameters()
		 };

		 return await MakeRequestAsync<TradeOrdersResponse, TradeOrdersErrorResponse>(request);
	  }

	  public class TradeOrdersParameters : Parameters
	  {
		 public TradeOrdersParameters()
		 {
			takeProfitAction = TradeOrdersAction.None;
			stopLossAction = TradeOrdersAction.None;
			trailingStopLossAction = TradeOrdersAction.None;
		 }

		 #region properties 
		 /// <summary>
		 /// The specification of the Take Profit to create/modify/cancel. If
		 /// takeProfit is set to null, the Take Profit Order will be cancelled if it
		 /// exists. If takeProfit is not provided, the exisiting Take Profit Order
		 /// will not be modified. If a sub-field of takeProfit is not specified, that
		 /// field will be set to a default value on create, and be inherited by the
		 /// replacing order on modify.
		 /// </summary>
		 public TakeProfitDetails takeProfit { get; protected set; }

		 /// <summary>
		 /// The specification of the Stop Loss to create/modify/cancel. If stopLoss
		 /// is set to null, the Stop Loss Order will be cancelled if it exists. If
		 /// stopLoss is not provided, the exisiting Stop Loss Order will not be
		 /// modified. If a sub-field of stopLoss is not specified, that field will be
		 /// set to a default value on create, and be inherited by the replacing order
		 /// on modify.
		 /// </summary>
		 public StopLossDetails stopLoss { get; protected set; }

		 /// <summary>
		 /// The specification of the Trailing Stop Loss to create/modify/cancel. If
		 /// trailingStopLoss is set to null, the Trailing Stop Loss Order will be
		 /// cancelled if it exists. If trailingStopLoss is not provided, the
		 /// exisiting Trailing Stop Loss Order will not be modified. If a sub-field
		 /// of trailngStopLoss is not specified, that field will be set to a default
		 /// value on create, and be inherited by the replacing order on modify.
		 /// </summary>
		 public TrailingStopLossDetails trailingStopLoss { get; protected set; }

		 /// <summary>
		 /// The API action to perform on the takeProfit.
		 /// Valid values are specified in the TradeOrdersAction class.
		 /// </summary>
		 public string takeProfitAction { get; protected set; }

		 /// <summary>
		 /// The API action to perform on the stopLoss.
		 /// Valid values are specified in the TradeOrdersAction class.
		 /// </summary>
		 public string stopLossAction { get; protected set; }

		 /// <summary>
		 /// The API action to perform on the trailingStopLoss.
		 /// Valid values are specified in the TradeOrdersAction class.
		 /// </summary>
		 public string trailingStopLossAction { get; protected set; }
		 #endregion

		 #region setter methods
		 public virtual void SetTakeProfit(string action, TakeProfitDetails details)
		 {
			takeProfitAction = action;
			takeProfit = action == TradeOrdersAction.Cancel ? null : details;
		 }

		 public virtual void SetStopLoss(string action, StopLossDetails details)
		 {
			stopLossAction = action;
			stopLoss = action == TradeOrdersAction.Cancel ? null : details;
		 }

		 public virtual void SetTrailingStopLoss(string action, TrailingStopLossDetails details)
		 {
			trailingStopLossAction = action;
			trailingStopLoss = action == TradeOrdersAction.Cancel ? null : details;
		 }
		 #endregion


		 internal override IDictionary<string, object> GetRequestParameters<P>(IList<ITypeConverter<string>> converters = null, bool excludeNulls = true)
		 {
			if (typeof(P) == typeof(BodyAttribute))
			{
			   var result = new Dictionary<string, object>();

			   if (takeProfitAction != TradeOrdersAction.None)
				  result.Add(nameof(takeProfit), takeProfit);
			   if (stopLossAction != TradeOrdersAction.None)
				  result.Add(nameof(stopLoss), stopLoss);
			   if (trailingStopLossAction != TradeOrdersAction.None)
				  result.Add(nameof(trailingStopLoss), trailingStopLoss);

			   return result;
			}

			return base.GetRequestParameters<P>(converters, excludeNulls);
		 }
	  }

	  public class TradeOrdersAction
	  {
		 public const string None = "NONE";
		 public const string Create = "CREATE";
		 public const string Replace = "REPLACE";
		 public const string Cancel = "CANCEL";
	  }
   }

   /// <summary>
   /// The PUT success response received from accounts/accountID/trades/tradeSpecifier/orders
   /// </summary>
   public class TradeOrdersResponse : Response
   {
	  /// <summary>
	  /// The Transaction created that cancels the Trade’s existing Take Profit
	  /// Order.
	  /// </summary>
	  public OrderCancelTransaction takeProfitOrderCancelTransaction { get; set; }

	  /// <summary>
	  /// The Transaction created that creates a new Take Profit Order for the
	  /// Trade.
	  /// </summary>
	  public TakeProfitOrderTransaction takeProfitOrderTransaction { get; set; }

	  /// <summary>
	  /// The Transaction created that immediately fills the Trade’s new Take
	  /// Profit Order. Only provided if the new Take Profit Order was immediately
	  /// filled.
	  /// </summary>
	  public OrderFillTransaction takeProfitOrderFillTransaction { get; set; }

	  /// <summary>
	  /// The Transaction created that immediately cancels the Trade’s new Take
	  /// Profit Order. Only provided if the new Take Profit Order was immediately
	  /// cancelled.
	  /// </summary>
	  public OrderCancelTransaction takeProfitOrderCreatedCancelTransaction { get; set; }

	  /// <summary>
	  /// The Transaction created that cancels the Trade’s existing Stop Loss
	  /// Order.
	  /// </summary>
	  public OrderCancelTransaction stopLossOrderCancelTransaction { get; set; }

	  /// <summary>
	  /// The Transaction created that creates a new Stop Loss Order for the Trade.
	  /// </summary>
	  public StopLossOrderTransaction stopLossOrderTransaction { get; set; }

	  /// <summary>
	  /// The Transaction created that immediately fills the Trade’s new Stop
	  /// Order. Only provided if the new Stop Loss Order was immediately filled.
	  /// </summary>
	  public OrderFillTransaction stopLossOrderFillTransaction { get; set; }

	  /// <summary>
	  /// The Transaction created that immediately cancels the Trade’s new Stop
	  /// Loss Order. Only provided if the new Stop Loss Order was immediately
	  /// cancelled.
	  /// </summary>
	  public OrderCancelTransaction stopLossOrderCreatedCancelTransaction { get; set; }

	  /// <summary>
	  /// The Transaction created that cancels the Trade’s existing Trailing Stop
	  /// Loss Order.
	  /// </summary>
	  public OrderCancelTransaction trailingStopLossOrderCancelTransaction { get; set; }

	  /// <summary>
	  /// The Transaction created that creates a new Trailing Stop Loss Order for
	  /// the Trade.
	  /// </summary>
	  public TrailingStopLossOrderTransaction trailingStopLossOrderTransaction { get; set; }
   }

   /// <summary>
   /// The PUT error response received from accounts/accountID/trades/tradeSpecifier/orders
   /// </summary>
   public class TradeOrdersErrorResponse : ErrorResponse
   {
	  /// <summary>
	  /// An OrderCancelRejectTransaction represents the rejection of the
	  /// cancellation of an Order in the client’s Account.
	  /// </summary>
	  public OrderCancelRejectTransaction takeProfitOrderCancelRejectTransaction { get; set; }

	  /// <summary>
	  /// A TakeProfitOrderRejectTransaction represents the rejection of the
	  /// creation of a TakeProfit Order.
	  /// </summary>
	  public TakeProfitOrderRejectTransaction takeProfitOrderRejectTransaction { get; set; }

	  /// <summary>
	  /// An OrderCancelRejectTransaction represents the rejection of the
	  /// cancellation of an Order in the client’s Account.
	  /// </summary>
	  public OrderCancelRejectTransaction stopLossOrderCancelRejectTransaction { get; set; }

	  /// <summary>
	  /// A StopLossOrderRejectTransaction represents the rejection of the creation
	  /// of a StopLoss Order.
	  /// </summary>
	  public StopLossOrderRejectTransaction stopLossOrderRejectTransaction { get; set; }

	  /// <summary>
	  /// An OrderCancelRejectTransaction represents the rejection of the
	  /// cancellation of an Order in the client’s Account.
	  /// </summary>
	  public OrderCancelRejectTransaction trailingStopLossOrderCancelRejectTransaction { get; set; }

	  /// <summary>
	  /// A TrailingStopLossOrderRejectTransaction represents the rejection of the
	  /// creation of a TrailingStopLoss Order.
	  /// </summary>
	  public TrailingStopLossOrderRejectTransaction trailingStopLossOrderRejectTransaction { get; set; }
   }
}
