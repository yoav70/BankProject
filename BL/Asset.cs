
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.DAL;

namespace Bank.BL{
    public class Asset : DalSendable {
        public const double ASSET_TRANSFER_COMMISION = 0.5; // Percent
        private static string TABLE_NAME = "Table_Assets";
        private int m_InternalID = -1;
        private double m_Worth;
        private int m_AccountInternalID;
        private bool m_Sale;
        private string m_Address;
        private string m_FullName;
        
        public double Worth { get => m_Worth; set => m_Worth = value; }
        public int AccountInternalID { get => m_AccountInternalID; set => m_AccountInternalID = value; }
        public bool Sale { get => m_Sale; }
        public string Address { get => m_Address; set => m_Address = value; }
        public string FullName { get => m_FullName; set => m_FullName = value; }
        public override int InternalID { get => m_InternalID; set => m_InternalID = value; }
        public override string TableName => TABLE_NAME;

        
        public Asset(DataRow db_AccountRow) {
            m_InternalID = (int)db_AccountRow["InternalID"];
            Worth = (double)db_AccountRow["Worth"];
            AccountInternalID = (int)db_AccountRow["AccountInternalID"];
            Address = (string)db_AccountRow["Address"];
            FullName = (string)db_AccountRow["FullName"];
        }
        private Asset(int m_InternalID) {
            this.InternalID = m_InternalID;
        }
        
        public override DataTable AsTable() {
            DataTable table = new DataTable(TableName);

            table.Columns.Add(new DataColumn("InternalID", typeof(Int32)) {
                AllowDBNull = false,
                Unique = true
            });
            table.Columns.Add(new DataColumn("Worth", typeof(double)));
            table.Columns.Add(new DataColumn("AccountInternalID", typeof(Int32)));
            table.Columns.Add(new DataColumn("Address", typeof(string)));
            table.Columns.Add(new DataColumn("FullName", typeof(string)));
            DataRow row = table.NewRow();
            row["InternalID"] = InternalID;
            row["Worth"] = Worth;
            row["AccountInternalID"] = AccountInternalID;
            row["Address"] = Address;
            row["FullName"] = FullName;
            table.Rows.Add(row);
            return table;
        }
        public static AssetList GetAssetsByAccount(Account acc) {
            DataTable queryResult = SQL_Dal.GetAllByField(new Asset(-1), "AccountInternalID", acc.InternalID.ToString());
            return AssetList.FromDataTable(queryResult);
        }

        public bool SetSale(int m_AccountInternalID, bool m_Sale) {
            if (this.m_AccountInternalID != m_AccountInternalID) return false;
            this.m_Sale = m_Sale;
            return this.PostDal();
        }
        
    }
}
