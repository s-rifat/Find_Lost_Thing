using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FindLostThings.Models;
using System.Web.Security;
using System.Data.SqlClient;

namespace FindLostThings.Controllers
{
    public class AccountController : Controller
    {
        private ProductContext db = new ProductContext();

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Login(Account account)
        {

            bool isValid = db.Accounts.Any(x => x.userName == account.userName && x.password == account.password);
            if (isValid)
            {

                FormsAuthentication.SetAuthCookie(account.userName, false);

                return RedirectToAction("Welcome", "Account");
            }

            ModelState.AddModelError("", "Invalid username and password");
            return View();
        }

        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Signup([Bind(Include = "userId,userName,password,confirmPassword,phoneNumber")] Account account)
        {
            if (ModelState.IsValid)
            {
               
                bool isExist = db.Accounts.Any(x => x.userName == account.userName);
                if (!isExist)
                {
                    db.Accounts.Add(account);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "userName exists");
                }
            }

            return View(account);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Accounts.SqlQuery("SELECT * FROM   Account  where Account.userName = @userName",
            new SqlParameter("@userName", User.Identity.Name)).ToList());
            // return View(db.Accounts.ToList());
        }

        [Authorize]
        public ActionResult Welcome()
        {   
            

            return View();
        }
      



        /*  / 
       // GET: Account
       public ActionResult Index()
       {
           return View(db.Accounts.ToList());
       }
  /*
        // GET: Account/Details/5
       public ActionResult Details(int? id)
       {
           if (id == null)
           {
               return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
           }
           Account account = db.Accounts.Find(id);
           if (account == null)
           {
               return HttpNotFound();
           }
           return View(account);
       }
          // GET: Account/Create
          public ActionResult Create()
          {
              return View();
          }

          // POST: Account/Create
          // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
          // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult Create([Bind(Include = "userId,userName,password,phoneNumber")] Account account)
          {
              if (ModelState.IsValid)
              {
                  db.Accounts.Add(account);
                  db.SaveChanges();
                  return RedirectToAction("Index");
              }

              return View(account);
          }

          // GET: Account/Edit/5
          public ActionResult Edit(int? id)
          {
              if (id == null)
              {
                  return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
              }
              Account account = db.Accounts.Find(id);
              if (account == null)
              {
                  return HttpNotFound();
              }
              return View(account);
          }

          // POST: Account/Edit/5
          // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
          // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult Edit([Bind(Include = "userId,userName,password,phoneNumber")] Account account)
          {
              if (ModelState.IsValid)
              {
                  db.Entry(account).State = EntityState.Modified;
                  db.SaveChanges();
                  return RedirectToAction("Index");
              }
              return View(account);
          }

          // GET: Account/Delete/5
          public ActionResult Delete(int? id)
          {
              if (id == null)
              {
                  return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
              }
              Account account = db.Accounts.Find(id);
              if (account == null)
              {
                  return HttpNotFound();
              }
              return View(account);
          }

          // POST: Account/Delete/5
          [HttpPost, ActionName("Delete")]
          [ValidateAntiForgeryToken]
          public ActionResult DeleteConfirmed(int id)
          {
              Account account = db.Accounts.Find(id);
              db.Accounts.Remove(account);
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
          }*/


    }
}
