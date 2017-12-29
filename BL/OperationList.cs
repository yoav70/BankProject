using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank {
    class OperationList : ArrayList {
        private OperationList() : base() {}
        public static OperationList FromDataTable(DataTable table) {
            throw new NotImplementedException();
        }
    }
}
