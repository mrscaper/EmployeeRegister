using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.UI.WebControls;

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
            [Display(Name="General Contractor")] public int EmployerId;
            [Display(Name = "General Contractor")] public Employer GeneralContractor;
        }
    }
}
