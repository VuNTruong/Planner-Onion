﻿using System.Security.Claims;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class CurrentUser : ICurrentUser
    {
        // User id of the currently logged in user
        private readonly string currentUserId;

        // Database context
        private readonly IApplicationDbContext _databaseContext;

        public CurrentUser(IHttpContextAccessor httpContextAccessor, IApplicationDbContext databaseContext)
        {
            // Http context accessor is injected in here via DI
            // Get user id of the currently logged in user
            currentUserId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            // Initialize database context
            _databaseContext = databaseContext;
        }

        public async Task<int> GetCurrentUserId()
        {
            // Reference the database, include user identity object as well
            var currentUserObject = await _databaseContext.UserProfiles
                .Include(userProfile => userProfile.User)
                .FirstOrDefaultAsync(userProfile => userProfile.User.Id == currentUserId);

            // Return the obtained user id
            return currentUserObject.Id;
        }

        // The function to get user object of the currently logged in user
        public async Task<User> GetCurrentUserObject()
        {
            // Reference the database, include user identity object as well
            User currentUserObject = await _databaseContext.Users
                .FirstOrDefaultAsync(user => user.Id == currentUserId);

            // Return the obtained user object
            return currentUserObject;
        }
    }
}
