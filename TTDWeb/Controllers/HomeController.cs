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

        #region 产品列表  hahahhaha
        public ActionResult ProductList()
        {
            return View();

        }
        
        #endregion

        public ActionResult Detail(string productid)
        {
            return View();
        }
    }
}
