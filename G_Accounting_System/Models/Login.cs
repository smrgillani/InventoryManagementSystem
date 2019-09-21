using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace G_Accounting_System.Models
{
    public class Login
    {
        [Required(ErrorMessage="Enter email")]
        [StringLength(100)]
        public string email { get; set; }
        [Required(ErrorMessage="Enter Password")]
        [DataType(DataType.Password)]
        [StringLength(50)]
        public string password { get; set; }
        public string ErrorMessage { get; set; }
    }
}