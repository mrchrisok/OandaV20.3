namespace OkonkwoOandaV20.TradeLibrary.REST.Streaming
{
   public class StreamResponse : IStreamResponse
   {
	  /// <summary>
	  /// Sent every 5 seconds to keep the stream alive
	  /// </summary>
	  public IHeartbeat heartbeat { get; set; }

	  /// <summary>
	  /// Determines if the stream response is a heartbeat
	  /// </summary>
	  /// <returns>True if a heartbeat, otherwise False.</returns>
	  public bool IsHeartbeat()
	  {
		 return heartbeat != null;
	  }
   }

   public interface IStreamResponse
   {
	  /// <summary>
	  /// Determines if the stream response is a heartbeat
	  /// </summary>
	  /// <returns>True if a heartbeat, otherwise False.</returns>
	  bool IsHeartbeat();
   }
}
