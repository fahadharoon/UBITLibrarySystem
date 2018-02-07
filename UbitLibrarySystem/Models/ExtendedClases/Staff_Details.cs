using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UbitLibrarySystem.Models
{
    [MetadataType(typeof(Staff_DetailsMetaData))]

    public partial class Staff_Details
    {

    }
    public class Staff_DetailsMetaData
    {
        [Required]
        [Display(Name = "Id")]
        public int Staff_id { get; set; }
        [Required]
        [Display(Name = "Issued By")]
        public string Staff_Name { get; set; }
        [Required]
        public string Designation { get; set; }



    }
}