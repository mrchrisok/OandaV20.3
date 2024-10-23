using Microsoft.VisualStudio.TestTools.UnitTesting;
using OkonkwoOandaV20.Framework;
using OkonkwoOandaV20.Framework.Factories;
using OkonkwoOandaV20.TradeLibrary.Account;
using OkonkwoOandaV20.TradeLibrary.Instrument;
using OkonkwoOandaV20.TradeLibrary.Order;
using OkonkwoOandaV20.TradeLibrary.Position;
using OkonkwoOandaV20.TradeLibrary.Pricing;
using OkonkwoOandaV20.TradeLibrary.REST;
using OkonkwoOandaV20.TradeLibrary.REST.OrderRequests;
using OkonkwoOandaV20.TradeLibrary.Transaction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static OkonkwoOandaV20.TradeLibrary.REST.Rest20;

namespace OkonkwoOandaV20Tests.TradeLibrary
{
   public abstract class Rest20TestsBase
   {
      static Rest20TestsBase()
      {
         m_Results = new Rest20TestResults();
      }

      #region Default Configuration

      // warning: do not rely on these
      // For best results, please create and use your own or your organization's Oanda Practice account

      private static EEnvironment m_TestEnvironment = EEnvironment.Practice;
      private static string m_TestToken = "api_token";
      private static short m_TokenAccounts = -1;
      private static string m_TestAccount = "account_number";

      #endregion

      #region Properties

      private static bool m_ApiOperationsComplete = false;
      private static string m_Currency = "USD";
      private static string m_TestInstrument = InstrumentName.Currency.USDCHF;
      private static List<Instrument> m_OandaInstruments;
      private static List<Price> m_OandaPrices;
      private static long m_FirstTransactionID;
      private static long m_LastTransactionID;
      private static DateTime m_LastTransactionTime;
      private static decimal m_TestNumber;
      private static bool m_TestInitialized;
      private static readonly Rest20TestResults m_Results;

      protected static Rest20TestResults Results { get { return m_Results; } }
      private static string AccountID { get { return Credentials.GetDefaultCredentials().DefaultAccountId; } }

      #endregion

      [TestInitialize]
      public void CheckIfAllApiOperationsHaveCompleted()
      {
         RunApiOperationsAsync().Wait();

         while (!m_ApiOperationsComplete)
         {
            Task.Delay(250).Wait();
         }
      }

      private static async Task RunApiOperationsAsync()
      {
         if (m_TestInitialized)
         {
            return;
         }

         m_TestInitialized = true;

         try
         {
            await SetApiCredentialsAsync();

            if (Credentials.GetDefaultCredentials() == null)
            {
               throw new Exception("Exception: RestV20Test - Credentials must be defined to run this test.");
            }

            if (Credentials.GetDefaultCredentials().HasServer(EServer.Account))
            {
               // first, get accounts
               // this operation adds the test AccountId to Credentials (if it is null)
               await Account_GetAccountsListAsync(m_TokenAccounts);

               // second, check market status
               if (await Utilities.IsMarketHaltedAsync())
               {
                  throw new MarketHaltedException("Unable to continue tests. OANDA Fx market is halted!");
               }

               // third, proceed with all other operations
               await Account_GetAccountDetailsAsync();
               await Account_GetAccountSummaryAsync();
               await Account_GetAccountsInstrumentsAsync();
               await Account_GetSingleAccountInstrumentAsync();
               await Account_PatchAccountConfigurationAsync();

               await Transaction_GetTransactionAsync();
               await Transaction_GetTransactionsSinceIdAsync();

               // important: do this before Orders and Trades tests
               await Pricing_GetPricingAsync();

               await Instrument_GetInstrumentCandlesAsync();
               await Instrument_GetInstrumentOrderBookAsync();
               await Instrument_GetInstrumentPositionBookAsync();

               // test the pricing stream
               if (Credentials.GetDefaultCredentials().HasServer(EServer.PricingStream))
               {
                  await Stream_GetStreamingPricesAsync();
               }

               // start transactions stream
               Task transactionsStreamCheckAsync = null;
               if (Credentials.GetDefaultCredentials().HasServer(EServer.TransactionsStream))
               {
                  transactionsStreamCheckAsync = Stream_GetStreamingTransactionsAsync();
               }

               // create stream traffic
               await Order_RunOrderOperationsAsync();
               await Trade_RunTradeOperationsAsync();
               await Position_RunPositionOperationsAsync();

               // stop transactions stream 
               if (transactionsStreamCheckAsync != null)
               {
                  await transactionsStreamCheckAsync;
               }

               // review the traffic
               await Transaction_GetTransactionsByDateRangeAsync();
               await Transaction_GetTransactionsByIdRangeAsync();
               await Account_GetAccountChangesAsync();
            }
         }
         catch (MarketHaltedException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            throw new Exception("An unhandled error occured during execution of REST V20 operations.", ex);
         }
         finally
         {
            m_ApiOperationsComplete = true;
         }
      }

      #region Account

      /// <summary>
      /// Retrieve the list of accounts associated with the account token
      /// </summary>
      /// <param name="listCount"></param>
      private static async Task Account_GetAccountsListAsync(short? listCount = null)
      {
         short count = 0;

         List<AccountProperties> result = await Rest20.GetAccountsAsync();
         m_Results.Verify("01.0", result.Count > 0, "Account list received.");

         string message = "Correct number of accounts received.";
         bool correctCount = true;
         if (listCount.HasValue && listCount.Value > 0)
         {
            count++;
            correctCount = result.Count == listCount;
            message = $"Correct number of accounts ({result.Count}) received.";
         }
         m_Results.Verify("01." + count.ToString(), correctCount, message);

         foreach (var account in result)
         {
            count++;
            string description = $"Account id {account.id} has correct format.";
            m_Results.Verify("01." + count.ToString(), account.id.Split('-').Length == 4, description);
         }

         // ensure the first account has sufficient funds
         m_TestAccount = m_TestAccount ?? result.OrderBy(r => r.id).First().id;
         Credentials.SetCredentials(m_TestEnvironment, m_TestToken, m_TestAccount);
      }

      // <summary>
      // Retrieve the full details for the test accountId
      // </summary>
      private static async Task Account_GetAccountDetailsAsync()
      {
         // first, kill all open trades
         var closeList = await Rest20.GetOpenTradesAsync(AccountID);

         foreach (var trade in closeList)
            Rest20.PutTradeCloseAsync(AccountID, trade.id).Wait();

         Account result = await Rest20.GetAccountAsync(AccountID);

         m_Results.Verify("08.0", result != null, $"Account {AccountID} info received.");
         m_Results.Verify("08.1", result.id == AccountID, $"Account id ({result.id}) is correct.");
         m_Results.Verify("08.2", result.currency == m_Currency, $"Account currency ({result.currency}) is correct.");
         m_Results.Verify("08.3", result.openTradeCount == 0, $"Account openTradeCount ({0}) is correct.");
         m_Results.Verify("08.4", result.trades.Count == result.openTradeCount, $"Account trades count ({0}) is correct.");
      }

