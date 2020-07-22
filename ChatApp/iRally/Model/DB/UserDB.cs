using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace iRally.Model.DB
{
    //DBから認証を行うクラス
    public class UserDB
    {
        public UserDB(UserInfo userInfo)
        {
            UserId = userInfo.UserId;
            Password = userInfo.Password;
        }

        private const string _table = "Users";
        public string UserId { get; }
        public string Password { get; }
        public UserInfo UserData => GetUserData();
        public bool IfUserExits => UserIdChecker();
        public bool IfPasswordCorrect => PasswordChecker(UserData);
        public bool IfHashed => HashedChecker(UserData);

        private UserInfo GetUserData()
        {
            var sql = $@"select * from {_table} where UserId = '{UserId}'";
            using var conn = new SqlConnection(Startup.ConnString);
            conn.Open();
            using var cmd = new SqlCommand(sql, conn);
            using var dr = cmd.ExecuteReader();
            dr.Read();
            return new UserInfo(dr);
        }

        private bool UserIdChecker()
        {
            var sql = $@"select UserId from {_table}";

            using var conn = new SqlConnection(Startup.ConnString);
            conn.Open();
            using var cmd = new SqlCommand(sql, conn);
            using var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (UserId != dr["UserId"].ToString()) continue;
                return true;
            }
            return false;
        }

        private bool PasswordChecker(UserInfo db)
        {
            const int saltTimes = 10000;
            const int passwordSize = 32;

            var saltArray = Convert.FromBase64String(db.PasswordSalt);
            var userPassword = Convert.FromBase64String(db.Password);

            using var derive = new Rfc2898DeriveBytes(Password, saltArray, saltTimes);
            var bytHashedPassword = derive.GetBytes(passwordSize);
            return bytHashedPassword.SequenceEqual(userPassword);
        }

        private bool HashedChecker(UserInfo db)
        {
            if (db.PasswordType == 0)
            {
                return false;
            }
            return db.PasswordType == 1;
        }
    }
}