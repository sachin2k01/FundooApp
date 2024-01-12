using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public class ForgotPasswordModel
    {
        public string eMail {  get; set; }

        public string Subject {  get; set; }
        public string Message { get; set; }



    }
}
