using NoteTakingApp.Application.APIResponse;
using NoteTakingApp.Application.DTOS;
using NoteTakingApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTakingApp.Application.Abstractions.IServices
{
    public interface INotesService
    {
        Task<APIRESPONSE<NoteResponse>> AddNote(NoteRequest model);
        Task<APIRESPONSE<IEnumerable<NoteResponse>>> GetAllUserNotes();
        Task<APIRESPONSE<IEnumerable<NoteResponse>>> GetAllTrashNotes();
        Task<APIRESPONSE<NoteResponse>> GetNoteById(Guid id);
        Task<APIRESPONSE<NoteResponse>> DeleteNote(Guid id);
        Task<APIRESPONSE<int>> DeleteTrashNote(Guid id);
        Task<APIRESPONSE<int>> DeleteNotes(List<Guid> ids);
        Task<APIRESPONSE<int>> RecoverNote(RecoverNoteRequest model);
        Task<APIRESPONSE<int>> CheckTrashedNotes();
        Task<APIRESPONSE<int>> RemoveNoteImage(Guid id);
        Task<APIRESPONSE<NoteResponse>> UpdateNote(UpdateNoteRequest model);
    }
}
