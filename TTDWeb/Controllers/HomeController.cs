using SqlServerDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TTDWeb.Common;
using TTDWeb.Models;

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
        public ActionResult ProductList(string type,string money,string term1,string term,string pindex)
        {
            //前端页要用这三个参数
            ViewBag.type=type;
            ViewBag.money = money;
            ViewBag.term = term;
            ViewBag.term1 = term1;

            decimal dYuanMoney = Convert.ToDecimal(money) * 10000;
            DA_Adapter da = new DA_Adapter();

            string sql1 = " select t1.sProductName,t1.sOrganID, t1.sProductType, t1.dAnnualRate, t1.sApplyCondition, t1.sRequiredFile, t1.sMemo, t1.sDetails,t1.sRepaymentType," + 
                          " t2.sOrganName, t2.sLogo" +
                          " from T_Product t1" +
                          " left join T_ForeignOrgan t2 on t1.sOrganID=t2.sOrganID"+
                          " where t1.sProductType='" + type + "' and t1.dMoneyBottom<=" + dYuanMoney.ToString() + 
                          " and t1.dMoneyTop>=" + dYuanMoney.ToString() +
                          " and t1.nTermBottom<="+ term +" and t1.nTermTop>="+ term;

            string sql2 = " select t1.sCustomName, t1.sOrganID, t2.sOrganName" +
                          " from T_Custom t1" +
                          " left join T_ForeignOrgan t2 on t1.sOrganID=t2.sOrganID";

            DataSet ds = new DataSet();
            string err = "";
            da.Common_Query_MultiTable(ref ds, sql1, "T_Product", sql2, "T_Custom", ref err);

            List<ProductModel> products = new List<ProductModel>();
            ProductModel p;
            DataRow[] listCustomRows;

            foreach (DataRow dr in ds.Tables["T_Product"].Rows)
            {
                listCustomRows=ds.Tables["T_Custom"].Select("sOrganID='" + dr["sOrganID"].ToString() + "'");
                p = Convert2Product(dr, listCustomRows, dYuanMoney, Convert.ToInt32(term));
                products.Add(p);//未分页显示。
            }
            //总页数（每页显示10条）
            int countPerPage = 3;
            int pageCount = products.Count / countPerPage + (products.Count % countPerPage > 0 ? 1 : 0);
            int pageIndex = (pindex == null || pindex == "") ? 1 : Convert.ToInt32(pindex);
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex;

            ViewBag.PageCount = pageCount;
            ViewBag.PageIndex = pageIndex;

            var productsShow = new List<ProductModel>();
            int start = pageIndex < 1 ? 0 : ((pageIndex - 1) * countPerPage);
            int end = (pageIndex * countPerPage) > products.Count ? products.Count : (pageIndex * countPerPage);
            for (int i = start; i < end; i++)
            {
                productsShow.Add(products[i]);
            }
            return View(productsShow);            
        }


        ProductModel Convert2Product(DataRow dr,DataRow[] listCustomRows,decimal money,int term)
        {
            ProductModel p;
            p = new ProductModel();
            p.ProductType = dr["sProductType"].ToString();
            p.ProductName = dr["sProductName"].ToString();
            p.OrganName = dr["sOrganName"].ToString();
            p.AnnualRate = Convert.ToDecimal(dr["dAnnualRate"]);
            p.AnnualRateDisplay = (p.AnnualRate * 100).ToString("F1") + "%";
            p.ApplyCondition = dr["sApplyCondition"].ToString();
            p.RequiredFile = dr["sRequiredFile"].ToString();
            p.Memo = dr["sMemo"].ToString();
            p.Details = dr["sDetails"].ToString();
            p.RepaymentType = dr["sRepaymentType"].ToString();
            p.RepaymentMonthly = CalcRepaymentMonthly(p.RepaymentType, p.AnnualRate, money, term).ToString("F2");   //每月偿还金额
            
            CustomModel c;
            foreach (DataRow drCustom in listCustomRows)
            {
                c = new CustomModel();
                c.CustomName = drCustom["sCustomName"].ToString();
                c.OrganName = drCustom["sOrganName"].ToString();
                p.Customs.Add(c);              
            }            
            return p;
        }

        decimal CalcRepaymentMonthly(string repaymentType, decimal annualRate, decimal money, int term)
        {
            decimal sumMonth = 0, moneyCap = 0, moneyAcc = 0, moneyBalance = 0;
            decimal monthlyRate = annualRate/12;
            switch (repaymentType)
            {
                case "01":  //月利清本
                    Message.FunctionCommon.BorrowRateCalc_03(false, money, money, monthlyRate, term, 
                                                             ref sumMonth, ref moneyCap, ref moneyAcc, ref moneyBalance);
                    break;
                case "02":  //等额本息
                    Message.FunctionCommon.BorrowRateCalc_01(money, money, monthlyRate, term,
                                                            ref sumMonth, ref moneyCap, ref moneyAcc, ref moneyBalance);
                    break;
                default:
                    break;
            }

            return sumMonth;
        }

        #endregion

        #region 车贷申请

         #region 第一步
         public ActionResult Carloan1(string productcode,string producttype) 
         {
             ViewBag.productcode = productcode;
             ViewBag.producttype = producttype;

             //第一步中的下拉选项预加载
             ViewBag.CarProperty = BizCommon.GetAA10Items("sCarProperty", "cast(aaa102 as int)");          //车贷-房产 下拉选项
             ViewBag.CarPurchasingPeriod = BizCommon.GetAA10Items("sCarPurchasingPeriod", "cast(aaa102 as int)");  //车贷-购车阶段 下拉选项

             if (Session[BizCommon.g_SessionName_ApplyProject] != null)
             {
                 //为了防止已填写数据丢失，此处将Session中的内容取出填入
                 ApplyingRecord p = (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord);
                 CarLoanStep1 m = new CarLoanStep1();

                 m.CarCustomerMonthlySalary = p.CarCustomerMonthlySalary;
                 m.CarProperty = p.CarProperty;
                 m.CarPurchasingPeriod = p.CarPurchasingPeriod;

                 return View(m);
             }
             else
             {
                 return View();
             }        
         }

         [HttpPost]
         public ActionResult Carloan1(CarLoanStep1 c, FormCollection values)
         {
             if (ModelState.IsValid)
             {
                 string productcode = values["productcode"].ToString();
                 string producttype = values["producttype"].ToString();

                 if (Session[BizCommon.g_SessionName_ApplyProject] != null)
                 {
                     //若session中已经存在申请对象，则将本步骤所取得的值赋到session中的对象上；
                     (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord).CarCustomerMonthlySalary = c.CarCustomerMonthlySalary;
                     (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord).CarProperty = c.CarProperty;
                     (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord).CarPurchasingPeriod = c.CarPurchasingPeriod;

                     (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord).ProductCode = productcode;
                     (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord).ProductType = producttype;

                     return View("Carloan2");
                 }
                 else
                 {
                     //否则，新创建一个申请对象，并将本步骤取得值赋到新的对象上，然后将对象放到session中；
                     ApplyingRecord p = new ApplyingRecord();
                     p.CarCustomerMonthlySalary = c.CarCustomerMonthlySalary;
                     p.CarProperty = c.CarProperty;
                     p.CarPurchasingPeriod = c.CarPurchasingPeriod;
                     p.ProductCode = productcode;
                     p.ProductType = producttype;

                     //第一步创建project类放到session中
                     if (Session[BizCommon.g_SessionName_ApplyProject] != null)
                         Session[BizCommon.g_SessionName_ApplyProject] = null;

                     Session[BizCommon.g_SessionName_ApplyProject] = p;

                     return View("Carloan2");
                 }
             }

             //万一发生异常时，将执行以下代码（即返回第一步页面）
             //第一步中的下拉选项预加载
             ViewBag.CarProperty = BizCommon.GetAA10Items("sCarProperty", "cast(aaa102 as int)");          //车贷-房产 下拉选项
             ViewBag.CarPurchasingPeriod = BizCommon.GetAA10Items("sCarPurchasingPeriod", "cast(aaa102 as int)");  //车贷-购车阶段 下拉选项
             return View("Carloan1");
         }
        #endregion

         #region 第二步
         public ActionResult Carloan2()
         {
             return View();
         }

         [HttpPost]
         public ActionResult Carloan2(CarLoanStep2 c)
         {
             if (ModelState.IsValid)
             {
                 if (Session[BizCommon.g_SessionName_ApplyProject] == null)    //若Session为空，则返回第一步（这是有可能的，长时间不操作）
                     return View("Carloan1");

                 ApplyingRecord p = Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord;

                 #region 完整的类赋值
                 p.CustomerEmail = c.CustomerEmail;
                 p.CustomerName = c.CustomerName;
                 p.CustomerPhone = c.CustomerPhone;

                 p.CaseState = "0";
                 p.CreatTime = DateTime.Today.ToString("yyyy-MM-dd");

                 p.FirmAccountBill = 0;
                 p.FirmAge = "";
                 p.FirmProperty = "";
                 p.FirmType = "";

                 p.HouseIncome = "";
                 p.HouseLocalorNot = "";
                 p.HouseNew = "";
                 p.HouseType = "";

                 p.IPaddress = BizCommon.GetIP(this);
                 
                 p.PerslCardNo = "";
                 p.PerslCreditAllowance = "";
                 p.PerslCreditDue = "";
                 p.PerslCreditOwner = "";
                 p.PerslEmployment = "";
                 p.PerslLoan = "";
                 p.PerslLoanDue = "";
                 p.PerslSalaryType = "";
                 p.PerslWorkingAge = "";
                 p.PerslYoBirth = "";

                 //此处无需针对以下两个属性赋值，在第一步的时候已经完成赋值
                 //p.ProductCode = productcode;
                 //p.ProductType = producttype;

                 #endregion

                 #region 保存至数据库,并跳转到第三步

                 string err = "";
                 if (DataAdapter.Apply_Insert(p, ref err))
                 {
                     return View("Carloan3");
                 }
                 else
                 {
                     //保存失败
                     ModelState.AddModelError("", "：（ 保存失败了！");
                     return View();
                 }

                 #endregion
             }
             else
             {
                 return View();
             }
         }
         #endregion

        #region 第三步（成功提示）
         public ActionResult Carloan3()
         {
             return View();
         }
        #endregion

        #endregion

        #region 房贷申请

         #region 第一步
         public ActionResult Homeloan1(string productcode, string producttype)
         {
             ViewBag.productcode = productcode;
             ViewBag.producttype = producttype;

             //第一步中的下拉选项预加载
             ViewBag.HouseLocalorNot = BizCommon.GetAA10Items("sHouseLocalorNot", "cast(aaa102 as int)");
             ViewBag.HouseNew = BizCommon.GetAA10Items("sHouseNew", "cast(aaa102 as int)");
             ViewBag.HouseType = BizCommon.GetAA10Items("sHouseType", "cast(aaa102 as int)");

             if (Session[BizCommon.g_SessionName_ApplyProject] != null)
             {
                 //为了防止已填写数据丢失，此处将Session中的内容取出填入
                 ApplyingRecord p = (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord);
                 HomeLoanStep1 m = new HomeLoanStep1();

                 m.HouseIncome = p.HouseIncome;         //输入
                 m.HouseLocalorNot = p.HouseLocalorNot; //选项
                 m.HouseNew = p.HouseNew;               //选项
                 m.HouseType = p.HouseType;             //选项

                 return View(m);
             }
             else
             {
                 return View();
             }        
         }

         [HttpPost]
         public ActionResult Homeloan1(HomeLoanStep1 c, FormCollection values)
         {
             if (ModelState.IsValid)
             {
                 string productcode = values["productcode"].ToString();
                 string producttype = values["producttype"].ToString();

                 if (Session[BizCommon.g_SessionName_ApplyProject] != null)
                 {
                     //若session中已经存在申请对象，则将本步骤所取得的值赋到session中的对象上；
                     (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord).HouseIncome = c.HouseIncome;
                     (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord).HouseLocalorNot = c.HouseLocalorNot;
                     (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord).HouseNew = c.HouseNew;
                     (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord).HouseType = c.HouseType;

                     (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord).ProductCode = productcode;
                     (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord).ProductType = producttype;

                     return View("Homeloan2");
                 }
                 else
                 {
                     //否则，新创建一个申请对象，并将本步骤取得值赋到新的对象上，然后将对象放到session中；
                     ApplyingRecord p = new ApplyingRecord();
                     p.HouseIncome = c.HouseIncome;
                     p.HouseLocalorNot = c.HouseLocalorNot;
                     p.HouseNew = c.HouseNew;
                     p.HouseType = c.HouseType;
                     p.ProductCode = productcode;
                     p.ProductType = producttype;

                     //第一步创建project类放到session中
                     if (Session[BizCommon.g_SessionName_ApplyProject] != null)
                         Session[BizCommon.g_SessionName_ApplyProject] = null;

                     Session[BizCommon.g_SessionName_ApplyProject] = p;

                     return View("Homeloan2");
                 }
             }


             //万一发生异常时，将执行以下代码（即返回第一步页面）
             //第一步中的下拉选项预加载
             ViewBag.HouseLocalorNot = BizCommon.GetAA10Items("sHouseLocalorNot", "cast(aaa102 as int)");
             ViewBag.HouseNew = BizCommon.GetAA10Items("sHouseNew", "cast(aaa102 as int)");
             ViewBag.HouseType = BizCommon.GetAA10Items("sHouseType", "cast(aaa102 as int)");
             return View("Homeloan1");
         }
         #endregion

         #region 第二步
         public ActionResult Homeloan2()
         {
             return View();
         }

         [HttpPost]
         public ActionResult Homeloan2(HomeLoanStep2 c)
         {
             if (ModelState.IsValid)
             {
                 if (Session[BizCommon.g_SessionName_ApplyProject] == null)    //若Session为空，则返回第一步（这是有可能的，长时间不操作）
                     return View("Homeloan1");

                 ApplyingRecord p = Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord;

                 #region 完整的类赋值
                 p.CarCustomerMonthlySalary = 0;
                 p.CarProperty = "";
                 p.CarPurchasingPeriod = ""; 
                
                 p.CustomerEmail = c.CustomerEmail;
                 p.CustomerName = c.CustomerName;
                 p.CustomerPhone = c.CustomerPhone;

                 p.CaseState = "0";
                 p.CreatTime = DateTime.Today.ToString("yyyy-MM-dd");

                 p.FirmAccountBill = 0;
                 p.FirmAge = "";
                 p.FirmProperty = "";
                 p.FirmType = "";

                 //以下几个属性在第一步中已经完成输入
                 //p.HouseIncome = "";
                 //p.HouseLocalorNot = "";
                 //p.HouseNew = "";
                 //p.HouseType = "";

                 p.IPaddress = BizCommon.GetIP(this);

                 p.PerslCardNo = "";
                 p.PerslCreditAllowance = "";
                 p.PerslCreditDue = "";
                 p.PerslCreditOwner = "";
                 p.PerslEmployment = "";
                 p.PerslLoan = "";
                 p.PerslLoanDue = "";
                 p.PerslSalaryType = "";
                 p.PerslWorkingAge = "";
                 p.PerslYoBirth = "";

                 //此处无需针对以下两个属性赋值，在第一步的时候已经完成赋值
                 //p.ProductCode = productcode;
                 //p.ProductType = producttype;

                 #endregion

                 #region 保存至数据库,并跳转到第三步

                 string err = "";
                 if (DataAdapter.Apply_Insert(p, ref err))
                 {
                     return View("Homeloan3");
                 }
                 else
                 {
                     //保存失败
                     ModelState.AddModelError("", "：（ 保存失败了！");
                     return View();
                 }

                 #endregion
             }
             else
             {
                 return View();
             }
         }
         #endregion

         #region 第三步
         public ActionResult Homeloan3()
         {
             return View();
         }
         #endregion

        #endregion

        #region 企业贷申请

        #region 第一步
         public ActionResult Firmloan1(string productcode, string producttype)
        {
            ViewBag.productcode = productcode;
            ViewBag.producttype = producttype;

            //第一步中的下拉选项预加载
            ViewBag.FirmAge = BizCommon.GetAA10Items("sFirmAge", "cast(aaa102 as int)");
            ViewBag.FirmProperty = BizCommon.GetAA10Items("sFirmProperty", "cast(aaa102 as int)");
            ViewBag.FirmType = BizCommon.GetAA10Items("sFirmType", "cast(aaa102 as int)");

            if (Session[BizCommon.g_SessionName_ApplyProject] != null)
            {
                //为了防止已填写数据丢失，此处将Session中的内容取出填入
                ApplyingRecord p = (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord);
                FirmLoanStep1 m = new FirmLoanStep1();

                m.FirmAccountBill = p.FirmAccountBill;         //输入
                m.FirmAge = p.FirmAge;                          //选项
                m.FirmProperty = p.FirmProperty;               //选项
                m.FirmType = p.FirmType;                         //选项

                return View(m);
            }
            else
            {
                return View();
            }        
        }

         [HttpPost]
         public ActionResult Firmloan1(FirmLoanStep1 c, FormCollection values)
         {
             if (ModelState.IsValid)
             {
                 string productcode = values["productcode"].ToString();
                 string producttype = values["producttype"].ToString();

                 if (Session[BizCommon.g_SessionName_ApplyProject] != null)
                 {
                     //若session中已经存在申请对象，则将本步骤所取得的值赋到session中的对象上；
                     (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord).FirmAccountBill = c.FirmAccountBill;
                     (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord).FirmAge = c.FirmAge;
                     (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord).FirmProperty = c.FirmProperty;
                     (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord).FirmType = c.FirmType;

                     (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord).ProductCode = productcode;
                     (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord).ProductType = producttype;

                     return View("Firmloan2");
                 }
                 else
                 {
                     //否则，新创建一个申请对象，并将本步骤取得值赋到新的对象上，然后将对象放到session中；
                     ApplyingRecord p = new ApplyingRecord();
                     p.FirmAccountBill = c.FirmAccountBill;
                     p.FirmAge = c.FirmAge;
                     p.FirmProperty = c.FirmProperty;
                     p.FirmType = c.FirmType;
                     p.ProductCode = productcode;
                     p.ProductType = producttype;

                     //第一步创建project类放到session中
                     if (Session[BizCommon.g_SessionName_ApplyProject] != null)
                         Session[BizCommon.g_SessionName_ApplyProject] = null;

                     Session[BizCommon.g_SessionName_ApplyProject] = p;

                     return View("Firmloan2");
                 }
             }


             //万一发生异常时，将执行以下代码（即返回第一步页面）
             //第一步中的下拉选项预加载
             ViewBag.FirmAge = BizCommon.GetAA10Items("sFirmAge", "cast(aaa102 as int)");
             ViewBag.FirmProperty = BizCommon.GetAA10Items("sFirmProperty", "cast(aaa102 as int)");
             ViewBag.FirmType = BizCommon.GetAA10Items("sFirmType", "cast(aaa102 as int)");
             return View("Firmloan1");
         }
        #endregion

        #region 第二步
        public ActionResult Firmloan2()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Firmloan2(FirmLoanStep2 c)
        {
            if (ModelState.IsValid)
            {
                if (Session[BizCommon.g_SessionName_ApplyProject] == null)    //若Session为空，则返回第一步（这是有可能的，长时间不操作）
                    return View("Firmloan1");

                ApplyingRecord p = Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord;

                #region 完整的类赋值
                p.CarCustomerMonthlySalary = 0;
                p.CarProperty = "";
                p.CarPurchasingPeriod = "";
                
                p.CustomerEmail = c.CustomerEmail;
                p.CustomerName = c.CustomerName;
                p.CustomerPhone = c.CustomerPhone;

                p.CaseState = "0";
                p.CreatTime = DateTime.Today.ToString("yyyy-MM-dd");

                //以下几个属性在第一步中已经完成输入
                //p.FirmAccountBill = 0;
                //p.FirmAge = "";
                //p.FirmProperty = "";
                //p.FirmType = "";


                p.HouseIncome = "";
                p.HouseLocalorNot = "";
                p.HouseNew = "";
                p.HouseType = "";

                p.IPaddress = BizCommon.GetIP(this);

                p.PerslCardNo = "";
                p.PerslCreditAllowance = "";
                p.PerslCreditDue = "";
                p.PerslCreditOwner = "";
                p.PerslEmployment = "";
                p.PerslLoan = "";
                p.PerslLoanDue = "";
                p.PerslSalaryType = "";
                p.PerslWorkingAge = "";
                p.PerslYoBirth = "";

                //此处无需针对以下两个属性赋值，在第一步的时候已经完成赋值
                //p.ProductCode = productcode;
                //p.ProductType = producttype;

                #endregion

                #region 保存至数据库,并跳转到第三步

                string err = "";
                if (DataAdapter.Apply_Insert(p, ref err))
                {
                    return View("Firmloan3");
                }
                else
                {
                    //保存失败
                    ModelState.AddModelError("", "：（ 保存失败了！");
                    return View();
                }

                #endregion
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region 第三步
        public ActionResult Firmloan3()
        {
            return View();
        }
        #endregion

        #endregion

        #region 消费贷申请

        #region 第一步
        public ActionResult Purchaseloan1(string productcode, string producttype)
        {
            ViewBag.productcode = productcode;
            ViewBag.producttype = producttype;

            //第一步中的下拉选项预加载
            ViewBag.PerslEmployment = BizCommon.GetAA10Items("sPerslEmployment", "cast(aaa102 as int)");
            ViewBag.PerslSalaryType = BizCommon.GetAA10Items("sPerslSalaryType", "cast(aaa102 as int)");
            ViewBag.PerslWorkingAge = BizCommon.GetAA10Items("sPerslWorkingAge", "cast(aaa102 as int)");

            if (Session[BizCommon.g_SessionName_ApplyProject] != null)
            {
                //为了防止已填写数据丢失，此处将Session中的内容取出填入
                ApplyingRecord p = (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord);
                PurchaseLoanStep1 m = new PurchaseLoanStep1();

                m.PerslEmployment = p.PerslEmployment;                  //选项
                m.PerslSalaryType = p.PerslSalaryType;                  //选项
                m.PerslWorkingAge = p.PerslWorkingAge;                  //选项
                m.PerslYoBirth = p.PerslYoBirth;                        //选项（但不从数据库中取）

                return View(m);
            }
            else
            {
                return View();
            }        
        }

        [HttpPost]
        public ActionResult Purchaseloan1(PurchaseLoanStep1 c, FormCollection values)
        {
            if (ModelState.IsValid)
            {
                string productcode = values["productcode"].ToString();
                string producttype = values["producttype"].ToString();

                if (Session[BizCommon.g_SessionName_ApplyProject] != null)
                {
                    //若session中已经存在申请对象，则将本步骤所取得的值赋到session中的对象上；
                    (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord).PerslEmployment = c.PerslEmployment;
                    (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord).PerslSalaryType = c.PerslSalaryType;
                    (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord).PerslWorkingAge = c.PerslWorkingAge;
                    (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord).PerslYoBirth = c.PerslYoBirth;

                    (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord).ProductCode = productcode;
                    (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord).ProductType = producttype;

                    return View("Purchaseloan2");
                }
                else
                {
                    //否则，新创建一个申请对象，并将本步骤取得值赋到新的对象上，然后将对象放到session中；
                    ApplyingRecord p = new ApplyingRecord();
                    p.PerslEmployment = c.PerslEmployment;
                    p.PerslSalaryType = c.PerslSalaryType;
                    p.PerslWorkingAge = c.PerslWorkingAge;
                    p.PerslYoBirth = c.PerslYoBirth;
                    p.ProductCode = productcode;
                    p.ProductType = producttype;

                    //第一步创建project类放到session中
                    if (Session[BizCommon.g_SessionName_ApplyProject] != null)
                        Session[BizCommon.g_SessionName_ApplyProject] = null;

                    Session[BizCommon.g_SessionName_ApplyProject] = p;

                    return View("Purchaseloan2");
                }
            }


            //万一发生异常时，将执行以下代码（即返回第一步页面）
            //第一步中的下拉选项预加载
            ViewBag.PerslEmployment = BizCommon.GetAA10Items("sPerslEmployment", "cast(aaa102 as int)");
            ViewBag.PerslSalaryType = BizCommon.GetAA10Items("sPerslSalaryType", "cast(aaa102 as int)");
            ViewBag.PerslWorkingAge = BizCommon.GetAA10Items("sPerslWorkingAge", "cast(aaa102 as int)");
            return View("Purchaseloan1");
        }
        #endregion

        #region 第二步
        public ActionResult Purchaseloan2()
        {
            //第一步中的下拉选项预加载
            ViewBag.PerslCardNo = BizCommon.GetAA10Items("sPerslCardNo", "cast(aaa102 as int)");
            ViewBag.PerslCreditOwner = BizCommon.GetAA10Items("sPerslCreditOwner", "cast(aaa102 as int)");
            ViewBag.PerslLoan = BizCommon.GetAA10Items("sPerslLoan", "cast(aaa102 as int)");
            ViewBag.PerslLoanDue = BizCommon.GetAA10Items("sPerslLoanDue", "cast(aaa102 as int)");

            if (Session[BizCommon.g_SessionName_ApplyProject] != null)
            {
                //为了防止已填写数据丢失，此处将Session中的内容取出填入
                ApplyingRecord p = (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord);
                PurchaseLoanStep2 m = new PurchaseLoanStep2();

                m.PerslCreditAllowance = p.PerslCreditAllowance;                //输入
                m.PerslCreditDue = p.PerslCreditDue;                            //输入

                m.PerslCardNo = p.PerslCardNo;                                  //选项
                m.PerslCreditOwner = p.PerslCreditOwner;                        //选项
                m.PerslLoan = p.PerslLoan;                                      //选项
                m.PerslLoanDue = p.PerslLoanDue;                                //选项

                return View(m);
            }
            else
            {
                return View();
            }        
        }

        [HttpPost]
        public ActionResult Purchaseloan2(PurchaseLoanStep2 c)
        {
            if (ModelState.IsValid)
            {
                if (Session[BizCommon.g_SessionName_ApplyProject] == null)    //若Session为空，则返回第一步（这是有可能的，长时间不操作）
                    return View("Purchaseloan1");

                if (Session[BizCommon.g_SessionName_ApplyProject] != null)
                {
                    //若session中已经存在申请对象，则将本步骤所取得的值赋到session中的对象上；
                    (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord).PerslCardNo = c.PerslCardNo;
                    (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord).PerslCreditAllowance = c.PerslCreditAllowance;
                    (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord).PerslCreditDue = c.PerslCreditDue;
                    (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord).PerslCreditOwner = c.PerslCreditOwner;
                    (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord).PerslLoan = c.PerslLoan;
                    (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord).PerslLoanDue = c.PerslLoanDue;

                    return View("Purchaseloan3");
                }
                else
                {
                    //否则，新创建一个申请对象，并将本步骤取得值赋到新的对象上，然后将对象放到session中；
                    ApplyingRecord p = new ApplyingRecord();
                    p.PerslCardNo = c.PerslCardNo;
                    p.PerslCreditAllowance = c.PerslCreditAllowance;
                    p.PerslCreditDue = c.PerslCreditDue;
                    p.PerslCreditOwner = c.PerslCreditOwner;
                    p.PerslLoan = c.PerslLoan;
                    p.PerslLoanDue = c.PerslLoanDue;

                    //第一步创建project类放到session中
                    if (Session[BizCommon.g_SessionName_ApplyProject] != null)
                        Session[BizCommon.g_SessionName_ApplyProject] = null;

                    Session[BizCommon.g_SessionName_ApplyProject] = p;

                    return View("Purchaseloan3");
                }
            }


            //万一发生异常时，将执行以下代码（即返回第一步页面）
            //第一步中的下拉选项预加载
            ViewBag.PerslCardNo = BizCommon.GetAA10Items("sPerslCardNo", "cast(aaa102 as int)");
            ViewBag.PerslCreditOwner = BizCommon.GetAA10Items("sPerslCreditOwner", "cast(aaa102 as int)");
            ViewBag.PerslLoan = BizCommon.GetAA10Items("sPerslLoan", "cast(aaa102 as int)");
            ViewBag.PerslLoanDue = BizCommon.GetAA10Items("sPerslLoanDue", "cast(aaa102 as int)");
            return View("Purchaseloan2");
        }
        #endregion

        #region 第三步
        public ActionResult Purchaseloan3()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Purchaseloan3(PurchaseLoanStep3 c)
        {
            if (ModelState.IsValid)
            {
                if (Session[BizCommon.g_SessionName_ApplyProject] == null)    //若Session为空，则返回第一步（这是有可能的，长时间不操作）
                    return View("Purchaseloan1");
                
                ApplyingRecord p = Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord;

                #region 完整的类赋值
                p.CarCustomerMonthlySalary = 0;
                p.CarProperty = "";
                p.CarPurchasingPeriod = "";

                p.CustomerEmail = c.CustomerEmail;
                p.CustomerName = c.CustomerName;
                p.CustomerPhone = c.CustomerPhone;

                p.CaseState = "0";
                p.CreatTime = DateTime.Today.ToString("yyyy-MM-dd");


                p.FirmAccountBill = 0;
                p.FirmAge = "";
                p.FirmProperty = "";
                p.FirmType = "";


                p.HouseIncome = "";
                p.HouseLocalorNot = "";
                p.HouseNew = "";
                p.HouseType = "";

                p.IPaddress = BizCommon.GetIP(this);

                //以下几个属性在前面几个步骤中已经完成输入
                //p.PerslCardNo = "";
                //p.PerslCreditAllowance = "";
                //p.PerslCreditDue = "";
                //p.PerslCreditOwner = "";
                //p.PerslEmployment = "";
                //p.PerslLoan = "";
                //p.PerslLoanDue = "";
                //p.PerslSalaryType = "";
                //p.PerslWorkingAge = "";
                //p.PerslYoBirth = "";

                //此处无需针对以下两个属性赋值，在第一步的时候已经完成赋值
                //p.ProductCode = productcode;
                //p.ProductType = producttype;

                #endregion

                #region 保存至数据库,并跳转到第三步

                string err = "";
                if (DataAdapter.Apply_Insert(p, ref err))
                {
                    return View("Purchaseloan4");
                }
                else
                {
                    //保存失败
                    ModelState.AddModelError("", "：（ 保存失败了！");
                    return View();
                }

                #endregion
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region 第四步
        public ActionResult Purchaseloan4()
        {
            return View();
        }
        #endregion

        #endregion

    }
}
