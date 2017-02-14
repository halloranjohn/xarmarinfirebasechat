using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Firebase;
using Firebase.Database;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using Java.Util;
using GoogleGson;
using Android.Gms.Common;
using Android.Util;
using Firebase.Iid;
using Firebase.Messaging;

namespace BigSlickChat.Droid
{
	[Activity(Label = "BigSlickChat.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IValueEventListener
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			FirebaseAuthServiceDroid.mainActivity = this;

			LoadApplication(new App());

			if (Intent.Extras != null)
			{
				foreach (var key in Intent.Extras.KeySet())
				{
					var value = Intent.Extras.GetString(key);
					Log.Debug("BIGSLICKAPP", "Key: {0} Value: {1}", key, value);
				}
			}

			Log.Debug("BIGSLICKAPPdddddddddddd", "InstanceID token: " + FirebaseInstanceId.Instance.Token);

			FirebaseMessaging.Instance.SubscribeToTopic("newsaa");

			IsPlayServicesAvailable();

			SendFirebaseMessage();
			//InitFirebase();
		}

		public void SendFirebaseMessage()
		{
			FirebaseMessaging fm = FirebaseMessaging.GetInstance();
			fm.Send(new RemoteMessage.Builder("248736078310" + "@gcm.googleapis.com")
					.SetMessageId(10)
			 	 	.AddData("my_message", "Hello World")
			  		.AddData("my_action", "SAY_HELLO")
					.Build());
				
		}


		private void InitFirebase()
		{
			//FirebaseOptions.Builder builder = new FirebaseOptions.Builder();
			//builder.SetApiKey("AIzaSyBgbB2YYb1iMj6be4RycaSxeOHpxTavJ0M");
			//builder.SetApplicationId("1:248736078310:android:749709b7e131ab1e");
			//builder.SetDatabaseUrl("https://bigslickchat.firebaseio.com/");


			//FirebaseApp.InitializeApp(this.ApplicationContext, builder.Build());
			DatabaseReference dr = FirebaseDatabase.Instance.GetReference("chat-items");

			dr.AddValueEventListener(this);
		}

		//FirebaseSendTestMessages()
		//{
		//	FirebaseMessaging fm = FirebaseMessaging.Instance;
		//	fm.Send(new RemoteMessage.Builder(Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid + "@gcm.googleapis.com")
		//	        .SetMessageId("testID").
		//	  .AddData("my_message", "Hello World")
		//	  .AddData("my_action", "SAY_HELLO")
		//	  .Build());
		//}

		public bool IsPlayServicesAvailable()
		{
			int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
			if (resultCode != ConnectionResult.Success)
			{
				if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
					System.Diagnostics.Debug.WriteLine(GoogleApiAvailability.Instance.GetErrorString(resultCode));
				else
				{
					System.Diagnostics.Debug.WriteLine("This device is not supported");
					Finish();
				}
				return false;
			}
			else
			{
				System.Diagnostics.Debug.WriteLine("Google Play Services is available.");
				return true;
			}
		}

		//public void OnDataChange(DataSnapshot snapshot)
		//{
		//	//_contacts = new List<ContactModel>();
		//	var obj = snapshot;
		//	//foreach (DataSnapshot s in obj.ToEnumerable())
		//	//{
		//	//	//var model = s.MapToContactModel();

		//	//	//if (model != null)
		//	//	//	_contacts.Add(model);
		//	//}

		//}

		void IValueEventListener.OnCancelled(DatabaseError error)
		{
			//throw new NotImplementedException();
		}

		void IValueEventListener.OnDataChange(DataSnapshot snapshot)
		{
			string chatItemsString = snapshot.Value.ToString();
			object chatItemsObject = snapshot.Value;

			HashMap javaObjChatItems = snapshot.Value.JavaCast<HashMap>();
			Gson gson = new GsonBuilder().SetPrettyPrinting().Create();
			string chatItemDaataString = gson.ToJson(javaObjChatItems);

			Dictionary<string, ChatItem> chatItems = JsonConvert.DeserializeObject<Dictionary<string, ChatItem>>(chatItemDaataString);
			//// create the serializer
			//var gson = new GsonBuilder()
			//	.SetPrettyPrinting()
			//	.Create();
			//String mapAsJson = JsonConvert.SerializeObject(javaObjChatItems);


			//Dictionary<string, ChatItem> chatItems = ObjectTypeHelper.Cast<Dictionary<string, ChatItem>>(snapshot.Value);
			//Dictionary<string, ChatItem> chatItems = snapshot.Value as Dictionary<string, ChatItem>;
			//snapshot.

			//HashMap javaObjChatItems = snapshot.Value.JavaCast<HashMap>();
			//String mapAsJson = new ObjectMapper().writeValueAsString(javaObjChatItems);
			//ChatItem chatItem = ObjectTypeHelper.Cast<ChatItem>(javaObjChatItems.Get("item1"));
			//Dictionary<string, ChatItem> chatItems = snapshot.GetValue(ChatItem.);
			//JSON.Stringify(chatItemsObject);
			//Dictionary<string, ChatItem> chatItems = JsonConvert.DeserializeObject<Dictionary<string, ChatItem>>(chatItemDaataString);
			//Dictionary<string, ChatItem> chatItems = snapshot.GetValue(type of Dictionary);//JsonConvert.DeserializeObject<Dictionary<string, ChatItem>>(chatItemsString); 
			//GenericTypeIndicator t = new GenericTypeIndicator() { };
			//NSDictionary chatItemData = snapshot.GetValue<NSDictionary>();
			//string test = snapshot.Key;
			//NSError error = null;
			//string chatItemDaataString = NSJsonSerialization.Serialize(chatItemData, NSJsonWritingOptions.PrettyPrinted, out error).ToString();

			//ChatItem chatItem = JsonConvert.DeserializeObject<ChatItem>(chatItemDaataString);
			//BigSlickChatPage.messages.Add(chatItems.First().Value);
			//throw new NotImplementedException();
		}


	}

	//public static class ObjectTypeHelper
	//{
	//	public static T Cast<T>(this Java.Lang.Object obj) where T : class
	//	{
	//		var propertyInfo = obj.GetType().GetProperty("Instance");
	//		return propertyInfo == null ? null : propertyInfo.GetValue(obj, null) as T;
	//	}
	//}
}
