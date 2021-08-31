using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;
using System.Data;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IJctEnrollmentAcademicSessionDao
    {
        /// <summary>
        /// Read the JctEnrollmentAcademicSessionDao from the database that has the provided enrollmentId and academicSessionId.
        /// </summary>
        /// <param name="enrollmentId"/>
        /// <param name="academicSessionId"/>
        /// <param name="connection"/>
        DataRow Read(int enrollmentId, int academicSessionId, IConnectable connection);

        /// <summary>
        /// Read the JctEnrollmentAcademicSessionDao from the database that have the provided enrollmentId.
        /// </summary>
        /// <param name="enrollmentId"/>
        /// <param name="connection"/>
        List<DataRow> Read_WithEnrollmentId(int enrollmentId, IConnectable connection);

        /// <summary>
        /// Read the JctEnrollmentAcademicSessionDao from the database that have the provided academicSessionId.
        /// </summary>
        /// <param name="enrollmentId"/>
        /// <param name="connection"/>
        List<DataRow> Read_WithAcademicSessionId(int academicSessionId, IConnectable connection);

        /// <summary>
        /// Populates the database with a new JctEnrollmentAcademicSessionDao record, linked to the provided
        /// enrollmentId and academicSessionId.
        /// </summary>
        /// <param name="enrollmentId"/>
        /// <param name="academicSessionId"/>
        /// <param name="connection"/>
        DataRow Write(int enrollmentId, int academicSessionId, IConnectable connection);

        /// <summary>
        /// Deletes from the database, the JctEnrollmentAcademicSessionDao which is linked to the
        /// enrollmentId and academicSessionId provided.
        /// <param name="enrollmentId"/>
        /// <param name="academicSessionId"/>
        /// <param name="connection"/>
        void Delete(int enrollmentId, int academicSessionId, IConnectable connection);
    }
}
