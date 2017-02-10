using System;

namespace BigSlickChat
{
	public interface FirebaseAuthService
	{
		void CreateUser(string username, string password, Action onCompleteAction = null, Action<string> onErrorAction = null);
		void SignIn(string username, string password, Action onCompleteAction = null, Action<string> onErrorAction = null);
		void SignOut();
		string GetUid();
	}
}

