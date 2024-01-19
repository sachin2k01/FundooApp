using Microsoft.AspNetCore.Http;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IUserBusinessNotes
    {
        public UserNotesEntity CreateUserNotes(UserNotesModel notes,int userId);
        public UserNotesEntity GetNotesById(int noteId,int userId);
        public List<UserNotesEntity> GetAllNodes();
        public List<UserNotesEntity> GetUserNotesById(int userId);
        public UserNotesEntity UpdateNotes(int userId, int noteId, NotesUpdateModel notesModel);
        public string DeleteNode(int id, int userId);
    }
}
