using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task3.Models;

namespace Task3.UserData
{
    public class UserRepository : IUserRepository
    {
        private static User[] users = new User[]
        {
            new User
            {
                Id = 1,
                Username = "Jeremy",
                Password = "password",
                FirstName = "Jeremy",
                LastName = "InWonderland",
                FullName = "Jeremy InWonderland",
                Email = "NaughtyRabbit@Email.com",
                Address = "WonderLand"
            },
            new User
            {
                Id = 2,
                Username = "Jack",
                Password = "password",
                FirstName = "Jack",
                LastName = "TheDebugger",
                FullName = "Bob TheDebugger",
                Email = "CanWeDebugIt@Email.com",
                Address = "NoBugTown",
                IsAdmin = true
            }
        };

        public void Post(User inputUser)
        {
            List<User> userList = users.ToList();
            userList.Add(inputUser);
            users = userList.ToArray();
        }

        public User[] GetAll() => users;

        public User Get(int inputId) => users.FirstOrDefault(u => u.Id == inputId);

        public User Get(string username, string password)
        {
            User returnedUser =
                (from u in users
                 where ((u.Username).ToLower() == username.ToLower()) && (u.Password == password)
                 select u).FirstOrDefault();
            return returnedUser;
        }

        public void Put(int inputId, User inputUser)
        {
            users = users
                .Where(u => u.Id == inputUser.Id)
                .Select(u =>
                {
                    u = inputUser;
                    return u;
                }
                ).ToArray();
        }

        public void Delete(int inputId)
        {
            users = users.Where(u => u.Id != inputId).ToArray();
        }
    }
}