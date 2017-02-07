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
	public class FirebaseServiceIos : FirebaseService
	{
		Dictionary<string, nuint> ChildChangedEventHandles;
		Dictionary<string, nuint> ChildAddedEventHandles;
		Dictionary<string, nuint> ValueEventHandles;

		public FirebaseServiceIos()
		{
			ChildChangedEventHandles = new Dictionary<string, nuint>();
			ChildAddedEventHandles = new Dictionary<string, nuint>();
			ValueEventHandles = new Dictionary<string, nuint>();
		}

		void FirebaseService.SetChildValueByAutoId(string nodeKey, object obj)
		{
			DatabaseReference rootRef = Database.DefaultInstance.GetRootReference();

			DatabaseReference nodeRef = rootRef.GetChild(nodeKey);
			string objectJsonString = JsonConvert.SerializeObject(obj);
			NSError error = null;
			NSData nsData = NSData.FromString(objectJsonString);
			NSObject nsObj = NSJsonSerialization.Deserialize(nsData, NSJsonReadingOptions.AllowFragments, out error);
			//NSObject nsObj = NSObjectConversionExtensions.ConvertToNSObject(obj);//NSObject.FromObject(obj);
			nodeRef.GetChildByAutoId().SetValue(nsObj);
		}

		void FirebaseService.SetValue(string nodeKey, object obj)
		{
			DatabaseReference rootRef = Database.DefaultInstance.GetRootReference();

			DatabaseReference nodeRef = rootRef.GetChild(nodeKey);
			string objectJsonString = JsonConvert.SerializeObject(obj);
			NSError error = null;
			NSData nsData = NSData.FromString(objectJsonString);
			NSObject nsObj = NSJsonSerialization.Deserialize(nsData, NSJsonReadingOptions.AllowFragments, out error);
			//NSObject nsObj = NSObjectConversionExtensions.ConvertToNSObject(obj);//NSObject.FromObject(obj);
			nodeRef.SetValue(nsObj);
		}

		void FirebaseService.AddChildEvent<T>(string nodeKey, Action<T> action)
		{
			DatabaseReference rootRef = Database.DefaultInstance.GetRootReference();

			DatabaseReference nodeRef = rootRef.GetChild(nodeKey);
			nuint handleReference = nodeRef.ObserveEvent(DataEventType.ChildAdded, (snapshot) =>
			{
				NSDictionary itemDict = snapshot.GetValue<NSDictionary>();
				NSError error = null;
				string itemDictString = NSJsonSerialization.Serialize(itemDict, NSJsonWritingOptions.PrettyPrinted, out error).ToString();

				T item = JsonConvert.DeserializeObject<T>(itemDictString);
				action(item);
			});

			ChildAddedEventHandles[nodeKey] = handleReference;
		}

		void FirebaseService.AddValueEvent<T>(string nodeKey, Action<T> action)
		{
			DatabaseReference rootRef = Database.DefaultInstance.GetRootReference();

			DatabaseReference nodeRef = rootRef.GetChild(nodeKey);
			nuint handleReference = nodeRef.ObserveEvent(DataEventType.Value, (snapshot) =>
			{
				NSDictionary itemDict = snapshot.GetValue<NSDictionary>();
				NSError error = null;
				string itemDictString = NSJsonSerialization.Serialize(itemDict, NSJsonWritingOptions.PrettyPrinted, out error).ToString();

				T item = JsonConvert.DeserializeObject<T>(itemDictString);
				action(item);
			});

			ValueEventHandles[nodeKey] = handleReference;
		}

		void FirebaseService.RemoveValueEvent<T>(string nodeKey)
		{
			if (ValueEventHandles.ContainsKey(nodeKey))
			{
				DatabaseReference rootRef = Database.DefaultInstance.GetRootReference();

				DatabaseReference nodeRef = rootRef.GetChild(nodeKey);
				nodeRef.RemoveObserver(ValueEventHandles[nodeKey]);
			}

		}

		void FirebaseService.RemoveChildEvent<T>(string nodeKey)
		{
			if (ChildAddedEventHandles.ContainsKey(nodeKey))
			{
				DatabaseReference rootRef = Database.DefaultInstance.GetRootReference();

				DatabaseReference nodeRef = rootRef.GetChild(nodeKey);
				nodeRef.RemoveObserver(ChildAddedEventHandles[nodeKey]);
			}
		}
	}


}
