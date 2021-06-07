namespace OkonkwoOandaV20.TradeLibrary.Order
{
   /// <summary>
   /// http://developer.oanda.com/rest-live-v20/order-df/
   /// </summary>
   public class DynamicOrderState
   {
      /// <summary>
      /// The Order’s ID.
      /// </summary>
      public long id { get; set; }

      /// <summary>
      /// The Order’s calculated trailing stop value.
      /// </summary>
      public decimal trailingStopValue { get; set; }

      /// <summary>
      /// The distance between the Trailing Stop Loss Order’s trailingStopValue and
      /// the current Market Price. This represents the distance (in price units)
      /// of the Order from a triggering price. If the distance could not be
      /// determined, this value will not be set.
      /// </summary>
      public decimal? triggerDistance { get; set; }

      /// <summary>
      /// True if an exact trigger distance could be calculated. If false, it means
      /// the provided trigger distance is a best estimate. If the distance could
      /// not be determined, this value will not be set.
      /// </summary>
      public bool? isTriggerDistanceExact { get; set; }
   }
}
