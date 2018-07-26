using System.ComponentModel.DataAnnotations;

namespace EmployeeRegister.Models
{
    using System;
    using System.Collections.Generic;
    [System.ComponentModel.DataAnnotations.MetadataType(typeof(EmployeeMetadata))]
    public partial class Employee
    {

        internal sealed class EmployeeMetadata
        {
            [Required] [MaxLength(255)] public string Name;
            [EmailAddress] public string Email;
        }
    }
}
