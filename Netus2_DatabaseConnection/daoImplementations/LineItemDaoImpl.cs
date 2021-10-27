using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Netus2_DatabaseConnection.daoImplementations
{
    public class LineItemDaoImpl : ILineItemDao
    {
        DaoObjectMapper daoObjectMapper = new DaoObjectMapper();
        public int? _taskId = null;

        public void SetTaskId(int taskId)
        {
            _taskId = taskId;
        }

        public int? GetTaskId()
        {
            return _taskId;
        }

        public void Delete(LineItem lineItem, IConnectable connection)
        {
            if (lineItem.Id <= 0)
                throw new Exception("Cannot delete a lineItem which doesn't have a database-assigned ID.\n" + lineItem.ToString());

            Delete_Mark(lineItem, connection);

            string sql = "DELETE FROM lineitem WHERE " +
                "lineitem_id = @lineitem_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@lineitem_id", lineItem.Id));

            connection.ExecuteNonQuery(sql, parameters);
        }

        private void Delete_Mark(LineItem lineItem, IConnectable connection)
        {
            IMarkDao markDaoImpl = DaoImplFactory.GetMarkDaoImpl();
            List<Mark> foundMarks = markDaoImpl.Read_AllWithLineItemId(lineItem.Id, connection);
            foreach (Mark foundMark in foundMarks)
            {
                markDaoImpl.Delete(foundMark, connection);
            }
        }

        public LineItem Read_UsingLineItemId(int lineItemId, IConnectable connection)
        {
            string sql = "SELECT * FROM lineItem WHERE lineitem_id = @lineitem_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@lineitem_id", lineItemId));

            List<LineItem> results = Read(sql, connection, parameters);

            if (results.Count == 0)
                return null;
            else if (results.Count == 1)
                return results[0];
            else
                throw new Exception(results.Count + " found matching lineItemId: " + lineItemId);
        }

        public List<LineItem> Read_AllWithClassEnrolledId(int classEnrolledId, IConnectable connection)
        {
            string sql = "SELECT * FROM lineitem WHERE class_id = @class_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@class_id", classEnrolledId));

            return Read(sql, connection, parameters);
        }

        public List<LineItem> Read(LineItem lineItem, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapLineItem(lineItem);

            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sql = new StringBuilder("SELECT * FROM lineItem WHERE 1=1 ");
            if (row["lineitem_id"] != DBNull.Value)
            {
                sql.Append("AND lineItem_id = @lineitem_id ");
                parameters.Add(new SqlParameter("@lineitem_id", row["lineitem_id"]));
            }
            else
            {
                if (row["name"] != DBNull.Value)
                {
                    sql.Append("AND name = @name ");
                    parameters.Add(new SqlParameter("@name", row["name"]));
                }

                if (row["descript"] != DBNull.Value)
                {
                    sql.Append("AND descript = @descript ");
                    parameters.Add(new SqlParameter("@descript", row["descript"]));
                }

                if (row["assign_date"] != DBNull.Value)
                {
                    sql.Append("AND assign_date = @assign_date ");
                    parameters.Add(new SqlParameter("@assign_date", row["assign_date"]));
                }

                if (row["due_date"] != DBNull.Value)
                {
                    sql.Append("AND due_date = @due_date ");
                    parameters.Add(new SqlParameter("@due_date", row["due_date"]));
                }

                if (row["class_id"] != DBNull.Value)
                {
                    sql.Append("AND class_id = @class_id ");
                    parameters.Add(new SqlParameter("@class_id", row["class_id"]));
                }

                if (row["enum_category_id"] != DBNull.Value)
                {
                    sql.Append("AND enum_category_id = @enum_category_id ");
                    parameters.Add(new SqlParameter("@enum_category_id", row["enum_category_id"]));
                }

                if (row["markValueMin"] != DBNull.Value)
                {
                    sql.Append("AND markValueMin = @markValueMin ");
                    parameters.Add(new SqlParameter("@markValueMin", row["markValueMin"]));
                }

                if (row["markValueMax"] != DBNull.Value)
                {
                    sql.Append("AND markValueMax = @markValueMax ");
                    parameters.Add(new SqlParameter("@markValueMax", row["markValueMax"]));
                }
            }

            return Read(sql.ToString(), connection, parameters);
        }

        private List<LineItem> Read(string sql, IConnectable connection, List<SqlParameter> parameters)
        {
            DataTable dtLineItem = DataTableFactory.CreateDataTable_Netus2_LineItem();
            dtLineItem = connection.ReadIntoDataTable(sql, dtLineItem, parameters);

            List<LineItem> results = new List<LineItem>();
            foreach (DataRow row in dtLineItem.Rows)
            {
                ClassEnrolled classEnrolled = Read_ClassEnrolled((int)row["class_id"], connection);
                results.Add(daoObjectMapper.MapLineItem(row, classEnrolled));
            }

            return results;
        }

        private ClassEnrolled Read_ClassEnrolled(int classId, IConnectable connection)
        {
            IClassEnrolledDao classEnrolledDaoImpl = DaoImplFactory.GetClassEnrolledDaoImpl();
            return classEnrolledDaoImpl.Read_UsingClassId(classId, connection);
        }

        public void Update(LineItem lineItem, IConnectable connection)
        {
            List<LineItem> foundLineItems = Read(lineItem, connection);
            if (foundLineItems.Count == 0)
                Write(lineItem, connection);
            else if (foundLineItems.Count == 1)
            {
                lineItem.Id = foundLineItems[0].Id;
                UpdateInternals(lineItem, connection);
            }
            else
                throw new Exception(foundLineItems.Count + " LineItems found matching the description of:\n" +
                    lineItem.ToString());
        }

        private void UpdateInternals(LineItem lineItem, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapLineItem(lineItem);

            if(row["lineitem_id"] != DBNull.Value)
            {
                List<SqlParameter> parameters = new List<SqlParameter>();

                StringBuilder sql = new StringBuilder("UPDATE lineItem SET ");
                if (row["name"] != DBNull.Value)
                {
                    sql.Append("name = @name, ");
                    parameters.Add(new SqlParameter("@name", row["name"]));
                }
                else
                    sql.Append("name = NULL, ");

                if (row["descript"] != DBNull.Value)
                {
                    sql.Append("descript = @descript, ");
                    parameters.Add(new SqlParameter("@descript", row["descript"]));
                }
                else
                    sql.Append("descript = NULL, ");

                if (row["assign_date"] != DBNull.Value)
                {
                    sql.Append("assign_date = @assign_date, ");
                    parameters.Add(new SqlParameter("@assign_date", row["assign_date"]));
                }
                else
                    sql.Append("assign_date = NULL, ");

                if (row["due_date"] != DBNull.Value)
                {
                    sql.Append("due_date = @due_date, ");
                    parameters.Add(new SqlParameter("@due_date", row["due_date"]));
                }
                else
                    sql.Append("due_date = NULL, ");

                if (row["class_id"] != DBNull.Value)
                {
                    sql.Append("class_id = @class_id, ");
                    parameters.Add(new SqlParameter("@class_id", row["class_id"]));
                }
                else
                    sql.Append("class_id = NULL, ");

                if (row["enum_category_id"] != DBNull.Value)
                {
                    sql.Append("enum_category_id = @enum_category_id, ");
                    parameters.Add(new SqlParameter("@enum_category_id", row["enum_category_id"]));
                }
                else
                    sql.Append("enum_category_id = NULL, ");

                if (row["markValueMin"] != DBNull.Value)
                {
                    sql.Append("markValueMin = @markValueMin, ");
                    parameters.Add(new SqlParameter("@markValueMin", row["markValueMin"]));
                }
                else
                    sql.Append("markValueMin = NULL, ");

                if (row["markValueMax"] != DBNull.Value)
                {
                    sql.Append("markValueMax = @markValueMax, ");
                    parameters.Add(new SqlParameter("@markValueMax", row["markValueMax"]));
                }
                else
                    sql.Append("markValueMax = NULL, ");

                sql.Append("changed = dbo.CURRENT_DATETIME(), ");
                sql.Append("changed_by = " + (_taskId != null ? _taskId.ToString() : "'Netus2'") + " ");
                sql.Append("WHERE lineItem_id = @lineitem_id");
                parameters.Add(new SqlParameter("@lineitem_id", row["lineitem_id"]));

                connection.ExecuteNonQuery(sql.ToString(), parameters);
            }
            else
                throw new Exception("The following LineItem needs to be inserted into the database, before it can be updated.\n" + lineItem.ToString());
        }

        public LineItem Write(LineItem lineItem, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapLineItem(lineItem);

            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sqlValues = new StringBuilder();
            if (row["name"] != DBNull.Value)
            {
                sqlValues.Append("@name, ");
                parameters.Add(new SqlParameter("@name", row["name"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["descript"] != DBNull.Value)
            {
                sqlValues.Append("@descript, ");
                parameters.Add(new SqlParameter("@descript", row["descript"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["assign_date"] != DBNull.Value)
            {
                sqlValues.Append("@assign_date, ");
                parameters.Add(new SqlParameter("@assign_date", row["assign_date"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["due_date"] != DBNull.Value)
            {
                sqlValues.Append("@due_date, ");
                parameters.Add(new SqlParameter("@due_date", row["due_date"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["class_id"] != DBNull.Value)
            {
                sqlValues.Append("@class_id, ");
                parameters.Add(new SqlParameter("@class_id", row["class_id"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["enum_category_id"] != DBNull.Value)
            {
                sqlValues.Append("@enum_category_id, ");
                parameters.Add(new SqlParameter("@enum_category_id", row["enum_category_id"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["markValueMin"] != DBNull.Value)
            {
                sqlValues.Append("@markValueMin, ");
                parameters.Add(new SqlParameter("@markValueMin", row["markValueMin"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["markValueMax"] != DBNull.Value)
            {
                sqlValues.Append("@markValueMax, ");
                parameters.Add(new SqlParameter("@markValueMax", row["markValueMax"]));
            }
            else
                sqlValues.Append("NULL, ");

            sqlValues.Append("dbo.CURRENT_DATETIME(), ");
            sqlValues.Append(_taskId != null ? _taskId.ToString() : "'Netus2'");

            string sql =
                "INSERT INTO lineItem " +
                "(name, descript, assign_date, due_date, class_id, enum_category_id, " +
                "markValueMin, markValueMax, created, created_by) " +
                "VALUES (" + sqlValues.ToString() + ")";

            row["lineitem_id"] = connection.InsertNewRecord(sql, parameters);

            ClassEnrolled classEnrolled = Read_ClassEnrolled((int)row["class_id"], connection);
            LineItem result = daoObjectMapper.MapLineItem(row, classEnrolled);

            return result;
        }
    }
}
