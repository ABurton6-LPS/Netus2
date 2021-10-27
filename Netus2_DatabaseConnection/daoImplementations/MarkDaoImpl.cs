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
    public class MarkDaoImpl : IMarkDao
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

        public void Delete(Mark mark, IConnectable connection)
        {
            if (mark.Id <= 0)
                throw new Exception("Cannot delete a mark which doesn't have a database-assigned ID.\n" + mark.ToString());

            string sql = "DELETE FROM mark WHERE " +
            "mark_id = @mark_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@mark_id", mark.Id));

            connection.ExecuteNonQuery(sql, parameters);
        }

        public List<Mark> Read_AllWithLineItemId(int lineItemId, IConnectable connection)
        {
            string sql = "SELECT * FROM mark WHERE lineItem_id = @lineItem_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@lineItem_id", lineItemId));

            return Read(sql, connection, parameters);
        }

        public List<Mark> Read_AllWithPersonId(int personId, IConnectable connection)
        {
            string sql = "SELECT * FROM mark WHERE person_id = @person_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_id", personId));

            return Read(sql, connection, parameters);
        }

        public List<Mark> Read(Mark mark, int personId, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapMark(mark, personId);

            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sql = new StringBuilder("SELECT * FROM mark WHERE 1=1 ");
            if (row["mark_id"] != DBNull.Value)
            {
                sql.Append("AND mark_id = @mark_id");
                parameters.Add(new SqlParameter("@mark_id", row["mark_id"]));
            }
            else
            {
                if (row["lineitem_id"] != DBNull.Value)
                {
                    sql.Append("AND lineitem_id = @lineitem_id ");
                    parameters.Add(new SqlParameter("@lineitem_id", row["lineitem_id"]));
                }

                if (row["person_id"] != DBNull.Value)
                {
                    sql.Append("AND person_id = @person_id ");
                    parameters.Add(new SqlParameter("@person_id", row["person_id"]));
                }

                if (row["enum_score_status_id"] != DBNull.Value)
                {
                    sql.Append("AND enum_score_status_id = @enum_score_status_id ");
                    parameters.Add(new SqlParameter("@enum_score_status_id", row["enum_score_status_id"]));
                }

                if (row["score"] != DBNull.Value)
                {
                    sql.Append("AND score = @score ");
                    parameters.Add(new SqlParameter("@score", row["score"]));
                }

                if (row["score_date"] != DBNull.Value)
                {
                    sql.Append("AND score_date = @score_date ");
                    parameters.Add(new SqlParameter("@score_date", row["score_date"]));
                }

                if (row["comment"] != DBNull.Value)
                {
                    sql.Append("AND comment = @comment ");
                    parameters.Add(new SqlParameter("@comment", row["comment"]));
                }
            }

            return Read(sql.ToString(), connection, parameters);
        }

        private List<Mark> Read(string sql, IConnectable connection, List<SqlParameter> parameters)
        {
            DataTable dtMark = DataTableFactory.CreateDataTable_Netus2_Mark();
            dtMark = connection.ReadIntoDataTable(sql, dtMark, parameters);

            List<Mark> results = new List<Mark>();
            foreach (DataRow row in dtMark.Rows)
            {
                LineItem lineItem = Read_LineItem((int)row["lineitem_id"], connection);
                results.Add(daoObjectMapper.MapMark(row, lineItem));
            }

            return results;
        }

        private LineItem Read_LineItem(int lineItemId, IConnectable connection)
        {
            ILineItemDao lineItemDaoImpl = DaoImplFactory.GetLineItemDaoImpl();
            return lineItemDaoImpl.Read_UsingLineItemId(lineItemId, connection);
        }

        public void Update(Mark mark, int personId, IConnectable connection)
        {
            List<Mark> foundMarks = Read(mark, personId, connection);
            if (foundMarks.Count == 0)
                throw new Exception("This new Mark must first be saved to the database before it can be attached to a student.\n" + mark.ToString());
            else if (foundMarks.Count == 1)
            {
                mark.Id = foundMarks[0].Id;
                UpdateInternals(mark, personId, connection);
            }
            else if (foundMarks.Count > 1)
                throw new Exception(foundMarks.Count + " Marks found matching the description of:\n" +
                    mark.ToString());

        }

        private void UpdateInternals(Mark mark, int personId, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapMark(mark, personId);

            if (row["mark_id"] != DBNull.Value)
            {
                List<SqlParameter> parameters = new List<SqlParameter>();

                StringBuilder sql = new StringBuilder("UPDATE mark SET ");
                if (row["lineitem_id"] != DBNull.Value)
                {
                    sql.Append("lineitem_id = @lineitem_id, ");
                    parameters.Add(new SqlParameter("@lineitem_id", row["lineitem_id"]));
                }
                else
                    sql.Append("lineitem_id = NULL, ");

                if (row["person_id"] != DBNull.Value)
                {
                    sql.Append("person_id = @person_id, ");
                    parameters.Add(new SqlParameter("@person_id", row["person_id"]));
                }
                else
                    sql.Append("person_id = NULL, ");

                if (row["enum_score_status_id"] != DBNull.Value)
                {
                    sql.Append("enum_score_status_id = @enum_score_status_id, ");
                    parameters.Add(new SqlParameter("@enum_score_status_id", row["enum_score_status_id"]));
                }
                else
                    sql.Append("enum_score_status_id = NULL, ");

                if (row["score"] != DBNull.Value)
                {
                    sql.Append("score = @score, ");
                    parameters.Add(new SqlParameter("@score", row["score"]));
                }
                else
                    sql.Append("score = NULL, ");

                if (row["score_date"] != DBNull.Value)
                {
                    sql.Append("score_date = @score_date, ");
                    parameters.Add(new SqlParameter("@score_date", row["score_date"]));
                }
                else
                    sql.Append("score_date = NULL, ");

                if (row["comment"] != DBNull.Value)
                {
                    sql.Append("comment = @comment, ");
                    parameters.Add(new SqlParameter("@comment", row["comment"]));
                }
                else
                    sql.Append("comment = NULL, ");

                sql.Append("changed = dbo.CURRENT_DATETIME(), ");
                sql.Append("changed_by = " + (_taskId != null ? _taskId.ToString() : "'Netus2'") + " ");
                sql.Append("WHERE mark_id = @mark_id");
                parameters.Add(new SqlParameter("@mark_id", row["mark_id"]));

                connection.ExecuteNonQuery(sql.ToString(), parameters);
            }
            else
                throw new Exception("The following Mark needs to be inserted into the database, before it can be updated.\n" + mark.ToString());
        }

        public Mark Write(Mark mark, int personId, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapMark(mark, personId);

            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sqlValues = new StringBuilder();
            if (row["lineitem_id"] != DBNull.Value)
            {
                sqlValues.Append("@lineitem_id, ");
                parameters.Add(new SqlParameter("@lineitem_id", row["lineitem_id"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["person_id"] != DBNull.Value)
            {
                sqlValues.Append("@person_id, ");
                parameters.Add(new SqlParameter("@person_id", row["person_id"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["enum_score_status_id"] != DBNull.Value)
            {
                sqlValues.Append("@enum_score_status_id, ");
                parameters.Add(new SqlParameter("@enum_score_status_id", row["enum_score_status_id"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["score"] != DBNull.Value)
            {
                sqlValues.Append("@score, ");
                parameters.Add(new SqlParameter("@score", row["score"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["score_date"] != DBNull.Value)
            {
                sqlValues.Append("@score_date, ");
                parameters.Add(new SqlParameter("@score_date", row["score_date"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["comment"] != DBNull.Value)
            {
                sqlValues.Append("@comment, ");
                parameters.Add(new SqlParameter("@comment", row["comment"]));
            }
            else
                sqlValues.Append("NULL, ");

            sqlValues.Append("dbo.CURRENT_DATETIME(), ");
            sqlValues.Append(_taskId != null ? _taskId.ToString() : "'Netus2'");

            string sql =
                "INSERT INTO mark " +
                "(lineitem_id, person_id, enum_score_status_id, score, score_date, comment, created, created_by) " +
                "VALUES (" + sqlValues.ToString() + ")";

            row["mark_id"] = connection.InsertNewRecord(sql, parameters);

            LineItem lineItem = Read_LineItem((int)row["lineitem_id"], connection);
            Mark result = daoObjectMapper.MapMark(row, lineItem);

            return result;
        }
    }
}
