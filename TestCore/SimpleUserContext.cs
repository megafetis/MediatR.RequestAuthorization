using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using MediatR.RequestAuthorization;

namespace TestCore
{
    public class SimpleUserContext:IUserContext
    {
        private readonly string _userName;
        private readonly string _userId;

        public SimpleUserContext(SimpleUserManager manager)
        {
            _userName = manager.UserName;
            _userId = manager.UserId;

            if (_userId != null)
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]{}));
            }
        }
        public ClaimsPrincipal User { get; }
        public string Id => _userId;
        public string Name => _userName;
        public bool IsAuthenticated => _userId != null;
        public string ClaimValue(string claimType)
        {
            throw new NotImplementedException();
        }

        public bool HasClaim(string type, string value)
        {
            throw new NotImplementedException();
        }

        public bool HasClaim(Predicate<Claim> match)
        {
            throw new NotImplementedException();
        }

        public string ExtraAttribute(string key)
        {
            throw new NotImplementedException();
        }
    }
}
