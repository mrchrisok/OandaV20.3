using OkonkwoOandaV20.TradeLibrary.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OkonkwoOandaV20.Framework.Factories
{
   public class OrderFactory
   {
	  public static List<IOrder> Create(IEnumerable<IOrder> data)
	  {
		 var orders = new List<IOrder>();

		 foreach (IOrder order in data)
		 {
			orders.Add(Create(order.type));
		 }

		 return orders;
	  }

	  private static readonly FieldInfo[] _orderTypeFields =
		 typeof(OrderType).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

	  private static readonly IEnumerable<Type> _orderTypes =
		 typeof(IOrder).Assembly.GetTypes().Where(type => type.GetInterfaces().Contains(typeof(IOrder)));

	  /// <summary>
	  /// Constructs an instance of an IOrder that matches the supplied type name.
	  /// </summary>
	  /// <param name="typeName">The type of the IOrder to construct.</param>
	  /// <returns>An IOrder object or null.</returns>
	  public static IOrder Create(string typeName)
	  {
		 var thisOrderTypeField = _orderTypeFields.FirstOrDefault(field => field.GetRawConstantValue().ToString() == typeName);
		 var transactionTypeName = $"{thisOrderTypeField.Name}Order";
		 var transactionType = _orderTypes.FirstOrDefault(type => type.Name == transactionTypeName);
		 var typeInstance = Activator.CreateInstance(transactionType ?? typeof(Order));

		 return typeInstance as IOrder;
	  }
   }
}
