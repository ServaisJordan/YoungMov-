using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using model;
using AutoMapper;
using DTO.UserControllerDTO;
using DAO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using DAL;
using Microsoft.AspNetCore.Identity;

namespace api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, DataAccess dal) :
            base(userManager, signInManager, mapper, dal)
        { }
        // GET api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> Get(int pageSize = 10, int pageIndex = 0, string userNameFilter = null)
        {
            IEnumerable<User> users = await dao.GetUsers(pageSize, pageIndex, userNameFilter);
            IEnumerable<UserDTO> usersDTO = users.Select(mapper.Map<UserDTO>);
            return Ok(usersDTO);
        }

        // GET api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> Get(string id)
        {
            User user = await dao.GetUser(id);
            return Ok(mapper.Map<UserDTO>(user));
        }


        /* [HttpGet("{userName}")]
        public async Task<ActionResult<UserDTO>> Get(string userName) {
            User user = (User) await dao.GetUser(userName);
            return Ok(mapper.Map<UserDTO>(user));
        } */

        // POST api/Users
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDTORegistration userDTO)
        {
            User currentUser = await GetCurrentUserAsync();
            if (currentUser.Role != "backoffice") return Unauthorized();
            User userRegistratiuon = mapper.Map<User>(userDTO);
            await userManager.CreateAsync(userRegistratiuon, userDTO.Password);
            User user = await dao.AddUser(mapper.Map<User>(userDTO));
            return Created("api/Users/" + user.Id, mapper.Map<UserDTO>(user));
        }

        // PUT api/Users/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDTO>> Put(string id, [FromBody] UserDTO userDTO)
        {
            User currentUser = await GetCurrentUserAsync();
            User userModel = await dao.GetUser(id);
            if (userModel == null) return NotFound();
            if (currentUser.Role == "client" && userModel.Id != currentUser.Id) return Unauthorized();
            User user = await dao.SetUser(mapper.Map(userDTO, userModel), userDTO.Timestamp);
            return Ok(mapper.Map<UserDTO>(user));
        }

        // DELETE api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            User currentUser = await GetCurrentUserAsync();
            User user = await dao.GetUser(id);
            if (currentUser.Role == "client" && currentUser.Id != user.Id) return Unauthorized();
            await dao.RemoveUser(user);
            return Ok();
        }
    }
}
