using System;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BigSlickChat 
{
	public class ChatItem : FirebaseItem
	{
		public string Message { private set; get; }

		public string Date { private set; get; }

		public static List<string> chatpaths = new List<string>()
		{
			"allMessages/",
			"allMessagesMore/",
		};

		public ChatItem(string message, string date)
		{
			Message = message;
			Date = date;

			Paths = chatpaths;
		}

		public override object GetFirebaseSaveData()
		{
			Dictionary<string, object> dict = new Dictionary<string, object>();

			dict.Add("Message", Message);
			dict.Add("Date", Date);

			return dict as object;
		}
	}
}


