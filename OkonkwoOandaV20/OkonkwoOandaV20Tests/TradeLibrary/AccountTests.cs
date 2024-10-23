using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace OkonkwoOandaV20Tests.TradeLibrary
{
   [TestClass]
   public class AccountTests : Rest20TestsBase
   {
      #region Account
      [TestMethod]
      public void success_retrieve_accounts_list()
      {
         var accountsRetrieved = Results.Items.FirstOrDefault(x => x.Key == "01.0").Value;

         Assert.IsTrue(accountsRetrieved.Success, $"{accountsRetrieved.Success}: {accountsRetrieved.Details}");
      }

      [TestMethod]
      public void success_retrieve_correct_number_of_accounts()
      {
         string key = "01.1";
         var result = Results.Items.FirstOrDefault(x => x.Key == key).Value;

         Assert.IsTrue(result.Success, $"{result.Success}: {result.Details}");
      }

      [TestMethod]
      public void success_account_numbers_have_correct_format()
      {
         string key = "01.";
         var results = Results.Items.Where(x => x.Key.StartsWith(key) && x.Key != "01.0" && x.Key != "01.1");
         var failure = results.FirstOrDefault(x => x.Value.Success == false);

         string message = failure.Key != null ? $"{failure.Value.Success}: {failure.Value.Details}" : "";

         Assert.IsTrue(failure.Key == null, failure.Key + ": " + message);
      }

      [TestMethod]
      public void success_retrieve_account_detail_info()
      {
         // 08
         var accountRetrieved = Results.Items.FirstOrDefault(x => x.Key == "08.0").Value;
         var idIsCorrect = Results.Items.FirstOrDefault(x => x.Key == "08.1").Value;
         var correctCurrency = Results.Items.FirstOrDefault(x => x.Key == "08.2").Value;
         var correctOpenTradeCount = Results.Items.FirstOrDefault(x => x.Key == "08.3").Value;
         var tradesMatchTradeCount = Results.Items.FirstOrDefault(x => x.Key == "08.4").Value;

         Assert.IsTrue(accountRetrieved.Success, $"08.0,{accountRetrieved.Success}: {accountRetrieved.Details}");
         Assert.IsTrue(idIsCorrect.Success, $"08.1,{idIsCorrect.Success}: {idIsCorrect.Details}");
         Assert.IsTrue(correctCurrency.Success, $"08.2,{correctCurrency.Success}: {correctCurrency.Details}");
         Assert.IsTrue(correctOpenTradeCount.Success, $"08.3,{correctOpenTradeCount.Success}: {correctOpenTradeCount.Details}");
         Assert.IsTrue(tradesMatchTradeCount.Success, $"08.4,{tradesMatchTradeCount.Success}: {tradesMatchTradeCount.Details}");
      }

      [TestMethod]
      public void success_retrieve_instruments_list()
      {
         string key = "02.0";
         var results = Results.Items.Where(x => x.Key == key);
         var success = results.FirstOrDefault(x => x.Value.Success == true);

         Assert.IsTrue(success.Key == key, success.Key + ": " + success.Value);
      }

      [TestMethod]
      public void success_retrieve_single_instrument_info()
      {
         // 03
         var instrumentReceived = Results.Items.FirstOrDefault(x => x.Key == "03.0").Value;
         var instrumentTypeCorrect = Results.Items.FirstOrDefault(x => x.Key == "03.1").Value;
         var instrumentNameCorrect = Results.Items.FirstOrDefault(x => x.Key == "03.2").Value;

         Assert.IsTrue(instrumentReceived.Success, $"{instrumentReceived.Success}: {instrumentReceived.Details}");
         Assert.IsTrue(instrumentTypeCorrect.Success, $"{instrumentTypeCorrect.Success}: {instrumentTypeCorrect.Details}");
         Assert.IsTrue(instrumentNameCorrect.Success, $"{instrumentNameCorrect.Success}: {instrumentNameCorrect.Details}");
      }

      [TestMethod]
      public void success_retrieve_account_summary_info()
      {
         // 04
         var summaryRetrieved = Results.Items.FirstOrDefault(x => x.Key == "04.0").Value;
         var accountIdIsCorrect = Results.Items.FirstOrDefault(x => x.Key == "04.1").Value;
         var currencyIsCorrect = Results.Items.FirstOrDefault(x => x.Key == "04.2").Value;

         Assert.IsTrue(summaryRetrieved.Success, $"{summaryRetrieved.Success}: {summaryRetrieved.Details}");
         Assert.IsTrue(accountIdIsCorrect.Success, $"{accountIdIsCorrect.Success}: {accountIdIsCorrect.Details}");
         Assert.IsTrue(currencyIsCorrect.Success, $"{currencyIsCorrect.Success}: {currencyIsCorrect.Details}");
      }

      [TestMethod]
      public void success_patch_account_configuration()
      {
         // 05
         var configRetrieved = Results.Items.FirstOrDefault(x => x.Key == "05.0").Value;
         var aliasChanged = Results.Items.FirstOrDefault(x => x.Key == "05.1").Value;
         var marginRateChanged = Results.Items.FirstOrDefault(x => x.Key == "05.2").Value;
         var aliasReverted = Results.Items.FirstOrDefault(x => x.Key == "05.3").Value;
         var marginRateReverted = Results.Items.FirstOrDefault(x => x.Key == "05.4").Value;

         Assert.IsTrue(configRetrieved.Success, $"{configRetrieved.Success}: {configRetrieved.Details}");
         Assert.IsTrue(aliasChanged.Success, $"{aliasChanged.Success}: {aliasChanged.Details}");
         Assert.IsTrue(marginRateChanged.Success, $"{marginRateChanged.Success}: {marginRateChanged.Details}");
         Assert.IsTrue(aliasReverted.Success, $"{aliasReverted.Success}: {aliasReverted.Details}");
         Assert.IsTrue(marginRateReverted.Success, $"{marginRateReverted.Success}: {marginRateReverted.Details}");
      }

      [TestMethod]
      public void success_retrieve_account_changes()
      {
         var changesRetrieved = Results.Items.FirstOrDefault(x => x.Key == "17.0").Value;
         var ordersFilledReceived = Results.Items.FirstOrDefault(x => x.Key == "17.1").Value;
         var ordersCancelledReceived = Results.Items.FirstOrDefault(x => x.Key == "17.2").Value;
         var tradesClosedReceived = Results.Items.FirstOrDefault(x => x.Key == "17.3").Value;
         var positionsReceived = Results.Items.FirstOrDefault(x => x.Key == "17.4").Value;
         var accountHasMargin = Results.Items.FirstOrDefault(x => x.Key == "17.5").Value;

         Assert.IsTrue(changesRetrieved.Success, $"{changesRetrieved.Success}: {changesRetrieved.Details}");
         Assert.IsTrue(ordersFilledReceived.Success, $"{ordersFilledReceived.Success}: {ordersFilledReceived.Details}");
         Assert.IsTrue(ordersCancelledReceived.Success, $"{ordersCancelledReceived.Success}: {ordersCancelledReceived.Details}");
         Assert.IsTrue(tradesClosedReceived.Success, $"{tradesClosedReceived.Success}: {tradesClosedReceived.Details}");
         Assert.IsTrue(positionsReceived.Success, $"{positionsReceived.Success}: {positionsReceived.Details}");
         Assert.IsTrue(accountHasMargin.Success, $"{accountHasMargin.Success}: {accountHasMargin.Details}");
      }

      [TestMethod]
      public void success_error_response_has_correct_type()
      {
         var caughtAccountConfigurationError = Results.Items.FirstOrDefault(x => x.Key == "05.E0").Value;

         Assert.IsTrue(caughtAccountConfigurationError.Success, $"{caughtAccountConfigurationError.Success}: {caughtAccountConfigurationError.Details}");
      }

      #endregion
   }
}
