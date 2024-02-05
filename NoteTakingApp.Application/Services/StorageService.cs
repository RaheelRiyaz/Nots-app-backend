using Microsoft.AspNetCore.Http;
using NoteTakingApp.Application.Abstractions.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTakingApp.Application.Services
{
    public class StorageService : IStorageService
    {
        private readonly string webRootPath;

        public StorageService(string webRootPath)
        {
            this.webRootPath = webRootPath;
        }



        public async Task<string> DeleteFileAsync(string filePath)
        {
            await Task.Run(() => File.Delete(webRootPath + filePath));

            return filePath;
        }



        public async Task<string> SaveFileAsync(IFormFile file)
        {
            var path = string.Concat(Guid.NewGuid(), file.FileName);
            var fullPath = string.Concat(GetPhysicalPath(),path);

            using var fs = new FileStream(fullPath, FileMode.Create);

            await file.CopyToAsync(fs);

            return GetVirtualPath()+path;
        }





        private string GetPhysicalPath()
        {
            var path = webRootPath + GetVirtualPath();

            if (!Path.Exists(path)) Directory.CreateDirectory(path);

            return path;
        }

        private string GetVirtualPath() => "/files/";
    }
}
