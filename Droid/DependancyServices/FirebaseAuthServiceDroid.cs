using System;
using System.Diagnostics;
using Android.Gms.Tasks;
using Android.Runtime;
using Android.Util;
using Firebase.Auth;
using Firebase.Iid;
using Java.Lang;
using Xamarin.Forms;
using Android.App;

[assembly: Dependency(typeof(BigSlickChat.Droid.FirebaseAuthServiceDroid))]
namespace BigSlickChat.Droid
{
    public class FirebaseAuthServiceDroid : FirebaseAuthService
    {
        private Activity CurrentActivity
        {
            get
            {
                return Forms.Context as Activity;
            }
        }

        public void CreateUser(string username, string password, Action onCompleteAction, Action<string> onErrorAction)
        {
            if (FirebaseAuth.Instance.CurrentUser == null)
            {
                FirebaseAuth.Instance.CreateUserWithEmailAndPassword(username, password)
                            .AddOnFailureListener(CurrentActivity, new FailureListener(onErrorAction))
                            .AddOnSuccessListener(CurrentActivity, new SuccessListener(onCompleteAction));
            }
            else
            {
                string uid = FirebaseAuth.Instance.CurrentUser.Uid;
            }
        }

        public void SignOut()
        {
            Log.Debug("BIGSLICKAPP", "InstanceID token: " + FirebaseInstanceId.Instance.Token);

            if (FirebaseAuth.Instance.CurrentUser != null)
            {
                FirebaseAuth.Instance.SignOut();
            }

            Log.Debug("BIGSLICKAPP", "InstanceID token: " + FirebaseInstanceId.Instance.Token);
        }

        public void SignIn(string username, string password, Action onCompleteAction, Action<string> onErrorAction)
        {
            if (FirebaseAuth.Instance.CurrentUser == null)
            {
                FirebaseAuth.Instance.SignInWithEmailAndPassword(username, password)
                            .AddOnSuccessListener(CurrentActivity, new SuccessListener(onCompleteAction))
                            .AddOnFailureListener(CurrentActivity, new FailureListener(onErrorAction));
            }
            else
            {
                onErrorAction("Already logged in as " + FirebaseAuth.Instance.CurrentUser.Email);
            }
        }

        public string GetUid()
        {
            if (FirebaseAuth.Instance.CurrentUser != null)
            {
                return FirebaseAuth.Instance.CurrentUser.Uid;
            }

            return null;
        }
    }

    public class FailureListener : Java.Lang.Object, IOnFailureListener
    {
        private Action<string> OnFailureAction;

        public FailureListener(Action<string> onFailureAction)
        {
            OnFailureAction = onFailureAction;
        }

        void IOnFailureListener.OnFailure(Java.Lang.Exception e)
        {
            Debug.WriteLine("FailureListener : " + e.Message);

            if (OnFailureAction != null)
                OnFailureAction(e.Message);
        }
    }

    public class CompleteListener : Java.Lang.Object, IOnCompleteListener
    {
        void IOnCompleteListener.OnComplete(Task task)
        {
            Debug.WriteLine("CompleteListener : ");
        }
    }

    public class SuccessListener : Java.Lang.Object, IOnSuccessListener
    {
        private Action OnSuccessAction;

        public SuccessListener(Action onSuccessAction)
        {
            OnSuccessAction = onSuccessAction;
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            Debug.WriteLine("SuccessListener : " + result.ToString());

            if (OnSuccessAction != null)
                OnSuccessAction();
        }
    }
}
