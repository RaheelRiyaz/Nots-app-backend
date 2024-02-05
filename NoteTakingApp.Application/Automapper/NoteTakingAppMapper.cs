using AutoMapper;
using NoteTakingApp.Application.DTOS;
using NoteTakingApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTakingApp.Application.Automapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserResponse>();   
        }
    }



    public class NoteMapper : Profile
    {
        public NoteMapper()
        {
            CreateMap<NoteRequest, Note>();
            CreateMap<Note, NoteResponse>();
            CreateMap<UpdateNoteRequest, Note>();
        }
    }
}
