using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftDelete
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db=new RegisterEntities())
            {
                foreach (var employer in db.Employers.Where(e=>e.Employer2==null))
                {
                    Console.WriteLine($"Employer {employer.Name.TrimEnd(' ')} {(employer.Deleted.GetValueOrDefault() ? "Is deleted":"Is cool")}");
                    foreach (var subContractor in employer.Employer1)
                    {
                        Console.WriteLine($"   Employer {subContractor.Name.TrimEnd(' ')} {(subContractor.Deleted.GetValueOrDefault() ? "Is deleted" : "Is cool")}");
                    }
                    foreach (var employee in employer.Employees)
                    {
                        Console.WriteLine($"   Employee {employee.Name.TrimEnd(' ')} {(employee.Deleted.GetValueOrDefault() ? "Is deleted" : "Is cool")}");
                    }
                }
            }
        }
    }
}
