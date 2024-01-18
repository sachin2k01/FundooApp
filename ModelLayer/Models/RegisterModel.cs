using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage ="Entered Name is Incorrect")]
        [RegularExpression(@"^[A-Z][a-z]+$")]
        
        public string FirstName { get; set; }

        [Required(ErrorMessage ="Enter Last Name Correctly")]
        [RegularExpression(@"^ [a-zA-Z]*$")]
        public string LastName { get; set; }


        [Required]
        [RegularExpression(@"^[a-z]{1}[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-]+$")]
        [DataType(DataType.EmailAddress)]

        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]{1,3}\w[a-z0-9!@]{7,}$")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
