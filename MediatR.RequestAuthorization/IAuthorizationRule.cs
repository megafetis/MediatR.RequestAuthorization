using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.RequestAuthorization
{
    public interface IAuthorizationRule<in TRequest>
    {
        Task Authorize(TRequest request, IUserContext userContext, CancellationToken cancellationToken);
    }
}
