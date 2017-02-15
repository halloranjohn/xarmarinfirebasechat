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

			OneSignal.StartInit("4170506a-4340-42ca-b2e2-3bb1ac033b36", "248736078310").EndInit();
			
			global::Xamarin.Forms.Forms.Init(this, bundle);

			LoadApplication(new App());
		}
	}
}
