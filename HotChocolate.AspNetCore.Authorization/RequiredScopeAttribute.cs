using HotChocolate.Types.Descriptors;
using HotChocolate.Types;
using System.Reflection;

namespace HotChocolate.AspNetCore.Authorization
{
    public class RequiredScopeAttribute : DescriptorAttribute
    {
        public RequiredScopeAttribute(params string[] requiredScopes)
        {
            Scopes = requiredScopes ?? throw new ArgumentNullException(nameof(requiredScopes));
        }
        public string[]? Scopes { get; set; }
        public string? RequiredScopesConfigurationKey { get; set; }
        public ApplyPolicy Apply { get; set; } = ApplyPolicy.BeforeResolver;

        protected override void TryConfigure(IDescriptorContext context, IDescriptor descriptor, ICustomAttributeProvider element)
        {
            if (descriptor is IObjectTypeDescriptor type)
            {
                type.Directive(CreateDirective());
            }
            else if (descriptor is IObjectFieldDescriptor field)
            {
                field.Directive(CreateDirective());
            }
        }

        private RequiredScopeAuthorizationDirective CreateDirective()
        {
            if (Scopes is not null && Scopes.Length > 0)
            {
                return new RequiredScopeAuthorizationDirective(
                    Scopes,
                    apply: Apply);
            }
            else
            {
                return new RequiredScopeAuthorizationDirective(
                    RequiredScopesConfigurationKey,
                    apply: Apply);
            }
        }
    }
}
