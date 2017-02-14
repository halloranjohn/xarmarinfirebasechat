using System;
using Android.App;
using Android.Content;
using Android.Media;
using Android.Support.V4.App;
using Android.Util;
using Firebase.Messaging;

namespace BigSlickChat.Droid
{
	[Service]
	[IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
	public class MyFirebaseMessagingService : FirebaseMessagingService
	{
		const string TAG = "MyFirebaseMsgService";
		public override void OnMessageReceived(RemoteMessage message)
		{
			Log.Debug(TAG, "From: " + message.From);
			sendNotification(message.Data["message"]);
			//Log.Debug(TAG, "Notification Message Body: " + message.GetNotification().Body);
		}

		private void sendNotification(String messageBody)
		{
			//Intent intent = new Intent(this, MainActivity.class);
	  //      intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
			//PendingIntent pendingIntent = PendingIntent.GetActivity(MainActivity., 0 /* Request code */, intent,
			//		PendingIntent.FLAG_ONE_SHOT);

			//Android.Net.Uri defaultSoundUri = RingtoneManager.GetDefaultUri(RingtoneType.Notification);
			//NotificationCompat.Builder notificationBuilder = new NotificationCompat.Builder(this)
			//		.SetSmallIcon(R.drawable.ic_stat_ic_notification)
			//		.setContentTitle("FCM Message")
			//		.setContentText(messageBody)
			//		.SetAutoCancel(true)
			//		.SetSound(defaultSoundUri)
			//		.setContentIntent(pendingIntent);

			//NotificationManager notificationManager =
			//		(NotificationManager)getSystemService(Context.NOTIFICATION_SERVICE);

			//notificationManager.notify(0 /* ID of notification */, notificationBuilder.build());
			// Construct a back stack for cross-task navigation:
   //	TaskStackBuilder stackBuilder = TaskStackBuilder.Create(this);
			//stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(MainActivity)));
			//stackBuilder.AddNextIntent(resultIntent);

			//PendingIntent resultPendingIntent =
			//stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);
			NotificationCompat.Builder builder = new NotificationCompat.Builder(this)
		.SetAutoCancel(true)                    // Dismiss from the notif. area when clicked
		//.SetContentIntent(resultPendingIntent)  // Start 2nd activity when the intent is clicked.
		.SetContentTitle("Button Clicked")      // Set its title
		//.SetNumber(count)                       // Display the count in the Content Info
				.SetSmallIcon(Resource.Drawable.icon)  // Display this icon
		.SetContentText(messageBody); // The message to display.
										// Finally, publish the notification:
			NotificationManager notificationManager =
				(NotificationManager)GetSystemService(Context.NotificationService);
			notificationManager.Notify(3, builder.Build());
    	}
	}
}