      /// <summary>
      /// Retrieve the list of instruments associated with the given accountId
      /// </summary>
      private static async Task Account_GetAccountsInstrumentsAsync()
      {
         // Get an instrument list (basic)
         List<Instrument> result = await Rest20.GetAccountInstrumentsAsync(AccountID);
         m_Results.Verify("02.0", result.Count > 0, "Instrument list received");

         // Store the instruments for other tests
         m_OandaInstruments = result;
      }

      /// <summary>
      /// Retrieve the list of instruments associated with the given accountId
      /// </summary>
      private static async Task Account_GetSingleAccountInstrumentAsync()
      {
         // Get an instrument list (basic)
         var instruments = new List<string>() { m_TestInstrument };
         string type = InstrumentType.Currency;
         var parameters = new AccountInstrumentsParameters() { instruments = instruments };

         List<Instrument> result = await Rest20.GetAccountInstrumentsAsync(AccountID, parameters);

         m_Results.Verify("03.0", result.Count == 1, $"{instruments[0]} info received.");
         m_Results.Verify("03.1", result[0].type == type, $"{instruments[0]} type ({result[0].type}) is correct.");
         m_Results.Verify("03.2", result[0].name == instruments[0], $"{instruments[0]} name ({result[0].name}) is correct.");
      }

      /// <summary>
      /// Retrieve summary information for the given accountId
      /// </summary>
      private static async Task Account_GetAccountSummaryAsync()
      {
         // 04
         AccountSummary result = await Rest20.GetAccountSummaryAsync(AccountID);
         m_Results.Verify("04.0", result != null, $"Account {AccountID} info received.");
         m_Results.Verify("04.1", result.id == AccountID, $"AccountSummary.id ({result.id}) is correct.");
         m_Results.Verify("04.2", result.currency == m_Currency, $"AccountSummary.currency ({result.currency}) is correct.");
      }

      /// <summary>
      /// Retrieve summary information for the given accountId
      /// </summary>
      private static async Task Account_PatchAccountConfigurationAsync()
      {
         // 05
         AccountSummary summary = await Rest20.GetAccountSummaryAsync(AccountID);

         m_FirstTransactionID = summary.lastTransactionID;
         m_LastTransactionID = summary.lastTransactionID;

         string alias = summary.alias;
         decimal? marginRate = summary.marginRate;

         string testAlias = $"testAlias_{DateTime.UtcNow:yyyyMMddTHHmmssfffffffK}";
         decimal? testMarginRate = (decimal)(marginRate == null ? 0.5 : (((double)marginRate.Value == 0.5) ? 0.4 : 0.5));
         var parameters = new AccountConfigurationParameters()
         {
            alias = testAlias,
            marginRate = testMarginRate
         };

         m_LastTransactionTime = DateTime.UtcNow;

         AccountConfigurationResponse response = null;

         // error test
         try { response = await Rest20.PatchAccountConfigurationAsync("fakeId", parameters); }
         catch (Exception ex)
         {
            var errorResponse = ErrorResponseFactory.Create(ex.Message) as AccountConfigurationErrorResponse;
            m_Results.Verify("05.E0", errorResponse != null, $"Error response has correct type: {nameof(AccountConfigurationErrorResponse)}");
         }

         // response test
         response = await Rest20.PatchAccountConfigurationAsync(AccountID, parameters);
         ClientConfigureTransaction newConfig = response.clientConfigureTransaction;

         m_Results.Verify("05.0", newConfig != null, "Account configuration retrieved successfully.");
         m_Results.Verify("05.1", newConfig.alias != alias, $"Account alias {newConfig.alias} updated successfully.");
         m_Results.Verify("05.2", newConfig.marginRate != marginRate, $"Account marginRate {newConfig.marginRate} updated succesfully.");

         parameters.alias = alias;
         parameters.marginRate = marginRate ?? (decimal)1.0;
         AccountConfigurationResponse response2 = await Rest20.PatchAccountConfigurationAsync(AccountID, parameters);
         ClientConfigureTransaction newConfig2 = response2.clientConfigureTransaction;

         m_Results.Verify("05.3", newConfig2.alias == alias, $"Account alias {newConfig2.alias} reverted successfully.");
         m_Results.Verify("05.4", newConfig2.marginRate == marginRate, $"Account marginRate {newConfig2.marginRate} reverted succesfully.");
      }

      private static async Task Account_GetAccountChangesAsync()
      {
         //17
         var parameters = new AccountChangesParameters() { sinceTransactionID = m_FirstTransactionID };
         AccountChangesResponse result = await Rest20.GetAccountChangesAsync(AccountID, parameters);

         var changes = result.changes;

         m_Results.Verify("17.0", result != null, "Account changes received.");
         m_Results.Verify("17.1", changes.ordersFilled.Count > 0, "Account has filled orders.");
         m_Results.Verify("17.2", changes.ordersCancelled.Count > 0, "Account has cancelled orders.");
         m_Results.Verify("17.3", changes.tradesClosed.Count > 0, "Account has closed trades.");
         m_Results.Verify("17.4", changes.positions.Count > 0, "Account has position.");
         m_Results.Verify("17.5", result.state.marginAvailable > 0, "Account has transactions.");
      }
      #endregion

      #region Instrument
      // <summary>
      // Retrieve the full details for the test accountId
      // </summary>
      private static async Task Instrument_GetInstrumentCandlesAsync()
      {
         short count = 500;

         // get all price types .. MBA
         string price = string.Concat(CandleStickPriceType.Midpoint, CandleStickPriceType.Bid, CandleStickPriceType.Ask);

         string instrument = m_TestInstrument;
         string granularity = CandleStickGranularity.Hours01;

         var parameters = new InstrumentCandlesParameters()
         {
            price = price,
            granularity = granularity,
            count = count
         };

         List<CandlestickPlus> result = await Rest20.GetInstrumentCandlesAsync(instrument, parameters);
         CandlestickPlus candle = result.FirstOrDefault();

         m_Results.Verify("12.0", result != null, "Candles list received.");
         m_Results.Verify("12.1", result.Count() == count, "Candles count is correct.");
         m_Results.Verify("12.2", candle.instrument == instrument, "Candles instrument is correct.");
         m_Results.Verify("12.3", candle.granularity == granularity, "Candles granularity is correct.");
         m_Results.Verify("12.4", candle.mid != null && candle.mid.o > 0, "Candle has mid prices");
         m_Results.Verify("12.5", candle.bid != null && candle.bid.o > 0, "Candle has bid prices");
         m_Results.Verify("12.6", candle.ask != null && candle.ask.o > 0, "Candle has ask prices");
      }

