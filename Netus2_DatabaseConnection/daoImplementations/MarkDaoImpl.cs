﻿using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.daoObjects;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
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
            MarkDao markDao = daoObjectMapper.MapMark(mark, personId);

            StringBuilder sql = new StringBuilder("DELETE FROM mark WHERE 1=1 ");
            sql.Append("AND mark_id = " + markDao.mark_id + " ");
            sql.Append("AND lineitem_id " + (markDao.lineitem_id != null ? "= " + markDao.lineitem_id + " " : "IS NULL "));
            if (markDao.person_id != null)
                sql.Append("AND person_id = " + markDao.person_id + " ");
            sql.Append("AND enum_score_status_id " + (markDao.enum_score_status_id != null ? "= " + markDao.enum_score_status_id + " " : "IS NULL "));
            sql.Append("AND score " + (markDao.score != null ? "= " + markDao.score + " " : "IS NULL "));
            sql.Append("AND score_date " + (markDao.score_date != null ? "= '" + markDao.score_date + "' " : "IS NULL "));
            sql.Append("AND comment " + (markDao.comment != null ? "= '" + markDao.comment + "' " : "IS NULL "));

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
                MarkDao markDao = daoObjectMapper.MapMark(mark, personId);

                if (markDao.mark_id != null)
                    sql.Append("AND mark_id = " + markDao.mark_id + " ");
                else
                {
                    if (markDao.lineitem_id != null)
                        sql.Append("AND lineitem_id = " + markDao.lineitem_id + " ");
                    if (markDao.person_id != null)
                        sql.Append("AND person_id = " + markDao.person_id + " ");
                    if (markDao.enum_score_status_id != null)
                        sql.Append("AND enum_score_status_id = " + markDao.enum_score_status_id + " ");
                    if (markDao.score != null)
                        sql.Append("AND score = " + markDao.score + " ");
                    if (markDao.score_date != null)
                        sql.Append("AND score_date = '" + markDao.score_date.ToString() + "' ");
                    if (markDao.comment != null)
                        sql.Append("AND comment = '" + markDao.comment + "' ");
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
            List<MarkDao> foundMarkDaos = new List<MarkDao>();

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    MarkDao foundMarkDao = new MarkDao();

                    List<string> columnNames = new List<string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                        columnNames.Add(reader.GetName(i));

                    foreach (string columnName in columnNames)
                    {
                        var value = reader.GetValue(reader.GetOrdinal(columnName));
                        switch (columnName)
                        {
                            case "mark_id":
                                if (value != DBNull.Value && value != null)
                                    foundMarkDao.mark_id = (int)value;
                                else
                                    foundMarkDao.mark_id = null;
                                break;
                            case "lineitem_id":
                                if (value != DBNull.Value && value != null)
                                    foundMarkDao.lineitem_id = (int)value;
                                else
                                    foundMarkDao.lineitem_id = null;
                                break;
                            case "person_id":
                                if (value != DBNull.Value && value != null)
                                    foundMarkDao.person_id = (int)value;
                                else
                                    foundMarkDao.person_id = null;
                                break;
                            case "enum_score_status_id":
                                if (value != DBNull.Value && value != null)
                                    foundMarkDao.enum_score_status_id = (int)value;
                                else
                                    foundMarkDao.enum_score_status_id = null;
                                break;
                            case "score":
                                if (value != DBNull.Value && value != null)
                                    foundMarkDao.score = (double)value;
                                else
                                    foundMarkDao.score = null;
                                break;
                            case "score_date":
                                if (value != DBNull.Value && value != null)
                                    foundMarkDao.score_date = (DateTime)value;
                                else
                                    foundMarkDao.score_date = null;
                                break;
                            case "comment":
                                foundMarkDao.comment = value != DBNull.Value ? (string)value : null;
                                break;
                            case "created":
                                if (value != DBNull.Value && value != null)
                                    foundMarkDao.created = (DateTime)value;
                                else
                                    foundMarkDao.created = null;
                                break;
                            case "created_by":
                                foundMarkDao.created_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case "changed":
                                if (value != DBNull.Value && value != null)
                                    foundMarkDao.changed = (DateTime)value;
                                else
                                    foundMarkDao.changed = null;
                                break;
                            case "changed_by":
                                foundMarkDao.changed_by = value != DBNull.Value ? (string)value : null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in mark table: " + columnName);
                        }
                    }
                    foundMarkDaos.Add(foundMarkDao);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            List<Mark> results = new List<Mark>();
            foreach (MarkDao foundMarkDao in foundMarkDaos)
            {
                LineItem lineItem = Read_LineItem((int)foundMarkDao.lineitem_id, connection);
                results.Add(daoObjectMapper.MapMark(foundMarkDao, lineItem));
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
            MarkDao markDao = daoObjectMapper.MapMark(mark, personId);

            if (markDao.mark_id != null)
            {
                StringBuilder sql = new StringBuilder("UPDATE mark SET ");
                sql.Append("lineitem_id = " + (markDao.lineitem_id != null ? markDao.lineitem_id + ", " : "NULL, "));
                sql.Append("person_id = " + (markDao.person_id != null ? markDao.person_id + ", " : "NULL, "));
                sql.Append("enum_score_status_id = " + (markDao.enum_score_status_id != null ? markDao.enum_score_status_id + ", " : "NULL, "));
                sql.Append("score = " + (markDao.score != null ? markDao.score + ", " : "NULL, "));
                sql.Append("score_date = " + (markDao.score_date != null ? "'" + markDao.score_date + "', " : "NULL, "));
                sql.Append("comment = " + (markDao.comment != null ? "'" + markDao.comment + "', " : "NULL, "));
                sql.Append("changed = GETDATE(), ");
                sql.Append("changed_by = 'Netus2' ");
                sql.Append("WHERE mark_id = " + markDao.mark_id);

                connection.ExecuteNonQuery(sql.ToString());
            }
            else
                throw new Exception("The following Mark needs to be inserted into the database, before it can be updated.\n" + mark.ToString());
        }

        public Mark Write(Mark mark, int personId, IConnectable connection)
        {
            MarkDao markDao = daoObjectMapper.MapMark(mark, personId);

            StringBuilder sqlValues = new StringBuilder();
            sqlValues.Append(markDao.lineitem_id != null ? markDao.lineitem_id + ", " : "NULL, ");
            sqlValues.Append(markDao.person_id != null ? markDao.person_id + ", " : "NULL, ");
            sqlValues.Append(markDao.enum_score_status_id != null ? markDao.enum_score_status_id + ", " : "NULL, ");
            sqlValues.Append(markDao.score != null ? markDao.score + ", " : "NULL, ");
            sqlValues.Append(markDao.score_date != null ? "'" + markDao.score_date.ToString() + "', " : "NULL, ");
            sqlValues.Append(markDao.comment != null ? "'" + markDao.comment + "', " : "NULL, ");
            sqlValues.Append("GETDATE(), ");
            sqlValues.Append("'Netus2'");

            string sql =
                "INSERT INTO mark " +
                "(lineitem_id, person_id, enum_score_status_id, score, score_date, comment, created, created_by) " +
                "VALUES (" + sqlValues.ToString() + ")";

            markDao.mark_id = connection.InsertNewRecord(sql);

            LineItem lineItem = Read_LineItem((int)markDao.lineitem_id, connection);

            Mark result = daoObjectMapper.MapMark(markDao, lineItem);

            return result;
        }
    }
}
