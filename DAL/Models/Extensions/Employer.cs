using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    using System;
    using System.Collections.Generic;
    [System.ComponentModel.DataAnnotations.MetadataType(typeof(EmployerMetadata))]
    public partial class Employer
    {
        /// <summary>
        /// Metadata for auto-generated class
        /// </summary>
        internal sealed class EmployerMetadata
        {
            /// <summary>
            /// These properties are never used thus, they do not warrant
            /// Compiler Warning (level 4) CS0649
            /// Field 'field' is never assigned to, and will always have its default value 'value'
            /// The compiler detected an uninitialized private or internal field declaration that is never assigned a value.
            /// </summary>
#pragma warning disable 0649
            [Required] [MaxLength(255)] public string Name;
            [Display(Name = "General Contractor")] public Employer GeneralContractor;
            [Display(Name = "General Contractor")] public int? EmployerId { get; set; }
#pragma warning restore 0649
        }
    }
}
