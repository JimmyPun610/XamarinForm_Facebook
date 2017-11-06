using System;
using Facebook;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using Java.Lang;
using Android.Content;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FacebookTrial.Droid
{
    [Activity(Label = "FacebookTrial", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity , IFacebookCallback
        , GraphRequest.ICallback
    {
        
        private ICallbackManager mFBCallManager;
        private IFacebookCallback mFBCallBack;
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

            FacebookSdk.ApplicationId = "126565711366771";
            FacebookSdk.SdkInitialize(Forms.Context);
            mFBCallManager = CallbackManagerFactory.Create();
            LoginManager.Instance.RegisterCallback(mFBCallManager, this);
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            mFBCallManager.OnActivityResult(requestCode, (int)resultCode, data);
        }
        #region Facebook Login callback
        public void OnCancel()
        {
            MessagingCenter.Send<string, FacebookLoginResult>("FacebookSDK", "OnLogin", new FacebookLoginResult
            {
                isSuccess = false,
                message = "User cancel"
            });
        }

        public void OnError(FacebookException error)
        {
            MessagingCenter.Send<string, FacebookLoginResult>("FacebookSDK", "OnLogin", new FacebookLoginResult
            {
                isSuccess = false,
                message = error.Message
            });
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            var loginResult = (LoginResult)result;
            MessagingCenter.Send<string, FacebookLoginResult>("FacebookSDK", "OnLogin", new FacebookLoginResult
            {
                isSuccess = true,
                message = "Success"
            });
        }
        #endregion
        #region Facebook Get Profile Image callback
        public void OnCompleted(GraphResponse response)
        {
            string url = "";
            try
            {
                url = response.JSONObject.GetJSONObject("data").Get("url").ToString();
            }catch(Java.Lang.Exception ex)
            {

            }
            var profile = Profile.CurrentProfile;
            MessagingCenter.Send<string, FacebookProfile>("FacebookSDK", "OnProfile", new FacebookProfile
            {
                firstName = profile.FirstName,
                id = profile.Id,
                lastName = profile.LastName,
                linkUri = profile.LinkUri.EncodedPath,
                middleName = profile.MiddleName,
                name = profile.Name,
                iconUrl = url
            });
        }
        #endregion  

    }




}

