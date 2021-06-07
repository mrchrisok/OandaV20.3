using System;
using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.Instrument
{
   public class OrderBook
   {
	  /// <summary>
	  /// The order book’s instrument
	  /// </summary>
	  public string instrument { get; set; }

	  /// <summary>
	  /// The time when the order book snapshot was created.
	  /// </summary>
	  public DateTime time { get; set; }

	  /// <summary>
	  /// The price (midpoint) for the order book’s instrument at the time of the 
	  /// order book snapshot
	  /// </summary>
	  public decimal price { get; set; }

	  /// <summary>
	  /// The price width for each bucket. Each bucket covers the price range from 
	  /// the bucket’s price to the bucket’s price + bucketWidth.
	  /// </summary>
	  public decimal bucketWidth { get; set; }

	  /// <summary>
	  /// The partitioned order book, divided into buckets using a default bucket
	  /// width. These buckets are only provided for price ranges which actually
	  /// contain order or position data.
	  /// </summary>
	  public List<OrderBookBucket> buckets { get; set; }
   }
}
