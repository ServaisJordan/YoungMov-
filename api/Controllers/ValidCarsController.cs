using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using model;
using System;
using AutoMapper;
using DTO.CarControllerDTO;
using DAO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using DAL;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using DTO;

namespace api.Controllers {
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ValidCarsController : BaseController {
        public ValidCarsController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, DataAccess dal) : base (userManager, signInManager, mapper, dal) {}

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] DTO.ValidationModelCar validationModelCar) {
            User currentUser = await GetCurrentUserAsync();
            if (currentUser.Role != Constants.ADMIN) return Unauthorized();
            Car car = await dao.GetCar(id);
            if (car == null) return NotFound();
            if (validationModelCar.IsValid) {
                car.ValidatedAt = DateTime.Now;
                await dao.SetCar(car);
            } else await dao.RemoveCar(car);
            return Ok();
        }
    }
}