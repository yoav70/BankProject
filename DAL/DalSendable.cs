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
        public abstract string TableName{ get; }
        public abstract string Path { get; set; }
    }
    
}
