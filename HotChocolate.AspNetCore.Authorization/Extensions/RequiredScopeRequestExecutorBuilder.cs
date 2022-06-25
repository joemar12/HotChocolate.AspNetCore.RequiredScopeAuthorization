using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RequiredScopeRequestExecutorBuilder
    {
        public static IRequestExecutorBuilder AddRequiredScopeAuthorization(this IRequestExecutorBuilder builder)
        {
            builder.AddAuthorization();

            builder.ConfigureSchema(sb => sb.AddRequireScopeAuthorizationDirective());
            builder.Services.TryAddSingleton<IRequiredScopeAuthorizationHandler, DefaultRequiredScopeAuthorizationHandler>();
            return builder;
        }
    }
}
