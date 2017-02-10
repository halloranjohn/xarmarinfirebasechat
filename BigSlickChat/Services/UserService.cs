using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace BigSlickChat
{
	public class UserService
	{
		private static UserService instance;

		private UserService() 
		{
            User = new User(DependencyService.Get<FirebaseAuthService>().GetUid(), new List<string>(), "FF0000");
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
                DependencyService.Get<FirebaseDatabaseService>().SetValue("users/" + user.id, user);
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

            OnUserDataSet = onUserDataUpdated;
            DependencyService.Get<FirebaseDatabaseService>().AddValueEvent<User>("users/" + uid, OnUserValueChanged);
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

        public void Signout()
        {
            DependencyService.Get<FirebaseAuthService>().SignOut();

            ValueChangeCount = 0;
            OnUserDataSet = null;
        }
	}
}


