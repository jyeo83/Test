using System;
using System.Collections.Generic;
using System.Text;
using Test.DOM.Models;

namespace Test.DAL.Repositories.UserRepo
{
    public interface IUserRepo
    {
        bool Create(User user, string password);
        User Authenticate(string username, string password);
        void LogOut();
    }
}
