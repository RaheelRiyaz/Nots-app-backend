using NoteTakingApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTakingApp.Application.Abstractions.IServices
{
    public interface ITokenService
    {
        string GnerateToken(User model);
    }
}
