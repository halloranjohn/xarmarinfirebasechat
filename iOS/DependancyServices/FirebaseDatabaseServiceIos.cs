using System;
using System.Collections.Generic;
using System.Globalization;
using Firebase.Database;
using Foundation;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Diagnostics;
using System.Collections.Specialized;

[assembly: Dependency(typeof(BigSlickChat.iOS.FirebaseServiceIos))]
namespace BigSlickChat.iOS
{
	public class FirebaseServiceIos : FirebaseDatabaseService
	{
		Dictionary<string, nuint> ChildRemovedEventHandles;
		Dictionary<string, nuint> ChildChangedEventHandles;
		Dictionary<string, nuint> ChildAddedEventHandles;
		Dictionary<string, nuint> ValueEventHandles;

		public FirebaseServiceIos()
		{
			ChildChangedEventHandles = new Dictionary<string, nuint>();
			ChildRemovedEventHandles = new Dictionary<string, nuint>();
			ChildAddedEventHandles = new Dictionary<string, nuint>();
			ValueEventHandles = new Dictionary<string, nuint>();
		}

		void FirebaseDatabaseService.SetChildValueByAutoId(string nodeKey, object obj, Action onSuccess, Action<string> onError)
		{
			DatabaseReference rootRef = Database.DefaultInstance.GetRootReference();

			DatabaseReference nodeRef = rootRef.GetChild(nodeKey);
			string objectJsonString = JsonConvert.SerializeObject(obj);
            NSError jsonError = null;
			NSData nsData = NSData.FromString(objectJsonString);
			NSObject nsObj = NSJsonSerialization.Deserialize(nsData, NSJsonReadingOptions.AllowFragments, out jsonError);

            nodeRef.GetChildByAutoId().SetValue(nsObj, (NSError error, DatabaseReference reference) => 
            {
                if (error == null)
                {
                    if (onSuccess != null)
                    {
                        onSuccess();
                    }
                }
                else if (onError != null)
                {
                    onError(error.Description);
                }       
            });
		}

		void FirebaseDatabaseService.SetValue(string nodeKey, object obj, Action onSuccess, Action<string> onError)
		{
			DatabaseReference rootRef = Database.DefaultInstance.GetRootReference();

			DatabaseReference nodeRef = rootRef.GetChild(nodeKey);

            NSObject nsObj = null;

            if(obj != null)
            {
                string objectJsonString = JsonConvert.SerializeObject(obj);
                NSError jsonError = null;
                NSData nsData = NSData.FromString(objectJsonString);

                nsObj = NSJsonSerialization.Deserialize(nsData, NSJsonReadingOptions.AllowFragments, out jsonError);                
            }

            nodeRef.SetValue(nsObj, (NSError error, DatabaseReference reference) =>
            {
                if(error == null)
                {
                    if(onSuccess != null)
                    {
                        onSuccess();                        
                    }
                }
                else if(onError != null)
                {
                    onError(error.Description);
                }
            });
		}

        void FirebaseDatabaseService.RemoveValue(string nodeKey, Action onSuccess, Action<string> onError)
        {
            DatabaseReference rootRef = Database.DefaultInstance.GetRootReference();

            DatabaseReference nodeRef = rootRef.GetChild(nodeKey);

            nodeRef.RemoveValue((NSError error, DatabaseReference reference) =>
            {
                if (error == null)
                {
                    if (onSuccess != null)
                    {
                        onSuccess();
                    }
                }
                else if (onError != null)
                {
                    onError(error.Description);
                }
            });
        }

		void FirebaseDatabaseService.AddChildEvent<T>(string nodeKey, Action<T> OnChildAdded, Action<T> OnChildRemoved, Action<T> OnChildChanged)
		{
			DatabaseReference rootRef = Database.DefaultInstance.GetRootReference();
			DatabaseReference nodeRef = rootRef.GetChild(nodeKey);

			nuint handleReference = AddChildEvent(nodeRef, DataEventType.ChildAdded, OnChildAdded);
			ChildAddedEventHandles[nodeKey] = handleReference;

			handleReference = AddChildEvent(nodeRef, DataEventType.ChildRemoved, OnChildRemoved);
			ChildRemovedEventHandles[nodeKey] = handleReference;

			handleReference = AddChildEvent(nodeRef, DataEventType.ChildChanged, OnChildChanged);
			ChildChangedEventHandles[nodeKey] = handleReference;
		}

		nuint AddChildEvent<T>(DatabaseReference nodeRef, DataEventType type, Action<T> eventAction)
		{
			nuint handleReference = nodeRef.ObserveEvent(type, (snapshot) =>
			{
                if (snapshot.HasChildren && eventAction != null)
				{
					NSDictionary itemDict = snapshot.GetValue<NSDictionary>();
					NSError error = null;
					string itemDictString = NSJsonSerialization.Serialize(itemDict, NSJsonWritingOptions.PrettyPrinted, out error).ToString();

					T item = JsonConvert.DeserializeObject<T>(itemDictString);
					eventAction(item);
				}
			});

			return handleReference;
		}

