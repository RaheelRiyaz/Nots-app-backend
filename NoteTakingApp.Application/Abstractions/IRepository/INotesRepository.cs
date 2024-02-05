using NoteTakingApp.Application.DTOS;
using NoteTakingApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTakingApp.Application.Abstractions.IRepository
{
    public interface INotesRepository : IBaseRepository<Note>
    {
        Task<IEnumerable<NoteResponse>> GetNotesByEntity(Guid entityId);
        Task<IEnumerable<NoteResponse>> GetTrashNotes(Guid entityId);
        Task<int> RecoverNote(Guid id);
        Task<int> RemoveNote(Guid id);
        Task<int> RemoveTrashNote(Guid id);
        Task<int> CheckTrashedNotes(Guid entityId);
        Task<NoteResponse?> GetTrashedNote(Guid id);
    }
}
