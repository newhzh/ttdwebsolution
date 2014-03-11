using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDWeb.Models
{
    public class CustomModel
    {
        /// <summary>
        /// 客户经理姓名
        /// </summary>
        public string CustomName { get; set; }

        /// <summary>
        /// 客户经理归属单位
        /// </summary>
        public string OrganID { get; set; }

        /// <summary>
        /// 归属单位名称
        /// </summary>
        public string OrganName { get; set; }


    }
}