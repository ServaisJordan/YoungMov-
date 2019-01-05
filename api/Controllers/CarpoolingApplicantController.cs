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

namespace api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CarpoolingApplicantController : ControllerBase
    {
        private readonly Dao dao;
        private readonly IMapper mapper;
        public CarpoolingApplicantController(IMapper mapper, DataAccess dal)
        {
            this.mapper = mapper;
            dao = dal;
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<CarpoolingApplicantDTO>> Get(int id)
        {
            CarpoolingApplicant carpoolingApplicant = mapper.Map<CarpoolingApplicant>(await dao.GetCarpoolingApplicant(id));
            if (carpoolingApplicant == null) return NotFound();
            return Ok(mapper.Map<CarpoolingApplicantDTO>(carpoolingApplicant));
        }

        
        [HttpPost]
        public async Task<ActionResult<CarpoolingApplicantDTO>> Post([FromBody] CarpoolingApplicantDTO carpoolingApplicantDTO)
        {
            CarpoolingApplicant carpoolingApplicant = await dao.AddCarpoolingApplicant(mapper.Map<CarpoolingApplicant>(carpoolingApplicantDTO));
            return Created("api/Cars/"+carpoolingApplicant.Id, mapper.Map<CarpoolingApplicantDTO>(carpoolingApplicant));
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await dao.RemoveTrustedCarpoolingDriver(id);
            return Ok();
        }
    }
}
