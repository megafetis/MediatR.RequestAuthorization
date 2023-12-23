# MediatR.RequestAuthorization

[![NuGet](https://img.shields.io/nuget/dt/MediatR.RequestAuthorization.svg)](https://www.nuget.org/packages/MediatR.RequestAuthorization/) 
[![NuGet](https://img.shields.io/nuget/vpre/MediatR.RequestAuthorization.svg)](https://www.nuget.org/packages/MediatR.RequestAuthorization/)

Authorization rules for [MediatR](https://www.nuget.org/packages/MediatR). This library uses pipline behavior ``IPipelineBehavior<,>`` in mediator middleware.


## Installing MediatR.RequestAuthorization

You should install [MediatR.RequestAuthorization with NuGet](https://www.nuget.org/packages/MediatR.RequestAuthorization):

    Install-Package MediatR.RequestAuthorization
    
Or via the .NET Core command line interface:

    dotnet add package MediatR.RequestAuthorization


## Implement `IUserContext` and register it on DI container

Simple implementation for aspnetcore:

```cs
public class HttpUserContext : IUserContext
{
    public IHttpContextAccessor Http { get; }

    public HttpUserContext(IHttpContextAccessor http)
    {
        Http = http;
        User = http?.HttpContext?.User;
    }

    public virtual string? ExtraAttribute(string key)
    {
        return null;
    }

    public ClaimsPrincipal? User { get; }
    public string? Id
    {
        get
        {
            if (User?.Identity != null && User.Identity.IsAuthenticated)
            {
                return User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier)?.Value ?? User.Claims.FirstOrDefault(p => p.Type == "sub")?.Value;
            }

            return null;
        }
    }
    public string Name => User?.Identity.Name;
    public bool IsAuthenticated => User?.Identity != null && User.Identity.IsAuthenticated;
    public virtual string? ClaimValue(string claimType)
    {
        return User?.Claims?.FirstOrDefault(p => p.Type == claimType)?.Value;
    }

    public virtual bool HasClaim(string type, string value)
    {
        return User?.HasClaim(type, value) ?? false;
    }

    public virtual bool HasClaim(Predicate<Claim> match)
    {
        return User?.HasClaim(match) ?? false;
    }
}

```

## Add `IAuthorizationRule<YourQueryOrCommand>` to your DI container
```cs
services.AddTrancient<IAuthorizationRule<YourQueryOrCommand>,MySecurityRuleImplementation>();

```

## Register behavior `IAuthorizationRule<YourQueryOrCommand>` to your DI container

```cs
services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(coreAsms); // all core handlers
    cfg.AddOpenBehavior(typeof(RequestAuthorizationBehavior<,>)); // enable security

});

```

##### Common usage (Example):


Define authorization rule for your IRequest impl class
```cs 
//your IRequest class
public class GetProfileQuery:IRequest<UserProfile>
{
    
}

// your IAuthorizationRule class
class GetProfileQuerySecurityRule:IAuthorizationRule<GetProfileQuery>
{
    private readonly ISomeService _someService;

    public GetProfileQuerySecurityRule(ISomeService someService)
    {
        _someService = someService;
    }

    public Task Authorize(TRequest request, IUserContext userContext, CancellationToken cancellationToken)
    {
        
        if(userContext.IsAuthenticated)
        {
            return SecurltyResult.Ok();
        }

        return SecurityResult.AnonimousUser(request);

    }
}

```

``SecurityResult`` is a static helper class, that wraps throwing Exceptions

``AccessDeniedException`` is  a helper Exception class. You may use your own exceptions.

