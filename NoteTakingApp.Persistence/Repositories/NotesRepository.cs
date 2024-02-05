using NoteTakingApp.Application.Abstractions.IRepository;
using NoteTakingApp.Application.DTOS;
using NoteTakingApp.Domain.Entities;
using NoteTakingApp.Persistence.Dapper;
using NoteTakingApp.Persistence.Data;
using NoteTakingApp.Persistence.SharedRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTakingApp.Persistence.Repositories
{
    public class NotesRepository : BaseRepository<Note>, INotesRepository
    {
        public NotesRepository(NoteTakingAppDbContext context) : base(context) { }

        public async Task<int> CheckTrashedNotes(Guid entityId)
        {
            string query = $@"DELETE NotesBacKup WHERE EntityId = @entityId
                            AND DATEDIFF(DAY,DeletedOn,GETDATE()) >= 15";

            return await context.ExecuteAsync(query, new { entityId });
        }




        public async Task<IEnumerable<NoteResponse>> GetNotesByEntity(Guid entityId)
        {
            string query = $@"SELECT Id,Title,
                            Description,CreatedOn,FilePath
                            FROM Notes
                            WHERE EntityId = @entityId
                            ORDER BY CreatedOn DESC";

            return await context.QueryAsync<NoteResponse>(query, new { entityId });
        }



        public async Task<NoteResponse?> GetTrashedNote(Guid id)
        {
            string query = $@"SELECT Id,Title,
                            Description,CreatedOn,FilePath
                            FROM NotesBackup
                            WHERE Id = @id";

            return await context.FirstOrDefaultAsync<NoteResponse>(query, new { id });
                            
        }



        public async Task<IEnumerable<NoteResponse>> GetTrashNotes(Guid entityId)
        {
            string query = $@"SELECT Id,Title,
                            Description,CreatedOn,FilePath
                            FROM NotesBacKup
                            WHERE DATEDIFF(DAY,DeletedOn,GETDATE()) < 15 
                            ORDER BY DeletedOn DESC ";

            return await context.QueryAsync<NoteResponse>(query, new { entityId });
        }




        public async Task<int> RecoverNote(Guid id)
        {
            return await context.ExecuteAsync("RECOVERNOTE", new {id}, commandType: CommandType.StoredProcedure);
        }



        public async Task<int> RemoveNote(Guid id)
        {
            string query = $@"DELETE FROM Notes WHERE ID = @id";

            return await context.ExecuteAsync(query, new { id });
        }

        public async Task<int> RemoveTrashNote(Guid id)
        {
            string query = $@"DELETE FROM NotesBacKup WHERE ID = @id";

            return await context.ExecuteAsync(query, new { id });
        }
    }
}
