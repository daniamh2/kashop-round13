using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace KASHOP.dal.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string? Sity { get; set; }
        public string? Street { get; set; }
        public string? PasswordResetCode{ get; set; }
        public DateTime? PasswordResetCodeExpiry { get; set; }
    }
}
