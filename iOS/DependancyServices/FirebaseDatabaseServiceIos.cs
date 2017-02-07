using System;
using System.Collections.Generic;
using System.Globalization;
using Firebase.Database;
using Foundation;
using Newtonsoft.Json;
using Xamarin.Forms;

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

		void FirebaseDatabaseService.SetChildValueByAutoId(string nodeKey, object obj)
		{
			DatabaseReference rootRef = Database.DefaultInstance.GetRootReference();

			DatabaseReference nodeRef = rootRef.GetChild(nodeKey);
			string objectJsonString = JsonConvert.SerializeObject(obj);
			NSError error = null;
			NSData nsData = NSData.FromString(objectJsonString);
			NSObject nsObj = NSJsonSerialization.Deserialize(nsData, NSJsonReadingOptions.AllowFragments, out error);
			nodeRef.GetChildByAutoId().SetValue(nsObj);
		}

		void FirebaseDatabaseService.SetValue(string nodeKey, object obj)
		{
			DatabaseReference rootRef = Database.DefaultInstance.GetRootReference();

			DatabaseReference nodeRef = rootRef.GetChild(nodeKey);
			string objectJsonString = JsonConvert.SerializeObject(obj);
			NSError error = null;
			NSData nsData = NSData.FromString(objectJsonString);
			NSObject nsObj = NSJsonSerialization.Deserialize(nsData, NSJsonReadingOptions.AllowFragments, out error);
			nodeRef.SetValue(nsObj);
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

		nuint AddChildEvent<T>(DatabaseReference nodeRef, DataEventType type, Action<T> eventAction )
		{
			nuint handleReference = nodeRef.ObserveEvent(type, (snapshot) =>
			{
				if (snapshot.HasChildren)
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

		void FirebaseDatabaseService.AddValueEvent<T>(string nodeKey, Action<T> action)
		{
			DatabaseReference rootRef = Database.DefaultInstance.GetRootReference();

			DatabaseReference nodeRef = rootRef.GetChild(nodeKey);
			nuint handleReference = nodeRef.ObserveEvent(DataEventType.Value, (snapshot) =>
			{
				if (snapshot.HasChildren)
				{
					NSDictionary itemDict = snapshot.GetValue<NSDictionary>();
					NSError error = null;
					string itemDictString = NSJsonSerialization.Serialize(itemDict, NSJsonWritingOptions.PrettyPrinted, out error).ToString();

					T item = JsonConvert.DeserializeObject<T>(itemDictString);
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
	}


}
