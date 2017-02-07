using System;
using System.Collections.Generic;

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
			stackLayout.BackgroundColor = Color.Blue;
		}

		void InitGotoChatroom2Button()
		{
			gotoChatroom2.Text = "Goto Chatroom 2";
			gotoChatroom2.HorizontalOptions = LayoutOptions.End;

			gotoChatroom2.Clicked += (sender, e) =>
			{
				Navigation.PushModalAsync(new BigSlickChatPage());	
			};
		}

		void InitGotoChatroom1Button()
		{
			gotoChatroom1.Text = "Goto Chatroom 1";
			gotoChatroom1.HorizontalOptions = LayoutOptions.End;

			gotoChatroom1.Clicked += (sender, e) =>
			{
				Navigation.PushModalAsync(new BigSlickChatPage());
			};
		}

		void InitBlueButton()
		{
			blueButton.Text = "Set Colour To Blue";
			blueButton.HorizontalOptions = LayoutOptions.End;

			blueButton.Clicked += (sender, e) =>
			{
				DependencyService.Get<FirebaseService>().SetValue("users/" + DependencyService.Get<FirebaseAuthService>().GetUid() + "/color", "0000FF");

			};
		}

		void InitRedButton()
		{
			redButton.Text = "Set Colour To Red";
			redButton.HorizontalOptions = LayoutOptions.End;

			redButton.Clicked += (sender, e) =>
			{
				DependencyService.Get<FirebaseService>().SetValue("users/" + DependencyService.Get<FirebaseAuthService>().GetUid() + "/color", "FF0000");

			};
		}
	}
}
