using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public class UserNotesModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        //public ICollection<IFormFile> ImagePaths { get; set; }
        //[DefaultValue("2024-01-16T11:17:55.323Z")]
        public DateTime Remainder { get; set; }
        public bool IsArchive { get; set; }
        public bool IsPinned { get; set; }
        public bool IsTrash { get; set; }

    }
}
