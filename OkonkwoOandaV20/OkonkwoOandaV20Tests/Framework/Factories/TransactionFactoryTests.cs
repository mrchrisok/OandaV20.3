using Microsoft.VisualStudio.TestTools.UnitTesting;
using OkonkwoOandaV20.Framework;
using OkonkwoOandaV20.Framework.Factories;
using OkonkwoOandaV20.TradeLibrary.Transaction;
using System.Collections.Generic;

namespace OkonkwoOandaV20Tests.Framework.Factories
{
   [TestClass]
   public class TransactionFactoryTests
   {
	  [TestMethod]
	  public void success_method_Create_returns_correct_transaction()
	  {
		 //arrange
		 var transactionTypeFields = typeof(TransactionType).GetFields();
		 var failedCreates = new List<string>();

		 //act
		 foreach (var typeField in transactionTypeFields)
		 {
			var typeName = typeField.GetRawConstantValue().ToString();
			var iTransaction = TransactionFactory.Create(typeName);

			var className = iTransaction.GetType().Name;

			if (className != $"{typeField.Name}Transaction")
			{
			   failedCreates.Add(typeName);
			}
			if (iTransaction.GetType() == typeof(Transaction))
			{
			   failedCreates.Add(typeName);
			}
		 }
		 var failedCreatesCsv = Utilities.ConvertListToDelimitedValues(failedCreates);

		 //assert
		 Assert.IsTrue(failedCreates.Count == 0, $"Create failed for types: '{failedCreatesCsv}'");
	  }
   }
}