        void FirebaseDatabaseService.AddSingleValueEvent<T>(string nodeKey, Action<T> action)
        {
            DatabaseReference rootRef = Database.DefaultInstance.GetRootReference();

            DatabaseReference nodeRef = rootRef.GetChild(nodeKey);

            nodeRef.ObserveSingleEvent(DataEventType.Value, (snapshot) =>
            {
				if (snapshot.Exists && snapshot.HasChildren && action != null)
				{
					NSDictionary itemDict = snapshot.GetValue<NSDictionary>();
					NSError error = null;
					string itemDictString = NSJsonSerialization.Serialize(itemDict, NSJsonWritingOptions.PrettyPrinted, out error).ToString();

					T item = JsonConvert.DeserializeObject<T>(itemDictString);
					action(item);
				}
				else
				{
					T item = default(T);
					action(item);
				}
            });
        }

		void FirebaseDatabaseService.AddValueEvent<T>(string nodeKey, Action<T> action)
		{
			DatabaseReference rootRef = Database.DefaultInstance.GetRootReference();

            DatabaseReference nodeRef = rootRef.GetChild(nodeKey);

			nuint handleReference = nodeRef.ObserveEvent(DataEventType.Value, (snapshot) =>
			{
				if (snapshot.Exists && snapshot.HasChildren && action != null)
				{
					NSDictionary itemDict = snapshot.GetValue<NSDictionary>();
					NSError error = null;
					string itemDictString = NSJsonSerialization.Serialize(itemDict, NSJsonWritingOptions.PrettyPrinted, out error).ToString();

					T item = JsonConvert.DeserializeObject<T>(itemDictString);
					action(item);
				}
				else
				{
					T item = default(T);
					action(item);
				}
			});

			ValueEventHandles[nodeKey] = handleReference;
		}

		void FirebaseDatabaseService.RemoveValueEvent(string nodeKey)
		{
			if (ValueEventHandles.ContainsKey(nodeKey))
			{
				DatabaseReference rootRef = Database.DefaultInstance.GetRootReference();

				DatabaseReference nodeRef = rootRef.GetChild(nodeKey);
				nodeRef.RemoveObserver(ValueEventHandles[nodeKey]);
			}

		}

		void FirebaseDatabaseService.RemoveChildEvent(string nodeKey)
		{
			if (ChildAddedEventHandles.ContainsKey(nodeKey))
			{
				DatabaseReference rootRef = Database.DefaultInstance.GetRootReference();

				DatabaseReference nodeRef = rootRef.GetChild(nodeKey);
				nodeRef.RemoveObserver(ChildAddedEventHandles[nodeKey]);
				nodeRef.RemoveObserver(ChildRemovedEventHandles[nodeKey]);
				nodeRef.RemoveObserver(ChildChangedEventHandles[nodeKey]);
			}
		}

		void FirebaseDatabaseService.Search<T>(string nodeKey, string orderByChildKey, Action<List<T>> action)
		{
			DatabaseReference rootRef = Database.DefaultInstance.GetRootReference();
			DatabaseReference nodeRef = rootRef.GetChild(nodeKey);

			var watch = System.Diagnostics.Stopwatch.StartNew();

			nodeRef.GetQueryOrderedByChild(orderByChildKey).ObserveSingleEvent(DataEventType.Value, (snapshot) =>
			{
				if(snapshot.Exists && snapshot.HasChildren && action != null)
				{
					watch.Stop();
					Debug.WriteLine("search time: " + watch.ElapsedMilliseconds);
					watch.Restart();

					NSEnumerator e = snapshot.Children;
					NSObject o = e.NextObject();
					DataSnapshot snap;
					NSMutableArray array = new NSMutableArray();

					while(o != null)
					{
						//Debug.WriteLine(o.ToString());
						snap = o as DataSnapshot;
						array.Add(snap.GetValue());
						o = e.NextObject();
					}

				   	NSError error = null;
					string itemArrayStr = NSJsonSerialization.Serialize(array, NSJsonWritingOptions.PrettyPrinted, out error).ToString();
					List<T> itemArr = JsonConvert.DeserializeObject<List<T>>(itemArrayStr);

					watch.Stop();
					Debug.WriteLine("finish search process time: " + watch.ElapsedMilliseconds);

					action(itemArr);
				}
				else
				{
					List<T> item = new List<T>();
					action(item);
				}
			});
		}

        //void FirebaseDatabaseService.ChildExists<T>(string nodeKey, Action<T> onNodeFound, Action onNodeMissing)
        //{
        //    DatabaseReference rootRef = Database.DefaultInstance.GetRootReference();

        //    DatabaseReference nodeRef = rootRef.GetChild(nodeKey);
        //    nodeRef.ObserveSingleEvent(DataEventType.Value, (snapshot) =>
        //    {
        //        if (snapshot.Exists && onNodeFound != null)
        //        {
        //            NSDictionary itemDict = snapshot.GetValue<NSDictionary>();
        //            NSError error = null;
        //            string itemDictString = NSJsonSerialization.Serialize(itemDict, NSJsonWritingOptions.PrettyPrinted, out error).ToString();

        //            T item = JsonConvert.DeserializeObject<T>(itemDictString);
        //            onNodeFound(item);
        //        }
        //        else if(onNodeMissing != null)
        //        {
        //            onNodeMissing();
        //        }
        //    });
        //}
	}


}
