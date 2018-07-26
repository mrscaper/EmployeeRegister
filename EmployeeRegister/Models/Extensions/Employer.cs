using System.ComponentModel.DataAnnotations;

namespace EmployeeRegister.Models
{
    using System;
    using System.Collections.Generic;
    [System.ComponentModel.DataAnnotations.MetadataType(typeof(EmployerMetadata))]
    public partial class Employer
    {

        internal sealed class EmployerMetadata
        {
            [Required] [MaxLength(255)] public string Name;
        }
    }
}
