using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.IO;
using System.Web;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace netBackend.Utils
{
    static class Security
    {
        public static string Encode(string data)
        {
            var md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(data)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;

        }

        public static string GetId(String token)
        {

            string result = string.Empty;//加密内容
            
            try
            {
                ClaimsPrincipal principal = new ClaimsPrincipal();
                JwtSecurityToken jwt = null;
                var handler = new JwtSecurityTokenHandler();
                jwt = handler.ReadJwtToken(token);
                // 根据token是否正常解析
                if (jwt == null)
                {
                    return "";
                }
                // 验证token是否有效
                var TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Token.Issuer,
                    ValidAudience = Token.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Token.Secret))

                    /***********************************TokenValidationParameters的参数默认值***********************************/
                    // RequireSignedTokens = true,
                    // SaveSigninToken = false,
                    // ValidateActor = false,
                    // 将下面两个参数设置为false，可以不验证Issuer和Audience，但是不建议这样做。    
                    // ValidateAudience = true,
                    // ValidateIssuer = true, 
                    // ValidateIssuerSigningKey = false,
                    // 是否要求Token的Claims中必须包含Expires
                    // RequireExpirationTime = true,
                    // 允许的服务器时间偏移量,默认300秒
                    // ClockSkew = TimeSpan.FromSeconds(300),
                    // 是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                    // ValidateLifetime = true
                };
                SecurityToken sercutityToken = null;
                // 验证token是否有效，如果过期，报错SecurityTokenExpiredException
                // 报错信息：IDX10223 : Lifetime validation failed
                principal = handler.ValidateToken(token, TokenValidationParameters, out sercutityToken);
                if (principal != null && principal.Claims.Count() > 0)
                {
                    Claim claim = principal.Claims.FirstOrDefault(x => x.Type == "id");
                    result = claim.Value;
                }
            }
            catch (SecurityTokenExpiredException ex)
            {
                //可以在这写续期代码

                return null;
            }
            return result;
        }
    }
}