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
            int id2 = db.Accounts.Single(x => x.userName == User.Identity.Name).userId;
            if (id == null || id!=id2)
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
        /*  [Authorize]
          public ActionResult Index()
          {
              return View(db.Accounts.SqlQuery("SELECT * FROM   Account  where Account.userName = @userName",
              new SqlParameter("@userName", User.Identity.Name)).ToList());
              // return View(db.Accounts.ToList());
          }*/

        [Authorize]
        public ActionResult Welcome()
        {

            return View(db.Accounts.SqlQuery("SELECT * FROM   Account  where Account.userName = @userName",
            new SqlParameter("@userName", User.Identity.Name)).SingleOrDefault());
        }

        [Authorize]

        public ActionResult Edit(int? id)
        {
            int id2 = db.Accounts.Single(x => x.userName == User.Identity.Name).userId;
            if (id == null|| id!=id2)
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
        [Authorize]
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

        [Authorize]
        public ActionResult Result(int? id)
        {   
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int userId1 = db.Accounts.Single(x => x.userName == User.Identity.Name).userId;
            int userId2 = db.Products.Single(x =>  x.productId==id).userId;
            if ( userId1 !=userId2)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            var L = new List<Product>();
            if (String.Equals(product.itemType, Common.Common.LOST))
            {
                L = db.Products.SqlQuery("SELECT * FROM Product where itemType = @itemType",
                new SqlParameter("@itemType", Common.Common.FOUND)).ToList();
            }
            else
            {
                L = db.Products.SqlQuery("SELECT * FROM Product where itemType = @itemType",
               new SqlParameter("@itemType", Common.Common.LOST)).ToList();
            }
            foreach (var v in L)
            {
                if (v.postalCode == product.postalCode && String.Equals(v.productName, product.productName) && String.Equals(v.color, product.color) && String.Equals(v.manufacturer, product.manufacturer) && String.Equals(v.model, product.model))
                {

                    Account account = db.Accounts.Find(v.userId);
                    return View(account);

                }

            }
            return RedirectToAction("Sorry", "Account");
        }
        [Authorize]
        public ActionResult Sorry()
        {
            return View();
        }
    }
}