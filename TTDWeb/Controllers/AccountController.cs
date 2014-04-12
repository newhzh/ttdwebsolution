using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("loginusername"))
            {
                string lastUserName = this.ControllerContext.HttpContext.Request.Cookies["loginusername"].Value;

                LoginModel m = new LoginModel();
                m.UserName = lastUserName;
                m.RememberMe = true;

                return View(m);
            }
            else
            {
                return View();
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl, FormCollection values)
        {
            if (ModelState.IsValid)
            {
                SqlServerDAL.DA_Custom daCustom = new SqlServerDAL.DA_Custom();
                string err = "";

                Message.CustomInfo custom = daCustom.LoginByPhone(model.UserName, model.Password, ref err);
                if (custom != null)
                {
                    if (model.RememberMe)
                    {
                        HttpCookie cookie = new HttpCookie("loginusername");
                        cookie.Value = model.UserName;
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                    }
                    Session.Add("loginedcustom", custom);

                    //记录最近登录时间
                    daCustom.UpdateCustomLastLoginTime(custom.CustomID);

                    FormsAuthentication.SetAuthCookie(custom.CustomName, false);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("MyProducts"); //进入个人首页
                    }
                }
                else
                {
                    if (err != "")
                    {
                        //err不为空，说明用户名密码正确，但未认证
                        ModelState.AddModelError("", err);
                    }
                    else
                    {
                        ModelState.AddModelError("", "用户名密码错误，登录失败！");
                    }
                }
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Remove("loginedcustom");

            return RedirectToAction("Index", "Home");
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

        #region 信贷经理·我收到的申请·订单显示

        public ActionResult Order()
        {
            return View();
        }

        #endregion

        #region 信贷经理·我收到的申请·订单编辑

        public ActionResult OrderEdit()
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

        #region 信贷经理·个人资料编辑·个人基本信息

        public ActionResult MyInfoEdit_Basic()
        {
            return View();
        }

        #endregion

        #region 信贷经理·个人资料编辑·身份认证

        public ActionResult MyInfoEdit_Identify()
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
