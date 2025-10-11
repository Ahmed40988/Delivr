using Deliver.BLL.DTOs.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliver.BLL.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<Result> CompleteCustomerprofileAsync(int userid, CompleteCustomerDTO request)
        {
            var result2 = await _userRepository.CompleteCustomerprofile(userid,request.Adapt<ApplicationUser>(),request.Address.Government,request.Address.City,request.Address.Zone,request.Address.Street);
            return Result.Success();
        }

    }
}
