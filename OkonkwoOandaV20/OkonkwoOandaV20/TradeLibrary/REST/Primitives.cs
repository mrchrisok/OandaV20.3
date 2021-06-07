namespace OkonkwoOandaV20.TradeLibrary.REST
{
   /// <summary>
   /// DateTime header added to each v20 request
   /// http://developer.oanda.com/rest-live-v20/primitives-df/
   /// </summary>
   public enum AcceptDatetimeFormat
   {
	  /// <summary>
	  /// If “UNIX” is specified, DateTime fields will be specified or returned in the “12345678.000000123” format.
	  /// The returned value represents the number of seconds since the Unix Epoch, January 1st, 1970.
	  /// </summary>
	  Unix,

	  /// <summary>
	  /// If “RFC3339” is specified, DateTime will be specified or returned in “yyyy-MM-ddTHH:mm:ss.fffffffZ” format.
	  /// </summary>
	  RFC3339
   }

   public class InstrumentName
   {
	  /// <summary>
	  /// Quotes and trading of these currency pairs are supported via v20 as of 2018-07-06.
	  /// Visit <see cref="https://www.oanda.com/currency/live-exchange-rates/">Oanda's Live Exchange Rates</see> page 
	  /// for a complete and current list of supported pairs.
	  /// </summary>
	  public class Currency
	  {
		 public const string EURUSD = "EUR_USD";
		 public const string USDJPY = "USD_JPY";
		 public const string AUDUSD = "AUD_USD";
		 public const string AUDJPY = "AUD_JPY";
		 public const string GBPUSD = "GBP_USD";
		 public const string NZDUSD = "NZD_USD";
		 public const string USDCHF = "USD_CHF";
		 public const string USDCAD = "USD_CAD";
	  }
   }

   /// <summary>
   /// A GuaranteedStopLossOrderLevelRestriction represents the total position size that can exist within a given 
   /// price window for Trades with guaranteed Stop Loss Orders attached for a specific Instrument.
   /// </summary>
   public class GuaranteedStopLossOrderLevelRestriction
   {
	  /// <summary>
	  /// Applies to Trades with a guaranteed Stop Loss Order attached for the
	  /// specified Instrument. This is the total allowed Trade volume that can
	  /// exist within the priceRange based on the trigger prices of the guaranteed
	  /// Stop Loss Orders.
	  /// </summary>
	  public decimal volume { get; set; }

	  /// <summary>
	  /// The price range the volume applies to. This value is in price units.
	  /// </summary>
	  public decimal priceRange { get; set; }
   }

   /// <summary>
   /// In the context of an Order or a Trade, defines whether the units are positive or negative.
   /// </summary>
   public class Direction
   {
	  /// <summary>
	  /// A long Order is used to to buy units of an Instrument. A Trade is long when it has bought units of an 
	  /// Instrument.
	  /// </summary>
	  public const string Long = "LONG";

	  /// <summary>
	  /// A short Order is used to to sell units of an Instrument. A Trade is short when it has sold units of an Instrument.
	  /// </summary>
	  public const string Short = "SHORT";
   }
}
