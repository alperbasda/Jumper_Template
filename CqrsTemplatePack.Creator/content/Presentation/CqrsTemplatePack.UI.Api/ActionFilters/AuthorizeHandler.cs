using CqrsTemplatePack.Application.Features.Auth.Commands.RefreshToken;
using CqrsTemplatePack.Common.IdentityConfigurations;
using Core.ApiHelpers.JwtHelper.Encyption;
using Core.ApiHelpers.JwtHelper.Models;
using Core.CrossCuttingConcerns.Exceptions.Types;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CqrsTemplatePack.UI.Api.ActionFilters;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeHandlerAttribute : Attribute, IAuthorizationFilter
{
    private string[] _requiredScopes { get; set; }
    private const string SUPER_USER_SCOPE = "admin_user_scope";

    public AuthorizeHandlerAttribute(params string[] scopes)
    {
        _requiredScopes = scopes;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        TokenParameters tokenParameters = context.HttpContext.RequestServices.GetService<TokenParameters>()!;

        tokenParameters.IpAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString() ?? " ";

        if (context.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues jwt))
        {
            jwt = CreateTokenFromRefreshTokenIfTokenInvalid(context.HttpContext, jwt);


            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt.ToString().Split(' ')[1]);

            if (token == null)
            {
                throw new AuthorizationException(string.Empty, "Geçersiz Kullanıcı. Lütfen Yeni Token Talep Edin.");
            }

            tokenParameters.AccessToken = jwt;

            var identity = new ClaimsIdentity(token!.Claims, "basic");
            context.HttpContext.User = new ClaimsPrincipal(identity);
            if (!string.IsNullOrEmpty(context.HttpContext?.User?.Identity?.Name))
                tokenParameters.UserName = context.HttpContext.User.Identity.Name;

            tokenParameters.UserId = Guid.Empty;
            var userIdClaim = token.Claims.FirstOrDefault(w => w.Type == ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userIdClaim))
                tokenParameters.UserId = Guid.Parse(userIdClaim);

            var clients = token.Claims.FirstOrDefault(w => w.Type == JwtRegisteredClaimNames.Sub)?.Value;
            if (!string.IsNullOrEmpty(clients))
                tokenParameters.ClientIds = clients.Split(',').ToArray();

            var scopes = token.Claims.Where(w => w.Type == "scope").Select(w => w.Value);
            if (scopes.Any())
            {
                tokenParameters.Scopes = scopes.ToArray();
                tokenParameters.IsSuperUser = scopes.Any(w => w == SUPER_USER_SCOPE);
            }

            if (_requiredScopes != null && _requiredScopes.Any() && !_requiredScopes.Intersect(tokenParameters.Scopes).Any() && !tokenParameters.IsSuperUser)
            {
                throw new AuthorizationException(string.Empty, "İşlme için yetkiye sahip değilsiniz.");
            }

            context.HttpContext!.Response.HttpContext.User = new ClaimsPrincipal(identity);

            return;
        }

        throw new AuthorizationException(string.Empty, "Geçersiz Kullanıcı. Lütfen Yeni Token Talep Edin.");
    }

    private string CreateTokenFromRefreshTokenIfTokenInvalid(HttpContext context, string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            handler.ValidateToken(token.Split(' ')[1], GetTokenOptions(context), out _);

            return token;
        }
        catch (Exception e)
        {
            throw new AuthorizationException(string.Empty, "Geçersiz Kullanıcı. Lütfen Yeni Token Talep Edin.");
        }
    }


    private TokenValidationParameters GetTokenOptions(HttpContext context)
    {
        JwtTokenOptions jwtTokenOptions = context.RequestServices.GetService<JwtTokenOptions>()!;
        return new TokenValidationParameters()
        {
            ValidIssuer = jwtTokenOptions!.Issuer,
            ValidAudience = jwtTokenOptions.Audience,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(jwtTokenOptions.SecurityKey),
            ValidateIssuerSigningKey = true,
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    }

}

