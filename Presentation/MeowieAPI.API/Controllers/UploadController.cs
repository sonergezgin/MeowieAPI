using MeowieAPI.Application.Abstractions.Storage;
using MeowieAPI.Application.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeowieAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        readonly IStorageService _storageService;

        public UploadController(IStorageService storageService)
        {
            _storageService = storageService;
        }
        [HttpPost("image")]
        public async Task<IActionResult> Upload([FromForm] FileUploadDTO request)
        {
            var res = await _storageService.UploadAsync(request.Username,"resource/profile-pictures", request.Image);
            return Ok(res);
        }
    }
}
