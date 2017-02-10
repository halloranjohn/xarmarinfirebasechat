using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace BigSlickChat
{
	public partial class SidebarPage : ContentPage
	{
		public SidebarPage()
		{
			InitializeComponent();

			InitControls();
		}

		private void InitControls()
		{
			InitStackLayout();
            InitAddRemoveStackLayout();
			InitRedButton();
			InitBlueButton();
            InitChatroomStackLayout();
            InitGotoLoginButton();
		}

        private void InitStackLayout()
		{
			stackLayout.Orientation = StackOrientation.Vertical;
            stackLayout.Padding = 20;
			stackLayout.HorizontalOptions = LayoutOptions.Center;
			stackLayout.VerticalOptions = LayoutOptions.Center;
			stackLayout.BackgroundColor = Color.FromHex(UserService.Instance.User.color);
		}

        void InitAddRemoveStackLayout()
        {
            stackLayout.Spacing = 30;

            //entry
            chatroomEntry.Placeholder = "Chatroom Id...";

            //add button
            addChatroomButton.Text = "Add";
            addChatroomButton.Clicked += OnAddChatroom;

            //remove button
            removeChatroomButton.Text = "Remove";
            removeChatroomButton.Clicked += OnRemoveChatroom;
        }

        void OnAddChatroom(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(chatroomEntry.Text))
            {
                //logic to add chat room
                ChatroomModel cr = new ChatroomModel();
                cr.Id = chatroomEntry.Text;
                cr.Title = chatroomEntry.Text;
                cr.UserIds = new List<string>() { DependencyService.Get<FirebaseAuthService>().GetUid() };

                ChatService.Instance.CreateChatroom(cr);
            }
        }

        void OnRemoveChatroom(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(chatroomEntry.Text))
            {
                //logic to remove chat room
                string chatroomId = chatroomEntry.Text;

                ChatService.Instance.RemoveChatroom(chatroomId);
            }
        }

        void InitChatroomStackLayout()
        {
            InitGotoChatroom1Button();
            InitGotoChatroom2Button();
        }

        void InitGotoLoginButton()
        {
            logoutBtn.Text = "Logout";
            logoutBtn.HorizontalOptions = LayoutOptions.Center;

            logoutBtn.Clicked += (sender, e) =>
            {
                Debug.WriteLine("NavigationStack :: " + Navigation.NavigationStack.Count);

                UserService.Instance.Signout();
                //DependencyService.Get<FirebaseAuthService>().SignOut();

                Navigation.PopModalAsync(false);
                Navigation.PushModalAsync(new LoginPage());
            };
        }

		void InitGotoChatroom2Button()
		{
			gotoChatroom2.Text = "Goto Chatroom 2";
			gotoChatroom2.HorizontalOptions = LayoutOptions.Center;

			gotoChatroom2.Clicked += (sender, e) =>
			{
				Debug.WriteLine("NavigationStack :: " + Navigation.NavigationStack.Count);
				Navigation.PopModalAsync(false);
				Navigation.PushModalAsync(new BigSlickChatPage("room2"));	
			};
		}

		void InitGotoChatroom1Button()
		{
			gotoChatroom1.Text = "Goto Chatroom 1";
			gotoChatroom1.HorizontalOptions = LayoutOptions.Center;

			gotoChatroom1.Clicked += (sender, e) =>
			{
				Debug.WriteLine("NavigationStack :: " + Navigation.NavigationStack.Count);
				Navigation.PopModalAsync(false);
				Navigation.PushModalAsync(new BigSlickChatPage("room1"));
			};
		}

		void InitBlueButton()
		{
			blueButton.Text = "Set Colour To Blue";
			blueButton.HorizontalOptions = LayoutOptions.Center;
			blueButton.Clicked += (sender, e) =>
			{
				SetColour("0000FF");
			};
		}

		void InitRedButton()
		{
			redButton.Text = "Set Colour To Red";
			redButton.HorizontalOptions = LayoutOptions.Center;

			redButton.Clicked += (sender, e) =>
			{
				SetColour("FF0000");
			};
		}

		private void SetColour(string color)
		{
			User user = UserService.Instance.User;
			user.color = color;
			UserService.Instance.User = user;
			stackLayout.BackgroundColor = Color.FromHex(UserService.Instance.User.color);
		}
	}
}
