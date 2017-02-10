using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace BigSlickChat
{
	public class User
	{
        public string id;
        public List<string> roomIds;
        public string color;

        public User(string id, List<string> roomIds, string color)
        {
            this.id = id;
            this.roomIds = roomIds;
            this.color = color;
        }

        public void AddChatroom(string roomId)
        {
            if(roomIds == null)
            {
                roomIds = new List<string>();    
            }

            if(!roomIds.Contains(roomId))
            {
                roomIds.Add(roomId);
            }
        }

        public void RemoveChatroom(string roomId)
        {
            if (roomIds.Contains(roomId))
            {
                roomIds.Remove(roomId);
            }
        }
	}
}
