using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.DOM.Models
{
    public class UserClaim : IdentityUserClaim<int>
    {
        public User User { get; set; }
    }
}
