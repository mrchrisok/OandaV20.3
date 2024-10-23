using Microsoft.VisualStudio.TestTools.UnitTesting;
using OkonkwoOandaV20.Framework;
using OkonkwoOandaV20.TradeLibrary.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OkonkwoOandaV20Tests.TradeLibrary.Transaction
{
   [TestClass]
   public class TransactionTests
   {
      [TestMethod]
      public void success_transaction_type_is_correct()
      {
         //arrange
         var transactionTypeFields = typeof(TransactionType).GetFields();
         var transactionTypes = typeof(ITransaction).Assembly.GetTypes()
           .Where(type => type.GetTypeInfo().ImplementedInterfaces.Contains(typeof(ITransaction)));
         var failedCreates = new List<string>();

         //act
         foreach (var type in transactionTypes)
         {
            if (type.Name == "Transaction")
            {
               continue;
            }
            if (type.IsAbstract)
            {
               continue;
            }

            var trimmedTypeName = type.Name.Replace("Transaction", "");
            var transactionTypeField = transactionTypeFields.FirstOrDefault(field => field.Name == trimmedTypeName);
            var transactionTypeName = transactionTypeField.GetRawConstantValue().ToString();

            var typeInstance = Activator.CreateInstance(type) as ITransaction;

            if (typeInstance.type != transactionTypeName)
            {
               failedCreates.Add(type.Name);
            }
         }
         var failedCreatesCsv = Utilities.ConvertListToDelimitedValues(failedCreates);

         //assert
         Assert.IsTrue(failedCreates.Count == 0, $"Create failed for types: '{failedCreatesCsv}'");
      }
   }
}
