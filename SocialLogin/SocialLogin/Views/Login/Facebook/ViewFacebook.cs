using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SocialLogin.Views.Login.Facebook
{
    public class ViewFacebook : ContentView
    {
        private WebView webView;

        public ViewFacebook()
        {
            var apiRequest = "https://www.facebook.com/dialog/oauth?client_id=" + Constants.Facebook.ClientId +
                "&display=popup&response_type=token&redirect_uri=http://www.facebook.com/connect/login_success.html";
            webView = new WebView
            {
                Source = apiRequest,
                HeightRequest = 500
            };
            webView.Navigated += WebViewOnNavigated;
            Content = webView;
        }
        private void WebViewOnNavigated(object sender, WebNavigatedEventArgs e)
        {
            var accessToken = ExtractAccessTokenFromUrl(e.Url);
            if (accessToken != "")
            {
                webView.IsVisible = false;
                var FacebookProfile = GetFacebookProfileAsync(accessToken).Result;
                if (FacebookProfile != null)
                {
                    Constants.Response.facebookProfile = FacebookProfile;
                    Application.Current.MainPage = new PageHome();
                }
            }
        }

        private string ExtractAccessTokenFromUrl(string url)
        {
            if (url.Contains("access_token") && url.Contains("&expires_in="))
            {
                var at = url.Replace("https://www.facebook.com/connect/login_success.html#access_token=", "");
                if (Xamarin.Forms.Device.OS == TargetPlatform.WinPhone || Xamarin.Forms.Device.OS == TargetPlatform.Windows)
                {
                    at = url.Replace("http://www.facebook.com/connect/login_success.html#access_token=", "");
                }
                var accessToken = at.Remove(at.IndexOf("&expires_in="));
                return accessToken;
            }
            return string.Empty;
        }

        public async Task<FacebookProfile> GetFacebookProfileAsync(string accessToken)
        {
            var requestUrl = "https://graph.facebook.com/v2.7/me/?fields=" + Constants.Facebook.Scope + "&access_token=" + accessToken;
            var httpClient = new HttpClient();
            var userJson = await httpClient.GetStringAsync(requestUrl);
            var facebookProfile = JsonConvert.DeserializeObject<FacebookProfile>(userJson);
            return facebookProfile;
        }
    }
}
