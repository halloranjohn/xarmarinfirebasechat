using System;
using System.Collections.Generic;
using Com.OneSignal;
using Xamarin.Forms;

[assembly: Dependency(typeof(BigSlickChat.Droid.OneSignalServiceDroid))]
namespace BigSlickChat.Droid
{
	public class OneSignalServiceDroid : OneSignalService
	{

		public void Init(string oneSignalAppId, string googleProjectNumber)
		{
			OneSignal.NotificationReceived exampleNotificationReceivedDelegate = delegate (OSNotification notification)
			{
				try
				{
					System.Console.WriteLine("OneSignal Notification Received:\nMessage: {0}", notification.payload.body);
					Dictionary<string, object> additionalData = notification.payload.additionalData;

					if (additionalData.Count > 0)
						System.Console.WriteLine("additionalData: {0}", additionalData);
				}
				catch (System.Exception e)
				{
					System.Console.WriteLine(e.StackTrace);
				}
			};

			// Notification Opened Delegate
			OneSignal.NotificationOpened exampleNotificationOpenedDelegate = delegate (OSNotificationOpenedResult result)
			{
				try
				{
					System.Console.WriteLine("OneSignal Notification opened:\nMessage: {0}", result.notification.payload.body);
					Dictionary<string, object> additionalData = result.notification.payload.additionalData;
					if (additionalData.Count > 0)
						System.Console.WriteLine("additionalData: {0}", additionalData);


					List<Dictionary<string, object>> actionButtons = result.notification.payload.actionButtons;
					if (actionButtons.Count > 0)
						System.Console.WriteLine("actionButtons: {0}", actionButtons);
				}
				catch (System.Exception e)
				{
					System.Console.WriteLine(e.StackTrace);
				}
			};



			//OneSignal.StartInit("4170506a-4340-42ca-b2e2-3bb1ac033b36", "248736078310").EndInit();

			// Initialize OneSignal
			OneSignal.StartInit(oneSignalAppId, googleProjectNumber)
			  .HandleNotificationReceived(exampleNotificationReceivedDelegate)
			  .HandleNotificationOpened(exampleNotificationOpenedDelegate)
			  .EndInit();
		}

		public void SendMessageToUser(string id, string message, Action successCallback, Action failureCallback)
		{
			// Just an example userId, use your own or get it the devices by calling OneSignal.GetIdsAvailable
			string userId = id;

			Dictionary<string, object> notification = new Dictionary<string, object>();
			notification["contents"] = new Dictionary<string, string>() { { "en", "message" } };

			notification["include_player_ids"] = new List<string>() { userId };
			// Example of scheduling a notification in the future.
			//notification["send_after"] = System.DateTime.Now.ToUniversalTime().AddSeconds(30).ToString("U");

			OneSignal.PostNotification(notification, (responseSuccess) =>
			{
				successCallback();
				//oneSignalDebugMessage = "Notification posted successful! Delayed by about 30 secounds to give you time to press the home button to see a notification vs an in-app alert.\n" + Json.Serialize(responseSuccess);
			}, (responseFailure) =>
			{
				failureCallback();
				//oneSignalDebugMessage = "Notification failed to post:\n" + Json.Serialize(responseFailure);
			});
		}
	}
}