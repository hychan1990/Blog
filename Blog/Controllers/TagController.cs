using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class TagController : Controller
    {
        
        [Route("/tag/{t}")]
        public IActionResult Index(string t)
        {
            List<Models.Blog> blogs = BlogFactory.GetBlogsByTag(1, t);
            //ViewData["ListMode"] = true;
            ViewData["SearchMode"] = "tag";
            ViewData["CurrentPage"] = 1;
            ViewData["CurrentSearchWord"] = t;
            ViewData["CurrentRoute"] = $"/tag/{t}"; 
            return View("~/Views/Blog/index.cshtml", blogs);
        }
        [Route("/tag/{t}/page/{p:int}")]
        public IActionResult ByPage(string t, int p)
        {
            List<Models.Blog> blogs = BlogFactory.GetBlogsByTag(p, t);
            //ViewData["ListMode"] = true;
            ViewData["SearchMode"] = "tag";
            ViewData["CurrentPage"] = p;
            ViewData["CurrentSearchWord"] = t;
            ViewData["CurrentRoute"] = $"/tag/{t}/page/{p}";
            return View("~/Views/Blog/index.cshtml", blogs);
        }
    }
}