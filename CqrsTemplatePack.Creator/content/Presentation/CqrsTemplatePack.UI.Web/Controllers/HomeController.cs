﻿//---------------------------------------------------------------------------------------
//      This code was generated by a Jumper template tool.
//---------------------------------------------------------------------------------------
using CqrsTemplatePack.UI.Web.Helpers;
using CqrsTemplatePack.UI.Web.Controllers.Base;
using CqrsTemplatePack.UI.Web.ActionFilters;
using Microsoft.AspNetCore.Mvc;

namespace CqrsTemplatePack.UI.Web.Controllers;

[AuthorizeHandler]
public class HomeController : MediatrController
{
    public async Task<IActionResult> Index()
    {
        return View();
    }
}