using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AuthFeatures
{
    public class SendPasswordResetTokenEmail : IRequest<string>
    {
        // Email of the user who needs to get password reset token
        public string Email { get; set; }

        // The class to perform password reset token send operation
        public class SendPasswordResetTokenEmailHandler : IRequestHandler<SendPasswordResetTokenEmail, string>
        {
            // Database context
            private readonly IApplicationDbContext _databaseContext;

            // User manager
            public UserManager<User> _userManager;

            // Email sender
            public IEmailSender _emailSender;

            // Constructor
            public SendPasswordResetTokenEmailHandler(IApplicationDbContext databaseContext, UserManager<User> userManager, IEmailSender emailSender)
            {
                // Initialize database context
                _databaseContext = databaseContext;

                // Initialize user manager
                _userManager = userManager;

                // Initialize email sender
                _emailSender = emailSender;
            }

            // The function to perform password reset operation
            public async Task<string> Handle(SendPasswordResetTokenEmail request, CancellationToken cancellationToken)
            {
                // Reference the database to get user object of the user who needs to get password reset email
                var userObject = await _databaseContext.Users
                    .FirstOrDefaultAsync(user => user.Email == request.Email);

                // If there is no account associated with that email, let the client know that
                if (userObject == null)
                {
                    return "Not found";
                }

                // Call the function to generate password reset token for the user
                string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(userObject);

                // Send email with password reset token to the user
                await _emailSender.SendEmailAsync(request.Email, "Reset password", $"Use this token yo reset your password {passwordResetToken}");

                // Return the result
                return "Done";
            }
        }
    }
}
