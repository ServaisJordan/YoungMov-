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

namespace api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class PrivateMessages : ControllerBase
    {
        private readonly Dao dao;
        private readonly IMapper mapper;
        public PrivateMessages(IMapper mapper, DataAccess dal)
        {
            this.mapper = mapper;
            dao = dal;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrivateMessageDTO>>> Get(int pageIndex = 0, int pageSize = 10) {
            IEnumerable<PrivateMessage> privateMessages = await dao.GetPrivateMessages(pageIndex, pageSize);
            IEnumerable<PrivateMessageDTO> privateMessageDTO = privateMessages.Select(c => mapper.Map<PrivateMessageDTO>(c));
            return Ok(privateMessageDTO);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<PrivateMessageDTO>> Get(int id)
        {
            PrivateMessageDTO privateMessageDTO = mapper.Map<PrivateMessageDTO>(await dao.GetPrivateMessage(id));
            if (privateMessageDTO == null) return NotFound();
            return Ok(privateMessageDTO);
        }

        
        [HttpPost]
        public async Task<ActionResult<PrivateMessageDTO>> Post([FromBody] PrivateMessageDTO privateMessageDTO)
        {
            PrivateMessage privateMessage = await dao.AddPrivateMessage(mapper.Map<PrivateMessage>(privateMessageDTO));
            return Created("api/PrivateMessages/"+privateMessage.Id, mapper.Map<PrivateMessageDTO>(privateMessage));
        }

        
        [HttpPut("{id}")]
        public async Task<ActionResult<PrivateMessageDTO>> Put(int id, [FromBody] PrivateMessageDTO PrivateMessageDTO)
        {
            PrivateMessage privateMessageModel = await dao.GetPrivateMessage(id);
            if (privateMessageModel == null) return NotFound();
            PrivateMessage privateMessage = await dao.SetPrivateMessage(mapper.Map(PrivateMessageDTO, privateMessageModel));
            return Ok(privateMessage);
        }

        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await dao.RemovePrivateMessage(id);
            return Ok();
        }
    }
}
