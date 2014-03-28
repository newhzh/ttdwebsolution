﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TTDWeb.Models
{
    #region 机构主页
    public class OrganPage
    {

        #region 列表显示

        /// <summary>
        /// 机构名称
        /// </summary>
        public string OrganName { get; set; }

        /// <summary>
        /// 机构logo
        /// </summary>
        public string OrganLogo { get; set; }

        /// <summary>
        /// 机构类型
        /// </summary>
        public string OrganType { get; set; }

        /// <summary>
        /// 机构简介
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        [Display(Name = "产品名称")]
        [Required(ErrorMessage = "产品名称必填")]
        public string ProductName { get; set; }

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

        #endregion

        #region 展开显示

        #region 产品详情
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
        /// 还款方式
        /// </summary>
        [Display(Name = "还款方式")]
        [Required(ErrorMessage = "还款方式必选")]
        public string RepaymentType { get; set; }

        /// <summary>
        /// 最快放款时间
        /// </summary>
        [Display(Name = "最快放款时间")]
        [Required(ErrorMessage = "最快放款时间必填")]
        [RegularExpression(@"^-?\d+$", ErrorMessage = "最快放款时间必需正整数!")]
        public int GetLoanDays { get; set; }

        /// <summary>
        /// 担保方式
        /// </summary>
        [Display(Name = "担保方式")]
        [Required(ErrorMessage = "担保方式必填")]
        public string VouchType { get; set; }

        #endregion

        #region 申请条件
        /// <summary>
        /// 申请条件
        /// </summary>
        [Display(Name = "申请条件")]
        [Required(ErrorMessage = "申请条件必填")]
        public string ApplyCondition { get; set; }
        #endregion

        #region 所需材料
        /// <summary>
        /// 所需材料
        /// </summary>
        [Display(Name = "所需材料")]
        [Required(ErrorMessage = "所需材料必填")]
        public string RequiredFile { get; set; }
        #endregion

        #region 详细说明
        /// <summary>
        /// 详细说明
        /// </summary>
        [Display(Name = "详细说明")]
        public string Details { get; set; }
        #endregion

        #region 客户经理
        /// <summary>
        /// 客户经理集合
        /// </summary>
        public List<CustomModel> Customs { get; set; }
        #endregion

        #endregion

    }
    #endregion
}