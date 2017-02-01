using System;
using Firebase.Database;
using Foundation;
using Newtonsoft.Json;
using Xamarin.Forms;

[assembly: Dependency(typeof(BigSlickChat.iOS.FirebaseServiceIos))]
namespace BigSlickChat.iOS
{
	public class FirebaseServiceIos : FirebaseService
	{
		public void FirebaseObserveEventChildChanged<T>(string nodeKey, Action<T> action)
		{
			DatabaseReference rootRef = Database.DefaultInstance.GetRootReference();

			DatabaseReference nodeRef = rootRef.GetChild(nodeKey);
			nuint handleReference2 = nodeRef.ObserveEvent(DataEventType.ChildChanged, (snapshot) =>
			{
				NSDictionary itemDict = snapshot.GetValue<NSDictionary>();
				NSError error = null;
				string itemDictString = NSJsonSerialization.Serialize(itemDict, NSJsonWritingOptions.PrettyPrinted, out error).ToString();

				T item = JsonConvert.DeserializeObject<T>(itemDictString);
				action(item);
			});
		}
	}
}
