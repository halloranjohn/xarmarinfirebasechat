using System;
using System.Collections.Generic;

namespace BigSlickChat
{
	public class UserService
	{
        public const string USERS_URL_PREFIX = "users/";

		private static UserService instance;

        private FirebaseAuthService firebaseAuthService;
        private FirebaseDatabaseService firebaseDatabaseService;

		private UserService() 
		{
        }

        public void Init(FirebaseAuthService fbAuthService, FirebaseDatabaseService fbDatabaseService)
        {
            firebaseAuthService = fbAuthService;
            firebaseDatabaseService = fbDatabaseService;

            User = new User(firebaseAuthService.GetUid(), new List<string>(), "FF0000");
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
                SaveUserToServer();
			}
		}
		private User user;

		public void UserAuthenticated(bool isNewUser, Action onUserDataUpdated)
		{
            string uid = firebaseAuthService.GetUid();

			if (isNewUser)
			{
                SaveUserToServer();
			}

            OnUserDataSet = onUserDataUpdated;
            firebaseDatabaseService.AddValueEvent<User>("users/" + uid, OnUserValueChanged);
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
            firebaseAuthService.SignOut();

            ValueChangeCount = 0;
            OnUserDataSet = null;
        }

        public void SaveUserToServer()
        {
            firebaseDatabaseService.SetValue(USERS_URL_PREFIX + user.id, User);
        }
	}
}


