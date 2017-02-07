using System;
namespace BigSlickChat
{
	public interface FirebaseDatabaseService
	{
		void AddChildEvent<T>(string nodeKey, Action<T> OnChildAdded, Action<T> OnChildRemoved, Action<T> OnChildChanged);
		void AddValueEvent<T>(string nodeKey, Action<T> OnValueEvent);
		void RemoveValueEvent(string nodeKey);
		void RemoveChildEvent(string nodeKey);
		void SetValue(string nodeKey, object obj);
		void SetChildValueByAutoId(string nodeKey, object obj);
	}
}
