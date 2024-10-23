using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace OkonkwoOandaV20Tests.TradeLibrary
{
   [TestClass]
   public class PricingTests : Rest20TestsBase
   {
      #region Pricing

      [TestMethod]
      public void success_get_prices_list()
      {
         var pricesReceived = Results.Items.FirstOrDefault(x => x.Key == "06.0").Value;
         var priceCountMatches = Results.Items.FirstOrDefault(x => x.Key == "06.1").Value;
         var allHaveBidLiquidity = Results.Items.FirstOrDefault(x => x.Key == "06.2").Value;
         var allHaveAskLiquidity = Results.Items.FirstOrDefault(x => x.Key == "06.3").Value;

         Assert.IsTrue(pricesReceived.Success, $"{pricesReceived.Success}: {pricesReceived.Details}");
         Assert.IsTrue(priceCountMatches.Success, $"{priceCountMatches.Success}: {priceCountMatches.Details}");
         Assert.IsTrue(allHaveBidLiquidity.Success, $"{allHaveBidLiquidity.Success}: {allHaveBidLiquidity.Details}");
         Assert.IsTrue(allHaveAskLiquidity.Success, $"{allHaveAskLiquidity.Success}: {allHaveAskLiquidity.Details}");
      }

      [TestMethod]
      public void success_pricing_stream_functional()
      {
         // 18
         var streamFunctional = Results.Items.FirstOrDefault(x => x.Key == "18.0").Value;
         var dataReceived = Results.Items.FirstOrDefault(x => x.Key == "18.1").Value;
         var dataHasInstrument = Results.Items.FirstOrDefault(x => x.Key == "18.2").Value;

         Assert.IsTrue(streamFunctional.Success, $"{streamFunctional.Success}: {streamFunctional.Details}");
         Assert.IsTrue(dataReceived.Success, $"{dataReceived.Success}: {dataReceived.Details}");
         Assert.IsTrue(dataHasInstrument.Success, $"{dataHasInstrument.Success}: {dataHasInstrument.Details}");

         // trap these
         // will throw if the instrument is not tradeable
         // the keys will not be present in _results
         try
         {
            var dataHasBids = Results.Items.FirstOrDefault(x => x.Key == "18.3").Value;
            var dataHasAsks = Results.Items.FirstOrDefault(x => x.Key == "18.4").Value;

            Assert.IsTrue(dataHasBids.Success, $"{dataHasBids.Success}: {dataHasBids.Details}");
            Assert.IsTrue(dataHasAsks.Success, $"{dataHasAsks.Success}: {dataHasAsks.Details}");
         }
         catch
         {

         }
      }

      #endregion
   }
}
