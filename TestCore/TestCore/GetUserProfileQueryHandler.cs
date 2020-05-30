using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace TestCore.TestCore
{
    class GetUserProfileQueryHandler:IRequestHandler<GetUserProfileQuery,UserProfile>
    {
        public Task<UserProfile> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new UserProfile()
            {
                UserName = "User1",
                DisplayName = "User 1",
                Id = "123"
            });
        }
    }
}
