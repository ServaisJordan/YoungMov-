using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using model;
using AutoMapper;
using DTO.CarpoolingApplicantControllerDTO;
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
    public class CarpoolingApplicantController : BaseController
    {
        public CarpoolingApplicantController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, DataAccess dal) :
            base(userManager, signInManager, mapper, dal)
        { }


        [HttpGet("{id}")]
        public async Task<ActionResult<CarpoolingApplicantDTO>> Get(int id)
        {
            CarpoolingApplicant carpoolingApplicant = mapper.Map<CarpoolingApplicant>(await dao.GetCarpoolingApplicant(id));
            if (carpoolingApplicant == null) return NotFound();
            return Ok(mapper.Map<CarpoolingApplicantDTO>(carpoolingApplicant));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CarpoolingApplicantDTO>> Put(int id, [FromBody] CarpoolingApplicantDTO carpoolingApplicantDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            User user = await GetCurrentUserAsync();
            CarpoolingApplicant carpoolingApplicantModel = await dao.GetCarpoolingApplicant(id);
            if (user.Role == Constants.ADMIN && user.Carpooling.SingleOrDefault(c => c.Id == carpoolingApplicantDTO.Carpooling) == null) return Unauthorized();
            CarpoolingApplicant carpoolingApplicant = await dao.SetCarpoolingApplicant(mapper.Map(carpoolingApplicantDTO, carpoolingApplicantModel));
            return Ok(mapper.Map<CarpoolingApplicantDTO>(carpoolingApplicant));
        }

        [HttpPost]
        public async Task<ActionResult<CarpoolingApplicantDTO>> Post([FromBody] CarpoolingApplicantDTO carpoolingApplicantDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = await GetCurrentUserAsync();
            if (user.Role == Constants.CLIENT && user.Id != carpoolingApplicantDTO.User) return Unauthorized();
            if (user.Carpooling.SingleOrDefault(c => c.Id == carpoolingApplicantDTO.Carpooling) != null)
                throw new JoiningHisOwnCarpoolingException(user.UserName);
            Carpooling carpooling = await dao.GetCarpooling(carpoolingApplicantDTO.Carpooling);
            if (carpooling.NbPlaces >= carpooling.CarpoolingApplicant.Count()) throw new TooMuchParticipantsException(carpooling.Id);
            CarpoolingApplicant carpoolingApplicant = await dao.AddCarpoolingApplicant(mapper.Map<CarpoolingApplicant>(carpoolingApplicantDTO));
            return Created("api/Cars/" + carpoolingApplicant.Id, mapper.Map<CarpoolingApplicantDTO>(carpoolingApplicant));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            CarpoolingApplicant carpoolingApplicant = await dao.GetCarpoolingApplicant(id);
            var user = await GetCurrentUserAsync();
            if (user.Role == Constants.CLIENT && user.Id != carpoolingApplicant.User) return Unauthorized();
            await dao.RemoveCarpoolingApplicant(carpoolingApplicant);
            return Ok();
        }
    }
}
