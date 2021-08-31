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
    public class LineItemDaoImpl : ILineItemDao
    {
        DaoObjectMapper daoObjectMapper = new DaoObjectMapper();

        public void Delete(LineItem lineItem, IConnectable connection)
        {
            Delete_Mark(lineItem, connection);

            DataRow row = daoObjectMapper.MapLineItem(lineItem);

            StringBuilder sql = new StringBuilder("DELETE FROM lineitem WHERE 1=1 ");
            sql.Append("AND lineitem_id = " + row["lineitem_id"] + " ");
            sql.Append("AND name " + (row["name"] != DBNull.Value ? "= '" + row["name"] + "' " : "IS NULL "));
            sql.Append("AND descript " + (row["descript"] != DBNull.Value ? "= '" + row["descript"] + "' " : "IS NULL "));
            sql.Append("AND assign_date " + (row["assign_date"] != DBNull.Value ? "= '" + row["assign_date"] + "' " : "IS NULL "));
            sql.Append("AND due_date " + (row["due_date"] != DBNull.Value ? "= '" + row["due_date"] + "' " : "IS NULL "));
            sql.Append("AND class_id " + (row["class_id"] != DBNull.Value ? "= " + row["class_id"] + " " : "IS NULL "));
            sql.Append("AND enum_category_id " + (row["enum_category_id"] != DBNull.Value ? "= " + row["enum_category_id"] + " " : "IS NULL "));
            sql.Append("AND markValueMin " + (row["markValueMin"] != DBNull.Value ? "= " + row["markValueMin"] + " " : "IS NULL "));
            sql.Append("AND markValueMax " + (row["markValueMax"] != DBNull.Value ? "= " + row["markValueMax"] + " " : "IS NULL "));

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
            DataRow row = daoObjectMapper.MapLineItem(lineItem);

            StringBuilder sql = new StringBuilder("SELECT * FROM lineItem WHERE 1=1 ");
            if (row["lineitem_id"] != DBNull.Value)
                sql.Append("AND lineItem_id = " + row["lineitem_id"] + " ");
            else
            {
                if (row["name"] != DBNull.Value)
                    sql.Append("AND name = '" + row["name"] + "' ");
                if (row["descript"] != DBNull.Value)
                    sql.Append("AND descript = '" + row["descript"] + "' ");
                if (row["assign_date"] != DBNull.Value)
                    sql.Append("AND assign_date = '" + row["assign_date"].ToString() + "' ");
                if (row["due_date"] != DBNull.Value)
                    sql.Append("AND due_date = '" + row["due_date"].ToString() + "' ");
                if (row["class_id"] != DBNull.Value)
                    sql.Append("AND class_id = " + row["class_id"] + " ");
                if (row["enum_category_id"] != DBNull.Value)
                    sql.Append("AND enum_category_id = " + row["enum_category_id"] + " ");
                if (row["markValueMin"] != DBNull.Value)
                    sql.Append("AND markValueMin = " + row["markValueMin"] + " ");
                if (row["markValueMax"] != DBNull.Value)
                    sql.Append("AND markValueMax = " + row["markValueMax"] + " ");
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
            DataTable dtLineItem = new DataTableFactory().Dt_Netus2_LineItem;

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                dtLineItem.Load(reader);                
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

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
            DataRow row = daoObjectMapper.MapLineItem(lineItem);

            if(row["lineitem_id"] != DBNull.Value)
            {
                StringBuilder sql = new StringBuilder("UPDATE lineItem SET ");
                sql.Append("name = " + (row["name"] != DBNull.Value ? "'" + row["name"] + "', " : "NULL, "));
                sql.Append("descript = " + (row["descript"] != DBNull.Value ? "'" + row["descript"] + "', " : "NULL, "));
                sql.Append("assign_date = " + (row["assign_date"] != DBNull.Value ? "'" + row["assign_date"].ToString() + "', " : "NULL, "));
                sql.Append("due_date = " + (row["due_date"] != DBNull.Value ? "'" + row["due_date"].ToString() + "', " : "NULL, "));
                sql.Append("class_id = " + (row["class_id"] != DBNull.Value ? row["class_id"] + ", " : "NULL, "));
                sql.Append("enum_category_id = " + (row["enum_category_id"] != DBNull.Value ? row["enum_category_id"] + ", " : "NULL, "));
                sql.Append("markValueMin = " + (row["markValueMin"] != DBNull.Value ? row["markValueMin"] + ", " : "NULL, "));
                sql.Append("markValueMax = " + (row["markValueMax"] != DBNull.Value ? row["markValueMax"] + ", " : "NULL, "));
                sql.Append("changed = GETDATE(), ");
                sql.Append("changed_by = 'Netus2' ");
                sql.Append("WHERE lineItem_id = " + row["lineitem_id"]);

                connection.ExecuteNonQuery(sql.ToString());
            }
            else
            {
                throw new Exception("The following LineItem needs to be inserted into the database, before it can be updated.\n" + lineItem.ToString());
            }
        }

        public LineItem Write(LineItem lineItem, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapLineItem(lineItem);

            StringBuilder sqlValues = new StringBuilder();
            sqlValues.Append(row["name"] != DBNull.Value ? "'" + row["name"] + "', " : "NULL, ");
            sqlValues.Append(row["descript"] != DBNull.Value ? "'" + row["descript"] + "', " : "NULL, ");
            sqlValues.Append(row["assign_date"] != DBNull.Value ? "'" + row["assign_date"].ToString() + "', " : "NULL, ");
            sqlValues.Append(row["due_date"] != DBNull.Value ? "'" + row["due_date"].ToString() + "', " : "NULL, ");
            sqlValues.Append(row["class_id"] != DBNull.Value ? row["class_id"] + ", " : "NULL, ");
            sqlValues.Append(row["enum_category_id"] != DBNull.Value ? row["enum_category_id"] + ", " : "NULL, ");
            sqlValues.Append(row["markValueMin"] != DBNull.Value ? row["markValueMin"] + ", " : "NULL, ");
            sqlValues.Append(row["markValueMax"] != DBNull.Value ? row["markValueMax"] + ", " : "NULL, ");
            sqlValues.Append("GETDATE(), ");
            sqlValues.Append("'Netus2'");

            string sql =
                "INSERT INTO lineItem " +
                "(name, descript, assign_date, due_date, class_id, enum_category_id, markValueMin, markValueMax, created, created_by) " +
                "VALUES (" + sqlValues.ToString() + ")";

            row["lineitem_id"] = connection.InsertNewRecord(sql);

            ClassEnrolled classEnrolled = Read_ClassEnrolled((int)row["class_id"], connection);
            LineItem result = daoObjectMapper.MapLineItem(row, classEnrolled);

            return result;
        }
    }
}
