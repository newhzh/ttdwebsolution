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
        [Display(Name = "申请人-姓名")]
        public string CustomerName { get; set; }

        /// <summary>
        /// 申请人-手机
        /// </summary>
        [Display(Name = "申请人-手机")]
        public string CustomerPhone { get; set; }

        /// <summary>
        /// 申请人-邮箱
        /// </summary>
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
        [Display(Name = "车贷-房产")]
        public string CarProperty { get; set; }

        /// <summary>
        /// 车贷-房产（用于显示）
        /// </summary>
        [Display(Name = "车贷-房产（用于显示）")]
        public string CarPropertyDisplay { get; set; }
        
        /// <summary>
        /// 车贷-月打卡工资
        /// </summary>
        [Display(Name = "车贷-月打卡工资")]
        [RegularExpression(@"^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$", ErrorMessage = "月打卡工资必须数值型!")]
        public decimal  CarCustomerMonthlySalary { get; set; }

        /// <summary>
        /// 车贷-购车阶段
        /// </summary>
        [Display(Name = "车贷-购车阶段")]
        public string CarPurchasingPeriod { get; set; }

        /// <summary>
        /// 车贷-购车阶段（用于显示）
        /// </summary>
        [Display(Name = "车贷-购车阶段（用于显示）")]
        public string CarPurchasingPeriodDisplay { get; set; }

        /// <summary>
        /// 房贷-房屋类型
        /// </summary>
        [Display(Name = "房贷-房屋类型")]
        public string HouseType { get; set; }

        /// <summary>
        /// 房贷-房屋类型（用于显示）
        /// </summary>
        [Display(Name = "房贷-房屋类型（用于显示）")]
        public string HouseTypeDisplay { get; set; }

        /// <summary>
        /// 房贷-户籍
        /// </summary>
        [Display(Name = "房贷-户籍")]
        public string HouseLocalorNot { get; set; }

        /// <summary>
        /// 房贷-户籍（用于显示）
        /// </summary>
        [Display(Name = "房贷-户籍（用于显示）")]
        public string HouseLocalorNotDisplay { get; set; }

        /// <summary>
        /// 房贷-新旧房
        /// </summary>
        [Display(Name = "房贷-新旧房")]
        public string HouseNew { get; set; }

        /// <summary>
        /// 房贷-新旧房（用于显示）
        /// </summary>
        [Display(Name = "房贷-新旧房（用于显示）")]
        public string HouseNewDisplay { get; set; }

        /// <summary>
        /// 房贷-月收入
        /// </summary>
        [Display(Name = "房贷-月收入")]
        public string HouseIncome { get; set; }

        /// <summary>
        /// 企业-类型
        /// </summary>
        [Display(Name = "企业-类型")]
        public string FirmType { get; set; }

        /// <summary>
        /// 企业-类型（用于显示）
        /// </summary>
        [Display(Name = "企业-类型（用于显示）")]
        public string FirmTypeDisplay { get; set; }

        /// <summary>
        /// 企业-流水
        /// </summary>
        [Display(Name = "企业-流水")]
        [RegularExpression(@"^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$", ErrorMessage = "企业流水必须数值型!")]
        public decimal  FirmAccountBill { get; set; }

        /// <summary>
        /// 企业-经营年限
        /// </summary>
        [Display(Name = "企业-经营年限")]
        public string FirmAge { get; set; }

        /// <summary>
        /// 企业-房产 
        /// </summary>
        [Display(Name = "企业-房产")]
        public string FirmProperty { get; set; }

        /// <summary>
        /// 企业-房产（用于显示）
        /// </summary>
        [Display(Name = "企业-房产（用于显示）")]
        public string FirmPropertyDisplay { get; set; }

        /// <summary>
        /// 个人-雇佣
        /// </summary>
        [Display(Name = "个人-雇佣")]
        public string PerslEmployment { get; set; }

        /// <summary>
        /// 个人-雇佣（用于显示）
        /// </summary>
        [Display(Name = "个人-雇佣（用于显示）")]
        public string PerslEmploymentDisplay { get; set; }

        /// <summary>
        /// 个人-出生年份
        /// </summary>
        [Display(Name = "个人-出生年份")]
        public string PerslYoBirth { get; set; }

        /// <summary>
        /// 个人-工资形式
        /// </summary>
        [Display(Name = "个人-工资形式")]
        public string PerslSalaryType { get; set; }

        /// <summary>
        /// 个人-工资形式（用于显示）
        /// </summary>
        [Display(Name = "个人-工资形式（用于显示）")]
        public string PerslSalaryTypeDisplay { get; set; }

        /// <summary>
        /// 个人-月打卡工资
        /// </summary>
        [Display(Name = "个人-月打卡工资")]
        public string PerslSalary { get; set; }

        /// <summary>
        /// 个人-工作时间
        /// </summary>
        [Display(Name = "个人-工作时间")]
        public string PerslWorkingAge { get; set; }

        /// <summary>
        /// 个人-是否有信用卡
        /// </summary>
        [Display(Name = "个人-是否有信用卡")]
        public string PerslCreditOwner { get; set; }

        /// <summary>
        /// 个人-是否有信用卡（用于显示）
        /// </summary>
        [Display(Name = "个人-是否有信用卡（用于显示）")]
        public string PerslCreditOwnerDisplay { get; set; }

        /// <summary>
        /// 个人-信用卡总额度
        /// </summary>
        [Display(Name = "个人-信用卡总额度")]
        public string PerslCreditAllowance { get; set; }

        /// <summary>
        /// 个人-信用卡是否负债
        /// </summary>
        [Display(Name = "个人-信用卡是否负债")]
        public string PerslCardNo { get; set; }

        /// <summary>
        /// 个人-信用卡是否负债（用于显示）
        /// </summary>
        [Display(Name = "个人-信用卡是否负债（用于显示）")]
        public string PerslCardNoDisplay { get; set; }

        /// <summary>
        /// 个人-信用卡负债额
        /// </summary>
        [Display(Name = "个人-信用卡负债额")]
        public string PerslCreditDue { get; set; }

        /// <summary>
        /// 个人-是否成功贷款
        /// </summary>
        [Display(Name = "个人-是否成功贷款")]
        public string PerslLoanSucc { get; set; }

        /// <summary>
        /// 个人-是否成功贷款（用于显示）
        /// </summary>
        [Display(Name = "个人-是否成功贷款（用于显示）")]
        public string PerslLoanSuccDisplay { get; set; }

        /// <summary>
        /// 个人-贷款是否负债
        /// </summary>
        [Display(Name = "个人-贷款是否负债")]
        public string PerslLoan { get; set; }

        /// <summary>
        /// 个人-贷款是否负债（用于显示）
        /// </summary>
        [Display(Name = "个人-贷款是否负债（用于显示）")]
        public string PerslLoanDisplay { get; set; }

        /// <summary>
        /// 个人-贷款负债额
        /// </summary>
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