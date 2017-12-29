using Bank.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Bank {
    class Operation : DalSendable {
        
        private int m_InternalID = -1;
        private int m_SenderInternalID;
        private int m_ReceiverInternalID;
        private int m_Amount;
        private int m_Commision;

        public override string TableName => "Table_Operations";
        public override int InternalID { get => m_InternalID; set => m_InternalID = value; }
        public int SenderInternalID { get => m_SenderInternalID; set => m_SenderInternalID = value; }
        public int ReceiverInternalID { get => m_ReceiverInternalID; set => m_ReceiverInternalID = value; }
        public int Amount { get => m_Amount; set => m_Amount = value; }
        public int Commision { get => m_Commision; set => m_Commision = value; }

        
        private Operation(int m_InternalID){
            this.InternalID = m_InternalID;
        }
        public Operation(DataRow db_AccountRow) {
            m_InternalID = (int)db_AccountRow["InternalID"];
            SenderInternalID = (int)db_AccountRow["SenderInternalID"];
            ReceiverInternalID = (int)db_AccountRow["ReceiverInternalID"];
            Amount = (int)db_AccountRow["Amount"];
            Commision = (int)db_AccountRow["Commision"];
        }
        
        public override DataTable AsTable() {
            DataTable table = new DataTable(TableName);

            table.Columns.Add(new DataColumn("InternalID", typeof(Int32)) {
                AllowDBNull = false,
                Unique = true
            });
            table.Columns.Add(new DataColumn("SenderInternalID", typeof(Int32)));
            table.Columns.Add(new DataColumn("ReceiverInternalID", typeof(Int32)));
            table.Columns.Add(new DataColumn("Amount", typeof(Int32)));
            table.Columns.Add(new DataColumn("Commision", typeof(Int32)));
            DataRow row = table.NewRow();
            row["InternalID"] = InternalID;
            row["SenderInternalID"] = SenderInternalID;
            row["ReceiverInternalID"] = ReceiverInternalID;
            row["Amount"] = Amount;
            row["Commision"] = Commision;
            table.Rows.Add(row);
            return table;
        }
    }
}
