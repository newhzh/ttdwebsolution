using SqlServerDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TTDWeb.Common;
using TTDWeb.Models;

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

        [AllowAnonymous]
        public ActionResult Reg()
        {
            //读取省级地区列表
            List<SelectListItem> items = GetRegionList("0", "");
            SelectList list = new SelectList(items,"Value","Text","--请选择--");
            ViewBag.Province = list;
            ViewBag.City = new SelectList(GetRegionList("0", "-99"), "Value", "Text", "--请选择--");//传空值
            ViewBag.County = new SelectList(GetRegionList("0", "-99"), "Value", "Text", "--请选择--");//传空值
            ViewBag.Organ = new SelectList(GetOrganList("-1"), "Value", "Text", "--请选择--");//传空值

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                DA_Custom daCustom = new DA_Custom();
                string err = "";
                Message.CustomInfo info = new Message.CustomInfo();

                #region 信息类Info赋值
                info.NickName = model.NickName;
                info.CellPhone = model.CellPhone;
                info.Pwd = model.Password;
                info.CustomName = model.CustomName;
                info.Email = model.Email;

                info.Address = "";
                info.Balance = 0;
                info.Birthday = Convert.ToDateTime("1900-01-01");

                info.CertApproveDate = Convert.ToDateTime("1900-01-01");
                info.CertApproveManName = "";
                info.CertGrade = "";
                info.CertNum = "";
                info.CertState = "0";
                info.CertType = "";
                info.City = "";
                info.CreditGradeBorrow = "01";//默认初级
                info.CreditGradeInvest = "01";//默认初级
                info.CreditScoreBorrow = 0;
                info.CreditScoreInvest = 0;
                info.CustomCode = "";
                info.CustomID = "";

                info.CustomState = "1";
                info.CustomType = "0";
                info.Fax = "";
                info.FileNameCopy1 = "";
                info.FileNameCopy2 = "";
                info.FileNameCopy3 = "";
                info.FileNameCopy4 = "";
                info.LastLoginDate = DateTime.Today;    //后台不会用到此值

                info.LinkManName = "";
                info.LinkManPhone = "";
                info.Memo = "";

                info.PC = "";
                info.PhotoFileName = "";
                info.Points = 0;
                info.Province = "";

                info.PYM = "";
                info.RegDate = DateTime.Today;
                info.RegWay = "0";  //0表示网站注册
                info.ServerStaffID = "";
                info.Sex = "";
                info.Tel = "";
                info.OutletDptID = "";
                #endregion

                if (daCustom.Insert(ref info, ref err, null))
                {
                    //注册成功，转入提示页面
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", err);
                }
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(model);
        }

        #region 验证手机号码是否可用
        public void CheckCellPhone(string CellPhone)
        {
            DA_Common da = new DA_Common();
            string sql = "select sCustomID from t_custom where sCellPhone='" + CellPhone + "'";
            DataSet ds = da.CommonQuery(sql);
            if (BizCommon.IsNullDataSet(ds))
            {
                Response.ContentType = "text/html";
                Response.Write("恭喜，此手机可以注册!");
            }
            else
            {
                Response.ContentType = "text/html";
                Response.Write("此手机已经存在，请换一个!");
            }
        }
        #endregion

        #region 验证邮箱是否可用
        public void CheckEmail(string Email)
        {
            DA_Common da = new DA_Common();
            string sql = "select sCustomID from t_custom where sEmail='" + Email + "'";
            DataSet ds = da.CommonQuery(sql);
            if (BizCommon.IsNullDataSet(ds))
            {
                Response.ContentType = "text/html";
                Response.Write("恭喜，此邮箱可以注册!");
            }
            else
            {
                Response.ContentType = "text/html";
                Response.Write("此邮箱已经存在，请换一个!");
            }
        }
        #endregion

        #region 验证昵称是否可用
        public void CheckNickName(string NickName)
        {
            DA_Common da = new DA_Common();
            string sql = "select sCustomID from t_custom where sNickName='" + NickName + "'";
            DataSet ds = da.CommonQuery(sql);
            if (BizCommon.IsNullDataSet(ds))
            {
                Response.ContentType = "text/html";
                Response.Write("恭喜，此昵称可以注册!");
            }
            else
            {
                Response.ContentType = "text/html";
                Response.Write("此昵称已经存在，请换一个!");
            }
        }
        #endregion

        #region 读取地区数据

        List<SelectListItem> GetRegionList(string level, string parentid)
        {
            string sql = "select sRegionID,sRegionName,sParentID from t_region where nLevel=" + level;
            if (parentid != "")
                sql += " and sParentID='" + parentid + "'";

            if (Convert.ToInt32(level) > 0 && parentid == "")
                sql += " and 1=2";

            DA_Common da = new DA_Common();
            DataSet ds = da.CommonQuery(sql);

            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "--请选择--", Value = "" });

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                items.Add(new SelectListItem { Text = dr["sRegionName"].ToString(), Value = dr["sRegionID"].ToString() });
            }
            return items;
        }

        public JsonResult GetRegionDataJson(string level,string parentid)
        {
            List<SelectListItem> items = GetRegionList(level, parentid);
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 读取机构数据
        List<SelectListItem> GetOrganList(string regionid)
        {
            string sql = "select sOrganID,sOrganName from t_foreignorgan where sregionid='" + regionid + "'";
            if (regionid == "")
                sql += " and 1=2";

            DA_Common da = new DA_Common();
            DataSet ds = da.CommonQuery(sql);

            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "--请选择--", Value = "" });

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                items.Add(new SelectListItem { Text = dr["sOrganName"].ToString(), Value = dr["sOrganID"].ToString() });
            }
            return items;
        }

        public JsonResult GetOrganDataJson(string regionid)
        {
            List<SelectListItem> items = GetOrganList(regionid);
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        #endregion

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
