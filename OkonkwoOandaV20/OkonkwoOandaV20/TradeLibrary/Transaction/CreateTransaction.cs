namespace OkonkwoOandaV20.TradeLibrary.Transaction
{
   /// <summary>
   /// A CreateTransaction represents the creation of an Account
   /// </summary>
   public class CreateTransaction : Transaction
   {
      /// <summary>
      /// The ID of the Division that the Account is in
      /// </summary>
      public int divisionID { get; set; }

      /// <summary>
      /// The ID of the Site that the Account was created at
      /// </summary>
      public int siteID { get; set; }

      /// <summary>
      /// The ID of the user that the Account was created for
      /// </summary>
      public int accountUserID { get; set; }

      /// <summary>
      /// The number of the Account within the site/division/user
      /// </summary>
      public int accountNumber { get; set; }

      /// <summary>
      /// The home currency of the Account.
      /// </summary>
      public string homeCurrency { get; set; }
   }
}