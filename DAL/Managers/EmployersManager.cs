using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    .Where(e => e.GeneralContractor == null || !independent)
                    .Include(e => e.SubContractors.Where(s=>s.Deleted!=true))
                    .Include(e => e.Employees)
                    .ToList();
                return employers;
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
                    .Include(e=>e.SubContractors.Select(s=>s.SubContractors))
                    .Include(e=>e.SubContractors.Select(s=>s.Employees))
                    .Single(e=>e.Id==id)
                    .SubContractors
                    .ToList();
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
                    .Include(e=>e.Employees)
                    .Include(e=>e.SubContractors)
                    .Single(e=>e.Id==id);
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
    }
}
