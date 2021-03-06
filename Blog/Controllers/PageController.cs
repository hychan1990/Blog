﻿using System;
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
            List<Models.Blog> blogs = BlogFactory.GetBlogsByPage(1);
            //ViewData["ListMode"] = true;
            return View("~/Views/Blog/index.cshtml", blogs);
        }
        [Route("/page/{p:int}")]
        public IActionResult ByPage(int p)
        {
            List<Models.Blog> blogs = BlogFactory.GetBlogsByPage(p);
            //ViewData["ListMode"] = true;
            ViewData["CurrentPage"] = p;
            return View("~/Views/Blog/index.cshtml", blogs);
        }
    }
}