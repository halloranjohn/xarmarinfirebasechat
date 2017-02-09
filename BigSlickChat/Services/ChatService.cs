using System.Collections.Generic;
using System;
using System.Diagnostics;

namespace BigSlickChat
{
    public class ChatService
    {
        public const string ROOMS_PREFIX_URL = "rooms/";

        private static ChatService instance;
        private FirebaseDatabaseService fbDatabaseService;

        protected ChatService()
        {            
        }

        public static ChatService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ChatService();
                }

                return instance;
            }
        }

        public void Init(FirebaseDatabaseService fbDatabaseService)
        {
            this.fbDatabaseService = fbDatabaseService;
        }

        public void CreateChatroom(ChatroomModel chatroomDataToAdd, Action<bool> onSuccess = null, Action<string> onError = null)
        {
            Action<ChatroomModel> onNodeFound = (chatroomDataExisting) => 
            {
                //Room already exists just add users
                foreach(string userId in chatroomDataToAdd.UserIds)
                {
                    if(!chatroomDataExisting.UserIds.Contains(userId))
                    {
                        chatroomDataExisting.UserIds.Add(userId);
                    }
                }

                if(onSuccess != null)
                {
                    onSuccess(false);                    
                }
            };

            Action onNodeMissing = () => 
            {          
                Action onSetValueSuccess = () => 
                {
                    if (onSuccess != null)
                        onSuccess(true);
                };

                Action<string> onSetValueError = (string errorDesc) => 
                {
                    if (onError != null)
                        onError(errorDesc);
                };

                fbDatabaseService.SetValue(ROOMS_PREFIX_URL + chatroomDataToAdd.Id, chatroomDataToAdd, onSetValueSuccess, onSetValueError); 

            };

            fbDatabaseService.ChildExists(ROOMS_PREFIX_URL + chatroomDataToAdd.Id, onNodeFound, onNodeMissing);
        }

        public void RemoveChatroom(string chatroomId, Action onSuccess = null, Action<string> onError = null)
        {
            Action onSetValueSuccess = () =>
                {
                    if (onSuccess != null)
                        onSuccess();
                };

            Action<string> onSetValueError = (string errorDesc) =>
            {
                if (onError != null)
                    onError(errorDesc);
            };

            fbDatabaseService.RemoveValue(ROOMS_PREFIX_URL + chatroomId, onSetValueSuccess, onSetValueError);
        }
    }
}
