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

            string result = string.Empty;//��������
            
            try
            {
                ClaimsPrincipal principal = new ClaimsPrincipal();
                JwtSecurityToken jwt = null;
                var handler = new JwtSecurityTokenHandler();
                jwt = handler.ReadJwtToken(token);
                // ����token�Ƿ���������
                if (jwt == null)
                {
                    return "";
                }
                // ��֤token�Ƿ���Ч
                var TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Token.Issuer,
                    ValidAudience = Token.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Token.Secret))

                    /***********************************TokenValidationParameters�Ĳ���Ĭ��ֵ***********************************/
                    // RequireSignedTokens = true,
                    // SaveSigninToken = false,
                    // ValidateActor = false,
                    // ������������������Ϊfalse�����Բ���֤Issuer��Audience�����ǲ�������������    
                    // ValidateAudience = true,
                    // ValidateIssuer = true, 
                    // ValidateIssuerSigningKey = false,
                    // �Ƿ�Ҫ��Token��Claims�б������Expires
                    // RequireExpirationTime = true,
                    // ����ķ�����ʱ��ƫ����,Ĭ��300��
                    // ClockSkew = TimeSpan.FromSeconds(300),
                    // �Ƿ���֤Token��Ч�ڣ�ʹ�õ�ǰʱ����Token��Claims�е�NotBefore��Expires�Ա�
                    // ValidateLifetime = true
                };
                SecurityToken sercutityToken = null;
                // ��֤token�Ƿ���Ч��������ڣ�����SecurityTokenExpiredException
                // ������Ϣ��IDX10223 : Lifetime validation failed
                principal = handler.ValidateToken(token, TokenValidationParameters, out sercutityToken);
                if (principal != null && principal.Claims.Count() > 0)
                {
                    Claim claim = principal.Claims.FirstOrDefault(x => x.Type == "id");
                    result = claim.Value;
                }
            }
            catch (SecurityTokenExpiredException ex)
            {
                //��������д���ڴ���

                return null;
            }
            return result;
        }
    }
}