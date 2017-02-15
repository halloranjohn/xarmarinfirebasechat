using System;
using Xamarin.Forms;

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
