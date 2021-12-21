using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace embedded_auth_with_sdk.Models
{
    public class OktaVerifyEnrollWithEmailModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
