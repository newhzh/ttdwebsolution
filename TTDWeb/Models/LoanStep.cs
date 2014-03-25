using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TTDWeb.Models
{
    #region 车贷申请

    #region 车贷申请第一步
    public class CarLoanStep1
    {
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
        public decimal CarCustomerMonthlySalary { get; set; }

        /// <summary>
        /// 车贷-购车阶段
        /// </summary>
        [Required(ErrorMessage = "车贷-购车阶段必填")]
        [Display(Name = "车贷-购车阶段")]
        public string CarPurchasingPeriod { get; set; }          
    }
    #endregion

    #region 车贷申请第二步
    public class CarLoanStep2
    {
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
        public string CustomerPhone { get; set; }

        /// <summary>
        /// 申请人-邮箱
        /// </summary>
        [Required(ErrorMessage = "申请人-邮箱必填")]
        [Display(Name = "申请人-邮箱")]
        public string CustomerEmail { get; set; }

    }
    #endregion

    #endregion

    #region 房贷申请

    #region 房贷申请第一步
    public class HomeLoanStep1
    {
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
        /// 房贷-月收入
        /// </summary>
        [Required(ErrorMessage = "房贷-月收入")]
        [Display(Name = "房贷-月收入")]
        public string HouseIncome { get; set; }

        /// <summary>
        /// 房贷-新旧房
        /// </summary>
        [Required(ErrorMessage = "房贷-新旧房必填")]
        [Display(Name = "房贷-新旧房")]
        public string HouseNew { get; set; }
    }
    #endregion

    #region 房贷申请第二步
    public class HomeLoanStep2
    {
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
        public string CustomerPhone { get; set; }

        /// <summary>
        /// 申请人-邮箱
        /// </summary>
        [Required(ErrorMessage = "申请人-邮箱必填")]
        [Display(Name = "申请人-邮箱")]
        public string CustomerEmail { get; set; }
    }    
    #endregion

    #endregion

    #region 企业贷申请

    #region 企业贷申请第一步
    public class FirmLoanStep1
    {
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
        public decimal FirmAccountBill { get; set; }

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
    }

    #endregion

    #region 企业贷申请第二步
    public class FirmLoanStep2
    {
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
        public string CustomerPhone { get; set; }

        /// <summary>
        /// 申请人-邮箱
        /// </summary>
        [Required(ErrorMessage = "申请人-邮箱必填")]
        [Display(Name = "申请人-邮箱")]
        public string CustomerEmail { get; set; }
    }
    #endregion

    #endregion

    #region 消费贷申请

    #region 消费贷申请第一步
    public class PurchaseLoanStep1
    {
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
        public string PerslSalaryType { get; set; }

        /// <summary>
        /// 个人-工作时间
        /// </summary>
        [Required(ErrorMessage = "个人-工作时间必填")]
        [Display(Name = "个人-工作时间")]
        public string PerslWorkingAge { get; set; }
    }

    #endregion

    #region 消费贷申请第二步
    public class PurchaseLoanStep2
    {
        /// <summary>
        /// 个人-是否有信用卡
        /// </summary>
        [Required(ErrorMessage = "个人-信用卡必填")]
        [Display(Name = "个人-信用卡")]
        public string PerslCreditOwner { get; set; }

        /// <summary>
        /// 个人-信用卡总额度
        /// </summary>
        [Display(Name = "个人-信用卡额度")]
        public string PerslCreditAllowance { get; set; }

        /// <summary>
        /// 个人-信用卡是否负债
        /// </summary>
        [Display(Name = "个人-信用卡数")]
        public string PerslCardPaid { get; set; }
                
        /// <summary>
        /// 个人-信用卡负债额
        /// </summary>
        [Display(Name = "个人-信用卡负债")]
        public string PerslCreditDue { get; set; }

        /// <summary>
        /// 个人-是否成功贷款
        /// </summary>
        [Required(ErrorMessage = "个人-贷款必填")]
        [Display(Name = "个人-贷款")]
        public string PerslLoanSucc { get; set; }

        /// <summary>
        /// 个人-贷款是否负债
        /// </summary>
        [Required(ErrorMessage = "个人-贷款必填")]
        [Display(Name = "个人-是否成功申请贷款")]
        public string PerslLoan { get; set; }

        /// <summary>
        /// 个人-贷款负债额
        /// </summary>
        [Required(ErrorMessage = "个人-贷款负债必填")]
        [Display(Name = "个人-贷款负债")]
        public string PerslLoanDue { get; set; }
    }
    #endregion

    #region 消费贷申请第三步
    public class PurchaseLoanStep3
    {
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
        public string CustomerPhone { get; set; }

        /// <summary>
        /// 申请人-邮箱
        /// </summary>
        [Required(ErrorMessage = "申请人-邮箱必填")]
        [Display(Name = "申请人-邮箱")]
        public string CustomerEmail { get; set; }
    }
    #endregion

    #endregion


}