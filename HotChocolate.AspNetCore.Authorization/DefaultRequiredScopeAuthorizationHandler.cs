using HotChocolate.Resolvers;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;

namespace HotChocolate.AspNetCore.Authorization
{
    public class DefaultRequiredScopeAuthorizationHandler : IRequiredScopeAuthorizationHandler
    {
        private readonly IConfiguration _config;

        public DefaultRequiredScopeAuthorizationHandler(IConfiguration config)
        {
            _config = config;
        }
        public AuthorizeResult ValidateScopes(IMiddlewareContext context, RequiredScopeAuthorizationDirective directive)
        {
            if (!TryGetUserScopes(context, out IList<string>? userScopes))
            {
                return AuthorizeResult.NotAllowed;
            }
            var requiredScopes = directive.Scopes ?? _config.GetValue<string>(directive.RequiredScopesConfigurationKey)
                .Split(' ')
                .ToList();
            if (requiredScopes is null || requiredScopes.Count < 1 || requiredScopes.All(x => string.IsNullOrEmpty(x)))
            {
                // can't validate if the required scopes are not provided so proceed
                return AuthorizeResult.Allowed;
            }
            var hasScopes = requiredScopes.Intersect(userScopes).Any();
            if (hasScopes)
            {
                return AuthorizeResult.Allowed;
            }
            return AuthorizeResult.NotAllowed;
        }

        private static bool TryGetUserScopes(IMiddlewareContext context, [NotNullWhen(true)] out IList<string>? scopes)
        {
            if (context.ContextData.TryGetValue(nameof(ClaimsPrincipal), out var obj)
                && obj is ClaimsPrincipal p)
            {
                var foundScopes = p.FindAll(ClaimConstants.Scope).Union(p.FindAll(ClaimConstants.Scp)).ToList();
                if (foundScopes != null)
                {
                    scopes = foundScopes.SelectMany(x => x.Value.Split(' ')).ToList();
                    return true;
                }
            }
            scopes = null;
            return false;
        }
    }
}
