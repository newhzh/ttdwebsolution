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
        [Display(Name = "产品编号")]
        public string ProductCode { get; set; }

        /// <summary>
        /// 申请人-姓名
        /// </summary>
        [Required(ErrorMessage = "申请人姓名必填")]
        [Display(Name = "申请人-姓名")]
        public string CustomerName { get; set; }

        /// <summary>
        /// 申请人-手机
        /// </summary>
        [Required(ErrorMessage = "申请人手机号必填")]
        [Display(Name = "申请人-手机")]
        public string CustomerPhone { get; set; }

        /// <summary>
        /// 申请人-邮箱
        /// </summary>
        [Required(ErrorMessage = "申请人邮箱必填")]
        [Display(Name = "申请人-邮箱")]
        public string CustomerEmail { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>        
        [Display(Name = "产品类型")]
        public string ProductType { get; set; }

        /// <summary>
        /// 车贷-房产
        /// </summary>
        [Required(ErrorMessage = "房产类型必填")]
        [Display(Name = "车贷-房产")]
        public string CarProperty { get; set; }

        /// <summary>
        /// 车贷-月打卡工资
        /// </summary>
        [Required(ErrorMessage = "月打卡工资必填")]
        [Display(Name = "车贷-月打卡工资")]
        [RegularExpression(@"^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$", ErrorMessage = "月打卡工资必须数值型!")]
        public decimal  CarCustomerMonthlySalary { get; set; }

        /// <summary>
        /// 车贷-购车阶段
        /// </summary>
        [Required(ErrorMessage = "请填入购车阶段")]
        [Display(Name = "车贷-购车阶段")]
        public string CarPurchasingPeriod { get; set; }

        /// <summary>
        /// 房贷-房屋类型
        /// </summary>
        [Required(ErrorMessage = "请输入房屋类型")]
        [Display(Name = "房贷-房屋类型")]
        public string HouseType { get; set; }

        /// <summary>
        /// 房贷-户籍
        /// </summary>
        [Required(ErrorMessage = "申请人户籍必填")]
        [Display(Name = "房贷-户籍")]
        public string HouseLocalorNot { get; set; }

        /// <summary>
        /// 房贷-新旧房
        /// </summary>
        [Required(ErrorMessage = "是否新旧房必填")]
        [Display(Name = "房贷-新旧房")]
        public string HouseNew { get; set; }

        /// <summary>
        /// 房贷-月收入
        /// </summary>
        [Required(ErrorMessage = "申请人月收入必填")]
        [Display(Name = "房贷-月收入")]
        public string HouseIncome { get; set; }

        /// <summary>
        /// 企业-类型
        /// </summary>
        [Required(ErrorMessage = "企业类型必填")]
        [Display(Name = "企业-类型")]
        public string FirmType { get; set; }

        /// <summary>
        /// 企业-流水
        /// </summary>
        [Required(ErrorMessage = "企业流水必填")]
        [Display(Name = "企业-流水")]
        [RegularExpression(@"^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$", ErrorMessage = "企业流水必须数值型!")]
        public decimal  FirmAccountBill { get; set; }

        /// <summary>
        /// 企业-经营年限
        /// </summary>
        [Required(ErrorMessage = "企业经营年限称必填")]
        [Display(Name = "企业-经营年限")]
        public string FirmAge { get; set; }

        /// <summary>
        /// 企业-房产 
        /// </summary>
        [Required(ErrorMessage = "房产类型必填")]
        [Display(Name = "企业-房产")]
        public string FirmProperty { get; set; }

        /// <summary>
        /// 个人-雇佣
        /// </summary>
        [Required(ErrorMessage = "雇佣信息必填")]
        [Display(Name = "个人-雇佣")]
        public string PerslEmployment { get; set; }

        /// <summary>
        /// 个人-出生年份
        /// </summary>
        [Required(ErrorMessage = "出生年份必填")]
        [Display(Name = "个人-出生年份")]
        public string PerslYoBirth { get; set; }

        /// <summary>
        /// 个人-工资形式
        /// </summary>
        [Required(ErrorMessage = "工资形式必填")]
        [Display(Name = "个人-工资形式")]
        public string PerslSalaryType { get; set; }

        /// <summary>
        /// 个人-月打卡工资
        /// </summary>
        [Required(ErrorMessage = "月收入必填")]
        [Display(Name = "个人-月打卡工资")]
        public string PerslSalary { get; set; }

        /// <summary>
        /// 个人-工作时间
        /// </summary>
        [Required(ErrorMessage = "工作年限必填")]
        [Display(Name = "个人-工作时间")]
        public string PerslWorkingAge { get; set; }

        /// <summary>
        /// 个人-是否有信用卡
        /// </summary>
        [Required(ErrorMessage = "是否有信用卡必填")]
        [Display(Name = "个人-是否有信用卡")]
        public string PerslCreditOwner { get; set; }

        /// <summary>
        /// 个人-信用卡总额度
        /// </summary>
        [Required(ErrorMessage = "信用卡总额度必填")]
        [Display(Name = "个人-信用卡总额度")]
        public string PerslCreditAllowance { get; set; }

        /// <summary>
        /// 个人-信用卡是否负债
        /// </summary>
        [Required(ErrorMessage = "信用卡是否负债必填")]
        [Display(Name = "个人-信用卡是否负债")]
        public string PerslCardNo { get; set; }

        /// <summary>
        /// 个人-信用卡负债额
        /// </summary>
        [Required(ErrorMessage = "信用卡负债额必填")]
        [Display(Name = "个人-信用卡负债额")]
        public string PerslCreditDue { get; set; }

        /// <summary>
        /// 个人-是否成功贷款
        /// </summary>
        [Required(ErrorMessage = "是否成功贷款必填")]
        [Display(Name = "个人-是否成功贷款")]
        public string PerslLoanSucc { get; set; }

        /// <summary>
        /// 个人-贷款是否负债
        /// </summary>
        [Required(ErrorMessage = "个人-贷款是否负债必填")]
        [Display(Name = "个人-贷款是否负债")]
        public string PerslLoan { get; set; }

        /// <summary>
        /// 个人-贷款负债额
        /// </summary>
        [Required(ErrorMessage = "贷款负债额必填")]
        [Display(Name = "个人-贷款负债")]
        public string PerslLoanDue { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public string CreatTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "申请状态")]
        public string CaseState { get; set; }

        /// <summary>
        /// 用户IP地址
        /// </summary>
        [Display(Name = "用户IP地址")]
        public string IPaddress { get; set; }

    }
}