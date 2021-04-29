using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace _18TWENTY8.Models
{

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base() { }
        public String UserNamedisp { get; set; }

    }
}


