using System;
using Xamarin.Forms;
namespace BigSlickChat
{
    public class FacebookLoginButton : Button
    {        
        public Action<string> OnFacebookLoginSuccess { get; set; }
        public Action<string> OnFacebookLoginError { get; set; }
        public Action OnFacebookLoginCancelled { get; set; }

        private string[] readPermissions = new string[] { "public_profile", "email", "user_friends" };

        public string[] ReadPermissions
        {
            get
            {
                return readPermissions;
            }
            set
            {
                readPermissions = value;
            }
        }

        private string[] writePermissions = null; //new string[] { "publish_actions" };

        public string[] WritePermissions
        {
            get
            {
                return writePermissions;
            }
            set
            {
                writePermissions = value;
            }
        }
    }
}
