using System;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using netBackend.Utils;
using System.Security.Claims;

namespace netBackend.Handlers
{
    public class UserAuthorizationHandler :
           AuthorizationHandler<Idenfication>
    {
        protected override Task HandleRequirementAsync
        (AuthorizationHandlerContext context, Idenfication requirement)
        {
            Console.WriteLine(context.Requirements);
            if(context.User == null)
            {
                return Task.CompletedTask; 
            }

            string value = context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            if (value.Equals( requirement.idenfication))
            {
                context.Succeed(requirement);
            }
            
            return Task.CompletedTask;
        }
    }
}