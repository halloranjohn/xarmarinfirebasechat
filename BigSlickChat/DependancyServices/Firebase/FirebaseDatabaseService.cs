using System;
namespace BigSlickChat
{
	public interface FirebaseDatabaseService
	{
		void AddChildEvent<T>(string nodeKey, Action<string, T> OnChildAdded = null, Action<string, T> OnChildRemoved = null, Action<string, T> OnChildChanged = null);		
        void AddValueEvent<T>(string nodeKey, Action<T> OnValueEvent = null);
        void AddSingleValueEvent<T>(string nodeKey, Action<T> OnValueEvent = null);
		void RemoveValueEvent(string nodeKey);
		void RemoveChildEvent(string nodeKey);
        void SetValue(string nodeKey, object obj, Action onSuccess = null, Action<string> onError = null);
		string SetChildValueByAutoId(string nodeKey, object obj, Action onSuccess = null, Action<string> onError = null);
        void RemoveValue(string nodeKey, Action onSuccess = null, Action<string> onError = null);
        //void ChildExists<T>(string nodeKey, Action<T> onNodeFound = null, Action onNodeMissing = null);
	}
}
