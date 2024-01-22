using BusinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ColloboratorBusiness:IColloboratorBusiness
    {
        private readonly IColloboratorRepo collobrepo;
        public ColloboratorBusiness(IColloboratorRepo collobrepo)
        {
            this.collobrepo = collobrepo;
            
        }
        public ColloboratorEntity AddColloborator(int NoteId, string collob_Email, int userId)
        {
            return collobrepo.AddColloborator(NoteId, collob_Email, userId);
        }

        public ColloboratorEntity GetColloboratorById(int id)
        {
            return collobrepo.GetColloboratorById(id);
        }

        public string DeleteColloborator(string collob_Email)
        {
            return collobrepo.DeleteColloborator(collob_Email);
        }

    }
}
