using System;
using System.Data;
using System.Text;

namespace Bank.DAL {
    public class SQL_Dal {

        public static bool Insert(DalSendable data) {
            StringBuilder buildF = new StringBuilder().Append("INSERT INTO ").Append(data.TableName).Append("( ");
            DataTable table = data.AsTable();
            
            foreach(DataColumn col in table.Columns)
                if (!col.ColumnName.Equals("InternalID")) buildF.Append("[").Append(col.ColumnName).Append("], ");
                
            if(buildF.Length-data.TableName.Length > 14) buildF.Remove(buildF.Length - 2, 1);
            buildF.Append(") ");
            StringBuilder buildV = new StringBuilder().Append("VALUES (");
            foreach (DataRow row in table.Rows) {
                foreach (DataColumn col in table.Columns)
                    if (!col.ColumnName.Equals("InternalID"))
                        if (col.DataType.Equals(typeof(Int32))) buildV.Append(row[col.ColumnName]).Append(", ");
                        else if (col.DataType.Equals(typeof(double))) buildV.Append(row[col.ColumnName]).Append(", ");
                        else if (col.DataType.Equals(typeof(string))) buildV.Append("'").Append(row[col.ColumnName]).Append("', ");
                        else if (col.DataType.Equals(typeof(bool))) buildV.Append("").Append((bool)row[col.ColumnName] ? "TRUE" : "FALSE" ).Append(", ");
                        
                break;
            }
            if (buildV.Length - data.TableName.Length > 8) buildV.Remove(buildV.Length - 2, 1);
            buildV.Append(")");
            return Dal.ExecuteSql(buildF.Append(buildV.ToString()).ToString());
        }

        public static bool Update(DalSendable data) {
            StringBuilder build = new StringBuilder().Append("UPDATE ").Append(data.TableName).Append(" SET");
            DataTable table = data.AsTable();
            
            foreach (DataRow row in table.Rows) foreach (DataColumn col in table.Columns) {
                    Console.WriteLine("Update Debug: " + col.ColumnName);
                    if (col.DataType.Equals(typeof(Int32)))
                        build.Append(" [").Append(col.ColumnName).Append("] = ").Append(row[col.ColumnName]).Append(", ");
                    else if (col.DataType.Equals(typeof(double)))
                        build.Append(" [").Append(col.ColumnName).Append("] = ").Append(row[col.ColumnName]).Append(", ");
                    else if (col.DataType.Equals(typeof(string)))
                        build.Append(" [").Append(col.ColumnName).Append("] = '").Append(row[col.ColumnName]).Append("', ");
                    else if (col.DataType.Equals(typeof(bool)))
                        build.Append(" [").Append(col.ColumnName).Append("] = ").Append((bool)row[col.ColumnName] ? "TRUE" : "FALSE").Append(", ");
                }
            if (build.Length-data.TableName.Length > 11) build.Remove(build.Length - 2, 1);
            build.Append(" WHERE InternalID = ").Append(data.InternalID);
            return Dal.ExecuteSql(build.ToString());
        }

        public static bool Delete(DalSendable data) => Dal.ExecuteSql("DELETE FROM " + data.TableName + "WHERE ID = " + data.InternalID);
        
        public static DataTable GetByInternalID(DalSendable objectFrame) {
            return Dal.GetCustomDataTable(objectFrame.TableName,
                "SELECT * FROM" + objectFrame.TableName + "WHERE InternalID = " + objectFrame.InternalID + ";");
        }

        public static DataTable GetAllByField(DalSendable objectFrame, string fieldName, string sqlVal) {
            return Dal.GetCustomDataTable(objectFrame.TableName, 
                "SELECT * FROM" + objectFrame.TableName + "WHERE " + fieldName + "= " + sqlVal + ";");
        }

        public static DataTable GetTableCustomSql(DalSendable objectFrame, string sqlCondition) {
            return Dal.GetCustomDataTable(objectFrame.TableName, 
                "SELECT * FROM" + objectFrame.TableName + "WHERE " + sqlCondition + ";");
        }

    }   
} 
