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
        public ActionResult ProductList(string type,string money,string term,string pindex)
        {
            DA_Adapter da = new DA_Adapter();

            string sql1 = " select t1.sProductName, t1.sProductType, t1.dAnnualRate, t1.sApplyCondition, t1.sRequiredFile, t1.sMemo, t1.sDetails," + 
                          " t2.sOrganName, t2.sLogo" +
                          " from T_Product t1" +
                          " left join T_ForeignOrgan t2 on t1.sOrganID=t2.sOrganID"+
                          " where t1.sProductType='"+ type +"' and t1.dMoneyBottom<="+ money +"and t1.dMoneyTop>="+ money +
                          " and t1.nTermBottom<="+ term +" and t1.nTermTop>="+ term;

            string sql2 = " select t1.sCustomName, t1.sOrganID, t2.sOrganName" +
                          " from T_Custom t1" +
                          " left join T_ForeignOrgan t2 on t1.sOrganID=t2.sOrganID";

            DataSet ds = new DataSet();
            string err = "";
            da.Common_Query_MultiTable(ref ds, sql1, "T_product", sql2, "T_Custom", ref err);

            List<ProductModel> products = new List<ProductModel>();
            ProductModel p;
            DataRow[] listCustomRows;

            foreach (DataRow dr in ds.Tables["T_product"].Rows)
            {
                listCustomRows=ds.Tables["T_Custom"].Select("sOrganID='" + dr["sOrganID"].ToString() + "'");
                p = Convert2Product(dr, listCustomRows);
                products.Add(p);//未分页显示。
            }
            //总页数（每页显示10条）
            int countPerPage = 10;
            int pageCount = products.Count / countPerPage + (products.Count % countPerPage > 0 ? 1 : 0);
            int pageIndex = (pindex == null || pindex == "") ? 1 : Convert.ToInt32(pindex);
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex;

            ViewBag.PageCount = pageCount;
            ViewBag.PageIndex = pageIndex;

            var productsShow = new List<ProductModel>();
            int start = (pageIndex - 1) * countPerPage;
            int end = (pageIndex * countPerPage) > products.Count ? products.Count : (pageIndex * countPerPage);
            for (int i = start; i < end; i++)
            {
                productsShow.Add(products[i]);
            }
            return View(productsShow);            
        }


         static ProductModel Convert2Product(DataRow dr,DataRow[] listCustomRows)
        {
            ProductModel p;
            p = new ProductModel();
            p.ProductType = dr["sProductType"].ToString();
            p.ProductName = dr["sProductName"].ToString();
            p.AnnualRate = Convert.ToDecimal(dr["dAnnualRate"]);
            p.ApplyCondition = dr["sApplyCondition"].ToString();
            p.RequiredFile = dr["sRequiredFile"].ToString();
            p.Memo = dr["sMemo"].ToString();
            p.Details = dr["sDetails"].ToString();

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
        #endregion


        #region 车贷申请

         #region 第一步
         public ActionResult Carloan1() 
         {
             return View();         
         }

         //[HttpPost]
         //public ActionResult Carloan1(ApplyingRecord a)
         //{
         //    if (Session[BizCommon.g_SessionName_ApplyProject] != null)
         //    {
         //        //为了防止已填写数据丢失，此处将Session中的内容取出填入
         //        ApplyingRecord p = (Session[BizCommon.g_SessionName_ApplyProject] as ApplyingRecord);
         //        ApplyStep01Model m = new ApplyStep01Model();

         //        m.Term = p.Term;
         //        m.Money = p.Money;
         //        m.ProfitRate = p.ProfitRate;
         //        m.Description = p.Description;

         //        return View(m);
         //    }
         //    else
         //    {
         //        return View();
         //    }
         //}
        #endregion

        #endregion

    }
}
