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
    public class UsersController : ControllerBase
    {
        private readonly DataAccess dal;
        private readonly IMapper mapper;

        public UsersController(IMapper mapper, DataAccess dal)
        {
            this.mapper = mapper;
            this.dal = dal;
        }
        // GET api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> Get(int pageSize = 10, int pageIndex = 0, string filter = null) {
            IEnumerable<User> users = await dal.GetUsers(pageSize, pageIndex, filter);
            IEnumerable<UserDTO> usersDTO = users.Select(mapper.Map<UserDTO>);
            return Ok(usersDTO);
        }

        // GET api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> Get(int id)
        {
            User user = (User) await dal.GetUser(id);
            return Ok(mapper.Map<UserDTO>(user));
        }

        // POST api/Users
        [HttpPost]
        public IActionResult Post([FromBody] UserDTO userDTO)
        {
            User user = dal.AddUser(mapper.Map<User>(userDTO));
            return Created("api/Users/"+user.Id, mapper.Map<UserDTO>(user));
        }

        // PUT api/Users/5
        [HttpPut("{id}")]
        public ActionResult<UserDTO> Put(int id, [FromBody] UserDTO userDTO)
        {
            User user = dal.SetUser(mapper.Map<User>(userDTO));
            return Ok(mapper.Map<UserDTO>(user));
        }

        // DELETE api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await dal.RemoveUser(id);
            return Ok();
        }
    }
}
