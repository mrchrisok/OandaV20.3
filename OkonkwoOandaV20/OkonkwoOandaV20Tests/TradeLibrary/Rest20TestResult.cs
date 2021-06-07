using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OkonkwoOandaV20Tests.TradeLibrary
{
   public class Rest20TestResult
   {
	  public bool Success { get; set; }
	  public string Details { get; set; }
   }

   public class Rest20TestResults
   {
	  #region Declarations

	  string m_LastMessage;
	  Dictionary<string, Rest20TestResult> m_Results = new Dictionary<string, Rest20TestResult>();
	  Dictionary<string, string> m_MutableMessages = new Dictionary<string, string>();

	  #endregion

	  #region Public properties and methods

	  public ReadOnlyDictionary<string, Rest20TestResult> Items
	  {
		 get { return new ReadOnlyDictionary<string, Rest20TestResult>(m_Results); }
	  }

	  public ReadOnlyDictionary<string, string> Messages
	  {
		 get { return new ReadOnlyDictionary<string, string>(m_MutableMessages); }
	  }

	  public string LastMessage
	  {
		 get { return m_LastMessage; }
	  }

	  //------
	  public bool Verify(bool success, string testDescription)
	  {
		 return Verify(DateTime.UtcNow.ToString(), success, testDescription);
	  }

	  public bool Verify(string success, string testDescription)
	  {
		 return Verify(DateTime.UtcNow.ToString(), !string.IsNullOrEmpty(success), testDescription);
	  }

	  public bool Verify(string key, string success, string testDescription)
	  {
		 return Verify(key, !string.IsNullOrEmpty(success), testDescription);
	  }

	  public bool Verify(string key, bool success, string testDescription)
	  {
		 m_Results.Add(key, new Rest20TestResult { Success = success, Details = testDescription });
		 if (!success)
		 {
			Add(key + ": " + success + ": " + testDescription); // add message
		 }
		 return success;
	  }

	  //------
	  public void Add(string key, Rest20TestResult testResult)
	  {
		 m_Results.Add(key, testResult);
	  }

	  public void Add(string message)
	  {
		 m_LastMessage = message;
		 m_MutableMessages.Add(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + ':' + m_MutableMessages.Count, message);
	  }

	  #endregion
   }
}
