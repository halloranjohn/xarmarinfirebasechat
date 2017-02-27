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
		public string BatchSetObj<T>(string nodePath, T obj) where T : FirebaseItem
		{
			//Set the object and get the key
			obj.Key = DependencyService.Get<FirebaseDatabaseService>().SetChildValueByAutoId(nodePath, obj.GetFirebaseSaveData());

			Dictionary<string, object> batch = new Dictionary<string, object>();

			foreach(string path in obj.Paths)
			{
				batch.Add(path + obj.Key, obj);
			}

			//Batch update all children, note use of root node path as all batched paths must be child nodes
			DependencyService.Get<FirebaseDatabaseService>().BatchSetChildValues("/", batch);

			return obj.Key;
		}

		/// <summary>
		/// updates obj and all its child values
		/// NB: Must be located beneath 
		/// </summary>
		/// <param name="nodePath">Node path.</param>
		/// <param name="obj">Object.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public void BatchUpdateObj<T>(string nodePath, string key, T obj) where T : FirebaseItem
		{
			//Update the object and get the key
			DependencyService.Get<FirebaseDatabaseService>().SetValue(nodePath + key, obj.GetFirebaseSaveData());

			Dictionary<string, object> batch = new Dictionary<string, object>();

			foreach(string path in obj.Paths)
			{
				batch.Add(path + key, obj);
			}

			//Batch update all children, note use of root node path as all batched paths must be child nodes
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
			string retKey = DependencyService.Get<FirebaseDatabaseService>().SetChildValueByAutoId(nodePath, obj.GetFirebaseSaveData());

			//Cycle through 
			foreach(string path in obj.Paths)
			{
				DependencyService.Get<FirebaseDatabaseService>().SetValue(path + retKey, obj);
			}
		}

		public void BatchRemoveObj<T>(string nodePath, string key, T obj) where T : FirebaseItem
		{
			//Update the object and get the key
			DependencyService.Get<FirebaseDatabaseService>().RemoveValue(nodePath + "/" + key);

			Dictionary<string, object> batch = new Dictionary<string, object>();

			foreach(string path in obj.Paths)
			{
				batch.Add(path + key, null);
			}

			//Batch update all children, note use of root node path as all batched paths must be child nodes
			DependencyService.Get<FirebaseDatabaseService>().BatchSetChildValues("/", batch);
		}
	}
}
