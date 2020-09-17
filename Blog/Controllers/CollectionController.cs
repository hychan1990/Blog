using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class CollectionController : Controller
    {
        public IActionResult Index()
        {
            return View(SidebarFactory.GetCollections());
        }
        
        [Route("/collection/{collectionId:int}")]
        public IActionResult PreviewCollection(int collectionId)
        {
            ViewData["SearchMode"] = "collection";
            ViewData["CurrentPage"] = 1;
            ViewData["CurrentSearchWord"] = collectionId;
            ViewData["CurrentRoute"] = $"/collection/{collectionId}";
            return View("~/Views/Blog/index.cshtml", BlogFactory.GetBlogsByCollection(1, collectionId));
        }
        [Route("/collection/{collectionId:int}/page/{p:int}")]
        public IActionResult ByPage(int collectionId, int p)
        {
            List<Models.Blog> blogs = BlogFactory.GetBlogsByCollection(p, collectionId);
            ViewData["SearchMode"] = "collection";
            ViewData["CurrentPage"] = p;
            ViewData["CurrentSearchWord"] = collectionId;
            ViewData["CurrentRoute"] = $"/collection/{collectionId}/page/{p}";
            return View("~/Views/Blog/index.cshtml", blogs);
        }
    }
}
