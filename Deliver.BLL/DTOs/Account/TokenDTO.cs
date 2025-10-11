using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliver.BLL.DTOs.Account
{
    public record TokenDTO
(
          int UserId,
          string Token,
          int expiresIn,
          string RefreshToken,
         DateTime RefreshTokenExpiration
        );
}
