using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace MediatR.RequestAuthorization
{
    public interface IUserContext
    {
        /// <summary>
        /// Access to user principal
        /// </summary>
        ClaimsPrincipal User { get; }

        /// <summary>
        /// User Id
        /// </summary>
        string Id { get; }
        /// <summary>
        /// User name
        /// </summary>
        string Name { get; }
        /// <summary>
        /// If User is authenticated 
        /// </summary>
        bool IsAuthenticated { get; }
        /// <summary>
        /// Get claim value by type
        /// </summary>
        /// <param name="claimType"></param>
        /// <returns></returns>
        string ClaimValue(string claimType);

        bool HasClaim(string type, string value);
        bool HasClaim(Predicate<Claim> match);

        /// <summary>
        /// Additional attributes to access in user context
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string ExtraAttribute(string key);
    }
}
