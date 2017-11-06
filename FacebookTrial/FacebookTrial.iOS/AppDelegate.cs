using System;
using System.Collections.Generic;
using System.Linq;
using Facebook;
using Foundation;
using UIKit;

namespace FacebookTrial.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            Facebook.CoreKit.ApplicationDelegate.SharedInstance.Init();
            Facebook.CoreKit.ApplicationDelegate.SharedInstance.FinishedLaunching(app, options);
          
            return base.FinishedLaunching(app, options);
        }



        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            bool handled = Facebook.CoreKit.ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation);
            return handled;
        }
    }
}
