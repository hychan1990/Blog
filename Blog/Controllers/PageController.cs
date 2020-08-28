using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Models;

namespace Blog.Controllers
{
    public class PageController : Controller
    {
        public IActionResult Index()
        {
            List<Models.Blog> blogs = BlogFactory.GetBlogs(1);
            ViewData["Preview"] = true;
            return View("~/Views/Blog/index.cshtml", blogs);
        }
    }
}