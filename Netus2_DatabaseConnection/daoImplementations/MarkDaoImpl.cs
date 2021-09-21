using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Netus2_DatabaseConnection.daoImplementations
{
    public class MarkDaoImpl : IMarkDao
    {
        DaoObjectMapper daoObjectMapper = new DaoObjectMapper();

        public void Delete(Mark mark, IConnectable connection)
        {
            Delete(mark, -1, connection);
        }

        public void Delete(Mark mark, int personId, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapMark(mark, personId);

            StringBuilder sql = new StringBuilder("DELETE FROM mark WHERE 1=1 ");
            sql.Append("AND mark_id = " + row["mark_id"] + " ");
            sql.Append("AND lineitem_id " + (row["lineitem_id"] != DBNull.Value ? "= " + row["lineitem_id"] + " " : "IS NULL "));
            if (row["person_id"] != DBNull.Value)
                sql.Append("AND person_id = " + row["person_id"] + " ");
            sql.Append("AND enum_score_status_id " + (row["enum_score_status_id"] != DBNull.Value ? "= " + row["enum_score_status_id"] + " " : "IS NULL "));
            sql.Append("AND score " + (row["score"] != DBNull.Value ? "= " + row["score"] + " " : "IS NULL "));
            sql.Append("AND score_date " + (row["score_date"] != DBNull.Value ? "= '" + row["score_date"] + "' " : "IS NULL "));
            sql.Append("AND comment " + (row["comment"] != DBNull.Value ? "= '" + row["comment"] + "' " : "IS NULL "));

            connection.ExecuteNonQuery(sql.ToString());
        }

        public List<Mark> Read(Mark mark, int personId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM mark WHERE 1=1 ");

            if (mark == null)
            {
                sql.Append("AND person_id = " + personId);
            }
            else
            {
                DataRow row = daoObjectMapper.MapMark(mark, personId);

                if (row["mark_id"] != DBNull.Value)
                    sql.Append("AND mark_id = " + row["mark_id"] + " ");
                else
                {
                    if (row["lineitem_id"] != DBNull.Value)
                        sql.Append("AND lineitem_id = " + row["lineitem_id"] + " ");
                    if (row["person_id"] != DBNull.Value)
                        sql.Append("AND person_id = " + row["person_id"] + " ");
                    if (row["enum_score_status_id"] != DBNull.Value)
                        sql.Append("AND enum_score_status_id = " + row["enum_score_status_id"] + " ");
                    if (row["score"] != DBNull.Value)
                        sql.Append("AND score = " + row["score"] + " ");
                    if (row["score_date"] != DBNull.Value)
                        sql.Append("AND score_date = '" + row["score_date"].ToString() + "' ");
                    if (row["comment"] != DBNull.Value)
                        sql.Append("AND comment = '" + row["comment"] + "' ");
                }
            }

            return Read(sql.ToString(), connection);
        }

        public List<Mark> Read_WithLineItemId(int lineItemId, IConnectable connection)
        {
            string sql = "SELECT * FROM mark WHERE lineitem_id = " + lineItemId;

            return Read(sql, connection);
        }

        private List<Mark> Read(string sql, IConnectable connection)
        {
            DataTable dtMark = new DataTableFactory().Dt_Netus2_Mark;
            dtMark = connection.ReadIntoDataTable(sql, dtMark);

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
            return lineItemDaoImpl.Read(lineItemId, connection);
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
                StringBuilder sql = new StringBuilder("UPDATE mark SET ");
                sql.Append("lineitem_id = " + (row["lineitem_id"] != DBNull.Value ? row["lineitem_id"] + ", " : "NULL, "));
                sql.Append("person_id = " + (row["person_id"] != DBNull.Value ? row["person_id"] + ", " : "NULL, "));
                sql.Append("enum_score_status_id = " + (row["enum_score_status_id"] != DBNull.Value ? row["enum_score_status_id"] + ", " : "NULL, "));
                sql.Append("score = " + (row["score"] != DBNull.Value ? row["score"] + ", " : "NULL, "));
                sql.Append("score_date = " + (row["score_date"] != DBNull.Value ? "'" + row["score_date"] + "', " : "NULL, "));
                sql.Append("comment = " + (row["comment"] != DBNull.Value ? "'" + row["comment"] + "', " : "NULL, "));
                sql.Append("changed = GETDATE(), ");
                sql.Append("changed_by = 'Netus2' ");
                sql.Append("WHERE mark_id = " + row["mark_id"]);

                connection.ExecuteNonQuery(sql.ToString());
            }
            else
                throw new Exception("The following Mark needs to be inserted into the database, before it can be updated.\n" + mark.ToString());
        }

        public Mark Write(Mark mark, int personId, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapMark(mark, personId);

            StringBuilder sqlValues = new StringBuilder();
            sqlValues.Append(row["lineitem_id"] != DBNull.Value ? row["lineitem_id"] + ", " : "NULL, ");
            sqlValues.Append(row["person_id"] != DBNull.Value ? row["person_id"] + ", " : "NULL, ");
            sqlValues.Append(row["enum_score_status_id"] != DBNull.Value ? row["enum_score_status_id"] + ", " : "NULL, ");
            sqlValues.Append(row["score"] != DBNull.Value ? row["score"] + ", " : "NULL, ");
            sqlValues.Append(row["score_date"] != DBNull.Value ? "'" + row["score_date"].ToString() + "', " : "NULL, ");
            sqlValues.Append(row["comment"] != DBNull.Value ? "'" + row["comment"] + "', " : "NULL, ");
            sqlValues.Append("GETDATE(), ");
            sqlValues.Append("'Netus2'");

            string sql =
                "INSERT INTO mark " +
                "(lineitem_id, person_id, enum_score_status_id, score, score_date, comment, created, created_by) " +
                "VALUES (" + sqlValues.ToString() + ")";

            row["mark_id"] = connection.InsertNewRecord(sql);

            LineItem lineItem = Read_LineItem((int)row["lineitem_id"], connection);

            Mark result = daoObjectMapper.MapMark(row, lineItem);

            return result;
        }
    }
}
