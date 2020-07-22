using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using iRally.Model.DB;

namespace iRally.Model
{
    public class UserInfo
    {
        [Required(ErrorMessage = "必須")]
        [DisplayName("ユーザーID")]
        public string UserId { get; set; }

        public string UserName { get; set; }

        public byte PasswordType { get; set; }

        [Required(ErrorMessage = "必須")]
        [DisplayName("パスワード")]
        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        public bool IsAdministrator { get; set; }

        public UserInfo(SqlDataReader dr)
        {
            UserId = dr["UserId"].ToString();
            UserName = dr["UserName"].ToString();
            PasswordType = (byte)dr["PasswordType"];
            PasswordSalt = dr["PasswordSalt"].ToString();
            Password = dr["Password"].ToString();
            IsAdministrator = (bool)dr["IsAdministrator"];
        }

        public UserInfo()
        {
        }
    }
}