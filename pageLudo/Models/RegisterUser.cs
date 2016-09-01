using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace pageLudo.Models
{
    public class RegisterUser
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Missing username", AllowEmptyStrings = false)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Missing password", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Password { get; set; }

        // nincs benne az adatbázisban, csak a regisztrációhoz kell
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [Compare("Password", ErrorMessage = "Does not match!")]
        public string ConfirmPassword { get; set; }

        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$",
ErrorMessage = "Wrong e-mail")]
        public string EmailID { get; set; }

        public string Status { get; set; }
        public string Token { get; set; }
    }
}