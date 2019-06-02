using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Akeem.ChangRead.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private protected IConfiguration configuration;
        public FileController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet("{file}")]
        public IActionResult Images(string file)
        {
            var stream = GetFileByte(file);
            if (stream == null)
            {
                stream = GetFileByte("nofile.jpg");
            }
            return File(stream, "image/jpeg");
        }

        [HttpGet("{file}")]
        public IActionResult Mp3(string file)
        {
            var stream = GetFileByte(file);
            if (stream == null)
            {
                stream = GetFileByte("nofile.jpg");
            }
            return File(stream, "audio/mp3");
        }

        [HttpGet("{file}")]
        public IActionResult Mp4(string file)
        {
            var stream = GetFileByte(file);
            if (stream == null)
            {
                stream = GetFileByte("nofile.jpg");
            }
            return File(stream, "video/mpeg4");
        }

        private byte[] GetFileByte(string file)
        {
            var dir = configuration["DownloadDirectory"];
            dir = dir.Replace("/", "\\");
            file = file.Replace("/", "\\");
            if (dir.Last() != '\\' && file.Last() != '\\')
            {
                dir += "\\";
            }
            var fileUrl = $"{dir}{file}";
            if (!System.IO.File.Exists(fileUrl))
            {
                return null;
            }
            else
            {
                return System.IO.File.ReadAllBytes(fileUrl);
            }
        }
    }
}
