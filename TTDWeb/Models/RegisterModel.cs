using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TTDWeb.Models
{
    #region 注册模型
    public class RegisterModel
    {
        [Required]
        [Display(Name = "手机")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)", ErrorMessage = "手机号码格式非法!")]
        public string CellPhone { get; set; }
        
        [Required]
        [Display(Name = "姓名")]
        public string CustomName { get; set; }

        [Required]
        [Display(Name = "省份")]
        public string Province { get; set; }

        [Required]
        [Display(Name = "城市")]
        public string City { get; set; }
 
        [Display(Name = "区县")]
        public string County { get; set; }

        [Required]
        [Display(Name = "机构")]
        public string Organ { get; set; }

        [Required]
        //[StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Pwd { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Pwd", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "电子邮箱")]
        //[DataType(DataType.EmailAddress)]
        //[RegularExpression("\\w+(\\.\\w+)*@\\w+(\\.\\w+)+", ErrorMessage = "电子邮箱格式非法!")]
        public string Email { get; set; }

        [Display(Name = "昵称")]
        public string NickName { get; set; }

        public string yzm { get; set; }

        //以下属性是新添加
        public string CustomID { get; set; }
        public string Sex { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string OrganAddress { get; set; }
        public string OrganName { get; set; }
        
        /// <summary>
        /// 当作职位
        /// </summary>
        public string OrganDpt { get; set; }
        public string WorkYears { get; set; }
    }
    #endregion
}