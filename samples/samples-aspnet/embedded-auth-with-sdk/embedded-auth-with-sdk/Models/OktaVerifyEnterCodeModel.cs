using Okta.Idx.Sdk.OktaVerify;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace embedded_auth_with_sdk.Models
{
    public class OktaVerifyEnterCodeModel
    {
        public OktaVerifyEnterCodeModel()
        { 
        }
        
        [Required]
        [Display(Name = "Enter code from Okta Verify app")]
        public string Code { get; set; }
    }
}