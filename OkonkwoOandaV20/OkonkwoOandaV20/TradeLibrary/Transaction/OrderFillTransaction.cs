using OkonkwoOandaV20.TradeLibrary.Pricing;
using System;
using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.Transaction
{
   /// <summary>
   /// An OrderFillTransaction represents the filling of an Order in the client's Account
   /// </summary>
   public class OrderFillTransaction : Transaction
   {
      /// <summary>
      /// The ID of the Order filled.
      /// </summary>
      public long orderID { get; set; }

      /// <summary>
      /// The client Order ID of the Order filled (only provided /// the client has 
      /// assigned one).
      /// </summary>
      public string clientOrderID { get; set; }

      /// <summary>
      /// The name of the filled Order’s instrument.
      /// </summary>
      public string instrument { get; set; }

      /// <summary>
      /// The number of units filled by the Order.
      /// </summary>
      public decimal units { get; set; }

      /// <summary>
      /// This is the conversion factor in effect for the Account at the time of
      /// the OrderFill for converting any gains realized in Instrument quote units
      /// into units of the Account’s home currency.
      /// </summary>
      public decimal gainQuoteHomeConversionFactor { get; set; }

      /// <summary>
      /// This is the conversion factor in effect for the Account at the time of
      /// the OrderFill for converting any losses realized in Instrument quote
      /// units into units of the Account’s home currency.
      /// </summary>
      public decimal lossQuoteHomeConversionFactor { get; set; }

      /// <summary>
      /// This field is now deprecated and should no longer be used. The individual
      /// tradesClosed, tradeReduced and tradeOpened fields contain the
      /// exact/official price each unit was filled at.
      /// </summary>
      [Obsolete("Deprecated: Will be removed in a future API update.")]
      public decimal price { get; set; }

      /// <summary>
      /// The price in effect for the account at the time of the Order fill.
      /// </summary>
      public ClientPrice fullPrice { get; set; }

      /// <summary>
      /// The reason that an Order was filled
      /// </summary>
      public string reason { get; set; }

      /// <summary>
      /// The profit or loss incurred when the Order was filled.
      /// </summary>
      public decimal pl { get; set; }

      /// <summary>
      /// The financing paid or collected when the Order was filled.
      /// </summary>
      public decimal financing { get; set; }

      /// <summary>
      /// The commission charged in the Account’s home currency as a result of
      /// filling the Order. The commission is always represented as a positive
      /// quantity of the Account’s home currency, however it reduces the balance
      /// in the Account.
      /// </summary>
      public decimal commission { get; set; }

      /// <summary>
      /// The total guaranteed execution fees charged for all Trades opened, closed
      /// or reduced with guaranteed Stop Loss Orders.
      /// </summary>
      public decimal guaranteedExecutionFee { get; set; }

      /// <summary>
      /// The Account’s balance after the Order was filled.
      /// </summary>
      public decimal accountBalance { get; set; }

      /// <summary>
      /// The Trade that was opened when the Order was filled (only provided ///
      /// filling the Order resulted in a new Trade).
      /// </summary>
      public TradeOpen tradeOpened { get; set; }

      /// <summary>
      /// The Trades that were closed when the Order was filled (only provided ///
      /// filling the Order resulted in a closing open Trades).
      /// </summary>
      public List<TradeReduce> tradesClosed { get; set; }

      /// <summary>
      /// The Trade that was reduced when the Order was filled (only provided ///
      /// filling the Order resulted in reducing an open Trade).
      /// </summary>
      public TradeReduce tradeReduced { get; set; }

      /// <summary>
      /// The half spread cost for the OrderFill, which is the sum of the
      /// halfSpreadCost values in the tradeOpened, tradesClosed and tradeReduced
      /// fields. This can be a positive or negative value and is represented in
      /// the home currency of the Account.
      /// </summary>
      public decimal halfSpreadCost { get; set; }
   }
}
