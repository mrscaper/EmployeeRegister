using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DAL.Models;

namespace DAL
{
    /// <summary>
    /// Data access for employer records.
    /// </summary>
    public class EmployersManager
    {
        /// <summary>
        /// Singleton
        /// </summary>
        public static readonly EmployersManager Instance = new EmployersManager();

        /// <summary>
        /// By defauld queries all independent employers.
        /// </summary>
        /// <param name="independent"></param>
        /// <returns>Collection of employers including their employees & sub-contractors.</returns>
        public IEnumerable<Employer> List(bool independent = true)
        {
            using (var db = new RegisterEntities())
            {
                var employers = db.Employers
                    .Where(e => e.Deleted != true)
                    .Where(e => e.GeneralContractor == null || !independent);
                foreach (var employer in employers)
                {
                    employer.SubContractors = employer.SubContractors.Where(s => s.Deleted != true).ToList();
                    employer.Employees = employer.Employees.Where(e => e.Deleted != true).ToList();
                }
                return employers.ToList();
            }
        }

        /// <summary>
        /// Queries all sub-contractors
        /// </summary>
        /// <param name="id">General Contractor</param>
        /// <returns>Collection of sub-contractors.</returns>
        public IEnumerable<Employer> ListSubContractors(int id)
        {
            using (var db = new RegisterEntities())
            {
                var subContractors = db.Employers
                    .Single(e => e.Id == id)
                    .SubContractors.Where(s=>s.Deleted!=true);
                foreach (var subContractor in subContractors)
                {
                    subContractor.SubContractors = subContractor.SubContractors.Where(s => s.Deleted != true).ToList();
                    subContractor.Employees = subContractor.Employees.Where(e => e.Deleted != true).ToList();
                }
                return subContractors;
            }
        }

        /// <summary>
        /// Queries a single Employer
        /// </summary>
        /// <param name="id">Employer</param>
        /// <returns>Single Employer including all Employees and SubContractors.</returns>
        public Employer Get(int id)
        {
            using (var db =new RegisterEntities())
            {
                var employer = db.Employers
                    .Single(e=>e.Id==id);
                employer.SubContractors = employer.SubContractors.Where(s => s.Deleted != true).ToList();
                employer.Employees = employer.Employees.Where(e => e.Deleted != true).ToList();
                return employer;
            }
        }

        /// <summary>
        /// Soft deletes a single Employer
        /// </summary>
        /// <param name="id">Employer</param>
        /// <returns>True if successful.</returns>
        public bool Delete(int id)
        {
            using (var db =new RegisterEntities())
            {
                var employer=db.Employers.Find(id);
                if (employer != null)
                {
                    employer.Deleted = true;
                    db.SaveChanges();
                    return true;
                }
            }

            return false;
        }


        public object GetSelectList(int? id)
        {
            using (var db=new RegisterEntities())
            {
                if (id.HasValue)
                {
                    return new SelectList(db.Employers.ToList(), "Id", "Name", id.Value);
                }
                else
                {
                    return new SelectList(db.Employers.ToList(), "Id", "Name");
                }
            }
        }


        public bool Add(Employer employer)
        {
            using (var db=new RegisterEntities())
            {
                try
                {
                    db.Employers.Add(employer);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
        }

        public bool Modify(Employer employer)
        {
            using (var db=new RegisterEntities())
            {
                try
                {
                    db.Entry(employer).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
        }
    }
}
