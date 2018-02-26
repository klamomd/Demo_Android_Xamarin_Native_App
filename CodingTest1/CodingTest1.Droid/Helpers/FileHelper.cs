using System;
using System.Collections.Generic;
using System.Text;
using CodingTest1.Droid.Models;

namespace CodingTest1.Droid.Helpers
{
    public static class FileHelper
    {
        public static string ConvertUserToFileString(User user)
        {
            return user.Username + ":" + user.Password;
        }

        public static User ConvertFileStringToUser(string fileString)
        {
            string[] values = fileString.Split(':');
            return new User(values[0], values[1]);
        }
    }
}
