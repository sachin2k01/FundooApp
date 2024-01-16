using ModelLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IUserNotesRepo
    {
        public UserNotesEntity CreateUserNotes(UserNotesModel notes);

    }
}
