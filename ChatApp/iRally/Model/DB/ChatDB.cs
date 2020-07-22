using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace iRally.Model
{
    public class ChatDB
    {
        private const string _table = "ChatLogs";
        private static readonly int _messageShown = 20;

        public static List<MessageInfo> GetChatHistory()
        {
            var history = new List<MessageInfo>();
            var sql = $@"select top {_messageShown} * from {_table} order by id desc";

            using var conn = new SqlConnection(Startup.ConnString);
            conn.Open();
            using var cmd = new SqlCommand(sql, conn);
            using var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                history.Add(new MessageInfo(dr));
            }
            return history;
        }

        public static void SendMessage(string message, string userId)
        {
            if (message == null)
            {
                return;
            }
            var history = GetChatHistory();
            var messageCount = history.Count;
            var sql = $@"select * from {_table}";
            var dt = new DataTable();

            using var ada = new SqlDataAdapter(sql, Startup.ConnString);
            ada.Fill(dt);
            var newRow = dt.NewRow();
            newRow["Message"] = message;
            newRow["PostAt"] = DateTime.Now.ToShortTimeString();
            newRow["Id"] = messageCount + 1;
            newRow["UserId"] = userId;

            dt.Rows.Add(newRow);

            var builder = new SqlCommandBuilder(ada);
            ada.InsertCommand = builder.GetInsertCommand();
            ada.Update(dt);
        }
    }
}