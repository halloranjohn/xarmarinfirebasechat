using System;
using Xamarin.Forms;
using System.Collections.Generic;
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

		/// <summary>
		/// Adds a new value and updates all child values
		/// NB: Must be located beneath 
		/// </summary>
		/// <param name="nodePath">Node path.</param>
		/// <param name="obj">Object.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public void SetAndBatchSetChildren<T>(string nodePath, T obj) where T : FirebaseItem
		{
			//Set the object and get the key
			string newKey = DependencyService.Get<FirebaseDatabaseService>().SetChildValueByAutoId(nodePath, obj);

			//Batch update all children
			Dictionary<string, object> batch = new Dictionary<string, object>();

			foreach(string path in obj.paths)
			{
				batch.Add(path + newKey, obj);
			}

			//how to update parts of paths and
			DependencyService.Get<FirebaseDatabaseService>().BatchSetChildValues("/", batch);
		}

		/// <summary>
		/// updates obj and all its child values
		/// NB: Must be located beneath 
		/// </summary>
		/// <param name="nodePath">Node path.</param>
		/// <param name="obj">Object.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public void UpdateAndBatchUpdateChildren<T>(string nodePath, string key, T obj) where T : FirebaseItem
		{
			//Update the object and get the key
			DependencyService.Get<FirebaseDatabaseService>().SetValue(nodePath + key, obj);

			//Batch update all children
			Dictionary<string, object> batch = new Dictionary<string, object>();

			foreach(string path in obj.paths)
			{
				batch.Add(path + key, obj);
			}

			//how to update parts of paths and
			DependencyService.Get<FirebaseDatabaseService>().BatchSetChildValues("/" + key, batch);
		}

		/// <summary>
		/// Sets the and batch update all values.
		/// NB, this func can update any paths, therefore requires multiple calls
		/// </summary>
		/// <param name="nodePath">Node path.</param>
		/// <param name="obj">Object.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public void SetAndUpdateAllValues<T>(string nodePath, T obj) where T : FirebaseItem
		{
			//Set the new object and get the key
			string retKey = DependencyService.Get<FirebaseDatabaseService>().SetChildValueByAutoId(nodePath, obj);

			//Cycle through 
			foreach(string path in obj.paths)
			{
				DependencyService.Get<FirebaseDatabaseService>().SetValue(path + retKey, obj);
			}
		}
	}
}
