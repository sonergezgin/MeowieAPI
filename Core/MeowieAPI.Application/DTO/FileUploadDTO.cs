using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MeowieAPI.Application.DTO
{
    public class FileUploadDTO
    {
        public string Username { get; set; }
        public IFormFile Image { get; set; }
    }
}
