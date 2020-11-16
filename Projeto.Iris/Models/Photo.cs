using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Iris.Models
{
    public class Photo
    {
        public int PhotoId { get; set; }
        public string PhotoBase64Location { get; set; }

        //Bidirecional
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
