﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeacherTitle.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //if (Request.Cookies["userCookie"] != null)
            //{

            //}
            ViewBag.Message = "欢迎使用 ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
