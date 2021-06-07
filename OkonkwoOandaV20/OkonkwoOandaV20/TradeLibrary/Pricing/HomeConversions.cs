namespace OkonkwoOandaV20.TradeLibrary.Pricing
{
   /// <summary>
   /// HomeConversions represents the factors to use to convert quantities of a given currency into the 
   /// Account’s home currency. The conversion factor depends on the scenario the conversion is required for.
   /// </summary>
   public class HomeConversions
   {
      /// <summary>
      /// The currency to be converted into the home currency.
      /// </summary>
      public string currency { get; set; }

      /// <summary>
      /// The factor used to convert any gains for an Account in the specified
      /// currency into the Account’s home currency. This would include positive
      /// realized P/L and positive financing amounts. Conversion is performed by
      /// multiplying the positive P/L by the conversion factor.
      /// </summary>
      public decimal accountGain { get; set; }

      /// <summary>
      /// The string representation of a decimal number.
      /// </summary>
      public string accountLoss { get; set; }

      /// <summary>
      /// The factor used to convert a Position or Trade Value in the specified
      /// currency into the Account’s home currency. Conversion is performed by
      /// multiplying the Position or Trade Value by the conversion factor.
      /// </summary>
      public decimal positionValue { get; set; }
   }
}
