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
    public class ColloboratorRepo:IColloboratorRepo
    {
        private readonly FundooContext fundooContext;
        public ColloboratorRepo(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;           
        }
        public ColloboratorEntity AddColloborator(int noteId,string collob_Email,int userId)
        {
            var UserDetails = fundooContext.UserNotes.FirstOrDefault(x => x.UserId == userId && x.NoteId == noteId);
            if (UserDetails != null)
            {
                ColloboratorEntity colloborator = new ColloboratorEntity();
                colloborator.UserId = userId;
                colloborator.NoteId = noteId;
                colloborator.C_Email = collob_Email;
                fundooContext.Colloborator.Add(colloborator);
                fundooContext.SaveChanges();
                return colloborator;

            }
            else
            {
                return null;
            }
        }

        public ColloboratorEntity GetColloboratorById(int id) 
        {
            var C_user=fundooContext.Colloborator.FirstOrDefault(x=>x.C_Id==id);
            if(C_user != null)
            {
                return C_user;
            }
            else
            {
                return null;
            }
        }

        public string DeleteColloborator(string collob_Email)
        {
            var del_colloborator=fundooContext.Colloborator.FirstOrDefault(x=>x.C_Email==collob_Email);
            if(del_colloborator != null)
            {
                fundooContext.Colloborator.Remove(del_colloborator);
                fundooContext.SaveChanges();
                return "Colloborator deleted successfully";
            }
            else
            {
                return "Invalid Details";
            }
        }
    }
}
