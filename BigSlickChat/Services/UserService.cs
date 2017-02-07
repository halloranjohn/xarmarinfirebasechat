using System;
using Xamarin.Forms;

namespace BigSlickChat
{
	public class UserService
	{
		private static UserService instance;

		private UserService() 
		{
			user = new User("FF0000");
		}

		public static UserService Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new UserService();
				}
				return instance;
			}
		}

		public User User
		{
			get
			{
				return user;
			}

			set
			{
				user = value;
				string uid = DependencyService.Get<FirebaseAuthService>().GetUid();
				DependencyService.Get<FirebaseDatabaseService>().SetValue("users/" + uid, user);
			}
		}
		private User user;

		public void UserAuthenticated(bool isNewUser, Action onUserDataUpdated)
		{
			string uid = DependencyService.Get<FirebaseAuthService>().GetUid();

			if (isNewUser)
			{
				DependencyService.Get<FirebaseDatabaseService>().SetValue("users/" + uid, User);
			}

			DependencyService.Get<FirebaseDatabaseService>().AddValueEvent<User>("users/" + uid, OnUserValueChanged);
			OnUserDataSet = onUserDataUpdated;
		}

		private Action OnUserDataSet;


		public int ValueChangeCount { get; private set; }

		public void OnUserValueChanged(User obj)
		{
			user = obj;

			if (ValueChangeCount == 0)
			{
				OnUserDataSet();
				OnUserDataSet = null;
			}

			ValueChangeCount++;
		}
	}
}


