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
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using System.IO;
using CloudinaryDotNet.Actions;

namespace api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : Controller
    {
        private readonly Cloudinary cloudinary;
        public ImagesController(IMapper mapper, DataAccess dal)
        {
            Account account = new Account("servaisjordan", "431558296296495", "6HOTTP_pfp-M7DMw5z3ThOimA5M");
            cloudinary = new CloudinaryDotNet.Cloudinary();
        }

        [HttpPost]
        public IActionResult PostAsync(IFormFile file)
        {
            var filePath = Path.GetTempFileName();
            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    ImageUploadResult results = cloudinary.Upload(new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream)
                    });
                    if (results.Error == null)
                        return Ok(results.Uri);
                    else
                        return BadRequest(results.Error.Message);
                }

            }
            return BadRequest("Unsupported format");
        }
    }
}