using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace VKPRApp.Api.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        const string callbackScheme = "vkprapp";

        [HttpGet("{scheme}")]
        public async Task Login([FromRoute] string scheme)
        {
            await HttpContext.ChallengeAsync(scheme, new AuthenticationProperties()
            {
                RedirectUri = Url.Action(nameof(LoginCallBack), "Auth", new { scheme })
            });
        }

        public async Task LoginCallBack(string scheme)
        {
            var auth = await HttpContext.AuthenticateAsync(scheme);

            var claims = auth.Principal.Identities.FirstOrDefault()?.Claims;
            var userId = string.Empty;
            userId = claims?.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var qs = new Dictionary<string, string>
            {
                { "access_token", auth.Properties.GetTokenValue("access_token") },
                { "refresh_token", auth.Properties.GetTokenValue("refresh_token") ?? string.Empty },
                { "expires_in", (auth.Properties.ExpiresUtc?.ToUnixTimeSeconds() ?? -1).ToString() },
                { "user_id", userId }
            };

            var url = callbackScheme + "://#" + string.Join(
                "&",
                qs.Where(kvp => !string.IsNullOrEmpty(kvp.Value) && kvp.Value != "-1")
                .Select(kvp => $"{WebUtility.UrlEncode(kvp.Key)}={WebUtility.UrlEncode(kvp.Value)}"));

            HttpContext.Response.Redirect(url);
        }
    }
}
