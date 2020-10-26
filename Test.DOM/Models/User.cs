using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.DOM.Models
{
    public class User : IdentityUser<int>
    {
        //public int UserId { get => base.Id; set => base.Id = value; }
        public string EmployeeNo { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }

        // Delete below after successful test of SSO
        public byte[] PasswordSalt { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<UserClaim> Claims { get; set; }
        public ICollection<IdentityUserToken<int>> Tokens { get; set; }
        public ICollection<IdentityUserLogin<int>> Logins { get; set; }
    }
}
