using ModelLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class UserNotesRepo:IUserNotesRepo
    {
        private readonly FundooContext _fundooContext;
        public UserNotesRepo(FundooContext fundooContext)
        {
            this._fundooContext = fundooContext;
            
        }

        public UserNotesEntity CreateUserNotes(UserNotesModel notes)
        {
            UserNotesEntity notesEntity = new UserNotesEntity();
            notesEntity.Title = notes.Title;
            notesEntity.Description = notes.Description;
            notesEntity.Color = notes.Color;
            notesEntity.ImagePaths = notes.ImagePaths;
            notesEntity.Remainder = notes.Remainder;
            notesEntity.IsArchive = notes.IsArchive;
            notesEntity.IsPinned = notes.IsPinned;
            notesEntity.IsTrash= notes.IsTrash;
            _fundooContext.UserNotes.Add(notesEntity);
            _fundooContext.SaveChanges();
            return notesEntity;

        }

    }
}
