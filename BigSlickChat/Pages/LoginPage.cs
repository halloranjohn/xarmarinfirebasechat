using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Diagnostics;

namespace BigSlickChat
{
	public partial class LoginPage : ContentPage
	{
		public LoginPage()
		{
			InitializeComponent();

            InitControls();

            if (UserSignedIn())
            {
                OnAuthComplete(false);
            }
		}

		bool UserSignedIn()
		{
            return DependencyService.Get<FirebaseAuthService>().GetUid() != null;
		}

		private void InitControls()
		{
			InitStackLayout();
			InitUsernameEntry();
			InitPasswordEntry();
			InitButtonsLayout();
			InitSignupButton();
			InitLoginButton();
            InitFacebookLoginButton();
			InitSignoutButton();
		}

		private void InitStackLayout()
		{
			stackLayout.Orientation = StackOrientation.Vertical;
			stackLayout.HorizontalOptions = LayoutOptions.Fill;
			stackLayout.VerticalOptions = LayoutOptions.Center;
			stackLayout.BackgroundColor = Color.Blue;
		}

		private void InitUsernameEntry()
		{
			usernameEntry.BackgroundColor = Color.Red;
			usernameEntry.VerticalOptions = LayoutOptions.Center;
			usernameEntry.HorizontalOptions = LayoutOptions.Fill;
			usernameEntry.Placeholder = "email";
		}

		private void InitPasswordEntry()
		{
			passwordEntry.BackgroundColor = Color.Red;
			passwordEntry.VerticalOptions = LayoutOptions.End;
			passwordEntry.HorizontalOptions = LayoutOptions.Fill;
			passwordEntry.Placeholder = "password";
		}

		private void InitButtonsLayout()
		{
            buttonStackLayout.Padding = 10;
			buttonStackLayout.HorizontalOptions = LayoutOptions.Center;
            buttonStackLayout.VerticalOptions = LayoutOptions.Start;
			buttonStackLayout.BackgroundColor = Color.White;
		}

		private void InitSignupButton()
		{
			signupButton.Text = "Signup";
            signupButton.HorizontalOptions = LayoutOptions.Center;

			signupButton.Clicked += (sender, e) =>
			{
				DependencyService.Get<FirebaseAuthService>().CreateUser(usernameEntry.Text, passwordEntry.Text, OnSignupCompleteAction, OnSignupErrorAction);
			};
		}

		private void InitLoginButton()
		{
			loginButton.Text = "Email Login";
            loginButton.HorizontalOptions = LayoutOptions.Center;

			loginButton.Clicked += (sender, e) =>
			{
				DependencyService.Get<FirebaseAuthService>().SignIn(usernameEntry.Text, passwordEntry.Text, OnLoginCompleteAction, OnLoginErrorAction);
			};
		}

        private void InitFacebookLoginButton()
        {
            facebookLoginButton.Text = "Facebook Login";
            facebookLoginButton.HorizontalOptions = LayoutOptions.Center;

            facebookLoginButton.OnFacebookLoginSuccess = (token) =>
            {
                DependencyService.Get<FirebaseAuthService>().SignInWithFacebook(token, OnLoginCompleteAction, OnLoginErrorAction);
            };
        }

		private void InitSignoutButton()
		{
			signOutButton.Text = "Logout";
            signOutButton.HorizontalOptions = LayoutOptions.Center;

			signOutButton.Clicked += (sender, e) =>
			{
                UserService.Instance.Signout();
				//DependencyService.Get<FirebaseAuthService>().SignOut();
			};
		}

		public void OnAuthComplete(bool isNewUser)
		{
			UserService.Instance.UserAuthenticated(isNewUser, OnUserDataSet);
		}

		public void OnUserDataSet()
		{
			//Navigation.PopModalAsync(true);
            Navigation.PushModalAsync(new SidebarPage());
			//Navigation.PushModalAsync(new BigSlickChatPage("room1"));
		}

		void OnSignupErrorAction(string obj)
		{
			this.DisplayAlert("Signup Error", obj, "OK");
		}

		void OnSignupCompleteAction()
		{
			OnAuthComplete(true);
		}

		void OnLoginErrorAction(string obj)
		{
			this.DisplayAlert("Login Error", obj, "OK");
		}

		void OnLoginCompleteAction()
		{
			OnAuthComplete(false);
		}

	}
}
