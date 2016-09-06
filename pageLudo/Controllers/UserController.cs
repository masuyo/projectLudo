using Entities;
using pageLudo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using pageLudo.FakeData.MethodClasses;

namespace pageLudo.Controllers
{
    public class UserController : Controller
    {

        [HttpGet]
        // db-ben megnézi, h adott searchString username vagy email, de előtte csekkolja, hogy melyik van beadva
        public ActionResult Search(string searchString)
        {
            string searchEmailRegEx = @"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$";
            string loginEmailID = (string)Session["LogedEmailID"];
            //bool emptyList = FakeData.MethodClasses.UserActions.UsernameSearch(searchString, "email@email.com").Any();

            UserActions ua = new UserActions();
            List<FakeData.DataClasses.UserData> getUsers = new List<FakeData.DataClasses.UserData>();
            getUsers = ua.UsernameSearch(searchString);

            if (Regex.IsMatch(searchString, searchEmailRegEx))
            {
                if (getUsers.Any())
                {
                    // visszakapott adatok

                    // keresett user profilja
                    return RedirectToAction();
                }
                else
                {
                    // nem található ilyen felhasználó a rendszerben
                    return RedirectToAction();
                }
            }
            else
            {
                if(UsernameSearch(searchString, Session["LogedEmailID"] != null))
                {
                    // visszakapott adatok

                    // keresett user profilja
                    return RedirectToAction();
                }
                else
                {
                    // nem található ilyen felhasználó a rendszerben
                    return RedirectToAction();
                }
            }
        }
 

        public ActionResult MyProfile()
        {
            return View();
        }

        public ActionResult Logout()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            return RedirectToAction("Index","Home");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginUser u) // Username helyett emaillel lép be
        {
            if (ModelState.IsValid)
            {
                using (DatabaseEntities DE = new DatabaseEntities())
                {

                    //var repo = new Repository.TableRepositories.UsersRepository(DE);
                    // kikeresi az adatbázisból a beadott adatok alapján a usert, ha megtalálja, Sessiont kap
                    // TODO: repositoryba kell egy lekérdezés Email és Password alapján

                    var obj = new LoginUser(){ UserID = 1, Username = "Cressida", Password = "123456", EmailID = "cressida@citromail.hu", Role = "admin"};
                    //var obj = ude.Users.Where(a => a.Username.Equals(u.Username) && a.Password.Equals(u.Password)).FirstOrDefault();
                    //if (obj != null)
                    //{
                    Session["LogedUserID"] = obj.UserID.ToString();
                    Session["LogedUsername"] = obj.Username.ToString();
                    Session["LogedEmailID"] = obj.EmailID.ToString();

                    //adminhoz kell, ha a sessionrole admin, akkor mást fog megjeleníthetővé tenni a layout
                    Session["LogedUserRole"] = obj.Role.ToString();
                    return RedirectToAction("AfterLogin");
                    //}
                }
            }
            return View(u);
        }

        public ActionResult AfterLogin()
        {
            if (Session["LogedUserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterUser u)
        {
            if (ModelState.IsValid)
            {
                using (DatabaseEntities DE = new DatabaseEntities())
                {
                    var repo = new Repository.TableRepositories.UsersRepository(DE);
                    if (repo.Register(u.Username, u.Password, u.EmailID))
                    {
                        ViewBag.Message = "Registration successful";
                    }
                    ModelState.Clear();
                    u = null;
                }
            }
            return View(u);
        }
    }
}