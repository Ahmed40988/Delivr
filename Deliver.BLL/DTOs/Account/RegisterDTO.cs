using Deliver.Entities.Entities;
using Deliver.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Deliver.BLL.DTOs.Account
{
    public record RegisterDTO
    (
      string Email,
      string Password
)
       {
        [JsonExtensionData]
    public Dictionary<string, object>? ExtraData { get; init; }
};

}
