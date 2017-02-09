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
			InitRedButton();
			InitBlueButton();
			InitGotoChatroom1Button();
			InitGotoChatroom2Button();
            InitGotoLoginButton();
		}

		private void InitStackLayout()
		{
			stackLayout.Orientation = StackOrientation.Vertical;
			stackLayout.HorizontalOptions = LayoutOptions.Center;
			stackLayout.VerticalOptions = LayoutOptions.Center;
			stackLayout.BackgroundColor = Color.FromHex(UserService.Instance.User.color);
		}

        void InitGotoLoginButton()
        {
            gotoLogin.Text = "Goto Login";
            gotoLogin.HorizontalOptions = LayoutOptions.End;

            gotoLogin.Clicked += (sender, e) =>
            {
                Debug.WriteLine("NavigationStack :: " + Navigation.NavigationStack.Count);
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
