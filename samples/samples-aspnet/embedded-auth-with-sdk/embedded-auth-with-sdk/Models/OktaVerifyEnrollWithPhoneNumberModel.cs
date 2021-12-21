using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace embedded_auth_with_sdk.Models
{
    public class OktaVerifyEnrollWithPhoneNumberModel
    {
        [Required]
        [Display(Name = "country code")]
        public string CountryCode { get; set; }

        [Required]
        [Display(Name = "phone number")]
        public string PhoneNumber { get; set; }
    }
}
