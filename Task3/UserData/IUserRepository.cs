using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task3.Models;

namespace Task3.UserData
{
    interface IUserRepository
    {
        void Post(User inputUser);
        User[] GetAll();
        User Get(int inputId);
        User Get(string inputUsername, string inputPassword);
        void Put(int inputId, User inputUser);
        void Delete(int inpuptId);
    }
}
