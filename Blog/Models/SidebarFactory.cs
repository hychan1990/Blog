using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using MyToolNetStandard;

namespace Blog.Models
{
    public static class SidebarFactory
    {
        static ExternalJsonConfigReader configReader = new ExternalJsonConfigReader(@"C:\Config\config.json");
        static string blogConnStr = configReader.GetValue("BlogConnStr");
        public static Author GetAuthor()
        {
            using (var connection = new SqlConnection(blogConnStr))
            {
                return connection.QuerySingle<Author>("select top 1 * from author where id = 1 order by id desc");
            }
        }
        /*
        public static List<TagsCount> GetTags()
        {
            using (var connection = new SqlConnection(blogConnStr))
            {
                return connection.Query<TagsCount>("select * from GetTagsCount() order by count desc").ToList();
            }
        }
        */

        
        public static Dictionary<string, int> GetSortedCount(string tableColumn)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            using (var connection = new SqlConnection(blogConnStr))
            {
                var unsplited = connection.Query<string>($"select {tableColumn} from view_visible_blog").ToList();
                foreach (var colValues in unsplited)
                {
                    if (colValues != null)
                    {
                        string[] colValue = colValues.Split(',');
                        foreach (var col in colValue)
                        {
                            string colTrim = col.Trim();
                            if (colTrim != "")
                            {
                                if (dict.ContainsKey(colTrim))
                                {
                                    dict[colTrim]++;
                                }
                                else
                                {
                                    dict[colTrim] = 1;
                                }
                            }
                        }
                    }
                }
            }
            var filtered = from entry in dict orderby entry.Value descending select entry;
            return filtered.ToDictionary(x => x.Key, x => x.Value);
        }
        public static List<Collection> GetCollections()
        {
            try
            {
                using (var connection = new SqlConnection(blogConnStr))
                {
                    var result = connection.Query<Collection>("select * from collection").ToList();
                    return result;
                }
            }
            catch (Exception)
            {
                return new List<Collection>();
            }
        }
        public static string GetCollectionName(int id)
        {
            try
            {
                using (var connection = new SqlConnection(blogConnStr))
                {
                    var result = connection.QuerySingle<string>("select name from collection where id = @id", new { id = id });
                    return result;
                }
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}