using Microsoft.IdentityModel.Tokens;
using netBackend.Models;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace netBackend.Utils
{
    public class Token
    {
        public const string Issuer = "IShopping";
        public const string Audience = "IShopping";

        public const string Secret =
        "q2xiARx$4x3TKqBJ";
      
        //Important note***************
        //The secret is a base64-encoded string, always make sure to 
        //use a secure long string so no one can guess it. ever!.a very recommended approach 
        //to use is through the HMACSHA256() class, to generate such a secure secret, 
        //you can refer to the below function 
        //you can run a small test by calling the GenerateSecureSecret() function 
        //to generate a random secure secret once, grab it, and use it as the secret above 
        //or you can save it into appsettings.json file and then load it from them, 
        //the choice is yours
        //​
        public static string GenerateSecureSecret()
        {
            var hmac = new HMACSHA256();
            return Convert.ToBase64String(hmac.Key);
        }
        
        public static string GenerateToken(String type)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(Secret);
            var key_ = new SymmetricSecurityKey(key);
            var signcreds = new SigningCredentials(key_, SecurityAlgorithms.HmacSha256);

            var claimsIdentity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.NameIdentifier, type)
            });
           
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Issuer = Issuer,
                Audience = Audience,
                Expires = DateTime.Now.AddMinutes(10),
                SigningCredentials = signcreds,

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static Object DecodeToken(String token)
        {
            var validation = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Issuer,
                ValidAudience = Audience,
                IssuerSigningKey = new SymmetricSecurityKey
                    (Convert.FromBase64String(Secret))
            };
            var claimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(token, validation, out SecurityToken validatedToken);//validatedToken:解密后的对象
            var jwtPayload = ((JwtSecurityToken)validatedToken).Payload.SerializeToJson(); //获取payload中的数据 
            Console.WriteLine(jwtPayload);
            return jwtPayload;
        }
    }
}