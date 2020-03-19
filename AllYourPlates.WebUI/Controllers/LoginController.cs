using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;

public class LoginController : Controller
{
    public ActionResult Index()
    {
        using (var openid = new OpenIdRelyingParty())
        {
            var response = openid.GetResponse();
            if (response != null)
            {
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:
                        {
                            var claimsResponse = response.GetExtension<ClaimsResponse>();
                            var username = response.ClaimedIdentifier;
                            if (claimsResponse != null && !string.IsNullOrEmpty(claimsResponse.Email))
                            {
                                username = claimsResponse.Email;
                            }
                            var cookie = FormsAuthentication.GetAuthCookie(username, false);
                            Response.AppendCookie(cookie);
                            break;
                        }
                    case AuthenticationStatus.Canceled:
                        {
                            TempData["message"] = "Login was cancelled at the provider";
                            break;
                        }
                    case AuthenticationStatus.Failed:
                        {
                            TempData["message"] = "Login failed using the provided OpenID identifier";
                            break;
                        }
                }
                return RedirectToAction("index", "home");
            }
            return View();
        }
    }

    [HttpPost]
    public ActionResult Index(string loginIdentifier)
    {
        if (string.IsNullOrEmpty(loginIdentifier) || !Identifier.IsValid(loginIdentifier))
        {
            ModelState.AddModelError(
                "loginIdentifier",
                "The specified login identifier is invalid"
            );
            // The login identifier entered by the user was incorrect
            // redisplay the partial view with error messages so that 
            // the suer can fix them:
            return View();
        }
        else
        {
            using (var openid = new OpenIdRelyingParty())
            {
                var request = openid.CreateRequest(
                    Identifier.Parse(loginIdentifier)
                );
                request.AddExtension(new ClaimsRequest
                {
                    Email = DemandLevel.Require
                });
                var response = request.RedirectingResponse;
                if (response.Status == HttpStatusCode.Redirect)
                {
                    // We need to redirect to the OpenId provider for authentication
                    // but because this action was invoked using AJAX we need
                    // to return JSON here:
                    return Json(new { redirectUrl = response.Headers[HttpResponseHeader.Location] });
                }
                return request.RedirectingResponse.AsActionResult();
            }
        }
    }

    [Authorize]
    [HttpPost]
    public ActionResult SignOut()
    {
        FormsAuthentication.SignOut();
        return RedirectToAction("index", "home");
    }
}