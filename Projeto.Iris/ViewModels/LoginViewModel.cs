using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Projeto.Iris.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "É necessario o email para fazer o login!"), 
         EmailAddress(ErrorMessage = "Insira um email válido!")]
        public string EmailLogin { get; set; }

        [Required(ErrorMessage = "É necessario o senha para fazer o login!")]      
        [DataType(DataType.Password)]
        public string PasswordLogin { get; set; }
    }
}
