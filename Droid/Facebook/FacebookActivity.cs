
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using Newtonsoft.Json;

namespace BigSlickChat.Droid
{
    [Activity(Label = "FacebookActivity")]
    public class FacebookActivity : Activity
    {
        public static FacebookLoginButtonRendererAndroid FbLoginButtonRendererAndroid;
        public ICallbackManager fbCallbackMgr;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            fbCallbackMgr = CallbackManagerFactory.Create();

            FacebookLoginButton fbLoginBtn = FbLoginButtonRendererAndroid.Element as FacebookLoginButton;

            LoginManager.Instance.LogInWithReadPermissions(this, fbLoginBtn.ReadPermissions);

            if(fbLoginBtn.WritePermissions != null)
            {
                LoginManager.Instance.LogInWithPublishPermissions(this, fbLoginBtn.WritePermissions);                
            }

            LoginManager.Instance.RegisterCallback(fbCallbackMgr, FbLoginButtonRendererAndroid.fbCallback);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            fbCallbackMgr.OnActivityResult(requestCode, (int)resultCode, data);

            this.Finish();
        }
    }
}


