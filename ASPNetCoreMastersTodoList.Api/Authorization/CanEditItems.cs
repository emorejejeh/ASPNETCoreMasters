using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreMastersTodoList.Api.Authorization
{
    public class CanEditItemsRequirement : IAuthorizationRequirement { }
    public class CanEditItemsHandler :
        AuthorizationHandler<CanEditItemsRequirement, ItemDto>
    {
        private readonly UserManager<IdentityUser> _userManager;

        public CanEditItemsHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            CanEditItemsRequirement requirement,
            ItemDto resource)
        {

            var user = context.User.Claims.First().Value;

            if(user == null)
            {
                return;
            }
            if (resource.CreatedBy == user)
            {
                context.Succeed(requirement);
            }
        }
    }
}
