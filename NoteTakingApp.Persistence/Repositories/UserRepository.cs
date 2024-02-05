using NoteTakingApp.Application.Abstractions.IRepository;
using NoteTakingApp.Domain.Entities;
using NoteTakingApp.Persistence.Data;
using NoteTakingApp.Persistence.SharedRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTakingApp.Persistence.Repositories
{
    public class UserRepository: BaseRepository<User>, IUserRepository 
    {
        public UserRepository(NoteTakingAppDbContext context) :base(context) { }
    }
}
