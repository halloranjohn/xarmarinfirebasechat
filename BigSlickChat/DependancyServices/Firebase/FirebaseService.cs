using System;
namespace BigSlickChat
{
	public interface FirebaseService
	{
		void AddChildEvent<T>(string nodeKey, Action<T> OnChildEvent);
		void AddValueEvent<T>(string nodeKey, Action<T> OnValueEvent);
		void RemoveValueEvent<T>(string nodeKey);
		void RemoveChildEvent<T>(string nodeKey);
		void SetValue(string nodeKey, object obj);
		void SetChildValueByAutoId(string nodeKey, object obj);
	}
}
