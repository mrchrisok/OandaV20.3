using OkonkwoOandaV20.TradeLibrary.Transaction;
using System;

namespace OkonkwoOandaV20.TradeLibrary.Order
{
   public interface IOrder
   {
	  string type { get; }
	  long id { get; set; }
	  DateTime createTime { get; set; }
	  string state { get; set; }
	  ClientExtensions clientExtensions { get; set; }
   }
}
