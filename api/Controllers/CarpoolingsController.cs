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

namespace api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CarpoolingsController : ControllerBase
    {
        private readonly Dao dao;
        private readonly IMapper mapper;

        public CarpoolingsController(IMapper mapper, DataAccess dal)
        {
            this.mapper = mapper;
            this.dao = dal;
        }
        // GET api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarpoolingDTO>>> Get(int pageSize = 10, int pageIndex = 0, string filterFrom = null, string filterTo = null) {
            IEnumerable<Carpooling> carpoolings = await dao.GetCarpoolings(pageSize, pageIndex, filterFrom, filterTo);
            IEnumerable<CarpoolingDTO> carpoolingsDTO = carpoolings.Select(mapper.Map<CarpoolingDTO>);
            return Ok(carpoolingsDTO);
        }

        // GET api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarpoolingDTO>> Get(int id)
        {
            Carpooling carpooling = await dao.GetCarpooling(id);
            return Ok(mapper.Map<CarpoolingDTO>(carpooling));
        }

        // POST api/Users
        [HttpPost]
        public IActionResult Post([FromBody] CarpoolingDTO carpoolingDTO)
        {
            Carpooling carpooling = dao.AddCarpooling(mapper.Map<Carpooling>(carpoolingDTO));
            return Created("api/Users/"+carpooling.Id, mapper.Map<CarpoolingDTO>(carpooling));
        }

        // PUT api/Users/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDTO>> Put(int id, [FromBody] CarpoolingDTO carpoolingDTO)
        {
            Carpooling carpoolingModel = await dao.GetCarpooling(id);
            if (carpoolingModel == null) return NotFound();
            Carpooling carpooling = dao.SetCarpooling(mapper.Map(carpoolingDTO, carpoolingModel));
            return Ok(mapper.Map<CarpoolingDTO>(carpooling));
        }

        // DELETE api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await dao.RemoveCarpooling(id);
            return Ok();
        }
    }
}
