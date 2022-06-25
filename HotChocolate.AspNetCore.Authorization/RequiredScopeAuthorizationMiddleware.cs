using HotChocolate.AspNetCore.RequiredScopeAuthorization.Properties;
using HotChocolate.Resolvers;

namespace HotChocolate.AspNetCore.Authorization
{
    internal sealed class RequiredScopeAuthorizationMiddleware
    {
        private readonly FieldDelegate _next;
        private readonly IRequiredScopeAuthorizationHandler _requireScopeHandler;

        public RequiredScopeAuthorizationMiddleware(FieldDelegate next, IRequiredScopeAuthorizationHandler requireScopeHandler)
        {
            _next = next ??
                throw new ArgumentNullException(nameof(next));
            _requireScopeHandler = requireScopeHandler ??
                throw new ArgumentNullException(nameof(requireScopeHandler));
        }

        public async Task InvokeAsync(IDirectiveContext context)
        {
            RequiredScopeAuthorizationDirective directive = context.Directive.ToObject<RequiredScopeAuthorizationDirective>();

            if (directive.Apply == ApplyPolicy.AfterResolver)
            {
                await _next(context).ConfigureAwait(false);
                AuthorizeResult state = _requireScopeHandler.ValidateScopes(context, directive);
                if (state != AuthorizeResult.Allowed && !IsError(context))
                {
                    SetError(context);
                }
            }
            else
            {
                AuthorizeResult state = _requireScopeHandler.ValidateScopes(context, directive);
                if (state == AuthorizeResult.Allowed)
                {
                    await _next(context).ConfigureAwait(false);
                }
                else
                {
                    SetError(context);
                }
            }
        }

        private bool IsError(IMiddlewareContext context) => context.Result is IError or IEnumerable<IError>;

        private void SetError(IMiddlewareContext context)
        {
            context.Result = ErrorBuilder.New()
                    .SetMessage(Resources.AuthorizeMiddleware_RequiredScopeNotFound)
                    .SetCode(RequiredScopeAuthorizationErrorCodes.ScopeNotFound)
                    .SetPath(context.Path)
                    .AddLocation(context.Selection.SyntaxNode)
                    .Build();
        }
    }

    public static class RequiredScopeAuthorizationErrorCodes
    {
        public const string ScopeNotFound = "AUTH_SCOPE_NOT_FOUND";
    }
}
