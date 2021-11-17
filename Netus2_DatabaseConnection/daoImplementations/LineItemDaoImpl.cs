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
            string sql = "SELECT * FROM lineitem WHERE class_enrolled_id = @class_enrolled_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@class_enrolled_id", classEnrolledId));

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

                if (row["class_enrolled_id"] != DBNull.Value)
                {
                    sql.Append("AND class_enrolled_id = @class_enrolled_id ");
                    parameters.Add(new SqlParameter("@class_enrolled_id", row["class_enrolled_id"]));
                }

                if (row["enum_category_id"] != DBNull.Value)
                {
                    sql.Append("AND enum_category_id = @enum_category_id ");
                    parameters.Add(new SqlParameter("@enum_category_id", row["enum_category_id"]));
                }

                if (row["mark_min"] != DBNull.Value)
                {
                    sql.Append("AND mark_min = @mark_min ");
                    parameters.Add(new SqlParameter("@mark_min", row["mark_min"]));
                }

                if (row["mark_max"] != DBNull.Value)
                {
                    sql.Append("AND mark_max = @mark_max ");
                    parameters.Add(new SqlParameter("@mark_max", row["mark_max"]));
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
                ClassEnrolled classEnrolled = Read_ClassEnrolled((int)row["class_enrolled_id"], connection);
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

                if (row["class_enrolled_id"] != DBNull.Value)
                {
                    sql.Append("class_enrolled_id = @class_enrolled_id, ");
                    parameters.Add(new SqlParameter("@class_enrolled_id", row["class_enrolled_id"]));
                }
                else
                    sql.Append("class_enrolled_id = NULL, ");

                if (row["enum_category_id"] != DBNull.Value)
                {
                    sql.Append("enum_category_id = @enum_category_id, ");
                    parameters.Add(new SqlParameter("@enum_category_id", row["enum_category_id"]));
                }
                else
                    sql.Append("enum_category_id = NULL, ");

                if (row["mark_min"] != DBNull.Value)
                {
                    sql.Append("mark_min = @mark_min, ");
                    parameters.Add(new SqlParameter("@mark_min", row["mark_min"]));
                }
                else
                    sql.Append("mark_min = NULL, ");

                if (row["mark_max"] != DBNull.Value)
                {
                    sql.Append("mark_max = @mark_max, ");
                    parameters.Add(new SqlParameter("@mark_max", row["mark_max"]));
                }
                else
                    sql.Append("mark_max = NULL, ");

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

            if (row["class_enrolled_id"] != DBNull.Value)
            {
                sqlValues.Append("@class_enrolled_id, ");
                parameters.Add(new SqlParameter("@class_enrolled_id", row["class_enrolled_id"]));
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

            if (row["mark_min"] != DBNull.Value)
            {
                sqlValues.Append("@mark_min, ");
                parameters.Add(new SqlParameter("@mark_min", row["mark_min"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["mark_max"] != DBNull.Value)
            {
                sqlValues.Append("@mark_max, ");
                parameters.Add(new SqlParameter("@mark_max", row["mark_max"]));
            }
            else
                sqlValues.Append("NULL, ");

            sqlValues.Append("dbo.CURRENT_DATETIME(), ");
            sqlValues.Append(_taskId != null ? _taskId.ToString() : "'Netus2'");

            string sql =
                "INSERT INTO lineItem " +
                "(name, descript, assign_date, due_date, class_enrolled_id, enum_category_id, " +
                "mark_min, mark_max, created, created_by) " +
                "VALUES (" + sqlValues.ToString() + ")";

            row["lineitem_id"] = connection.InsertNewRecord(sql, parameters);

            ClassEnrolled classEnrolled = Read_ClassEnrolled((int)row["class_enrolled_id"], connection);
            LineItem result = daoObjectMapper.MapLineItem(row, classEnrolled);

            return result;
        }
    }
}
