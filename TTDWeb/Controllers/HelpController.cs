using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TTDWeb.Controllers
{
    public class HelpController : Controller
    {

        #region 投诉客户经理
        public ActionResult Complain()
        {
            return View();
        }
        #endregion

        #region 关于我们
        public ActionResult Aboutus()
        {
            return View();
        }
        #endregion

        #region 联系我们
        public ActionResult Contactus()
        {
            return View();
        }
        #endregion

        #region 加入我们
        public ActionResult Joinus()
        {
            return View();
        }
        #endregion

        #region 意见反馈
        public ActionResult Feedback()
        {
            return View();
        }
        #endregion

        #region 提交成功
        public ActionResult Success()
        {
            return View();
        }
        #endregion

        #region 服务条款
        public ActionResult Serverlist()
        {
            return View();
        }
        #endregion

        #region 接收客户端文件上传

        public void up(string folder)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    string sfolder = folder;
                    if (sfolder == null || sfolder == "") sfolder = "doc";

                    HttpPostedFileBase file = Request.Files[0];
                    string filePath = Server.MapPath("~/") + sfolder + "\\" + file.FileName;
                    file.SaveAs(filePath);
                    Response.Write("Success\r\n");
                }
                catch (Exception ex)
                {
                    Response.Write("Error！详细：" + ex.ToString());
                }
            }

        }

        #endregion

    }
}
