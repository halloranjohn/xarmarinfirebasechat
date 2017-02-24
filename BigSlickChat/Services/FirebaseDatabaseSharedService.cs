using System;
using Xamarin.Forms;
namespace BigSlickChat
{
	public class FirebaseDatabaseSharedService
	{
		private static FirebaseDatabaseSharedService instance;

		private FirebaseDatabaseService fbDatabaseService;

		public static FirebaseDatabaseSharedService Instance
		{
			get
			{
				if(instance == null)
				{
					instance = new FirebaseDatabaseSharedService();
				}

				return instance;
			}
		}

		public void Init(FirebaseDatabaseService fbDatabaseService)
		{
			this.fbDatabaseService = fbDatabaseService;
		}


		public void SetValue<T>(string nodePath, T obj) where T : FirebaseItem
		{
			string retKey = DependencyService.Get<FirebaseDatabaseService>().SetChildValueByAutoId(nodePath, obj);

			//TODO: we could batch these 
			foreach(string path in obj.paths)
			{
				DependencyService.Get<FirebaseDatabaseService>().SetValue(path + retKey, obj);
			}

			//how to update parts of paths and 
		}
	}
}
