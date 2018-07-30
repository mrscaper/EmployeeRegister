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
    public class EmploymentsController : Controller
    {
        private RegisterEntities db = new RegisterEntities();

        // GET: Employments
        public ActionResult Index()
        {
            return View();
        }

        private Tuple<IEnumerable<Employee>, IEnumerable<Employer>> GetEmployed(int id)
        {
            var employer = EmployersManager.Instance.Get(id);
            var subContractors = EmployersManager.Instance.ListSubContractors(id);
            var employees = employer.Employees.ToList();
            var result=new Tuple<IEnumerable<Employee>, IEnumerable<Employer>>(employees, subContractors);
            return result;
        }

        public ActionResult GetNode(int? id)
        {
            if (id.HasValue)
            {
                var model = GetEmployed(id.Value);
                return PartialView("Node", model);
            }

            var employers = DAL.EmployersManager.Instance.List();
            return PartialView("Root", employers);
        }
    }
}