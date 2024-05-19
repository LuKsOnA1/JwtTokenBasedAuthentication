﻿using EntityLayer.DTOs.Image;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Abstract;
using System.Security.Claims;

namespace JWT_TokenBasedAuthentication.Controllers
{
	[Route("api/ImageServices")]
	[ApiController]
	public class ImageController(
		IUserService userService
		) : ControllerBase
	{
		[Authorize]
		[HttpPost("UploadImage")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<IActionResult> UploadProfilePicture([FromForm]ImageUploadDTO model)
		{
			ValidateFileUpload(model);

			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId is null) return BadRequest("Something went wrong! Please try again later.");

			var response = await userService.ProfilePictureUploadAsync(userId, model);
			if (!response.Flag) return BadRequest(response.Message);

			return Ok(response.Message);
		}

		// Check if the uploaded file has the correct extension and size
		private void ValidateFileUpload(ImageUploadDTO model)
		{
			var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

			if (!allowedExtensions.Contains(Path.GetExtension(model.File.FileName)))
			{
				ModelState.AddModelError("file", "Only '.jpg', '.jpeg' or '.png' file extensions are supported!");
			}

			if (model.File.Length > 10485760)
			{
				ModelState.AddModelError("file", "Only files with size 10MB or less are allowed!");
			}
		}
	}
}