using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly string _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

        public UploadController()
        {
            if (!Directory.Exists(_uploadPath))
                Directory.CreateDirectory(_uploadPath);
        }

        [HttpPost]
        public async Task<IActionResult> UploadChunk(IFormFile file, [FromForm] string fileName, [FromForm] int chunkIndex, [FromForm] int totalChunks)
        {
            var tempFilePath = Path.Combine(_uploadPath, fileName + ".part");

            await using (var stream = new FileStream(tempFilePath, FileMode.Append))
            {
                await file.CopyToAsync(stream);
            }

            if (chunkIndex == totalChunks - 1)
            {
                var finalFilePath = Path.Combine(_uploadPath, fileName);
                if (System.IO.File.Exists(finalFilePath))
                    System.IO.File.Delete(finalFilePath);

                System.IO.File.Move(tempFilePath, finalFilePath);
            }

            return Ok(new { chunkIndex, totalChunks });
        }
    }
}