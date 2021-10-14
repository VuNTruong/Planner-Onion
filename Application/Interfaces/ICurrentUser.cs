using System;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICurrentUser
    {
        // The function to get numeric user id of the currently logged in user
        public Task<int> GetCurrentUserId();

        // The function to get user object of the currently logged in user
        public Task<User> GetCurrentUserObject();
    }
}
