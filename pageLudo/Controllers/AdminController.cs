using pageLudo.Models;
using SignalRServer.MVCData.DataClasses;
using SignalRServer.MVCData.MethodClasses;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult Edit(string emailID)
        {
            Session["EditUserEmailID"] = emailID;
            return View("EditView",emailID);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(UserHandlingData u)
        {
            try
            {
                aa = new AdminActions();
                aa.UserSetting(Session["EditUserEmailID"].ToString(), u.Username, u.Password, u.EmailID, u.Role);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(string emailID)
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(string emailID, FormCollection collection)
        {
            try
            {
                aa = new AdminActions();
                aa.DeleteUser(emailID);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
