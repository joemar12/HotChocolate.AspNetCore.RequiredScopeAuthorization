using HotChocolate.AspNetCore.Authorization;

namespace HotChocolate
{
    public static class RequiredScopeAuthorizationSchemaBuilderExtensions
    {
        public static ISchemaBuilder AddRequireScopeAuthorizationDirective(
        this ISchemaBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.AddDirectiveType<RequiredScopeAuthorizationDirectiveType>();
        }
    }
}
