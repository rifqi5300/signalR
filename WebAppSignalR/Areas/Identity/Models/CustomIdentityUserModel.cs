using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppSignalR.Areas.Identity.Models
{
    public class CustomIdentityUserModel : IdentityUser
    {
        public string FirstName { get; set; }
    }
}
