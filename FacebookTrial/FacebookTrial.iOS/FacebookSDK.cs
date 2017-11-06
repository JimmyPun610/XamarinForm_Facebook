using Facebook.CoreKit;
using FacebookTrial.iOS;
using Foundation;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(FacebookSDK))]
namespace FacebookTrial.iOS
{
    class FacebookSDK : IFacebook
    {
        public void GetProfileAsync()
        {
            Profile.LoadCurrentProfile((profile, error) =>
            {
                MessagingCenter.Send<string, FacebookProfile>("FacebookSDK", "OnProfile", new FacebookProfile
                {
                    firstName = profile.FirstName,
                    id = profile.UserID,
                    lastName = profile.LastName,
                    linkUri = profile.LinkUrl.AbsoluteString,
                    middleName = profile.MiddleName,
                    name = profile.Name,
                    iconUrl = profile.ImageUrl(ProfilePictureMode.Normal, new CoreGraphics.CGSize(50, 50)).AbsoluteString
                });
            });
           
        }

        public void Login(string[] permission)
        {
            Facebook.LoginKit.LoginManager manager = new Facebook.LoginKit.LoginManager();
            manager.LogInWithReadPermissions(permission, UIApplication.SharedApplication.KeyWindow.RootViewController, (result, error) =>
            {
                if (error != null)
                {
                    MessagingCenter.Send<string, FacebookLoginResult>("FacebookSDK", "OnLogin", new FacebookLoginResult
                    {
                        isSuccess = false,
                        message = error.LocalizedDescription
                    });
                   
                }
                else if (result.IsCancelled)
                {
                    MessagingCenter.Send<string, FacebookLoginResult>("FacebookSDK", "OnLogin", new FacebookLoginResult
                    {
                        isSuccess = false,
                        message = "User cancel"
                    });
                }
                else
                {
                    var accessToken = result.Token;
                    MessagingCenter.Send<string, FacebookLoginResult>("FacebookSDK", "OnLogin", new FacebookLoginResult
                    {
                        isSuccess = true,
                        message = "Success"
                    });
                }
            });

        }
    }
}
