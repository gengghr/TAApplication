using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PS4.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the TAUser class
    public class TAUser : IdentityUser
    {
        public string Name { get; set; }
        public string uID { get; set; }

    }
}
