using ModelLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using BusinessLayer.Interface;
using RepositoryLayer.Interfaces;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Services
{
    public class UserNotesBusiness:IUserBusinessNotes
    {
        private readonly IUserNotesRepo _notesrepo;
        public UserNotesBusiness(IUserNotesRepo notesrepo)
        {
            this._notesrepo = notesrepo;
            
        }
        public UserNotesEntity CreateUserNotes(UserNotesModel notes, int userId)
        {
            return _notesrepo.CreateUserNotes(notes,userId);
        }
        public UserNotesEntity GetNotesById(int noteId, int userId)
        {
            return _notesrepo.GetNotesById(noteId,userId);
        }
        public List<UserNotesEntity> GetAllNodes()
        {
            return _notesrepo.GetAllNodes();
        }

        public List<UserNotesEntity> GetUserNotesById(int userId)
        {
            return _notesrepo.GetUserNotesById(userId);
        }
        public UserNotesEntity UpdateNotes(int userId, int noteId, NotesUpdateModel notesModel)
        {
            return _notesrepo.UpdateNotes(userId, noteId, notesModel);
        }
        public string DeleteNode(int id, int userId)
        {
            return _notesrepo.DeleteNode(id, userId);
        }
    }
}
