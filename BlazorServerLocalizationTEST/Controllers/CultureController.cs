using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorServerLocalizationTEST.Resources;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorServerLocalization.Controllers;
[Route("/[controller]")]
[ApiController]
public class CultureController : ControllerBase
{
    [HttpGet("{language}")]
    public ActionResult SetCultureLanguage(string language)
    {
        Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, 
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(language)));
        return Redirect("/");
    }
}
