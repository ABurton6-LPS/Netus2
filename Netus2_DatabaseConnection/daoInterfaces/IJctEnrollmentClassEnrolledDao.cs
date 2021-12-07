using Netus2_DatabaseConnection.dbAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IJctEnrollmentClassEnrolledDao
    {
        /// <summary>
        /// Deletes the link between the two values passed in.
        /// </summary>
        /// <param name="enrollmentId"></param>
        /// <param name="classEnrolledId"></param>
        /// <param name="connection"></param>
        public void Delete(int enrollmentId, int classEnrolledId, IConnectable connection);

        /// <summary>
        /// Queries the database for the link between the two values passed in.
        /// </summary>
        /// <param name="enrollmentId"></param>
        /// <param name="classEnrolledId"></param>
        /// <param name="connection"></param>
        /// <returns>Null, if no record is found.</returns>
        public DataRow Read(int enrollmentId, int classEnrolledId, IConnectable connection);

        /// <summary>
        /// Queries the database for the records associated with the passed-in data.
        /// </summary>
        /// <param name="enrollmentId"></param>
        /// <param name="connection"></param>
        /// <returns>Empty list, if no records are found.</returns>
        public List<DataRow> Read_AllWithEnrollmentId(int enrollmentId, IConnectable connection);

        /// <summary>
        /// Queries the database for the records associated with the passed-in data.
        /// </summary>
        /// <param name="classEnrolledId"></param>
        /// <param name="connection"></param>
        /// <returns>Empty list, if no records are found.</returns>
        public List<DataRow> Read_AllWithClassEnrolledId(int classEnrolledId, IConnectable connection);

        /// <summary>
        /// Queries the database for any records that are not in the temporary table.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns>Empty list, if no records are found.</returns>
        public List<DataRow> Read_AllClassEnrolledNotInTempTable(IConnectable connection);

        /// <summary>
        /// Checks to see if the provided data is associated to any record currently in the databse.
        /// If not, then writes this record to the database.
        /// If so, then updates the database record to match this object.
        /// </summary>
        /// <param name="enrollmentId"></param>
        /// <param name="classEnrolledId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="connection"></param>
        public void Update(int enrollmentId, int classEnrolledId, DateTime? startDate, DateTime? endDate, IConnectable connection);

        /// <summary>
        /// Writes a link between the provided data points to the database.
        /// </summary>
        /// <param name="enrollmentId"></param>
        /// <param name="classEnrolledId"></param>
        /// <param name="connection"></param>
        /// <returns>A copy of the record that was written.</returns>
        public DataRow Write(int enrollmentId, int classEnrolledId, DateTime? startDate, DateTime? endDate, IConnectable connection);


        /// <summary>
        /// Writes a link between the provided data points to the temporary table.
        /// </summary>
        /// <param name="enrollmentId"></param>
        /// <param name="class_enrolled_id"></param>
        /// <param name="connection"></param>
        public void Write_ToTempTable(int enrollmentId, int class_enrolled_id, IConnectable connection);
    }
}
