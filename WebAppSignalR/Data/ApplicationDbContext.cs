using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAppSignalR.Areas.Identity.Models;

namespace WebAppSignalR.Data
{
    public class ApplicationDbContext : IdentityDbContext<CustomIdentityUserModel>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
