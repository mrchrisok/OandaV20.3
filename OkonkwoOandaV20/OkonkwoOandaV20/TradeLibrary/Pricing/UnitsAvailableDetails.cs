namespace OkonkwoOandaV20.TradeLibrary.Pricing
{
   /// <summary>
   /// Representation of how many units of an Instrument are available to be traded for both long and short 
   /// Orders
   /// </summary>
   public class UnitsAvailableDetails
   {
      /// <summary>
      /// The units available for long Orders.
      /// </summary>
      public long @long { get; set; }

      /// <summary>
      /// The units available for short Orders.
      /// </summary>
      public long @short { get; set; }
   }
}