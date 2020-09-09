using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class CategoryController : Controller
    {
        [Route("/category/{t}")]
        public IActionResult Index(string t)
        {
            List<Models.Blog> blogs = BlogFactory.GetBlogsByCategory(1, t);
            //ViewData["ListMode"] = true;
            ViewData["SearchMode"] = "category";
            ViewData["CurrentPage"] = 1;
            ViewData["CurrentSearchWord"] = t;
            ViewData["CurrentRoute"] = $"/category/{t}";
            return View("~/Views/Blog/index.cshtml", blogs);
        }
        [Route("/category/{t}/page/{p:int}")]
        public IActionResult ByPage(string t, int p)
        {
            List<Models.Blog> blogs = BlogFactory.GetBlogsByCategory(p, t);
            //ViewData["ListMode"] = true;
            ViewData["SearchMode"] = "category";
            ViewData["CurrentPage"] = p;
            ViewData["CurrentSearchWord"] = t;
            ViewData["CurrentRoute"] = $"/category/{t}/page/{p}";
            return View("~/Views/Blog/index.cshtml", blogs);
        }
    }
}
