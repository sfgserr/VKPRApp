using Android.App;
using Android.Content;
using Android.Content.PM;

namespace VKPRApp.Platforms.Android
{
    [Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTop, Exported = true)]
    [IntentFilter(new[] { Intent.ActionView },
              Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
              DataScheme = Constants.CallBackScheme)]
    class WebAuthenticationCallbackAcivity : WebAuthenticatorCallbackActivity
    {
        
    }
}
