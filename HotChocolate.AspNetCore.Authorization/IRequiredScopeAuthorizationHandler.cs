using HotChocolate.Resolvers;

namespace HotChocolate.AspNetCore.Authorization
{
    public interface IRequiredScopeAuthorizationHandler
    {
        AuthorizeResult ValidateScopes(
            IMiddlewareContext context,
            RequiredScopeAuthorizationDirective directive);
    }
}
