using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Models;
using Microsoft.AspNetCore.Http;
using System.Net;
using MyToolNetStandard;

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
            //ViewData["ListMode"] = true;
            ViewData["Title"] = "";
            return View(blogs);
        }
        [Route("/blog/{id:int}")]
        public IActionResult Individual(int id)
        {
            //ViewData["ListMode"] = false;
            ViewData["BlogId"] = id;
            var admin = AuthService.AuthenticateAdmin(Request.Cookies["a1"]);
            if (admin)
            {
                ViewData["AdminMode"] = true;
            }

            List<Models.Blog> blogs = BlogFactory.GetBlog(id);
            if (blogs.Count > 0)
            {
                ViewData["Title"] = blogs[0].Title;
                if (!admin)
                {
                    BlogFactory.AddViewCount(id);
                }
                return View("Detail", blogs[0]);
            }
            else
            {
                return Redirect("~/");
            }

        }
        public IActionResult Create()
        {
            /*
            TempData["ip"] = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            if (TempData["ip"].ToString() != "::1" || Request.Cookies["login"] != "1")
            {
                return Redirect("~/");
            }*/

            if (AuthService.AuthenticateAdmin(Request.Cookies["a1"]))
            {
                return View();
            }
            return Redirect("~/");
        }
        [HttpPost]
        public IActionResult Create(Blog.Models.Blog blog)
        {
            /*
            TempData["ip"] = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            if (TempData["ip"].ToString() != "::1" || Request.Cookies["login"] != "1")
            {
                return Redirect("~/");
            }
            */
            if (!ModelState.IsValid)
            {
                return View();
            }
            blog.Author_Id = 1;
            blog.Create_Ts = DateTime.Now;
            blog.Deleted = false;
            try
            {
                if (AuthService.AuthenticateAdmin(Request.Cookies["a1"]))
                {
                    BlogFactory.CreateBlog(blog);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "出現錯誤。";
                return View();
            }

            return Redirect("~/");
        }
        [Route("/blog/edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var blogList = BlogFactory.GetBlog(id);
            if (blogList.Count > 0)
            {
                Blog.Models.Blog blog = blogList[0];
                ViewData["EditMode"] = true;
                return View("Create", blog);
            }
            return Redirect("~/");
        }
        [Route("/blog/edit/{id:int}")]
        [HttpPost]
        public IActionResult Edit(Blog.Models.Blog blog)
        {
            /*
            TempData["ip"] = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            if (TempData["ip"].ToString() != "::1" || Request.Cookies["login"] != "1")
            {
                return Redirect("~/");
            }
            */
            if (!ModelState.IsValid)
            {
                return View();
            }
            blog.Author_Id = 1;
            blog.Modify_Ts = DateTime.Now;
            blog.Deleted = false;
            try
            {
                if (AuthService.AuthenticateAdmin(Request.Cookies["a1"]))
                {
                    BlogFactory.UpdateBlog(blog);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "出現錯誤。";
                return View();
            }

            return Redirect("~/");
        }
    }
}