using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace UbitLibrarySystem.Models
{
    [MetadataType(typeof(Book_DetailsMetaData))]

    public partial class Book_Details
    {

    }
    public class Book_DetailsMetaData
    {
       
       
        [Required]
        [Display(Name = "Actual Books")]
        public int No_of_Copies_Actual { get; set; }
        [Required]
        [Display(Name = "Available Books")]
        public int No_of_Copies_Available { get; set; }
        [Required]
        [Display(Name = "Title")]
         public string Book_Title { get; set; }
        [Required]
        [Display(Name = "Author")]
        public string Book_Author { get; set; }
        
    }
}