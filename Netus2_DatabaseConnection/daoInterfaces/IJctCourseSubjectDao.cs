using Netus2.daoObjects;
using Netus2.dbAccess;
using System.Collections.Generic;

namespace Netus2.daoInterfaces
{
    public interface IJctCourseSubjectDao
    {
        /// <summary>
        /// Read the JctcourseSubjectDaos from the database that have the provided courseId.
        /// </summary>
        /// <param name="courseId"/>
        /// <param name="connection"/>
        List<JctCourseSubjectDao> Read(int courseId, IConnectable connection);

        /// <summary>
        /// Read the JctcourseSubjectDao from the database that has the provided courseId and subjectId.
        /// </summary>
        /// <param name="courseId"/>
        /// <param name="subjectId"/>
        /// <param name="connection"/>
        JctCourseSubjectDao Read(int courseId, int subjectId, IConnectable connection);

        /// <summary>
        /// Populates the database with a new JctcourseSubjectDao record, linked to the provided
        /// courseId and subjectId.
        /// </summary>
        /// <param name="courseId"/>
        /// <param name="subjectId"/>
        /// <param name="connection"/>
        JctCourseSubjectDao Write(int courseId, int subjectId, IConnectable connection);

        /// <summary>
        /// Deletes from the database, the JctCourseSubjectDao which is linked to the
        /// courseId and subjectId provided.
        /// </summary>
        /// <param name="courseId"/>
        /// <param name="subjectId"/>
        /// <param name="connection"/>
        void Delete(int courseId, int subjectId, IConnectable connection);
    }
}
