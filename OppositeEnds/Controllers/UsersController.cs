﻿using OppositeEnds.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Helpers;
using System.Web.Mvc;

namespace OppositeEnds.Controllers
{
    public class UsersController : Controller
    {
        private OppositeEndsContext db = new OppositeEndsContext();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {

                var hashedPassword = Crypto.HashPassword(user.Password);
                user.Password = hashedPassword;
                user.ConfirmPassword = hashedPassword;
                db.Users.Add(user);


                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        //Get log in
        public ActionResult SignIn()
        {
            return View();
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult SignIn([Bind(Include = "Email,Password")] AccountLoginViewModel userTryingToLogin)
        //{
        //    if (userTryingToLogin != null & ModelState.IsValid)
        //    {

        //        FundRaisers doesUserExist = db.FundRaisers.FirstOrDefault(s => s.Email.Equals(userTryingToLogin.Email));
        //        try
        //        {
        //            if (!doesUserExist.verified)
        //            {
        //                ModelState.AddModelError("doesUserExist", "Email Not verified. Please check your email confirmation");
        //                return View("SignInFailure");
        //            }

        //            bool a = Crypto.VerifyHashedPassword(doesUserExist.Password, userTryingToLogin.Password);
        //            if (a == true)
        //            {

        //                // creating authetication ticket
        //                FormsAuthentication.SetAuthCookie(userTryingToLogin.Email, false);
        //                Session["userId"] = doesUserExist.FundRaisersId;
        //                return RedirectToAction("Index", "Home");
        //            }
        //            else
        //            {
        //                @ViewBag.Message = "Error.Ivalid login.";
        //                return View("SignInFailure");
        //            }

        //        }
        //        catch
        //        {
        //            @ViewBag.Message = "Error.Ivalid login.";
        //            return View("SignInFailure");

        //        }
        //    }
        //    ModelState.AddModelError("UserDoesNotExist", "Username or Password is Incorrect! Please try Again!!");
        //    return View(userTryingToLogin);

        //}



        //public ActionResult SignInFailure()
        //{
        //    return View();
        //}





        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn([Bind(Include = "UserName,Password")] User userTryingToLogin)
        {


            {

                if (userTryingToLogin != null)
                {

                    User doesUserExist = db.Users.FirstOrDefault(s => s.UserName.Equals(userTryingToLogin.UserName));
                    try
                    {
                        //bool a = Crypto.VerifyHashedPassword(doesUserExist.Password, userTryingToLogin.P assword);
                        //if (a == true)
                        //{
                        //}
                        if (userTryingToLogin.Password == doesUserExist.Password)
                        {
                            System.Web.Security.FormsAuthentication.SetAuthCookie(doesUserExist.Email, false);
                            //return View("AdminDashBoard");
                            return RedirectToAction("AdminDashBoard", "Users");
                        }

                        else
                        {
                            ModelState.AddModelError("Error", "Invalid Username/Password combination. Please try again");
                            return View(userTryingToLogin);
                        }
                    }
                    catch
                    {
                        ModelState.AddModelError("Error", "Invalid Username/Password combination. Please try again");
                        return View(userTryingToLogin);
                    }


                }
           
            }
            return View();
        }




        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }



        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,UserName,Email,Password,ConfirmPassword,verified")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }


        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Logout()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");

        }
        public ActionResult AdminDashBoard()
        {
            return View();
        }
    }
}
