using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Models;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        
        /// <summary>
        /// Index is the view name
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            List<Models.Blog> blogs = BlogFactory.GetBlogs(1);
            ViewData["Preview"] = true;
            return View(blogs);
        }
        [Route("/Blog/{id:int}")]
        public IActionResult Individual(int id)
        {
            ViewData["Preview"] = false;
            ViewData["BlogId"] = id;
            List<Models.Blog> blogs = BlogFactory.GetBlog(id);
            return View("index", blogs);
        }

    }
}