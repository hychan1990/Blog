using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Models;
using Microsoft.AspNetCore.Http;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        private IHttpContextAccessor _accessor;
        public BlogController(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
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
        public IActionResult Create()
        {
            TempData["ip"] = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            if (TempData["ip"].ToString() != "::1")
            {
                return Redirect("~/");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Create(Blog.Models.Blog blog)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "please check format.";
                return View();
            }
            blog.Author_Id = 1;
            blog.Create_Ts = DateTime.Now;
            blog.Deleted = false;
            try
            {
                BlogFactory.CreateBlog(blog);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.ToString();
                return View();
            }
            
            return Redirect("~/");
        }
    }
}