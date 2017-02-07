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
		}

		private void InitStackLayout()
		{
			stackLayout.Orientation = StackOrientation.Vertical;
			stackLayout.HorizontalOptions = LayoutOptions.Fill;
			stackLayout.VerticalOptions = LayoutOptions.FillAndExpand;
			stackLayout.BackgroundColor = Color.FromHex(UserService.Instance.User.color);
		}

		void InitGotoChatroom2Button()
		{
			gotoChatroom2.Text = "Goto Chatroom 2";
			gotoChatroom2.HorizontalOptions = LayoutOptions.End;

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
			gotoChatroom1.HorizontalOptions = LayoutOptions.End;

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
			blueButton.HorizontalOptions = LayoutOptions.End;

			blueButton.Clicked += (sender, e) =>
			{
				SetColour("0000FF");
			};
		}

		void InitRedButton()
		{
			redButton.Text = "Set Colour To Red";
			redButton.HorizontalOptions = LayoutOptions.End;

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
