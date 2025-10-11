using Deliver.BLL.DTOs.Customer;
using Deliver.BLL.DTOs.Customer.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliver.BLL.Interfaces
{
    public interface IUserService
    {   
        Task<Result>CompleteCustomerprofileAsync(int userid,CompleteCustomerDTO  request);    

    }
}
