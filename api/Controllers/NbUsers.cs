using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DAO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using DAL;
using Microsoft.AspNetCore.Identity;
using model;

namespace api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class NbUsers : BaseController
    {

        public NbUsers(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, DataAccess dal) : base(userManager, signInManager, mapper, dal) { }

        [HttpGet]
        public async Task<ActionResult<int>> Get(DateTime? date = null, char? gender = null)
        {
            User user = await GetCurrentUserAsync();
            if (user.Role != "backoffice") return Unauthorized();
            return Ok(await dao.GetNumberOfUsers(date, gender));
        }
    }
}