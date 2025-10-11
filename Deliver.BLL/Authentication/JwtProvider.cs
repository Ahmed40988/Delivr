using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Deliver.Entities.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Deliver.BLL.Authentication;

public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
    private readonly JwtOptions options = options.Value;

    public (string token, int expiresIn) GenerateToken(ApplicationUser user)
    {
        Claim[] claims = [
             new(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
         new(JwtRegisteredClaimNames.Email,user.Email!),
         new(JwtRegisteredClaimNames.GivenName,user.FirstName!),
         new(JwtRegisteredClaimNames.FamilyName,user.LastName!),
         new("phone_number", user.PhoneNumber ?? ""),
         new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            ];
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var expirationDate = DateTime.UtcNow.AddMinutes(options.ExpiryMinutes);
        var token = new JwtSecurityToken(
            issuer: options.Issuer,
            audience: options.Audience,
            claims: claims,
            expires: expirationDate,
            signingCredentials: signingCredentials
            );
        return (token: new JwtSecurityTokenHandler().WriteToken(token), expiresIn: options.ExpiryMinutes * 60);
    }

    public string? ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key));
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                IssuerSigningKey = symmetricSecurityKey,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            return jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;

        }
        catch
        {
            return null;
        }

    }
}
