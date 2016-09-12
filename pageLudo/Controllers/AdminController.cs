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

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Details/5
        public ActionResult Details(string emailID)
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(string emailID)
        {
            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(string emailID, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

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
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
