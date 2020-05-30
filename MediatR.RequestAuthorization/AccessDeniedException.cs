using System;
using System.Collections.Generic;
using System.Text;

namespace MediatR.RequestAuthorization
{
    /// <summary>
    /// Exception 
    /// </summary>
    public class AccessDeniedException:Exception
    {
        public AccessDeniedException(object request, string userName = null, string message = null):base(message)
        {
            UserName = userName;
            Request = request;
        }

        public string UserName { get; }
        public object Request { get; }
    }
}
