﻿using Microsoft.AspNetCore.Http;
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
        public UserNotesEntity CreateUserNotes(UserNotesModel notes, int userId);
        public UserNotesEntity GetNotesById(int noteId,int userId);
        public List<UserNotesEntity> GetAllNodes();
        public List<UserNotesEntity> GetUserNotesById(int userId);
        public UserNotesEntity UpdateNotes(int noteId, int userId, NotesUpdateModel notesModel);
        public UserNotesEntity DeleteNode(int id, int userId);

        public UserNotesEntity ArchieveNotes(int userId, int noteId);

        public UserNotesEntity TrashNotes(int userId, int noteId);

        public UserNotesEntity AddNoteColor(int userId, int noteId, string color);



    }
}
