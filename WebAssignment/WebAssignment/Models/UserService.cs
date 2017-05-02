using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAssignment.Models;
using System.Linq.Expressions;

namespace WebAssignment.Models
{
    public abstract class UserService
    {

        //Fetches user by name and password
        public static User getUser(string userName, string password)
        {
            using (var connection = DbService.getConnection())
            {
                try
                {
                    User user = (from u in connection.User
                                where u.Username == userName && u.Password == password
                                select u).SingleOrDefault();
                    return user;
                }
                catch
                {
                    return null;
                }
            }
        }

        //Fetches user by id
        public static User getUserById(int userId)
        {
            using (var connection = DbService.getConnection())
            {                
                User user = (from u in connection.User
                             where u.Id == userId
                             select u).SingleOrDefault();
                return user;
            }
        }

        //Create method for user table
        public static User createUser(User newUser)
        {
            var check = checkIfUserAlreadyExists(newUser.Username);
                if (check == true) {
                    return null; 
                }
                else{
                    using (var dbconnection = DbService.getConnection()) { 
                        dbconnection.User.Add(newUser);
                        dbconnection.SaveChanges();
                        return newUser;
                    }
                }   
        }

        //Method to check if username already exists in db
        public static bool checkIfUserAlreadyExists(String newUsername)
        {
            using (var dbconnection = DbService.getConnection())
            {
                List<User> existingUsers = (from x in dbconnection.User
                                            select x).ToList();
                bool found = false;
                    foreach (var u in existingUsers)
                    {
                        if (u.Username.Equals(newUsername))
                        {
                            found = true;
                            break;
                        }
                    }
                return found;
            }
        }
    }
}