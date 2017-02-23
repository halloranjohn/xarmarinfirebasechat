using System;
using System.Collections.Generic;
namespace BigSlickChat
{
	public interface FirebaseDatabaseService
	{
		void AddChildEvent<T>(string nodeKey, Action<T> OnChildAdded = null, Action<T> OnChildRemoved = null, Action<T> OnChildChanged = null);		
        void AddValueEvent<T>(string nodeKey, Action<T> OnValueEvent = null);
        void AddSingleValueEvent<T>(string nodeKey, Action<T> OnValueEvent = null);
		void RemoveValueEvent(string nodeKey);
		void RemoveChildEvent(string nodeKey);
        void SetValue(string nodeKey, object obj, Action onSuccess = null, Action<string> onError = null);
		void SetChildValueByAutoId(string nodeKey, object obj, Action onSuccess = null, Action<string> onError = null);
        void RemoveValue(string nodeKey, Action onSuccess = null, Action<string> onError = null);
		//void Search<T>(string nodeKey, string orderByChild, Action<T> action);
		void Search<T>(string nodeKey, Action<List<T>> action, string orderByChildKey = null, string startAt = null, string endAt = null);
        //void ChildExists<T>(string nodeKey, Action<T> onNodeFound = null, Action onNodeMissing = null);
	}
}
