using OkonkwoOandaV20.TradeLibrary.Transaction;

namespace OkonkwoOandaV20.TradeLibrary.REST.OrderRequests
{
   public interface IOrderRequest
   {
	  string type { get; set; }
	  ClientExtensions clientExtensions { get; set; }
   }
}
