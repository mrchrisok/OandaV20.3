using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OkonkwoOandaV20.TradeLibrary.REST.Streaming
{
   public abstract class StreamSession<T> where T : IStreamResponse
   {
	  protected readonly string _accountID;
	  protected WebSession _session;
	  protected bool _shutdown;

	  public delegate void DataHandler(T data);
	  public event DataHandler DataReceived;
	  public void OnDataReceived(T data)
	  {
		 DataReceived?.Invoke(data);
	  }

	  public delegate void SessionStatusHandler(string accountID, bool started, Exception e);
	  public event SessionStatusHandler SessionStatusChanged;
	  public void OnSessionStatusChanged(bool started, Exception e)
	  {
		 SessionStatusChanged?.Invoke(_accountID, started, e);
	  }

	  protected StreamSession(string accountID)
	  {
		 _accountID = accountID;
	  }

	  protected abstract Task<WebSession> GetSessionAsync();

	  public virtual async Task StartSessionAsync()
	  {
		 _shutdown = false;
		 _session = await GetSessionAsync();

		 await Task.Run(() =>
		 {
			try
			{
			   using (_session.WebResponse)
			   {
				  var stream = Rest20.GetResponseStream(_session.WebResponse);
				  var reader = new StreamReader(stream);

				  while (!_shutdown)
				  {
					 string line = reader.ReadLine();
					 var data = JsonConvert.DeserializeObject<T>(line, _session.JsonSerializerSettings);

					 OnSessionStatusChanged(!_shutdown, null);

					 OnDataReceived(data);
				  }
			   }
			}
			catch (Exception e)
			{
			   _shutdown = true;
			   throw e;
			}
			finally
			{
			   _session = null;
			}
		 });
	  }

	  public void StopSession()
	  {
		 _shutdown = true;
	  }

	  public bool Stopped()
	  {
		 return _shutdown;
	  }
   }
}
