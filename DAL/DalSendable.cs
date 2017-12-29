using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.DAL {
    public abstract class DalSendable {
        public abstract DataTable AsTable();
        public abstract int InternalID { get; set; }
        public abstract string TableName { get; }

        public bool PostDal() {
            if (InternalID < 1) return SQL_Dal.Insert(this);
            else return SQL_Dal.Update(this);
        }
        
    }
    
}