      private static async Task Instrument_GetInstrumentOrderBookAsync()
      {
         // currently, Oanda only provides snapshots at 0th, 20th and 40th minute of each hour
         // this behavior appears to be undocumented, but was deduced by examining the 'Link' header of the response

         OrderBook result = null;

         // error test - future snapshot time
         var futureDateTime = DateTime.UtcNow.AddHours(1);
         string unavailableSnapshotTimeString = $"{Utilities.ConvertDateTimeUtcToAcceptDateFormat(futureDateTime).Split(':')[0]}:00:00Z";
         DateTime? unavailabeSnapshotTime = Utilities.ConvertAcceptDateFormatDateToDateTimeUtc(unavailableSnapshotTimeString);
         var parameters = new InstrumentOrderBookParameters()
         {
            time = unavailabeSnapshotTime,
            getLastTimeOnFailure = false
         };
         try { result = await Rest20.GetInstrumentOrderBookAsync(m_TestInstrument, parameters); }
         catch (Exception ex)
         {
            var errorResponse = ErrorResponseFactory.Create(ex.Message);
            m_Results.Verify("19.E0", errorResponse != null, "Future snapshot time returns an error response.");
         }

         // get the 0th hour (or previous) snapshot
         string availableSnapshotTimeString = $"{Utilities.ConvertDateTimeUtcToAcceptDateFormat(DateTime.UtcNow).Split(':')[0]}:00:00Z";
         DateTime? availableSnapshotTime = Utilities.ConvertAcceptDateFormatDateToDateTimeUtc(availableSnapshotTimeString);
         parameters.time = availableSnapshotTime;
         parameters.getLastTimeOnFailure = true;
         result = await Rest20.GetInstrumentOrderBookAsync(m_TestInstrument, parameters);

         m_Results.Verify("19.0", result != null, "Order book snapshot was received.");
         m_Results.Verify("19.1", result.time == availableSnapshotTime, "Order book snapshot time is correct.");
         m_Results.Verify("19.2", result.buckets.Count > 0, "Order book snapshot has buckets.");
      }

      private static async Task Instrument_GetInstrumentPositionBookAsync()
      {
         // currently, Oanda only provides snapshots at 0th, 20th and 40th minute of each hour
         // this behavior appears to be undocumented, but was deduced by examining the 'Link' header of the response

         PositionBook result = null;

         // error test - future snapshot time
         var futureDateTime = DateTime.UtcNow.AddHours(1);
         string unavailableSnapshotTimeString = $"{Utilities.ConvertDateTimeUtcToAcceptDateFormat(futureDateTime).Split(':')[0]}:00:00Z";
         DateTime? unavailableSnapshotTime = Utilities.ConvertAcceptDateFormatDateToDateTimeUtc(unavailableSnapshotTimeString);
         var parameters = new InstrumentPositionBookParameters()
         {
            time = unavailableSnapshotTime,
            getLastTimeOnFailure = false
         };
         try { result = await Rest20.GetInstrumentPositionBookAsync(m_TestInstrument, parameters); }
         catch (Exception ex)
         {
            var errorResponse = ErrorResponseFactory.Create(ex.Message);
            m_Results.Verify("20.E0", errorResponse != null, "Future snapshot time returns an error response.");
         }

         // get the 0th hour (or previous) snapshot
         string availableSnapshotTimeString = $"{Utilities.ConvertDateTimeUtcToAcceptDateFormat(DateTime.UtcNow).Split(':')[0]}:00:00Z";
         DateTime? availableSnapshotTime = Utilities.ConvertAcceptDateFormatDateToDateTimeUtc(availableSnapshotTimeString);
         parameters.time = availableSnapshotTime;
         parameters.getLastTimeOnFailure = true;
         result = await Rest20.GetInstrumentPositionBookAsync(m_TestInstrument, parameters);

         m_Results.Verify("20.0", result != null, "Position book snapshot was received.");
         m_Results.Verify("20.1", result.time == availableSnapshotTime, "Position book snapshot time is correct.");
         m_Results.Verify("20.2", result.buckets.Count > 0, "Position book snapshot has buckets.");
      }
      #endregion

