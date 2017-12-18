using System;
using Bank.DAL;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.BL{
    public class Account : DalSendable {
        private static string TABLE_NAME = "Table_Accounts";
        private int m_InternalID;
        private int m_CitizenID;
        private int m_PhoneNumber;
        private int m_AccountNumber;
        private double m_Balance;
        private string m_FullName;
        private int m_Password;
        private string m_Path;

        
        public int CitizenID { get => m_CitizenID; set => m_CitizenID = value; }
        public int PhoneNumber { get => m_PhoneNumber; set => m_PhoneNumber = value; }
        public int AccountNumber { get => m_AccountNumber; set => m_AccountNumber = value; }
        public double Balance { get => m_Balance; set => m_Balance = value; }
        public string FullName { get => m_FullName; set => m_FullName = value; }
        public int Password { get => m_Password;}
        public override int InternalID { get => m_InternalID; set => m_InternalID = value; }
        public override string Path { get => m_Path; set => m_Path = value; }
        public override string TableName { get => TABLE_NAME; }

        public Account(int m_CitizenID, int m_PhoneNumber, int m_AccountNumber, double m_Balance, string m_FullName, string m_Password, string m_Path) {
            
            this.CitizenID = m_CitizenID;
            this.PhoneNumber = m_PhoneNumber;
            this.AccountNumber = m_AccountNumber;
            this.Balance = m_Balance;
            this.FullName = m_FullName;
            this.m_Password = m_Password.GetHashCode();
            this.Path = m_Path;
        }
        private Account(int m_InternalID, string m_Path) {
            this.InternalID = m_InternalID;
            this.Path = m_Path;
        }
        public Account(DataRow db_AccountRow, string m_Path) {
            m_InternalID = (int)db_AccountRow["InternalID"];
            CitizenID = (int)db_AccountRow["CitizenID"];
            PhoneNumber = (int)db_AccountRow["PhoneNumber"];
            AccountNumber = (int)db_AccountRow["AccountNumber"];
            m_Password = (int)db_AccountRow["Password"];
            Balance = (double)db_AccountRow["Balance"];
            FullName = (string)db_AccountRow["FullName"];
            this.Path = m_Path;
        }
        public bool Verify(string password) {
            return password.GetHashCode().Equals(Password);
        }
        public bool AddBalance(double balance) {
            if (balance + Balance < 0) return false;
            else Balance += balance;
            return true;
        }

        public override DataTable AsTable() {
            DataTable table = new DataTable(TableName);

            table.Columns.Add(new DataColumn("InternalID", typeof(Int32)) {
                AllowDBNull = false,
                Unique = true
            });
            table.Columns.Add(new DataColumn("CitizenID", typeof(Int32)));
            table.Columns.Add(new DataColumn("PhoneNumber", typeof(Int32)));
            table.Columns.Add(new DataColumn("AccountNumber", typeof(Int32)));
            table.Columns.Add(new DataColumn("Balance", typeof(Int32)));
            table.Columns.Add(new DataColumn("Password", typeof(Int32)));
            table.Columns.Add(new DataColumn("FullName", typeof(string)));
            DataRow row = table.NewRow();
            row["InternalID"] = InternalID;
            row["CitizenID"] = CitizenID;
            row["PhoneNumber"] = PhoneNumber;
            row["AccountNumber"] = AccountNumber;
            row["Balance"] = Balance;
            row["FullName"] = FullName;
            row["Password"] = Password;
            table.Rows.Add(row);
            return table;
        }

        public static Account GetAccountByInternalId(int m_InternalID, string m_Path) {
            return new Account(SQL_Dal.GetByInternalID(
                    new Account(m_InternalID, m_Path)).Rows[0], m_Path);
        }
    }
}
