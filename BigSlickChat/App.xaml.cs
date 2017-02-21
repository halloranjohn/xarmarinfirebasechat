using System;
using Xamarin.Forms;
using Com.OneSignal;
using System.Diagnostics;

namespace BigSlickChat
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

            InitServices();

			MainPage = new NavigationPage(new LoginPage());
			//MainPage = new LoginPage();

			OneSignal.Current.StartInit("02860e0a-f3ba-421c-9cb9-5c3a3d69d03e").EndInit();

			DependencyService.Get<OneSignalService>().SendMessageToUser("95805d2f-3885-4360-8699-7dba6a244224",
			                                                            "Test Message", () => { Debug.WriteLine("Success"); }, () => Debug.WriteLine("Failure"));
		}

        void InitServices()
        {
            UserService.Instance.Init(DependencyService.Get<FirebaseAuthService>(), 
                                      DependencyService.Get<FirebaseDatabaseService>());
            
            ChatService.Instance.Init(DependencyService.Get<FirebaseDatabaseService>());
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
