﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace FacebookTrial
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var facebookSdkHandler = new FacebookSDKHandler();
            MainPage = new FacebookTrial.MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
