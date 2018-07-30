using System.Collections.Generic;
using DAL;
using DAL.Models;

namespace EmployeeRegister.Models
{
    public class EmploymentNodeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<EmploymentNodeViewModel> Children { get; set; }

        public EmploymentNodeViewModel(Employee employee)
        {
            Id = employee.Id;
            Name = employee.Name;
            Children = null;
        }

        public EmploymentNodeViewModel(Employer employer)
        {
            Id = employer.Id;
            Name = employer.Name;
            Children = new List<EmploymentNodeViewModel>();
            foreach (var employee in employer.Employees)
            {
                Children.Add(new EmploymentNodeViewModel(employee));
            }
            foreach (var contractor in employer.SubContractors)
            {
                Children.Add(new EmploymentNodeViewModel(contractor));
            }
        }
    }
}