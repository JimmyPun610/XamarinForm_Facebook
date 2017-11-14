# XamarinForm_Facebook
Facebook SDK for Xamarin Form

Currently implemented Login Function and GetProfile and Profile Picture feature.
The sample use Xamarin Form MessagingCenter and DependencyService with Xamarin Android and Xamarin iOS Facebook Library to do the job.

For the Library

https://components.xamarin.com/view/facebookandroid

https://components.xamarin.com/view/facebookios


1. Setup in Protable Project

  - Create CommonObject class to construct the class for MessagingCenter usage. (CommonObject.cs)
  - Create DependencyService Interface (IFacebook.cs)
  - Subscribe different message in messaging center (FacebookSDKHandler.cs)
  
2. Setup in Android Project

  - Add the things to androidmanifest.xml, modify [FacebookAppID] to your facebook app ID, [App package name] to your app package name
  ```XML
<uses-permission android:name="android.permission.INTERNET" />
	<application android:label="FacebookTrial.Android">
    <meta-data android:name="com.facebook.sdk.ApplicationId"
           android:value="[FacebookAppID]"/>

    <activity android:name=[App package name]"
        android:configChanges=
                "keyboard|keyboardHidden|screenLayout|screenSize|orientation"
        android:label="FacebookTrial" />
    <activity
        android:name="com.facebook.CustomTabActivity"
        android:exported="true">
      <intent-filter>
        <action android:name="android.intent.action.VIEW" />
        <category android:name="android.intent.category.DEFAULT" />
        <category android:name="android.intent.category.BROWSABLE" />
        <data android:scheme="fb[FacebookAppID]" />
      </intent-filter>
    </activity>

  </application>
```

  - In MainActivity.cs, inherit IFacebookCallback and GraphRequest.ICallback
  - Modification in MainActivity.cs
  ```C#
   private ICallbackManager mFBCallManager;
   private IFacebookCallback mFBCallBack;
   protected override void OnCreate(Bundle bundle)
       { 
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

            FacebookSdk.ApplicationId = FACEBOOK_APP_ID;
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
  ```
  
    - Create FacebookSDK class to implement IFacebookSDK logic (FacebookSDK.cs in Android project)
    
3.  Setup iOS project
- Add these to Info.plist
```XML
 <key>LSApplicationQueriesSchemes</key>
    <array>
      <string>fbapi</string>
      <string>fb-messenger-api</string>
      <string>fbauth2</string>
      <string>fbshareextension</string>
    </array>
    <key>CFBundleURLTypes</key>
    <array>
      <dict>
        <key>CFBundleURLSchemes</key>
        <array>
          <string>fb[facebook app id]</string>
        </array>
      </dict>
    </array>
    <key>FacebookAppID</key>
    <string>[facebook app id]</string>
    <key>FacebookDisplayName</key>
    <string>APPNAME</string>
```
- Implement FacebookSDK.cs for IFacebook logic (FacebookSDK in IFacebook)
- In AppDelegate.cs, Add these code behind LoadApplication(new App());
```C#
         Facebook.CoreKit.ApplicationDelegate.SharedInstance.Init();
            Facebook.CoreKit.ApplicationDelegate.SharedInstance.FinishedLaunching(app, options);
```
- Add this code the AppDelegate.cs
```C#
  public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            bool handled = Facebook.CoreKit.ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation);
            return handled;
        }
```
