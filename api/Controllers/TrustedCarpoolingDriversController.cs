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

namespace api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class TrustedCarpoolingDriversContoller : ControllerBase
    {
        private readonly Dao dao;
        private readonly IMapper mapper;
        public TrustedCarpoolingDriversContoller(IMapper mapper, DataAccess dal)
        {
            this.mapper = mapper;
            dao = dal;
        }

        
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
            TrustedCarpoolingDriver trustedCarpoolingDriver = await dao.AddTrustedCarpoolingDriver(mapper.Map<TrustedCarpoolingDriver>(trustedCarpoolingDriverDTO));
            return Created("api/Cars/"+trustedCarpoolingDriver.Id, mapper.Map<TrustedCarpoolingDriverDTO>(trustedCarpoolingDriver));
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await dao.RemoveTrustedCarpoolingDriver(id);
            return Ok();
        }
    }
}
