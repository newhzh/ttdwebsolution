using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TTDWeb.Controllers
{
    public class AccountController : Controller
    {
        #region 金融机构·首页

        public ActionResult OrganIndex()
        {
            return View();
        }

        #endregion

        #region 信贷经理·注册

        public ActionResult Reg()
        {
            return View();
        }

        #endregion

        #region 信贷经理·登录

        public ActionResult Login()
        {
            return View();
        }

        #endregion

        #region 信贷经理·首页

        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region 信贷经理·我收到的申请

        public ActionResult MyApplition()
        {
            return View();
        }

        #endregion

        #region 信贷经理·个人资料

        public ActionResult MyInfo()
        {
            return View();
        }

        #endregion

        #region 信贷经理·发布产品

        public ActionResult CreateProduct()
        {
            return View();
        }

        #endregion

        #region 信贷经理·我发布的产品

        public ActionResult MyProducts()
        {
            return View();
        }

        #endregion
    }
}
