using System;
using System.Net.Http;

using Xamarin.Forms;

namespace SocialLogin.Views.Login.Google
{
    public class ViewGoogle : ContentView
    {
        private WebView webview;

        public ViewGoogle()
        {
            string url = "https://accounts.google.com/o/oauth2/auth?";
            url += "scope=https://www.googleapis.com/auth/userinfo.email&";
            url += "state=%2Fprofile&";
            url += "redirect_uri=http://www.app4society.com&";
            url += "response_type=token&";
            url += "client_id=" + Constants.Google.ClientId;
            webview = new WebView
            {
                Source = url,
                HeightRequest = 500
            };
            webview.Navigating += Webview_Navigating;
            Content = webview;
        }

        private void Webview_Navigating(object sender, WebNavigatingEventArgs e)
        {
            var url = e.Url;
            if (url.Contains("access_token="))
            {
                webview.IsVisible = false;
                Uri access = new Uri(e.Url);
                var acc = access.Query;
                string[] access_token = System.Text.RegularExpressions.Regex.Split(url, "access_token");
                string URI = "https://www.googleapis.com/oauth2/v1/userinfo?access_token" + access_token[1];
                HttpClient client = new HttpClient();
                using (var response = client.GetAsync(URI).Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var GoogleProfile = response.Content.ReadAsStringAsync().Result;
                        Constants.Response.googleProfile = Newtonsoft.Json.JsonConvert.DeserializeObject<GoogleProfile>(GoogleProfile);
                        Application.Current.MainPage = new PageHome();
                    }
                }
            }
        }
    }
}
