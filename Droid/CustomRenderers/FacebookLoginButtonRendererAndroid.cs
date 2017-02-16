using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using BigSlickChat;
using Xamarin.Facebook.Login;
using Android.App;
using Xamarin.Facebook;
using Android.Runtime;

[assembly: ExportRenderer(typeof(FacebookLoginButton), typeof(BigSlickChat.Droid.FacebookLoginButtonRendererAndroid))]
namespace BigSlickChat.Droid
{
    public class FacebookLoginButtonRendererAndroid : ButtonRenderer
    {
        public static ICallbackManager fbCallbackMgr;
        FacebookCallback<LoginResult> fbCallback;

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (fbCallbackMgr == null)
            {
                fbCallbackMgr = CallbackManagerFactory.Create();
            }

            if (e.OldElement != null)
            {
                e.OldElement.Clicked -= HandleFacebookLoginClicked;
                fbCallback = null;
            }

            if (e.NewElement != null)
            {
                FacebookLoginButton fbLoginBtn = e.NewElement as FacebookLoginButton;

                fbCallback = new FacebookCallback<LoginResult>();
                fbCallback.OnSuccessCallback = fbLoginBtn.OnFacebookLoginSuccess;
                fbCallback.OnErrorCallback = fbLoginBtn.OnFacebookLoginError;
                fbCallback.OnCancelCallback = fbLoginBtn.OnFacebookLoginCancelled;

                e.NewElement.Clicked += HandleFacebookLoginClicked;
            }
        }

        private void HandleFacebookLoginClicked(object sender, System.EventArgs e)
        {
            FacebookLoginButton fbLoginBtn = this.Element as FacebookLoginButton;

            LoginManager.Instance.LogInWithReadPermissions(Forms.Context as Activity, fbLoginBtn.ReadPermissions);

            //LoginManager.Instance.LogInWithPublishPermissions(this, Arrays.asList("publish_actions"));

            LoginManager.Instance.RegisterCallback(fbCallbackMgr, fbCallback);
        }
    }

    public class FacebookCallback<LoginResult> : Java.Lang.Object, IFacebookCallback where LoginResult : Xamarin.Facebook.Login.LoginResult
    {
        public Action<string> OnSuccessCallback;
        public Action<string> OnErrorCallback;
        public Action OnCancelCallback;

        public void OnCancel()
        {
            if (OnCancelCallback != null)
            {
                OnCancelCallback();
            }
        }

        public void OnError(FacebookException error)
        {
            if(OnErrorCallback != null)
            {
                OnErrorCallback(error.Message);
            }
        }

        public void OnSuccess(Java.Lang.Object obj)
        {
            LoginResult loginResult = obj.JavaCast<LoginResult>();                                         

            if(loginResult != null)
            {
                if(OnSuccessCallback != null)
                {
                    OnSuccessCallback(loginResult.AccessToken.Token);                    
                }
            }
        }
    }   
}
