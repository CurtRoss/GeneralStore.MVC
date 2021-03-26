using GeneralStore.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeneralStore.MVC.Controllers
{
    public class CustomerController : Controller
    {
        // Application DB Context

        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Customer
        public ActionResult Index()
        {
            List<Customer> customerList = _db.Customers.ToList();
            List<Customer> orderedList = customerList.OrderBy(p => p.LastName).ToList();
            return View(orderedList);
        }

        // Get : Create Customer
        [HttpGet]
        public ActionResult Create()
        {
            
            return View();
        }

        // Post : Create Customer
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _db.Customers.Add(customer);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // Get : Delete Customer
        //Customer/Delete/{id}
        [HttpGet]
        public ActionResult Delete (int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            Customer customer = _db.Customers.Find(id);

            if (customer == null)
                HttpNotFound();

            return View(customer);
        }

        // Post : Delete Customer
        //Customer/Delete/{id}

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult Delete(int id)
        {
            Customer customer = _db.Customers.Find(id);
            _db.Customers.Remove(customer);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        // Get : Edit Customer
        // Customer/Edit/{id}
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Customer customer = _db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // Post : Edit Customer
        // Customer/Edit/{id}

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(customer).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(customer);
        }

        // Get : Customer Details
        // Customer/Details/{id}

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            Customer customer = _db.Customers.Find(id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }
    }
}