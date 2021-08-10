using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.daoObjects;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Netus2_DatabaseConnection.daoImplementations
{
    public class LineItemDaoImpl : ILineItemDao
    {
        DaoObjectMapper daoObjectMapper = new DaoObjectMapper();

        public void Delete(LineItem lineItem, IConnectable connection)
        {
            Delete_Mark(lineItem, connection);

            LineItemDao lineItemDao = daoObjectMapper.MapLineItem(lineItem);

            StringBuilder sql = new StringBuilder("DELETE FROM lineitem WHERE 1=1 ");
            sql.Append("AND lineitem_id = " + lineItemDao.lineitem_id + " ");
            sql.Append("AND name " + (lineItemDao.name != null ? "= '" + lineItemDao.name + "' " : "IS NULL "));
            sql.Append("AND descript " + (lineItemDao.descript != null ? "= '" + lineItemDao.descript + "' " : "IS NULL "));
            sql.Append("AND assign_date " + (lineItemDao.assign_date != null ? "= '" + lineItemDao.assign_date + "' " : "IS NULL "));
            sql.Append("AND due_date " + (lineItemDao.due_date != null ? "= '" + lineItemDao.due_date + "' " : "IS NULL "));
            sql.Append("AND class_id " + (lineItemDao.class_id != null ? "= " + lineItemDao.class_id + " " : "IS NULL "));
            sql.Append("AND enum_category_id " + (lineItemDao.enum_category_id != null ? "= " + lineItemDao.enum_category_id + " " : "IS NULL "));
            sql.Append("AND markValueMin " + (lineItemDao.markValueMin != null ? "= " + lineItemDao.markValueMin + " " : "IS NULL "));
            sql.Append("AND markValueMax " + (lineItemDao.markValueMax != null ? "= " + lineItemDao.markValueMax + " " : "IS NULL "));

            connection.ExecuteNonQuery(sql.ToString());
        }

        private void Delete_Mark(LineItem lineItem, IConnectable connection)
        {
            IMarkDao markDaoImpl = DaoImplFactory.GetMarkDaoImpl();
            List<Mark> foundMarks = markDaoImpl.Read_WithLineItemId(lineItem.Id, connection);
            foreach (Mark foundMark in foundMarks)
            {
                markDaoImpl.Delete(foundMark, connection);
            }
        }

        public List<LineItem> Read(LineItem lineItem, IConnectable connection)
        {
            LineItemDao lineItemDao = daoObjectMapper.MapLineItem(lineItem);

            StringBuilder sql = new StringBuilder("SELECT * FROM lineItem WHERE 1=1 ");
            if (lineItemDao.lineitem_id != null)
                sql.Append("AND lineItem_id = " + lineItemDao.lineitem_id + " ");
            else
            {
                if (lineItemDao.name != null)
                    sql.Append("AND name = '" + lineItemDao.name + "' ");
                if (lineItemDao.descript != null)
                    sql.Append("AND descript = '" + lineItemDao.descript + "' ");
                if (lineItemDao.assign_date != null)
                    sql.Append("AND assign_date = '" + lineItemDao.assign_date.ToString() + "' ");
                if (lineItemDao.due_date != null)
                    sql.Append("AND due_date = '" + lineItemDao.due_date.ToString() + "' ");
                if (lineItemDao.class_id != null)
                    sql.Append("AND class_id = " + lineItemDao.class_id + " ");
                if (lineItemDao.enum_category_id != null)
                    sql.Append("AND enum_category_id = " + lineItemDao.enum_category_id + " ");
                if (lineItemDao.markValueMin != null)
                    sql.Append("AND markValueMin = " + lineItemDao.markValueMin + " ");
                if (lineItemDao.markValueMax != null)
                    sql.Append("AND markValueMax = " + lineItemDao.markValueMax + " ");
            }

            return Read(sql.ToString(), connection);
        }

        public LineItem Read(int lineItemId, IConnectable connection)
        {
            string sql = "SELECT * FROM lineItem WHERE lineItem_id = " + lineItemId;
            List<LineItem> results = Read(sql, connection);
            if (results.Count > 0)
                return results[0];
            else
                return null;
        }

        public List<LineItem> Read_WithClassEnrolledId(int classEnrolledId, IConnectable connection)
        {
            string sql = "SELECT * FROM lineitem WHERE class_id = " + classEnrolledId;

            return Read(sql, connection);
        }

        private List<LineItem> Read(string sql, IConnectable connection)
        {
            List<LineItemDao> foundLineItemDaos = new List<LineItemDao>();

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    LineItemDao foundLineItemDao = new LineItemDao();

                    List<string> columnNames = new List<string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                        columnNames.Add(reader.GetName(i));

                    foreach (string columnName in columnNames)
                    {
                        var value = reader.GetValue(reader.GetOrdinal(columnName));
                        switch (columnName)
                        {
                            case "lineitem_id":
                                if (value != DBNull.Value)
                                    foundLineItemDao.lineitem_id = (int)value;
                                else
                                    foundLineItemDao.lineitem_id = null;
                                break;
                            case "name":
                                foundLineItemDao.name = value != DBNull.Value ? (string)value : null;
                                break;
                            case "descript":
                                foundLineItemDao.descript = value != DBNull.Value ? (string)value : null;
                                break;
                            case "assign_date":
                                if (value != DBNull.Value)
                                    foundLineItemDao.assign_date = (DateTime)value;
                                else
                                    foundLineItemDao.assign_date = null;
                                break;
                            case "due_date":
                                if (value != DBNull.Value)
                                    foundLineItemDao.due_date = (DateTime)value;
                                else
                                    foundLineItemDao.due_date = null;
                                break;
                            case "class_id":
                                if (value != DBNull.Value)
                                    foundLineItemDao.class_id = (int)value;
                                else
                                    foundLineItemDao.class_id = null;
                                break;
                            case "enum_category_id":
                                if (value != DBNull.Value)
                                    foundLineItemDao.enum_category_id = (int)value;
                                else
                                    foundLineItemDao.enum_category_id = null;
                                break;
                            case "markValueMin":
                                if (value != DBNull.Value)
                                    foundLineItemDao.markValueMin = (double)value;
                                else
                                    foundLineItemDao.markValueMin = null;
                                break;
                            case "markValueMax":
                                if (value != DBNull.Value)
                                    foundLineItemDao.markValueMax = (double)value;
                                else
                                    foundLineItemDao.markValueMax = null;
                                break;
                            case "created":
                                if (value != DBNull.Value)
                                    foundLineItemDao.created = (DateTime)value;
                                else
                                    foundLineItemDao.created = null;
                                break;
                            case "created_by":
                                foundLineItemDao.created_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case "changed":
                                if (value != DBNull.Value)
                                    foundLineItemDao.changed = (DateTime)value;
                                else
                                    foundLineItemDao.changed = null;
                                break;
                            case "changed_by":
                                foundLineItemDao.changed_by = value != DBNull.Value ? (string)value : null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in lineItem table: " + columnName);
                        }
                    }
                    foundLineItemDaos.Add(foundLineItemDao);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            List<LineItem> results = new List<LineItem>();
            foreach (LineItemDao foundLineItemDao in foundLineItemDaos)
            {
                ClassEnrolled classEnrolled = Read_ClassEnrolled((int)foundLineItemDao.class_id, connection);
                results.Add(daoObjectMapper.MapLineItem(foundLineItemDao, classEnrolled));
            }

            return results;
        }

        private ClassEnrolled Read_ClassEnrolled(int classId, IConnectable connection)
        {
            IClassEnrolledDao classEnrolledDaoImpl = DaoImplFactory.GetClassEnrolledDaoImpl();
            return classEnrolledDaoImpl.Read(classId, connection);
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
            LineItemDao lineItemDao = daoObjectMapper.MapLineItem(lineItem);

            if(lineItemDao.lineitem_id != null)
            {
                StringBuilder sql = new StringBuilder("UPDATE lineItem SET ");
                sql.Append("name = " + (lineItemDao.name != null ? "'" + lineItemDao.name + "', " : "NULL, "));
                sql.Append("descript = " + (lineItemDao.descript != null ? "'" + lineItemDao.descript + "', " : "NULL, "));
                sql.Append("assign_date = " + (lineItemDao.assign_date != null ? "'" + lineItemDao.assign_date.ToString() + "', " : "NULL, "));
                sql.Append("due_date = " + (lineItemDao.due_date != null ? "'" + lineItemDao.due_date.ToString() + "', " : "NULL, "));
                sql.Append("class_id = " + (lineItemDao.class_id != null ? lineItemDao.class_id + ", " : "NULL, "));
                sql.Append("enum_category_id = " + (lineItemDao.enum_category_id != null ? lineItemDao.enum_category_id + ", " : "NULL, "));
                sql.Append("markValueMin = " + (lineItemDao.markValueMin != null ? lineItemDao.markValueMin + ", " : "NULL, "));
                sql.Append("markValueMax = " + (lineItemDao.markValueMax != null ? lineItemDao.markValueMax + ", " : "NULL, "));
                sql.Append("changed = GETDATE(), ");
                sql.Append("changed_by = 'Netus2' ");
                sql.Append("WHERE lineItem_id = " + lineItemDao.lineitem_id);

                connection.ExecuteNonQuery(sql.ToString());
            }
            else
            {
                throw new Exception("The following LineItem needs to be inserted into the database, before it can be updated.\n" + lineItem.ToString());
            }
        }

        public LineItem Write(LineItem lineItem, IConnectable connection)
        {
            LineItemDao lineItemDao = daoObjectMapper.MapLineItem(lineItem);

            StringBuilder sqlValues = new StringBuilder();
            sqlValues.Append(lineItemDao.name != null ? "'" + lineItemDao.name + "', " : "NULL, ");
            sqlValues.Append(lineItemDao.descript != null ? "'" + lineItemDao.descript + "', " : "NULL, ");
            sqlValues.Append(lineItemDao.assign_date != null ? "'" + lineItemDao.assign_date.ToString() + "', " : "NULL, ");
            sqlValues.Append(lineItemDao.due_date != null ? "'" + lineItemDao.due_date.ToString() + "', " : "NULL, ");
            sqlValues.Append(lineItemDao.class_id != null ? lineItemDao.class_id + ", " : "NULL, ");
            sqlValues.Append(lineItemDao.enum_category_id != null ? lineItemDao.enum_category_id + ", " : "NULL, ");
            sqlValues.Append(lineItemDao.markValueMin != null ? lineItemDao.markValueMin + ", " : "NULL, ");
            sqlValues.Append(lineItemDao.markValueMax != null ? lineItemDao.markValueMax + ", " : "NULL, ");
            sqlValues.Append("GETDATE(), ");
            sqlValues.Append("'Netus2'");

            string sql =
                "INSERT INTO lineItem " +
                "(name, descript, assign_date, due_date, class_id, enum_category_id, markValueMin, markValueMax, created, created_by) " +
                "VALUES (" + sqlValues.ToString() + ")";

            lineItemDao.lineitem_id = connection.InsertNewRecord(sql);

            ClassEnrolled classEnrolled = Read_ClassEnrolled((int)lineItemDao.class_id, connection);
            LineItem result = daoObjectMapper.MapLineItem(lineItemDao, classEnrolled);

            return result;
        }
    }
}
