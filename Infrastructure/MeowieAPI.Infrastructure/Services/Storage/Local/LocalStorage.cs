using MeowieAPI.Application.Abstractions.Services;
using MeowieAPI.Application.Abstractions.Storage.Local;
using MeowieAPI.Application.Repositories.CommentRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MeowieAPI.Infrastructure.Services.Storage.Local
{


    public class LocalStorage : ILocalStorage
    {

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUserService _userService;
        private readonly ICommentWriteRepository _commentWriteRepository;
        public LocalStorage(IWebHostEnvironment webHostEnvironment, IUserService userService, ICommentWriteRepository commentWriteRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _userService = userService;
            _commentWriteRepository = commentWriteRepository;
        }
        public async Task DeleteAsync(string path, string fileName)
        {
             File.Delete($"{path}\\{fileName}");
            
        }

        public List<string> GetFiles(string path)
        {
            DirectoryInfo directory = new(path);
            return directory.GetFiles().Select(f => f.Name).ToList();
        }

        public  bool HasFile(string path, string fileName)
        {
            return  File.Exists($"{path}\\{fileName}");
        }

        public async Task<(string userName, string fileName)> UploadAsync(string userName, string path, IFormFile file)
        {

            var user = await _userService.GetUserByUsername(userName);
            if (user == null) throw new InvalidOperationException("User not found");

            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            if (user.ProfileImage != null)
            {
                string imagePath = $"{uploadPath}\\{user.ProfileImage}";
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }

            string extension = Path.GetExtension(file.FileName);
            string fileName = $"{userName}{extension}";

            user.ProfileImage = fileName;
            await _commentWriteRepository.SaveAsync();
            await CopyFileAsync($"{uploadPath}\\{fileName}", file);

            return (userName, fileName);
        }

        async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);

                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
