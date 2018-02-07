using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UbitLibrarySystem.Models
{
    [MetadataType(typeof(Category_DetailsMetaData))]

    public partial class Category_Details
    {

    }
    public class Category_DetailsMetaData
    {
        [Required]
        [Display(Name = "Id")]
        public int Category_id { get; set; }
        [Required]
        [Display(Name = " Category")]
        public string Category_Name { get; set; }




    }
}