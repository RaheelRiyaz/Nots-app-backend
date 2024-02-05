using AutoMapper;
using NoteTakingApp.Application.Abstractions.IRepository;
using NoteTakingApp.Application.Abstractions.IServices;
using NoteTakingApp.Application.APIResponse;
using NoteTakingApp.Application.DTOS;
using NoteTakingApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTakingApp.Application.Services
{
    public class NotesService : INotesService
    {
        private readonly INotesRepository notesRepository;
        private readonly IMapper mapper;
        private readonly IContextService contextService;
        private readonly IStorageService storageService;

        public NotesService(INotesRepository notesRepository,IMapper mapper,IContextService contextService, IStorageService storageService)
        {
            this.notesRepository = notesRepository;
            this.mapper = mapper;
            this.contextService = contextService;
            this.storageService = storageService;
        }



        public async Task<APIRESPONSE<NoteResponse>> AddNote(NoteRequest model)
        {
            var note = mapper.Map<Note>(model);

            if (model.File is not null)
            {
                var filePath = await storageService.SaveFileAsync(model.File);
                note.FilePath = filePath;
                
            }

            note.EntityId = await contextService.GetId();

            var res = await notesRepository.AddAsync(note);

            if (res > 0) 
                return APIRESPONSE<NoteResponse>.SuccessResponse(mapper.Map<NoteResponse>(note),message:"Note added successfully");

            return APIRESPONSE<NoteResponse>.ErrorResponse();
        }




        public async Task<APIRESPONSE<int>> CheckTrashedNotes()
        {
            //await notesRepository.CheckTrashedNotes(await contextService.GetId());

            var res = await notesRepository.CheckTrashedNotes(await contextService.GetId());

            if (res > 0) 
                return APIRESPONSE<int>.SuccessResponse(res, "Expired trash notes cleared");


            return APIRESPONSE<int>.ErrorResponse();
        }




        public async Task<APIRESPONSE<NoteResponse>> DeleteNote(Guid id)
        {
            var note = await notesRepository.GetByIdAsync(id);
           
            if (note is null)
                return APIRESPONSE<NoteResponse>.ErrorResponse();

            var res = await notesRepository.RemoveNote(note.Id);

            if(res > 0)
                return APIRESPONSE<NoteResponse>.SuccessResponse(mapper.Map<NoteResponse>(note),"Note deleted successfully");

            return APIRESPONSE<NoteResponse>.ErrorResponse();

        }



        public async Task<APIRESPONSE<int>> DeleteNotes(List<Guid> ids)
        {
            var res = await notesRepository.DeleteRangeAsync(ids);

            if (res > 0)
                return APIRESPONSE<int>.SuccessResponse(res, $"{res} notes deleted successfully");

            return APIRESPONSE<int>.ErrorResponse();
        }




        public async Task<APIRESPONSE<int>> DeleteTrashNote(Guid id)
        {
            var note = await notesRepository.GetTrashedNote(id);
            var res = await notesRepository.RemoveTrashNote(id);

            if (note?.FilePath is not null)
            {
                await storageService.DeleteFileAsync(note.FilePath);
            }

            if (res > 0)
                return APIRESPONSE<int>.SuccessResponse(res, "Note removed from trash successfully");

            return APIRESPONSE<int>.ErrorResponse();
        }




        public async Task<APIRESPONSE<IEnumerable<NoteResponse>>> GetAllTrashNotes()
        {
            await notesRepository.CheckTrashedNotes(await contextService.GetId());

            var notes = await notesRepository.GetTrashNotes(await contextService.GetId());

            return APIRESPONSE<IEnumerable<NoteResponse>>.SuccessResponse(notes);
        }



        public async Task<APIRESPONSE<IEnumerable<NoteResponse>>> GetAllUserNotes()
        {
            var notes = await notesRepository.GetNotesByEntity(await  contextService.GetId());

            return APIRESPONSE<IEnumerable<NoteResponse>>.SuccessResponse(notes);
        }



        public async Task<APIRESPONSE<NoteResponse>> GetNoteById(Guid id)
        {
            var note = await notesRepository.GetByIdAsync(id);

            if(note is null)
                return APIRESPONSE<NoteResponse>.ErrorResponse();

            return APIRESPONSE<NoteResponse>.SuccessResponse(mapper.Map<NoteResponse>(note));
        }



        public async Task<APIRESPONSE<int>> RecoverNote(RecoverNoteRequest model)
        {
            var res = await notesRepository.RecoverNote(model.Id);
            if (res > 0) return APIRESPONSE<int>.SuccessResponse(res, "Note recovered successfully");

            return APIRESPONSE<int>.ErrorResponse();
        }



        public async Task<APIRESPONSE<int>> RemoveNoteImage(Guid id)
        {
            var note = await notesRepository.GetByIdAsync(id);

            if (note is null) 
                return APIRESPONSE<int>.ErrorResponse();

            await storageService.DeleteFileAsync(note.FilePath!);

            note.FilePath = null;
            await notesRepository.UpdateAsync(note);


            return APIRESPONSE<int>.SuccessResponse(1,message:"Note image removed successfully");
        }



        public async Task<APIRESPONSE<NoteResponse>> UpdateNote(UpdateNoteRequest model)
        {
            var note = await notesRepository.GetByIdAsync(model.Id);
            if(note is null) return APIRESPONSE<NoteResponse>.ErrorResponse();

            note.UpdatedOn = DateTime.Now;
            note.Title = model.Title;
            note.Description = model.Description;


            var res = await notesRepository.UpdateAsync(note);

            if (res > 0) 
                return APIRESPONSE<NoteResponse>.SuccessResponse(mapper.Map<NoteResponse>(note),"Note updated successfully");

            return APIRESPONSE<NoteResponse>.ErrorResponse();
        }
    }
}
