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
                return connection.Query<Blog>("select * from view_visible_blog order by id desc").ToList();
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
            if (page == 0)
            {
                throw new Exception("page can't be 0, please pass 1 or larger.");
            }
            using (var connection = new SqlConnection(blogConnStr))
            {
                return connection.Query<Blog>($@"select * from GetPage({page},{blogPerPage})").ToList();

                //return connection.Query<Blog>($"select top {page*blogPerPage} * from blog where visible = 1 order by id desc").ToList();
            }

            //return Sql.Query<Blog>(blogConnStr, "select * from dbo.blog");
        }
        public static int GetTotalPages()
        {
            using (var connection = new SqlConnection(blogConnStr))
            {
                //id is int so 5 = 0, 15 = 1, need to add 1
                return connection.QuerySingle<int>($"SELECT COUNT(id)/10+1 as page FROM view_visible_blog");

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
                return connection.Query<Blog>($"select * from view_visible_blog where id={id}").ToList();
            }
            //return Sql.Query<Blog>(blogConnStr, $"select * from dbo.blog where id={id}");
        }
        public static BlogIdPair GetNextBlog(int id)
        {
            try
            {
                using (var connection = new SqlConnection(blogConnStr))
                {
                    return connection.QuerySingle<BlogIdPair>($"select * from GetNextBlog({id})");
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public static BlogIdPair GetPrevBlog(int id)
        {
            try
            {
                using (var connection = new SqlConnection(blogConnStr))
                {
                    return connection.QuerySingle<BlogIdPair>($"select * from GetPrevBlog({id})");
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }
    }
}
