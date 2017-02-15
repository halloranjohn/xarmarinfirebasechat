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
using UserNotifications;
using Firebase.CloudMessaging;
using Firebase.InstanceID;

namespace BigSlickChat.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IUNUserNotificationCenterDelegate, IMessagingDelegate
	{        
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{            
			Firebase.Analytics.App.Configure();

			global::Xamarin.Forms.Forms.Init();

            //@podonnell: This was causing the app to crash when relaunching after sucessful Facebook login
            //removing seeing as it doesnt seem to be needed for anything
            //Facebook.CoreKit.ApplicationDelegate.SharedInstance.FinishedLaunching(app, options);

            SetupFirebasePushNotifications(app);

			LoadApplication(new App());
            		
			return base.FinishedLaunching(app, options);
		}

        void SetupFirebasePushNotifications(UIApplication app)
        {
            InstanceId.Notifications.ObserveTokenRefresh((object sender, NSNotificationEventArgs e) =>
            {                
                Debug.WriteLine("DEVICE MESSAGING TOKEN\n" + InstanceId.SharedInstance.Token);

                Messaging.SharedInstance.Connect(error =>
                {
                    if (error != null)
                    {
                        // Handle if something went wrong while connecting
                        Debug.WriteLine("MESSAGING CONNECT ERROR " + error.Description);
                    }
                    else
                    {
                        // Let the user know that connection was successful
                        Debug.WriteLine("MESSAGING CONNECT SUCCESS");
                    }
                });
            });

            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                //iOS 10 or later
                UNAuthorizationOptions authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;

                UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) =>
                {
                    Console.WriteLine(granted);
                });

                // For iOS 10 display notification (sent via APNS)
                UNUserNotificationCenter.Current.Delegate = this;

                // For iOS 10 data message (sent via FCM)
                Messaging.SharedInstance.RemoteMessageDelegate = this;
            }
            else
            {
                // iOS 9 or before
                UIUserNotificationType allNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
                UIUserNotificationSettings settings = UIUserNotificationSettings.GetSettingsForTypes(allNotificationTypes, null);
                UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            }

            UIApplication.SharedApplication.RegisterForRemoteNotifications();

            Debug.WriteLine("DEVICE MESSAGING TOKEN\n" + InstanceId.SharedInstance.Token);
        }

        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            //Use this method to prep your app for when a user clicks on your notification
            //Note: there is a known bug where this isnt called in iOS 10.0 it was fixed in 10.1
        }

        public void ApplicationReceivedRemoteMessage(RemoteMessage remoteMessage)
        {
            //This is needed to allow Firebase to forward messages received whilst in the foreground on iOS 10
        }

        // To receive notifications in foreground on iOS 10 devices.
        [Export("userNotificationCenter:willPresentNotification:withCompletionHandler:")]
        public void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            //present the notification as an alert even when received whilst app is in foreground
            completionHandler(UNNotificationPresentationOptions.Alert);
        }

        public override void DidEnterBackground(UIApplication uiApplication)
        {
            Messaging.SharedInstance.Disconnect();

            base.DidEnterBackground(uiApplication);
        }

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            // We need to handle URLs by passing them to their own OpenUrl in order to make the SSO authentication work.
            return ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation);
        }
    }
}
