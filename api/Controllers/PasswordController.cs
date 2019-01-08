using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using model;
using AutoMapper;
using DTO.UserControllerDTO;
using DAO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using DAL;
using Microsoft.AspNetCore.Identity;
using Exceptions;

namespace api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController : BaseController
    {
        public PasswordController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, DataAccess dal) :
            base(userManager, signInManager, mapper, dal)
        { }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] NewPasswordModel newPasswordModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            User currentUser = await GetCurrentUserAsync();
            if (currentUser.Id != newPasswordModel.Id) return Unauthorized();
            IdentityResult identityResult = await userManager.ChangePasswordAsync(currentUser, newPasswordModel.ActualPassword, newPasswordModel.NewPassword);
            if (!identityResult.Succeeded) throw new WrongPasswordException(newPasswordModel.ActualPassword);
            return Accepted();
        }
    }
}