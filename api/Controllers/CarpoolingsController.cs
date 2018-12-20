using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using model;
using AutoMapper;
using DTO;
using Microsoft.EntityFrameworkCore;
using DAL;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CarpoolingsController : ControllerBase
    {
        private readonly DataAccess dal;
        private readonly IMapper mapper;

        public CarpoolingsController(IMapper mapper, DataAccess dal)
        {
            this.mapper = mapper;
            this.dal = dal;
        }
        // GET api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarpoolingDTO>>> Get() {
            IEnumerable<Carpooling> carpoolings = await dal.GetCarpoolings();
            IEnumerable<CarpoolingDTO> carpoolingsDTO = carpoolings.Select(mapper.Map<CarpoolingDTO>);
            return Ok(carpoolingsDTO);
        }

        // GET api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarpoolingDTO>> Get(int id)
        {
            Carpooling carpooling = await dal.GetCarpooling(id);
            return Ok(mapper.Map<CarpoolingDTO>(carpooling));
        }

        // POST api/Users
        [HttpPost]
        public IActionResult Post([FromBody] CarpoolingDTO carpoolingDTO)
        {
            Carpooling carpooling = dal.AddCarpooling(mapper.Map<Carpooling>(carpoolingDTO));
            return Created("api/Users/"+carpooling.Id, mapper.Map<CarpoolingDTO>(carpooling));
        }

        // PUT api/Users/5
        [HttpPut("{id}")]
        public ActionResult<UserDTO> Put(int id, [FromBody] CarpoolingDTO carpoolingDTO)
        {
            Carpooling carpooling = dal.SetCarpooling(mapper.Map<Carpooling>(carpoolingDTO));
            return Ok(mapper.Map<CarpoolingDTO>(carpooling));
        }

        // DELETE api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await dal.RemoveCarpooling(id);
            return Ok();
        }
    }
}