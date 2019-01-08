using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using model;
using AutoMapper;
using DTO.CarpoolingControllerDTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using DAO;
using DAL;
using Microsoft.AspNetCore.Identity;
using Exceptions;
using DTO;


namespace api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CarpoolingsController : BaseController
    {
        public CarpoolingsController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, DataAccess dal) :
            base(userManager, signInManager, mapper, dal)
        { }
        // GET api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarpoolingDTO>>> Get(int? pageSize = null, int pageIndex = 0, string filterFrom = null, string filterTo = null)
        {
            IEnumerable<Carpooling> carpoolings = await dao.GetCarpoolings(pageSize, pageIndex, filterFrom, filterTo);
            IEnumerable<CarpoolingDTO> carpoolingsDTO = carpoolings.Select(mapper.Map<CarpoolingDTO>);
            return Ok(carpoolingsDTO);
        }

        // GET api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarpoolingDTO>> Get(int id)
        {
            Carpooling carpooling = await dao.GetCarpooling(id);
            if (carpooling == null) return NotFound(id);
            return Ok(mapper.Map<CarpoolingDTO>(carpooling));
        }

        // POST api/Users
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CarpoolingDTO carpoolingDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = await GetCurrentUserAsync();
            if (user.Role == Constants.CLIENT && carpoolingDTO.Creator != user.Id) return Unauthorized();
            if (DateTime.Compare(carpoolingDTO.DateStart, carpoolingDTO.DateEnd) > 0) 
                throw new DateStartLaterThanDateEndException(carpoolingDTO.DateStart, carpoolingDTO.DateEnd); 
            Carpooling carpooling = await dao.AddCarpooling(mapper.Map<Carpooling>(carpoolingDTO));
            return Created("api/Users/" + carpooling.Id, mapper.Map<CarpoolingDTO>(carpooling));
        }

        // PUT api/Users/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDTO>> Put(int id, [FromBody] CarpoolingDTO carpoolingDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = await GetCurrentUserAsync();
            Car car = user.Car.SingleOrDefault(c => c.Id == carpoolingDTO.Car);
            if (car == null) throw new NotOwnerException(user.UserName);
            if (car.ValidatedAt == null) throw new NotValidatedException(car.Id);
            Carpooling carpoolingModel = await dao.GetCarpooling(id);
            if (carpoolingModel == null) return NotFound();
            if (user.Role == Constants.CLIENT && carpoolingModel.Creator != user.Id) return Unauthorized();
            Carpooling carpooling = await dao.SetCarpooling(mapper.Map(carpoolingDTO, carpoolingModel), carpoolingDTO.Timestamp);
            return Ok(mapper.Map<CarpoolingDTO>(carpooling));
        }

        // DELETE api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Carpooling carpooling = await dao.GetCarpooling(id);
            var user = await GetCurrentUserAsync();
            if (user.Role == Constants.CLIENT && carpooling.Creator != user.Id) return Unauthorized();
            await dao.RemoveCarpooling(carpooling);
            return Ok();
        }
    }
}
