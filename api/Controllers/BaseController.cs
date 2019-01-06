using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using model;
using AutoMapper;
using DAL;
using DAO;


namespace api.Controllers
{


    public abstract class BaseController : ControllerBase
    {
        protected readonly UserManager<User> userManager;
        protected readonly SignInManager<User> signInManager;
        protected readonly IMapper mapper;
        protected readonly Dao dao;
        public BaseController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, DataAccess dal)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
            this.dao = dal;
        }

        [NonAction]
        protected async Task<User> GetCurrentUserAsync()
        {
            if (this.HttpContext.User == null)
                throw new Exception("the user is unauthorize");
            Claim userNameClaim = this.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
            if (userNameClaim == null)
                throw new Exception("an error has occure with the token");
            return await userManager.FindByNameAsync(userNameClaim.Value);
        }

        [NonAction]
        public bool IsInRole(string roleName)
        {

            var view = this.HttpContext.User.Claims;
            Claim roleClaim = view.FirstOrDefault(claim => claim.Type == "Roles" && claim.Value == roleName);

            return roleClaim != null;
        }
    }
}