using System;
using System.Linq;
using System.Collections.Generic;
using Sample.Models;
using Sample.Services;
using Newtonsoft.Json;

namespace Sample.DataServices
{
    public class UsersDataService
    {
        public static user Register(string email, string password, SampleDBContext DBContext = null)
        {
            if(DBContext == null)
            {
                DBContext = new SampleDBContext();
            }
            SaltedPassword saltedPassword = EncryptoService.GenerateSaltedHash(64, password);
            user newUser = new user() { email = email, salt = saltedPassword.Salt, password = saltedPassword.Hash, created_at = DateTime.Now };
            try
            {
                DBContext.users.Add(newUser);
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return newUser;
        }

        public static Boolean SetupUserToForgotPasswordState(user user, SampleDBContext DBContext = null)
        {
            if (DBContext == null)
            {
                DBContext = new SampleDBContext();
            }
            // update user reset_password_token
            user.reset_password_token = EncryptoService.GetSpecificLengthRandomString(64, true);
            user.reset_experation = DateTime.Now.AddMinutes(10);
            return DBContext.SaveChanges() == 1;
        }

        public static user GetUserByEmail(string email, SampleDBContext DBContext = null)
        {
            if (DBContext == null)
            {
                DBContext = new SampleDBContext();
            }
            user user = DBContext.users.FirstOrDefault(x => x.email == email);
            return user;
        }
    }
}