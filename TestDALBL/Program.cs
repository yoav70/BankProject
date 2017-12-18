using Bank.BL;
using Bank.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Bank.TestDALBL {
    class Program {
        static void Main(string[] args) {
            Account testAcc = new Account(318803079, 0549461800, 24547, 1000, "Yoav Rosen", "psw", AppDomain.CurrentDomain.BaseDirectory);
            Console.WriteLine(SQL_Dal.Insert(testAcc));
            
            Console.ReadLine();
        }
    }
}
