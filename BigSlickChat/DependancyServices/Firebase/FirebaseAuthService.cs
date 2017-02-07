using System;

namespace BigSlickChat
{
	public interface FirebaseAuthService
	{
		void CreateUser(string username, string password, Action onCompleteAction, Action<string> onErrorAction);
		void SignIn(string username, string password, Action onCompleteAction, Action<string> onErrorAction);
		void SignOut();
		string GetUid();
	}
}

