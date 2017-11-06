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
using FacebookTrial.Droid;
using Xamarin.Facebook.Login;
using Xamarin.Forms;
using Xamarin.Facebook;

[assembly: Xamarin.Forms.Dependency(typeof(FacebookSDK))]
namespace FacebookTrial.Droid
{

    public class FacebookSDK : IFacebook
    {
        public ProfileTracker profileTracker;

        public void Login(string[] permission)
        {
            LoginManager.Instance.LogInWithReadPermissions((MainActivity)Forms.Context, permission);
        }
        public void GetProfileAsync()
        {
            //var profile = Profile.CurrentProfile;
            Bundle param = new Bundle();
            param.PutBoolean("redirect", false);
            new GraphRequest(AccessToken.CurrentAccessToken, string.Format("me/picture"), param, HttpMethod.Get, (MainActivity)Forms.Context).ExecuteAsync();
        }



        class CustomProfileTracker : ProfileTracker
        {
            public delegate void CurrentProfileChangedDelegate(Profile oldProfile, Profile currentProfile);

            public CurrentProfileChangedDelegate HandleCurrentProfileChanged { get; set; }

            protected override void OnCurrentProfileChanged(Profile oldProfile, Profile currentProfile)
            {
                var p = HandleCurrentProfileChanged;
                if (p != null)
                    p(oldProfile, currentProfile);
            }
        }
    }
}