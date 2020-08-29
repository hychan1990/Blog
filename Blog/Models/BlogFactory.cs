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
        static int blogPerPage = 10;
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
                return connection.Query<Blog>("select * from blog where visible = 1 and deleted = 0 order by id desc").ToList();
            }
            
            //return Sql.Query<Blog>(blogConnStr, "select * from dbo.blog");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page">1 for the first page</param>
        /// <returns></returns>
        public static List<Blog> GetBlogs(int page)
        {
            if (page ==0)
            {
                throw new Exception("page can't be 0, please pass 1 or larger.");
            }
            page = page - 1;
            using (var connection = new SqlConnection(blogConnStr))
            {
                return connection.Query<Blog>($@"SELECT * FROM blog where visible = 1 and deleted = 0
ORDER BY id desc OFFSET {page * blogPerPage} ROWS FETCH NEXT {blogPerPage} ROWS ONLY;").ToList();
                
                //return connection.Query<Blog>($"select top {page*blogPerPage} * from blog where visible = 1 order by id desc").ToList();
            }

            //return Sql.Query<Blog>(blogConnStr, "select * from dbo.blog");
        }
        public static int GetTotalPages()
        {
            using (var connection = new SqlConnection(blogConnStr))
            {
                //id is int so 5 = 0, 15 = 1, need to add 1
                return connection.QuerySingle<int>($"SELECT COUNT(id)/10+1 as page FROM blog where visible = 1 and deleted = 0");
               
            }
        }
        /// <summary>
        /// using int as parameter can avoid sql injection too. how brilliant me!
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Blog> GetBlog(int id)
        {
            using (var connection = new SqlConnection(blogConnStr))
            {
                return connection.Query<Blog>($"select * from dbo.blog where id={id} and visible = 1 and deleted = 0").ToList();
            }
            //return Sql.Query<Blog>(blogConnStr, $"select * from dbo.blog where id={id}");
        }
    }
}
