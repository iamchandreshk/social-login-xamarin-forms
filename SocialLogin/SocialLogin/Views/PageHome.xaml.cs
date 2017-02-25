
using Xamarin.Forms;

namespace SocialLogin.Views
{
    public partial class PageHome : ContentPage
    {
        public PageHome()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            if (Constants.Response.facebookProfile != null)
            {
                TextName.Text = Constants.Response.facebookProfile.Name;
            }
            else if (Constants.Response.googleProfile != null)
            {
                TextName.Text = Constants.Response.googleProfile.name;
            }
            else
            {
                Application.Current.MainPage = new PageLogin();
            }
        }
    }
}