      #region Order
      /// <summary>
      /// Runs operations available at OANDA's Order endpoint
      /// </summary>
      /// <returns></returns>
      private static async Task Order_RunOrderOperationsAsync()
      {
         if (await Utilities.IsMarketHaltedAsync())
            throw new MarketHaltedException("OANDA Fx market is halted!");

         DateTime expiry = DateTime.UtcNow.AddMonths(1);
         decimal price = GetOandaPrice(m_TestInstrument) * (decimal)0.9;

         #region create new pending order
         var orderRequest1 = new MarketIfTouchedOrderRequest(GetOandaInstrument())
         {
            units = 1, // buy
            timeInForce = TimeInForce.GoodUntilDate,
            gtdTime = expiry,
            priceBound = price * (decimal)1.01,
            clientExtensions = new ClientExtensions()
            {
               id = "test_order_1",
               comment = "test order 1 comment",
               tag = "test_order_1"
            },
            tradeClientExtensions = new ClientExtensions()
            {
               id = "test_trade_1",
               comment = "test trade 1 comment",
               tag = "test_trade_1"
            }
         };

         // first, kill any open orders
         var openOrders = await Rest20.GetPendingOrdersAsync(AccountID);
         openOrders.ForEach(async x => await Rest20.PutOrderCancelAsync(AccountID, x.id));

         // error test - create order
         PostOrderResponse response1 = null;
         orderRequest1.price = -1;
         var postOrderParameters1_1 = new PostOrderParameters() { order = orderRequest1 };
         try { response1 = await Rest20.PostOrderAsync(AccountID, postOrderParameters1_1); }
         catch (Exception ex)
         {
            var errorResponse = ErrorResponseFactory.Create(ex.Message) as PostOrderErrorResponse;
            m_Results.Verify("11.E0", errorResponse != null, $"Error response has correct type: {nameof(PostOrderErrorResponse)}");
         }

         // create order1
         orderRequest1.price = price;
         var postOrderParameter1_2 = new PostOrderParameters() { order = orderRequest1 };
         response1 = await Rest20.PostOrderAsync(AccountID, postOrderParameter1_2);
         var orderTransaction1 = response1.orderCreateTransaction;

         m_Results.Verify("11.0", orderTransaction1 != null && orderTransaction1.id > 0, "Order successfully opened");
         m_Results.Verify("11.1", orderTransaction1.type == TransactionType.MarketIfTouchedOrder, "Order type is correct.");

         // create order2
         var orderRequest2 = new MarketIfTouchedOrderRequest(GetOandaInstrument())
         {
            units = -1, // sell
            timeInForce = TimeInForce.GoodUntilDate,
            gtdTime = expiry,
            priceBound = price * (decimal)0.99,
            clientExtensions = new ClientExtensions()
            {
               id = "test_order_2",
               comment = "test order 2 comment",
               tag = "test_order_2"
            },
            tradeClientExtensions = new ClientExtensions()
            {
               id = "test_trade_2",
               comment = "test trade 2 comment",
               tag = "test_trade_2"
            }
         };
         orderRequest2.price = price;
         var postOrderParameter2_1 = new PostOrderParameters() { order = orderRequest2 };
         PostOrderResponse response2 = await Rest20.PostOrderAsync(AccountID, postOrderParameter2_1);
         var orderTransaction2 = response2.orderCreateTransaction;

         // Get all orders
         var allOrders = await Rest20.GetOrdersAsync(AccountID);
         m_Results.Verify("11.2", allOrders != null && allOrders.Count > 0, "All orders list successfully retrieved");
         m_Results.Verify("11.3", allOrders.Where(x => x.id == orderTransaction1.id || x.id == orderTransaction2.id).Count() == 2, "Test orders in all orders return.");

         // Get pending orders
         var pendingOrders = await Rest20.GetPendingOrdersAsync(AccountID);
         m_Results.Verify("11.4", pendingOrders != null && pendingOrders.Count > 0, "Pending orders list successfully retrieved");
         m_Results.Verify("11.5", pendingOrders.Where(x => x.state != OrderState.Pending).Count() == 0, "Only pending orders returned.");
         m_Results.Verify("11.6", pendingOrders.Where(x => x.id == orderTransaction1.id || x.id == orderTransaction2.id).Count() == 2, "Test orders in pending orders return.");

         // Get orders by ID list
         var pendingOrderParameters = new OrdersParameters() { ids = new List<string>() };
         pendingOrders.ForEach(pendingOrder => pendingOrderParameters.ids.Add(pendingOrder.id.ToString()));
         var allPendingOrders = await Rest20.GetOrdersAsync(AccountID, pendingOrderParameters);
         m_Results.Verify("11.29", allPendingOrders.Where(x => x.id == orderTransaction1.id || x.id == orderTransaction2.id).Count() == 2, "Test orders in orders by ID list return.");
         // kill orderTransaction2 because it is no longer needed
         await Rest20.PutOrderCancelAsync(AccountID, orderTransaction2.id);

         // Get order details
         var order1 = await Rest20.GetOrderAsync(AccountID, orderTransaction1.id) as MarketIfTouchedOrder;
         m_Results.Verify("11.7", order1 != null && order1.id == orderTransaction1.id, "Order details successfully retrieved.");
         m_Results.Verify("11.8", (order1.clientExtensions == null) == (orderRequest1.clientExtensions == null), "order client extensions successfully retrieved.");
         m_Results.Verify("11.9", (order1.tradeClientExtensions == null) == (orderRequest1.tradeClientExtensions == null), "order trade client extensions successfully retrieved.");

         // create extensions
         var newOrderExtensions = orderRequest1.clientExtensions;
         newOrderExtensions.comment = "updated order 1 comment";
         var newTradeExtensions = orderRequest1.tradeClientExtensions;
         newTradeExtensions.comment = "updated trade 1 comment";

         // error test - update extensions
         OrderClientExtensionsResponse extensionsModifyResponse = null;
         var extensionsParameters = new OrderClientExtensionsParameters()
         {
            clientExtensions = newOrderExtensions,
            tradeClientExtensions = newTradeExtensions
         };
         try { extensionsModifyResponse = await Rest20.PutOrderClientExtensionsAsync(AccountID, -1, extensionsParameters); }
         catch (Exception ex)
         {
            var errorResponse = ErrorResponseFactory.Create(ex.Message) as OrderClientExtensionsErrorResponse;
            m_Results.Verify("11.E1", errorResponse != null, $"Error response has correct type: {nameof(OrderClientExtensionsErrorResponse)}");
         }

         // Udpate extensions
         extensionsModifyResponse = await Rest20.PutOrderClientExtensionsAsync(AccountID, order1.id, extensionsParameters);

         m_Results.Verify("11.10",
           extensionsModifyResponse != null,
           "Order extensions update received successfully.");

         m_Results.Verify("11.11",
           extensionsModifyResponse.orderClientExtensionsModifyTransaction.orderID == order1.id,
           "Correct order extensions updated.");

         m_Results.Verify("11.12",
           extensionsModifyResponse.orderClientExtensionsModifyTransaction.clientExtensionsModify.comment == "updated order 1 comment",
           "Order extensions comment updated successfully.");

         m_Results.Verify("11.13",
           extensionsModifyResponse.orderClientExtensionsModifyTransaction.tradeClientExtensionsModify.comment == "updated trade 1 comment",
           "Order trade extensions comment updated successfully.");

         // error test - cancel+replace an existing order
         OrderReplaceResponse cancelReplaceResponse = null;
         orderRequest1.price = -1;
         var orderReplaceParameters1_1 = new OrderReplaceParameters() { order = orderRequest1 };
         try { cancelReplaceResponse = await Rest20.PutOrderReplaceAsync(AccountID, order1.id, orderReplaceParameters1_1); }
         catch (Exception ex)
         {
            var errorResponse = ErrorResponseFactory.Create(ex.Message) as OrderReplaceErrorResponse;
            m_Results.Verify("11.E2", errorResponse != null, $"Error response has correct type: {nameof(OrderReplaceErrorResponse)}");
         }

         // Cancel & Replace an existing order
         orderRequest1.price = price;
         orderRequest1.units += 10;
         var orderReplaceParameters1_2 = new OrderReplaceParameters() { order = orderRequest1 };
         cancelReplaceResponse = await Rest20.PutOrderReplaceAsync(AccountID, order1.id, orderReplaceParameters1_2);
         var cancelTransaction = cancelReplaceResponse.orderCancelTransaction;
         var newOrderTransaction = cancelReplaceResponse.orderCreateTransaction;

         m_Results.Verify("11.14",
           cancelTransaction != null && cancelTransaction.orderID == order1.id,
           "Order ancel+replace cancelled successfully.");

         m_Results.Verify("11.15",
           newOrderTransaction != null && newOrderTransaction.id > 0 && newOrderTransaction.id != order1.id,
           "Order cancel+replace replaced successfully.");

         // Get new order details
         var newOrder = await Rest20.GetOrderAsync(AccountID, newOrderTransaction.id) as MarketIfTouchedOrder;
         m_Results.Verify("11.16", newOrder != null && newOrder.units == orderRequest1.units, "New order details are correct.");

         // error test - cancel an order
         OrderCancelResponse cancelOrderResponse = null;
         try { cancelOrderResponse = await Rest20.PutOrderCancelAsync(AccountID, -1); }
         catch (Exception ex)
         {
            var errorResponse = ErrorResponseFactory.Create(ex.Message) as OrderCancelErrorResponse;
            m_Results.Verify("11.E3", errorResponse != null, $"Error response has correct type: {nameof(OrderCancelErrorResponse)}");
         }

         // Cancel an order
         cancelOrderResponse = await Rest20.PutOrderCancelAsync(AccountID, newOrder.id);
         m_Results.Verify("11.17", cancelOrderResponse != null, "Cancelled order retrieved successfully.");
         var cancelTransaction2 = cancelOrderResponse.orderCancelTransaction;
         m_Results.Verify("11.18", cancelTransaction2.orderID == newOrder.id, "Order cancelled successfully.");
         #endregion

         #region Create new pending order with exit orders

         // need this for rounding, etc.
         var owxInstrument = GetOandaInstrument(m_TestInstrument);

         decimal owxOrderPrice = Math.Round(GetOandaPrice(m_TestInstrument), owxInstrument.displayPrecision);
         decimal owxStopLossPrice = Math.Round(owxOrderPrice - ((decimal)0.10 * owxOrderPrice), owxInstrument.displayPrecision);
         decimal owxTakeProfitPrice = Math.Round(owxOrderPrice + ((decimal)0.10 * owxOrderPrice), owxInstrument.displayPrecision);

         var owxRequest = new LimitOrderRequest(owxInstrument)
         {
            units = 1, // buy
            timeInForce = TimeInForce.GoodUntilDate,
            gtdTime = expiry,
            price = owxOrderPrice,
            priceBound = owxOrderPrice + ((decimal)0.01 * owxOrderPrice),
            stopLossOnFill = new StopLossDetails(owxInstrument)
            {
               timeInForce = TimeInForce.GoodUntilCancelled,
               price = owxStopLossPrice
            },
            takeProfitOnFill = new TakeProfitDetails(owxInstrument)
            {
               timeInForce = TimeInForce.GoodUntilCancelled,
               price = owxTakeProfitPrice
            }
         };

         // create order with exit orders
         var postOrderParameters = new PostOrderParameters() { order = owxRequest };
         var owxResponse = await Rest20.PostOrderAsync(AccountID, postOrderParameters);
         var owxOrderTransaction = owxResponse.orderCreateTransaction;
         m_Results.Verify("11.19", owxOrderTransaction != null && owxOrderTransaction.id > 0, "Order with exit orders successfully opened");
         m_Results.Verify("11.20", owxOrderTransaction.type == TransactionType.LimitOrder, "Order with exit orders type is correct.");

         // get order with exit orders details
         var pendingOrders2 = await Rest20.GetPendingOrdersAsync(AccountID);
         var order2 = await Rest20.GetOrderAsync(AccountID, pendingOrders2[0].id) as LimitOrder;
         m_Results.Verify("11.21",
           order2 != null && order2.stopLossOnFill.price == owxStopLossPrice,
           "Order with exit orders stoploss details are correct.");
         m_Results.Verify("11.22",
           order2 != null && order2.takeProfitOnFill.price == owxTakeProfitPrice,
           "Order with exit orders takeprofit details are correct.");

         // cancel & replace order with exit orders - update stoploss and takeprofit
         owxStopLossPrice = Math.Round(owxOrderPrice - ((decimal)0.15 * owxOrderPrice), owxInstrument.displayPrecision);
         owxRequest.stopLossOnFill = new StopLossDetails(owxInstrument) { price = owxStopLossPrice };

         owxTakeProfitPrice = Math.Round(owxOrderPrice + ((decimal)0.15 * owxOrderPrice), owxInstrument.displayPrecision);
         owxRequest.takeProfitOnFill = new TakeProfitDetails(owxInstrument) { price = owxTakeProfitPrice };

         var orderReplaceParameters = new OrderReplaceParameters() { order = owxRequest };
         var owxReplaceResponse = await Rest20.PutOrderReplaceAsync(AccountID, order2.id, orderReplaceParameters);
         var owxCancelTransaction = owxReplaceResponse.orderCancelTransaction;
         var owxNewOrderTransaction = owxReplaceResponse.orderCreateTransaction;

         m_Results.Verify("11.23",
           owxCancelTransaction != null && owxCancelTransaction.orderID == order2.id,
           "Order with exit orders cancel+replace cancelled successfully.");
         m_Results.Verify("11.24",
           owxCancelTransaction != null && owxCancelTransaction.reason == OrderCancelReason.ClientRequestReplaced,
           "Order with exit orders cancel+replace has correct reason.");
         m_Results.Verify("11.25",
           owxNewOrderTransaction != null && owxNewOrderTransaction.id > 0 && owxNewOrderTransaction.id == owxCancelTransaction.replacedByOrderID,
           "Order with exit orders cancel+replace replaced successfully.");

         // Get order with exit orders replacement order details
         var owxOrderTransaction2 = owxNewOrderTransaction as LimitOrderTransaction;
         m_Results.Verify("11.26",
           owxOrderTransaction2 != null && owxOrderTransaction2.reason == LimitOrderReason.Replacement,
           "Order with exit orders replacement order reason is correct.");
         m_Results.Verify("11.27",
           owxOrderTransaction2 != null && owxOrderTransaction2.stopLossOnFill.price == owxStopLossPrice,
           "Order with exit orders replacement order stoploss is correct.");
         m_Results.Verify("11.28",
           owxOrderTransaction2 != null && owxOrderTransaction2.takeProfitOnFill.price == owxTakeProfitPrice,
           "Order with exit orders replacement order takeprofit is correct.");
         #endregion
      }

