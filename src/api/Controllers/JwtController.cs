using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using DTO.UserControllerDTO;
using model;
using DAO;
using DAL;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using AutoMapper;
using DTO;
using Exceptions;

namespace api.Controllers
{

    // Ce controller est accessible même aux utilisateurs non identifiés
    // En effet, ces derniers doivent pouvoir demander un token!
    [AllowAnonymous]
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class JwtController : BaseController
    {
        private readonly JwtIssuerOptions _jwtOptions;

        public JwtController(UserManager<User> userManager, SignInManager<User> signInManager, IOptions<JwtIssuerOptions> jwtOptions, IMapper mapper, DataAccess dal) :
            base(userManager, signInManager, mapper, dal)
        {
            _jwtOptions = jwtOptions.Value;
        }

        // POST api/Jwt
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (loginModel.Role != Constants.CLIENT && loginModel.Role != Constants.ADMIN) throw new UnknowRoleException(loginModel.Role);
            var result = await signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, false, false);
            if (result.Succeeded)
            {
                var appUser = userManager.Users.SingleOrDefault(r => r.UserName == loginModel.UserName);
                return Ok(await GenerateJwtToken(appUser));
            }
            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserDTORegistration userRegistration)
        {
            if (!ModelState.IsValid) BadRequest(ModelState);
            var user = mapper.Map<User>(userRegistration);
            var result = await userManager.CreateAsync(user, userRegistration.Password);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);
                return Ok(await GenerateJwtToken(user));
            }
            throw new ApplicationException("UNKNOW_ERROR");
        }

        private static long ToUnixEpochDate(DateTime date)
              => (long)Math.Round((date.ToUniversalTime() -
                                   new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                                  .TotalSeconds);

        private async Task<Object> GenerateJwtToken(IdentityUser user)
        {
            var claims = new[]
           {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat,
                        ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(),
                        ClaimValueTypes.Integer64)
            };
            //IEnumerable<string> roles = await _userManager.GetRolesAsync(user);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);

            // Sérialisation et retour
            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)_jwtOptions.ValidFor.TotalSeconds,
                userId = user.Id
            };
            return response;
        }
    }

}
