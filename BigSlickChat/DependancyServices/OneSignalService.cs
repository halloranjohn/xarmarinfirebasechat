using System;
namespace BigSlickChat
{
	public interface OneSignalService
	{
		void Init(string oneSignalAppId, string googleProjectNumber);
		void SendMessageToUser(string id, string message, Action successCallback, Action failureCallback);
	}
}