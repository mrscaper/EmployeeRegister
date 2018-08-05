using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL
{
    /// <summary>
    /// Data access for employee records
    /// </summary>
    public class EmployeesManager
    {
        /// <summary>
        /// Singleton
        /// </summary>
        public static readonly EmployeesManager Instance = new EmployeesManager();

        /// <summary>
        /// Queries a single Employee
        /// </summary>
        /// <param name="id">Employee</param>
        /// <returns>Single Employer including the Employer.</returns>
        public Employee Get(int id)
        {
            using (var db = new RegisterEntities())
            {
                return db.Employees.Find(id);
            }
        }

        /// <summary>
        /// Soft deletes a single Employee
        /// </summary>
        /// <param name="id">Employee</param>
        /// <returns>True if successful.</returns>
        public bool Delete(int id)
        {
            using (var db = new RegisterEntities())
            {
                var employee = db.Employees.Find(id);
                if (employee != null)
                {
                    employee.Deleted = true;
                    db.SaveChanges();
                    return true;
                }
            }

            return false;
        }


        public bool Add(Employee employee)
        {
            using (var db = new RegisterEntities())
            {
                try
                {
                    db.Employees.Add(employee);
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

        public bool Modify(Employee employee)
        {
            using (var db = new RegisterEntities())
            {
                try
                {
                    db.Entry(employee).State = EntityState.Modified;
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
