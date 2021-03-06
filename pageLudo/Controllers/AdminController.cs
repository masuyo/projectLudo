﻿using pageLudo.Models;
using SignalRServer.MVCData.DataClasses;
using SignalRServer.MVCData.MethodClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace pageLudo.Controllers
{
    public class AdminController : Controller
    {
        UserHandlingModel uhdm;
        AdminActions aa;
        List<UserData> udList;

        // kilistázza az összes usert
        public ActionResult AllUsersPage()
        {
            aa = new AdminActions();
            udList = aa.GetAllUsers();
            uhdm = new UserHandlingModel();
            List<UserHandlingData> luhd = new List<UserHandlingData>();
            foreach (var u in udList)
            {
                luhd.Add(new UserHandlingData { Username = u.Username,EmailID = u.EmailID,Role = u.Role });
            }
            uhdm.List = luhd;
            return View("AdminView", uhdm);
        }

        // admin főoldal
        public ActionResult Index()
        {
            return View();
        }

        // user edit view
        public ActionResult EditUser(string emailID)
        {
            HttpContext.Session["EditUserEmailID"] = emailID.ToString();
            return View("EditView");
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(UserHandlingData u)
        {
            try
            {
                aa = new AdminActions();
                string editUserEmailID = HttpContext.Session["EditUserEmailID"].ToString();

                string hashedPassword = null;

                if (u.Password != null)
                {
                    var sha1 = new SHA1CryptoServiceProvider();
                    byte[] sha1data = sha1.ComputeHash(Encoding.ASCII.GetBytes(u.Password));
                    hashedPassword = new ASCIIEncoding().GetString(sha1data);
                }

                if (aa.UserSetting(editUserEmailID, u.Username, hashedPassword, u.EmailID, u.Role))
                {
                    ViewBag.Message = "Edit was successful";
                }
                else
                {
                    ViewBag.Message = "Unsuccessful edit";
                }

                return RedirectToAction("AllUsersPage");
            }
            catch
            {
                return RedirectToAction("AllUsersPage");
            }
        }

        public ActionResult DeleteUser(string emailID)
        {
            try
            {
                aa = new AdminActions();
                aa.DeleteUser(emailID);
                return RedirectToAction("AllUsersPage");
            }
            catch
            {
                return View();
            }
        }
    }
}
