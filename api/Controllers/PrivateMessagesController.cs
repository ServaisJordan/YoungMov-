using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using model;
using AutoMapper;
using DTO.PrivateMessageControllerDTO;
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
    public class PrivateMessages : BaseController
    {
        public PrivateMessages(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, DataAccess dal) :
            base(userManager, signInManager, mapper, dal)
        { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrivateMessageDTO>>> Get(int pageIndex = 0, int pageSize = 10)
        {
            User user = await GetCurrentUserAsync();
            if (user.Role != "client") return Unauthorized();
            IEnumerable<PrivateMessage> privateMessages = await dao.GetPrivateMessages(pageIndex, pageSize);
            IEnumerable<PrivateMessageDTO> privateMessageDTO = privateMessages.Select(c => mapper.Map<PrivateMessageDTO>(c));
            return Ok(privateMessageDTO);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<PrivateMessageDTO>> Get(int id)
        {
            User user = await GetCurrentUserAsync();
            PrivateMessageDTO privateMessageDTO = mapper.Map<PrivateMessageDTO>(await dao.GetPrivateMessage(id));
            if (privateMessageDTO == null) return NotFound();
            if (user.Id != privateMessageDTO.Creator) return Unauthorized();
            return Ok(privateMessageDTO);
        }


        [HttpPost]
        public async Task<ActionResult<PrivateMessageDTO>> Post([FromBody] PrivateMessageDTO privateMessageDTO)
        {
            User user = await GetCurrentUserAsync();
            if (user.Id != privateMessageDTO.Creator) return Unauthorized();
            PrivateMessage privateMessage = await dao.AddPrivateMessage(mapper.Map<PrivateMessage>(privateMessageDTO));
            return Created("api/PrivateMessages/" + privateMessage.Id, mapper.Map<PrivateMessageDTO>(privateMessage));
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<PrivateMessageDTO>> Put(int id, [FromBody] PrivateMessageDTO PrivateMessageDTO)
        {
            User user = await GetCurrentUserAsync();
            PrivateMessage privateMessageModel = await dao.GetPrivateMessage(id);
            if (privateMessageModel == null) return NotFound();
            if (user.Id != privateMessageModel.Creator) return Unauthorized();
            PrivateMessage privateMessage = await dao.SetPrivateMessage(mapper.Map(PrivateMessageDTO, privateMessageModel));
            return Ok(privateMessage);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            User user = await GetCurrentUserAsync();
            PrivateMessage privateMessage = await dao.GetPrivateMessage(id);
            if (user.Id != privateMessage.Creator) return Unauthorized();
            await dao.RemovePrivateMessage(privateMessage);
            return Ok();
        }
    }
}
