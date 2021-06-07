using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace OkonkwoOandaV20Tests.TradeLibrary
{
   [TestClass]
   public class TradeTests : Rest20TestsBase
   {
	  #region Trade

	  [TestMethod]
	  public void success_place_market_order()
	  {
		 var marketOrderCreated = Results.Items.FirstOrDefault(x => x.Key == "13.0").Value;
		 var marketOrderFilled = Results.Items.FirstOrDefault(x => x.Key == "13.1").Value;

		 Assert.IsTrue(marketOrderCreated.Success, $"{marketOrderCreated.Success}: {marketOrderCreated.Details}");
		 Assert.IsTrue(marketOrderFilled.Success, $"{marketOrderFilled.Success}: {marketOrderFilled.Details}");
	  }

	  [TestMethod]
	  public void success_get_trades_list()
	  {
		 var allTradesRetrieved = Results.Items.FirstOrDefault(x => x.Key == "13.2").Value;
		 var openTradesRetrieved = Results.Items.FirstOrDefault(x => x.Key == "13.3").Value;

		 Assert.IsTrue(allTradesRetrieved.Success, $"{allTradesRetrieved.Success}: {allTradesRetrieved.Details}");
		 Assert.IsTrue(openTradesRetrieved.Success, $"{openTradesRetrieved.Success}: {openTradesRetrieved.Details}");
	  }

	  [TestMethod]
	  public void success_get_trade_details()
	  {
		 var detailsRetrieved = Results.Items.FirstOrDefault(x => x.Key == "13.4").Value;

		 Assert.IsTrue(detailsRetrieved.Success, $"{detailsRetrieved.Success}: {detailsRetrieved.Details}");
	  }

	  [TestMethod]
	  public void success_update_trade_extensions()
	  {
		 var extensionsUpdated = Results.Items.FirstOrDefault(x => x.Key == "13.5").Value;
		 var extensionsTradeCorrect = Results.Items.FirstOrDefault(x => x.Key == "13.6").Value;
		 var tradeExtensionCommentCorrect = Results.Items.FirstOrDefault(x => x.Key == "13.7").Value;

		 Assert.IsTrue(extensionsUpdated.Success, $"{extensionsUpdated.Success}: {extensionsUpdated.Details}");
		 Assert.IsTrue(extensionsTradeCorrect.Success, $"{extensionsTradeCorrect.Success}: {extensionsTradeCorrect.Details}");
		 Assert.IsTrue(tradeExtensionCommentCorrect.Success, $"{tradeExtensionCommentCorrect.Success}: {tradeExtensionCommentCorrect.Details}");
	  }

	  [TestMethod]
	  public void success_patch_exit_orders()
	  {
		 var takeProfitPatched = Results.Items.FirstOrDefault(x => x.Key == "13.8").Value;
		 var takeProfitPriceCorrect = Results.Items.FirstOrDefault(x => x.Key == "13.9").Value;
		 var stopLossPatched = Results.Items.FirstOrDefault(x => x.Key == "13.10").Value;
		 var stopLossPriceCorrect = Results.Items.FirstOrDefault(x => x.Key == "13.11").Value;
		 var trailingStopLossPatched = Results.Items.FirstOrDefault(x => x.Key == "13.12").Value;
		 var trailingStopLossDistanceCorrect = Results.Items.FirstOrDefault(x => x.Key == "13.13").Value;

		 var takeProfitCancelled = Results.Items.FirstOrDefault(x => x.Key == "13.14").Value;
		 var stopLossCancelled = Results.Items.FirstOrDefault(x => x.Key == "13.15").Value;
		 var trailingStopLossCancelled = Results.Items.FirstOrDefault(x => x.Key == "13.16").Value;

		 Assert.IsTrue(takeProfitPatched.Success, $"{takeProfitPatched.Success}: {takeProfitPatched.Details}");
		 Assert.IsTrue(takeProfitPriceCorrect.Success, $"13.9,{takeProfitPriceCorrect.Success}: {takeProfitPriceCorrect.Details}");
		 Assert.IsTrue(stopLossPatched.Success, $"{stopLossPatched.Success}: {stopLossPatched.Details}");
		 Assert.IsTrue(stopLossPriceCorrect.Success, $"{stopLossPriceCorrect.Success}: {stopLossPriceCorrect.Details}");
		 Assert.IsTrue(trailingStopLossPatched.Success, $"{trailingStopLossPatched.Success}: {trailingStopLossPatched.Details}");
		 Assert.IsTrue(trailingStopLossDistanceCorrect.Success, $"{trailingStopLossDistanceCorrect.Success}: {trailingStopLossDistanceCorrect.Details}");
		 Assert.IsTrue(takeProfitCancelled.Success, $"{takeProfitCancelled.Success}: {takeProfitCancelled.Details}");
		 Assert.IsTrue(stopLossCancelled.Success, $"{stopLossCancelled.Success}: {stopLossCancelled.Details}");
		 Assert.IsTrue(trailingStopLossCancelled.Success, $"{trailingStopLossCancelled.Success}: {trailingStopLossCancelled.Details}");
	  }

	  [TestMethod]
	  public void success_close_open_trade()
	  {
		 var tradeClosed = Results.Items.FirstOrDefault(x => x.Key == "13.17").Value;
		 var tradeCloseHasTime = Results.Items.FirstOrDefault(x => x.Key == "13.18").Value;
		 var tradeCloseHasInstrument = Results.Items.FirstOrDefault(x => x.Key == "13.19").Value;
		 var tradeCloseUnitsCorrect = Results.Items.FirstOrDefault(x => x.Key == "13.20").Value;
		 var tradeCloseHasPrice = Results.Items.FirstOrDefault(x => x.Key == "13.21").Value;

		 Assert.IsTrue(tradeClosed.Success, $"{tradeClosed.Success}: {tradeClosed.Details}");
		 Assert.IsTrue(tradeCloseHasTime.Success, $"{tradeCloseHasTime.Success}: {tradeCloseHasTime.Details}");
		 Assert.IsTrue(tradeCloseHasInstrument.Success, $"{tradeCloseHasInstrument.Success}: {tradeCloseHasInstrument.Details}");
		 Assert.IsTrue(tradeCloseUnitsCorrect.Success, $"{tradeCloseUnitsCorrect.Success}: {tradeCloseUnitsCorrect.Details}");
		 Assert.IsTrue(tradeCloseHasPrice.Success, $"{tradeCloseHasPrice.Success}: {tradeCloseHasPrice.Details}");
	  }

	  [TestMethod]
	  public void success_error_response_has_correct_type()
	  {
		 var caughtTradeClientExtensionsModifyError = Results.Items.FirstOrDefault(x => x.Key == "13.E0").Value;
		 var caughtTradePatchExitOrdersError = Results.Items.FirstOrDefault(x => x.Key == "13.E1").Value;
		 var caughtTradeCloseError = Results.Items.FirstOrDefault(x => x.Key == "13.E2").Value;

		 Assert.IsTrue(caughtTradeClientExtensionsModifyError.Success, $"{caughtTradeClientExtensionsModifyError.Success}: {caughtTradeClientExtensionsModifyError.Details}");
		 Assert.IsTrue(caughtTradePatchExitOrdersError.Success, $"{caughtTradePatchExitOrdersError.Success}: {caughtTradePatchExitOrdersError.Details}");
		 Assert.IsTrue(caughtTradeCloseError.Success, $"{caughtTradeCloseError.Success}: {caughtTradeCloseError.Details}");
	  }

	  #endregion
   }
}
