using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using model;
using AutoMapper;
using DTO.TrustedCarpoolingDriverControllerDTO;
using DAO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using DAL;
using Microsoft.AspNetCore.Identity;
using Exceptions;
using DTO;

namespace api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class TrustedCarpoolingDriversController : BaseController
    {
        public TrustedCarpoolingDriversController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, DataAccess dal) :
            base(userManager, signInManager, mapper, dal)
        { }


        [HttpGet("{id}")]
        public async Task<ActionResult<TrustedCarpoolingDriverDTO>> Get(int id)
        {
            TrustedCarpoolingDriverDTO trustedCarpoolingDriver = mapper.Map<TrustedCarpoolingDriverDTO>(await dao.GetTrustedCarpoolingDriver(id));
            if (trustedCarpoolingDriver == null) return NotFound();
            return Ok(mapper.Map<TrustedCarpoolingDriverDTO>(trustedCarpoolingDriver));
        }


        [HttpPost]
        public async Task<ActionResult<TrustedCarpoolingDriverDTO>> Post([FromBody] TrustedCarpoolingDriverDTO trustedCarpoolingDriverDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            User currentUser = await GetCurrentUserAsync();
            if (currentUser.Id != trustedCarpoolingDriverDTO.User) return Unauthorized();
            User carpooler = await userManager.FindByIdAsync(trustedCarpoolingDriverDTO.Carpooler);
            if (carpooler.TrustedCarpoolingDriverCode == null) throw new NotATrustedCarpoolingDriver(carpooler.UserName);
            TrustedCarpoolingDriver trustedCarpoolingDriver = await dao.AddTrustedCarpoolingDriver(mapper.Map<TrustedCarpoolingDriver>(trustedCarpoolingDriverDTO));
            return Created("api/Cars/" + trustedCarpoolingDriver.Id, mapper.Map<TrustedCarpoolingDriverDTO>(trustedCarpoolingDriver));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            User user = await GetCurrentUserAsync();
            TrustedCarpoolingDriver trustedCarpoolingDriver = await dao.GetTrustedCarpoolingDriver(id);
            if (user.UserName == Constants.CLIENT && user.Id == trustedCarpoolingDriver.User)
                await dao.RemoveTrustedCarpoolingDriver(trustedCarpoolingDriver);
            return Ok();
        }
    }
}
