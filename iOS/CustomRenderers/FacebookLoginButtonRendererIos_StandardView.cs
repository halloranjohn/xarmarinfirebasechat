//using UIKit;
//using Xamarin.Forms;
//using Xamarin.Forms.Platform.iOS;
//using XamarinNativeFacebook;
//using XamarinNativeFacebook.iOS;
//using BigSlickChat;
//using CoreGraphics;


//[assembly: ExportRenderer(typeof(FacebookLoginButton), typeof(FacebookLoginButtonRendererIos))]
//namespace XamarinNativeFacebook.iOS
//{
//    //////////////      NOTE: FORMS FACEBOOK BUTTON SHOULD BE OF TYPE VIEW IN ORDER TO USE THIS RENDERER        //////////////          

//    public class FacebookLoginButtonRendererIos : ViewRenderer<FacebookLoginButton, Facebook.LoginKit.LoginButton>
//    {
//        protected override void OnElementChanged(ElementChangedEventArgs<FacebookLoginButton> e)
//        {
//            base.OnElementChanged(e);
            
//            if(Control == null)
//            {
//                FacebookLoginButton fbLoginBtn = this.Element;

//                Facebook.LoginKit.LoginButton nativeFBLoginBtn = new Facebook.LoginKit.LoginButton();

//                nativeFBLoginBtn.Completed += (object sender, Facebook.LoginKit.LoginButtonCompletedEventArgs args) => 
//                {
//                    if(args.Error != null)
//                    {
//                        if(fbLoginBtn.OnFacebookLoginError != null)
//                        {
//                            fbLoginBtn.OnFacebookLoginError(args.Error.Description);                            
//                        }
//                    }
//                    else if(args.Result.IsCancelled)
//                    {
//                        if(fbLoginBtn.OnFacebookLoginCancelled != null)
//                        {
//                            fbLoginBtn.OnFacebookLoginCancelled();                            
//                        }
//                    }
//                    else if (Facebook.CoreKit.AccessToken.CurrentAccessToken != null)
//                    {
//                        if(fbLoginBtn.OnFacebookLoginSuccess != null)
//                        {
//                            fbLoginBtn.OnFacebookLoginSuccess(Facebook.CoreKit.AccessToken.CurrentAccessToken.TokenString);                            
//                        }
//                    }
//                };

//                SetNativeControl(nativeFBLoginBtn);
//            }
//        }
//    }
//}