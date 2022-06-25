# Overview

This project extends the existing HotChocolate.AspNetCore.Authorization functionality by providing a way to validate required scopes for queries and mutations. Similar to how AspNetCore's RequiredScope attribute works.

# How to use

The attribute is in the same namespace as HotChocolate's Authorize

You can provide a space delimited string of scope names

```C#
  [RequiredScope("scope1 scope2 scope3")]
  public IQueryable<Dto> Sample([Service] IApplicationDbContext context)
  ...
```

or provide the configuration key

```C#
  [RequiredScope(RequiredScopesConfigurationKey = "RequiredScopesConfigKey")]
  public IQueryable<Dto> Sample([Service] IApplicationDbContext context)
  ...
```
