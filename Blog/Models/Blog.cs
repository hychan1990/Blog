using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Markdown_Content { get; set; }
        public string Category { get; set; }
        public string Tags { get; set; }
        public string Author_Id { get; set; }
        public string Thumbnail { get; set; }
        public string Creator { get; set; }
        public DateTime Create_Ts { get; set; }
        public string Modifier { get; set; }
        public DateTime Modify_Ts { get; set; }
        public bool Visible { get; set; }
        public byte[] Password { get; set; }
        public bool Deleted { get; set; }
        public bool NoComment { get; set; }
        public bool NoRobots { get; set; }

    }
}
