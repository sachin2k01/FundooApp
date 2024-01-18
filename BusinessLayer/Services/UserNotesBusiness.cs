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

    }
}
