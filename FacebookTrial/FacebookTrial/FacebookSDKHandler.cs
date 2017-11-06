
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FacebookTrial
{
    public class FacebookSDKHandler
    {
        public event EventHandler<FacebookLoginResult> OnLogin;
        public event EventHandler<FacebookProfile> OnProfile;
        public FacebookSDKHandler()
        {
            init();
        }
        private void init()
        {
            MessagingCenter.Subscribe<string, FacebookLoginResult>("FacebookSDK", "OnLogin", (sender, arg) =>
            {
                if (arg.isSuccess)
                {
                    if(OnLogin != null)
                        OnLogin.Invoke(this, arg);
                }
            });


            MessagingCenter.Subscribe<string, FacebookProfile>("FacebookSDK", "OnProfile", (sender, arg) =>
            {
                if (OnProfile != null)
                    OnProfile.Invoke(this, arg);
            });
        }

     
    }
}