      /// <summary>
      /// Places a market order.
      /// </summary>
      /// <param name="key">The key root used to store the order success and order fill results.</param>
      /// <returns></returns>
      private static async Task PlaceMarketOrder(string key, decimal units = 0, bool closeAllTrades = true)
      {
         // I'm fine with a throw here
         // To each his/her own on doing something different.
         if (await Utilities.IsMarketHaltedAsync())
            throw new MarketHaltedException("OANDA Fx market is halted!");

         if (closeAllTrades)
         {
            var closeList = await Rest20.GetOpenTradesAsync(AccountID);
            closeList.ForEach(async x => await Rest20.PutTradeCloseAsync(AccountID, x.id));
         }

         // this should have a value by now
         var instrument = GetOandaInstrument(m_TestInstrument);
         if (units == 0) units = instrument.minimumTradeSize;

         var orderRequest = new MarketOrderRequest(GetOandaInstrument())
         {
            units = units, // buy
            clientExtensions = new ClientExtensions()
            {
               id = "test_market_order_1",
               comment = "test market order comment",
               tag = "test_market_order"
            },
            tradeClientExtensions = new ClientExtensions()
            {
               id = "test_market_trade_1",
               comment = "test market trade comment",
               tag = "test_market_trade"
            }
         };

         var parameters = new PostOrderParameters() { order = orderRequest };
         var response = await Rest20.PostOrderAsync(AccountID, parameters);
         var createTransaction = response.orderCreateTransaction;
         var fillTransaction = response.orderFillTransaction;

         m_Results.Verify(key + ".0", createTransaction != null && createTransaction.id > 0, "Market order successfully placed.");
         m_Results.Verify(key + ".1", fillTransaction != null && fillTransaction.id > 0, "Market order successfully filled.");
      }
      #endregion

