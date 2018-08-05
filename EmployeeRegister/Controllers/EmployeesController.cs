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
using EmployeeRegister.Models;

namespace EmployeeRegister.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = EmployeesManager.Instance.Get(id.Value);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.EmployerId = EmployersManager.Instance.GetSelectList(null);
            return PartialView();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Deleted,Name,Email,EmployerId")] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, detail = $"Adding {employee.Name} failed." },
                    JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (EmployeesManager.Instance.Add(employee))
                    return Json(new { success = true, parent = employee.EmployerId.HasValue ? $"Employer_{employee.EmployerId}" : null }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { success = false, detail = $"Adding {employee.Name} failed." },
                        JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = EmployeesManager.Instance.Get(id.Value);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployerId = EmployersManager.Instance.GetSelectList(employee.EmployerId);
            return PartialView(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Deleted,Name,Email,EmployerId")] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, detail = $"Editing {employee.Name} failed." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var oldEmployee = EmployeesManager.Instance.Get(employee.Id);
                if (EmployeesManager.Instance.Modify(employee))
                    return Json(new { success = true, newParent = employee.EmployerId.HasValue ? $"Employer_{employee.EmployerId}" : null, parent = oldEmployee.EmployerId.HasValue ? $"Employer_{oldEmployee.EmployerId}" : null }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { success = false, detail = $"Editing {employee.Name} failed." }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Employees/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var employee = EmployeesManager.Instance.Get(id);
            if (EmployeesManager.Instance.Delete(id))
            {
                return Json(new { success = true, parent = $"Employer_{employee.EmployerId}" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, responseText = "Your message successfuly sent!" }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}
