using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId {  get; set; }

        [Required]
        public string FirstName {  get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
        
        public string Password { get; set; }

        public virtual ICollection<UserNotesEntity> UserNotes { get; set; }

    }
}
