using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using BigSlickChat;
using Xamarin.Facebook.Login;
using Android.App;
using Xamarin.Facebook;
using Android.Runtime;
using Newtonsoft.Json;

[assembly: ExportRenderer(typeof(FacebookLoginButton), typeof(BigSlickChat.Droid.FacebookLoginButtonRendererAndroid))]
namespace BigSlickChat.Droid
{
    public class FacebookLoginButtonRendererAndroid : ButtonRenderer
    {
        public FacebookCallback<LoginResult> fbCallback;

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                e.OldElement.Clicked -= HandleFacebookLoginClicked;
            }

            if (e.NewElement != null)
            {
                //TODO: Remove static ref here and find some way to pass callbacks to newly created activity maybe use fragment instead of activity?
                //tried passing through using the intents extra serialized data fields but cant serialize the callbacks
                FacebookActivity.FbLoginButtonRendererAndroid = this;

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
            Activity activity = this.Context as Activity;
            Android.Content.Intent intent = new Android.Content.Intent(activity, typeof(FacebookActivity));

            Forms.Context.StartActivity(intent);
        }
    }
}
