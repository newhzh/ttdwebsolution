using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace TTDWeb.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "请填入手机号码")]
        [Display(Name = "帐号")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "请填入密码")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "记住登录账户?")]
        public bool RememberMe { get; set; }
    }
}
