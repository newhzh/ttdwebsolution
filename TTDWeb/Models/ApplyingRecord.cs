using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TTDWeb.Models
{
    public class ApplyingRecord
    {
        /// <summary>
        /// 产品编号
        /// </summary>
        [Required(ErrorMessage = "产品编号称必填")]
        [Display(Name = "产品编号")]
        public string ProductCode { get; set; }

        /// <summary>
        /// 申请人-姓名
        /// </summary>
        [Required(ErrorMessage = "申请人-姓名必填")]
        [Display(Name = "申请人-姓名")]
        public string CustomerName { get; set; }

        /// <summary>
        /// 申请人-手机
        /// </summary>
        [Required(ErrorMessage = "申请人-手机必填")]
        [Display(Name = "申请人-手机")]
        public string BustomerPhone { get; set; }

        /// <summary>
        /// 申请人-邮箱
        /// </summary>
        [Required(ErrorMessage = "申请人-邮箱必填")]
        [Display(Name = "申请人-邮箱")]
        public string CustomerEmail { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        [Required(ErrorMessage = "融资方名称必填")]
        [Display(Name = "融资方名称")]
        public string ProductType { get; set; }

        /// <summary>
        /// 车贷-房产
        /// </summary>
        [Required(ErrorMessage = "车贷-房产必填")]
        [Display(Name = "车贷-房产")]
        public string CarProperty { get; set; }

        /// <summary>
        /// 车贷-月打卡工资
        /// </summary>
        [Display(Name = "车贷-月打卡工资")]
        [RegularExpression(@"^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$", ErrorMessage = "利率必须数值型!")]
        public decimal  CarCustomerMonthlySalary { get; set; }

        /// <summary>
        /// 车贷-购车阶段
        /// </summary>
        [Required(ErrorMessage = "车贷-购车阶段必填")]
        [Display(Name = "车贷-购车阶段")]
        public string CarPurchasingPeriod { get; set; }

        /// <summary>
        /// 房贷-房屋类型
        /// </summary>
        [Required(ErrorMessage = "房贷-房屋类型必填")]
        [Display(Name = "房贷-房屋类型")]
        public string HouseType { get; set; }

        /// <summary>
        /// 房贷-户籍
        /// </summary>
        [Required(ErrorMessage = "房贷-户籍必填")]
        [Display(Name = "房贷-户籍")]
        public string HouseLocalorNot { get; set; }

        /// <summary>
        /// 房贷-新旧房
        /// </summary>
        [Required(ErrorMessage = "房贷-新旧房必填")]
        [Display(Name = "房贷-新旧房")]
        public string HouseNew { get; set; }

        /// <summary>
        /// 企业-类型
        /// </summary>
        [Required(ErrorMessage = "企业-类型必填")]
        [Display(Name = "企业-类型")]
        public string FirmType { get; set; }

        /// <summary>
        /// 企业-流水
        /// </summary>
        [Display(Name = "企业-流水")]
        [RegularExpression(@"^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$", ErrorMessage = "利率必须数值型!")]
        public decimal  FirmAccountBill { get; set; }

        /// <summary>
        /// 企业-经营年限
        /// </summary>
        [Required(ErrorMessage = "企业-经营年限称必填")]
        [Display(Name = "企业-经营年限")]
        public string FirmAge { get; set; }

        /// <summary>
        /// 企业-房产
        /// </summary>
        [Required(ErrorMessage = "企业-房产必填")]
        [Display(Name = "企业-房产")]
        public string FirmProperty { get; set; }

        /// <summary>
        /// 个人-雇佣
        /// </summary>
        [Required(ErrorMessage = "个人-雇佣称必填")]
        [Display(Name = "个人-雇佣")]
        public string PerslEmployment { get; set; }

        /// <summary>
        /// 个人-出生年份
        /// </summary>
        [Required(ErrorMessage = "个人-出生年份必填")]
        [Display(Name = "个人-出生年份")]
        public string PerslYoBirth { get; set; }

        /// <summary>
        /// 个人-工资形式
        /// </summary>
        [Required(ErrorMessage = "融资方名称个人-工资形式必填")]
        [Display(Name = "个人-工资形式")]
        public string PerslSalaryTypee { get; set; }

        /// <summary>
        /// 个人-工作时间
        /// </summary>
        [Required(ErrorMessage = "个人-工作时间必填")]
        [Display(Name = "个人-工作时间")]
        public string PerslWorkingAge { get; set; }

        /// <summary>
        /// 个人-信用卡
        /// </summary>
        [Required(ErrorMessage = "个人-信用卡必填")]
        [Display(Name = "个人-信用卡")]
        public string PerslCreditOwner { get; set; }

        /// <summary>
        /// 个人-信用卡数
        /// </summary>
        [Display(Name = "个人-信用卡数")]
        public string PerslCardNo { get; set; }

        /// <summary>
        /// 个人-信用卡额度
        /// </summary>
        [Display(Name = "个人-信用卡额度")]
        public string PerslCreditAllowance { get; set; }

        /// <summary>
        /// 个人-信用卡负债
        /// </summary>
        [Display(Name = "个人-信用卡负债")]
        public string PerslCreditDue { get; set; }

        /// <summary>
        /// 个人-贷款
        /// </summary>
        [Required(ErrorMessage = "个人-贷款必填")]
        [Display(Name = "个人-贷款")]
        public string PerslLoan { get; set; }

        /// <summary>
        /// 个人-贷款负债
        /// </summary>
        [Required(ErrorMessage = "个人-贷款负债必填")]
        [Display(Name = "个人-贷款负债")]
        public string PerslLoanDue { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "融资方名称")]
        public string CreatTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        public string CaseState { get; set; }

        /// <summary>
        /// 用户IP地址
        /// </summary>
        [Display(Name = "融资方名称")]
        public string IPaddress { get; set; }

    }
}