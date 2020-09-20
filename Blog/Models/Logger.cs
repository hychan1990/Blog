using Dapper;
using MyToolNetStandard;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public static class Logger
    {
        static ExternalJsonConfigReader configReader = new ExternalJsonConfigReader(@"C:\Config\config.json");
        static string blogConnStr = configReader.GetValue("BlogConnStr");
        public static void InsertLog(string level, string header, string content)
        {
            using (var connection = new SqlConnection(blogConnStr))
            {
                connection.Execute("insert into dbo.log " +
                    "values(getdate(), @log_level, @log_header, @log_content)",
                    new { log_level = level, log_header = header, log_content = content });
            }
        }
    }
}
