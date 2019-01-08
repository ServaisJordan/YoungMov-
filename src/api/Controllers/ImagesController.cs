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
using Microsoft.AspNetCore.Identity;
using System;
using Exceptions;
using DTO;

namespace api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ImagesController : BaseController
    {
        private readonly Cloudinary cloudinary;
        public ImagesController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, DataAccess dal) :
            base(userManager, signInManager, mapper, dal)
        {
            Account account = new Account("servaisjordan", "431558296296495", "6HOTTP_pfp-M7DMw5z3ThOimA5M");
            cloudinary = new CloudinaryDotNet.Cloudinary(account);
        }

        [HttpPost]
        public async Task<IActionResult> FacePhoto(IFormFile file)
        {
            return await SavePhoto(file);
        }
        [HttpPost]
        public async Task<IActionResult> IdentityPiece(IFormFile file)
        {
            return await SavePhoto(file, false);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ValidFacePhoto(string id, [FromBody] PhotoValidationModel photoValidationModel) {
            User currentUser = await GetCurrentUserAsync();
            if (currentUser.Role != Constants.ADMIN) return Unauthorized();
            User user = await dao.GetUser(id);
            if (user == null) return NotFound();
            if (photoValidationModel.IsValid) {
                if (user.FacePhotoFilename == null) throw new PhotoNotFoundException(user.UserName);
                user.FacePhotoValidatedAt = DateTime.Now;
                if (user.IdentityPieceValidatedAt != null) await dao.AttributeTrustedCarpoolingDriverCode(user);
            } else {
                user.FacePhotoFilename = null;
                user.FacePhotoSentAt = null;
            }
            await dao.SetUser(user, user.Timestamp);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ValideIdentityPiecePhoto(string id, [FromBody] PhotoValidationModel photoValidationModel) {
            User currentUser = await GetCurrentUserAsync();
            if (currentUser.Role != Constants.ADMIN) return Unauthorized();
            User user = await dao.GetUser(id);
            if (user == null) return NotFound();
            if (photoValidationModel.IsValid) {
                if (user.IdentityPieceFilename == null) throw new PhotoNotFoundException(user.UserName);
                user.IdentityPieceValidatedAt = DateTime.Now;
                if (user.FacePhotoValidatedAt != null) await dao.AttributeTrustedCarpoolingDriverCode(user);
            } else {
                user.IdentityPieceFilename = null;
                user.IdentityPieceSentAt = null;
            }
            await dao.SetUser(user, user.Timestamp);
            return Ok();
        }

        private async Task<IActionResult> SavePhoto(IFormFile file, Boolean isFacePhoto = true)
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
                    {
                        var user = await GetCurrentUserAsync();
                        if (isFacePhoto)
                        {
                            user.FacePhotoFilename = results.Uri.ToString();
                            user.FacePhotoSentAt = DateTime.Now;
                            user.FacePhotoValidatedAt = null;
                        }
                        else
                        { 
                            user.IdentityPieceFilename = results.Uri.ToString();
                            user.IdentityPieceSentAt = DateTime.Now;
                            user.IdentityPieceValidatedAt = null;
                        }
                        user.TrustedCarpoolingDriverCode = null;
                        await dao.SetUser(user, user.Timestamp);
                        return Ok(results.Uri);
                    }
                    else
                        return BadRequest(results.Error.Message);
                }

            }
            return BadRequest("Unsupported format");
        }
    }
}