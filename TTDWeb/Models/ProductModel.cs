using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TTDWeb.Models
{
    public class ProductModel
    {
        public ProductModel() 
        {
            Customs = new List<CustomModel>();
        }

        /// <summary>
        /// 自动编号
        /// </summary>
        public string ProductCode { get; set; }
        
        /// <summary>
        /// 产品名称
        /// </summary>
        [Display(Name = "产品名称")]
        [Required(ErrorMessage = "产品名称必填")]
        public string ProductName { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        public string OrganName { get; set; }

        /// <summary>
        /// 机构ID
        /// </summary>
        public string OrganID { get; set; }

        /// <summary>
        /// 机构logo
        /// </summary>
        public string OrganLogo { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        [Display(Name = "产品类型")]
        [Required(ErrorMessage = "产品类型必选")]
        public string ProductType { get; set; }

        /// <summary>
        /// 年化利率
        /// </summary>
        [Display(Name = "年化利率")]
        [RegularExpression(@"^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$", ErrorMessage = "利率必须数值型!")]
        public decimal AnnualRate { get; set; }

        /// <summary>
        /// 年华利率（用于显示）
        /// </summary>
        public string AnnualRateDisplay { get; set; }

        /// <summary>
        /// 还款方式
        /// </summary>
        [Display(Name = "还款方式")]
        [Required(ErrorMessage = "还款方式必选")]
        public string RepaymentType { get; set; }

        /// <summary>
        /// 额度下限
        /// </summary>
        [RegularExpression(@"^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$", ErrorMessage = "额度下限必须数值型!")]
        public decimal MoneyBottom { get; set; }

        /// <summary>
        /// 额度上限
        /// </summary>
        [RegularExpression(@"^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$", ErrorMessage = "额度上限必须数值型!")]
        public decimal MoneyTop { get; set; }

        /// <summary>
        /// 期限下限
        /// </summary>
        [RegularExpression(@"^-?\d+$", ErrorMessage = "期限下限必需正整数!")]
        public int TermBottom { get; set; }

        /// <summary>
        /// 期限上限
        /// </summary>
        [RegularExpression(@"^-?\d+$", ErrorMessage = "期限上限必需正整数!")]
        public int TermTop { get; set; }

        /// <summary>
        /// 月服务费
        /// </summary>
        [Display(Name = "月服务费")]
        [Required(ErrorMessage = "月服务费必填")]
        [RegularExpression(@"^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$", ErrorMessage = "月服务费必须数值型!")]
        public decimal ServerFeeMonthly { get; set; }

        /// <summary>
        /// 一次性费用
        /// </summary>
        [Display(Name = "一次性费用")]
        [Required(ErrorMessage = "一次性费用必填")]
        [RegularExpression(@"^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$", ErrorMessage = "一次性费用必须数值型!")]
        public decimal ServerFeeOncee { get; set; }

        /// <summary>
        /// 最快放款时间
        /// </summary>
        [Display(Name = "最快放款时间")]
        [Required(ErrorMessage = "最快放款时间必填")]
        [RegularExpression(@"^-?\d+$", ErrorMessage = "最快放款时间必需正整数!")]
        public int GetLoanDays { get; set; }

        /// <summary>
        /// 申请条件
        /// </summary>
        [Display(Name = "申请条件")]
        [Required(ErrorMessage = "申请条件必填")]
        public string ApplyCondition { get; set; }

        /// <summary>
        /// 所需材料
        /// </summary>
        [Display(Name = "所需材料")]
        [Required(ErrorMessage = "所需材料必填")]
        public string RequiredFile { get; set; }

        /// <summary>
        /// 详细说明
        /// </summary>
        [Display(Name = " 详细说明")]
        public string Details { get; set; }

        /// <summary>
        /// 提前还款条件
        /// </summary>
        [Display(Name = "提前还款条件")]
        [Required(ErrorMessage = "提前还款条件必填")]
        public string EarlyRepaymentCondition { get; set; }

        /// <summary>
        /// 机构简介
        /// </summary>
        [Display(Name = "机构简介")]
        public string Memo { get; set; }

        /// <summary>
        /// 担保方式
        /// </summary>
        [Display(Name = "担保方式")]
        [Required(ErrorMessage = "担保方式必填")]
        public string VouchType { get; set; }

        /// <summary>
        /// 每月偿还金额
        /// </summary>
        public string RepaymentMonthly { get; set; }

        /// <summary>
        /// 客户经理集合
        /// </summary>
        public List<CustomModel> Customs { get; set; }
    }

    #region AA10枚举模型
    public class AA10_Item
    {
        public AA10_Item(string text, string val)
        {
            Text = text;
            Value = val;
        }

        public string Text { get; set; }
        public string Value { get; set; }
    }
    #endregion
}