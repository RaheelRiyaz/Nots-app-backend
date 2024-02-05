using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteTakingApp.Application.Abstractions.IServices;
using NoteTakingApp.Application.APIResponse;
using NoteTakingApp.Application.DTOS;

namespace NoteTakingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly INotesService notesService;

        public NotesController(INotesService notesService)
        {
            this.notesService = notesService;
        }


        [HttpPost]
        public async Task<APIRESPONSE<NoteResponse>> AddNote([FromForm] NoteRequest model)
        {
            return await notesService.AddNote(model);
        }



        [HttpGet("{id:guid}")]
        public async Task<APIRESPONSE<NoteResponse>> NoteByid(Guid id)
        {
            return await notesService.GetNoteById(id);
        }


        [HttpGet]
        public async Task<APIRESPONSE<IEnumerable<NoteResponse>>> GetNotes()
        {
            return await notesService.GetAllUserNotes();
        }


        [HttpDelete("{id:guid}")]
        public async Task<APIRESPONSE<NoteResponse>> DeleteNote(Guid id)
        {
            return await notesService.DeleteNote(id);
        }




        [HttpDelete("trash-note/{id:guid}")]
        public async Task<APIRESPONSE<int>> DeleteTrashNote(Guid id)
        {
            return await notesService.DeleteTrashNote(id);
        }



        [HttpDelete("note-image/{id:guid}")]
        public async Task<APIRESPONSE<int>> DeleteNoteImage(Guid id)
        {
            return await notesService.RemoveNoteImage(id);
        }



        [HttpPost("delete-notes")]
        public async Task<APIRESPONSE<int>> DeleteNote(List<Guid> ids)
        {
            return await notesService.DeleteNotes(ids);
        }




        [HttpGet("trash-notes")]
        public async Task<APIRESPONSE<IEnumerable<NoteResponse>>> GetTrashNotes()
        {
            return await notesService.GetAllTrashNotes();
        }


        [HttpPut]
        public async Task<APIRESPONSE<NoteResponse>> UpdateNote(UpdateNoteRequest model)
        {
            return await notesService.UpdateNote(model);
        }



        [HttpPost("recover-note")]
        public async Task<APIRESPONSE<int>> RecoverNote(RecoverNoteRequest model)
        {
            return await notesService.RecoverNote(model);
        }

    }
}