      #region Trade
      /// <summary>
      /// Runs operations available at OANDA's Trade endpoint
      /// </summary>
      /// <returns></returns>
      private static async Task Trade_RunTradeOperationsAsync()
      {
         await PlaceMarketOrder("13");

         // get list of trades
         var trades = await Rest20.GetTradesAsync(AccountID);
         m_Results.Verify("13.2", trades.Count > 0 && trades[0].id > 0, "Trades list retrieved.");

         // get list of open trades
         var openTrades = await Rest20.GetTradesAsync(AccountID);
         m_Results.Verify("13.3", openTrades.Count > 0 && openTrades[0].id > 0, "Open trades list retrieved.");

         if (openTrades.Count == 0)
            throw new InvalidOperationException("Trade test operations cannot continue without an open trade.");

         // get details for a trade
         var trade = await Rest20.GetTradeAsync(AccountID, openTrades[0].id);
         m_Results.Verify("13.4", trade.id > 0 && trade.price > 0 && trade.initialUnits != 0, "Trade details retrieved");

         // create extensions
         var updatedExtensions = trade.clientExtensions;
         updatedExtensions.comment = "updated test market trade comment";

         // error test - update extensions
         TradeClientExtensionsResponse extensionsModifyResponse = null;
         var clientExtensionsParameters = new TradeClientExtensionsParameters() { clientExtensions = updatedExtensions };
         try { extensionsModifyResponse = await Rest20.PutTradeClientExtensionsAsync(AccountID, -1, clientExtensionsParameters); }
         catch (Exception ex)
         {
            var errorResponse = ErrorResponseFactory.Create(ex.Message) as TradeClientExtensionsErrorResponse;
            m_Results.Verify("13.E0", errorResponse != null, $"Error response has correct type: {nameof(TradeClientExtensionsErrorResponse)}");
         }

         // update extensions
         extensionsModifyResponse = await Rest20.PutTradeClientExtensionsAsync(AccountID, trade.id, clientExtensionsParameters);

         m_Results.Verify("13.5", extensionsModifyResponse != null, "Order extensions update received successfully.");
         m_Results.Verify("13.6", extensionsModifyResponse.tradeClientExtensionsModifyTransaction.tradeID == trade.id, "Correct trade extensions updated.");
         m_Results.Verify("13.7", extensionsModifyResponse.tradeClientExtensionsModifyTransaction.tradeClientExtensionsModify.comment == "updated test market trade comment", "Trade extensions comment updated successfully.");

         // need this for rounding, etc.
         var instrument = GetOandaInstrument(trade.instrument);

         // create takeProfit 
         decimal takeProfitPrice = Math.Round((decimal)1.10 * trade.price, instrument.displayPrecision);
         var takeProfit = new TakeProfitDetails(instrument)
         {
            price = takeProfitPrice,
            timeInForce = TimeInForce.GoodUntilCancelled,
            clientExtensions = new ClientExtensions()
            {
               id = "take_profit_1",
               comment = "take profit comment",
               tag = "take_profit"
            }
         };
         //var patch1 = new PatchExitOrdersRequest() { takeProfit = takeProfit };
         var patch1 = new TradeOrdersParameters();
         patch1.SetTakeProfit(TradeOrdersAction.Create, takeProfit);

         // error test - patch open trade
         TradeOrdersResponse response = null;
         patch1.takeProfit.price = -1;
         try { response = await Rest20.PutTradeOrdersAsync(AccountID, trade.id, patch1); }
         catch (Exception ex)
         {
            var errorResponse = ErrorResponseFactory.Create(ex.Message) as TradeOrdersErrorResponse;
            m_Results.Verify("13.E1", errorResponse != null, $"Error response has correct type: {nameof(TradeOrdersErrorResponse)}");
         }

         // add a takeProft to an open trade
         patch1.takeProfit.price = takeProfitPrice;
         var takeProfitPatch = (await Rest20.PutTradeOrdersAsync(AccountID, trade.id, patch1)).takeProfitOrderTransaction;
         m_Results.Verify("13.8", takeProfitPatch != null && takeProfitPatch.id > 0, "Take profit patch received.");
         m_Results.Verify("13.9", takeProfitPatch.price == takeProfitPrice, "Trade patched with take profit.");

         // Add a stopLoss to an open trade
         decimal stopLossPrice = Math.Round(trade.price - ((decimal)0.10 * trade.price), instrument.displayPrecision);
         var stopLoss = new StopLossDetails(instrument)
         {
            price = stopLossPrice,
            timeInForce = TimeInForce.GoodUntilCancelled,
            clientExtensions = new ClientExtensions()
            {
               id = "stop_loss_1",
               comment = "stop loss comment",
               tag = "stop_loss"
            }
         };
         var patch2 = new TradeOrdersParameters();
         patch2.SetStopLoss(TradeOrdersAction.Create, stopLoss);
         var stopLossPatch = (await Rest20.PutTradeOrdersAsync(AccountID, trade.id, patch2)).stopLossOrderTransaction;
         m_Results.Verify("13.10", takeProfitPatch != null && takeProfitPatch.id > 0, "Stop loss patch received.");
         m_Results.Verify("13.11", stopLossPatch.price == stopLossPrice, "Trade patched with stop loss.");

         // Add a trailingStopLoss to an open trade
         decimal distance = 2 * (trade.price - stopLoss.price.Value);
         var trailingStopLoss = new TrailingStopLossDetails(instrument)
         {
            distance = distance,
            timeInForce = TimeInForce.GoodUntilCancelled,
            clientExtensions = new ClientExtensions()
            {
               id = "trailing_stop_loss_1",
               comment = "trailing stop loss comment",
               tag = "trailing_stop_loss"
            }
         };

         var patch3 = new TradeOrdersParameters();

         // stopLoss and trailingStopLoss cannot exist simultaneously
         // thus the stopLoss must be cancelled whilst creating the trailingStopLoss
         patch3.SetStopLoss(TradeOrdersAction.Cancel, null);
         patch3.SetTrailingStopLoss(TradeOrdersAction.Create, trailingStopLoss);

         var trailingStopLossPatch = (await Rest20.PutTradeOrdersAsync(AccountID, trade.id, patch3));
         var trailingStopLossOrderTransaction = trailingStopLossPatch.trailingStopLossOrderTransaction;
         m_Results.Verify("13.12", trailingStopLossPatch.stopLossOrderCancelTransaction != null, "Stop loss cancelled.");
         m_Results.Verify("13.13", trailingStopLossOrderTransaction != null && trailingStopLossOrderTransaction.id > 0, "Trailing stop loss patch created.");
         m_Results.Verify("13.14", trailingStopLossOrderTransaction.distance == distance, "Trade patched with correct trailing stop loss.");

         // remove dependent orders
         var tradeOrdersParameters = new TradeOrdersParameters();
         tradeOrdersParameters.SetTakeProfit(TradeOrdersAction.Cancel, null);
         tradeOrdersParameters.SetTrailingStopLoss(TradeOrdersAction.Cancel, null);

         var result = await Rest20.PutTradeOrdersAsync(AccountID, trade.id, tradeOrdersParameters);
         m_Results.Verify("13.15", result.takeProfitOrderCancelTransaction != null, "Take profit cancelled.");
         //m_Results.Verify("13.15", result.stopLossOrderCancelTransaction != null, "Stop loss cancelled.");
         m_Results.Verify("13.16", result.trailingStopLossOrderCancelTransaction != null, "Trailing stop loss cancelled.");

         if (await Utilities.IsMarketHaltedAsync())
            throw new MarketHaltedException("OANDA Fx market is halted!");

         // error test - close open trade
         TradeCloseResponse tradeCloseResponse = null;
         var tradeCloseParameters = new TradeCloseParameters();
         try { tradeCloseResponse = await Rest20.PutTradeCloseAsync(AccountID, -1, tradeCloseParameters); }
         catch (Exception ex)
         {
            var errorResponse = ErrorResponseFactory.Create(ex.Message) as TradeCloseErrorResponse;
            m_Results.Verify("13.E2", errorResponse != null, $"Error response has correct type: {nameof(TradeCloseErrorResponse)}");
         }

         // close an open trade
         var closedDetails = (await Rest20.PutTradeCloseAsync(AccountID, trade.id, tradeCloseParameters)).orderFillTransaction;
         m_Results.Verify("13.17", closedDetails.id > 0, "Trade closed");
         m_Results.Verify("13.18", closedDetails.time != DateTime.MinValue, "Trade close details has time.");
         m_Results.Verify("13.19", !string.IsNullOrEmpty(closedDetails.instrument), "Trade close details instrument correct.");
         m_Results.Verify("13.20", closedDetails.units == -1 * trade.initialUnits, "Trade close details units correct.");
         m_Results.Verify("13.21", closedDetails.tradesClosed[0].price > 0, "Trade close details has price.");
      }
      #endregion

