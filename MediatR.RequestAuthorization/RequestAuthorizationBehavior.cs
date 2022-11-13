using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.RequestAuthorization
{
    public class RequestAuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IAuthorizationRule<TRequest>> _rules;
        private readonly IUserContext _userContext;

        public RequestAuthorizationBehavior(IEnumerable<IAuthorizationRule<TRequest>> rules, IUserContext userContext)
        {
            _rules = rules;
            _userContext = userContext;
        }


        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_rules.Any())
            {
                await Task.WhenAll(_rules.Select(x => x.Authorize(request, _userContext, cancellationToken)));
            }

            return await next();
        }
    }
}
