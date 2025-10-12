using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliver.Dal.Abstractions.Errors
{
    public static class UserErrors
    {
        public static readonly Abstractions.Error InvalidCredentials =
            new("User.InvalidCredentials", "Invalid email/password", StatusCodes.Status401Unauthorized);

        public static readonly Abstractions.Error UserNotFound =
            new("User.NotFound", "No User was found with the given ID", StatusCodes.Status404NotFound);

        public static readonly Abstractions.Error DisabledUser =
            new("User.DisabledUser", "Disabled User ,Please Contact your Administrator ", StatusCodes.Status401Unauthorized);

        public static readonly Abstractions.Error LockedUser =
            new("User.LockedUser", "Locked User ,Please Contact your Administrator ", StatusCodes.Status401Unauthorized);

        public static readonly Abstractions.Error InvalidJwtToken =
            new("User.InvalidJwtToken", "Invalid Jwt token", StatusCodes.Status401Unauthorized);

        public static readonly Abstractions.Error InvalidRefreshToken =
            new("User.InvalidRefreshToken", "Invalid refresh token", StatusCodes.Status401Unauthorized);

        public static readonly Abstractions.Error DuplicatedEmail =
            new("User.DuplicatedEmail", "Another user with the same email already exists", StatusCodes.Status409Conflict);

        public static readonly Abstractions.Error EmailNotConfirmed =
            new("User.EmailNotConfirmed", "Email is not confirmed", StatusCodes.Status401Unauthorized);

        public static readonly Abstractions.Error InvalidCode =
            new("User.InvalidOTP", "Invalid OTP", StatusCodes.Status401Unauthorized);

        public static readonly Abstractions.Error OTPExpired =
            new("User.OTPexpired", "OTP expired or not found", StatusCodes.Status401Unauthorized);

        public static readonly Abstractions.Error DuplicatedConfirmation =
            new("User.DuplicatedConfirmation", "Email already confirmed", StatusCodes.Status400BadRequest);

        public static readonly Abstractions.Error InvalidRoles =
            new("User.InvalidRoles", "Invalid roles", StatusCodes.Status400BadRequest);

        public static readonly Abstractions.Error InvalidAddress =
            new("User.InvalidAddress", "Could not save address", StatusCodes.Status400BadRequest);

        public static readonly Abstractions.Error InvalidVehicle =
            new("Delivery.InvalidVehicle", "Vehicle type does not exist.", StatusCodes.Status400BadRequest);

        public static readonly Abstractions.Error DeliveryNotFound =
            new("Delivery.NotFound", "Delivery does not exist.", StatusCodes.Status400BadRequest);

        public static readonly Abstractions.Error DuplicatedVehicle =
            new("Delivery.DuplicatedVehicle", "This vehicle type is already assigned to you.", StatusCodes.Status409Conflict);

        
        public static readonly Abstractions.Error InvalidEmail =
            new("User.InvalidEmail", "Invalid email format", StatusCodes.Status400BadRequest);
    }
}
