using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using model;
using AutoMapper;
using DTO.CarControllerDTO;
using DAO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using DAL;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : BaseController
    {
        public CarsController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, DataAccess dal) :
            base(userManager, signInManager, mapper, dal)
        { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDTO>>> Get(int pageIndex = 0, int? pageSize = null)
        {
            IEnumerable<Car> cars = await dao.GetCars(pageIndex, pageSize);
            IEnumerable<CarDTO> carsDTO = cars.Select(c => mapper.Map<CarDTO>(c));
            return Ok(carsDTO);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CarDTO>> Get(int id)
        {
            CarDTO carDTO = mapper.Map<CarDTO>(await dao.GetCar(id));
            if (carDTO == null) return NotFound();
            return Ok(carDTO);
        }


        [HttpPost]
        public async Task<ActionResult<CarDTO>> Post([FromBody] CarDTO carDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            User user = await GetCurrentUserAsync();
            if (user.Role == "client" && user.Id != carDTO.Owner)
                return Unauthorized();
            Car car = await dao.AddCar(mapper.Map<Car>(carDTO));
            return Created("api/Cars/" + car.Id, mapper.Map<CarDTO>(car));
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<CarDTO>> Put(int id, [FromBody] CarDTO carDTO)
        {
            var user = await GetCurrentUserAsync();
            if (user.Role == "client" && user.Id != carDTO.Owner)
                return Unauthorized();
            Car carModel = await dao.GetCar(id);
            if (carModel == null) return NotFound();
            Car car = await dao.SetCar(mapper.Map(carDTO, carModel));
            return Ok(car);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await GetCurrentUserAsync();
            if (user.Role == "client" && user.Car.SingleOrDefault(c => c.Id == id) == null)
                return Unauthorized();
            Car car = await dao.GetCar(id);
            await dao.RemoveCar(car);
            return Ok();
        }
    }
}
