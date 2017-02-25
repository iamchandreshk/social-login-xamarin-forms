namespace SocialLogin.Constants
{
    public class Response
    {
        public static Views.Login.Facebook.FacebookProfile facebookProfile { get; set; }
        public static Views.Login.Google.GoogleProfile googleProfile { get; set; }
    }

    public class Facebook
    {
        public static string ClientId = "";
        public static string Scope = "";
    }

    public class Google
    {
        public static string ClientId = "";
    }
}
