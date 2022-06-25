using HotChocolate.Types;

namespace HotChocolate.AspNetCore.Authorization
{
    public class RequiredScopeAuthorizationDirectiveType : DirectiveType<RequiredScopeAuthorizationDirective>
    {
        protected override void Configure(IDirectiveTypeDescriptor<RequiredScopeAuthorizationDirective> descriptor)
        {
            descriptor
            .Name("RequireScope")
            .Location(DirectiveLocation.Schema)
            .Location(DirectiveLocation.Object)
            .Location(DirectiveLocation.FieldDefinition)
            .Repeatable()
            .Internal();

            descriptor.Argument(t => t.Scopes)
                .Description("Scopes required to access the annotated resource.")
                .Type<ListType<NonNullType<StringType>>>();

            descriptor.Argument(t => t.Apply)
                .Description(
                    "Defines when when the resolver shall be executed." +
                    "By default the resolver is executed after the policy " +
                    "has determined that the current user is allowed to access " +
                    "the field.")
                .Type<NonNullType<ApplyPolicyType>>()
                .DefaultValue(ApplyPolicy.BeforeResolver);

            descriptor.Use<RequiredScopeAuthorizationMiddleware>();
        }
    }
}
