using Microsoft.IdentityModel.Tokens;
using SmallNetCore.Common.Convets;
using SmallNetCore.Common.Utils;
using SmallNetCore.Models.Base;
using SmallNetCore.Models.Configs;
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
        ///// <summary>
        ///// 颁发JWT字符串
        ///// </summary>
        ///// <param name="tokenModel"></param>
        ///// <returns></returns>
        //public static string IssueJwt(TokenModel tokenModel)
        //{
        //    string iss = Appsettings.GetSectionValue("JwtSetting:Issuer");
        //    string aud = Appsettings.GetSectionValue("JwtSetting:Audience");
        //    string secret = Appsettings.GetSectionValue("JwtSetting:SecretKey");
        //    //var claims = new Claim[] //old
        //    var claims = new List<Claim>
        //    {
        //     /*
        //     * 特别重要：
        //       1、这里将用户的部分信息，比如 uid 存到了Claim 中，如果你想知道如何在其他地方将这个 uid从 Token 中取出来，请看下边的SerializeJwt() 方法
        //       2、你也可以研究下 HttpContext.User.Claims ，具体的你可以看看 Policys/PermissionHandler.cs 类中是如何使用的。
        //     */

        //    new Claim(JwtRegisteredClaimNames.Jti, tokenModel.Uid.ToString()),
        //    new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
        //    new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
        //    //这个就是过期时间，目前是过期1000秒，可自定义，注意JWT有自己的缓冲过期时间
        //    new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddSeconds(1000)).ToUnixTimeSeconds()}"),
        //    new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(1000).ToString()),
        //    new Claim(JwtRegisteredClaimNames.Iss,iss),
        //    new Claim(JwtRegisteredClaimNames.Aud,aud),
        //   };

        //    // 可以将一个用户的多个角色全部赋予；
        //    claims.AddRange(tokenModel.Role.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));


        //    //秘钥 (SymmetricSecurityKey 对安全性的要求，密钥的长度太短会报出异常)
        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var jwt = new JwtSecurityToken(
        //        issuer: iss,
        //        claims: claims,
        //        signingCredentials: creds);

        //    var jwtHandler = new JwtSecurityTokenHandler();
        //    var encodedJwt = jwtHandler.WriteToken(jwt);

        //    return encodedJwt;
        //}

        ///// <summary>
        ///// 解析
        ///// </summary>
        ///// <param name="jwtStr"></param>
        ///// <returns></returns>
        //public static TokenModel SerializeJwt(string jwtStr)
        //{
        //    var jwtHandler = new JwtSecurityTokenHandler();
        //    if (!jwtHandler.CanReadToken(jwtStr))
        //    {
        //        return null;
        //    }
        //    string iss = Appsettings.GetSectionValue("Authentication:JwtSetting:Issuer");
        //    JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
        //    if (jwtToken.Issuer != iss)
        //    {
        //        return null;//不正确            　　　　　　
        //    }
        //    if (jwtToken.ValidTo < DateTime.Now)
        //    {
        //        return null;//过期            　　　　　
        //    }

        //    object role;
        //    try
        //    {
        //        jwtToken.Payload.TryGetValue(ClaimTypes.Role, out role);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }
        //    var tm = new TokenModel
        //    {
        //        Uid = jwtToken.Id.ToString(),
        //        Role = role != null ? role.ToString() : "",
        //    };
        //    return tm;
        //}

        public static string GetToken(TokenModel user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(CenterConfigs.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //创建用户身份标识，可按需要添加更多信息
            var claims = new Claim[]
            {
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
               new Claim(ClaimTypes.Name, user.UId.ToString()), // 放用户主键
            };

            //创建令牌
            var token = new JwtSecurityToken(
              issuer: CenterConfigs.Issuer,
              audience: CenterConfigs.Audience,
              signingCredentials: creds,
              claims: claims,
              notBefore: DateTime.Now,
              expires: DateTime.Now.AddSeconds(CenterConfigs.JWTExpireSeconds.ObjToInt())
            );

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }
    }
}

