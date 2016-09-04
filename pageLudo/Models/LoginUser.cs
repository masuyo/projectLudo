using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace pageLudo.Models
{
    public class LoginUser
    {
        public int UserID { get; set; }

        public string Username { get; set; }

        [Required(ErrorMessage = "Missing password", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Missing email", AllowEmptyStrings = false)]
        public string EmailID { get; set; }

        public string Role { get; set; }

        public string Status { get; set; }
        public string Token { get; set; }
    }
}