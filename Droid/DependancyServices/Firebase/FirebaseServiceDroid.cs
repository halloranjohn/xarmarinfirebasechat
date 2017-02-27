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

		public void AddValueEvent<T>(string nodeKey, Action<T> action)
		{
			DatabaseReference dr = GetDatabaseReference(nodeKey);

			if (dr != null)
			{
				ValueEventListener<T> listener = new ValueEventListener<T>(action);
				dr.AddValueEventListener(listener);

				ValueEventListeners.Add(nodeKey, listener);
			}
		}

		public void AddSingleValueEvent<T>(string nodeKey, Action<T> action)
		{
			DatabaseReference dr = GetDatabaseReference(nodeKey);

			if (dr != null)
			{
				ValueEventListener<T> listener = new ValueEventListener<T>(action);
				dr.AddListenerForSingleValueEvent(listener);


				ValueEventListeners.Add(nodeKey, listener);
			}

		}

		public void AddChildEvent<T>(string nodeKey, Action<string, T> OnChildAdded, Action<string, T> OnChildRemoved, Action<string, T> OnChildUpdated)
		{
			DatabaseReference dr = GetDatabaseReference(nodeKey);

			if (dr != null)
			{
				ChildEventListener<T> listener = new ChildEventListener<T>(OnChildAdded);
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

		public string SetChildValueByAutoId(string nodeKey, object obj, Action onSuccess = null, Action<string> onError = null)
		{
			DatabaseReference dr = FirebaseDatabase.Instance.GetReference(nodeKey);

			if (dr != null)
			{
				string objJsonString = JsonConvert.SerializeObject(obj);

				Gson gson = new GsonBuilder().SetPrettyPrinting().Create();
				HashMap dataHashMap = new HashMap();
				Java.Lang.Object jsonObj = gson.FromJson(objJsonString, dataHashMap.Class);
				dataHashMap = jsonObj.JavaCast<HashMap>();
				DatabaseReference newChildRef = dr.Push();
				newChildRef.SetValue(dataHashMap);
				return newChildRef.Key;
			}

			return null;
		}

		public void SetValue(string nodeKey, object obj, Action onSuccess = null, Action<string> onError = null)
		{
			DatabaseReference dr = FirebaseDatabase.Instance.GetReference(nodeKey);

			if (dr != null)
			{
				string objJsonString = JsonConvert.SerializeObject(obj);

				Gson gson = new GsonBuilder().SetPrettyPrinting().Create();
				HashMap dataHashMap = new HashMap();
				Java.Lang.Object jsonObj = gson.FromJson(objJsonString, dataHashMap.Class);
				dataHashMap = jsonObj.JavaCast<HashMap>();
				dr.SetValue(dataHashMap);
			}
		}

		public void BatchSetChildValues(string nodeKey, Dictionary<string, object> dict, Action onSuccess = null, Action<string> onError = null)
		{
			throw new NotImplementedException();
		}

		public void RemoveValueEvent(string nodeKey)
		{
			DatabaseReference dr = DatabaseReferences[nodeKey];

			if (dr != null)
			{
				dr.RemoveEventListener(ValueEventListeners[nodeKey]);
				ValueEventListeners.Remove(nodeKey);
			}
		}

		public void RemoveChildEvent(string nodeKey)
		{
			DatabaseReference dr = DatabaseReferences[nodeKey];

			if (dr != null)
			{
				dr.RemoveEventListener(ChildEventListeners[nodeKey]);
				ChildEventListeners.Remove(nodeKey);
			}
		}

		public void RemoveValue(string nodeKey, Action onSuccess = null, Action<string> onError = null)
		{
			DatabaseReference dr = FirebaseDatabase.Instance.GetReference(nodeKey);

			if (dr != null)
			{
				dr.RemoveValue();
			}
		}

		void FirebaseDatabaseService.Search<T>(string nodeKey, Action<List<T>> action)
		{ 
			throw new NotImplementedException();
		}

		void FirebaseDatabaseService.Search<T>(string nodeKey, Action<List<T>> action, string orderByChildKey)
		{
			throw new NotImplementedException();
		}

		void FirebaseDatabaseService.Search<T>(string nodeKey, Action<List<T>> action, string orderByChildKey, string startAt, string endAt)
		{
			throw new NotImplementedException();
		}

		void FirebaseDatabaseService.SearchOrderedByFirstValues<T>(string nodeKey, Action<List<T>> action, uint limitToFirst)
		{
			throw new NotImplementedException();
		}

		void FirebaseDatabaseService.SearchOrderedByLastValues<T>(string nodeKey, Action<List<T>> action, uint limitToLast)
		{
			throw new NotImplementedException();
		}
	}

}
