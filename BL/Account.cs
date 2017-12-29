using System;
using System.IO;
using Bank.DAL;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.BL{
    public class Account : DalSendable {
        private static string TABLE_NAME = "Table_Accounts";
        private int m_InternalID = -1;
        private int m_CitizenID;
        private int m_PhoneNumber;
        private double m_Balance;
        private string m_FullName;
        private int m_Password;
  
        public int CitizenID { get => m_CitizenID; set => m_CitizenID = value; }
        public int PhoneNumber { get => m_PhoneNumber; set => m_PhoneNumber = value; }
        public double Balance { get => m_Balance; set => m_Balance = value; }
        public string FullName { get => m_FullName; set => m_FullName = value; }
        public int Password { get => m_Password;}
        public override int InternalID { get => m_InternalID; set => m_InternalID = value; }
        public override string TableName { get => TABLE_NAME; }

        
        public Account(DataRow db_AccountRow) {
            m_InternalID = (int)db_AccountRow["InternalID"];
            CitizenID = (int)db_AccountRow["CitizenID"];
            PhoneNumber = (int)db_AccountRow["PhoneNumber"];
            m_Password = (int)db_AccountRow["Password"];
            Balance = (double)db_AccountRow["Balance"];
            FullName = (string)db_AccountRow["FullName"];
        }
        private Account(int m_InternalID) {
            this.InternalID = m_InternalID;
        }

        public bool Verify(string password) {
            return password.GetHashCode().Equals(Password);
        }
        
        public override DataTable AsTable() {
            DataTable table = new DataTable(TableName);

            table.Columns.Add(new DataColumn("InternalID", typeof(Int32)) {
                AllowDBNull = false,
                Unique = true
            });
            table.Columns.Add(new DataColumn("CitizenID", typeof(Int32)));
            table.Columns.Add(new DataColumn("PhoneNumber", typeof(Int32)));
            table.Columns.Add(new DataColumn("Balance", typeof(double)));
            table.Columns.Add(new DataColumn("Password", typeof(Int32)));
            table.Columns.Add(new DataColumn("FullName", typeof(string)));
            DataRow row = table.NewRow();
            row["InternalID"] = InternalID;
            row["CitizenID"] = CitizenID;
            row["PhoneNumber"] = PhoneNumber;
            row["Balance"] = Balance;
            row["FullName"] = FullName;
            row["Password"] = Password;
            table.Rows.Add(row);
            return table;
        }

        public static Account GetAccountByInternalId(int m_InternalID) {
            return new Account(SQL_Dal.GetByInternalID(
                    new Account(m_InternalID)).Rows[0]);
        }

        public bool BuyAsset(Asset asset) {
            throw new NotImplementedException();  
        }
    }
}
