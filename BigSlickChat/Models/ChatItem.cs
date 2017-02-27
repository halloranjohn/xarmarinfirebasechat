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

	}
}


