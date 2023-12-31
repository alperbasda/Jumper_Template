﻿//---------------------------------------------------------------------------------------
//      This code was generated by a Jumper template tool.
//---------------------------------------------------------------------------------------
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CqrsTemplatePack.UI.Web.Controllers.Base
{
    public class MediatrController : Controller
    {
        private IMediator? _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
