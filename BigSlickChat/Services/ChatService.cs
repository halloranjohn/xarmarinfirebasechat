using System.Collections.Generic;
using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace BigSlickChat
{
	public class ChatService
	{
		public const string ROOMS_URL_PREFIX = "rooms/";
		public const string USER_IDS_URL_SUFFIX = "/UserIds";

		private static ChatService instance;
		private FirebaseDatabaseService fbDatabaseService;

		protected ChatService()
		{
		}

		public static ChatService Instance
		{
			get
			{
				if(instance == null)
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
			Action<ChatroomModel> onValueEvent = (ChatroomModel chatroomDataExisting) =>
			{
				Action onSetValueSuccess = () =>
					{
						if(onSuccess != null)
							onSuccess(true);
					};

				Action<string> onSetValueError = (string errorDesc) =>
				{
					if(onError != null)
						onError(errorDesc);
				};

				if(chatroomDataExisting == null)
				{
					fbDatabaseService.SetValue(ROOMS_URL_PREFIX + chatroomDataToAdd.Id, chatroomDataToAdd, onSetValueSuccess, onSetValueError);
				}
				else
				{
					//Room already exists just add users
					foreach(string userId in chatroomDataToAdd.UserIds)
					{
						if(!chatroomDataExisting.UserIds.Contains(userId))
						{
							chatroomDataExisting.UserIds.Add(userId);
						}
					}

					fbDatabaseService.SetValue(ROOMS_URL_PREFIX + chatroomDataToAdd.Id + USER_IDS_URL_SUFFIX, chatroomDataExisting.UserIds, onSetValueSuccess, onSetValueError);
				}
			};

			fbDatabaseService.AddSingleValueEvent(ROOMS_URL_PREFIX + chatroomDataToAdd.Id, onValueEvent);
		}

		public void RemoveChatroom(string chatroomId, Action onSuccess = null, Action<string> onError = null)
		{
			Action onSetValueSuccess = () =>
				{
					if(onSuccess != null)
						onSuccess();
				};

			Action<string> onSetValueError = (string errorDesc) =>
			{
				if(onError != null)
					onError(errorDesc);
			};

			fbDatabaseService.RemoveValue(ROOMS_URL_PREFIX + chatroomId, onSetValueSuccess, onSetValueError);
		}

		public void SearchChats(string text)
		{
			string path = BigSlickChatPage.MESSAGES_URL_PREFIX + "Bri/" + BigSlickChatPage.MESSAGES_URL_SUFFIX;
			fbDatabaseService.Search<ChatItem>(path, OnSearchReturn, "Message", "PQ", "R");
		}

		public void OnSearchReturn(List<ChatItem> items)
		{
			Debug.WriteLine("OnSearchReturn. Num items: " + items.Count);

			foreach(ChatItem item in items)
			{
				Debug.WriteLine(JsonConvert.SerializeObject(item));
			}

			//list.ScrollTo(messages[messages.Count - 1], ScrollToPosition.End, true);

			//entry.Text = null;
		}
	}
}
