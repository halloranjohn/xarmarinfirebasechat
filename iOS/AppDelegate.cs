using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Firebase.Auth;
using Firebase.Database;
using Foundation;
using Newtonsoft.Json;
using UIKit;

using Facebook.CoreKit;

namespace BigSlickChat.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{        
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{            
			Firebase.Analytics.App.Configure();

			global::Xamarin.Forms.Forms.Init();

            //@podonnell: This was causing the app to crash when relaunching after sucessful Facebook login
            //removing seeing as it doesnt seem to be needed for anything
            //Facebook.CoreKit.ApplicationDelegate.SharedInstance.FinishedLaunching(app, options);

			LoadApplication(new App());
            		
			return base.FinishedLaunching(app, options);
		}

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            // We need to handle URLs by passing them to their own OpenUrl in order to make the SSO authentication works.
            return ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation);
        }

	}
}
