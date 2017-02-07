using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Firebase.Auth;
using Firebase.Database;
using Foundation;
using Newtonsoft.Json;
using UIKit;

namespace BigSlickChat.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			Firebase.Analytics.App.Configure();

			global::Xamarin.Forms.Forms.Init();

			LoadApplication(new App());

			//InitFirebase();


			return base.FinishedLaunching(app, options);
		}

		//private void InitFirebase()
		//{
		//	Firebase.Analytics.App.Configure();

		//	//Database.DefaultInstance.GetRootReference();//getInstance().getReference();
		//												//DatabaseReference myRef = database.GetChild("grocery-items/");
		//												//Debug.WriteLine(myRef.ToString());
		//												////myRef.setValue(data);
		//												//myRef.GetChild("testst").ObserveSingleEvent(

		//	DatabaseReference rootNode = Database.DefaultInstance.GetRootReference();

		//	DatabaseReference chatItemsNode = rootNode.GetChild("chat-items");

		//	//nuint handleReference = chatItemsNode.ObserveEvent(DataEventType.Value, (snapshot) =>
		//	chatItemsNode.ObserveSingleEvent(DataEventType.Value, (snapshot) =>                                                 
		//	{
		//		NSDictionary chatItemData = snapshot.GetValue<NSDictionary>();
		//		NSError error = null;
		//		string chatItemDaataString = NSJsonSerialization.Serialize(chatItemData, NSJsonWritingOptions.PrettyPrinted, out error).ToString();

		//		Dictionary<string, ChatItem> chatItems = JsonConvert.DeserializeObject<Dictionary<string, ChatItem>>(chatItemDaataString);
		//		//Debug.WriteLine("test");
		//		//foreach (NSObject chatItem in chatItemData.Values)
		//		//{

		//		//	string chatItemString = NSJsonSerialization.Serialize(chatItem, NSJsonWritingOptions.PrettyPrinted, out error).ToString();

		//		//	ChatItem realChatItem = JsonConvert.DeserializeObject<ChatItem>(chatItemString); //NSJsonSerialization.Deserialize(chatItem, NSJsonReadingOptions.AllowFragments, null);
		//		//	BigSlickChatPage.messages.Add(realChatItem);
		//		//}

		//		foreach (ChatItem chatItem in chatItems.Values)
		//		{
		//			BigSlickChatPage.messages.Add(chatItem);
		//		}

		//		// Do magic with the folder data
		//	});

		//	//nuint handleReference2 = chatItemsNode.ObserveEvent(DataEventType.ChildChanged, test);
		//	FirebaseObserveEventChildChanged<ChatItem>("chat-items", (item) => { BigSlickChatPage.messages.Add(item); Debug.WriteLine("sdsdsdssd"); });
		//	//nuint handleReference2 = chatItemsNode.ObserveEvent(DataEventType.ChildChanged, (snapshot) =>
		//	//{
		//	//	NSDictionary chatItemData = snapshot.GetValue<NSDictionary>();
		//	//	string test = snapshot.Key;
		//	//	NSError error = null;
		//	//	string chatItemDaataString = NSJsonSerialization.Serialize(chatItemData, NSJsonWritingOptions.PrettyPrinted, out error).ToString();

		//	//	ChatItem chatItem = JsonConvert.DeserializeObject<ChatItem>(chatItemDaataString);
		//	//	BigSlickChatPage.messages.Add(chatItem);
		//	//});

		//	//nuint handleReference2 = chatItemsNode.ObserveEvent(DataEventType.ChildChanged,

		//	//chatItemsNode.GetChild("testst").SetValue(new NSString("SDSDSDSDSDS"));

		//	//Auth.DefaultInstance.CreateUser("john@test.com", "qwerty", (user, error) =>
		//	// {
		//	//    Debug.WriteLine("dsdsds");
		//	//                     });
		//	//Auth.DefaultInstance.SignIn("john@test.com", "qwerty", (user, error) =>
		//	//{
		//	//	Debug.WriteLine(error.LocalizedDescription + " dsdsdsdsd ");
		//	//});
		//}

		//public void FirebaseObserveEventChildChanged<T>(string nodeKey, Action<T> action)
		//{
		//	DatabaseReference rootRef = Database.DefaultInstance.GetRootReference();

		//	DatabaseReference nodeRef = rootRef.GetChild(nodeKey);
		//	nuint handleReference2 = nodeRef.ObserveEvent(DataEventType.ChildChanged, (snapshot) =>
		//	{
		//		NSDictionary itemDict = snapshot.GetValue<NSDictionary>();
		//		NSError error = null;
		//		string itemDictString = NSJsonSerialization.Serialize(itemDict, NSJsonWritingOptions.PrettyPrinted, out error).ToString();

		//		T item = JsonConvert.DeserializeObject<T>(itemDictString);
		//		action(item);
		//		//BigSlickChatPage.messages.Add(item);
		//	});
		//}

		//public void test(DataSnapshot snapshot) 
		//	{
		//		NSDictionary chatItemData = snapshot.GetValue<NSDictionary>();
		//		string test = snapshot.Key;
		//		NSError error = null;
		//		string chatItemDaataString = NSJsonSerialization.Serialize(chatItemData, NSJsonWritingOptions.PrettyPrinted, out error).ToString();

		//		ChatItem chatItem = JsonConvert.DeserializeObject<ChatItem>(chatItemDaataString);
		//		BigSlickChatPage.messages.Add(chatItem);
		//	}


	}
}
