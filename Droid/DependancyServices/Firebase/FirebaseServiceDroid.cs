using System;
using System.Collections.Generic;
using Android.Runtime;
using Firebase.Database;
using GoogleGson;
using Java.Util;
using Newtonsoft.Json;
using Xamarin.Forms;

[assembly: Dependency(typeof(BigSlickChat.Droid.FirebaseServiceDroid))]
namespace BigSlickChat.Droid
{
	public class FirebaseServiceDroid : FirebaseService
	{
		Dictionary<string, DatabaseReference> DatabaseReferences;
		Dictionary<string, IValueEventListener> ValueEventListeners;

		public FirebaseServiceDroid()
		{
			DatabaseReferences = new Dictionary<string, DatabaseReference>();
			ValueEventListeners = new Dictionary<string, IValueEventListener>();
		}

		public void FirebaseObserveEventChildChanged<T>(string nodeKey, Action<T> action)
		{
			DatabaseReference dr = FirebaseDatabase.Instance.GetReference(nodeKey);
			ValueEventListener<T> listener = new ValueEventListener<T>(action);
			dr.AddValueEventListener(listener);

			DatabaseReferences.Add(nodeKey, dr);
			ValueEventListeners.Add(nodeKey, listener);
		}

		public void FirebaseObserveEventChildRemoved<T>(string nodeKey, Action<T> action)
		{
			DatabaseReference dr = FirebaseDatabase.Instance.GetReference(nodeKey);
			ValueEventListener<T> listener = new ValueEventListener<T>(action);
			dr.AddValueEventListener(listener);

			DatabaseReferences.Add(nodeKey, dr);
			ValueEventListeners.Add(nodeKey, listener);
		}

		public void DatabaseReferenceSetValue(string nodeKey, object obj)
		{
			DatabaseReference dr = FirebaseDatabase.Instance.GetReference(nodeKey);

			if (dr != null)
			{
				string objJsonString = JsonConvert.SerializeObject(obj);

				Gson gson = new GsonBuilder().SetPrettyPrinting().Create();
				HashMap dataHashMap = new HashMap();
				Java.Lang.Object jsonObj = gson.FromJson(objJsonString, dataHashMap.Class);
				dataHashMap = jsonObj.JavaCast<HashMap>();
				dr.Push().SetValue(dataHashMap);
			}
		}

		public void FirebaseRemoveObserveEventChildChanged<T>(string nodeKey, Action<T> action)
		{
			DatabaseReference dr = DatabaseReferences[nodeKey];

			if (dr != null)
			{
				dr.RemoveEventListener(ValueEventListeners[nodeKey]);
			}
		}

	}

}
