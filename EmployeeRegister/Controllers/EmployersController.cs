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
            ViewBag.EmployerId = EmployersManager.Instance.GetSelectList(null);
            return PartialView();
        }

        // POST: Employers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Deleted,EmployerId")] Employer employer)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, detail = $"Adding {employer.Name} failed." },
                    JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (EmployersManager.Instance.Add(employer))
                    return Json(new { success = true, parent = employer.EmployerId.HasValue ? $"Employer_{employer.EmployerId}" : null }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { success = false, detail = $"Adding {employer.Name} failed." },
                        JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// GET: Employers/Edit/5
        /// </summary>
        /// <param name="id">Employer</param>
        /// <returns></returns>
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
            ViewBag.EmployerId = EmployersManager.Instance.GetSelectList(employer.EmployerId);
            return PartialView(employer);
        }

        /// <summary>
        /// POST: Employers/Edit/5
        /// To protect from overposting attacks, please enable the specific properties you want to bind to, for
        /// more details see https://go.microsoft.com/fwlink/?LinkId=317598. 
        /// </summary>
        /// <param name="employer">Employer entity</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Deleted,EmployerId")] Employer employer)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, detail = $"Editing {employer.Name} failed." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var oldEmployer = EmployersManager.Instance.Get(employer.Id);
                if (EmployersManager.Instance.Modify(employer))
                    return Json(new { success = true, newParent = employer.EmployerId.HasValue ? $"Employer_{employer.EmployerId}" : null, parent = oldEmployer.EmployerId.HasValue ? $"Employer_{oldEmployer.EmployerId}" : null }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { success = false, detail = $"Editing {employer.Name} failed." }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// POST: Employers/Delete/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var employer = EmployersManager.Instance.Get(id);

            if (EmployersManager.Instance.Delete(id))
            {
                return Json(new { success = true, parent = employer.GeneralContractor != null ? $"Employer_{employer.EmployerId}" : null }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, detail = $"Deleting {employer.Name} failed." }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
