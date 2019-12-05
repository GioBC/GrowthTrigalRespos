using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using GrowthTrigal.Web.Data.Entities;
using GrowthTrigal.Web.Models;

namespace GrowthTrigal.Web.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task<IdentityResult> UpdateUserAsync(User user);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(User user, string roleName);

        Task<bool> IsUserInRoleAsync(User user, string roleName);
        Task GenerateEmailConfirmationTokenAsync(User user);
        Task ConfirmEmailAsync(User user, object token);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

        Task<bool> DeleteUserAsync(string email);

        Task<SignInResult> ValidatePasswordAsync(User user, string password);

    }
}
