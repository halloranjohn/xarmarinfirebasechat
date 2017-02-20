using System;
using Xamarin.Facebook;
using Android.Runtime;
using Android.OS;
using System.Runtime.Serialization;
namespace BigSlickChat.Droid
{
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
            if (OnErrorCallback != null)
            {
                OnErrorCallback(error.Message);
            }
        }

        public void OnSuccess(Java.Lang.Object obj)
        {
            LoginResult loginResult = obj.JavaCast<LoginResult>();

            if (loginResult != null)
            {
                if (OnSuccessCallback != null)
                {
                    OnSuccessCallback(loginResult.AccessToken.Token);
                }
            }
        }
    }
}
