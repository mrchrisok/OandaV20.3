using System;
using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.Pricing
{
   /// <summary>
   /// The specification of an Account-specific Price
   /// </summary>
   public class Price
   {
	  /// <summary>
	  /// The string “PRICE”. Used to identify the a Price object when found in a 
	  /// stream.
	  /// </summary>
	  public string type { get; set; }

	  /// <summary>
	  /// The Price’s Instrument.
	  /// </summary>
	  public string instrument { get; set; }

	  /// <summary>
	  /// The date/time when the Price was created
	  /// </summary>
	  public DateTime time { get; set; }

	  /// <summary>
	  /// The status of the Price.
	  /// </summary>
	  [Obsolete("Deprecated: Will be removed in a future API update.")]
	  public string status { get; set; }

	  /// <summary>
	  /// Flag indicating if the Price is tradeable or not
	  /// </summary>
	  public bool tradeable { get; set; }

	  /// <summary>
	  /// The list of prices and liquidity available on the Instrument’s bid side.
	  /// It is possible for this list to be empty if there is no bid liquidity
	  /// currently available for the Instrument in the Account.
	  /// </summary>
	  public List<PriceBucket> bids { get; set; }

	  /// <summary>
	  /// The list of prices and liquidity available on the Instrument’s ask side.
	  /// It is possible for this list to be empty if there is no ask liquidity
	  /// currently available for the Instrument in the Account.
	  /// </summary>
	  public List<PriceBucket> asks { get; set; }

	  /// <summary>
	  /// The closeout bid Price. This Price is used when a bid is required to
	  /// closeout a Position (margin closeout or manual) yet there is no bid
	  /// liquidity. The closeout bid is never used to open a new position.
	  /// </summary>
	  public decimal closeoutBid { get; set; }

	  /// <summary>
	  /// The closeout ask Price. This Price is used when a ask is required to
	  /// closeout a Position (margin closeout or manual) yet there is no ask
	  /// liquidity. The closeout ask is never used to open a new position.
	  /// </summary>
	  public decimal closeoutAsk { get; set; }

	  /// <summary>
	  /// The factors used to convert quantities of this price’s Instrument’s quote 
	  /// currency into a quantity of the Account’s home currency.  When the 
	  /// includeHomeConversions is present in the pricing request (regardless of 
	  /// its value), this field will not be present.
	  /// </summary>
	  [Obsolete("Deprecated: Will be removed in a future API update.")]
	  public QuoteHomeConversionFactors quoteHomeConversionFactors { get; set; }

	  /// <summary>
	  /// Representation of how many units of an Instrument are available to be 
	  /// traded by an Order depending on its postionFill option.
	  /// </summary>
	  [Obsolete("Deprecated: Will be removed in a future API update.")]
	  public UnitsAvailable unitsAvailable { get; set; }
   }
}
