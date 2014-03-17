using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TTDWeb.Models;

namespace TTDWeb.Common
{
    public class DataAdapter
    {
        public DataAdapter()
        {
        }

        #region 保存·贷款申请

        public static bool Apply_Insert(ApplyingRecord p,ref string err)
        {
            SqlServerDAL.DA_Common.GetNewID_ByDate(DateTime.Today.ToString("yyyyMMdd"), "T_ApplyRecord", "sApplyID", 5, "A", 0);
            string newID = "";
            string sql = "insert into T_ApplyRecord(sApplyID , sProductCode , sCustomerName , sCustomerPhone , sCustomerEmail , sProductType , sCarProperty , dCarCustomerMonthlySalary , sCarPurchasingPeriod , sHouseType , sHouseIncome , sHouseLocalorNot , sHouseNew , sFirmType , dFirmAccountBill , sFirmAge , sFirmProperty , sPerslEmployment , sPerslYoBirth , sPerslSalaryType , sPerslWorkingAge , sPerslCreditOwner , sPerslCardNo , sPerslCreditAllowance , sPerslCreditDue , sPerslLoan , sPerslLoanDue , dtCreatTime , sCaseState , sIPaddress) values ( " +
                    "'" + newID + "'" +
                    ",'" + p.ProductCode + "'" +
                    ",'" + p.CustomerName + "'" +
                    ",'" + p.CustomerPhone + "'" +
                    ",'" + p.CustomerEmail + "'" +
                    ",'" + p.ProductType + "'" +
                    ",'" + p.CarProperty + "'" +
                    "," + p.CarCustomerMonthlySalary.ToString() +
                    ",'" + p.CarPurchasingPeriod + "'" +
                    ",'" + p.HouseType + "'" +
                    ",'" + p.HouseIncome + "'" +
                    ",'" + p.HouseLocalorNot + "'" +
                    ",'" + p.HouseNew + "'" +
                    ",'" + p.FirmType + "'" +
                    "," + p.FirmAccountBill.ToString() +
                    ",'" + p.FirmAge + "'" +
                    ",'" + p.FirmProperty + "'" +
                    ",'" + p.PerslEmployment + "'" +
                    ",'" + p.PerslYoBirth + "'" +
                    ",'" + p.PerslSalaryType + "'" +
                    ",'" + p.PerslWorkingAge + "'" +
                    ",'" + p.PerslCreditOwner + "'" +
                    ",'" + p.PerslCardNo + "'" +
                    ",'" + p.PerslCreditAllowance + "'" +
                    ",'" + p.PerslCreditDue + "'" +
                    ",'" + p.PerslLoan + "'" +
                    ",'" + p.PerslLoanDue + "'" +
                    ",GetDate()" + 
                    ",'" + p.CaseState + "'" +
                    ",'" + p.IPaddress + "'" + ")";

            SqlServerDAL.DA_Adapter da = new SqlServerDAL.DA_Adapter();
            int val = da.Common_Excute(sql, ref err);
            if (val == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

    }
}