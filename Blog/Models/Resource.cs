using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public enum FileType
    {
        picture, zip, txt, video, music
    }
    public class Resource
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public FileType FileType { get; set; }
        public string Directory { get; set; }
        public bool Visible { get; set; }
    }
}
