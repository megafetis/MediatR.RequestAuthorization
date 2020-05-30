# MediatR.RequestAuthorization

[![NuGet](https://img.shields.io/nuget/dt/MediatR.RequestAuthorization.svg)](https://www.nuget.org/packages/MediatR.RequestAuthorization/) 
[![NuGet](https://img.shields.io/nuget/vpre/MediatR.RequestAuthorization.svg)](https://www.nuget.org/packages/MediatR.RequestAuthorization/)

Authorization rules for [MediatR](https://www.nuget.org/packages/MediatR). This library uses pipline behavior ``IPipelineBehavior<,>`` in mediator middleware.


## Installing MediatR.RequestAuthorization

You should install [MediatR.RequestAuthorization with NuGet](https://www.nuget.org/packages/MediatR.RequestAuthorization):

    Install-Package MediatR.RequestAuthorization
    
Or via the .NET Core command line interface:

    dotnet add package MediatR.RequestAuthorization


##### Common usage (Example):

You must register ``RequestAuthorizationBehavior<,>`` and ``IUserContext``services in your service factory ``ServiceFactory``.
If you are using Microsoft.DependencyInjection, take [MediatR.RequestAuthorization.AspNetCore](https://www.nuget.org/packages/MediatR.RequestAuthorization.AspNetCore) package 
to register them.


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

