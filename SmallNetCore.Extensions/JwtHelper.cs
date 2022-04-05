using Microsoft.IdentityModel.Tokens;
using SmallNetCore.Common.Convets;
using SmallNetCore.Common.Utils;
using SmallNetCore.Models.Base;
using SmallNetCore.Models.Configs;
using SmallNetCore.Models.Entitys;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmallNetCore.Extensions
{
    public class JwtHelper
    {
        public static string GetToken(TokenModel user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(CenterConfigs.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //创建用户身份标识，可按需要添加更多信息
            var claims = new Claim[]
            {
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
               new Claim(ClaimTypes.Name, user.UId.ToString()), // 目前只存放用户主键，当然其他信息可以无限扩展存储，例如角色之类的
            };

            //创建令牌
            var token = new JwtSecurityToken(
              issuer: CenterConfigs.Issuer,
              audience: CenterConfigs.Audience,
              signingCredentials: creds,
              claims: claims,
              notBefore: DateTime.Now,
              expires: DateTime.Now.AddSeconds(CenterConfigs.JWTExpireSeconds.ToInt())
            );

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }
    }
}

