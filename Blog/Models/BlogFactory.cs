using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyToolNetStandard;
using Dapper;
using System.Data.SqlClient;

namespace Blog.Models
{
    public static class BlogFactory
    {
        static ExternalJsonConfigReader configReader = new ExternalJsonConfigReader(@"C:\Config\config.json");
        static string blogConnStr = configReader.GetValue("BlogConnStr");
        //static Dictionary<int, Blog> blogs = new Dictionary<int, Blog>();
        static BlogFactory()
        {
            
        }
        public static void UpdateCache()
        {
            
        }
        public static List<Blog> GetBlogs()
        {
            using (var connection = new SqlConnection(blogConnStr))
            {
                return connection.Query<Blog>("select * from blog").ToList();
            }
            
            //return Sql.Query<Blog>(blogConnStr, "select * from dbo.blog");
        }
        public static List<Blog> GetBlog(int id)
        {
            using (var connection = new SqlConnection(blogConnStr))
            {
                return connection.Query<Blog>($"select * from dbo.blog where id={id}").ToList();
            }
            //return Sql.Query<Blog>(blogConnStr, $"select * from dbo.blog where id={id}");
        }
    }
}
