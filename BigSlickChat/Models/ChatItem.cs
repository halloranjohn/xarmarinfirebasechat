using System;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace BigSlickChat 
{
	public class ChatItem
	{	
		//[JsonProperty("message")]	
		//public string message;

		//[JsonProperty("date")]
		//public string date;

		public ChatItem(string message, string date)
		{
			Message = message;
			Date = date;
		}

		public string Message { private set; get; }

		public string Date { private set; get; }

	}
}


