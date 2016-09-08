using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pageLudo.Models
{
    public class UserListingModel
    {
        public UserListingData EditObject { get; set; }
        public List<UserListingData> List { get; set; }
    }
}