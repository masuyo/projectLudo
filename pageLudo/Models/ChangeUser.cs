﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace pageLudo.Models
{
    public class ChangeUser
    {
        // no required

        [StringLength(50, MinimumLength = 6, ErrorMessage = "Username must be at least 6 char long")]
        public string Username { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be at least 6 char long")]
        public string Password { get; set; }

        // nincs benne az adatbázisban, csak a regisztrációhoz kell
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [Compare("Password", ErrorMessage = "Does not match!")]
        public string ConfirmPassword { get; set; }

        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$",
ErrorMessage = "Wrong e-mail")]
        public string EmailID { get; set; }
    }
}