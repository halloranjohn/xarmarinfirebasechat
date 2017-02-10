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

            Action<User> onUserFound = (User userOnServer) => 
            {
                User = userOnServer;
                onUserDataUpdated();
            };

            Action onUserMissing = () => 
            {
                User = new User(uid, new List<string>(), "FF0000");
                onUserDataUpdated();
            };

            firebaseDatabaseService.ChildExists(USERS_URL_PREFIX + uid, onUserFound, onUserMissing);

            //OnUserDataSet = onUserDataUpdated;
            //firebaseDatabaseService.AddValueEvent<User>(USERS_URL_PREFIX + uid, OnUserValueChanged);

            //if (isNewUser)
            //{
            //    SaveUserToServer();
            //}
        }

		//private Action OnUserDataSet;


		//public int ValueChangeCount { get; private set; }

		//public void OnUserValueChanged(User obj)
		//{
		//	user = obj;

		//	if (ValueChangeCount == 0)
		//	{
		//		OnUserDataSet();
		//		OnUserDataSet = null;
		//	}

		//	ValueChangeCount++;
		//}

        public void Signout()
        {
            firebaseAuthService.SignOut();

            user = null;

            //ValueChangeCount = 0;
            //OnUserDataSet = null;
        }

        public void SaveUserToServer()
        {
            firebaseDatabaseService.SetValue(USERS_URL_PREFIX + user.id, User);
        }
	}
}


