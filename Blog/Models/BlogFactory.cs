﻿using System;
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
        #region get part
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
        public static List<Blog> GetBlogsByPage(int page)
        {
            if (page == 0)
            {
                throw new Exception("page can't be 0, please pass 1 or larger.");
            }
            using (var connection = new SqlConnection(blogConnStr))
            {
                return connection.Query<Blog>($@"select * from GetPage(@page,@blogPerPage)"
                    , new { page = page, blogPerPage = blogPerPage }).ToList();

                //return connection.Query<Blog>($"select top {page*blogPerPage} * from blog where visible = 1 order by id desc").ToList();
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page">1 for the first page</param>
        /// <returns></returns>
        public static List<Blog> GetBlogsByTag(int page, string tag)
        {
            if (page == 0)
            {
                throw new Exception("page can't be 0, please pass 1 or larger.");
            }
            using (var connection = new SqlConnection(blogConnStr))
            {
                return connection.Query<Blog>($@"select * from GetPageByTag(@page,@blogPerPage,@tag)"
                    , new { page = page, blogPerPage = blogPerPage, tag = tag }).ToList();

                //return connection.Query<Blog>($"select top {page*blogPerPage} * from blog where visible = 1 order by id desc").ToList();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page">1 for the first page</param>
        /// <returns></returns>
        public static List<Blog> GetBlogsByCategory(int page, string category)
        {
            if (page == 0)
            {
                throw new Exception("page can't be 0, please pass 1 or larger.");
            }
            using (var connection = new SqlConnection(blogConnStr))
            {
                return connection.Query<Blog>($@"select * from GetPageByCategory(@page,@blogPerPage,@category)"
                    , new { page = page, blogPerPage = blogPerPage, category = category }).ToList();

                //return connection.Query<Blog>($"select top {page*blogPerPage} * from blog where visible = 1 order by id desc").ToList();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page">1 for the first page</param>
        /// <returns></returns>
        public static List<Blog> GetBlogsByCollection(int page, int collectionId)
        {
            if (page == 0)
            {
                throw new Exception("page can't be 0, please pass 1 or larger.");
            }
            using (var connection = new SqlConnection(blogConnStr))
            {
                return connection.Query<Blog>($@"select * from GetPageByCollection(@page,@blogPerPage,@collectionId)"
                    , new { page = page, blogPerPage = blogPerPage, collectionId = collectionId }).ToList();

                //return connection.Query<Blog>($"select top {page*blogPerPage} * from blog where visible = 1 order by id desc").ToList();
            }
        }

        public static int GetTotalPages()
        {
            using (var connection = new SqlConnection(blogConnStr))
            {
                //id is int so 5 = 0, 15 = 1, need to add 1
                return connection.QuerySingle<int>($"select dbo.GetTotalPages()");
            }
        }
        public static int GetTotalPagesByTag(string tag)
        {
            using (var connection = new SqlConnection(blogConnStr))
            {
                //id is int so 5 = 0, 15 = 1, need to add 1
                return connection.QuerySingle<int>($@"SELECT dbo.GetTotalPagesByTag(@tag, @blogPerPage)", new { tag = tag, blogPerPage = blogPerPage });
            }
        }
        public static int GetTotalPagesByCategory(string tag)
        {
            using (var connection = new SqlConnection(blogConnStr))
            {
                //id is int so 5 = 0, 15 = 1, need to add 1
                return connection.QuerySingle<int>($@"SELECT dbo.GetTotalPagesByCategory(@tag, @blogPerPage)", new { tag = tag, blogPerPage = blogPerPage });
            }
        }
        public static int GetTotalPagesByCollection(int collectionId)
        {
            using (var connection = new SqlConnection(blogConnStr))
            {
                //id is int so 5 = 0, 15 = 1, need to add 1
                return connection.QuerySingle<int>($@"SELECT dbo.GetTotalPagesByCollection(@collectionId, @blogPerPage)", new { collectionId = collectionId, blogPerPage = blogPerPage });
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
                return connection.Query<Blog>($"select * from GetVisibleBlogs() where id=@id", new { id = id }).ToList();
            }
            //return Sql.Query<Blog>(blogConnStr, $"select * from dbo.blog where id={id}");
        }
        public static BlogIdPair GetBlogNextBlog(int id)
        {
            try
            {
                using (var connection = new SqlConnection(blogConnStr))
                {
                    return connection.QuerySingle<BlogIdPair>($"select * from GetBlogNextBlog(@id)", new { id = id });
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public static BlogIdPair GetBlogPrevBlog(int id)
        {
            try
            {
                using (var connection = new SqlConnection(blogConnStr))
                {
                    return connection.QuerySingle<BlogIdPair>($"select * from GetBlogPrevBlog(@id)", new { id = id });
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }
        #endregion
        #region Post
        public static void CreateBlog(Blog blog)
        {
            using (var connection = new SqlConnection(blogConnStr))
            {

                connection.Execute(@"execute dbo.sp_insert_blog @title,@markdown_content,@category,@tags,@author_id
,@thumbnail,@deleted,@password,@no_comment,@no_robots",
                    new
                    {
                        title = blog.Title,
                        markdown_content = blog.Markdown_Content,
                        category = blog.Category,
                        tags = blog.Tags,
                        author_id = blog.Author_Id,
                        thumbnail = blog.Thumbnail,
                        deleted = blog.Deleted,
                        password = blog.Password,
                        no_comment = blog.NoComment,
                        no_robots = blog.NoRobots
                    });
            }
        }
        public static void UpdateBlog(Blog blog)
        {
            using (var connection = new SqlConnection(blogConnStr))
            {

                connection.Execute(@"execute dbo.sp_update_blog @blog_id,@title,@markdown_content,@category,@tags
,@thumbnail,@deleted,@password,@no_comment,@no_robots",
                    new
                    {
                        blog_id = blog.Id,
                        title = blog.Title,
                        markdown_content = blog.Markdown_Content,
                        category = blog.Category,
                        tags = blog.Tags,
                        thumbnail = blog.Thumbnail,
                        deleted = blog.Deleted,
                        password = blog.Password,
                        no_comment = blog.NoComment,
                        no_robots = blog.NoRobots
                    });
            }
        }
        public static void AddViewCount(int id)
        {
            try
            {
                using (var connection = new SqlConnection(blogConnStr))
                {
                    connection.Execute("execute sp_update_blog_view_count @id", new { id = id });
                }
            }
            catch (Exception)
            {

            }

        }
        #endregion
    }
}
