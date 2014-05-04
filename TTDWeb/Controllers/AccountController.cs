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
            SelectList list = new SelectList(items, "Value", "Text", "--请选择--");
            ViewBag.Province = list;
            ViewBag.City = new SelectList(GetRegionList("0", "-99"), "Value", "Text", "--请选择--");//传空值
            //ViewBag.County = new SelectList(GetRegionList("0", "-99"), "Value", "Text", "--请选择--");//传空值
            ViewBag.Organ = new SelectList(GetOrganList("-1"), "Value", "Text", "--请选择--");//传空值

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Reg(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                DA_Custom daCustom = new DA_Custom();
                string err = "";
                Message.CustomInfo info = new Message.CustomInfo();

                #region 信息类Info赋值
                info.NickName = "";//model.NickName;
                info.CellPhone = model.CellPhone;
                info.Pwd = model.Pwd;
                info.CustomName = model.CustomName;
                info.Email = "";//model.Email;
                info.Province = model.Province;
                info.City = model.City;
                info.OrganID = model.Organ;

                info.Address = "";
                info.Balance = 0;
                info.Birthday = Convert.ToDateTime("1900-01-01");

                info.CertApproveDate = Convert.ToDateTime("1900-01-01");
                info.CertApproveManName = "";
                info.CertGrade = "";
                info.CertNum = "";
                info.CertState = "1";
                info.CertType = "";

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
            //读取省级地区列表
            List<SelectListItem> items = GetRegionList("0", "");
            SelectList list = new SelectList(items, "Value", "Text", "--请选择--");
            ViewBag.Province = list;
            ViewBag.City = new SelectList(GetRegionList("0", "-99"), "Value", "Text", "--请选择--");//传空值
            //ViewBag.County = new SelectList(GetRegionList("0", "-99"), "Value", "Text", "--请选择--");//传空值
            ViewBag.Organ = new SelectList(GetOrganList("-1"), "Value", "Text", "--请选择--");//传空值
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

        public JsonResult GetRegionDataJson(string level, string parentid)
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
            if (Session["loginedcustom"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            Message.CustomInfo loginUser = Session["loginedcustom"] as Message.CustomInfo;
            string sql1 = "select t1.sEmail,t1.sCustomName,t1.sCertState,t1.sSex,t1.dtBirthday,t1.sCellPhone,t1.sOrganID,t1.sWorkYears, " +
                "t2.sOrganName,t2.sAddress " +
                " from t_custom t1 " +
                " left join t_foreignorgan t2 on t1.sorganid=t2.sorganid " +
                " where t1.sCustomID='" + loginUser.CustomID + "'";
            string sql2 = " select t1.sProductCode,t1.sProductName,t1.sOrganID, t1.sProductType, t1.dAnnualRate, t1.sApplyCondition, t1.sRequiredFile, t1.sMemo, t1.sDetails,t1.sRepaymentType,t1.sChars," +
                          "t1.dMoneyTop,t1.dMoneyBottom," +
                          "t1.nTermTop, t1.nTermBottom," +
                          "t1.nGetLoanDays,t1.dServerFeeOnce,t1.dServerFeeMonthly," +
                          " t2.sOrganName, t2.sLogo" +
                          " from T_Product t1" +
                          " left join T_ForeignOrgan t2 on t1.sOrganID=t2.sOrganID" +
                          " where t1.sOrganID='" + loginUser.OrganID + "'";
            string sql3 = "select t1.*," +
                "t2.aaa103 as sProductTypeName " +
                " from t_applyrecord t1 " +
                " left join aa10 t2 on t1.sProductType=t2.aaa102 and t2.aaa100='sProductType'" +
                " inner join t_product t3 on t1.sProductCode=t3.sProductCode and t3.sOrganID='" + loginUser.OrganID + "'";

            DA_Adapter da = new DA_Adapter();
            DataSet ds = new DataSet();
            string err = "";
            da.Common_Query_MultiTable(ref ds, sql1, "T_Custom", sql2, "T_Product", sql3, "T_ApplyRecord", ref err);

            DataRow drCustom = ds.Tables["T_Custom"].Rows[0];
            CustomModel m = new CustomModel();


            m.CellPhone = drCustom["sCellPhone"] is DBNull ? "" : drCustom["sCellPhone"].ToString();
            m.CustomID = loginUser.CustomID;
            m.CustomName = drCustom["sCustomName"] is DBNull ? "" : drCustom["sCustomName"].ToString();
            m.CertState = drCustom["sCertState"] is DBNull ? "" : drCustom["sCertState"].ToString();
            m.DateOfBirth = drCustom["dtBirthday"] is DBNull ? "" : Convert.ToDateTime(drCustom["dtBirthday"]).ToString("yyyy-MM-dd");
            m.Email = drCustom["sEmail"] is DBNull ? "" : drCustom["sEmail"].ToString();
            m.Occupation = "";
            m.OrganAddress = drCustom["sAddress"] is DBNull ? "" : drCustom["sAddress"].ToString();
            m.OrganID = drCustom["sOrganID"] is DBNull ? "" : drCustom["sOrganID"].ToString();
            m.OrganName = drCustom["sOrganName"] is DBNull ? "" : drCustom["sOrganName"].ToString();
            m.Sex = drCustom["sSex"] is DBNull ? "" : drCustom["sSex"].ToString();
            m.WorkingAge = drCustom["sWorkYears"] is DBNull ? "" : drCustom["sWorkYears"].ToString();


            #region 加载产品列表
            DataRow[] listCustomRows;
            ProductModel p;
            decimal dYuanMoney = 100000;
            int term = 12;
            foreach (DataRow drProduct in ds.Tables["T_Product"].Rows)
            {
                listCustomRows = ds.Tables["T_Custom"].Select("sOrganID='" + drCustom["sOrganID"].ToString() + "'");
                p = BizCommon.Convert2Product(drProduct, listCustomRows, dYuanMoney, term);
                m.ProductList.Add(p);//未分页显示。
            }

            #endregion

            #region 加载申请列表

            ApplyingRecord apply;
            foreach (DataRow drApply in ds.Tables["T_ApplyRecord"].Rows)
            {
                apply = new ApplyingRecord();
                apply.CarCustomerMonthlySalary = drApply["dCarCustomerMonthlySalary"] is DBNull ? 0 : Convert.ToDecimal(drApply["dCarCustomerMonthlySalary"]);

                apply.CarProperty = drApply["sCarProperty"] is DBNull ? "" : drApply["sCarProperty"].ToString();
                apply.CarPropertyDisplay = ToCarPropertyDisplay(apply.CarProperty);

                apply.CarPurchasingPeriod = drApply["sCarPurchasingPeriod"] is DBNull ? "" : drApply["sCarPurchasingPeriod"].ToString();
                apply.CarPurchasingPeriodDisplay = ToCarPurchasingPeriodDisplay(apply.CarPurchasingPeriod);

                apply.CaseState = drApply["sCaseState"] is DBNull ? "" : drApply["sCaseState"].ToString();
                apply.CreatTime = drApply["dtCreatTime"] is DBNull ? "" : drApply["dtCreatTime"].ToString();
                apply.CustomerEmail = drApply["sCustomerEmail"] is DBNull ? "" : drApply["sCustomerEmail"].ToString();
                apply.CustomerName = drApply["sCustomerName"] is DBNull ? "" : drApply["sCustomerName"].ToString();
                apply.CustomerPhone = drApply["sCustomerPhone"] is DBNull ? "" : drApply["sCustomerPhone"].ToString();
                apply.FirmAccountBill = drApply["dFirmAccountBill"] is DBNull ? 0 : Convert.ToDecimal(drApply["dFirmAccountBill"]);
                apply.FirmAge = drApply["sFirmAge"] is DBNull ? "" : drApply["sFirmAge"].ToString();


                apply.FirmProperty = drApply["sFirmProperty"] is DBNull ? "" : drApply["sFirmProperty"].ToString();
                apply.FirmPropertyDisplay = ToFirmPropertyDisplay(apply.FirmProperty);

                apply.FirmType = drApply["sFirmType"] is DBNull ? "" : drApply["sFirmType"].ToString();
                apply.FirmTypeDisplay = ToFirmTypeDisplay(apply.FirmType);

                apply.HouseIncome = drApply["sHouseIncome"] is DBNull ? "" : drApply["sHouseIncome"].ToString();

                apply.HouseLocalorNot = drApply["sHouseLocalorNot"] is DBNull ? "" : drApply["sHouseLocalorNot"].ToString();
                apply.HouseLocalorNotDisplay = ToHouseLocalorNotDisplay(apply.HouseLocalorNot);

                apply.HouseNew = drApply["sHouseNew"] is DBNull ? "" : drApply["sHouseNew"].ToString();
                apply.HouseNewDisplay = YesOrNo(apply.HouseNew);

                apply.HouseType = drApply["sHouseType"] is DBNull ? "" : drApply["sHouseType"].ToString();
                apply.HouseTypeDisplay = ToHouseTypeDisplay(apply.HouseType);

                apply.PerslCardNo = drApply["sPerslCardNo"] is DBNull ? "" : drApply["sPerslCardNo"].ToString();
                apply.PerslCardNoDisplay = YesOrNo(apply.PerslCardNo);

                apply.PerslCreditAllowance = drApply["sPerslCreditAllowance"] is DBNull ? "" : drApply["sPerslCreditAllowance"].ToString();
                apply.PerslCreditDue = drApply["sPerslCreditDue"] is DBNull ? "" : drApply["sPerslCreditDue"].ToString();

                apply.PerslCreditOwner = drApply["sPerslCreditOwner"] is DBNull ? "" : drApply["sPerslCreditOwner"].ToString();
                apply.PerslCreditOwnerDisplay = YesOrNo(apply.PerslCreditOwner);

                apply.PerslEmployment = drApply["sPerslEmployment"] is DBNull ? "" : drApply["sPerslEmployment"].ToString();
                apply.PerslEmploymentDisplay = ToPerslEmploymentDisplay(apply.PerslEmployment);

                apply.PerslLoan = drApply["sPerslLoan"] is DBNull ? "" : drApply["sPerslLoan"].ToString();
                apply.PerslLoanDisplay = YesOrNo(apply.PerslLoan);

                apply.PerslLoanDue = drApply["sPerslLoanDue"] is DBNull ? "" : drApply["sPerslLoanDue"].ToString();

                apply.PerslLoanSucc = drApply["sPerslLoanSucc"] is DBNull ? "" : drApply["sPerslLoanSucc"].ToString();
                apply.PerslLoanSuccDisplay = YesOrNo(apply.PerslLoanSucc);

                apply.PerslSalaryType = drApply["sPerslSalaryType"] is DBNull ? "" : drApply["sPerslSalaryType"].ToString();
                apply.PerslSalaryTypeDisplay = ToPerslSalaryTypeDisplay(apply.PerslSalaryType);

                apply.PerslWorkingAge = drApply["sPerslWorkingAge"] is DBNull ? "" : drApply["sPerslWorkingAge"].ToString();
                apply.PerslYoBirth = drApply["sPerslYoBirth"] is DBNull ? "" : drApply["sPerslYoBirth"].ToString();
                apply.ProductCode = drApply["sProductCode"] is DBNull ? "" : drApply["sProductCode"].ToString();
                apply.ProductType = drApply["sProductType"] is DBNull ? "" : drApply["sProductType"].ToString();

                m.ApplyingRecordList.Add(apply);
            }



            #endregion

            return View(m);
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
            if (Session["loginedcustom"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            Message.CustomInfo loginUser = Session["loginedcustom"] as Message.CustomInfo;
            string sql1 = "select t1.sEmail,t1.sCustomName,t1.sCertState,t1.sSex,t1.dtBirthday,t1.sCellPhone,t1.sOrganID,t1.sWorkYears, " +
                "t2.sOrganName,t2.sAddress " +
                " from t_custom t1 " +
                " left join t_foreignorgan t2 on t1.sorganid=t2.sorganid " +
                " where t1.sCustomID='" + loginUser.CustomID + "'";
            string sql2 = " select t1.sProductCode,t1.sProductName,t1.sOrganID, t1.sProductType, t1.dAnnualRate, t1.sApplyCondition, t1.sRequiredFile, t1.sMemo, t1.sDetails,t1.sRepaymentType,t1.sChars," +
                          "t1.dMoneyTop,t1.dMoneyBottom," +
                          "t1.nTermTop, t1.nTermBottom," +
                          "t1.nGetLoanDays,t1.dServerFeeOnce,t1.dServerFeeMonthly," +
                          " t2.sOrganName, t2.sLogo" +
                          " from T_Product t1" +
                          " left join T_ForeignOrgan t2 on t1.sOrganID=t2.sOrganID" +
                          " where t1.sOrganID='" + loginUser.OrganID + "'";
            string sql3 = "select t1.*," +
                "t2.aaa103 as sProductTypeName " +
                " from t_applyrecord t1 " +
                " left join aa10 t2 on t1.sProductType=t2.aaa102 and t2.aaa100='sProductType'" +
                " inner join t_product t3 on t1.sProductCode=t3.sProductCode and t3.sOrganID='" + loginUser.OrganID + "'";

            DA_Adapter da = new DA_Adapter();
            DataSet ds = new DataSet();
            string err = "";
            da.Common_Query_MultiTable(ref ds, sql1, "T_Custom", sql2, "T_Product", sql3, "T_ApplyRecord", ref err);

            DataRow drCustom = ds.Tables["T_Custom"].Rows[0];
            CustomModel m = new CustomModel();


            m.CellPhone = drCustom["sCellPhone"] is DBNull ? "" : drCustom["sCellPhone"].ToString();
            m.CustomID = loginUser.CustomID;
            m.CustomName = drCustom["sCustomName"] is DBNull ? "" : drCustom["sCustomName"].ToString();
            m.CertState = drCustom["sCertState"] is DBNull ? "" : drCustom["sCertState"].ToString();
            m.DateOfBirth = drCustom["dtBirthday"] is DBNull ? "" : Convert.ToDateTime(drCustom["dtBirthday"]).ToString("yyyy-MM-dd");
            m.Email = drCustom["sEmail"] is DBNull ? "" : drCustom["sEmail"].ToString();
            m.Occupation = "";
            m.OrganAddress = drCustom["sAddress"] is DBNull ? "" : drCustom["sAddress"].ToString();
            m.OrganID = drCustom["sOrganID"] is DBNull ? "" : drCustom["sOrganID"].ToString();
            m.OrganName = drCustom["sOrganName"] is DBNull ? "" : drCustom["sOrganName"].ToString();
            m.Sex = drCustom["sSex"] is DBNull ? "" : drCustom["sSex"].ToString();
            m.WorkingAge = drCustom["sWorkYears"] is DBNull ? "" : drCustom["sWorkYears"].ToString();


            #region 加载产品列表
            DataRow[] listCustomRows;
            ProductModel p;
            decimal dYuanMoney = 100000;
            int term = 12;
            foreach (DataRow drProduct in ds.Tables["T_Product"].Rows)
            {
                listCustomRows = ds.Tables["T_Custom"].Select("sOrganID='" + drCustom["sOrganID"].ToString() + "'");
                p = BizCommon.Convert2Product(drProduct, listCustomRows, dYuanMoney, term);
                m.ProductList.Add(p);//未分页显示。
            }

            #endregion

            #region 加载申请列表

            ApplyingRecord apply;
            foreach (DataRow drApply in ds.Tables["T_ApplyRecord"].Rows)
            {
                apply = new ApplyingRecord();
                apply.CarCustomerMonthlySalary = drApply["dCarCustomerMonthlySalary"] is DBNull ? 0 : Convert.ToDecimal(drApply["dCarCustomerMonthlySalary"]);
                apply.CarProperty = drApply["sCarProperty"] is DBNull ? "" : drApply["sCarProperty"].ToString();
                apply.CarPurchasingPeriod = drApply["sCarPurchasingPeriod"] is DBNull ? "" : drApply["sCarPurchasingPeriod"].ToString();
                apply.CaseState = drApply["sCaseState"] is DBNull ? "" : drApply["sCaseState"].ToString();
                apply.CreatTime = drApply["dtCreatTime"] is DBNull ? "" : drApply["dtCreatTime"].ToString();
                apply.CustomerEmail = drApply["sCustomerEmail"] is DBNull ? "" : drApply["sCustomerEmail"].ToString();
                apply.CustomerName = drApply["sCustomerName"] is DBNull ? "" : drApply["sCustomerName"].ToString();
                apply.CustomerPhone = drApply["sCustomerPhone"] is DBNull ? "" : drApply["sCustomerPhone"].ToString();
                apply.FirmAccountBill = drApply["dFirmAccountBill"] is DBNull ? 0 : Convert.ToDecimal(drApply["dFirmAccountBill"]);
                apply.FirmAge = drApply["sFirmAge"] is DBNull ? "" : drApply["sFirmAge"].ToString();
                apply.FirmProperty = drApply["sFirmProperty"] is DBNull ? "" : drApply["sFirmProperty"].ToString();
                apply.FirmType = drApply["sFirmType"] is DBNull ? "" : drApply["sFirmType"].ToString();
                apply.HouseIncome = drApply["sHouseIncome"] is DBNull ? "" : drApply["sHouseIncome"].ToString();
                apply.HouseLocalorNot = drApply["sHouseLocalorNot"] is DBNull ? "" : drApply["sHouseLocalorNot"].ToString();
                apply.HouseNew = drApply["sHouseNew"] is DBNull ? "" : drApply["sHouseNew"].ToString();
                apply.HouseType = drApply["sFirmType"] is DBNull ? "" : drApply["sFirmType"].ToString();
                apply.PerslCardNo = drApply["sPerslCardNo"] is DBNull ? "" : drApply["sPerslCardNo"].ToString();
                apply.PerslCreditAllowance = drApply["sPerslCreditAllowance"] is DBNull ? "" : drApply["sPerslCreditAllowance"].ToString();
                apply.PerslCreditDue = drApply["sPerslCreditDue"] is DBNull ? "" : drApply["sPerslCreditDue"].ToString();
                apply.PerslCreditOwner = drApply["sPerslCreditOwner"] is DBNull ? "" : drApply["sPerslCreditOwner"].ToString();
                apply.PerslEmployment = drApply["sPerslEmployment"] is DBNull ? "" : drApply["sPerslEmployment"].ToString();
                apply.PerslLoan = drApply["sPerslLoan"] is DBNull ? "" : drApply["sPerslLoan"].ToString();
                apply.PerslLoanDue = drApply["sPerslLoanDue"] is DBNull ? "" : drApply["sPerslLoanDue"].ToString();
                apply.PerslLoanSucc = drApply["sPerslLoanSucc"] is DBNull ? "" : drApply["sPerslLoanSucc"].ToString();
                apply.PerslSalaryType = drApply["sPerslSalaryType"] is DBNull ? "" : drApply["sPerslSalaryType"].ToString();
                apply.PerslWorkingAge = drApply["sPerslWorkingAge"] is DBNull ? "" : drApply["sPerslWorkingAge"].ToString();
                apply.PerslYoBirth = drApply["sPerslYoBirth"] is DBNull ? "" : drApply["sPerslYoBirth"].ToString();
                apply.ProductCode = drApply["sProductCode"] is DBNull ? "" : drApply["sProductCode"].ToString();
                apply.ProductType = drApply["sProductType"] is DBNull ? "" : drApply["sProductType"].ToString();

                m.ApplyingRecordList.Add(apply);
            }

            #endregion

            return View(m);
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

        #region 信贷经理·个人主页

        public ActionResult MyProducts()
        {
            if (Session["loginedcustom"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            Message.CustomInfo loginUser = Session["loginedcustom"] as Message.CustomInfo;
            string sql1 = "select t1.sEmail,t1.sCustomName,t1.sCertState,t1.sSex,t1.dtBirthday,t1.sCellPhone,t1.sOrganID,t1.sWorkYears, " +
                "t2.sOrganName,t2.sAddress " +
                " from t_custom t1 " +
                " left join t_foreignorgan t2 on t1.sorganid=t2.sorganid " +
                " where t1.sCustomID='" + loginUser.CustomID + "'";
            string sql2 = " select t1.sProductCode,t1.sProductName,t1.sOrganID, t1.sProductType, t1.dAnnualRate, t1.sApplyCondition, t1.sRequiredFile, t1.sMemo, t1.sDetails,t1.sRepaymentType,t1.sChars," +
                          "t1.dMoneyTop,t1.dMoneyBottom," +
                          "t1.nTermTop, t1.nTermBottom," +
                          "t1.nGetLoanDays,t1.dServerFeeOnce,t1.dServerFeeMonthly," +
                          " t2.sOrganName, t2.sLogo" +
                          " from T_Product t1" +
                          " left join T_ForeignOrgan t2 on t1.sOrganID=t2.sOrganID" +
                          " where t1.sOrganID='" + loginUser.OrganID + "'";
            string sql3 = "select t1.*," +
                "t2.aaa103 as sProductTypeName " +
                " from t_applyrecord t1 " +
                " left join aa10 t2 on t1.sProductType=t2.aaa102 and t2.aaa100='sProductType'" +
                " inner join t_product t3 on t1.sProductCode=t3.sProductCode and t3.sOrganID='" + loginUser.OrganID + "'";

            DA_Adapter da = new DA_Adapter();
            DataSet ds = new DataSet();
            string err = "";
            da.Common_Query_MultiTable(ref ds, sql1, "T_Custom", sql2, "T_Product", sql3, "T_ApplyRecord", ref err);

            DataRow drCustom = ds.Tables["T_Custom"].Rows[0];
            CustomModel m = new CustomModel();


            m.CellPhone = drCustom["sCellPhone"] is DBNull ? "" : drCustom["sCellPhone"].ToString();
            m.CustomID = loginUser.CustomID;
            m.CustomName = drCustom["sCustomName"] is DBNull ? "" : drCustom["sCustomName"].ToString();
            m.CertState = drCustom["sCertState"] is DBNull ? "" : drCustom["sCertState"].ToString();
            m.DateOfBirth = drCustom["dtBirthday"] is DBNull ? "" : Convert.ToDateTime(drCustom["dtBirthday"]).ToString("yyyy-MM-dd");
            m.Email = drCustom["sEmail"] is DBNull ? "" : drCustom["sEmail"].ToString();
            m.Occupation = "";
            m.OrganAddress = drCustom["sAddress"] is DBNull ? "" : drCustom["sAddress"].ToString();
            m.OrganID = drCustom["sOrganID"] is DBNull ? "" : drCustom["sOrganID"].ToString();
            m.OrganName = drCustom["sOrganName"] is DBNull ? "" : drCustom["sOrganName"].ToString();
            m.Sex = drCustom["sSex"] is DBNull ? "" : drCustom["sSex"].ToString();
            m.WorkingAge = drCustom["sWorkYears"] is DBNull ? "" : drCustom["sWorkYears"].ToString();


            #region 加载产品列表
            DataRow[] listCustomRows;
            ProductModel p;
            decimal dYuanMoney = 100000;
            int term = 12;
            foreach (DataRow drProduct in ds.Tables["T_Product"].Rows)
            {
                listCustomRows = ds.Tables["T_Custom"].Select("sOrganID='" + drCustom["sOrganID"].ToString() + "'");
                p = BizCommon.Convert2Product(drProduct, listCustomRows, dYuanMoney, term);
                m.ProductList.Add(p);//未分页显示。
            }

            #endregion

            #region 加载申请列表

            ApplyingRecord apply;
            foreach (DataRow drApply in ds.Tables["T_ApplyRecord"].Rows)
            {
                apply = new ApplyingRecord();
                apply.CarCustomerMonthlySalary = drApply["dCarCustomerMonthlySalary"] is DBNull ? 0 : Convert.ToDecimal(drApply["dCarCustomerMonthlySalary"]);
                
                apply.CarProperty = drApply["sCarProperty"] is DBNull ? "" : drApply["sCarProperty"].ToString();
                apply.CarPropertyDisplay = ToCarPropertyDisplay(apply.CarProperty);

                apply.CarPurchasingPeriod = drApply["sCarPurchasingPeriod"] is DBNull ? "" : drApply["sCarPurchasingPeriod"].ToString();
                apply.CarPurchasingPeriodDisplay = ToCarPurchasingPeriodDisplay(apply.CarPurchasingPeriod);

                apply.CaseState = drApply["sCaseState"] is DBNull ? "" : drApply["sCaseState"].ToString();
                apply.CreatTime = drApply["dtCreatTime"] is DBNull ? "" : drApply["dtCreatTime"].ToString();
                apply.CustomerEmail = drApply["sCustomerEmail"] is DBNull ? "" : drApply["sCustomerEmail"].ToString();
                apply.CustomerName = drApply["sCustomerName"] is DBNull ? "" : drApply["sCustomerName"].ToString();
                apply.CustomerPhone = drApply["sCustomerPhone"] is DBNull ? "" : drApply["sCustomerPhone"].ToString();
                apply.FirmAccountBill = drApply["dFirmAccountBill"] is DBNull ? 0 : Convert.ToDecimal(drApply["dFirmAccountBill"]);
                apply.FirmAge = drApply["sFirmAge"] is DBNull ? "" : drApply["sFirmAge"].ToString();


                apply.FirmProperty = drApply["sFirmProperty"] is DBNull ? "" : drApply["sFirmProperty"].ToString();
                apply.FirmPropertyDisplay = ToFirmPropertyDisplay(apply.FirmProperty);

                apply.FirmType = drApply["sFirmType"] is DBNull ? "" : drApply["sFirmType"].ToString();
                apply.FirmTypeDisplay = ToFirmTypeDisplay(apply.FirmType);

                apply.HouseIncome = drApply["sHouseIncome"] is DBNull ? "" : drApply["sHouseIncome"].ToString();
                
                apply.HouseLocalorNot = drApply["sHouseLocalorNot"] is DBNull ? "" : drApply["sHouseLocalorNot"].ToString();
                apply.HouseLocalorNotDisplay = ToHouseLocalorNotDisplay(apply.HouseLocalorNot);

                apply.HouseNew = drApply["sHouseNew"] is DBNull ? "" : drApply["sHouseNew"].ToString();
                apply.HouseNewDisplay = YesOrNo(apply.HouseNew);

                apply.HouseType = drApply["sFirmType"] is DBNull ? "" : drApply["sFirmType"].ToString();
                apply.HouseTypeDisplay = ToHouseTypeDisplay(apply.HouseType);

                apply.PerslCardNo = drApply["sPerslCardNo"] is DBNull ? "" : drApply["sPerslCardNo"].ToString();
                apply.PerslCardNoDisplay = YesOrNo(apply.PerslCardNo);

                apply.PerslCreditAllowance = drApply["sPerslCreditAllowance"] is DBNull ? "" : drApply["sPerslCreditAllowance"].ToString();
                apply.PerslCreditDue = drApply["sPerslCreditDue"] is DBNull ? "" : drApply["sPerslCreditDue"].ToString();

                apply.PerslCreditOwner = drApply["sPerslCreditOwner"] is DBNull ? "" : drApply["sPerslCreditOwner"].ToString();
                apply.PerslCreditOwnerDisplay = YesOrNo(apply.PerslCreditOwner);

                apply.PerslEmployment = drApply["sPerslEmployment"] is DBNull ? "" : drApply["sPerslEmployment"].ToString();
                apply.PerslEmploymentDisplay = ToPerslEmploymentDisplay(apply.PerslEmployment);

                apply.PerslLoan = drApply["sPerslLoan"] is DBNull ? "" : drApply["sPerslLoan"].ToString();
                apply.PerslLoanDisplay = YesOrNo(apply.PerslLoan);

                apply.PerslLoanDue = drApply["sPerslLoanDue"] is DBNull ? "" : drApply["sPerslLoanDue"].ToString();

                apply.PerslLoanSucc = drApply["sPerslLoanSucc"] is DBNull ? "" : drApply["sPerslLoanSucc"].ToString();
                apply.PerslLoanSuccDisplay = YesOrNo(apply.PerslLoanSucc);

                apply.PerslSalaryType = drApply["sPerslSalaryType"] is DBNull ? "" : drApply["sPerslSalaryType"].ToString();
                apply.PerslSalaryTypeDisplay = ToPerslSalaryTypeDisplay(apply.PerslSalaryType);

                apply.PerslWorkingAge = drApply["sPerslWorkingAge"] is DBNull ? "" : drApply["sPerslWorkingAge"].ToString();
                apply.PerslYoBirth = drApply["sPerslYoBirth"] is DBNull ? "" : drApply["sPerslYoBirth"].ToString();
                apply.ProductCode = drApply["sProductCode"] is DBNull ? "" : drApply["sProductCode"].ToString();
                apply.ProductType = drApply["sProductType"] is DBNull ? "" : drApply["sProductType"].ToString();

                m.ApplyingRecordList.Add(apply);
            }



            #endregion

            return View(m);
        }
        #endregion

        #region 申请显示转化
        string ToPerslSalaryTypeDisplay(string any)
        {
            string s = "";
            switch (any)
            {
                case "0":
                    s = "发到工资卡";
                    break;
                case "1":
                    s = "现金领取";
                    break;
            }
            return s;
        }

        string ToPerslEmploymentDisplay(string P)
        {
            string s = "";
            switch (P)
            {
                case "0":
                    s = "无固定职业";
                    break;
                case "1":
                    s = "私营企业";
                    break;
                case "2":
                    s = "公务员/事业单位";
                    break;
                case "3":
                    s = "国企/上市企业";
                    break;
            }
            return s;
        }

        string ToFirmPropertyDisplay(string Property)
        {
            string s = "";
            switch (Property)
            {
                case "1":
                    s = "没有本地房产";
                    break;
                case "2":
                    s = "有（有房产证）";
                    break;              
            }
            return s;
        }

        string ToFirmTypeDisplay(string FirmTypeDisplay)
        {
            string s = "";
            switch (FirmTypeDisplay)
            {
                case "1":
                    s = "个体工商户";
                    break;
                case "2":
                    s = "私营企业";
                    break;
                case "3":
                    s = "公务员/事业单位";
                    break;
                case "4":
                    s = "国企";
                    break;
                case "5":
                    s = "世界500强企业";
                    break;
                case "6":
                    s = "上市企业";
                    break;
                case "7":
                    s = "普通企业";
                    break;
                case "8":
                    s = "无固定职业";
                    break;
            }
            return s;
        }

        string ToCarPropertyDisplay(string CarProperty)
        {
            string s = "";
            switch (CarProperty)
            {
                case "1":
                    s = "没有本地商品房";
                    break;
                case "2":
                    s = "名下有房";
                    break;
                case "3":
                    s = "父母、配偶名下有房";
                    break;
            }
            return s;
        }
        
        string ToCarPurchasingPeriodDisplay(string CarPurchasingPeriod)
        {
            string s = "";
            switch (CarPurchasingPeriod)
            {
                case "1":
                    s = "未选好车型";
                    break;
                case "2":
                    s = "准备去4S店看车";
                    break;
                case "3":
                    s = "已经选好车型";
                    break;
            }
            return s;
        }

        string ToHouseTypeDisplay(string HouseType)
        {
            string s = "";
            switch (HouseType)
            {
                case "0":
                    s = "商品房";
                    break;
                case "1":
                    s = "商铺";
                    break;
                case "2":
                    s = "写字楼";
                    break;
                case "3":
                    s = "房改房";
                    break;
                case "4":
                    s = "经济适用房";
                    break;
            }
            return s;
        }

        string ToHouseLocalorNotDisplay(string any)
        {
            string s = "";
            switch (any)
            {
                case "0":
                    s = "本地户口";
                    break;
                case "1":
                    s = "外地户口";
                    break;
            }
            return s;
        }

        string YesOrNo(string any)
        {
            string s = "";
            switch (any)
            {
                case "0":
                    s = "是";
                    break;
                case "1":
                    s = "否";
                    break;
            }
            return s;
        }
        #endregion

        
    }
}

