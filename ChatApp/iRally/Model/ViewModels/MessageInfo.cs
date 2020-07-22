using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace iRally.Model
{
    public class MessageInfo
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("送信時間")]
        public DateTime PostAt { get; set; }

        [DisplayName("メッセージ")]
        public string Message { get; set; }

        [DisplayName("UserId")]
        public string UserId { get; set; }

        public MessageInfo(SqlDataReader dr)
        {
            Id = (int)dr["Id"];
            Message = dr["Message"].ToString();
            UserId = dr["UserId"].ToString();
            PostAt = (DateTime)dr["PostAt"];
        }    
    }
}