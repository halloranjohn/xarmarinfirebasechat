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
using Com.OneSignal;
using OneSignalPush.MiniJSON;

namespace BigSlickChat.Droid 
{ 
	[Activity(Label = "BigSlickChat.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			//InitOneSignal();

			global::Xamarin.Forms.Forms.Init(this, bundle);

			LoadApplication(new App());
		}

		//private void InitOneSignal()
		//{	
		//	OneSignal.NotificationReceived exampleNotificationReceivedDelegate = delegate (OSNotification notification)
		//	{
		//	   try
		//	   {
		//		   System.Console.WriteLine("OneSignal Notification Received:\nMessage: {0}", notification.payload.body);
		//		   Dictionary<string, object> additionalData = notification.payload.additionalData;

		//		   if (additionalData.Count > 0)
		//			   System.Console.WriteLine("additionalData: {0}", additionalData);
		//	   }
		//	   catch (System.Exception e)
		//	   {
		//		   System.Console.WriteLine(e.StackTrace);
		//	   }
		//	};

		//	// Notification Opened Delegate
		//	OneSignal.NotificationOpened exampleNotificationOpenedDelegate = delegate (OSNotificationOpenedResult result)
		//	{
		//		try
		//		{
		//			System.Console.WriteLine("OneSignal Notification opened:\nMessage: {0}", result.notification.payload.body);
		//			Dictionary<string, object> additionalData = result.notification.payload.additionalData;
		//			if (additionalData.Count > 0)
		//				System.Console.WriteLine("additionalData: {0}", additionalData);


		//			List<Dictionary<string, object>> actionButtons = result.notification.payload.actionButtons;
		//			if (actionButtons.Count > 0)
		//				System.Console.WriteLine("actionButtons: {0}", actionButtons);
		//		}
		//		catch (System.Exception e)
		//		{
		//			System.Console.WriteLine(e.StackTrace);
		//		}
		//	};



		//	//OneSignal.StartInit("4170506a-4340-42ca-b2e2-3bb1ac033b36", "248736078310").EndInit();

		//	// Initialize OneSignal
		//	OneSignal.StartInit("4170506a-4340-42ca-b2e2-3bb1ac033b36", "248736078310")
		//	  .HandleNotificationReceived(exampleNotificationReceivedDelegate)
		//	  .HandleNotificationOpened(exampleNotificationOpenedDelegate)
		//	  .EndInit();

		//	//// Set our view from the "main" layout resource
		//	//SetContentView(Resource.Layout.Main);

		//	//// Get our button from the layout resource,
		//	//// and attach an event to it
		//	//Button button = FindViewById<Button>(Resource.Id.myButton);

		//	//button.Click += delegate
		//	//{
		//	//	button.Text = string.Format("{0} clicks!", count++);
		//	//};

		//	//SomeMethod();
		//}

		////private static string oneSignalDebugMessage;




	}
}
