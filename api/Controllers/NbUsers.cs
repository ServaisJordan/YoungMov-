using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DAO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using DAL;

namespace api
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class NbUsers : ControllerBase {
        private readonly IMapper mapper;
        private readonly Dao dao;

        public NbUsers(IMapper mapper, DataAccess dal) {
            this.mapper = mapper;
            this.dao = dal;
        }

        [HttpGet]
        public async Task<ActionResult<int>> Get(DateTime ?date = null, char ?gender = null) {
            return Ok(await dao.GetNumberOfUsers(date, gender));
        }
    }
}