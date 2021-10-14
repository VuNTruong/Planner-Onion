using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.AuthFeatures
{
    public class SignOut : IRequest<string>
    {
        // The function to handle sign out operation
        public class SignOutHandler : IRequestHandler<SignOut, string>
        {
            // Sign in manager
            private readonly SignInManager<User> _signInManager;

            // Constructor
            public SignOutHandler(SignInManager<User> signInManager)
            {
                // Initialize sign in manager
                _signInManager = signInManager;
            }

            public async Task<string> Handle(SignOut request, CancellationToken cancellationToken)
            {
                // Perform the sign out operation and get the result
                await _signInManager.SignOutAsync();

                // Return the result
                return "Done";
            }
        }
    }
}
