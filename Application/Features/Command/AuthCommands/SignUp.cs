using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Command.AuthCommands
{
    public class SignUp : IRequest<string>
    {
        // Sign up email
        public string Email { get; set; }

        // Sign up full name
        public string FullName { get; set; }

        // Sign up password
        public string Password { get; set; }

        // The function to perform sign up operation
        public class SignUpHandler : IRequestHandler<SignUp, string>
        {
            // User manager
            private readonly UserManager<User> _userManager;

            // Database context
            private readonly IApplicationDbContext _databaseContext;

            // Auto mapper
            private IMapper _mapper;

            // Constructor
            public SignUpHandler(UserManager<User> userManager, IApplicationDbContext databaseContext,
                IMapper mapper)
            {
                // Initialize user manager
                _userManager = userManager;

                // Initialize database context
                _databaseContext = databaseContext;

                // Initialize auto mapper
                _mapper = mapper;
            }

            public async Task<string> Handle(SignUp request, CancellationToken cancellationToken)
            {
                // Create the new user profile object
                var newUserProfileObject = new UserProfile
                {
                    FullName = request.FullName
                };

                // Add new user profile object to the user profile table
                await _databaseContext.UserProfiles
                    .AddAsync(newUserProfileObject);

                // Save changes
                await _databaseContext.SaveChangesAsync();

                // Get user profile id of the created user profile
                int createdUserProfileId = newUserProfileObject.Id;

                // Create the new user object
                var newUser = _mapper.Map<User>(request);
                newUser.UserProfileId = createdUserProfileId;

                // Perform the sign up operation and get the result
                var signUpResult = await _userManager.CreateAsync(newUser, request.Password);

                if (signUpResult.Succeeded)
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
