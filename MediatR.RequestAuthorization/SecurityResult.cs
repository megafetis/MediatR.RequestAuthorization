using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MediatR.RequestAuthorization
{
    public static class SecurityResult
    {
        public static Task Denied(object request, string userName = null, string message = null)
        {
            throw new AccessDeniedException(request, userName, message);
        }

        public static Task AnonymousUser(object request)
        {
            throw new AccessDeniedException(request, null, "Access denied. User is Anonymous");
        }

        public static Task Ok()
        {
            return Task.CompletedTask;
        }
    }
}
