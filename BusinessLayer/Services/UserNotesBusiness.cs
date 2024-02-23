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
        public UserNotesEntity DeleteNode(int id, int userId)
        {
            return _notesrepo.DeleteNode(id, userId);
        }

        public UserNotesEntity ArchieveNotes(int userId, int noteId)
        {
            return _notesrepo.ArchieveNotes(userId, noteId);
        }

        public UserNotesEntity TrashNotes(int userId, int noteId)
        {
            return _notesrepo.TrashNotes(userId, noteId);
        }
        public UserNotesEntity AddNoteColor(int userId, int noteId, string color)
        {
            return _notesrepo.AddNoteColor(userId, noteId, color);
        }
    }
}
