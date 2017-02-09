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
	public class FirebaseServiceDroid : FirebaseDatabaseService
	{
		Dictionary<string, DatabaseReference> DatabaseReferences;
		Dictionary<string, IValueEventListener> ValueEventListeners;
		Dictionary<string, IChildEventListener> ChildEventListeners;

		public FirebaseServiceDroid()
		{
			DatabaseReferences = new Dictionary<string, DatabaseReference>();
			ValueEventListeners = new Dictionary<string, IValueEventListener>();
			ChildEventListeners = new Dictionary<string, IChildEventListener>();
		}

		private DatabaseReference GetDatabaseReference(string nodeKey)
		{
			if (DatabaseReferences.ContainsKey(nodeKey))
			{
				return DatabaseReferences[nodeKey];
			}
			else
			{
				DatabaseReference dr = FirebaseDatabase.Instance.GetReference(nodeKey);
				DatabaseReferences[nodeKey] =  dr;
				return dr;
			}
		}

		public void ObserveValueEvent<T>(string nodeKey, Action<T> action)
		{
			DatabaseReference dr = GetDatabaseReference(nodeKey);

			if (dr != null)
			{
				ValueEventListener<T> listener = new ValueEventListener<T>(action);
				dr.AddValueEventListener(listener);

				ValueEventListeners.Add(nodeKey, listener);
			}

		}

		public void ObserveChildEvent<T>(string nodeKey, Action<T> action)
		{
			DatabaseReference dr = GetDatabaseReference(nodeKey);

			if (dr != null)
			{
				ChildEventListener<T> listener = new ChildEventListener<T>(action);
				dr.AddChildEventListener(listener);

				ChildEventListeners.Add(nodeKey, listener);
			}
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

		public void RemoveValueEvent<T>(string nodeKey)
		{
			DatabaseReference dr = DatabaseReferences[nodeKey];

			if (dr != null)
			{
				dr.RemoveEventListener(ValueEventListeners[nodeKey]);
			}
		}

        public void AddChildEvent<T>(string nodeKey, Action<T> OnChildAdded = null, Action<T> OnChildRemoved = null, Action<T> OnChildChanged = null)
        {
            throw new NotImplementedException();
        }

        public void AddValueEvent<T>(string nodeKey, Action<T> OnValueEvent = null)
        {
            throw new NotImplementedException();
        }

        public void AddSingleValueEvent<T>(string nodeKey, Action<T> OnValueEvent = null)
        {
            throw new NotImplementedException();
        }

        public void RemoveValueEvent(string nodeKey)
        {
            throw new NotImplementedException();
        }

        public void RemoveChildEvent(string nodeKey)
        {
            throw new NotImplementedException();
        }

        public void SetValue(string nodeKey, object obj, Action onSuccess = null, Action<string> onError = null)
        {
            throw new NotImplementedException();
        }

        public void SetChildValueByAutoId(string nodeKey, object obj, Action onSuccess = null, Action<string> onError = null)
        {
            throw new NotImplementedException();
        }

        public void ChildExists<T>(string nodeKey, Action<T> onNodeFound = null, Action onNodeMissing = null)
        {
            throw new NotImplementedException();
        }
    }

}
