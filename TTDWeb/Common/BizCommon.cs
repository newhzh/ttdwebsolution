using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Message;
using SqlServerDAL;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using TTDWeb.Models;

namespace TTDWeb.Common
{
    public class BizCommon
    {
        #region Session名称定义
        public static string g_SessionName_ApplyProject = "ApplyProject";
        #endregion

        #region 读取AA10集合作为下拉选项
        public static SelectList GetAA10Items(string fieldName, string orderBy)
        {
            DA_Adapter adapter = new DA_Adapter();
            string sOrderBy = "AAA102";
            if (orderBy != "")
                sOrderBy = orderBy;
            DataSet ds = adapter.AA10_Read("AAA100='" + fieldName + "'", sOrderBy);

            List<Models.AA10_Item> aa10ItemList = new List<Models.AA10_Item>();
            AA10_Item item;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                //为了后台返回值轻松取得DropDownList的Text值，将text隐藏于value中
                item = new AA10_Item(dr["AAA103"].ToString(), dr["AAA102"].ToString() + "," + dr["AAA103"].ToString());
                aa10ItemList.Add(item);
            }

            return new SelectList(aa10ItemList, "Value", "Text");
        }
        #endregion

        #region 发送E-Mail

        public static void SendSystemMail(string context, string toMailAddress, string subject, ref string err)
        {
            try
            {
                if (toMailAddress == "") return;

                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("ttdserver@163.com", "天代宝客服中心");
                msg.To.Add(toMailAddress);
                msg.Subject = subject;
                msg.Body = context;

                SmtpClient client = new SmtpClient("smtp.163.com");
                NetworkCredential nc = new NetworkCredential("ttdserver@163.com", "_ttd123456");
                client.Credentials = nc;
                client.Send(msg);
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
        }

        #endregion

        #region 判断DataSet
        public static bool IsNullDataSet(DataSet ds)
        {
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return true;
            else
                return false;
        }

        #endregion

        #region 按时间段模拟在线人数

        public static int GetOnlineMemberCount()
        {
            int val = 0;
            Random rd;
            if (DateTime.Now.Hour == 0)
            {
                rd = new Random(10);
                val = rd.Next(200);
            }
            else if (DateTime.Now.Hour == 1)
            {
                rd = new Random(10);
                val = rd.Next(200);
            }
            else if (DateTime.Now.Hour >= 2 && DateTime.Now.Hour <= 6)
            {
                rd = new Random(0);
                val = rd.Next(10);
            }
            else if (DateTime.Now.Hour == 7)
            {
                rd = new Random(5);
                val = rd.Next(100);
            }
            else if (DateTime.Now.Hour == 8)
            {
                rd = new Random(10);
                val = rd.Next(200);
            }
            else if (DateTime.Now.Hour == 9)
            {
                rd = new Random(10);
                val = rd.Next(300);
            }
            else if (DateTime.Now.Hour == 10)
            {
                rd = new Random(20);
                val = rd.Next(400);
            }
            else if (DateTime.Now.Hour == 11)
            {
                rd = new Random(30);
                val = rd.Next(500);
            }
            else if (DateTime.Now.Hour == 12)
            {
                rd = new Random(20);
                val = rd.Next(400);
            }
            else if (DateTime.Now.Hour >= 13 && DateTime.Now.Hour <= 17)
            {
                rd = new Random(400);
                val = rd.Next(400);
            }
            else if (DateTime.Now.Hour >= 18 && DateTime.Now.Hour <= 19)
            {
                rd = new Random(10);
                val = rd.Next(100);
            }
            else if (DateTime.Now.Hour >= 20 && DateTime.Now.Hour <= 23)
            {
                rd = new Random(30);
                val = rd.Next(300);
            }

            return val;
        }

        #endregion

        

        #region 获取所在地名称
        public static string GetRegionFullName(string provinceID, string cityID, string countryID)
        {
            string err = "";
            string sql1 = "select sRegionName from t_region where sRegionID='" + provinceID + "'";
            string sql2 = "select sRegionName from t_region where sRegionID='" + cityID + "'";
            string sql3 = "select sRegionName from t_region where sRegionID='" + countryID + "'";
            DataSet ds = new DataSet();
            DA_Common da = new DA_Common();
            da.CommonQuery(ref ds, sql1, "T1", sql2, "T2", sql3, "T3", ref err);
            if (BizCommon.IsNullDataSet(ds)) return "";

            string result = "";
            if (ds.Tables["T1"].Rows.Count > 0)
                result = ds.Tables["T1"].Rows[0]["sRegionName"].ToString();
            if (ds.Tables["T2"].Rows.Count > 0)
                result += " " + ds.Tables["T2"].Rows[0]["sRegionName"].ToString();
            if (ds.Tables["T3"].Rows.Count > 0)
                result += " " + ds.Tables["T3"].Rows[0]["sRegionName"].ToString();

            return result;
        }
        #endregion

        #region 计算两个DateTime之间相差月数零天数

        public static string DateDiff_MonthDays(DateTime dtBegin, DateTime dtEnd)
        {
            if (dtEnd <= dtBegin) return "已经过期";

            int m = 0, d = 0;
            if (dtBegin.Year == dtEnd.Year)
            {
                //同年内（dtEnd的月份必然大于dtBegin）
                if (dtEnd.Month == dtBegin.Month)
                {
                    m = 0;
                    d = dtEnd.Day - dtBegin.Day;
                }
                else
                {
                    if (dtEnd.Day >= dtBegin.Day)
                    {
                        m = dtEnd.Month - dtBegin.Month;
                        d = dtEnd.Day - dtBegin.Day;
                    }
                    else
                    {
                        if (dtEnd.Month - dtBegin.Month <= 1)
                        {
                            m = 0;
                            TimeSpan ts1 = dtEnd - dtBegin;
                            d = ts1.Days;
                        }
                        else
                        {
                            m = dtEnd.Month - dtBegin.Month - 1;
                            DateTime dtTemp2 = Convert.ToDateTime(dtEnd.AddMonths(-1).Year.ToString() + "-" + dtEnd.AddMonths(-1).Month.ToString() + "-" + dtBegin.Day.ToString());
                            TimeSpan ts2 = dtEnd - dtTemp2;
                            d = ts2.Days;
                        }
                    }
                }
            }
            else
            {
                //跨年，dtEnd年份必然比btBegin大
                if (dtEnd.Day >= dtBegin.Day)
                {
                    m = (dtEnd.Year - dtBegin.Year - 1) * 12 + (12 - dtBegin.Month) + dtEnd.Month;
                    d = dtEnd.Day - dtBegin.Day;
                }
                else
                {
                    m = (dtEnd.Year - dtBegin.Year - 1) * 12 + (12 - dtBegin.Month) + dtEnd.Month - 1;
                    DateTime dtTemp = Convert.ToDateTime(dtEnd.AddMonths(-1).Year.ToString() + "-" + dtEnd.AddMonths(-1).Month.ToString() + "-" + dtBegin.Day.ToString());
                    TimeSpan ts = dtEnd - dtTemp;
                    d = ts.Days;
                }
            }

            return "还剩" + m.ToString() + "个月零" + d.ToString() + "天";
        }

        /// <summary>
        /// 计算两个时间内相差的月份，多出的天数不计算在内
        /// </summary>
        /// <param name="dtBegin">开始日期</param>
        /// <param name="dtEnd">结束日期</param>
        /// <returns></returns>
        public static int DateDiff_Months(DateTime dtBegin, DateTime dtEnd)
        {
            if (dtEnd <= dtBegin) return 0;

            if (dtBegin.Year == dtEnd.Year)
            {
                //同年内（dtEnd的月份必然大于dtBegin）
                if (dtEnd.Month == dtBegin.Month)
                {
                    return 0;
                }
                else
                {
                    if (dtEnd.Day >= dtBegin.Day)
                    {
                        return dtEnd.Month - dtBegin.Month;
                    }
                    else
                    {
                        if (dtEnd.Month - dtBegin.Month <= 1)
                        {
                            return 0;
                        }
                        else
                        {
                            return dtEnd.Month - dtBegin.Month - 1;
                        }
                    }
                }
            }
            else
            {
                //跨年，dtEnd年份必然比btBegin大
                if (dtEnd.Day >= dtBegin.Day)
                {
                    return (dtEnd.Year - dtBegin.Year - 1) * 12 + (12 - dtBegin.Month) + dtEnd.Month;
                }
                else
                {
                    return (dtEnd.Year - dtBegin.Year - 1) * 12 + (12 - dtBegin.Month) + dtEnd.Month - 1;
                }
            }
        }

        #endregion
    }
}