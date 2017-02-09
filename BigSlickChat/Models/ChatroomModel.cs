using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace BigSlickChat
{
    public class ChatroomModel : INotifyPropertyChanged
    {
        private string id;
        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title= value;
                OnPropertyChanged("Title");
            }
        }

        private List<string> userIds;
        public List<string> UserIds
        {
            get
            {
                return userIds;
            }
            set
            {
                userIds = value;
                OnPropertyChanged("UserIds");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            //this.VerifyPropertyName(propertyName);

            if (this.PropertyChanged != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                this.PropertyChanged(this, e);
            }
        }
    }
}
