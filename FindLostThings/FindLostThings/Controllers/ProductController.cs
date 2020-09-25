using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FindLostThings.Models;

namespace FindLostThings.Controllers
{   
    [Authorize]
    public class ProductController : Controller
    {
        private ProductContext db = new ProductContext();
   
      
        /* public int CommonChars(string left, string right)
       {
           return left.GroupBy(c => c)
               .Join(
                   right.GroupBy(c => c),
                   g => g.Key,
                   g => g.Key,
                   (lg, rg) => lg.Zip(rg, (l, r) => l).Count())
               .Sum();
       }*/

        public int CommonChar(string s1, string s2)
        {
            if (s1 == "N/A" || s2 == "N/A")
                return 0;

            int n1 = s1.Length;
            int n2 = s2.Length;


            if (n1 < n2)
            {
                int cnt1 = 0;
                for (int i = 0; i <= n2 - n1; i++)
                {
                    int cnt2 = 0;
                    for (int j = 0, k = i; j < n2; j++, k++)
                    {
                        if (Char.ToLower(s1[j]) == Char.ToLower(s2[k]))
                            cnt2++;
                    }
                    cnt1 = Math.Max(cnt1, cnt2);
                }
                return cnt1;
            }
            else
            {
                int cnt1 = 0;
                for (int i = 0; i <= n1 - n2; i++)
                {

                    int cnt2 = 0;
                    for (int j = 0, k = i; j < n2; j++, k++)
                    {
                        if (Char.ToLower(s2[j]) == Char.ToLower(s1[k]))
                        {
                            cnt2++;
                        }
                    }
                    cnt1 = Math.Max(cnt1, cnt2);
                }
                return cnt1;
            }

        }


        // GET: Product

        public ActionResult Index()
        {
           
            return View(db.Products.SqlQuery("SELECT * FROM Product INNER JOIN Account ON Product.userId = Account.userId where Account.userName = @userName",
            new SqlParameter("@userName", User.Identity.Name)).ToList());       
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {   
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int userId1 = db.Accounts.Single(x => x.userName == User.Identity.Name).userId;
            int userId2 = db.Products.Single(x => x.productId == id).userId;
            if ( userId1!=userId2)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create(int id)

        {  
            if (id == 1)
                Common.Common.bol = true;
            else if(id==2)
                Common.Common.bol = false;
            else
                 return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "productId,productName,manufacturer,model,color,postalCode,date,description")] Product product)
        {

            Account user = db.Accounts
                 .SqlQuery("Select * from Account where userName=@userName", new SqlParameter("@userName", User.Identity.Name))
                 .FirstOrDefault();


            product.userId = user.userId;
            if (string.IsNullOrEmpty(product.model))
                product.model = "N/A";

            if (string.IsNullOrEmpty(product.manufacturer))
                product.manufacturer = "N/A";

            if (string.IsNullOrEmpty(product.description))
                product.description= "N/A";


            if (Common.Common.bol)
            {
                product.itemType = Common.Common.LOST;
               
            }
            else
            {
                product.itemType = Common.Common.FOUND;
            }

           

            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();

               
                return RedirectToAction("Index");
            }


            return View(product);
        }

     
        /*   // GET: Product/Edit/5
           public ActionResult Edit(int? id)
           {
               if (id == null)
               {
                   return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
               }
               Product product = db.Products.Find(id);
               if (product == null)
               {
                   return HttpNotFound();
               }
               return View(product);
           }

           // POST: Product/Edit/5
           // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
           // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
           [HttpPost]
           [ValidateAntiForgeryToken]
           public ActionResult Edit([Bind(Include = "productId,productName,manufacturer,model,color,postalCode,date,description,userType,userId")] Product product)
           {

               if (ModelState.IsValid)
               {
                   db.Entry(product).State = EntityState.Modified;
                   db.SaveChanges();
                   return RedirectToAction("Index");
               }
               return View(product);
           }*/

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int userId1 = db.Accounts.Single(x => x.userName == User.Identity.Name).userId;
            int userId2 = db.Products.Single(x => x.productId == id).userId;
            if ( userId1 != userId2)//
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
    }
}
