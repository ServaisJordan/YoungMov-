using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DTO;
using DAL;
using DAO;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        
        private string[] values = new string[] { "value1", "value2" }; 
        public ValuesController(IMapper mapper, DataAccess dal)
        {
        }
        // GET api/Users
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get() {
            return Ok(values);
        }

        // GET api/Users/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return Ok(values[id+1]);
        }

        // POST api/Users
        [HttpPost]
        public void Post([FromBody] UserDTO userDTO)
        {
        }

        // PUT api/Users/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UserDTO userDTO)
        {
        }

        // DELETE api/Users/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
