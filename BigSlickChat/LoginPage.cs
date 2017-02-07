using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BigSlickChat
{
	public partial class LoginPage : ContentPage
	{
		public LoginPage()
		{
			InitializeComponent();

			InitControls();
		}

		private void InitControls()
		{
			InitStackLayout();
			InitUsernameEntry();
			InitPasswordEntry();
			InitButtonsLayout();
			InitSignupButton();
			InitLoginButton();
			InitSignoutButton();
		}

		private void InitStackLayout()
		{
			stackLayout.Orientation = StackOrientation.Vertical;
			stackLayout.HorizontalOptions = LayoutOptions.Fill;
			stackLayout.VerticalOptions = LayoutOptions.FillAndExpand;
			stackLayout.BackgroundColor = Color.Blue;
		}

		private void InitUsernameEntry()
		{
			usernameEntry.BackgroundColor = Color.Red;
			usernameEntry.VerticalOptions = LayoutOptions.Center;
			usernameEntry.HorizontalOptions = LayoutOptions.Fill;
		}

		private void InitPasswordEntry()
		{
			passwordEntry.BackgroundColor = Color.Red;
			passwordEntry.VerticalOptions = LayoutOptions.End;
			passwordEntry.HorizontalOptions = LayoutOptions.Fill;
		}

		private void InitButtonsLayout()
		{
			buttonStackLayout.Orientation = StackOrientation.Horizontal;
			buttonStackLayout.HorizontalOptions = LayoutOptions.Fill;
			buttonStackLayout.VerticalOptions = LayoutOptions.EndAndExpand;
			buttonStackLayout.BackgroundColor = Color.White;
		}

		private void InitSignupButton()
		{
			signupButton.Text = "Signup";
			signupButton.HorizontalOptions = LayoutOptions.Start;

			signupButton.Clicked += (sender, e) =>
			{
				DependencyService.Get<FirebaseAuthService>().CreateUser(usernameEntry.Text, passwordEntry.Text, () =>
				{
					DependencyService.Get<FirebaseService>().SetValue("users/" + DependencyService.Get<FirebaseAuthService>().GetUid() + "/color", Color.Red.ToString());
					Navigation.PushModalAsync(new BigSlickChatPage());
				});

			};
		}

		private void InitLoginButton()
		{
			loginButton.Text = "Login";
			loginButton.HorizontalOptions = LayoutOptions.End;

			loginButton.Clicked += (sender, e) =>
			{
				DependencyService.Get<FirebaseAuthService>().SignIn(usernameEntry.Text, passwordEntry.Text, () =>
				{
					Navigation.PushModalAsync(new BigSlickChatPage());
				});
			};
		}

		private void InitSignoutButton()
		{
			signOutButton.Text = "Logout";
			signOutButton.HorizontalOptions = LayoutOptions.End;

			signOutButton.Clicked += (sender, e) =>
			{
				DependencyService.Get<FirebaseAuthService>().SignOut();
			};
		}
	}
}
