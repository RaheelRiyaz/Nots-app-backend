using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTakingApp.Application.DTOS
{
    public record NoteRequest(string Title, string Description,IFormFile? File);
    public record NoteResponse(Guid Id, string Title, string Description,DateTime CreatedOn,string FilePath);
    public record UpdateNoteRequest(Guid Id, string Title, string Description);
    public record RecoverNoteRequest(Guid Id);
}
