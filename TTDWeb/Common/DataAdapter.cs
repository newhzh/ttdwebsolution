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

        public static bool Apply_Insert(ApplyingRecord p, ref string err)
        {
            string newID = SqlServerDAL.DA_Common.GetNewID_ByDate(DateTime.Today.ToString("yyyyMMdd"), "T_ApplyRecord", "sApplyID", 5, "A", 0);
            string sql = "insert into T_ApplyRecord(sApplyID , sProductCode , sCustomerName , sCustomerPhone , sCustomerEmail , sProductType , sCarProperty , dCarCustomerMonthlySalary , sCarPurchasingPeriod , sHouseType , sHouseIncome , sHouseLocalorNot , sHouseNew , sFirmType , dFirmAccountBill , sFirmAge , sFirmProperty , sPerslEmployment , sPerslYoBirth , sPerslSalaryType , sPerslWorkingAge , sPerslCreditOwner , sPerslCardNo , sPerslCreditAllowance , sPerslCreditDue , sPerslLoan , sPerslLoanDue ,sPerslLoanSucc, dtCreatTime , sCaseState , sIPaddress) values ( " +
                    "'" + newID + "'" +
                    ",'" + p.ProductCode + "'" +
                    ",'" + p.CustomerName + "'" +
                    ",'" + p.CustomerPhone + "'" +
                    ",'" + p.CustomerEmail + "'" +
                    ",'" + p.ProductType + "'" +
                    ",'" + (p.CarProperty.Split(',').Length > 1 ? p.CarProperty.Split(',')[0] : p.CarProperty) + "'" +
                    "," + p.CarCustomerMonthlySalary.ToString() +
                    ",'" + (p.CarPurchasingPeriod.Split(',').Length > 1 ? p.CarPurchasingPeriod.Split(',')[0] : p.CarPurchasingPeriod) + "'" +
                    ",'" + (p.HouseType.Split(',').Length > 1 ? p.HouseType.Split(',')[0] : p.HouseType) + "'" +
                    ",'" + p.HouseIncome + "'" +
                    ",'" + (p.HouseLocalorNot.Split(',').Length > 1 ? p.HouseLocalorNot.Split(',')[0] : p.HouseLocalorNot) + "'" +
                    ",'" + (p.HouseNew.Split(',').Length > 1 ? p.HouseNew.Split(',')[0] : p.HouseNew) + "'" +
                    ",'" + (p.FirmType.Split(',').Length > 1 ? p.FirmType.Split(',')[0] : p.FirmType) + "'" +
                    "," + p.FirmAccountBill.ToString() +
                    ",'" + (p.FirmAge.Split(',').Length > 1 ? p.FirmAge.Split(',')[0] : p.FirmAge) + "'" +
                    ",'" + (p.FirmProperty.Split(',').Length > 1 ? p.FirmProperty.Split(',')[0] : p.FirmProperty) + "'" +
                    ",'" + (p.PerslEmployment.Split(',').Length > 1 ? p.PerslEmployment.Split(',')[0] : p.PerslEmployment) + "'" +
                    ",'" + p.PerslYoBirth + "'" +
                    ",'" + (p.PerslSalaryType.Split(',').Length > 1 ? p.PerslSalaryType.Split(',')[0] : p.PerslSalaryType) + "'" +
                    ",'" + (p.PerslWorkingAge.Split(',').Length > 1 ? p.PerslWorkingAge.Split(',')[0] : p.PerslWorkingAge) + "'" +
                    ",'" + (p.PerslCreditOwner.Split(',').Length > 1 ? p.PerslCreditOwner.Split(',')[0] : p.PerslCreditOwner) + "'" +
                    ",'" + (p.PerslCardNo.Split(',').Length > 1 ? p.PerslCardNo.Split(',')[0] : p.PerslCardNo) + "'" +
                    ",'" + (p.PerslCreditAllowance.Split(',').Length > 1 ? p.PerslCreditAllowance.Split(',')[0] : p.PerslCreditAllowance) + "'" +
                    ",'" + (p.PerslCreditDue.Split(',').Length > 1 ? p.PerslCreditDue.Split(',')[0] : p.PerslCreditDue) + "'" +
                    ",'" + (p.PerslLoan.Split(',').Length > 1 ? p.PerslLoan.Split(',')[0] : p.PerslLoan) + "'" +
                    ",'" + (p.PerslLoanDue.Split(',').Length > 1 ? p.PerslLoanDue.Split(',')[0] : p.PerslLoanDue) + "'" +
                    ",'" + (p.PerslLoanSucc.Split(',').Length > 1 ? p.PerslLoanSucc.Split(',')[0] : p.PerslLoanSucc) + "'" +
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