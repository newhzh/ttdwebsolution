using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TTDWeb.Models
{
    #region 机构主页
    public class OrganPage
    {
        public OrganPage()
        {
            ProductList = new List<ProductModel>();
        }

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
        /// 电话
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 评级
        /// </summary>
        public string Ranking { get; set; }

        /// <summary>
        /// 已成功受理数量
        /// </summary>
        public string HandledCount { get; set; }        

        /// <summary>
        /// 机构下属的产品列表
        /// </summary>
        public List<ProductModel> ProductList { get; set; }

        #endregion

        

    }
    #endregion
}