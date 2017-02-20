using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace BigSlickChat
{
	public partial class App : Application
	{
		void HandleAction()
		{

		}

		public App()
		{
			InitializeComponent();

            InitServices();

			MainPage = new NavigationPage(new LoginPage());
			//MainPage = new LoginPage();
		}

		void InitServices()
		{
			UserService.Instance.Init(DependencyService.Get<FirebaseAuthService>(),
									  DependencyService.Get<FirebaseDatabaseService>());

			ChatService.Instance.Init(DependencyService.Get<FirebaseDatabaseService>());

			DependencyService.Get<OneSignalService>().Init("4170506a-4340-42ca-b2e2-3bb1ac033b36", "248736078310");

			// Send test message
			DependencyService.Get<OneSignalService>().SendMessageToUser("d0da377d-f93f-4a0c-8768-8a548a84bb99",
																		"Test Message", () => { Debug.WriteLine("Success"); }, () => Debug.WriteLine("Failure"));
		}

        protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
