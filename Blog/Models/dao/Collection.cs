using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class Collection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Create_ts { get; set; }
        public DateTime? Modify_ts { get; set; }
    }
}
