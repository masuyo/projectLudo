﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pageLudo.Controllers
{
    public class GamesController : Controller
    {
        public ActionResult Ludo()
        {
            return View();
        }

        //public ActionResult Chess()
        //{
        //    return View();
        //}
    }
}