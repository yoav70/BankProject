using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.DAL;

namespace Bank.BL{
    class Asset : DalSendable {
        private static string TABLE_NAME = "Table_Assets";

        private int m_InternalID;
        private int m_Worth;
        private int m_AccountInternalID;
        private int m_AccountNumber;
        private string m_Address;
        private string m_FullName;
        private string m_Path;

        public Asset(int m_InternalID, int m_Worth, int m_AccountInternalID, int m_AccountNumber, string m_Address, string m_FullName, string m_Path) {
            this.m_InternalID = m_InternalID;
            this.m_Worth = m_Worth;
            this.m_AccountInternalID = m_AccountInternalID;
            this.m_AccountNumber = m_AccountNumber;
            this.m_Address = m_Address;
            this.m_FullName = m_FullName;
            this.m_Path = m_Path;
        }

        public int Worth { get => m_Worth; set => m_Worth = value; }
        public int AccountInternalID { get => m_AccountInternalID; set => m_AccountInternalID = value; }
        public int AccountNumber { get => m_AccountNumber; set => m_AccountNumber = value; }
        public string Address { get => m_Address; set => m_Address = value; }
        public string FullName { get => m_FullName; set => m_FullName = value; }
        public override string Path { get => m_Path; set => m_Path = value; }
        public override int InternalID { get => m_InternalID; set => m_InternalID = value; }
        public override string TableName => TABLE_NAME;



        public override DataTable AsTable() {
            DataTable table = new DataTable(TableName);

            table.Columns.Add(new DataColumn("InternalID", typeof(Int32)) {
                AllowDBNull = false,
                Unique = true
            });
            table.Columns.Add(new DataColumn("Worth", typeof(Int32)));
            table.Columns.Add(new DataColumn("AccountInternalID", typeof(Int32)));
            table.Columns.Add(new DataColumn("AccountNumber", typeof(Int32)));
            table.Columns.Add(new DataColumn("Address", typeof(string)));
            table.Columns.Add(new DataColumn("FullName", typeof(string)));
            DataRow row = table.NewRow();
            row["InternalID"] = InternalID;
            row["Worth"] = Worth;
            row["AccountInternalID"] = AccountInternalID;
            row["AccountNumber"] = AccountNumber;
            row["Address"] = Address;
            row["FullName"] = FullName;
            table.Rows.Add(row);
            return table;
        }
    }
}
