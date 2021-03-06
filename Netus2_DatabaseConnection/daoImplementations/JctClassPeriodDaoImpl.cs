using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Netus2_DatabaseConnection.daoImplementations
{
    public class JctClassPeriodDaoImpl : IJctClassPeriodDao
    {
        public void Delete(int classId, int periodId, IConnectable connection)
        {
            if (classId <= 0 || periodId < 0)
                throw new Exception("Cannot delete a record from jct_class_enrolled_period " +
                    "without a database-assigned ID for both classId and periodId." +
                    "\nclassId: " + classId +
                    "\nperiodId: " + periodId);

            string sql = "DELETE FROM jct_class_enrolled_period WHERE 1=1 " +
                "AND class_enrolled_id = @class_enrolled_id " +
                "AND enum_period_id = @enum_period_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@class_enrolled_id", classId));
            parameters.Add(new SqlParameter("@enum_period_id", periodId));

            connection.ExecuteNonQuery(sql, parameters);
        }

        public DataRow Read(int classId, int periodId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_class_enrolled_period WHERE 1=1 " +
            "AND class_enrolled_id = @class_enrolled_id " +
            "AND enum_period_id = @enum_period_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@class_enrolled_id", classId));
            parameters.Add(new SqlParameter("@enum_period_id", periodId));

            List<DataRow> results = Read(sql, connection, parameters);

            if (results.Count == 0)
                return null;
            else if (results.Count == 1)
                return results[0];
            else
                throw new Exception(results.Count + " found matching classId: " + classId + " and periodId: " + periodId);
        }

        public List<DataRow> Read_AllWithClassId(int classId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_class_enrolled_period WHERE " +
                "class_enrolled_id = @class_enrolled_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@class_enrolled_id", classId));

            return Read(sql, connection, parameters);
        }

        public List<DataRow> Read_AllWithPeriodId(int periodId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_class_enrolled_period WHERE " +
                "period_id = @period_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@period_id", periodId));

            return Read(sql, connection, parameters);
        }

        private List<DataRow> Read(string sql, IConnectable connection, List<SqlParameter> parameters)
        {
            DataTable dtJctClassPeriod = DataTableFactory.CreateDataTable_Netus2_JctClassPeriod();
            dtJctClassPeriod = connection.ReadIntoDataTable(sql, dtJctClassPeriod, parameters);

            List<DataRow> foundDataRows = new List<DataRow>();
            foreach (DataRow row in dtJctClassPeriod.Rows)
                foundDataRows.Add(row);

            return foundDataRows;
        }

        public DataRow Write(int classId, int periodId, IConnectable connection)
        {
            string sql = "INSERT INTO jct_class_enrolled_period (" +
                "class_enrolled_id, enum_period_id) VALUES (" +
                "@class_enrolled_id, @period_id)";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@class_enrolled_id", classId));
            parameters.Add(new SqlParameter("@period_id", periodId));

            connection.ExecuteNonQuery(sql, parameters);

            DataRow jctClassPeriodDao = DataTableFactory.CreateDataTable_Netus2_JctClassPeriod().NewRow();
            jctClassPeriodDao["class_enrolled_id"] = classId;
            jctClassPeriodDao["enum_period_id"] = periodId;

            return jctClassPeriodDao;
        }
    }
}
