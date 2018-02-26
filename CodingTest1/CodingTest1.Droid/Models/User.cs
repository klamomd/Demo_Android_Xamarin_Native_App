using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace CodingTest1.Droid.Models
{
    public class User
    {
        // CONSTRUCTOR
        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        // PROPERTIES
        public string Username { get; }
        public string Password { get; }
    }
}
