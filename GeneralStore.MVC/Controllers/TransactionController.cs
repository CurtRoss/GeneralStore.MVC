using GeneralStore.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeneralStore.MVC.Controllers
{
    public class TransactionController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();


        // Get Transaction/Create
        public ActionResult Create()
        {
            ViewData["Products"] = _db.Products.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.ProductId.ToString()
            });

            ViewData["Customers"] = _db.Customers.Select(c => new SelectListItem
            {
                Text = c.FirstName + " " + c.LastName,
                Value = c.CustomerId.ToString()
            });
            return View();
        }

        // Post Transaction/Create
        [HttpPost]
        public ActionResult Create(Transaction model)
        {
            model.DateOfTransaction = DateTimeOffset.Now;
            var createdObj = _db.Transactions.Add(model);

            if (_db.SaveChanges() == 1)
            {
                return RedirectToAction("Index");
                //return Redirect("/transaction/"
                //    +createdObj.TransactionID);
            }

            //ViewData["ErrMessage"]
            return View(model);
        }



        public ActionResult Index()
        {
            return View(_db.Transactions.ToArray());
        }

        public ActionResult Details(int transactionId)
        {
            var transaction = _db.Transactions.Find(transactionId);
            return View(transaction);
        }
        public ActionResult Edit(int transactionId)
        {
            var transaction = _db.Transactions.Find(transactionId);
            if (transaction == null)
            {
                return Redirect("Index");
            }

            ViewData["Products"] = _db.Products.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.ProductId.ToString()
            });

            ViewData["Customers"] = _db.Customers.Select(c => new SelectListItem
            {
                Text = c.FirstName + " " + c.LastName,
                Value = c.CustomerId.ToString()
            });
            return View(transaction);
        }
        [HttpPost]
        public ActionResult Edit(Transaction model)
        {
            var entity = _db.Transactions.Find(model.TransactionID);
            entity.CustomerId = model.CustomerId;
            entity.ProductId = model.ProductId;
            if (_db.SaveChanges() == 1)
            {
                return Redirect("Index");
            }

            ViewData["Products"] = _db.Products.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.ProductId.ToString()
            });

            ViewData["Customers"] = _db.Customers.Select(c => new SelectListItem
            {
                Text = c.FirstName + " " + c.LastName,
                Value = c.CustomerId.ToString()
            });

            return View(model);
        }
        public ActionResult Delete(int transactionId)
        {
            var transaction = _db.Transactions.Find(transactionId);
            if (transaction == null)
            {
                return Redirect("Index");
            }
            return View(transaction);
        }

        [HttpPost]
        public ActionResult Delete(Transaction model)
        {
            //_db.Entry(model).Entity
            var entity = _db.Transactions.Find(model.TransactionID);
            _db.Transactions.Remove(entity);
            if (_db.SaveChanges() == 1)
            {
                return Redirect("index");
            }
            //ViewData["ErrMessage"]
            return View(model);
        }
    }
}