using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IColloboratorBusiness
    {
        public ColloboratorEntity AddColloborator(int NoteId, string collob_Email, int userId);
        public ColloboratorEntity GetColloboratorById(int id);

        public string DeleteColloborator(string collob_Email);

    }
}
