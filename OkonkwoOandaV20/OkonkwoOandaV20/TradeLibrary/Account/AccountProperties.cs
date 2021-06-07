namespace OkonkwoOandaV20.TradeLibrary.Account
{
   public class AccountProperties
   {
      /// <summary>
      /// The Account's identifier
      /// </summary>
      public string id;

      /// <summary>
      /// The Account’s associated MT4 Account ID. This field will not be present
      /// if the Account is not an MT4 account.
      /// </summary>
      public int mt4AccountID;

      /// <summary>
      /// The Account's tags
      /// </summary>
      public string[] tags;
   }
}
