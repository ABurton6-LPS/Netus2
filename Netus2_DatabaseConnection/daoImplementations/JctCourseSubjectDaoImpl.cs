using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Netus2_DatabaseConnection.daoImplementations
{
    public class JctCourseSubjectDaoImpl : IJctCourseSubjectDao
    {
        public void Delete(int courseId, int subjectId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("DELETE FROM jct_course_subject WHERE 1=1 ");
            sql.Append("AND course_id = " + courseId + " ");
            sql.Append("AND enum_subject_id = " + subjectId);

            connection.ExecuteNonQuery(sql.ToString());
        }

        public DataRow Read(int courseId, int subjectId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM jct_course_subject WHERE 1=1 ");
            sql.Append("AND course_id = " + courseId + " ");
            sql.Append("AND enum_subject_id = " + subjectId);

            List<DataRow> results = Read(sql.ToString(), connection);
            if (results.Count == 1)
                return results[0];
            else if (results.Count == 0)
                return null;
            else
                throw new Exception("The jct_course_subject table contains a duplicate record.\n" +
                    "course_id = " + courseId + ", subject_id = " + subjectId);
        }

        public List<DataRow> Read(int courseId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM jct_course_subject WHERE 1=1 ");
            sql.Append("AND course_id = " + courseId);

            return Read(sql.ToString(), connection);
        }

        private List<DataRow> Read(string sql, IConnectable connection)
        {
            DataTable dtJctCourseSubject = DataTableFactory.Dt_Netus2_JctCourseSubject;
            dtJctCourseSubject = connection.ReadIntoDataTable(sql, dtJctCourseSubject);

            List<DataRow> jctCourseSubjectDaos = new List<DataRow>();
            foreach (DataRow row in dtJctCourseSubject.Rows)
            {
                jctCourseSubjectDaos.Add(row);
            }

            return jctCourseSubjectDaos;
        }

        public DataRow Write(int courseId, int subjectId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("INSERT INTO jct_course_subject (course_id, enum_subject_id) VALUES (");
            sql.Append(courseId + ", ");
            sql.Append(subjectId + ")");

            connection.ExecuteNonQuery(sql.ToString());

            DataRow jctCourseSubjectDao = DataTableFactory.Dt_Netus2_JctCourseSubject.NewRow();
            jctCourseSubjectDao["course_id"] = courseId;
            jctCourseSubjectDao["enum_subject_id"] = subjectId;

            return jctCourseSubjectDao;
        }
    }
}
