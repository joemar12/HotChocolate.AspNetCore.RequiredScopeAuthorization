using System.Runtime.Serialization;

namespace HotChocolate.AspNetCore.Authorization
{
    public class RequiredScopeAuthorizationDirective : ISerializable
    {
        public RequiredScopeAuthorizationDirective(string? configKey,
                                     ApplyPolicy apply = ApplyPolicy.BeforeResolver)
            : this(null, configKey, apply)
        {

        }

        public RequiredScopeAuthorizationDirective(IReadOnlyList<string>? scopes,
                                     string? configKey = null,
                                     ApplyPolicy apply = ApplyPolicy.BeforeResolver)
        {
            Scopes = scopes;
            RequiredScopesConfigurationKey = configKey;
            Apply = apply;
        }

        public IReadOnlyList<string>? Scopes { get; }
        public string? RequiredScopesConfigurationKey { get; }
        public ApplyPolicy Apply { get; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Scopes), Scopes?.ToList());
            info.AddValue(nameof(RequiredScopesConfigurationKey), RequiredScopesConfigurationKey ?? string.Empty);
            info.AddValue(nameof(Apply), (int)Apply);
        }
    }
}
