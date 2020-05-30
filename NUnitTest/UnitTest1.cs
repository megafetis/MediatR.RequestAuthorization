using System;
using System.Threading.Tasks;
using MediatR;
using MediatR.RequestAuthorization;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TestCore;
using TestCore.TestCore;

namespace NUnitTest
{
    public class Tests
    {
        private IServiceProvider serviceProvider;
        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddMediatR(typeof(UserProfile).Assembly);
            services.AddTransient<IUserContext, SimpleUserContext>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestAuthorizationBehavior<,>));
            services.AddTransient(typeof(IAuthorizationRule<GetUserProfileQuery>), typeof(GetUserProfileQuerySecurityRule));
           
            services.AddSingleton<SimpleUserManager>();

            serviceProvider = services.BuildServiceProvider();

        }

        [Test]
        public async Task TestDenied()
        {
            var userManager = serviceProvider.GetService<SimpleUserManager>();
            userManager.UserId = null;
            userManager.UserName = null;

            var mediator = serviceProvider.GetService<IMediator>();

            UserProfile result = null;

            try
            {
                result = await mediator.Send(new GetUserProfileQuery());
            }
            catch (AccessDeniedException e)
            {
                result = null;
            }


            Assert.Null(result);
        }

        [Test]
        public async Task TestAllow()
        {
            var userManager = serviceProvider.GetService<SimpleUserManager>();
            userManager.UserId = "123";
            userManager.UserName = "User1";

            var mediator = serviceProvider.GetService<IMediator>();

            UserProfile result = null;

            try
            {
                result = await mediator.Send(new GetUserProfileQuery());
            }
            catch (AccessDeniedException e)
            {
                result = null;
            }


            Assert.NotNull(result);
        }
    }
}