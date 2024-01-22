using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class ColloboratorEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int C_Id {  get; set; }

        public string C_Email {  get; set; }

        [ForeignKey("UserNotes")]
        public int NoteId {  get; set; }

        [ForeignKey("Users")]
        public int UserId {get; set; }
        [JsonIgnore]
        public virtual UserEntity Users { get; set; }

        [JsonIgnore]
        public virtual UserNotesEntity UserNotes { get; set; }
    }
}
