using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FacebookTrial
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
          
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IFacebook>().Login(new string[] { "public_profile" });
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            DependencyService.Get<IFacebook>().GetProfileAsync();
        }
    }
}
