using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CqrsTemplatePack.UI.Api.Controllers.Base
{
    public class MediatrController : Controller
    {
        private IMediator? _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
