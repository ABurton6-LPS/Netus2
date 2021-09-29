using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Netus2_DatabaseConnection.daoImplementations
{
    public class JctClassPersonDaoImpl : IJctClassPersonDao
    {
        public void Delete(int classId, int personId, IConnectable connection)
        {
            string sql = "DELETE FROM jct_class_person WHERE class_id = " + classId +
                " AND person_id = " + personId;

            connection.ExecuteNonQuery(sql);
        }

        public DataRow Read(int classId, int personId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_class_person WHERE class_id = " + classId +
                " AND person_id = " + personId;

            List<DataRow> result = Read(sql, connection);
            if (result.Count > 0)
                return result[0];
            else
                return null;
        }

        public List<DataRow> Read_WithClassId(int classId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_class_person WHERE class_id = " + classId;

            return Read(sql, connection);
        }

        public List<DataRow> Read_WithPersonId(int personId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_class_person WHERE person_id = " + personId;

            return Read(sql, connection);
        }

        private List<DataRow> Read(string sql, IConnectable connection)
        {
            DataTable dtJctClassPerson = DataTableFactory.CreateDataTable_Netus2_JctClassPerson();
            dtJctClassPerson = connection.ReadIntoDataTable(sql, dtJctClassPerson);

            List<DataRow> jctClassPersonDaos = new List<DataRow>();
            foreach (DataRow row in dtJctClassPerson.Rows)
            {
                jctClassPersonDaos.Add(row);
            }

            return jctClassPersonDaos;
        }

        public DataRow Write(int classId, int personId, int roleId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("INSERT INTO jct_class_person (class_id, person_id, enum_role_id) VALUES (");
            sql.Append(classId + ", ");
            sql.Append(personId + ", ");
            sql.Append(roleId + ")");

            connection.ExecuteNonQuery(sql.ToString());

            DataRow jctClassPersonDao = DataTableFactory.CreateDataTable_Netus2_JctClassPerson().NewRow();
            jctClassPersonDao["class_id"] = classId;
            jctClassPersonDao["person_id"] = personId;
            jctClassPersonDao["enum_role_id"] = roleId;

            return jctClassPersonDao;
        }
    }
}
