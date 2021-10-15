using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Command.AuthCommands
{
    public class ResetPassword : IRequest<string>
    {
        // Email of the user who needs to get password reset
        public string Email { get; set; }

        // Password reset token
        public string PasswordResetToken { get; set; }

        // New password of the user
        public string NewPassword { get; set; }

        // The class to perform password reset operation
        public class ResetPasswordHandler : IRequestHandler<ResetPassword, string>
        {
            // Database context
            private readonly IApplicationDbContext _databaseContext;

            // User manager
            public UserManager<User> _userManager;

            // Constructor
            public ResetPasswordHandler(IApplicationDbContext databaseContext, UserManager<User> userManager)
            {
                // Initialize database context
                _databaseContext = databaseContext;

                // Initialize user manager
                _userManager = userManager;
            }

            // The function to perform password reset procedure
            public async Task<string> Handle(ResetPassword request, CancellationToken cancellationToken)
            {
                // Reference the database to get user object of the user who needs to reset password
                var userObject = await _databaseContext.Users
                    .FirstOrDefaultAsync(user => user.Email == request.Email);

                // Call the function to start with password reset procedure
                IdentityResult passwordResetResult = await _userManager.ResetPasswordAsync(userObject, request.PasswordResetToken, request.NewPassword);

                // If password is successfully resetted, return the result
                if (passwordResetResult.Succeeded)
                {
                    return "Done";
                } else
                {
                    return "Not done";
                }
            }
        }
    }
}
