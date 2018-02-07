using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UbitLibrarySystem.Models
{
    [MetadataType(typeof(Publication_DetailsMetaData))]

    public partial class Publication_Details
    {

    }
    public class Publication_DetailsMetaData
    {
        [Required]
        [Display(Name = "Id")]
        public int Publication_id { get; set; }
        [Required]
        [Display(Name = "Publisher")]
        public string Publisher_Name { get; set; }




    }
}