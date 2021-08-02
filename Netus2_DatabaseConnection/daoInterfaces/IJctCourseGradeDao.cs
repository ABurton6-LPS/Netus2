using Netus2_DatabaseConnection.daoObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IJctCourseGradeDao
    {
        /// <summary>
        /// Read the JctcourseGradeDaos from the database that have the provided courseId.
        /// </summary>
        /// <param name="courseId"/>
        /// <param name="connection"/>
        List<JctCourseGradeDao> Read(int courseId, IConnectable connection);

        /// <summary>
        /// Read the JctcourseGradeDao from the database that has the provided courseId and gradeId.
        /// </summary>
        /// <param name="courseId"/>
        /// <param name="gradeId"/>
        /// <param name="connection"/>
        JctCourseGradeDao Read(int courseId, int gradeId, IConnectable connection);

        /// <summary>
        /// Populates the database with a new JctcourseGradeDao record, linked to the provided
        /// courseId and gradeId.
        /// </summary>
        /// <param name="courseId"/>
        /// <param name="gradeId"/>
        /// <param name="connection"/>
        JctCourseGradeDao Write(int courseId, int gradeId, IConnectable connection);

        /// <summary>
        /// Deletes from the database, the JctCourseGradeDao which is linked to the
        /// courseId and gradeId provided.
        /// </summary>
        /// <param name="courseId"/>
        /// <param name="gradeId"/>
        /// <param name="connection"/>
        void Delete(int courseId, int gradeId, IConnectable connection);
    }
}
