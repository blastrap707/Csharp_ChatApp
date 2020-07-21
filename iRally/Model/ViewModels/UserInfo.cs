using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iRally.Model
{
    public class UserInfo
    {
        [DisplayName("UserId")]
        [Required(ErrorMessage = "{0} is required.")]
        public string UserId { get; set; }

        [DisplayName("Password")]
        public string Password { get; set; }
    }
}