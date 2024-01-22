using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IColloboratorRepo
    {
        public ColloboratorEntity AddColloborator(int NoteId, string collob_Email, int userId);
        public ColloboratorEntity GetColloboratorById(int id);
        public string DeleteColloborator(string collob_Email);

    }
}
