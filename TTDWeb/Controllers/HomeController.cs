using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TTDWeb.Controllers
{
    public class HomeController : Controller
    {
        #region 首页
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 产品列表 
        public ActionResult ProductList(string type,string money,string term)
        {
            string sql = "";
            
            
            return View();

        }
        
        #endregion

        #region 产品详情页
        public ActionResult Detail(string productid)
        {
            return View();
        }
        #endregion

        #region 车贷申请

        #endregion

    }
}
