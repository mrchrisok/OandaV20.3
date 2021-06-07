using OkonkwoOandaV20.TradeLibrary.REST;
using OkonkwoOandaV20.TradeLibrary.Transaction;
using System;
using System.Linq;
using System.Reflection;

namespace OkonkwoOandaV20.TradeLibrary.Order
{
   public class Order : Response, IOrder
   {
	  /// <summary>
	  /// The type of the Order.
	  /// Valid values are specified in the OrderType class.
	  /// </summary>
	  public virtual string type
	  {
		 get
		 {
			var thisTypeName = this.GetType().Name.Replace("Order", "");
			var thisTransactionTypeField = _orderTypeFields.FirstOrDefault(constant => constant.Name == thisTypeName);
			var thisTransactionType = thisTransactionTypeField?.GetRawConstantValue().ToString() ?? string.Empty;
			return thisTransactionType;
		 }
	  }
	  private static readonly FieldInfo[] _orderTypeFields =
		 typeof(OrderType).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

	  /// <summary>
	  /// The Order’s identifier, unique within the Order’s Account.
	  /// </summary>
	  public long id { get; set; }

	  /// <summary>
	  /// The time when the Order was created.
	  /// </summary>
	  public DateTime createTime { get; set; }

	  /// <summary>
	  /// The current state of the Order.
	  /// Valid values are specified in the OrderState class.
	  /// </summary>
	  public string state { get; set; }

	  /// <summary>
	  /// The client extensions of the Order. Do not set, modify, or delete 
	  /// clientExtensions if your account is associated with MT4.
	  /// </summary>
	  public ClientExtensions clientExtensions { get; set; }
   }
}
