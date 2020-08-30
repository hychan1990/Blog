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
        public static Dictionary<string, int> GetSortedCount(string tableColumn)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            using (var connection = new SqlConnection(blogConnStr))
            {
                var unsplited = connection.Query<string>($"select {tableColumn} from view_visible_blog").ToList();
                foreach (var colValues in unsplited)
                {
                    string[] colValue = colValues.Split(',');
                    foreach (var col in colValue)
                    {
                        if (col.Trim() != "")
                        {
                            if (dict.ContainsKey(col))
                            {
                                dict[col]++;
                            }
                            else
                            {
                                dict[col] = 1;
                            }
                        }
                    }
                }
            }
            var filtered = from entry in dict orderby entry.Value descending select entry;
            return filtered.ToDictionary(x => x.Key, x => x.Value);
        }
    }
}