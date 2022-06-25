# Overview

Currently, HotChocolate.AspNetCore.Authorization does not support scope verification so I created this package to extend its functionality. This works similar to AspNetCore's ```RequiredScope``` attribute.

# How to use

The attribute is in the same namespace as HotChocolate's Authorize

First, register in services

```C#
  services.AddGraphQLServer()
          .AddRequiredScopeAuthorization()
```

Then add the ```RequiredScope``` attribute in your queries/mutations.
You can provide a list of scope names in the params

```C#
  [RequiredScope("scope1", "scope2", "scope3")]
  public IQueryable<Dto> Sample([Service] IApplicationDbContext context)
  {
     ...
  }
```

or provide the configuration key

```C#
  [RequiredScope(RequiredScopesConfigurationKey = "RequiredScopesConfigKey")]
  public IQueryable<Dto> Sample([Service] IApplicationDbContext context)
  {
     ...
  }
```

note: this might be supported by HotChocolate in the future and might be deprecated
