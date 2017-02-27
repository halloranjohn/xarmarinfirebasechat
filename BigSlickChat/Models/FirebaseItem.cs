using System;
using System.Collections.Generic;
namespace BigSlickChat
{
	public class FirebaseItem
	{
		public string Key { set; get; }

		public List<string> Paths { set; get; }

		public FirebaseItem()
		{
		}

		public virtual object GetFirebaseSaveData()
		{
			//default returns the entire object, over
			return this;
		}
	}
}
