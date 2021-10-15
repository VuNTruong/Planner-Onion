using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Command.AuthCommands
{
    public class SignIn : IRequest<Dictionary<string, object>>
    {
        // Sign in email
        public string Email { get; set; }

        // Sign in password
        public string Password { get; set; }

        // The function to handle sign in operation
        public class SignInHandler : IRequestHandler<SignIn, Dictionary<string, object>>
        {
            // Sign in manager
            private readonly SignInManager<User> _signInManager;

            // Error getter
            private readonly IErrorGetter _errorGetter;

            // Constructor
            public SignInHandler(SignInManager<User> signInManager, IErrorGetter errorGetter)
            {
                // Initialize sign in manager
                _signInManager = signInManager;

                // Initialize error getter
                _errorGetter = errorGetter;
            }

            public async Task<Dictionary<string, object>> Handle(SignIn request, CancellationToken cancellationToken)
            {
                // Prepare the result
                var result = new Dictionary<string, object>();

                // Perform sign in operation and get sign in result
                var signInResult = await _signInManager.PasswordSignInAsync(request.Email, request.Password, true, false);

                if (signInResult.Succeeded)
                {
                    // Add data to the result
                    result.Add("status", "Done");
                } else
                {
                    // Call the function to get list of sign in errors
                    List<string> signInErrors = await _errorGetter.GetLoginError(request.Email, signInResult);

                    // Add data to the result
                    result.Add("status", "Not done");
                    result.Add("errors", signInErrors);
                }

                // Return the result
                return result;
            }
        }
    }
}
