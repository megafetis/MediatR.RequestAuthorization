using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR.RequestAuthorization;

namespace TestCore.TestCore
{
    public class GetUserProfileQuerySecurityRule:IAuthorizationRule<GetUserProfileQuery>
    {
        public Task Authorize(GetUserProfileQuery request, IUserContext userContext, CancellationToken cancellationToken)
        {
            if (userContext.IsAuthenticated)
            {
                return SecurityResult.Ok();
            }

            return SecurityResult.AnonymousUser(request);
        }
    }
}
