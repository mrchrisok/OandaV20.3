using OkonkwoOandaV20.Framework;
using OkonkwoOandaV20.Framework.Factories;
using OkonkwoOandaV20.TradeLibrary.REST;
using OkonkwoOandaV20.TradeLibrary.REST.OrderRequests;
using static OkonkwoOandaV20.TradeLibrary.REST.Rest20;

namespace OkonkwoOandaV20App
{
   class Program
   {
      static void Main(string[] args)
      {
         WriteNewLine("Hello trader! Welcome to OkonkwoOandaV20.2");

         SetApiCredentials();

         StartTransactionsStream();

         PutOnATrade().Wait();

         StopTransactionsStream();

         Console.ReadKey();
      }

      static string AccountID { get; set; }
      const string INSTRUMENT = InstrumentName.Currency.EURUSD;

      static void SetApiCredentials()
      {
         WriteNewLine("Setting your V20 credentials ...");

         AccountID = "101-001-1913854-002";
         var environment = EEnvironment.Practice;
         var token = "501570c62a3e1673b3c9545a346f095a-57b7c1786b7536742c625e39108f44f0";

         Credentials.SetCredentials(environment, token, AccountID);

         WriteNewLine("Nice! Credentials are set.");
      }

      #region trading
      static private async Task PutOnATrade()
      {
         WriteNewLine("Checking to see if EUR_USD is open for trading ...");

         // first, check the market status for EUR_USD
         // if it is tradeable, we'll try to make some money :)
         if (!(await Utilities.IsMarketHaltedAsync(INSTRUMENT)))
         {
            WriteNewLine("EUR_USD is open and rockin', so let's start trading!");

            long? tradeID = await PlaceMarketOrder();

            if (tradeID.HasValue)
            {
               // we have an open trade.
               // give it some time to make money :)
               await Task.Delay(10000);

               WriteNewLine("Okay, we've waited 10 seconds. Closing trade now ...");

               // now, let' close the trade and collect our profits! .. hopefully
               TradeCloseResponse closeResponse = null;
               try
               {
                  var parameters = new TradeCloseParameters() { units = "ALL" };
                  closeResponse = await Rest20.PutTradeCloseAsync(AccountID, tradeID.Value, parameters);
               }
               catch
               {
                  WriteNewLine("Oops. The trade can't be closed. Something went wrong. :(");
               }

               if (closeResponse != null)
               {
                  WriteNewLine("Nice! The trade is closed.");

                  var profit = closeResponse.orderFillTransaction.pl;
                  WriteNewLine($"Our profit was USD {profit}");

                  if (profit > 0)
                     WriteNewLine($"Nice work! You are an awesome trader.");
                  else
                  {
                     WriteNewLine($"Looks like you need to learn some money-making strategies. :(");
                     WriteNewLine($"Keep studying, learning, but most of all .. keep trading!!");
                  }
               }
            }
            else
            {
               WriteNewLine($"Looks like something went awry with the trade. you need to learn some money-making strategies. :(");
            }
         }
         else
         {
            WriteNewLine("Sorry, Oanda markets are closed or Euro market is not tradeable.");
            WriteNewLine("Try again another time.");
         }
      }

      static async Task<long?> PlaceMarketOrder(string side = "buy")
      {
         WriteNewLine("Creating a EUR_USD market BUY order ...");

         var instrumentsParameters = new AccountInstrumentsParameters() { instruments = new List<string>() { INSTRUMENT } };
         var oandaInstrument = (await Rest20.GetAccountInstrumentsAsync(AccountID, instrumentsParameters)).First();
         long orderUnits = side == "buy" ? 10 : -10;

         var orderRequest = new MarketOrderRequest(oandaInstrument)
         {
            units = orderUnits
         };

         PostOrderResponse response = null;
         try
         {
            var postOrderParameters = new PostOrderParameters() { order = orderRequest };
            response = await Rest20.PostOrderAsync(AccountID, postOrderParameters);
            WriteNewLine("Congrats! You've put on a trade! Let it run! :)");
         }
         catch (Exception ex)
         {
            var errorResponse = ErrorResponseFactory.Create(ex.Message);

            WriteNewLine("Oops. Order creation failed.");
            WriteNewLine($"The failure message is: {errorResponse.errorMessage}.");
            WriteNewLine("Try again later.");
         }

         return response?.orderFillTransaction?.tradeOpened?.tradeID;
      }
      #endregion

      #region transactions stream
      static Semaphore _transactionReceived;
      static TransactionsSession _transactionsSession;

      static void StartTransactionsStream()
      {
         WriteNewLine("Starting transactions stream ...");

         _transactionsSession = new TransactionsSession(AccountID);
         _transactionReceived = new Semaphore(0, 100);
         _transactionsSession.DataReceived += OnTransactionReceived;

         _transactionsSession.StartSessionAsync();

         bool success = _transactionReceived.WaitOne(10000);

         if (success)
            WriteNewLine("Good news!. Transactions stream is functioning.");
         else
            WriteNewLine("Bad news!. Transactions stream is not functioning.");
      }

      protected static void OnTransactionReceived(TransactionsStreamResponse data)
      {
         if (!data.IsHeartbeat())
            WriteNewLine($"V20 notification - New account transaction: {data.transaction.type}");

         _transactionReceived.Release();
      }

      static void StopTransactionsStream()
      {
         _transactionsSession.StopSession();
      }
      #endregion

      static void WriteNewLine(string message)
      {
         Console.WriteLine($"\n{message}");
      }
   }
}
