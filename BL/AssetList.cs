
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.BL {
    public class AssetList : ArrayList{
        private AssetList() : base() { }
        public static AssetList FromDataTable (DataTable table) {
            AssetList result = new AssetList();
            foreach (DataRow row in table.Rows) result.Add(new Asset(row));
            return result;
        }
    }
}
