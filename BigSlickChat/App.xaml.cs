using Xamarin.Forms;

namespace BigSlickChat
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new BigSlickChatPage();
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
