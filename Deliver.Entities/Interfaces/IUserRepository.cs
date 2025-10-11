using Deliver.Entities.Entities;
using Deliver.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliver.Entities.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> FindByEmailAsync(string email);
        Task<ApplicationUser?> FindByIDAsync(int userid);
        Task<bool?> Any(string email);
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
        Task CreateUserProfileAsync(int userid, UserType userType);
        Task<bool> CompleteCustomerprofile(int userid,ApplicationUser user, string governmentName, string cityName, string zoneName, string streetName);
        Task<IList<string>> GetRolesAsync(ApplicationUser user);
        Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role);

    }
}
