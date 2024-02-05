using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTakingApp.Application.Abstractions.IServices
{
    public interface IStorageService
    {
        Task<string> SaveFileAsync(IFormFile file);
        Task<string> DeleteFileAsync(string filePath);
    }
}
