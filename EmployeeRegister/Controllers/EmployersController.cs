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

        // GET: Employers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employer employer = EmployersManager.Instance.Get(id.Value);
            if (employer == null)
            {
                return HttpNotFound();
            }
            return View(employer);
        }

        // GET: Employers/Create
        public ActionResult Create()
        {
            ViewBag.EmployerId = EmployersManager.Instance.GetSelectList();
            return PartialView();
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
                EmployersManager.Instance.Add(employer);
                return RedirectToAction("Index", "Employments");
            }

            ViewBag.EmployerId = EmployersManager.Instance.GetSelectList();
            return RedirectToAction("Index", "Employments");
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
            ViewBag.EmployerId = EmployersManager.Instance.GetSelectList();
            return PartialView(employer);
        }

        // POST: Employers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Deleted,EmployerId")] Employer employer)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                if(EmployersManager.Instance.Modify(employer))
                    return RedirectToAction("Index","Employments");
                else
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

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
    }
}
