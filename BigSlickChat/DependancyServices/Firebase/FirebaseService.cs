using System;
namespace BigSlickChat
{
	public interface FirebaseService
	{
		void ObserveChildEvent<T>(string nodeKey, Action<T> action);
		void ObserveValueEvent<T>(string nodeKey, Action<T> action);
		void RemoveValueEvent<T>(string nodeKey);
		void DatabaseReferenceSetValue(string nodeKey, object obj);
	}
}