      #region Position
      /// <summary>
      /// Runs operations available at OANDA's Position endpoint
      /// </summary>
      /// <returns></returns>
      private static async Task Position_RunPositionOperationsAsync()
      {
         if (await Utilities.IsMarketHaltedAsync())
            throw new MarketHaltedException("OANDA Fx market is halted!");

         short units = 1;

         // make sure there's a position to test
         await PlaceMarketOrder("14", units);

         // verification function
         Func<Position, short, short> verifyPosition = (position, i) =>
         {
            string key = "14.";

            m_Results.Verify(key + i.ToString(), position.@long != null, "Position has direction");
            m_Results.Verify(key + (i + 1).ToString(), position.@long.units > 0, "Position has units");
            m_Results.Verify(key + (i + 2).ToString(), position.@long.averagePrice > 0, "Position has avgPrice");
            m_Results.Verify(key + (i + 3).ToString(), !string.IsNullOrEmpty(position.instrument), "Position has instrument");

            return i += 4;
         };

         // get list of historical positions
         // only open positions will have units > 0
         var positions = await Rest20.GetPositionsAsync(AccountID);
         m_Results.Verify("14.2", positions.Count > 0, "All historical positions retrieved");

         // get list of open positions
         var openPositions = await Rest20.GetOpenPositionsAsync(AccountID);
         m_Results.Verify("14.3", openPositions.Count > 0, "Open positions retrieved");

         short increment = 4;
         foreach (var position in openPositions)
         {
            increment = verifyPosition(position, increment);
         }

         // get position for a given instrument
         var onePosition = await Rest20.GetPositionAsync(AccountID, m_TestInstrument);
         increment = verifyPosition(onePosition, increment);

         // error test - close open position
         var parameters = new PositionCloseParameters() { longUnits = "FAKE" };
         PositionCloseResponse response = null;
         try { response = await Rest20.PutPositionCloseAsync(AccountID, m_TestInstrument, parameters); }
         catch (Exception ex)
         {
            var errorResponse = ErrorResponseFactory.Create(ex.Message) as PositionCloseErrorResponse;
            m_Results.Verify("14.E0", errorResponse != null, $"Error response has correct type: {nameof(PositionCloseErrorResponse)}");
         }

         // closeout open position 
         parameters.longUnits = "ALL";
         response = await Rest20.PutPositionCloseAsync(AccountID, m_TestInstrument, parameters);
         m_LastTransactionID = response.lastTransactionID;
         m_Results.Verify("14." + increment.ToString(), response.longOrderCreateTransaction != null && response.longOrderCreateTransaction.id > 0,
           "Position close order created.");
         m_Results.Verify("14." + (increment + 1).ToString(), response.longOrderFillTransaction != null && response.longOrderFillTransaction.id > 0,
           "Position close fill order created.");
         m_Results.Verify("14." + (increment + 2).ToString(), response.longOrderFillTransaction.units == -1 * units,
           "Position close units correct.");

      }
      #endregion

      #region Transaction
      private static async Task Transaction_GetTransactionsByDateRangeAsync()
      {
         // 09
         var attemptsRemaining = 3;
         var transactionType = TransactionType.OrderFill;
         var parameters = new TransactionsParameters()
         {
            from = m_LastTransactionTime,
            type = new List<string>(new[] { transactionType })
         };

         List<ITransaction> results = null;

         while (attemptsRemaining >= 0)
         {
            attemptsRemaining--;
            results = await Rest20.GetTransactionsAsync(AccountID, parameters);

            if (results?.Count > 0) break;

            await Task.Delay(250);
         }


         m_Results.Verify("09.0",
           results != null, $"Transactions info received.");
         m_Results.Verify("09.1",
           results.Where(x => x.type == transactionType).Count() > 0, $"{transactionType} transactions returned.");
         m_Results.Verify("09.2",
           results.Where(x => x.type != transactionType).Count() > 0, $"Non {transactionType} transactions returned.");
      }

      private static async Task Transaction_GetTransactionAsync()
      {
         //15
         ITransaction result = await Rest20.GetTransactionAsync(AccountID, m_LastTransactionID);

         m_Results.Verify("15.0", result != null, "Transaction info received.");
         m_Results.Verify("15.1", result.id > 0, "Transaction has id.");
         m_Results.Verify("15.2", result.type != null, "Transaction has type.");
         m_Results.Verify("15.3", result.time != null, "Transaction has time.");
      }

      private static async Task Transaction_GetTransactionsByIdRangeAsync()
      {
         // 16
         var parameters = new TransactionsByIdRangeParameters()
         {
            from = m_FirstTransactionID,
            to = m_LastTransactionID
         };

         List<ITransaction> results = await Rest20.GetTransactionsByIdRangeAsync(AccountID, parameters);
         results.OrderBy(x => x.id);

         m_Results.Verify("16.0", results != null, $"Transactions info received.");
         m_Results.Verify("16.1", results.First().id == m_FirstTransactionID, $"Id of first transaction is correct.");
         m_Results.Verify("16.2", results.Where(x => x.id < m_FirstTransactionID).Count() == 0, "All Id's are greater than first transactionId.");
         m_Results.Verify("16.3", results.Where(x => x.type == TransactionType.ClientConfigure).Count() > 0, "Client configure transactions returned.");
         m_Results.Verify("16.4", results.Where(x => x.type == TransactionType.MarketOrder).Count() > 0, "Market order transactions returned.");
      }

      private static async Task Transaction_GetTransactionsSinceIdAsync()
      {
         // 10
         var parameters = new TransactionsSinceIdParameters() { id = m_LastTransactionID };
         List<ITransaction> results = await Rest20.GetTransactionsSinceIdAsync(AccountID, parameters);
         results.OrderBy(x => x.id);

         m_Results.Verify("10.0", results != null, "Transactions info received.");
         m_Results.Verify("10.1", results.First().id == m_LastTransactionID + 1, $"Id of first transaction is correct.");
         m_Results.Verify("10.2", results.Where(x => x.id <= m_LastTransactionID).Count() == 0, "All Id's are greater than last transactionId.");
         m_Results.Verify("10.3", results.Where(x => x.type == TransactionType.ClientConfigure).Count() > 0, "Client configure transactions returned.");
      }

