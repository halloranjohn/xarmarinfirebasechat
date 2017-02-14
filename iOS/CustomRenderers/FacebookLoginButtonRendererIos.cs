using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinNativeFacebook;
using XamarinNativeFacebook.iOS;
using BigSlickChat;
using CoreGraphics;


[assembly: ExportRenderer(typeof(FacebookLoginButton), typeof(FacebookLoginButtonRendererIos))]
namespace XamarinNativeFacebook.iOS
{
    public class FacebookLoginButtonRendererIos : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            UIButton nativeBtn = Control;

            if(e.OldElement != null)
            {
                nativeBtn.TouchUpInside -= HandleFacebookLoginClicked;
            }

            if (e.NewElement != null)
            {
                nativeBtn.TouchUpInside += HandleFacebookLoginClicked;
            }
        }

        private void HandleFacebookLoginClicked(object sender, System.EventArgs e)
        {
            FacebookLoginButton fbLoginBtn = Element as FacebookLoginButton;

            Facebook.LoginKit.LoginManager fbLoginMgr = new Facebook.LoginKit.LoginManager();

            fbLoginMgr.LogInWithReadPermissions(fbLoginBtn.ReadPermissions, this.ViewController, (Facebook.LoginKit.LoginManagerLoginResult result, Foundation.NSError error) =>
            {
                if (error != null)
                {
                    if(fbLoginBtn.OnFacebookLoginError != null)
                    {
                        fbLoginBtn.OnFacebookLoginError(error.Description);                        
                    }
                }
                else if (result.IsCancelled)
                {
                    if(fbLoginBtn.OnFacebookLoginCancelled != null)
                    {                        
                        fbLoginBtn.OnFacebookLoginCancelled();
                    }
                }
                else
                {
                    if (Facebook.CoreKit.AccessToken.CurrentAccessToken != null)
                    {
                        if(fbLoginBtn.OnFacebookLoginSuccess != null)
                        {
                            fbLoginBtn.OnFacebookLoginSuccess(Facebook.CoreKit.AccessToken.CurrentAccessToken.TokenString);                            
                        }
                    }
                }
            });
        }
    }
}