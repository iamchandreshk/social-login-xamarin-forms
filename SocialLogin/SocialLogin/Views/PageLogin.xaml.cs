using System;

using Xamarin.Forms;

namespace SocialLogin.Views
{
    public partial class PageLogin : ContentPage
    {
        public PageLogin()
        {
            InitializeComponent();
        }

        private void ButtonGoogle_Clicked(object sender, EventArgs e)
        {
            StackContainerMain.IsVisible = true;
            StackContainerChild.Children.Add(new Login.Google.ViewGoogle());
        }

        private void ButtonFacebook_Clicked(object sender, EventArgs e)
        {
            StackContainerMain.IsVisible = true;
            StackContainerChild.Children.Add(new Login.Facebook.ViewFacebook());
        }
    }
}
