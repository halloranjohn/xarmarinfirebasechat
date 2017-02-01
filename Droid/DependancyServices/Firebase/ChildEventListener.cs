using System;
using Android.Runtime;
using Firebase.Database;
using GoogleGson;
using Java.Util;
using Newtonsoft.Json;

namespace BigSlickChat.Droid
{
	public class ChildEventListener<T> : Java.Lang.Object,  IChildEventListener
	{
		public Action<T> action;

		public ChildEventListener(Action<T> action)
		{
			this.action = action;
		}

		void IChildEventListener.OnCancelled(DatabaseError error)
		{
			throw new NotImplementedException();
		}

		void IChildEventListener.OnChildAdded(DataSnapshot snapshot, string previousChildName)
		{
			HashMap dataHashMap = snapshot.Value.JavaCast<HashMap>();
			Gson gson = new GsonBuilder().Create();
			string chatItemDaataString = gson.ToJson(dataHashMap);

			// Try to deserialize :
			try
			{
				T chatItems = JsonConvert.DeserializeObject<T>(chatItemDaataString);
				action(chatItems);
			}
			catch
			{

			}
		}

		void IChildEventListener.OnChildChanged(DataSnapshot snapshot, string previousChildName)
		{
			throw new NotImplementedException();
		}

		void IChildEventListener.OnChildMoved(DataSnapshot snapshot, string previousChildName)
		{
			throw new NotImplementedException();
		}

		void IChildEventListener.OnChildRemoved(DataSnapshot snapshot)
		{
			throw new NotImplementedException();
		}
	}
}