      #endregion

      #region Pricing
      private static async Task Pricing_GetPricingAsync()
      {
         var parameters = new PricingParameters() { instruments = new List<string>() };
         m_OandaInstruments.ForEach(x => parameters.instruments.Add(x.name));

         List<Price> prices = await Rest20.GetPricingAsync(AccountID, parameters);

         bool allPricesHaveBidLiquidity = prices.All(x => x.bids.FirstOrDefault()?.liquidity > 0);
         bool allPricesHaveAskLiquidity = prices.All(x => x.asks.FirstOrDefault()?.liquidity > 0);

         m_Results.Verify("06.0", prices != null, "Prices retrieved successfully.");
         m_Results.Verify("06.1", prices.Count == m_OandaInstruments.Count, $"Correct count ({prices.Count}) of prices retrieved.");
         m_Results.Verify("06.2", allPricesHaveBidLiquidity, $"All prices have bid liquidity.");
         m_Results.Verify("06.3", allPricesHaveAskLiquidity, $"All prices have ask liquidity.");

         m_OandaPrices = prices;
      }
      #endregion

      #region Stream
      static Semaphore _transactionThreads;
      protected static Task Stream_GetStreamingTransactionsAsync()
      {
         // 07
         var session = new TransactionsSession(AccountID);
         session.DataReceived += OnTransactionReceived;

         // in this unit test only 1 thread will transit this section
         // so a maximumCount of 10 threads is clearly more than adequate
         _transactionThreads = new Semaphore(0, 100);

         // this is not awaited because StartSessionAsync only completes if
         // 1) StopSession() is called
         // 2) an exception occurs while processing the pricing stream
         session.StartSessionAsync();

         // block the thread here until it released by calling Release()
         // the block is released automatically after 10secs if Release() is not called
         // doing this to allow up to 10secs for a price to be received from the stream
         bool success = _transactionThreads.WaitOne(10000);
         m_Results.Verify("07.0", success, "Transaction events stream is functioning.");

         return Task.Run(() =>
         {
            // wait until a transaction is received .. max 20secs
            var autoStopTime = DateTime.UtcNow.AddSeconds(20);
            do
            {
               Task.Delay(millisecondsDelay: 250);
            }
            while (!_gotTransaction || DateTime.UtcNow < autoStopTime);

            // this will set _shutdown to true in StreamSession.cs
            // once _shutdown = true, the pricing stream is stopped and disposed
            session.StopSession();
         });
      }

      static bool _gotTransaction = false;
      protected static void OnTransactionReceived(TransactionsStreamResponse data)
      {
         if (!_gotTransaction && !data.IsHeartbeat())
         {
            // only testing first data message

            m_Results.Verify("07.1", data.transaction != null, "Transaction received");
            if (data.transaction != null)
            {
               m_Results.Verify("07.2", data.transaction.id != 0, "Transaction has id.");
               m_Results.Verify("07.3", data.transaction.accountID == AccountID, $"Transaction has correct accountID: ({AccountID}).");
            }

            _gotTransaction = true;
         }

         _transactionThreads.Release();
      }

      static Semaphore _pricingThreads;
      protected static Task Stream_GetStreamingPricesAsync()
      {
         var session = new PricingSession(AccountID, m_OandaInstruments);
         session.DataReceived += OnPricingReceived;

         // in this unit test only 1 thread will transit this section
         // so a maximumCount of 10 threads is clearly more than adequate
         _pricingThreads = new Semaphore(initialCount: 0, maximumCount: 100);

         // this is not awaited because StartSessionAsync only completes if
         // 1) StopSession() is called
         // 2) an exception occurs while processing the pricing stream
         session.StartSessionAsync();

         // block the thread here until it released by calling Release()
         // the block is released automatically after 10secs if Release() is not called
         // doing this to allow up to 10secs for a price to be received from the stream
         bool success = _pricingThreads.WaitOne(10000);

         // this will set _shutdown to true in StreamSession.cs
         // once _shutdown = true, the pricing stream is stopped and disposed
         session.StopSession();

         m_Results.Verify("18.0", success, "Pricing stream is functioning.");
         return Task.CompletedTask;
      }

      static bool _gotPrice = false;
      protected static void OnPricingReceived(PricingStreamResponse data)
      {
         if (!_gotPrice)
         {
            if (data.price != null)
            {
               m_Results.Verify("18.1", data.price != null, "Pricing data received.");
               m_Results.Verify("18.2", data.price.instrument != null, "Streaming price has instrument");

               if (data.price.tradeable)
               {
                  m_Results.Verify("18.3", data.price.bids.Count > 0, "Streaming price has bids");
                  m_Results.Verify("18.4", data.price.asks.Count > 0, "Streaming price has asks");
               }

               _gotPrice = true;
            }
         }
         _pricingThreads.Release();
      }
      #endregion

      #region Utilities

      /// <summary>
      /// Reads the api key from a supplied file name
      /// </summary>
      /// <returns></returns>
      private static async Task SetApiCredentialsAsync(string fileName = null)
      {
         fileName = fileName ?? @"C:\Users\mrchr\source\assets\OANDAV20\oandaPracticeApiCredentials.txt";

         try
         {
            using (StreamReader sr = new StreamReader(fileName))
            {
               string apiCredentials = await sr.ReadToEndAsync();

               // an OANDA trade or practice account is required to generate a valid token
               // for info, go to: https://www.oanda.com/account/tpa/personal_token
               m_TestToken = apiCredentials.Split('|')[0];

               // this should be a v20 account, not a standard/legacy account
               // if null, this will be set to the first v20 account found using the token above
               m_TestAccount = apiCredentials.Split('|')[1];

               m_TokenAccounts = Convert.ToInt16(apiCredentials.Split('|')[2]);
            }
         }
         catch (DirectoryNotFoundException)
         {
            // directory not found
         }
         catch (FileNotFoundException)
         {
            // file not found
         }
         catch (Exception ex)
         {
            throw new Exception("Could not read api credentials.", ex);
         }

         Credentials.SetCredentials(m_TestEnvironment, m_TestToken, m_TestAccount);
      }

      private static Instrument GetOandaInstrument(string instrument = null)
      {
         instrument = instrument ?? m_TestInstrument;
         return m_OandaInstruments.FirstOrDefault(x => x.name == instrument);
      }

      private static decimal GetOandaPrice(string instrument = null)
      {
         instrument = instrument ?? m_TestInstrument;
         return m_OandaPrices.FirstOrDefault(x => x.instrument == instrument).closeoutBid;
      }

      /// <summary>
      /// Computes and returns the next test characteristic (whole number)
      /// </summary>
      /// <returns></returns>
      private static string GetTestNumber(bool incrementCharacteristic = false)
      {
         if (incrementCharacteristic)
            m_TestNumber = Math.Truncate(m_TestNumber) + 1;
         else
            m_TestNumber = m_TestNumber + (decimal).001;

         return m_TestNumber.ToString("0.000");
      }

      #endregion
   }
}
