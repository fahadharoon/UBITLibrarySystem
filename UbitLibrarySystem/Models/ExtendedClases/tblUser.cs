using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UbitLibrarySystem.Models
{
    [MetadataType(typeof(tblUserMetaData))]
    public partial class tblUser
    {
       

    }
    public partial class tblUserMetaData
    {
        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email Id Required")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Display(Name = "Password")]
     
        public string password { get; set; }

        [Display(Name ="Confirm Password")]
   
        public string confirm_password { get; set; }

        [Display(Name = "Select User")]

        public int roleid { get; set; }

    }
}