using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDWeb.Models
{
    public class CustomModel
    {
        public CustomModel() 
        {
            ProductList = new List<ProductModel>();
            ApplyingRecordList = new List<ApplyingRecord>();
        }

        public List<ProductModel> ProductList { get; set; }
        public List<ApplyingRecord> ApplyingRecordList { get; set; }

        public string CustomID { get; set; }

        /// <summary>
        /// 客户经理姓名
        /// </summary>
        public string CustomName { get; set; }
        
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 出身年月
        /// </summary>
        public string DateOfBirth { get; set; }

          
        /// <summary>
        /// 客户经理电话（手机）
        /// </summary>
        public string CellPhone { get; set; }

        /// <summary>
        /// 客户经理归属单位
        /// </summary>
        public string OrganID { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string OrganName { get; set; }

        /// <summary>
        /// 单位地址
        /// </summary>
        public string OrganAddress { get; set; }
                
        /// <summary>
        /// 用户账号（必须用邮箱，需认证）
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 工作职位
        /// </summary>
        public string Occupation { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string Pwd { get; set; }

        /// <summary>
        /// 工作年限
        /// </summary>
        public string WorkingAge { get; set; }

        /// <summary>
        /// 认证状态
        /// </summary>
        public string CertState { get; set; }

        
        /// <summary>
        /// 认证状态(用于显示)
        /// </summary>
        public string CertStateDisplay { get; set; }
        

    }
}