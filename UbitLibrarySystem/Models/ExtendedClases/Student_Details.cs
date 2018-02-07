using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace UbitLibrarySystem.Models
{
    [MetadataType(typeof(Student_DetailsMetaData))]

    public partial class Student_Details
    {

    }
    public class Student_DetailsMetaData
    {
        [Required]
        [Display(Name = "Id")]
        public int Student_id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Student_Name { get; set; }
        [Required]
        [Display(Name = "Date Of Birth")]
        public System.DateTime Date_Of_Birth { get; set; }
        [Required]
        [Display(Name = "Contact Number")]
        public string Contact_No { get; set; }

       [Required]
        public string Gender { get; set; }
       [Required]
        public string Program { get; set; }
       
    }
}
