using System;
using System.Linq;
using System.Reflection;

namespace OkonkwoOandaV20.TradeLibrary.Transaction
{
   /// <summary>
   /// The base Transaction specification. Specifies properties that are common between all Transactions.
   /// </summary>
   public class Transaction : ITransaction
   {
	  /// <summary>
	  /// The Transaction’s Identifier.
	  /// </summary>
	  public long id { get; set; }

	  /// <summary>
	  /// The Type of the Transaction.
	  /// Valid values are specified in the TransactionType class
	  /// </summary>
	  public virtual string type
	  {
		 get
		 {
			var thisTypeName = this.GetType().Name.Replace("Transaction", "");
			var thisTransactionType = _transactionTypeConstants.FirstOrDefault(constant => constant.Name == thisTypeName);
			var thisTransactionTypeName = thisTransactionType?.GetRawConstantValue().ToString() ?? string.Empty;
			return thisTransactionTypeName;
		 }
	  }
	  private static readonly FieldInfo[] _transactionTypeConstants = typeof(TransactionType).GetFields();

	  /// <summary>
	  /// The date/time when the Transaction was created.
	  /// </summary>
	  public DateTime time { get; set; }

	  /// <summary>
	  /// The ID of the user that initiated the creation of the Transaction.
	  /// </summary>
	  public int userID { get; set; }

	  /// <summary>
	  /// The ID of the Account the Transaction was created for.
	  /// </summary>
	  public string accountID { get; set; }

	  /// <summary>
	  /// The ID of the “batch” that the Transaction belongs to. Transactions in
	  /// the same batch are applied to the Account simultaneously.
	  /// </summary>
	  public long batchID { get; set; }

	  /// <summary>
	  /// The Request ID of the request which generated the transaction.
	  /// </summary>
	  public string requestID { get; set; }
   }

   public interface ITransaction
   {
	  /// <summary>
	  /// The Transaction’s Identifier.
	  /// </summary>
	  long id { get; set; }

	  /// <summary>
	  /// The Type of the Transaction.
	  /// Valid values are specified in the TransactionType class
	  /// </summary>
	  string type { get; }

	  /// <summary>
	  /// The date/time when the Transaction was created.
	  /// </summary>
	  DateTime time { get; set; }

	  /// <summary>
	  /// The ID of the user that initiated the creation of the Transaction.
	  /// </summary>
	  int userID { get; set; }

	  /// <summary>
	  /// The ID of the Account the Transaction was created for.
	  /// </summary>
	  string accountID { get; set; }

	  /// <summary>
	  /// The ID of the “batch” that the Transaction belongs to. Transactions in
	  /// the same batch are applied to the Account simultaneously.
	  /// </summary>
	  long batchID { get; set; }

	  /// <summary>
	  /// The Request ID of the request which generated the transaction.
	  /// </summary>
	  string requestID { get; set; }
   }
}
