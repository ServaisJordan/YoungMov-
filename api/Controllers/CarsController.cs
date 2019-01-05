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

namespace api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly Dao dao;
        private readonly IMapper mapper;
        public CarsController(IMapper mapper, DataAccess dal)
        {
            this.mapper = mapper;
            dao = dal;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDTO>>> Get(int pageIndex = 0, int pageSize = 10) {
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
        public async Task<ActionResult<CarDTO>> Post([FromBody] CarDTO CarDTO)
        {
            Car car = await dao.AddCar(mapper.Map<Car>(CarDTO));
            return Created("api/Cars/"+car.Id, mapper.Map<CarDTO>(car));
        }

        
        [HttpPut("{id}")]
        public async Task<ActionResult<CarDTO>> Put(int id, [FromBody] CarDTO carDTO)
        {
            Car carModel = await dao.GetCar(id);
            if (carModel == null) return NotFound();
            Car car = await dao.SetCar(mapper.Map(carDTO, carModel));
            return Ok(car);
        }

        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await dao.RemoveCar(id);
            return Ok();
        }
    }
}
