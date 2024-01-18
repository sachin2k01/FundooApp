﻿using Microsoft.AspNetCore.Http;
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
    }
}
