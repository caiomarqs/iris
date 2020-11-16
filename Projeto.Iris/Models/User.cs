using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Iris.Models
{
    public class User
    {
        [HiddenInput]
        public int UserId { get; set; }

        [Required (ErrorMessage = "Nome é obrigatório"), Display(Name = "Nome")] 
        [MaxLength(40, ErrorMessage = "Máximo de 40 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O cpf é obrigatório")]
        [MaxLength(11, ErrorMessage = "Máximo de 11 caracteres")]
        public string Cpf { get; set; }

        [EmailAddress, Required(ErrorMessage = "Email Obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória"), Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public ICollection<Photo> Photos { get; set; }
    }
}
