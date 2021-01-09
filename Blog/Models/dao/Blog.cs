using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class Blog
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        public string Markdown_Content { get; set; }
        [Required]
        [StringLength(50)]
        public string Category { get; set; }
        [StringLength(50)]
        public string Tags { get; set; }
        public int Author_Id { get; set; }
        [StringLength(200)]
        public string Thumbnail { get; set; }
        [Required]
        public DateTime Create_Ts { get; set; }
        public DateTime? Modify_Ts { get; set; }
        public byte[] Password { get; set; }
        [Required]
        public bool Deleted { get; set; }
        [Required]
        public bool NoComment { get; set; }
        [Required]
        public bool NoRobots { get; set; }
        public int view_count { get; set; }
        public int pin { get; set; }
    }
}
