using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RichTextEditor.Controller
{
    [ApiController]
    [Route("api/froala")]
    public class FroalaController1 : Microsoft.AspNetCore.Mvc.Controller
    {
        [HttpPost("file")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            // Save the file to a folder (e.g., "wwwroot/uploads")
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","richtexteditorfileupload", "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, file.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Return the URL to access the uploaded file
            var fileUrl = $"/uploads/{file.FileName}";
            return Ok(new { link = fileUrl });
        }

        [HttpPost("image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No image uploaded");
            }

            // Save the image to a folder (e.g., "wwwroot/images")
            var imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "richtexteditorfileupload", "images");
            if (!Directory.Exists(imagesFolder))
            {
                Directory.CreateDirectory(imagesFolder);
            }

            var filePath = Path.Combine(imagesFolder, file.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Return the URL to access the uploaded image
            var imageUrl = $"/images/{file.FileName}";
            return Ok(new { link = imageUrl });
        }


        [HttpPost("video")]
        public async Task<IActionResult> UploadVideo(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No video uploaded");
            }

            // Save the video to a folder (e.g., "wwwroot/videos")
            var videosFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "richtexteditorfileupload", "videos");
            if (!Directory.Exists(videosFolder))
            {
                Directory.CreateDirectory(videosFolder);
            }

            var filePath = Path.Combine(videosFolder, file.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Return the URL to access the uploaded video
            var videoUrl = $"/videos/{file.FileName}";
            return Ok(new { link = videoUrl });
        }

        [HttpGet("folder")]
        public IActionResult GetImagesFromFolder()
        {
            // Define the folder where images are stored
            var imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "richtexteditorfileupload", "uploads");

            if (!Directory.Exists(imagesFolder))
            {
                return NotFound("Folder not found");
            }

            // Get the list of image files in the folder
            var imageFiles = Directory.GetFiles(imagesFolder)
                .Select(Path.GetFileName) // Get the file names
                .Where(f => f.EndsWith(".jpg") || f.EndsWith(".png") || f.EndsWith(".jpeg")) // Filter only images
                .Select(f => new { url = $"/uploads/{f}", thumb = $"/uploads/{f}" }); // Generate the URL

            return Ok(imageFiles); // Return the list of image URLs
        }

        [HttpDelete("delete")]
        public IActionResult DeleteImage([FromForm] string src)
        {
            // Define the path to the image to be deleted
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + src);

            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound("Image not found");
            }

            // Delete the image
            System.IO.File.Delete(imagePath);

            return Ok("Image deleted successfully");
        }
    }
}
