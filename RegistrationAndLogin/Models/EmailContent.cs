using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RegistrationAndLogin.Models
{
    public class EmailContent
    {
        [Required]
        [DisplayName("To")]
        public string supplier_id { get; set; }
        [Required]
        [DisplayName("Subject")]
        public string EmailSubject { get; set; }
        [Required]
        [DisplayName("Message")]
        public string EmailBody { get; set; }
        //[Not Required]
        [DisplayName("Attachment")]
        public HttpPostedFile File { get; set; }
    }
}