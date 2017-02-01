using System;
namespace BigSlickChat
{
	public interface FirebaseService
	{
		void FirebaseObserveEventChildChanged<T>(string nodeKey, Action<T> action);
		void FirebaseRemoveObserveEventChildChanged<T>(string nodeKey, Action<T> action);
		void DatabaseReferenceSetValue(string nodeKey, object obj);
	}
}
