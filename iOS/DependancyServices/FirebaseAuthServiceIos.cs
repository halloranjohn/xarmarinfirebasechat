using System;
using System.Diagnostics;
using Firebase.Auth;
using Foundation;
using Xamarin.Forms;

[assembly: Dependency(typeof(BigSlickChat.iOS.FirebaseAuthServiceIos))]
namespace BigSlickChat.iOS
{
	public class FirebaseAuthServiceIos : FirebaseAuthService
	{
		public FirebaseAuthServiceIos()
		{
		}

		public void CreateUser(string username, string password, Action onCompleteAction)
		{
			if (Auth.DefaultInstance.CurrentUser == null)
			{
				Auth.DefaultInstance.CreateUser(username, password, (user, error) =>
				{
					if (error != null)
					{
						Debug.Write("CREATE USER ERROR " + error.ToString());
					}
					else
					{
						onCompleteAction();
					}
				});
			}
			else
			{
				string uid = Auth.DefaultInstance.CurrentUser.Uid;
			}

		}

		public void SignOut()
		{
			if (Auth.DefaultInstance.CurrentUser != null)
			{
				NSError error = null;
				Auth.DefaultInstance.SignOut(out error);
			}
		}

		public void SignIn(string username, string password, Action onCompleteAction)
		{
			if (Auth.DefaultInstance.CurrentUser == null)
			{
				Auth.DefaultInstance.SignIn(username, password, (user, error) => 
				{
					if (error != null)
					{
						Debug.Write("SIGNIN USER ERROR " + error.ToString());
					}
					else
					{
						onCompleteAction();
					}
				});
			}
			else
			{
				string uid = Auth.DefaultInstance.CurrentUser.Uid;
			}
		}

		public string GetUid()
		{
			return Auth.DefaultInstance.CurrentUser.Uid;
		}
	}
}
