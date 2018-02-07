using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UbitLibrarySystem.Models
{
    [MetadataType(typeof(Borrower_DetailsMetaData))]

    public partial class Borrower_Details
    {

    }
    public class Borrower_DetailsMetaData
    {
        public int PKBorrower_id { get; set; }
        [Required]
        [Display(Name = "Id")]
        public int Borrower_id { get; set; }
        [Required]
        [Display(Name = "Issue Date")]
        public System.DateTime Borrowed_From_Date { get; set; }


        [Required]
        [Display(Name = "Return Date")]
        public System.DateTime Borrowed_To_Date { get; set; }



    }
}