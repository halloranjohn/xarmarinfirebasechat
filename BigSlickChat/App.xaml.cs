using System;
using Xamarin.Forms;
using Com.OneSignal;

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
