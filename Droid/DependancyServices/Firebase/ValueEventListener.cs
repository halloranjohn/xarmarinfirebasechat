using System;
using Android.Runtime;
using Firebase.Database;
using GoogleGson;
using Java.Util;
using Newtonsoft.Json;

namespace BigSlickChat.Droid
{
	public class ValueEventListener<T> : Java.Lang.Object, IValueEventListener
	{
		public Action<T> action;

		public ValueEventListener(Action<T> action)
		{
			this.action = action;
		}

		void IValueEventListener.OnCancelled(DatabaseError error)
		{
			//throw new NotImplementedException();
		}

		void IValueEventListener.OnDataChange(DataSnapshot snapshot)
		{
			if (snapshot.Exists() && snapshot.HasChildren)
			{
                if (action != null)
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
			}
            else if(action != null)
			{
				T item = default(T);
				action(item);
			}
		}
	}
}
