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
            List<Models.Blog> blogs = BlogFactory.GetBlogsByPage(1);
            ViewData["ListMode"] = true;
            return View(blogs);
        }
        [Route("/blog/{id:int}")]
        public IActionResult Individual(int id)
        {
            ViewData["ListMode"] = false;
            ViewData["BlogId"] = id;
            List<Models.Blog> blogs = BlogFactory.GetBlog(id);
            return View("index", blogs);
        }

    }
}