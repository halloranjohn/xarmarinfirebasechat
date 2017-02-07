using System;

namespace BigSlickChat
{
	public interface FirebaseAuthService
	{
		void CreateUser(string username, string password, Action onCompleteAction);
		void SignIn(string username, string password, Action onCompleteAction);
		void SignOut();
		string GetUid();
	}
}

