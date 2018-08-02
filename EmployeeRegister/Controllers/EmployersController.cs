using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Models;

namespace EmployeeRegister.Controllers
{
    public class EmployersController : Controller
    {
        private RegisterEntities db = new RegisterEntities();

        // GET: Employers
        public ActionResult Index()
        {
            var employers = db.Employers.Include(e => e.GeneralContractor).Where(e=>e.Deleted!=true);
            return View(employers.ToList());
        }

        // GET: Employers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employer employer = db.Employers.Find(id);
            if (employer == null)
            {
                return HttpNotFound();
            }
            return View(employer);
        }

        // GET: Employers/Create
        public ActionResult Create()
        {
            ViewBag.EmployerId = new SelectList(db.Employers, "Id", "Name");
            return View();
        }

        // POST: Employers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Deleted,EmployerId")] Employer employer)
        {
            if (ModelState.IsValid)
            {
                db.Employers.Add(employer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployerId = new SelectList(db.Employers, "Id", "Name", employer.EmployerId);
            return View(employer);
        }

        // GET: Employers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employer = EmployersManager.Instance.Get(id.Value);
            if (employer == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployerId = new SelectList(db.Employers, "Id", "Name", employer.EmployerId);
            return PartialView(employer);
        }

        // POST: Employers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Deleted,EmployerId")] Employer employer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployerId = new SelectList(db.Employers, "Id", "Name", employer.EmployerId);
            return View(employer);
        }

        //// GET: Employers/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Employer employer = db.Employers.Find(id);
        //    if (employer == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(employer);
        //}

        // POST: Employers/Delete/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            return EmployersManager.Instance.Delete(id) ? new HttpStatusCodeResult(HttpStatusCode.OK) : new HttpStatusCodeResult(HttpStatusCode.NotFound);
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
