using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace OkonkwoOandaV20Tests.TradeLibrary
{
   [TestClass]
   public class OrderTests : Rest20TestsBase
   {
	  #region Order

	  [TestMethod]
	  public void success_create_order()
	  {
		 var orderCreated = Results.Items.FirstOrDefault(x => x.Key == "11.0").Value;
		 var orderTypeIsCorrect = Results.Items.FirstOrDefault(x => x.Key == "11.1").Value;

		 Assert.IsTrue(orderCreated.Success, $"{orderCreated.Success}: {orderCreated.Details}");
		 Assert.IsTrue(orderTypeIsCorrect.Success, $"{orderTypeIsCorrect.Success}: {orderTypeIsCorrect.Details}");
	  }

	  [TestMethod]
	  public void success_get_all_orders()
	  {
		 var allOrdersRetrieved = Results.Items.FirstOrDefault(x => x.Key == "11.2").Value;
		 var testOrderTypeReturned = Results.Items.FirstOrDefault(x => x.Key == "11.3").Value;
		 var allOrdersByIDRetrieved = Results.Items.FirstOrDefault(x => x.Key == "11.29").Value;

		 Assert.IsTrue(allOrdersRetrieved.Success, $"{allOrdersRetrieved.Success}: {allOrdersRetrieved.Details}");
		 Assert.IsTrue(testOrderTypeReturned.Success, $"{testOrderTypeReturned.Success}: {testOrderTypeReturned.Details}");
		 Assert.IsTrue(allOrdersByIDRetrieved.Success, $"11.29,{allOrdersByIDRetrieved.Success}: { allOrdersByIDRetrieved.Details}");
	  }

	  [TestMethod]
	  public void success_get_pending_orders()
	  {
		 var pendingOrdersRetrieved = Results.Items.FirstOrDefault(x => x.Key == "11.4").Value;
		 var onlyPendingOrderRetrieved = Results.Items.FirstOrDefault(x => x.Key == "11.5").Value;
		 var testPendingOrderRetrieved = Results.Items.FirstOrDefault(x => x.Key == "11.6").Value;

		 Assert.IsTrue(pendingOrdersRetrieved.Success, $"{pendingOrdersRetrieved.Success}: {pendingOrdersRetrieved.Details}");
		 Assert.IsTrue(onlyPendingOrderRetrieved.Success, $"{onlyPendingOrderRetrieved.Success}: {onlyPendingOrderRetrieved.Details}");
		 Assert.IsTrue(testPendingOrderRetrieved.Success, $"{testPendingOrderRetrieved.Success}: {testPendingOrderRetrieved.Details}");
	  }

	  [TestMethod]
	  public void success_get_order_details()
	  {
		 var orderDetailsRetrieved = Results.Items.FirstOrDefault(x => x.Key == "11.7").Value;
		 var clientExtensionsRetrieved = Results.Items.FirstOrDefault(x => x.Key == "11.8").Value;
		 var tradeExtensionsRetrieved = Results.Items.FirstOrDefault(x => x.Key == "11.9").Value;

		 Assert.IsTrue(orderDetailsRetrieved.Success, $"{orderDetailsRetrieved.Success}: {orderDetailsRetrieved.Details}");
		 Assert.IsTrue(clientExtensionsRetrieved.Success, $"{clientExtensionsRetrieved.Success}: {clientExtensionsRetrieved.Details}");
		 Assert.IsTrue(tradeExtensionsRetrieved.Success, $"{tradeExtensionsRetrieved.Success}: {tradeExtensionsRetrieved.Details}");
	  }

	  [TestMethod]
	  public void success_update_order_extensions()
	  {
		 var extensionsUpdated = Results.Items.FirstOrDefault(x => x.Key == "11.10").Value;
		 var extensionsOrderCorrect = Results.Items.FirstOrDefault(x => x.Key == "11.11").Value;
		 var clientExtensionCommentCorrect = Results.Items.FirstOrDefault(x => x.Key == "11.12").Value;
		 var tradeExtensionCommentCorrect = Results.Items.FirstOrDefault(x => x.Key == "11.13").Value;

		 Assert.IsTrue(extensionsUpdated.Success, $"{extensionsUpdated.Success}: {extensionsUpdated.Details}");
		 Assert.IsTrue(extensionsOrderCorrect.Success, $"{extensionsOrderCorrect.Success}: {extensionsOrderCorrect.Details}");
		 Assert.IsTrue(clientExtensionCommentCorrect.Success, $"{clientExtensionCommentCorrect.Success}: {clientExtensionCommentCorrect.Details}");
		 Assert.IsTrue(tradeExtensionCommentCorrect.Success, $"{tradeExtensionCommentCorrect.Success}: {tradeExtensionCommentCorrect.Details}");
	  }

	  [TestMethod]
	  public void success_cancel_replace_order()
	  {
		 var orderWasCancelled = Results.Items.FirstOrDefault(x => x.Key == "11.14").Value;
		 var orderWasReplaced = Results.Items.FirstOrDefault(x => x.Key == "11.15").Value;
		 var newOrderDetailsCorrect = Results.Items.FirstOrDefault(x => x.Key == "11.16").Value;

		 Assert.IsTrue(orderWasCancelled.Success, $"{orderWasCancelled.Success}: {orderWasCancelled.Details}");
		 Assert.IsTrue(orderWasReplaced.Success, $"{orderWasReplaced.Success}: {orderWasReplaced.Details}");
		 Assert.IsTrue(newOrderDetailsCorrect.Success, $"{newOrderDetailsCorrect.Success}: {newOrderDetailsCorrect.Details}");
	  }

	  [TestMethod]
	  public void success_cancel_order()
	  {
		 var cancelledOrderReturned = Results.Items.FirstOrDefault(x => x.Key == "11.17").Value;
		 var orderWasCancelled = Results.Items.FirstOrDefault(x => x.Key == "11.18").Value;

		 Assert.IsTrue(cancelledOrderReturned.Success, $"{cancelledOrderReturned.Success}: {cancelledOrderReturned.Details}");
		 Assert.IsTrue(orderWasCancelled.Success, $"{orderWasCancelled.Success}: {orderWasCancelled.Details}");
	  }

	  [TestMethod]
	  public void success_cancel_replace_order_with_exit_orders()
	  {
		 var orderWasCreated = Results.Items.FirstOrDefault(x => x.Key == "11.19").Value;
		 var orderTypeIsCorrect = Results.Items.FirstOrDefault(x => x.Key == "11.20").Value;
		 var orderHasStopLoss = Results.Items.FirstOrDefault(x => x.Key == "11.21").Value;
		 var orderHasTakeProfit = Results.Items.FirstOrDefault(x => x.Key == "11.22").Value;
		 var orderWasCancelled = Results.Items.FirstOrDefault(x => x.Key == "11.23").Value;
		 var cancelledOrderHasCorrectReason = Results.Items.FirstOrDefault(x => x.Key == "11.24").Value;
		 var newOrderWasCreated = Results.Items.FirstOrDefault(x => x.Key == "11.25").Value;
		 var newOrderReasonIsCorrect = Results.Items.FirstOrDefault(x => x.Key == "11.26").Value;
		 var newOrderStopLossIsCorrect = Results.Items.FirstOrDefault(x => x.Key == "11.27").Value;
		 var newOrderTakeProfitIsCorrect = Results.Items.FirstOrDefault(x => x.Key == "11.28").Value;

		 Assert.IsTrue(orderWasCreated.Success, $"{orderWasCreated.Success}: {orderWasCreated.Details}");
		 Assert.IsTrue(orderTypeIsCorrect.Success, $"{orderTypeIsCorrect.Success}: {orderTypeIsCorrect.Details}");
		 Assert.IsTrue(orderHasStopLoss.Success, $"11.21,{orderHasStopLoss.Success}: {orderHasStopLoss.Details}");
		 Assert.IsTrue(orderHasTakeProfit.Success, $"{orderHasTakeProfit.Success}: {orderHasTakeProfit.Details}");
		 Assert.IsTrue(orderWasCancelled.Success, $"{orderWasCancelled.Success}: {orderWasCancelled.Details}");
		 Assert.IsTrue(cancelledOrderHasCorrectReason.Success, $"{cancelledOrderHasCorrectReason.Success}: {cancelledOrderHasCorrectReason.Details}");
		 Assert.IsTrue(newOrderWasCreated.Success, $"{newOrderWasCreated.Success}: {newOrderWasCreated.Details}");
		 Assert.IsTrue(newOrderReasonIsCorrect.Success, $"{newOrderReasonIsCorrect.Success}: {newOrderReasonIsCorrect.Details}");
		 Assert.IsTrue(newOrderStopLossIsCorrect.Success, $"{newOrderStopLossIsCorrect.Success}: {newOrderStopLossIsCorrect.Details}");
		 Assert.IsTrue(newOrderTakeProfitIsCorrect.Success, $"{newOrderTakeProfitIsCorrect.Success}: {newOrderTakeProfitIsCorrect.Details}");
	  }

	  [TestMethod]
	  public void success_error_response_has_correct_type()
	  {
		 var caughtOrderPostError = Results.Items.FirstOrDefault(x => x.Key == "11.E0").Value;
		 var caughtOrderClientExtensionsModifyError = Results.Items.FirstOrDefault(x => x.Key == "11.E1").Value;
		 var caughtOrderCancelReplaceError = Results.Items.FirstOrDefault(x => x.Key == "11.E2").Value;
		 var caughtOrderCancelError = Results.Items.FirstOrDefault(x => x.Key == "11.E3").Value;

		 Assert.IsTrue(caughtOrderPostError.Success, $"{caughtOrderPostError.Success}: {caughtOrderPostError.Details}");
		 Assert.IsTrue(caughtOrderClientExtensionsModifyError.Success, $"{caughtOrderClientExtensionsModifyError.Success}: {caughtOrderClientExtensionsModifyError.Details}");
		 Assert.IsTrue(caughtOrderCancelReplaceError.Success, $"{caughtOrderCancelReplaceError.Success}: {caughtOrderCancelReplaceError.Details}");
		 Assert.IsTrue(caughtOrderCancelError.Success, $"{caughtOrderCancelError.Success}: {caughtOrderCancelError.Details}");
	  }

	  #endregion
   }
}
