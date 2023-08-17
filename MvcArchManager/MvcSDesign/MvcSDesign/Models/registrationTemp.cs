using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcSDesign.Models
{
    public class registrationTemp
    {
        public int registrationID { get; set; }

        [Required(ErrorMessage = "Please enter user name")]
        [Display(Name = "User name")]
        [StringLength(30, ErrorMessage = "User name must be between {2} to {1} character", MinimumLength = 4)]
        public string username { get; set; }


        [Required(ErrorMessage = "Please enter password")]
        [Display(Name = "Password")]
        [StringLength(20, ErrorMessage = "Password must be between {2} to {1} character", MinimumLength = 4)]
        public string password { get; set; }


        [Required(ErrorMessage = "Please enter  name")]
        [Display(Name = "Name")]
        public string name { get; set; }

        [Required(ErrorMessage = "Please enter  designation ")]
        [Display(Name = "Designation")]
        public string designation { get; set; }


        [Required(ErrorMessage = "Please enter  cell no ")]
        [Display(Name = "Cell no")]
        [StringLength(15, ErrorMessage = "Password must be between {2} to {1} character", MinimumLength = 10)]

        public string cellNo { get; set; }

        [Required(ErrorMessage = "Please enter  Country code ")]
        [Display(Name = "Country Code")]
        public string countryCode { get; set; }



        [Required(ErrorMessage = "Please enter  Whats App no ")]
        [Display(Name = "Whats App no")]
        [StringLength(15, ErrorMessage = "WhatsApp number must be 10 digit", MinimumLength = 10)]
        public string whatsappno { get; set; }



        [Required(ErrorMessage = "Please enter EMail ID")]
        [Display(Name = "GMail ID")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email ID")]
        [EmailAddress]
        public string gmailID { get; set; }


        [Required(ErrorMessage = "Please enter  GMail password ")]

        [Display(Name = "GMail Password")]
        public string gmailPassword { get; set; }

        public string prjcount { get; set; }

        [Display(Name = "Department")]
        public string department { get; set; }

        public string designerType { get; set; }

        public string designerDesignation { get; set; }

        public string logintype { get; set; }
        public IEnumerable<SelectListItem> loginTypeList { get; set; }
        public IEnumerable<SelectListItem> designationList { get; set; }

        public IEnumerable<SelectListItem> designerDesignationList { get; set; }

        public IEnumerable<SelectListItem> designerTypeList { get; set; }

        public IEnumerable<SelectListItem> departmentList
        {
            get; set;
        }
    }
}